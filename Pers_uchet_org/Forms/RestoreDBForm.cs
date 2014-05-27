using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Pers_uchet_org.Properties;

namespace Pers_uchet_org.Forms
{
    public partial class RestoreDBForm : Form
    {
        #region Поля

        private DataTable _backupTable;

        #endregion

        #region Инициализаторы и конструкторы

        public RestoreDBForm()
        {
            InitializeComponent();
        }

        private void RestoreDBForm_Load(object sender, EventArgs e)
        {
            if (!Settings.Default.IsBackupEnabled)
            {
                MainForm.ShowInfoMessage("Резервное копирование отключено в настройках!", "Предупреждение");
                //restoreButton.Enabled = false;
            }

            string backupPath = Settings.Default.BackupPath;
            backupFolderLabel.Text += backupPath;
            DirectoryInfo directoryInfo = new DirectoryInfo(backupPath);
            if (!Directory.Exists(backupPath))
            {
                restoreButton.Enabled = false;
            }
            else
            {
                //copyListBox.Items.Clear();
                _backupTable = Backup.CreateTable();
                Backup.FillTable(_backupTable, directoryInfo, Backup.SearchPatternType.All);

                copyListBox.DataSource = _backupTable;
                copyListBox.ValueMember = Backup.columnPathName;
                copyListBox.DisplayMember = Backup.columnDateTimeName;
            }
        }

        #endregion

        #region Методы - обработчики событий

        private void cancelButton_Click(object sender, EventArgs e)
        {
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            if (copyListBox.SelectedValue == null)
            {
                MainForm.ShowInfoMessage("Резервная копия не выбрана!", "Ошибка восстановления");
                return;
            }

            string databaseFilePath = Settings.Default.DataBasePath;

            if (!File.Exists(Settings.Default.BackupPath + "\\" + copyListBox.SelectedValue.ToString()))
            {
                MainForm.ShowErrorMessage("Файл резервной копии не найден!", "Ошибка восстановления");
                return;
            }

            //if (!File.Exists(databaseFilePath.Trim()))
            //{
            //    MainForm.ShowErrorMessage("Файл базы данных не найден!", "Ошибка восстановления");
            //    return;
            //}

            if (MainForm.ShowQuestionMessage(
                "Убедитесь, что в данный момент больше никто не работает с программой!\nПосле восстановления программа будет перезапущена!\n\nПродолжить восстановление?",
                "Восстановление из резервной копии") != DialogResult.Yes)
            {
                return;
            }
            try
            {
                restoreButton.Enabled = false;
                File.Delete(databaseFilePath + ".tmp");
                Backup.RestoreBackup(Settings.Default.BackupPath, copyListBox.SelectedValue.ToString(),
                    databaseFilePath);
                MainForm.ShowInfoMessage("Восстановление успешно!", "Восстановление из резервной копии");
                Backup.isBackupCreate = Backup.BackupCreate.DoNotCreate;

                //Перезапустить программу
                Application.Restart();
            }
            catch (Exception exception)
            {
                MainForm.ShowErrorMessage(exception.Message, "Ошибка восстановления из резервной копии");
            }
            finally
            {
                restoreButton.Enabled = true;
            }
        }

        private void copyListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (copyListBox.SelectedValue != null)
            {
                backupFileNameLabel.Text = "Имя файла: " + copyListBox.SelectedValue.ToString();
            }
        }

        #endregion
    }
}