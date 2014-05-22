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
    public partial class SvodVedomostPrintForm : Form
    {
        public SvodVedomostPrintForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
