using System;
using System.Data;
using System.IO;
using System.Linq;
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
            // Загружаем список таблиц
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

                // Читаем SQL скрипт из файла
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

                // Разделяем скрипт на отдельные команды
                string[] commands = sqlScript.Split(new[] { ";\r\n", ";\n", ";" }, StringSplitOptions.RemoveEmptyEntries);

                int successCount = 0;
                int errorCount = 0;
                StringBuilder errors = new StringBuilder();

                foreach (string command in commands)
                {
                    string trimmedCommand = command.Trim();

                    // Пропускаем комментарии и пустые строки
                    if (string.IsNullOrWhiteSpace(trimmedCommand) ||
                        trimmedCommand.StartsWith("--") ||
                        trimmedCommand.StartsWith("/*"))
                        continue;

                    try
                    {
                        DatabaseHelper.ExecuteNonQueryCreateDB(trimmedCommand);
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

                // Обновляем список таблиц
                LoadTables();

                // Формируем сообщение о результате
                string message = "Восстановление структуры БД завершено!\n\n" +
                                $"Успешно выполнено команд: {successCount}\n" +
                                $"Ошибок: {errorCount}\n\n";

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
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*",
                Title = "Выберите CSV файл для импорта",
                CheckFileExists = true,
                CheckPathExists = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            // Проверка выбора таблицы
            if (cmbTables.SelectedIndex <= 0 || cmbTables.Text.Contains("--"))
            {
                MessageBox.Show(
                    "Выберите таблицу для импорта данных!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbTables.Focus();
                return;
            }

            // Проверка выбора файла
            if (string.IsNullOrWhiteSpace(txtFilePath.Text) || !File.Exists(txtFilePath.Text))
            {
                MessageBox.Show(
                    "Выберите существующий CSV файл!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                btnSelectFile.Focus();
                return;
            }

            string tableName = cmbTables.Text;
            string filePath = txtFilePath.Text;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnImport.Enabled = false;
                btnSelectFile.Enabled = false;
                cmbTables.Enabled = false;

                // Получаем структуру таблицы
                string describeQuery = $"DESCRIBE `{tableName}`";
                DataTable tableStructure = DatabaseHelper.ExecuteQuery(describeQuery);

                // Определяем колонки (кроме AUTO_INCREMENT)
                var columns = tableStructure.AsEnumerable()
                    .Where(row => !row["Extra"].ToString().Contains("auto_increment"))
                    .Select(row => row["Field"].ToString())
                    .ToList();

                int expectedColumnCount = columns.Count;

                // Читаем CSV файл
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                if (lines.Length == 0)
                {
                    MessageBox.Show(
                        "CSV файл пуст!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                int importedCount = 0;
                int errorCount = 0;
                StringBuilder errors = new StringBuilder();

                // Определяем начальную строку
                bool hasHeader = chkHasHeader.Checked;
                int startIndex = hasHeader ? 1 : 0;

                // Если есть заголовок - проверяем его
                if (hasHeader && lines.Length > 0)
                {
                    string[] headerColumns = lines[0].Split(';');
                    if (headerColumns.Length != expectedColumnCount)
                    {
                        DialogResult headerWarning = MessageBox.Show(
                            $"Количество колонок в заголовке ({headerColumns.Length}) не совпадает\n" +
                            $"с ожидаемым количеством ({expectedColumnCount}).\n\n" +
                            $"Ожидаемые колонки:\n{string.Join(", ", columns)}\n\n" +
                            "Продолжить импорт?",
                            "Предупреждение",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (headerWarning == DialogResult.No)
                            return;
                    }
                }

                // Импорт данных
                for (int i = startIndex; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Разбиваем строку по разделителю
                    string[] values = line.Split(';');

                    // Проверка количества значений
                    if (values.Length != expectedColumnCount)
                    {
                        errorCount++;
                        errors.AppendLine($"Строка {i + 1}: Ожидается {expectedColumnCount} значений, получено {values.Length}");

                        if (errorCount <= 10) // Показываем первые 10 ошибок
                        {
                            errors.AppendLine($"  Содержимое: {line.Substring(0, Math.Min(100, line.Length))}...");
                        }
                        continue;
                    }

                    try
                    {
                        // Формируем INSERT запрос
                        string columnsList = string.Join(", ", columns.Select(c => $"`{c}`"));
                        string valuesList = string.Join(", ", values.Select((v, idx) => "@param" + idx));

                        string insertQuery = $"INSERT INTO `{tableName}` ({columnsList}) VALUES ({valuesList})";

                        // Создаем параметры
                        MySqlParameter[] parameters = values.Select((v, idx) =>
                        {
                            string value = v.Trim();
                            // Обработка пустых значений как NULL
                            if (string.IsNullOrEmpty(value))
                                return new MySqlParameter("@param" + idx, DBNull.Value);
                            else
                                return new MySqlParameter("@param" + idx, value);
                        }).ToArray();

                        DatabaseHelper.ExecuteNonQuery(insertQuery, parameters);
                        importedCount++;
                    }
                    catch (MySqlException ex)
                    {
                        errorCount++;
                        if (errorCount <= 10)
                        {
                            errors.AppendLine($"Строка {i + 1}: {ex.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        if (errorCount <= 10)
                        {
                            errors.AppendLine($"Строка {i + 1}: {ex.Message}");
                        }
                    }
                }

                // Формирование отчета
                string message = $"Импорт завершен!\n\n" +
                                $"Успешно импортировано записей: {importedCount}\n" +
                                $"Ошибок: {errorCount}\n" +
                                $"Таблица: {tableName}\n\n";

                if (errorCount > 0 && errorCount <= 10)
                {
                    message += "Первые ошибки:\n" + errors.ToString();
                }
                else if (errorCount > 10)
                {
                    message += $"(Показаны первые 10 из {errorCount} ошибок)\n\n" + errors.ToString();
                }

                MessageBox.Show(
                    message,
                    "Результат импорта",
                    MessageBoxButtons.OK,
                    errorCount == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                // Очищаем поле пути после успешного импорта
                if (importedCount > 0)
                {
                    txtFilePath.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при импорте данных:\n\n" + ex.Message +
                    "\n\nПроверьте:\n" +
                    "• Формат CSV файла (разделитель - точка с запятой)\n" +
                    "• Соответствие количества колонок\n" +
                    "• Корректность данных",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnImport.Enabled = true;
                btnSelectFile.Enabled = true;
                cmbTables.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}