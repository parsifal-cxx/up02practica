namespace LibraryAIS
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBoxSecurity = new System.Windows.Forms.GroupBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.numInactivityTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.chkEnableInactivityLock = new System.Windows.Forms.CheckBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.groupBoxSecurity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInactivityTimeout)).BeginInit();
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
            this.panelTop.Size = new System.Drawing.Size(500, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Настройки системы";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxSecurity
            // 
            this.groupBoxSecurity.Controls.Add(this.lblWarning);
            this.groupBoxSecurity.Controls.Add(this.lblSeconds);
            this.groupBoxSecurity.Controls.Add(this.numInactivityTimeout);
            this.groupBoxSecurity.Controls.Add(this.lblTimeout);
            this.groupBoxSecurity.Controls.Add(this.chkEnableInactivityLock);
            this.groupBoxSecurity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxSecurity.Location = new System.Drawing.Point(20, 80);
            this.groupBoxSecurity.Name = "groupBoxSecurity";
            this.groupBoxSecurity.Size = new System.Drawing.Size(460, 180);
            this.groupBoxSecurity.TabIndex = 1;
            this.groupBoxSecurity.TabStop = false;
            this.groupBoxSecurity.Text = "Безопасность";
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblWarning.ForeColor = System.Drawing.Color.Green;
            this.lblWarning.Location = new System.Drawing.Point(20, 120);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(420, 50);
            this.lblWarning.TabIndex = 4;
            this.lblWarning.Text = "ℹ Система будет автоматически блокироваться при отсутствии активности.";
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSeconds.Location = new System.Drawing.Point(230, 85);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(42, 15);
            this.lblSeconds.TabIndex = 3;
            this.lblSeconds.Text = "секунд";
            // 
            // numInactivityTimeout
            // 
            this.numInactivityTimeout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numInactivityTimeout.Location = new System.Drawing.Point(160, 80);
            this.numInactivityTimeout.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            this.numInactivityTimeout.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numInactivityTimeout.Name = "numInactivityTimeout";
            this.numInactivityTimeout.Size = new System.Drawing.Size(60, 25);
            this.numInactivityTimeout.TabIndex = 2;
            this.numInactivityTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numInactivityTimeout.Value = new decimal(new int[] { 30, 0, 0, 0 });
            this.numInactivityTimeout.ValueChanged += new System.EventHandler(this.numInactivityTimeout_ValueChanged);
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTimeout.Location = new System.Drawing.Point(20, 85);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(134, 15);
            this.lblTimeout.TabIndex = 1;
            this.lblTimeout.Text = "Время неактивности:";
            // 
            // chkEnableInactivityLock
            // 
            this.chkEnableInactivityLock.AutoSize = true;
            this.chkEnableInactivityLock.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkEnableInactivityLock.Location = new System.Drawing.Point(20, 40);
            this.chkEnableInactivityLock.Name = "chkEnableInactivityLock";
            this.chkEnableInactivityLock.Size = new System.Drawing.Size(329, 23);
            this.chkEnableInactivityLock.TabIndex = 0;
            this.chkEnableInactivityLock.Text = "Включить автоматическую блокировку системы";
            this.chkEnableInactivityLock.UseVisualStyleBackColor = true;
            this.chkEnableInactivityLock.CheckedChanged += new System.EventHandler(this.chkEnableInactivityLock_CheckedChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnReset);
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 280);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(500, 60);
            this.panelButtons.TabIndex = 2;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(20, 15);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 35);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "По умолчанию";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(260, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(370, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 340);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.groupBoxSecurity);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки системы";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.panelTop.ResumeLayout(false);
            this.groupBoxSecurity.ResumeLayout(false);
            this.groupBoxSecurity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInactivityTimeout)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBoxSecurity;
        private System.Windows.Forms.CheckBox chkEnableInactivityLock;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.NumericUpDown numInactivityTimeout;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
    }
}