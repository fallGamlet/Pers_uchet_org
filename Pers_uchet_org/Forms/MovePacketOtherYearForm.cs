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
    public partial class MovePacketOtherYearForm : Form
    {
        private long listId;

        public MovePacketOtherYearForm()
        {
            InitializeComponent();
        }

        public MovePacketOtherYearForm(long list_id)
        {
            InitializeComponent();
            this.listId = list_id;
        }

        private void MovePacketOtherYearForm_Load(object sender, EventArgs e)
        {
            yearNumericUpDown.Value = MainForm.RepYear;
            labelMain.Text = labelMain.Text + listId.ToString();
        }

        private void movePacketButton_Click(object sender, EventArgs e)
        {
            StajDohodForm.newRepYear = (int)yearNumericUpDown.Value;
        }
    }
}
