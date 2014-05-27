using System;
using System.Windows.Forms;
using Pers_uchet_org.Properties;

namespace Pers_uchet_org.Forms
{
    public partial class SettingsLanForm : Form
    {
        public SettingsLanForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (SaveProperties())
            {
                MainForm.ShowInfoMessage("Настройки успешно сохранены", "Сохранение настроек");
                DialogResult = DialogResult.OK;
            }
        }

        private bool SaveProperties()
        {
            try
            {
                Settings.Default.ProxyUseAuto = autoProxyRadioButton.Checked;
                Settings.Default.UseDefaultCredentials = !autoProxyRadioButton.Checked &&
                                                         customCredentialsCheckBox.Checked;
                Settings.Default.ProxyAddr = serverProxyTextBox.Text.Trim();
                int port;
                if (Int32.TryParse(portProxyTextBox.Text, out port))
                    Settings.Default.ProxyPort = port;
                Settings.Default.ProxyLogin = loginProxyTextBox.Text.Trim();
                Settings.Default.ProxyPass = passwordProxyTextBox.Text;
                Settings.Default.Save();
                return true;
            }
            catch (Exception e)
            {
                MainForm.ShowErrorFlexMessage(e.Message, "Ошибка сохранения настроек");
                return false;
            }
        }

        private void showPassButton_MouseDown(object sender, MouseEventArgs e)
        {
            passwordProxyTextBox.UseSystemPasswordChar = false;
        }

        private void showPassButton_MouseUp(object sender, MouseEventArgs e)
        {
            passwordProxyTextBox.UseSystemPasswordChar = true;
        }

        private void SettingsLanForm_Load(object sender, EventArgs e)
        {
            ReadSettings();

            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(autoProxyRadioButton, "Используются настройки из обозревателя IE");
        }

        private void ReadSettings()
        {
            try
            {
                bool proxyUseAuto = Settings.Default.ProxyUseAuto;
                bool useDefaultCredentials = Settings.Default.UseDefaultCredentials;

                string proxyAddr = Settings.Default.ProxyAddr;
                int proxyPort = Settings.Default.ProxyPort;
                string proxyLogin = Settings.Default.ProxyLogin;
                string proxyPass = Settings.Default.ProxyPass;

                //bool bypassProxyOnLocal = Properties.Settings.Default.BypassProxyOnLocal;

                autoProxyRadioButton.Checked = proxyUseAuto;
                manualProxyRadioButton.Checked = !proxyUseAuto;

                serverProxyTextBox.Text = proxyAddr;
                portProxyTextBox.Text = proxyPort.ToString();

                customCredentialsCheckBox.Checked = useDefaultCredentials;
                loginProxyTextBox.Text = proxyLogin;
                passwordProxyTextBox.Text = proxyPass;
                customCredentialsCheckBox_CheckedChanged(customCredentialsCheckBox, null);
            }
            catch (Exception e)
            {
                MainForm.ShowErrorFlexMessage(e.Message, "Ошибка считывания настроек");
            }
        }

        private void autoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            bool isEnabled = !(sender as RadioButton).Checked;

            serverProxyLabel.Enabled = isEnabled;
            serverProxyTextBox.Enabled = isEnabled;
            portProxyTextBox.Enabled = isEnabled;
            customCredentialsCheckBox.Enabled = isEnabled;
        }

        private void customCredentialsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool isEnabled = (sender as CheckBox).Checked && (sender as CheckBox).Enabled;

            loginProxyLabel.Enabled = isEnabled;
            loginProxyTextBox.Enabled = isEnabled;
            passwordProxyLabel.Enabled = isEnabled;
            passwordProxyTextBox.Enabled = isEnabled;
            showPassProxyButton.Enabled = isEnabled;
        }

        private void customCredentialsCheckBox_EnabledChanged(object sender, EventArgs e)
        {
            bool isEnabled = (sender as CheckBox).Checked && (sender as CheckBox).Enabled;

            loginProxyLabel.Enabled = isEnabled;
            loginProxyTextBox.Enabled = isEnabled;
            passwordProxyLabel.Enabled = isEnabled;
            passwordProxyTextBox.Enabled = isEnabled;
            showPassProxyButton.Enabled = isEnabled;
        }
    }
}