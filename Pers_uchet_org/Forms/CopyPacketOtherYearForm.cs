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
    public partial class CopyPacketOtherYearForm : Form
    {
        private long listId;

        public CopyPacketOtherYearForm()
        {
            InitializeComponent();
        }

        public CopyPacketOtherYearForm(long list_id)
        {
            InitializeComponent();
            this.listId = list_id;
        }

        private void CopyPacketOtherYearForm_Load(object sender, EventArgs e)
        {
            yearNumericUpDown.Value = MainForm.RepYear;
            labelMain.Text = labelMain.Text + listId.ToString();
        }

        private void copyPacketButton_Click(object sender, EventArgs e)
        {
            StajDohodForm.NewRepYear = (int)yearNumericUpDown.Value;
        }
    }
}
