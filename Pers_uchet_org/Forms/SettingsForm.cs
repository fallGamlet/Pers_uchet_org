using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Pers_uchet_org.Properties;

namespace Pers_uchet_org.Forms
{
    public partial class SettingsForm : Form
    {
        #region Поля

        private string _connection;
        private FolderBrowserDialog _folderBrowserDialog;

        #endregion

        // конструктор класса
        public SettingsForm(string connection)
        {
            InitializeComponent();
            _connection = connection;
            _folderBrowserDialog = new FolderBrowserDialog();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            ReadDefaultSetting();
            //ReadCustomSetting();

            //databasePathTextBox.ReadOnly = true;
            //databaseBrowseButton.Enabled = false;

            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(autoProxyRadioButton, "Используются настройки из обозревателя IE");
        }

        #region Методы - свои

        private void ReadDefaultSetting()
        {
            try
            {
                isBackupEnableCheckBox.Checked = Settings.Default.IsBackupEnabled;
                backupMaxCountBox.Value = Settings.Default.BackupMaxCount;
                backupPathTextBox.Text = Settings.Default.BackupPath.Replace('/', '\\');

                backupMaxCountBox.Enabled = isBackupEnableCheckBox.Checked;
                backupPathTextBox.Enabled = isBackupEnableCheckBox.Checked;

                databasePathTextBox.Text = Settings.Default.DataBasePath.Replace('/', '\\');


                bool proxyUseAuto = Settings.Default.ProxyUseAuto;
                bool useDefaultCredentials = Settings.Default.UseDefaultCredentials;

                string proxyAddr = Settings.Default.ProxyAddr;
                int proxyPort = Settings.Default.ProxyPort;
                string proxyLogin = Settings.Default.ProxyLogin;
                string proxyPass = Settings.Default.ProxyPass;

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

        //private bool ReadCustomSetting()
        //{
        //    try
        //    {
        //        isBackupEnableCheckBox.Checked = Settings.Default.IsBackupEnabled;
        //        backupMaxCountBox.Value = Settings.Default.BackupMaxCount;
        //        backupPathTextBox.Text = Settings.Default.BackupPath;
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.Print("Ошибка считывания настроек:\n{0}", e.Message);
        //        return false;
        //    }
        //}

        private bool SaveProperties()
        {
            try
            {
                Settings.Default.IsBackupEnabled = isBackupEnableCheckBox.Checked;
                Settings.Default.BackupMaxCount = backupMaxCountBox.Value;
                if (Directory.Exists(backupPathTextBox.Text.Trim()) ||
                    (MainForm.ShowQuestionMessage(
                        "Папка для резервного копирования не найдена!\r\nВы уверены что хотите сохранить этот путь?",
                        "Сохранение настроек") == DialogResult.Yes))
                    Settings.Default.BackupPath = backupPathTextBox.Text.Trim().Replace('\\', '/');

                if (File.Exists(databasePathTextBox.Text.Trim()) ||
                    (MainForm.ShowQuestionMessage(
                        "Файл базы данных не найден!\r\nВы уверены что хотите сохранить в этот путь?",
                        "Сохранение настроек") == DialogResult.Yes))
                    Settings.Default.DataBasePath = databasePathTextBox.Text.Trim().Replace('\\', '/');

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

        private void OpenPathInExplorer(string path)
        {
            Process process = new Process();
            process.StartInfo.FileName = "explorer";
            process.StartInfo.Arguments = path;
            process.Start();
            process.Close();
        }

        //void zip_AddProgress(object sender, AddProgressEventArgs e)
        //{
        //    this.Text = "";
        //}

        #endregion

        #region Методы - обработчики событий

        private void saveButton_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    this.orgTableAdapter1.Update(_orgTable);
            //    MessageBox.Show(this, "Данные были успешно сохранены", "Сохранение прошло успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch(Exception err)
            //{
            //    MessageBox.Show(this, "Бали обнаружены ошибки при попытке сохранить данные в базу данных. Сообщение: "+err, "Сохранение не было осуществено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            if (SaveProperties())
                MainForm.ShowInfoMessage("Настройки успешно сохранены", "Сохранение настроек");
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void isBackupEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            backupMaxCountBox.Enabled = isBackupEnableCheckBox.Checked;
            backupPathTextBox.Enabled = isBackupEnableCheckBox.Checked;
            backupBrowseButton.Enabled = isBackupEnableCheckBox.Checked;
            //createBackupButton.Enabled = isBackupEnableCheckBox.Checked;
        }

        private void databaseBrowseButton_Click(object sender, EventArgs e)
        {
            //Если при нажатии зажат shift, то открыть папку в проводнике
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                OpenPathInExplorer(databasePathTextBox.Text.Substring(0, databasePathTextBox.Text.LastIndexOf('\\')));
                return;
            }

            _folderBrowserDialog.Description = "Выберите папку c базой данных";
            _folderBrowserDialog.ShowNewFolderButton = false;
            if (_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                databasePathTextBox.Text = Path.Combine(_folderBrowserDialog.SelectedPath, Settings.Default.DataBaseFileName);
            }
        }

        private void backupBrowseButton_Click(object sender, EventArgs e)
        {
            //Если при нажатии зажат shift, то открыть папку в проводнике
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                OpenPathInExplorer(backupPathTextBox.Text);
                return;
            }

            _folderBrowserDialog.Description = "Выберите папку для резервных копий";
            _folderBrowserDialog.ShowNewFolderButton = true;
            if (_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                backupPathTextBox.Text = _folderBrowserDialog.SelectedPath;
            }
        }

        private void vacuumButton_Click(object sender, EventArgs e)
        {
            if (
                MainForm.ShowQuestionMessage(
                    "Команда VACUUM блокирует таблицы в монопольном режиме.\nЭто означает, что все запросы, использующие обрабатываемую базу, приостанавливаются и ожидают снятия блокировки.\n\nПродолжить?",
                    "Предупреждение") != DialogResult.Yes)
                return;

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connection))
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "VACUUM";
                        if (command.ExecuteNonQuery() < 0)
                            MainForm.ShowErrorMessage("Операция не выполнена!", "Ошибка операции VACUUM");
                    }
                }
            }
            catch (Exception exception)
            {
                MainForm.ShowErrorMessage(exception.Message, "Ошибка операции VACUUM");
            }
        }

        private void createBackupButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(backupPathTextBox.Text.Trim()))
            {
                MainForm.ShowErrorMessage("Папка для резервного копирования не найдена!", "Создание резервной копии");
                return;
            }

            if (!File.Exists(databasePathTextBox.Text.Trim()))
            {
                MainForm.ShowErrorMessage("Файл базы данных не найден!", "Создание резервной копии");
                return;
            }

            try
            {
                createBackupButton.Enabled = false;
                string backupPath = backupPathTextBox.Text.Trim().Trim('\\');
                string databasePath = databasePathTextBox.Text.Trim();
                Backup.CreateBackup(backupPath, databasePath, Backup.TypeBackup.ManualBackup);
                MainForm.ShowInfoMessage("Резервная копия успешно создана!", "Создание резервной копии");
            }
            catch (Exception exception)
            {
                MainForm.ShowErrorMessage(exception.Message, "Ошибка создания резервной копии");
            }
            finally
            {
                createBackupButton.Enabled = true;
            }
        }

        private void autoProxyRadioButton_CheckedChanged(object sender, EventArgs e)
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

        private void showPassProxyButton_MouseDown(object sender, MouseEventArgs e)
        {
            passwordProxyTextBox.UseSystemPasswordChar = false;
        }

        private void showPassProxyButton_MouseUp(object sender, MouseEventArgs e)
        {
            passwordProxyTextBox.UseSystemPasswordChar = true;
        }

        #endregion
    }
}