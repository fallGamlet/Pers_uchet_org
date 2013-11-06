using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class OperatorEnterForm : Form
    {
        #region Конструктор
        public OperatorEnterForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Свойства
        public string Login
        {
            get { return this.loginBox.Text; }
            set { this.loginBox.Text = value; }
        }

        public string Password
        {
            get { return this.passwordBox.Text; }
            set { this.passwordBox.Text = value; }
        }
        #endregion

        private void loginBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                passwordBox.Focus();
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                acceptButton.Focus();
        }

        private void OperatorEnterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
