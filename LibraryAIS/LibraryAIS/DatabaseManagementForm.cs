using System;
using System.Windows.Forms;

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
                cmbTables.Items.Clear();
                cmbTables.Items.Add("-- Выберите таблицу --");
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
            MessageBox.Show("Функция будет реализована в следующем этапе",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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