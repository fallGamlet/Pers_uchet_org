using System;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class OperatorsEditPersonForm : Form
    {
        public OperatorsEditPersonForm()
        {
            InitializeComponent();
        }

        public string OperatorName
        {
            get { return nameBox.Text; }
            set { nameBox.Text = value; }
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