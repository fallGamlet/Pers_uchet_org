using System;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class MovePacketOtherYearForm : Form
    {
        private long _listId;

        public MovePacketOtherYearForm()
        {
            InitializeComponent();
        }

        public MovePacketOtherYearForm(long listId)
        {
            InitializeComponent();
            _listId = listId;
        }

        private void MovePacketOtherYearForm_Load(object sender, EventArgs e)
        {
            yearNumericUpDown.Value = MainForm.RepYear;
            labelMain.Text = labelMain.Text + _listId.ToString();
        }

        private void movePacketButton_Click(object sender, EventArgs e)
        {
            StajDohodForm.NewRepYear = (int)yearNumericUpDown.Value;
        }
    }
}