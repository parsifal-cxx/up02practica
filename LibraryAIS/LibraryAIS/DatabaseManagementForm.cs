using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LibraryAIS
{
    public partial class DatabaseManagementForm : Form
    {
        public DatabaseManagementForm()
        {
            InitializeComponent();
        }

        private void DatabaseManagementForm_Load(object sender, EventArgs e)
        {
            LoadTables();
        }

        private void LoadTables()
        {
            try
            {
                string query = "SHOW TABLES";
                DataTable tables = DatabaseHelper.ExecuteQuery(query);

                cmbTables.Items.Clear();
                cmbTables.Items.Add("-- Выберите таблицу --");

                foreach (DataRow row in tables.Rows)
                {
                    cmbTables.Items.Add(row[0].ToString());
                }

                cmbTables.SelectedIndex = 0;
            }
            catch (Exception)
            {
                cmbTables.Items.Clear();
                cmbTables.Items.Add("-- БД не инициализирована --");
                cmbTables.SelectedIndex = 0;
            }
        }

        private void btnRestoreStructure_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "ВНИМАНИЕ! Эта операция выполнит следующие действия:\n\n" +
                "1. Удалит все существующие таблицы\n" +
                "2. Удалит все данные из базы данных\n" +
                "3. Создаст новую структуру БД\n\n" +
                "ВСЕ ДАННЫЕ БУДУТ БЕЗВОЗВРАТНО ПОТЕРЯНЫ!\n\n" +
                "Вы уверены, что хотите продолжить?",
                "Подтверждение восстановления структуры БД",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnRestoreStructure.Enabled = false;
                btnImport.Enabled = false;

                string sqlFilePath = Path.Combine(Application.StartupPath, "database_structure.sql");

                if (!File.Exists(sqlFilePath))
                {
                    MessageBox.Show(
                        "Файл database_structure.sql не найден!\n\n" +
                        $"Ожидаемый путь: {sqlFilePath}\n\n" +
                        "Убедитесь, что файл находится в папке с программой.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                string sqlScript = File.ReadAllText(sqlFilePath, Encoding.UTF8);

                string[] commands = sqlScript.Split(new[] { ";\r\n", ";\n", ";" }, StringSplitOptions.RemoveEmptyEntries);

                int successCount = 0;
                int errorCount = 0;
                StringBuilder errors = new StringBuilder();

                foreach (string command in commands)
                {
                    string trimmedCommand = command.Trim();

                    if (string.IsNullOrWhiteSpace(trimmedCommand) ||
                        trimmedCommand.StartsWith("--") ||
                        trimmedCommand.StartsWith("/*"))
                        continue;

                    try
                    {
                        DatabaseHelper.ExecuteNonQuery(trimmedCommand);
                        successCount++;
                    }
                    catch (MySqlException ex)
                    {
                        errorCount++;
                        errors.AppendLine($"Команда: {trimmedCommand.Substring(0, Math.Min(50, trimmedCommand.Length))}...");
                        errors.AppendLine($"Ошибка: {ex.Message}");
                        errors.AppendLine();
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        errors.AppendLine($"Неожиданная ошибка: {ex.Message}");
                        errors.AppendLine();
                    }
                }

                LoadTables();

                string message = "Восстановление структуры БД завершено!\n\n" +
                                $"✓ Успешно выполнено команд: {successCount}\n" +
                                $"✗ Ошибок: {errorCount}\n\n";

                if (errorCount > 0 && errorCount <= 5)
                {
                    message += "Ошибки:\n" + errors.ToString();
                }
                else if (errorCount > 5)
                {
                    message += "(Слишком много ошибок для отображения)\n\n" +
                              "Проверьте подключение к БД и корректность SQL скрипта.";
                }
                else
                {
                    message += "Теперь можно импортировать данные из CSV файлов.";
                }

                MessageBox.Show(
                    message,
                    "Результат восстановления",
                    MessageBoxButtons.OK,
                    errorCount == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Критическая ошибка при восстановлении структуры БД:\n\n" + ex.Message +
                    "\n\nПроверьте:\n" +
                    "• Подключение к серверу MySQL\n" +
                    "• Права доступа пользователя БД\n" +
                    "• Корректность файла database_structure.sql",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnRestoreStructure.Enabled = true;
                btnImport.Enabled = true;
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Функция будет реализована в следующем этапе",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Функция будет реализована в следующем этапе",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}