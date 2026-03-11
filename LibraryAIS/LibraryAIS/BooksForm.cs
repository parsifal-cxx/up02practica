using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LibraryAIS
{
    public partial class BooksForm : Form
    {
        private DataTable booksTable;
        private PaginationHelper pagination;

        // Константы для условного форматирования
        private const int CRITICAL_STOCK_LEVEL = 3;  // Критический остаток
        private const int WARNING_STOCK_LEVEL = 10;  // Предупреждение

        public BooksForm()
        {
            InitializeComponent();
            pagination = new PaginationHelper(20); // 20 записей на страницу

            // Подписываемся на событие завершения привязки данных
            dgvBooks.DataBindingComplete += DgvBooks_DataBindingComplete;
        }

        private void BooksForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadPublishers();
            LoadSortOptions();
            LoadBooks();
            ConfigureButtonsByRole();
        }

        private void ConfigureButtonsByRole()
        {
            // Библиотекарь может только просматривать
            if (CurrentUser.IsLibrarian)
            {
                btnPurchase.Visible = false;
                btnWriteOff.Visible = false;
                btnEdit.Visible = false;
            }
        }

        private void LoadCategories()
        {
            string query = "SELECT CategoryID, Name FROM categories ORDER BY Name";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            DataRow emptyRow = dt.NewRow();
            emptyRow["CategoryID"] = 0;
            emptyRow["Name"] = "Все категории";
            dt.Rows.InsertAt(emptyRow, 0);

            cmbFilterCategory.DataSource = dt;
            cmbFilterCategory.DisplayMember = "Name";
            cmbFilterCategory.ValueMember = "CategoryID";
            cmbFilterCategory.SelectedIndex = 0;
        }

        private void LoadPublishers()
        {
            string query = "SELECT PublisherID, Name FROM publishers ORDER BY Name";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            DataRow emptyRow = dt.NewRow();
            emptyRow["PublisherID"] = 0;
            emptyRow["Name"] = "Все издательства";
            dt.Rows.InsertAt(emptyRow, 0);

            cmbFilterPublisher.DataSource = dt;
            cmbFilterPublisher.DisplayMember = "Name";
            cmbFilterPublisher.ValueMember = "PublisherID";
            cmbFilterPublisher.SelectedIndex = 0;
        }

        private void LoadSortOptions()
        {
            cmbSort.Items.Clear();
            cmbSort.Items.Add("Без сортировки");
            cmbSort.Items.Add("По названию (А-Я)");
            cmbSort.Items.Add("По названию (Я-А)");
            cmbSort.Items.Add("По году (сначала новые)");
            cmbSort.Items.Add("По году (сначала старые)");
            cmbSort.SelectedIndex = 0;
        }

        /// <summary>
        /// Загрузка книг с учетом пагинации
        /// </summary>
        private void LoadBooks()
        {
            // Сбрасываем на первую страницу
            pagination.Reset();

            // Подсчитываем общее количество записей
            UpdateTotalRecords();

            // Загружаем данные текущей страницы
            LoadPageData();

            // Обновляем элементы управления пагинацией
            UpdatePaginationControls();
        }

        /// <summary>
        /// Подсчет общего количества записей с учетом фильтров
        /// </summary>
        private void UpdateTotalRecords()
        {
            string countQuery = BuildCountQuery();
            MySqlParameter[] parameters = BuildQueryParameters();

            object result = DatabaseHelper.ExecuteScalar(countQuery, parameters);
            pagination.TotalRecords = result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// Построение запроса для подсчета записей
        /// </summary>
        private string BuildCountQuery()
        {
            string baseQuery = @"SELECT COUNT(DISTINCT b.BookID)
                FROM books b
                LEFT JOIN authors a1 ON b.AuthorOne = a1.AuthorID
                LEFT JOIN authors a2 ON b.AuthorTwo = a2.AuthorID
                LEFT JOIN publishers p ON b.Publisher = p.PublisherID
                LEFT JOIN bookcategories bc ON b.BookID = bc.BookID
                LEFT JOIN categories c ON bc.CategoryID = c.CategoryID";

            string whereClause = BuildWhereClause();

            if (!string.IsNullOrEmpty(whereClause))
            {
                baseQuery += " WHERE " + whereClause;
            }

            return baseQuery;
        }

        /// <summary>
        /// Построение WHERE условия для фильтрации
        /// </summary>
        private string BuildWhereClause()
        {
            string whereClause = "";

            // Фильтр по поиску
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                whereClause = "(b.Title LIKE @search OR CONCAT(a1.Surname, ' ', a1.Name) LIKE @search)";
            }

            // Фильтр по издательству
            if (cmbFilterPublisher.SelectedValue != null)
            {
                int publisherId = 0;
                if (int.TryParse(cmbFilterPublisher.SelectedValue.ToString(), out publisherId))
                {
                    if (publisherId > 0)
                    {
                        if (!string.IsNullOrEmpty(whereClause)) whereClause += " AND ";
                        whereClause += "b.Publisher = @publisherId";
                    }
                }
            }

            // Фильтр по категории
            if (cmbFilterCategory.SelectedValue != null)
            {
                int categoryId = 0;
                if (int.TryParse(cmbFilterCategory.SelectedValue.ToString(), out categoryId))
                {
                    if (categoryId > 0)
                    {
                        if (!string.IsNullOrEmpty(whereClause)) whereClause += " AND ";
                        whereClause += "bc.CategoryID = @categoryId";
                    }
                }
            }

            return whereClause;
        }

        /// <summary>
        /// Построение параметров для SQL запроса
        /// </summary>
        private MySqlParameter[] BuildQueryParameters()
        {
            var paramsList = new System.Collections.Generic.List<MySqlParameter>();

            // Параметр поиска
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                paramsList.Add(new MySqlParameter("@search", "%" + txtSearch.Text.Trim() + "%"));
            }

            // Параметр издательства
            if (cmbFilterPublisher.SelectedValue != null)
            {
                int publisherId = 0;
                if (int.TryParse(cmbFilterPublisher.SelectedValue.ToString(), out publisherId))
                {
                    if (publisherId > 0)
                    {
                        paramsList.Add(new MySqlParameter("@publisherId", publisherId));
                    }
                }
            }

            // Параметр категории
            if (cmbFilterCategory.SelectedValue != null)
            {
                int categoryId = 0;
                if (int.TryParse(cmbFilterCategory.SelectedValue.ToString(), out categoryId))
                {
                    if (categoryId > 0)
                    {
                        paramsList.Add(new MySqlParameter("@categoryId", categoryId));
                    }
                }
            }

            // Параметры пагинации
            paramsList.Add(new MySqlParameter("@limit", pagination.PageSize));
            paramsList.Add(new MySqlParameter("@offset", pagination.Offset));

            return paramsList.ToArray();
        }

        /// <summary>
        /// Построение ORDER BY части запроса
        /// </summary>
        private string BuildOrderByClause()
        {
            switch (cmbSort.SelectedIndex)
            {
                case 1: return "ORDER BY b.Title ASC";
                case 2: return "ORDER BY b.Title DESC";
                case 3: return "ORDER BY b.Year DESC";
                case 4: return "ORDER BY b.Year ASC";
                default: return "ORDER BY b.Title ASC";
            }
        }

        /// <summary>
        /// Загрузка данных конкретной страницы
        /// </summary>
        private void LoadPageData()
        {
            string query = @"SELECT b.BookID, b.Title as 'Название', 
                    CONCAT(a1.Surname, ' ', a1.Name, ' ', IFNULL(a1.Patronymic, '')) as 'Автор',
                    IFNULL(CONCAT(a2.Surname, ' ', a2.Name, ' ', IFNULL(a2.Patronymic, '')), '') as 'Соавтор',
                    c.Name as 'Категория',
                    p.Name as 'Издательство',
                    b.Year as 'Год',
                    CAST(b.Count AS SIGNED) as 'Всего',
                    CAST(b.AvailableNow AS SIGNED) as 'Доступно',
                    b.Publisher as PublisherID
                    FROM books b
                    LEFT JOIN authors a1 ON b.AuthorOne = a1.AuthorID
                    LEFT JOIN authors a2 ON b.AuthorTwo = a2.AuthorID
                    LEFT JOIN publishers p ON b.Publisher = p.PublisherID
                    LEFT JOIN bookcategories bc ON b.BookID = bc.BookID
                    LEFT JOIN categories c ON bc.CategoryID = c.CategoryID";

            string whereClause = BuildWhereClause();
            if (!string.IsNullOrEmpty(whereClause))
            {
                query += " WHERE " + whereClause;
            }

            query += " " + BuildOrderByClause();
            query += " LIMIT @limit OFFSET @offset";

            MySqlParameter[] parameters = BuildQueryParameters();

            booksTable = DatabaseHelper.ExecuteQuery(query, parameters);
            dgvBooks.DataSource = booksTable;

            // Скрываем служебные колонки
            if (dgvBooks.Columns.Contains("BookID"))
                dgvBooks.Columns["BookID"].Visible = false;
            if (dgvBooks.Columns.Contains("PublisherID"))
                dgvBooks.Columns["PublisherID"].Visible = false;

            // Обновляем информацию о записях
            UpdateRecordsInfo();
        }

        /// <summary>
        /// Обновление информации о количестве записей
        /// </summary>
        private void UpdateRecordsInfo()
        {
            if (pagination.TotalRecords == 0)
            {
                lblRecordsInfo.Text = "Записей: 0 из 0";
            }
            else
            {
                lblRecordsInfo.Text = string.Format("Записей: {0}-{1} из {2}",
                    pagination.GetStartRecord(),
                    pagination.GetEndRecord(),
                    pagination.TotalRecords);
            }
        }

        // ============================================
        // УСЛОВНОЕ ФОРМАТИРОВАНИЕ
        // ============================================

        /// <summary>
        /// Обработчик события завершения привязки данных
        /// </summary>
        private void DgvBooks_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyConditionalFormatting();
        }

        /// <summary>
        /// Применение условного форматирования к строкам таблицы
        /// </summary>
        private void ApplyConditionalFormatting()
        {
            if (dgvBooks.Rows.Count == 0)
                return;

            // Проверяем наличие необходимых колонок
            if (!dgvBooks.Columns.Contains("Доступно") || !dgvBooks.Columns.Contains("Всего"))
                return;

            foreach (DataGridViewRow row in dgvBooks.Rows)
            {
                try
                {
                    // Получаем значения
                    object availableObj = row.Cells["Доступно"].Value;
                    object totalObj = row.Cells["Всего"].Value;

                    if (availableObj == null || availableObj == DBNull.Value ||
                        totalObj == null || totalObj == DBNull.Value)
                        continue;

                    int availableCount = Convert.ToInt32(availableObj);
                    int totalCount = Convert.ToInt32(totalObj);

                    // Определяем цвет строки на основе остатков
                    Color rowColor = GetRowColor(availableCount, totalCount);

                    // Применяем форматирование
                    ApplyRowFormatting(row, rowColor);
                }
                catch (Exception ex)
                {
                    // Игнорируем ошибки преобразования для конкретной строки
                    System.Diagnostics.Debug.WriteLine($"Ошибка форматирования строки: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Определение цвета строки на основе количества доступных книг
        /// </summary>
        private Color GetRowColor(int availableCount, int totalCount)
        {
            // Критический уровень - все книги выданы или очень мало
            if (availableCount == 0)
            {
                return Color.FromArgb(255, 200, 200); // Светло-красный
            }
            else if (availableCount <= CRITICAL_STOCK_LEVEL)
            {
                return Color.FromArgb(255, 220, 200); // Оранжевый
            }
            // Предупреждение - мало книг
            else if (availableCount <= WARNING_STOCK_LEVEL)
            {
                return Color.FromArgb(255, 255, 200); // Светло-желтый
            }
            // Дополнительная проверка: если доступно менее 30% от общего количества
            else if (totalCount > 0 && (double)availableCount / totalCount < 0.3)
            {
                return Color.FromArgb(255, 245, 220); // Очень светло-желтый
            }
            // Нормальный уровень
            else
            {
                return Color.White; // Обычный белый фон
            }
        }

        /// <summary>
        /// Применение форматирования к строке
        /// </summary>
        private void ApplyRowFormatting(DataGridViewRow row, Color backgroundColor)
        {
            row.DefaultCellStyle.BackColor = backgroundColor;

            // Настройка цвета текста для лучшей читаемости
            if (backgroundColor.R < 128 || backgroundColor.G < 128 || backgroundColor.B < 128)
            {
                row.DefaultCellStyle.ForeColor = Color.White;
            }
            else
            {
                row.DefaultCellStyle.ForeColor = Color.Black;
            }

            // Выделяем колонку "Доступно" жирным шрифтом для критических значений
            if (row.Cells["Доступно"].Value != null &&
                row.Cells["Доступно"].Value != DBNull.Value)
            {
                int available = Convert.ToInt32(row.Cells["Доступно"].Value);

                if (available <= CRITICAL_STOCK_LEVEL)
                {
                    row.Cells["Доступно"].Style.Font = new Font(dgvBooks.Font, FontStyle.Bold);
                }
            }
        }

        // ============================================
        // МЕТОДЫ ПАГИНАЦИИ (УПРОЩЕННАЯ ВЕРСИЯ)
        // ============================================

        /// <summary>
        /// Обновление элементов управления пагинацией
        /// </summary>
        private void UpdatePaginationControls()
        {
            // Обновляем состояние кнопок Назад/Вперед
            btnPrevPage.Enabled = pagination.HasPreviousPage;
            btnNextPage.Enabled = pagination.HasNextPage;

            // Обновляем текстовые поля
            txtCurrentPage.Text = pagination.CurrentPage.ToString();
            lblPageInfo.Text = $"из {pagination.TotalPages}";

            // Блокируем кнопку "Перейти" если страниц нет
            btnGoToPage.Enabled = pagination.TotalPages > 0;
        }

        /// <summary>
        /// Переход на конкретную страницу
        /// </summary>
        private void GoToPage(int pageNumber)
        {
            if (pagination.GoToPage(pageNumber))
            {
                LoadPageData();
                UpdatePaginationControls();
            }
            else
            {
                MessageBox.Show($"Номер страницы должен быть от 1 до {pagination.TotalPages}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCurrentPage.Text = pagination.CurrentPage.ToString();
            }
        }

        /// <summary>
        /// Обработчик кнопки "Назад"
        /// </summary>
        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (pagination.GoToPreviousPage())
            {
                LoadPageData();
                UpdatePaginationControls();
            }
        }

        /// <summary>
        /// Обработчик кнопки "Вперед"
        /// </summary>
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (pagination.GoToNextPage())
            {
                LoadPageData();
                UpdatePaginationControls();
            }
        }

        /// <summary>
        /// Обработчик кнопки "Перейти"
        /// </summary>
        private void btnGoToPage_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCurrentPage.Text, out int pageNumber))
            {
                GoToPage(pageNumber);
            }
            else
            {
                MessageBox.Show("Введите корректный номер страницы!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCurrentPage.Text = pagination.CurrentPage.ToString();
            }
        }

        /// <summary>
        /// Обработчик нажатия клавиш в поле номера страницы
        /// </summary>
        private void txtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры и управляющие клавиши
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Переход по Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnGoToPage_Click(sender, e);
            }
        }

        // ============================================
        // ФИЛЬТРАЦИЯ И СОРТИРОВКА
        // ============================================

        private void ApplyFilters()
        {
            // Сбрасываем на первую страницу при изменении фильтров
            pagination.Reset();

            // Пересчитываем общее количество записей
            UpdateTotalRecords();

            // Загружаем данные
            LoadPageData();

            // Обновляем пагинацию
            UpdatePaginationControls();
        }

        private void ApplySort()
        {
            // При сортировке остаемся на текущей странице
            LoadPageData();
        }

        private void FilterByCategory()
        {
            ApplyFilters();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterByCategory();
        }

        private void cmbFilterPublisher_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplySort();
        }

        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbFilterCategory.SelectedIndex = 0;
            cmbFilterPublisher.SelectedIndex = 0;
            cmbSort.SelectedIndex = 0;
            LoadBooks();
        }

        // ============================================
        // ЛОГИКА ЗАКУПКИ И СПИСАНИЯ
        // ============================================

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            // 1. Диалог: Новая или Существующая?
            DialogResult result = MessageBox.Show(
                "Вы оформляете поступление совершенно новой книги, которой еще нет в каталоге?\n\n" +
                "ДА - Создать новую карточку книги и оформить приход.\n" +
                "НЕТ - Добавить экземпляры к уже существующей книге.",
                "Тип поступления",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Cancel) return;

            if (result == DialogResult.Yes)
            {
                // Новая книга -> BookEditForm
                BookEditForm editForm = new BookEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBooks();
                }
            }
            else
            {
                // Существующая книга -> StockOperationForm
                if (dgvBooks.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Сначала выберите книгу в таблице, к которой нужно добавить экземпляры!",
                        "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["BookID"].Value);
                StockOperationForm stockForm = new StockOperationForm(bookId, true);
                if (stockForm.ShowDialog() == DialogResult.OK)
                {
                    LoadPageData(); // Обновляем только текущую страницу
                }
            }
        }

        private void btnWriteOff_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите книгу для списания!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["BookID"].Value);

            // Открываем форму списания
            StockOperationForm stockForm = new StockOperationForm(bookId, false);

            if (stockForm.ShowDialog() == DialogResult.OK)
            {
                LoadPageData(); // Обновляем только текущую страницу

                // Проверка на нулевой остаток
                int currentCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                    "SELECT Count FROM books WHERE BookID = " + bookId, null));

                if (currentCount <= 0)
                {
                    if (MessageBox.Show(
                        "Все экземпляры этой книги списаны (количество = 0).\n" +
                        "Хотите полностью удалить карточку книги из каталога?",
                        "Удаление пустой карточки",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        DatabaseHelper.ExecuteNonQuery("DELETE FROM bookcategories WHERE BookID = " + bookId);
                        DatabaseHelper.ExecuteNonQuery("DELETE FROM books WHERE BookID = " + bookId);
                        MessageBox.Show("Карточка книги удалена.", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBooks();
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите книгу для редактирования!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["BookID"].Value);
            BookEditForm editForm = new BookEditForm(bookId);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadPageData(); // Обновляем только текущую страницу
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BooksForm_FormClosing(object sender, FormClosingEventArgs e) { }

        private void dgvBooks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяем, что кликнули по строке, а не по заголовку
            if (e.RowIndex >= 0)
            {
                int bookId = Convert.ToInt32(dgvBooks.Rows[e.RowIndex].Cells["BookID"].Value);

                // Открываем форму просмотра
                BookViewForm viewForm = new BookViewForm(bookId);
                viewForm.ShowDialog();
            }
        }

        private void lblLegendLow_Click(object sender, EventArgs e)
        {

        }
    }
}