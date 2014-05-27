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
    }
}