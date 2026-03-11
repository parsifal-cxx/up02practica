using System;
using System.Windows.Forms;
using LibraryAIS.Properties;

namespace LibraryAIS
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Загружаем текущие настройки
            LoadSettings();
        }

        /// <summary>
        /// Загрузка текущих настроек из Properties.Settings
        /// </summary>
        private void LoadSettings()
        {
            chkEnableInactivityLock.Checked = Settings.Default.EnableInactivityLock;
            numInactivityTimeout.Value = Settings.Default.InactivityTimeout;

            // Включаем/выключаем поле таймаута в зависимости от чекбокса
            numInactivityTimeout.Enabled = chkEnableInactivityLock.Checked;
            lblTimeout.Enabled = chkEnableInactivityLock.Checked;
            lblSeconds.Enabled = chkEnableInactivityLock.Checked;

            UpdateWarningMessage();
        }

        /// <summary>
        /// Обновление предупреждающего сообщения
        /// </summary>
        private void UpdateWarningMessage()
        {
            if (!chkEnableInactivityLock.Checked)
            {
                lblWarning.Text = "⚠ Блокировка по неактивности отключена. Система не будет автоматически завершать сеанс.";
                lblWarning.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblWarning.Text = $"ℹ Система будет автоматически блокироваться после {numInactivityTimeout.Value} секунд бездействия.";
                lblWarning.ForeColor = System.Drawing.Color.Green;
            }
        }

        /// <summary>
        /// Сохранение настроек
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидация
            if (chkEnableInactivityLock.Checked)
            {
                if (numInactivityTimeout.Value < 10)
                {
                    MessageBox.Show("Время неактивности не может быть меньше 10 секунд!",
                        "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numInactivityTimeout.Focus();
                    return;
                }

                if (numInactivityTimeout.Value > 3600)
                {
                    MessageBox.Show("Время неактивности не может превышать 3600 секунд (1 час)!",
                        "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numInactivityTimeout.Focus();
                    return;
                }
            }

            try
            {
                // Сохраняем настройки
                Settings.Default.EnableInactivityLock = chkEnableInactivityLock.Checked;
                Settings.Default.InactivityTimeout = (int)numInactivityTimeout.Value;
                Settings.Default.Save();

                MessageBox.Show("Настройки успешно сохранены!\n\n" +
                    "Изменения вступят в силу при следующем входе в систему.",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении настроек:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Отмена изменений
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Сброс настроек к значениям по умолчанию
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы уверены, что хотите сбросить настройки к значениям по умолчанию?\n\n" +
                "Время неактивности: 30 секунд\n" +
                "Блокировка: Включена",
                "Подтверждение сброса",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                chkEnableInactivityLock.Checked = true;
                numInactivityTimeout.Value = 30;
                UpdateWarningMessage();
            }
        }

        /// <summary>
        /// Включение/выключение поля таймаута
        /// </summary>
        private void chkEnableInactivityLock_CheckedChanged(object sender, EventArgs e)
        {
            numInactivityTimeout.Enabled = chkEnableInactivityLock.Checked;
            lblTimeout.Enabled = chkEnableInactivityLock.Checked;
            lblSeconds.Enabled = chkEnableInactivityLock.Checked;

            UpdateWarningMessage();
        }

        /// <summary>
        /// Обновление сообщения при изменении значения таймаута
        /// </summary>
        private void numInactivityTimeout_ValueChanged(object sender, EventArgs e)
        {
            UpdateWarningMessage();
        }
    }
}