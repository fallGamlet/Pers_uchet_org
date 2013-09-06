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
    public partial class SettingsEditDepForm : Form
    {
        public SettingsEditDepForm()
        {
            InitializeComponent();
        }

        #region Свойства
        public string NameDep
        {
            get { return this.depnameBox.Text; }
            set { this.depnameBox.Text = value; }
        }
        #endregion
    }
}
