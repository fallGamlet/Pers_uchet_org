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
    public partial class SvodVedomostEditDocumentForm : Form
    {
        public SvodVedomostEditDocumentForm()
        {
            InitializeComponent();

            int rowCount = 12;
            this.dataView.Rows.Add(rowCount);
            for (int i = 0; i < rowCount; i++)
                this.dataView[0, i].Value = i + 1;
            
        }

        private void printButton_Click(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
