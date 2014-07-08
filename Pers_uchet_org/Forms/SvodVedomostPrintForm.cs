using System;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class SvodVedomostPrintForm : Form
    {
        public SvodVedomostPrintForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string Performer {
            get { return this.nameBox.Text; }
            set { this.nameBox.Text = value; }
        }

        public DateTime PrintDate
        {
            get { return this.printDate.Value; }
            set { this.printDate.Value = value; }
        }
    }
}
