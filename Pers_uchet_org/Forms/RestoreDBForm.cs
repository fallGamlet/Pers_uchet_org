using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pers_uchet_org
{
    public partial class RestoreDBForm : Form
    {
        #region Поля

        private DataTable backupTable;
        private string columnDateTimeName = "dateTime";
        private string columnPathName = "path";

        #endregion
        public RestoreDBForm()
        {
            InitializeComponent();
        }

        private void RestoreDBForm_Load(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.IsBackupEnabled)
            {
                MainForm.ShowInfoMessage("Резервное копирование отключено в настройках!", "Предупреждение");
                restoreButton.Enabled = false;
            }
            string backupPath = Properties.Settings.Default.BackupPath;
            DirectoryInfo directoryInfo = new DirectoryInfo(backupPath);
            if (!Directory.Exists(backupPath))
            {
                restoreButton.Enabled = false;
            }
            else
            {
                //copyListBox.Items.Clear();
                backupTable = new DataTable();
                backupTable.Columns.Add(columnDateTimeName, typeof(string));
                backupTable.Columns.Add(columnPathName, typeof(string));
                foreach (FileInfo backupFile in directoryInfo.GetFiles("pu_bkp_????-??-??_(??-??-??).zip", SearchOption.TopDirectoryOnly))
                {
                    DataRow row = backupTable.NewRow();
                    row[columnDateTimeName] = ExtractDateTimeFromBackupName(backupFile.Name);
                    row[columnPathName] = backupFile.Name;
                    backupTable.Rows.Add(row);
                }

                copyListBox.DataSource = backupTable;
                copyListBox.ValueMember = columnPathName;
                copyListBox.DisplayMember = columnDateTimeName;
            }
        }

        private object ExtractDateTimeFromBackupName(string fileName)
        {
            string result = fileName.Substring(7, 4);
            result += "." + fileName.Substring(12, 2);
            result += "." + fileName.Substring(15, 2);
            result += "   " + fileName.Substring(19, 2);
            result += ":" + fileName.Substring(22, 2);
            result += ":" + fileName.Substring(25, 2);

            return result;
        }

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
            string backupStr = copyListBox.SelectedValue.ToString();
        }
    }
}
