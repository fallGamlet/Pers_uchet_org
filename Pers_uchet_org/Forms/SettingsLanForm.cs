using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool SaveProperties()
        {
            try
            {
                Properties.Settings.Default.ProxyUseAuto = autoProxyRadioButton.Checked;
                Properties.Settings.Default.UseDefaultCredentials = autoProxyRadioButton.Checked ? false : customCredentialsCheckBox.Checked;
                Properties.Settings.Default.ProxyAddr = serverProxyTextBox.Text.Trim();
                int port;
                if (Int32.TryParse(portProxyTextBox.Text, out port))
                    Properties.Settings.Default.ProxyPort = port;
                Properties.Settings.Default.ProxyLogin = loginProxyTextBox.Text.Trim();
                Properties.Settings.Default.ProxyPass = passwordProxyTextBox.Text;
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
        }

        private void ReadSettings()
        {
            try
            {
                bool proxyUseAuto = Properties.Settings.Default.ProxyUseAuto;
                bool useDefaultCredentials = Properties.Settings.Default.UseDefaultCredentials;

                string proxyAddr = Properties.Settings.Default.ProxyAddr;
                int proxyPort = Properties.Settings.Default.ProxyPort;
                string proxyLogin = Properties.Settings.Default.ProxyLogin;
                string proxyPass = Properties.Settings.Default.ProxyPass;

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
