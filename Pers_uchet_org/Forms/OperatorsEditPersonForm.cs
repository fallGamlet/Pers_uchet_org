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
    public partial class OperatorsEditPersonForm : Form
    {
        public OperatorsEditPersonForm()
        {
            InitializeComponent();
        }

        public string OperatorName
        {
            get { return this.nameBox.Text; }
            set { this.nameBox.Text = value; }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(OperatorName))
            {
                MainForm.ShowInfoMessage("Имя оператора не может быть пустым!", "Ошибка добавления оператора");
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
