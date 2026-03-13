namespace LibraryAIS
{
    partial class DatabaseManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseManagementForm));
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBoxRestore = new System.Windows.Forms.GroupBox();
            this.lblRestoreInfo = new System.Windows.Forms.Label();
            this.btnRestoreStructure = new System.Windows.Forms.Button();
            this.groupBoxImport = new System.Windows.Forms.GroupBox();
            this.lblImportInfo = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.cmbTables = new System.Windows.Forms.ComboBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.chkHasHeader = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.groupBoxRestore.SuspendLayout();
            this.groupBoxImport.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(700, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(700, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Управление базой данных";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxRestore
            // 
            this.groupBoxRestore.Controls.Add(this.lblRestoreInfo);
            this.groupBoxRestore.Controls.Add(this.btnRestoreStructure);
            this.groupBoxRestore.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxRestore.Location = new System.Drawing.Point(20, 80);
            this.groupBoxRestore.Name = "groupBoxRestore";
            this.groupBoxRestore.Size = new System.Drawing.Size(660, 120);
            this.groupBoxRestore.TabIndex = 1;
            this.groupBoxRestore.TabStop = false;
            this.groupBoxRestore.Text = "Восстановление структуры базы данных";
            // 
            // lblRestoreInfo
            // 
            this.lblRestoreInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRestoreInfo.ForeColor = System.Drawing.Color.DarkRed;
            this.lblRestoreInfo.Location = new System.Drawing.Point(20, 30);
            this.lblRestoreInfo.Name = "lblRestoreInfo";
            this.lblRestoreInfo.Size = new System.Drawing.Size(620, 40);
            this.lblRestoreInfo.TabIndex = 0;
            this.lblRestoreInfo.Text = "⚠ ВНИМАНИЕ! Восстановление структуры БД удалит все существующие таблицы и данные" +
                "!\r\nИспользуйте эту функцию только для создания новой базы данных или полного во" +
                "сстановления.";
            // 
            // btnRestoreStructure
            // 
            this.btnRestoreStructure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRestoreStructure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreStructure.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestoreStructure.ForeColor = System.Drawing.Color.White;
            this.btnRestoreStructure.Location = new System.Drawing.Point(20, 75);
            this.btnRestoreStructure.Name = "btnRestoreStructure";
            this.btnRestoreStructure.Size = new System.Drawing.Size(250, 35);
            this.btnRestoreStructure.TabIndex = 1;
            this.btnRestoreStructure.Text = "Восстановить структуру БД";
            this.btnRestoreStructure.UseVisualStyleBackColor = false;
            this.btnRestoreStructure.Click += new System.EventHandler(this.btnRestoreStructure_Click);
            // 
            // groupBoxImport
            // 
            this.groupBoxImport.Controls.Add(this.lblImportInfo);
            this.groupBoxImport.Controls.Add(this.lblTable);
            this.groupBoxImport.Controls.Add(this.cmbTables);
            this.groupBoxImport.Controls.Add(this.lblFile);
            this.groupBoxImport.Controls.Add(this.txtFilePath);
            this.groupBoxImport.Controls.Add(this.btnSelectFile);
            this.groupBoxImport.Controls.Add(this.chkHasHeader);
            this.groupBoxImport.Controls.Add(this.btnImport);
            this.groupBoxImport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxImport.Location = new System.Drawing.Point(20, 215);
            this.groupBoxImport.Name = "groupBoxImport";
            this.groupBoxImport.Size = new System.Drawing.Size(660, 240);
            this.groupBoxImport.TabIndex = 2;
            this.groupBoxImport.TabStop = false;
            this.groupBoxImport.Text = "Импорт данных из CSV файла";
            // 
            // lblImportInfo
            // 
            this.lblImportInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblImportInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.lblImportInfo.Location = new System.Drawing.Point(20, 30);
            this.lblImportInfo.Name = "lblImportInfo";
            this.lblImportInfo.Size = new System.Drawing.Size(620, 30);
            this.lblImportInfo.TabIndex = 0;
            this.lblImportInfo.Text = "ℹ Выберите таблицу и CSV файл для импорта данных. Формат файла: значения раздел" +
                "ены точкой с запятой (;).\r\nКоличество колонок в CSV должно соответствовать стру" +
                "ктуре таблицы.";
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTable.Location = new System.Drawing.Point(20, 70);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(59, 15);
            this.lblTable.TabIndex = 1;
            this.lblTable.Text = "Таблица:";
            // 
            // cmbTables
            // 
            this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTables.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Location = new System.Drawing.Point(20, 90);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(300, 25);
            this.cmbTables.TabIndex = 2;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFile.Location = new System.Drawing.Point(20, 125);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(69, 15);
            this.lblFile.TabIndex = 3;
            this.lblFile.Text = "CSV файл:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilePath.Location = new System.Drawing.Point(20, 145);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(500, 25);
            this.txtFilePath.TabIndex = 4;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSelectFile.ForeColor = System.Drawing.Color.White;
            this.btnSelectFile.Location = new System.Drawing.Point(530, 145);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(110, 25);
            this.btnSelectFile.TabIndex = 5;
            this.btnSelectFile.Text = "Выбрать...";
            this.btnSelectFile.UseVisualStyleBackColor = false;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // chkHasHeader
            // 
            this.chkHasHeader.AutoSize = true;
            this.chkHasHeader.Checked = true;
            this.chkHasHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHasHeader.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkHasHeader.Location = new System.Drawing.Point(20, 185);
            this.chkHasHeader.Name = "chkHasHeader";
            this.chkHasHeader.Size = new System.Drawing.Size(245, 19);
            this.chkHasHeader.TabIndex = 6;
            this.chkHasHeader.Text = "Первая строка содержит заголовки";
            this.chkHasHeader.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(490, 180);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(150, 35);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = "Импортировать";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 470);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(700, 60);
            this.panelButtons.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Gray;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(560, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 35);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DatabaseManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 530);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.groupBoxImport);
            this.Controls.Add(this.groupBoxRestore);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление базой данных";
            this.Load += new System.EventHandler(this.DatabaseManagementForm_Load);
            this.panelTop.ResumeLayout(false);
            this.groupBoxRestore.ResumeLayout(false);
            this.groupBoxImport.ResumeLayout(false);
            this.groupBoxImport.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBoxRestore;
        private System.Windows.Forms.Label lblRestoreInfo;
        private System.Windows.Forms.Button btnRestoreStructure;
        private System.Windows.Forms.GroupBox groupBoxImport;
        private System.Windows.Forms.Label lblImportInfo;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.ComboBox cmbTables;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.CheckBox chkHasHeader;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnClose;
    }
}