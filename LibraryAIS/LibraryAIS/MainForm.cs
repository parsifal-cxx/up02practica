using System;
using System.Windows.Forms;
using LibraryAIS.Properties;

namespace LibraryAIS
{
    public partial class MainForm : Form
    {
        private InactivityTracker inactivityTracker;

        public MainForm()
        {
            InitializeComponent();

            // Инициализация трекера неактивности
            InitializeInactivityTracker();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Отображение информации о пользователе в статус-баре
            statusUser.Text = "Пользователь: " + CurrentUser.FullName;
            statusRole.Text = "Роль: " + CurrentUser.RoleName;

            ConfigureMenuByRole();

            // Запускаем трекер неактивности
            StartInactivityTracker();
        }

        private void InitializeInactivityTracker()
        {
            // Проверяем, включена ли блокировка в настройках
            if (Settings.Default.EnableInactivityLock)
            {
                int timeout = Settings.Default.InactivityTimeout;

                // Создаем трекер с заданным таймаутом
                inactivityTracker = new InactivityTracker(timeout);

                // Подписываемся на событие обнаружения неактивности
                inactivityTracker.InactivityDetected += InactivityTracker_InactivityDetected;
            }
        }

   
        private void StartInactivityTracker()
        {
            if (inactivityTracker != null && Settings.Default.EnableInactivityLock)
            {
                inactivityTracker.Start();
            }
        }


        private void StopInactivityTracker()
        {
            if (inactivityTracker != null)
            {
                inactivityTracker.Stop();
            }
        }

        private void InactivityTracker_InactivityDetected(object sender, EventArgs e)
        {
            // Останавливаем трекер
            StopInactivityTracker();

            // Показываем сообщение пользователю
            MessageBox.Show(
                "Сеанс работы завершен из-за длительного отсутствия активности.\n\n" +
                "Для продолжения работы необходимо повторно войти в систему.",
                "Сеанс завершен",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Выполняем блокировку системы
            LockSystem();
        }


        private void LockSystem()
        {
            // Скрываем главную форму
            this.Hide();

            // Сохраняем информацию о предыдущем пользователе
            string previousLogin = CurrentUser.Login;
            string previousUserName = CurrentUser.FullName;

            // Очищаем данные текущего пользователя
            CurrentUser.Clear();

            // Создаем и показываем форму авторизации (с флагом повторного входа)
            LoginForm loginForm = new LoginForm(true); // true = повторный вход
            DialogResult result = loginForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Пользователь успешно авторизовался

                // Проверяем, тот же ли пользователь вошел
                if (CurrentUser.Login != previousLogin)
                {
                    MessageBox.Show(
                        $"Внимание! Вошел другой пользователь: {CurrentUser.FullName}\n\n" +
                        $"Предыдущий пользователь: {previousUserName}\n" +
                        "Интерфейс будет обновлен в соответствии с правами доступа.",
                        "Смена пользователя",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // Обновляем информацию о пользователе
                UpdateUserInfo();

                // Перенастраиваем меню
                ConfigureMenuByRole();

                // Переинициализируем и перезапускаем трекер
                if (inactivityTracker != null)
                {
                    inactivityTracker.Dispose();
                }
                InitializeInactivityTracker();
                StartInactivityTracker();

                // Показываем главную форму
                this.Show();
            }
            else
            {
                // Пользователь отменил вход - закрываем приложение
                MessageBox.Show(
                    "Работа с системой завершена.",
                    "Выход из системы",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Application.Exit();
            }
        }


        private void UpdateUserInfo()
        {
            statusUser.Text = "Пользователь: " + CurrentUser.FullName;
            statusRole.Text = "Роль: " + CurrentUser.RoleName;
        }

        private void ConfigureMenuByRole()
        {
            // Сначала скрываем всё
            menuBooks.Visible = false;
            menuBorrowings.Visible = false;
            menuStudents.Visible = false;
            menuCatalogs.Visible = false;
            menuUsers.Visible = false;
            menuReports.Visible = false;
            TimerMenu.Visible = false;

            // Скрываем подпункты справочников по умолчанию
            menuAuthors.Visible = false;
            menuCategories.Visible = false;
            menuPublishers.Visible = false;
            menuGroups.Visible = false;

            if (CurrentUser.IsAdmin)
            {
                // Администратор
                menuUsers.Visible = true;
                menuStudents.Visible = true;

                // Админ видит только справочник групп
                menuCatalogs.Visible = true;
                menuGroups.Visible = true;
                TimerMenu.Visible = true; // Настройки доступны только администратору
            }
            else if (CurrentUser.IsLibrarian)
            {
                // Библиотекарь
                menuBorrowings.Visible = true;
                menuBooks.Visible = true;
                menuStudents.Visible = true;
                // Справочники ему не нужны
            }
            else if (CurrentUser.IsHeadLibrarian)
            {
                // Заведующий библиотекой
                menuBooks.Visible = true;
                menuReports.Visible = true;

                // Заведующий видит все справочники, КРОМЕ ГРУПП
                menuCatalogs.Visible = true;
                menuAuthors.Visible = true;
                menuCategories.Visible = true;
                menuPublishers.Visible = true;
            }
        }

        private void menuBooks_Click(object sender, EventArgs e)
        {
            BooksForm booksForm = new BooksForm();
            booksForm.ShowDialog();
        }

        private void menuBorrowings_Click(object sender, EventArgs e)
        {
            BorrowingsForm borrowingsForm = new BorrowingsForm();
            borrowingsForm.ShowDialog();
        }

        private void menuStudents_Click(object sender, EventArgs e)
        {
            StudentsForm studentsForm = new StudentsForm();
            studentsForm.ShowDialog();
        }

        private void menuAuthors_Click(object sender, EventArgs e)
        {
            AuthorsForm authorsForm = new AuthorsForm();
            authorsForm.ShowDialog();
        }

        private void menuCategories_Click(object sender, EventArgs e)
        {
            CategoriesForm categoriesForm = new CategoriesForm();
            categoriesForm.ShowDialog();
        }

        private void menuPublishers_Click(object sender, EventArgs e)
        {
            PublishersForm publishersForm = new PublishersForm();
            publishersForm.ShowDialog();
        }

        private void menuGroups_Click(object sender, EventArgs e)
        {
            GroupsForm groupsForm = new GroupsForm();
            groupsForm.ShowDialog();
        }

        private void menuUsers_Click(object sender, EventArgs e)
        {
            UsersForm usersForm = new UsersForm();
            usersForm.ShowDialog();
        }

        private void menuReports_Click(object sender, EventArgs e)
        {
            ReportsForm reportsForm = new ReportsForm();
            reportsForm.ShowDialog();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!InputHelper.ConfirmClose())
            {
                e.Cancel = true;
                return;
            }

            StopInactivityTracker();

            if (inactivityTracker != null)
            {
                inactivityTracker.Dispose();
                inactivityTracker = null;
            }

            CurrentUser.Clear();
        }

        private void TimerMenu_Click(object sender, EventArgs e)
        {
            // Временно останавливаем трекер
            bool wasTrackerRunning = inactivityTracker != null && inactivityTracker.IsEnabled;
            if (wasTrackerRunning)
            {
                StopInactivityTracker();
            }
            SettingsForm settingsForm = new SettingsForm();
            DialogResult result = settingsForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                MessageBox.Show(
                    "Новые настройки блокировки вступят в силу при следующем входе в систему.",
                    "Информация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            if (wasTrackerRunning)
            {
                StartInactivityTracker();
            }
        }
    }
}