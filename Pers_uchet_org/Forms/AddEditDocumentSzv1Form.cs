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
    public partial class AddEditDocumentSzv1Form : Form
    {
        public AddEditDocumentSzv1Form()
        {
            InitializeComponent();
        }

        private void addGeneralPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditGeneralPeriodForm generalPeriodForm = new AddEditGeneralPeriodForm();
            if (generalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void editGeneralPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditGeneralPeriodForm generalPeriodForm = new AddEditGeneralPeriodForm();
            if (generalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void delGeneralPeriodButton_Click(object sender, EventArgs e)
        {

        }

        private void addAdditionalPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditAdditionalPeriodForm additionalPeriodForm = new AddEditAdditionalPeriodForm();
            if (additionalPeriodForm.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
 
            }
        }

        private void editAdditionalPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditAdditionalPeriodForm additionalPeriodForm = new AddEditAdditionalPeriodForm();
            if (additionalPeriodForm.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
 
            }
        }

        private void delAdditionalPeriodButton_Click(object sender, EventArgs e)
        {

        }

        private void addSpecialPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditSpecialPeriodForm specialPeriodForm = new AddEditSpecialPeriodForm();
            if (specialPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void editSpecialPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditSpecialPeriodForm specialPeriodForm = new AddEditSpecialPeriodForm();
            if (specialPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void delSpecialPeriodButton_Click(object sender, EventArgs e)
        {

        }
    }
}
