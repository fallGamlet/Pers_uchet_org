using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pers_uchet_org
{
    public partial class SettingsForm : Form
    {
        #region Поля
        //
        string _connection;

        #endregion
        
        // конструктор класса
        public SettingsForm(string connection)
        {
            InitializeComponent();
            _connection = connection;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }

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
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void browseprinterButton_Click(object sender, EventArgs e)
        {
            SettingsPrinterForm tmpForm = new SettingsPrinterForm();
            tmpForm.Owner = this;
            tmpForm.ShowDialog(this);
        }
        #endregion
    }
}
