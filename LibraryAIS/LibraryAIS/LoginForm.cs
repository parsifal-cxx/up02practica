using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using LibraryAIS.Properties;

namespace LibraryAIS
{
    public partial class LoginForm : Form
    {
        private int failedAttempts = 0;
        private CaptchaGenerator captchaGenerator;
        private string currentCaptcha = "";
        private int blockTimeLeft = 0;
        private Timer blockTimer;
        private bool isReturningFromMainForm = false;

        public LoginForm()
        {
            InitializeComponent();
            captchaGenerator = new CaptchaGenerator();

            // Инициализация таймера блокировки
            blockTimer = new Timer();
            blockTimer.Interval = 1000; // 1 секунда
            blockTimer.Tick += BlockTimer_Tick;
        }

        public LoginForm(bool isReturning) : this()
        {
            this.isReturningFromMainForm = isReturning;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (isReturningFromMainForm)
            {
                lblTitle.Text = "Повторная авторизация";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Проверка заполнения полей
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Введите логин!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (txtLogin.Text.Trim().Equals(Settings.Default.DefaultAdminLogin, StringComparison.OrdinalIgnoreCase) &&
                txtPassword.Text == Settings.Default.DefaultAdminPassword)
            {
                // Вход как специальный администратор для управления БД
                CurrentUser.UserID = -1; // Специальный ID
                CurrentUser.Login = "admin";
                CurrentUser.Name = "Системный";
                CurrentUser.Surname = "Администратор";
                CurrentUser.Patronymic = "";
                CurrentUser.RoleID = 1; // Администратор
                CurrentUser.RoleName = "Администратор";

                // Сброс счетчика и скрытие CAPTCHA
                failedAttempts = 0;
                HideCaptcha();

                // Если это повторный вход после блокировки - просто закрываем форму
                if (isReturningFromMainForm)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }

                // Открываем форму управления БД
                this.Hide();
                DatabaseManagementForm dbForm = new DatabaseManagementForm();

                if (dbForm.ShowDialog() == DialogResult.OK)
                {
                    // После работы с БД возвращаемся к обычной авторизации
                    CurrentUser.Clear();
                    txtLogin.Clear();
                    txtPassword.Clear();
                    HideCaptcha();
                    this.Show();
                    txtLogin.Focus();
                }
                else
                {
                    // Пользователь закрыл форму управления БД - выход
                    Application.Exit();
                }
                return;
            }


            // Проверка CAPTCHA если она активна
            if (captchaPanel.Visible)
            {
                if (string.IsNullOrWhiteSpace(txtCaptcha.Text))
                {
                    MessageBox.Show("Введите код с картинки!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCaptcha.Focus();
                    return;
                }

                if (!txtCaptcha.Text.Trim().Equals(currentCaptcha, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Неверный код с картинки!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StartBlockTimer();
                    return;
                }
            }

            // Попытка авторизации из БД
            string query = @"SELECT u.UserID, u.Login, u.Name, u.Surname, u.Patronymic, 
                            u.Role, r.Name as RoleName 
                            FROM users u 
                            LEFT JOIN roles r ON u.Role = r.RoleID 
                            WHERE u.Login = @login AND u.Password = @password";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@login", txtLogin.Text.Trim()),
                new MySqlParameter("@password", txtPassword.Text)
            };

            DataTable result = null;

            try
            {
                result = DatabaseHelper.ExecuteQuery(query, parameters);
            }
            catch (MySqlException ex)
            {
                // Ошибка подключения к БД
                MessageBox.Show(
                    "Ошибка подключения к базе данных!\n\n" +
                    $"Детали ошибки: {ex.Message}\n\n" +
                    "Возможные причины:\n" +
                    "• База данных не создана\n" +
                    "• Структура БД не восстановлена\n" +
                    "• Сервер MySQL недоступен\n\n" +
                    "Для восстановления базы данных используйте:\n" +
                    "Логин: admin\n" +
                    "Пароль: admin",
                    "Ошибка подключения к БД",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                // Другие ошибки
                MessageBox.Show(
                    "Произошла непредвиденная ошибка:\n\n" + ex.Message +
                    "\n\nДля восстановления БД используйте admin/admin",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (result != null && result.Rows.Count > 0)
            {
                // Успешная авторизация
                DataRow user = result.Rows[0];

                CurrentUser.UserID = Convert.ToInt32(user["UserID"]);
                CurrentUser.Login = user["Login"].ToString();
                CurrentUser.Name = user["Name"].ToString();
                CurrentUser.Surname = user["Surname"].ToString();
                CurrentUser.Patronymic = user["Patronymic"].ToString();
                CurrentUser.RoleID = Convert.ToInt32(user["Role"]);
                CurrentUser.RoleName = user["RoleName"].ToString();

                // Сброс счетчика и скрытие CAPTCHA
                failedAttempts = 0;
                HideCaptcha();

                if (isReturningFromMainForm)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Hide();

                MainForm mainForm = new MainForm();
                mainForm.ShowDialog();

                if (mainForm.DialogResult != DialogResult.OK)
                {
                    Application.Exit();
                }
                else
                {
                    CurrentUser.Clear();
                    txtLogin.Clear();
                    txtPassword.Clear();
                    txtCaptcha.Clear();
                    HideCaptcha();
                    this.Show();
                    txtLogin.Focus();
                }
            }
            else
            {
                // Неудачная попытка
                failedAttempts++;
                MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (failedAttempts == 1)
                {
                    ShowCaptcha();
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
                else if (failedAttempts > 1)
                {
                    StartBlockTimer();
                }
            }
        }

        private void ShowCaptcha()
        {
            captchaPanel.Visible = true;
            GenerateNewCaptcha();

            this.ClientSize = new Size(532, 460);
        }

        private void HideCaptcha()
        {
            captchaPanel.Visible = false;
            txtCaptcha.Clear();

            this.ClientSize = new Size(532, 309);
        }

        private void GenerateNewCaptcha()
        {
            Bitmap captchaImage = captchaGenerator.GenerateCaptchaImage(300, 80);
            currentCaptcha = captchaGenerator.CaptchaText;
            pictureBoxCaptcha.Image = captchaImage;
            txtCaptcha.Clear();
            txtCaptcha.Focus();
        }

        private void btnRefreshCaptcha_Click(object sender, EventArgs e)
        {
            GenerateNewCaptcha();
        }

        private void StartBlockTimer()
        {
            blockTimeLeft = 10;
            lblBlockMessage.Text = $"Вход заблокирован на {blockTimeLeft} секунд...";
            lblBlockMessage.Visible = true;

            txtLogin.Enabled = false;
            txtPassword.Enabled = false;
            txtCaptcha.Enabled = false;
            btnLogin.Enabled = false;
            btnRefreshCaptcha.Enabled = false;

            blockTimer.Start();
        }

        private void BlockTimer_Tick(object sender, EventArgs e)
        {
            blockTimeLeft--;

            if (blockTimeLeft > 0)
            {
                lblBlockMessage.Text = $"Вход заблокирован на {blockTimeLeft} секунд...";
            }
            else
            {
                blockTimer.Stop();
                lblBlockMessage.Visible = false;

                txtLogin.Enabled = true;
                txtPassword.Enabled = true;
                txtCaptcha.Enabled = true;
                btnLogin.Enabled = true;
                btnRefreshCaptcha.Enabled = true;

                GenerateNewCaptcha();

                txtLogin.Clear();
                txtPassword.Clear();
                txtCaptcha.Clear();
                txtLogin.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из программы?",
                "Подтверждение выхода", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (isReturningFromMainForm)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void pictureBoxCaptcha_Click(object sender, EventArgs e)
        {
        }
    }
}