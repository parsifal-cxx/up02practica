namespace LibraryAIS
{
    partial class BooksForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BooksForm));
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnResetFilters = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblFilterCategory = new System.Windows.Forms.Label();
            this.cmbFilterCategory = new System.Windows.Forms.ComboBox();
            this.lblFilterPublisher = new System.Windows.Forms.Label();
            this.cmbFilterPublisher = new System.Windows.Forms.ComboBox();
            this.lblSort = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.btnWriteOff = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelPagination = new System.Windows.Forms.Panel();
            this.lblRecordsInfo = new System.Windows.Forms.Label();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.txtCurrentPage = new System.Windows.Forms.TextBox();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnGoToPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.lblLegendCritical = new System.Windows.Forms.Label();
            this.lblLegendWarning = new System.Windows.Forms.Label();
            this.lblLegendLow = new System.Windows.Forms.Label();
            this.lblLegendNormal = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelPagination.SuspendLayout();
            this.panelLegend.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 50);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1000, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Книги и материалы библиотеки";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelSearch.Controls.Add(this.btnResetFilters);
            this.panelSearch.Controls.Add(this.lblSearch);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.lblFilterCategory);
            this.panelSearch.Controls.Add(this.cmbFilterCategory);
            this.panelSearch.Controls.Add(this.lblFilterPublisher);
            this.panelSearch.Controls.Add(this.cmbFilterPublisher);
            this.panelSearch.Controls.Add(this.lblSort);
            this.panelSearch.Controls.Add(this.cmbSort);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 50);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1000, 70);
            this.panelSearch.TabIndex = 1;
            // 
            // btnResetFilters
            // 
            this.btnResetFilters.Location = new System.Drawing.Point(817, 33);
            this.btnResetFilters.Name = "btnResetFilters";
            this.btnResetFilters.Size = new System.Drawing.Size(75, 23);
            this.btnResetFilters.TabIndex = 0;
            this.btnResetFilters.Text = "Сбросить";
            this.btnResetFilters.Click += new System.EventHandler(this.btnResetFilters_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(10, 10);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(42, 13);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Поиск:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(10, 36);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblFilterCategory
            // 
            this.lblFilterCategory.Location = new System.Drawing.Point(220, 10);
            this.lblFilterCategory.Name = "lblFilterCategory";
            this.lblFilterCategory.Size = new System.Drawing.Size(100, 23);
            this.lblFilterCategory.TabIndex = 3;
            this.lblFilterCategory.Text = "Категория:";
            // 
            // cmbFilterCategory
            // 
            this.cmbFilterCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterCategory.Location = new System.Drawing.Point(222, 36);
            this.cmbFilterCategory.Name = "cmbFilterCategory";
            this.cmbFilterCategory.Size = new System.Drawing.Size(180, 21);
            this.cmbFilterCategory.TabIndex = 4;
            this.cmbFilterCategory.SelectedIndexChanged += new System.EventHandler(this.cmbFilterCategory_SelectedIndexChanged);
            // 
            // lblFilterPublisher
            // 
            this.lblFilterPublisher.Location = new System.Drawing.Point(410, 10);
            this.lblFilterPublisher.Name = "lblFilterPublisher";
            this.lblFilterPublisher.Size = new System.Drawing.Size(100, 23);
            this.lblFilterPublisher.TabIndex = 5;
            this.lblFilterPublisher.Text = "Издательство:";
            // 
            // cmbFilterPublisher
            // 
            this.cmbFilterPublisher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterPublisher.Location = new System.Drawing.Point(412, 36);
            this.cmbFilterPublisher.Name = "cmbFilterPublisher";
            this.cmbFilterPublisher.Size = new System.Drawing.Size(200, 21);
            this.cmbFilterPublisher.TabIndex = 6;
            this.cmbFilterPublisher.SelectedIndexChanged += new System.EventHandler(this.cmbFilterPublisher_SelectedIndexChanged);
            // 
            // lblSort
            // 
            this.lblSort.Location = new System.Drawing.Point(620, 10);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(100, 23);
            this.lblSort.TabIndex = 7;
            this.lblSort.Text = "Сортировка:";
            // 
            // cmbSort
            // 
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.Location = new System.Drawing.Point(622, 36);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(180, 21);
            this.cmbSort.TabIndex = 8;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);
            // 
            // dgvBooks
            // 
            this.dgvBooks.AllowUserToAddRows = false;
            this.dgvBooks.AllowUserToDeleteRows = false;
            this.dgvBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(0, 120);
            this.dgvBooks.MultiSelect = false;
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.RowHeadersVisible = false;
            this.dgvBooks.RowHeadersWidth = 51;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.Size = new System.Drawing.Size(1000, 330);
            this.dgvBooks.TabIndex = 2;
            this.dgvBooks.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBooks_CellDoubleClick);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.panelLegend);
            this.panelButtons.Controls.Add(this.btnPurchase);
            this.panelButtons.Controls.Add(this.btnWriteOff);
            this.panelButtons.Controls.Add(this.btnEdit);
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 500);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1000, 50);
            this.panelButtons.TabIndex = 3;
            // 
            // btnPurchase
            // 
            this.btnPurchase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurchase.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPurchase.ForeColor = System.Drawing.Color.White;
            this.btnPurchase.Location = new System.Drawing.Point(10, 10);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(159, 32);
            this.btnPurchase.TabIndex = 0;
            this.btnPurchase.Text = "Закупка / Приход";
            this.btnPurchase.UseVisualStyleBackColor = false;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);
            // 
            // btnWriteOff
            // 
            this.btnWriteOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnWriteOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWriteOff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnWriteOff.ForeColor = System.Drawing.Color.White;
            this.btnWriteOff.Location = new System.Drawing.Point(175, 10);
            this.btnWriteOff.Name = "btnWriteOff";
            this.btnWriteOff.Size = new System.Drawing.Size(120, 32);
            this.btnWriteOff.TabIndex = 1;
            this.btnWriteOff.Text = "Списание";
            this.btnWriteOff.UseVisualStyleBackColor = false;
            this.btnWriteOff.Click += new System.EventHandler(this.btnWriteOff_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(301, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(126, 32);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Gray;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(870, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 32);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelPagination
            // 
            this.panelPagination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelPagination.Controls.Add(this.lblRecordsInfo);
            this.panelPagination.Controls.Add(this.btnPrevPage);
            this.panelPagination.Controls.Add(this.txtCurrentPage);
            this.panelPagination.Controls.Add(this.lblPageInfo);
            this.panelPagination.Controls.Add(this.btnGoToPage);
            this.panelPagination.Controls.Add(this.btnNextPage);
            this.panelPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPagination.Location = new System.Drawing.Point(0, 450);
            this.panelPagination.Name = "panelPagination";
            this.panelPagination.Size = new System.Drawing.Size(1000, 50);
            this.panelPagination.TabIndex = 4;
            // 
            // lblRecordsInfo
            // 
            this.lblRecordsInfo.AutoSize = true;
            this.lblRecordsInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRecordsInfo.Location = new System.Drawing.Point(10, 17);
            this.lblRecordsInfo.Name = "lblRecordsInfo";
            this.lblRecordsInfo.Size = new System.Drawing.Size(96, 15);
            this.lblRecordsInfo.TabIndex = 0;
            this.lblRecordsInfo.Text = "Записей: 0 из 0";
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPrevPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrevPage.ForeColor = System.Drawing.Color.White;
            this.btnPrevPage.Location = new System.Drawing.Point(350, 10);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(80, 30);
            this.btnPrevPage.TabIndex = 1;
            this.btnPrevPage.Text = "◄ Назад";
            this.btnPrevPage.UseVisualStyleBackColor = false;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCurrentPage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtCurrentPage.Location = new System.Drawing.Point(440, 12);
            this.txtCurrentPage.MaxLength = 5;
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(50, 25);
            this.txtCurrentPage.TabIndex = 2;
            this.txtCurrentPage.Text = "1";
            this.txtCurrentPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCurrentPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrentPage_KeyPress);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPageInfo.Location = new System.Drawing.Point(495, 17);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(31, 15);
            this.lblPageInfo.TabIndex = 3;
            this.lblPageInfo.Text = "из 1";
            // 
            // btnGoToPage
            // 
            this.btnGoToPage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGoToPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnGoToPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGoToPage.ForeColor = System.Drawing.Color.White;
            this.btnGoToPage.Location = new System.Drawing.Point(540, 10);
            this.btnGoToPage.Name = "btnGoToPage";
            this.btnGoToPage.Size = new System.Drawing.Size(70, 30);
            this.btnGoToPage.TabIndex = 4;
            this.btnGoToPage.Text = "Перейти";
            this.btnGoToPage.UseVisualStyleBackColor = false;
            this.btnGoToPage.Click += new System.EventHandler(this.btnGoToPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNextPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNextPage.ForeColor = System.Drawing.Color.White;
            this.btnNextPage.Location = new System.Drawing.Point(620, 10);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(80, 30);
            this.btnNextPage.TabIndex = 5;
            this.btnNextPage.Text = "Вперед ►";
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // panelLegend
            // 
            this.panelLegend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLegend.BackColor = System.Drawing.Color.White;
            this.panelLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLegend.Controls.Add(this.lblLegendCritical);
            this.panelLegend.Controls.Add(this.lblLegendWarning);
            this.panelLegend.Controls.Add(this.lblLegendLow);
            this.panelLegend.Controls.Add(this.lblLegendNormal);
            this.panelLegend.Location = new System.Drawing.Point(489, 4);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(332, 42);
            this.panelLegend.TabIndex = 5;
            // 
            // lblLegendCritical
            // 
            this.lblLegendCritical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblLegendCritical.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLegendCritical.Location = new System.Drawing.Point(-3, 0);
            this.lblLegendCritical.Name = "lblLegendCritical";
            this.lblLegendCritical.Size = new System.Drawing.Size(172, 20);
            this.lblLegendCritical.TabIndex = 1;
            this.lblLegendCritical.Text = "  Нет в наличии (0 экз.)";
            this.lblLegendCritical.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLegendWarning
            // 
            this.lblLegendWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(200)))));
            this.lblLegendWarning.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLegendWarning.Location = new System.Drawing.Point(-3, 20);
            this.lblLegendWarning.Name = "lblLegendWarning";
            this.lblLegendWarning.Size = new System.Drawing.Size(172, 20);
            this.lblLegendWarning.TabIndex = 2;
            this.lblLegendWarning.Text = "  Критический остаток (≤3 экз.)";
            this.lblLegendWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLegendLow
            // 
            this.lblLegendLow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.lblLegendLow.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLegendLow.Location = new System.Drawing.Point(166, 0);
            this.lblLegendLow.Name = "lblLegendLow";
            this.lblLegendLow.Size = new System.Drawing.Size(172, 20);
            this.lblLegendLow.TabIndex = 3;
            this.lblLegendLow.Text = "  Малый остаток (≤10 экз.)";
            this.lblLegendLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLegendLow.Click += new System.EventHandler(this.lblLegendLow_Click);
            // 
            // lblLegendNormal
            // 
            this.lblLegendNormal.BackColor = System.Drawing.Color.White;
            this.lblLegendNormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLegendNormal.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLegendNormal.Location = new System.Drawing.Point(166, 20);
            this.lblLegendNormal.Name = "lblLegendNormal";
            this.lblLegendNormal.Size = new System.Drawing.Size(172, 20);
            this.lblLegendNormal.TabIndex = 4;
            this.lblLegendNormal.Text = "  Нормальный остаток";
            this.lblLegendNormal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BooksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 550);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.panelPagination);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(900, 589);
            this.Name = "BooksForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Книги и материалы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BooksForm_FormClosing);
            this.Load += new System.EventHandler(this.BooksForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelPagination.ResumeLayout(false);
            this.panelPagination.PerformLayout();
            this.panelLegend.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblFilterCategory;
        private System.Windows.Forms.ComboBox cmbFilterCategory;
        private System.Windows.Forms.Label lblFilterPublisher;
        private System.Windows.Forms.ComboBox cmbFilterPublisher;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Button btnResetFilters;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Button btnWriteOff;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelPagination;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.TextBox txtCurrentPage;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnGoToPage;
        private System.Windows.Forms.Label lblRecordsInfo;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.Label lblLegendCritical;
        private System.Windows.Forms.Label lblLegendWarning;
        private System.Windows.Forms.Label lblLegendLow;
        private System.Windows.Forms.Label lblLegendNormal;
    }
}