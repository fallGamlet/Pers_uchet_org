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
    public partial class SvodVedomostForm : Form
    {
        public SvodVedomostForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            SvodVedomostEditDocumentForm tmpform = new SvodVedomostEditDocumentForm();
            tmpform.Owner = this;
            tmpform.ShowDialog();
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            SvodVedomostPrintForm tmpform = new SvodVedomostPrintForm();
            tmpform.Owner = this;
            tmpform.ShowDialog();
        }
    }
}
