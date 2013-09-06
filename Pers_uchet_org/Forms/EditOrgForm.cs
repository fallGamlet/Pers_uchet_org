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
    public partial class EditOrgForm : Form
    {
        public EditOrgForm()
        {
            InitializeComponent();
        }

        #region Свойства
        public string RegnumOrg
        {
            get { return this.regnumorgBox.Text; }
            set { this.regnumorgBox.Text = value; }
        }

        public string NameOrg
        {
            get { return this.nameorgBox.Text; }
            set { this.nameorgBox.Text = value; }
        }

        public string ChiefpostOrg
        {
            get { return this.bosspostorgBox.Text; }
            set { this.bosspostorgBox.Text = value; }
        }

        public string ChieffioOrg
        {
            get { return this.bossfioorgBox.Text; }
            set { this.bossfioorgBox.Text = value; }
        }

        public string BookerfioOrg
        {
            get { return this.buhfioorgBox.Text; }
            set { this.buhfioorgBox.Text = value; }
        }
        #endregion
    }
}
