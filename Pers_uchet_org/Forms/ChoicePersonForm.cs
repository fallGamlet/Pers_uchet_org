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
    public partial class ChoicePersonForm : Form
    {
        public ChoicePersonForm()
        {
            InitializeComponent();
        }

        private void choiceButton_Click(object sender, EventArgs e)
        {
            AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form();
            if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }
    }
}
