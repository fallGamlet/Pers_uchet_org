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
            get { return this.passwordBox.Text.Trim(); }
            set { this.passwordBox.Text = value; }
        }

        public string PasswordOld
        {
            get { return _oldpassword; }
            set
            {
                if (value != null)
                    _oldpassword = value;
                else
                    _oldpassword = "";
                if (_oldpassword.Length <= 0)
                {
                    this.oldpasswordLabel.Enabled = false;
                    this.oldpasswordLabel.Visible = false;
                    this.oldPasswordBox.Enabled = false;
                    this.oldPasswordBox.Visible = false;
                    this.showPassButton1.Enabled = false;
                    this.showPassButton1.Visible = false;
                }
                else
                {
                    this.oldpasswordLabel.Enabled = true;
                    this.oldpasswordLabel.Visible = true;
                    this.oldPasswordBox.Enabled = true;
                    this.oldPasswordBox.Visible = true;
                    this.showPassButton1.Enabled = true;
                    this.showPassButton1.Visible = true;

                }
            }
        }

        public string PasswordConf
        {
            get { return this.confPasswordBox.Text; }
            set { this.confPasswordBox.Text = value; }
        }
        #endregion

        #region Методы - обработчики событий
        private void ChangePasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                bool res = _oldpassword == this.oldPasswordBox.Text;
                string err = "";
                if (!res)
                    err += "Неверно указан старый пароль!";

                if (Password != PasswordConf)
                {
                    res &= false;
                    err += "\nНовый пароль и его подтверждение не совпадают!";
                }

                if (!res)
                    MainForm.ShowWarningMessage(err, "Внимание");
                e.Cancel = !res;
            }
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            bool res = Password == PasswordConf;
            confPasswordBox.BackColor = res ? Color.Aquamarine : Color.LightSalmon;
            this.okButton.Enabled = res;
        }

        private void showPassButton1_MouseDown(object sender, MouseEventArgs e)
        {
            oldPasswordBox.UseSystemPasswordChar = false;
        }

        private void showPassButton1_MouseUp(object sender, MouseEventArgs e)
        {
            oldPasswordBox.UseSystemPasswordChar = true;
        }

        private void showPassButton2_MouseDown(object sender, MouseEventArgs e)
        {
            passwordBox.UseSystemPasswordChar = false;
        }

        private void showPassButton2_MouseUp(object sender, MouseEventArgs e)
        {
            passwordBox.UseSystemPasswordChar = true;
        }

        private void showPassButton3_MouseDown(object sender, MouseEventArgs e)
        {
            confPasswordBox.UseSystemPasswordChar = false;
        }

        private void showPassButton3_MouseUp(object sender, MouseEventArgs e)
        {
            confPasswordBox.UseSystemPasswordChar = true;
        }

        private void oldPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                passwordBox.Focus();
        }
       
        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                confPasswordBox.Focus();
        }

        private void confPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                okButton.Focus();
        }
        #endregion
    }
}
