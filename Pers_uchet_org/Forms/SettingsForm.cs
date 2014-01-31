using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ionic.Zip;
using Pers_uchet_org.Properties;


namespace Pers_uchet_org
{
    public partial class SettingsForm : Form
    {
        #region Поля
        string _connection;
        private FolderBrowserDialog folderDialog;
        #endregion

        // конструктор класса
        public SettingsForm(string connection)
        {
            InitializeComponent();
            _connection = connection;
            folderDialog = new FolderBrowserDialog();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            ReadDefaultSetting();
            //ReadCustomSetting();

            //databasePathTextBox.ReadOnly = true;
            //databaseBrowseButton.Enabled = false;
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
            }
            catch (Exception e)
            {
                MainForm.ShowErrorFlexMessage(e.Message, "Ошибка считывания настроек");
            }
        }

        private bool ReadCustomSetting()
        {
            try
            {
                isBackupEnableCheckBox.Checked = Settings.Default.IsBackupEnabled;
                backupMaxCountBox.Value = Settings.Default.BackupMaxCount;
                backupPathTextBox.Text = Settings.Default.BackupPath;
                return true;
            }
            catch (Exception e)
            {
                Debug.Print("Ошибка считывания настроек:\n{0}", e.Message);
                return false;
            }
        }

        private bool SaveProperties()
        {
            try
            {
                Settings.Default.IsBackupEnabled = isBackupEnableCheckBox.Checked;
                Settings.Default.BackupMaxCount = backupMaxCountBox.Value;
                if (Directory.Exists(backupPathTextBox.Text.Trim()) || (MainForm.ShowQuestionMessage("Папка для резервного копирования не найдена!\r\nВы уверены что хотите сохранить этот путь?", "Сохранение настроек") == DialogResult.Yes))
                    Settings.Default.BackupPath = backupPathTextBox.Text.Trim().Replace('\\', '/');

                if (File.Exists(databasePathTextBox.Text.Trim()) || (MainForm.ShowQuestionMessage("Файл базы данных не найден!\r\nВы уверены что хотите сохранить в этот путь?", "Сохранение настроек") == DialogResult.Yes))
                    Settings.Default.DataBasePath = databasePathTextBox.Text.Trim().Replace('\\', '/');

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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                OpenPathInExplorer(databasePathTextBox.Text.Substring(0, databasePathTextBox.Text.LastIndexOf('\\')));
                return;
            }

            folderDialog.Description = "Выберите папку c базой данных";
            folderDialog.ShowNewFolderButton = false;
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                databasePathTextBox.Text = folderDialog.SelectedPath.TrimEnd('\\') + "\\" +
                                           Properties.Settings.Default.DataBaseFileName;
            }
        }

        private void backupBrowseButton_Click(object sender, EventArgs e)
        {
            //Если при нажатии зажат shift, то открыть папку в проводнике
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                OpenPathInExplorer(backupPathTextBox.Text);
                return;
            }

            folderDialog.Description = "Выберите папку для резервных копий";
            folderDialog.ShowNewFolderButton = true;
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                backupPathTextBox.Text = folderDialog.SelectedPath;
            }
        }

        private void vacuumButton_Click(object sender, EventArgs e)
        {
            if (MainForm.ShowQuestionMessage("Команда VACUUM блокирует таблицы в монопольном режиме.\nЭто означает, что все запросы, использующие обрабатываемую базу, приостанавливаются и ожидают снятия блокировки.\n\nПродолжить?", "Предупреждение") != DialogResult.Yes)
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

        #endregion
    }
}
