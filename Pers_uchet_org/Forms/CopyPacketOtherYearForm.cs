using System;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class CopyPacketOtherYearForm : Form
    {
        private long _listId;

        public CopyPacketOtherYearForm()
        {
            InitializeComponent();
        }

        public CopyPacketOtherYearForm(long listId)
        {
            InitializeComponent();
            this._listId = listId;
        }

        private void CopyPacketOtherYearForm_Load(object sender, EventArgs e)
        {
            yearNumericUpDown.Value = MainForm.RepYear;
            labelMain.Text = labelMain.Text + _listId;
        }

        private void copyPacketButton_Click(object sender, EventArgs e)
        {
            StajDohodForm.NewRepYear = (int)yearNumericUpDown.Value;
        }
    }
}