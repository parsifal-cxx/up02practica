using System;
using System.Data;
using System.Windows.Forms;

namespace LibraryAIS
{
    public partial class StudentsForm : Form
    {
        private DataTable studentsTable;
        private DataView studentsView;

        public StudentsForm()
        {
            InitializeComponent();
        }

        private void StudentsForm_Load(object sender, EventArgs e)
        {
            LoadGroups();
            LoadStudents();
            ConfigureButtonsByRole();
        }

        private void ConfigureButtonsByRole()
        {
            if (CurrentUser.IsLibrarian)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
            }
        }

        private void LoadGroups()
        {
            string query = "SELECT GroupID, Name FROM `groups` ORDER BY Name";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            DataRow emptyRow = dt.NewRow();
            emptyRow["GroupID"] = 0;
            emptyRow["Name"] = "Все группы";
            dt.Rows.InsertAt(emptyRow, 0);

            cmbFilterGroup.DataSource = dt;
            cmbFilterGroup.DisplayMember = "Name";
            cmbFilterGroup.ValueMember = "GroupID";
            cmbFilterGroup.SelectedIndex = 0;
        }

        private void LoadStudents()
        {
            string query = @"SELECT 
                            s.StudentID,
                            s.Surname as RawSurname,
                            s.Name as RawName,
                            s.Patronymic as RawPatronymic,
                            g.Name as 'Группа',
                            s.Phone as RawPhone,
                            s.`Group` as GroupID
                            FROM students s
                            LEFT JOIN `groups` g ON s.`Group` = g.GroupID
                            ORDER BY s.Surname";

            studentsTable = DatabaseHelper.ExecuteQuery(query);

            if (!studentsTable.Columns.Contains("ФИО"))
                studentsTable.Columns.Add("ФИО", typeof(string));
            if (!studentsTable.Columns.Contains("Телефон"))
                studentsTable.Columns.Add("Телефон", typeof(string));

            ApplyPersonalDataMasking();

            studentsView = new DataView(studentsTable);
            dgvStudents.DataSource = studentsView;

            ConfigureGridColumns();
        }

        private void ApplyPersonalDataMasking()
        {
            foreach (DataRow row in studentsTable.Rows)
            {
                row["ФИО"] = BuildMaskedFio(
                    row["RawSurname"]?.ToString(),
                    row["RawName"]?.ToString(),
                    row["RawPatronymic"]?.ToString());

                row["Телефон"] = MaskPhone(row["RawPhone"]?.ToString());
            }
        }

        private string BuildMaskedFio(string surname, string name, string patronymic)
        {
            surname = string.IsNullOrWhiteSpace(surname) ? "" : surname.Trim();
            name = string.IsNullOrWhiteSpace(name) ? "" : name.Trim();
            patronymic = string.IsNullOrWhiteSpace(patronymic) ? "" : patronymic.Trim();

            string result = surname;

            if (!string.IsNullOrEmpty(name))
                result += " " + name.Substring(0, 1) + ".";

            if (!string.IsNullOrEmpty(patronymic))
                result += " " + patronymic.Substring(0, 1) + ".";

            return result.Trim();
        }

        private string MaskPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return "";

            phone = phone.Trim();

            if (phone.Length <= 2)
                return new string('*', phone.Length);

            if (phone.Length <= 4)
                return new string('*', phone.Length - 1) + phone.Substring(phone.Length - 1);

            return phone.Substring(0, 2) +
                   new string('*', phone.Length - 4) +
                   phone.Substring(phone.Length - 2);
        }

        private void ConfigureGridColumns()
        {
            if (dgvStudents.Columns.Contains("StudentID"))
                dgvStudents.Columns["StudentID"].Visible = false;
            if (dgvStudents.Columns.Contains("GroupID"))
                dgvStudents.Columns["GroupID"].Visible = false;
            if (dgvStudents.Columns.Contains("RawSurname"))
                dgvStudents.Columns["RawSurname"].Visible = false;
            if (dgvStudents.Columns.Contains("RawName"))
                dgvStudents.Columns["RawName"].Visible = false;
            if (dgvStudents.Columns.Contains("RawPatronymic"))
                dgvStudents.Columns["RawPatronymic"].Visible = false;
            if (dgvStudents.Columns.Contains("RawPhone"))
                dgvStudents.Columns["RawPhone"].Visible = false;

            if (dgvStudents.Columns.Contains("ФИО"))
                dgvStudents.Columns["ФИО"].DisplayIndex = 0;
            if (dgvStudents.Columns.Contains("Группа"))
                dgvStudents.Columns["Группа"].DisplayIndex = 1;
            if (dgvStudents.Columns.Contains("Телефон"))
                dgvStudents.Columns["Телефон"].DisplayIndex = 2;
        }

        private void ApplyFilters()
        {
            if (studentsView == null) return;

            string filter = "";

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string searchText = txtSearch.Text.Replace("'", "''");
                filter = "(RawSurname LIKE '%" + searchText + "%' OR RawName LIKE '%" + searchText + "%' OR RawPatronymic LIKE '%" + searchText + "%')";
            }

            if (cmbFilterGroup.SelectedValue != null && Convert.ToInt32(cmbFilterGroup.SelectedValue) > 0)
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += "GroupID = " + cmbFilterGroup.SelectedValue;
            }

            studentsView.RowFilter = filter;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbFilterGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbFilterGroup.SelectedIndex = 0;
            LoadStudents();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            StudentEditForm editForm = new StudentEditForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите студента для редактирования!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int studentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["StudentID"].Value);
            StudentEditForm editForm = new StudentEditForm(studentId);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите студента для удаления!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (InputHelper.ConfirmDelete())
            {
                int studentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["StudentID"].Value);

                string query = "DELETE FROM students WHERE StudentID = " + studentId;
                int result = DatabaseHelper.ExecuteNonQuery(query);

                if (result > 0)
                {
                    MessageBox.Show("Студент успешно удален!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}