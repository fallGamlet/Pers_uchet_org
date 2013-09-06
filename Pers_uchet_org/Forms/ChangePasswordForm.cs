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
    public partial class ChangePasswordForm : Form
    {
        #region Поля
        string _oldpassword;
        #endregion

        #region Конструктор
        public ChangePasswordForm()
        {
            InitializeComponent();
            _oldpassword = "";
        }
        #endregion

        #region Свойства
        public string Password
        {
            get { return this.passwordBox.Text; }
            set { this.passwordBox.Text = value; }
        }

        public string PasswordOld
        {
            get { return _oldpassword; }
            set 
            {
                if (value != null)
                    _oldpassword = value.Trim();
                else
                    _oldpassword = "";
                if (_oldpassword.Length <= 0)
                {
                    this.oldpasswordLabel.Enabled = false;
                    this.oldpasswordLabel.Visible = false;
                    this.oldpasswordBox.Enabled = false;
                    this.oldpasswordBox.Visible = false;
                }
                else
                {
                    this.oldpasswordLabel.Enabled = true;
                    this.oldpasswordLabel.Visible = true;
                    this.oldpasswordBox.Enabled = true;
                    this.oldpasswordBox.Visible = true;
                }
            }
        }

        public string PasswordConf
        {
            get { return this.confpasswordBox.Text; }
            set { this.confpasswordBox.Text = value; }
        }
        #endregion

        #region Методы - обработчики событий
        private void ChangePasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                bool res = true;
                res &= _oldpassword.Length > 0 ? _oldpassword == PasswordOld : true;
                if (!res)
                    MainForm.ShowWarningMessage("Не совпадают текущий пароль и новый пароль.\nСмена пароля невозможна!", "Внимание");
                res &= Password == PasswordConf;
                if (!res)
                    MainForm.ShowWarningMessage("Не совпадают введенный пароль и с полем его подтверждения!", "Внимание");
                e.Cancel = !res;
            }
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            bool res  = _oldpassword.Trim().Length <= 0 || _oldpassword == this.oldpasswordBox.Text.Trim();
            res &= Password.Trim().Length > 0 && Password == PasswordConf;
            this.okButton.Enabled = res;
        }
        #endregion
    }
}
