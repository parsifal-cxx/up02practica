using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LibraryAIS
{
    public partial class LoginForm : Form
    {
        private int failedAttempts = 0;
        private CaptchaGenerator captchaGenerator;
        private string currentCaptcha = "";
        private int blockTimeLeft = 0;
        private Timer blockTimer;

        public LoginForm()
        {
            InitializeComponent();
            captchaGenerator = new CaptchaGenerator();

            // Инициализация таймера блокировки
            blockTimer = new Timer();
            blockTimer.Interval = 1000; // 1 секунда
            blockTimer.Tick += BlockTimer_Tick;
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

                    // Блокировка на 10 секунд
                    StartBlockTimer();
                    return;
                }
            }

            // Попытка авторизации
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

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
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

                // Открываем главную форму
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.ShowDialog();

                // После закрытия главной формы - показываем форму авторизации снова
                CurrentUser.Clear();
                txtLogin.Clear();
                txtPassword.Clear();
                txtCaptcha.Clear();
                this.Show();
            }
            else
            {
                // Неудачная попытка
                failedAttempts++;
                MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // После первой неудачной попытки показываем CAPTCHA
                if (failedAttempts == 1)
                {
                    ShowCaptcha();
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
                else if (failedAttempts > 1)
                {
                    // Блокировка на 10 секунд
                    StartBlockTimer();
                }
            }
        }

        private void ShowCaptcha()
        {
            captchaPanel.Visible = true;
            GenerateNewCaptcha();

            // Изменяем размер формы
            this.Height = 410;
            panelMain.Height = 410;
        }

        private void HideCaptcha()
        {
            captchaPanel.Visible = false;
            txtCaptcha.Clear();

            // Возвращаем исходный размер формы
            this.Height = 290;
            panelMain.Height = 290;
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

            // Блокируем элементы управления
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
                // Разблокировка
                blockTimer.Stop();
                lblBlockMessage.Visible = false;

                txtLogin.Enabled = true;
                txtPassword.Enabled = true;
                txtCaptcha.Enabled = true;
                btnLogin.Enabled = true;
                btnRefreshCaptcha.Enabled = true;

                // Генерируем новую CAPTCHA
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
                Application.Exit();
            }
        }

        private void pictureBoxCaptcha_Click(object sender, EventArgs e)
        {

        }
    }
}