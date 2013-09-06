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
    public partial class AnketaUvolitForm : Form
    {
        public AnketaUvolitForm()
        {
            InitializeComponent();
        }

        public DateTime DismissDate
        {
            get { return this.dateBox.Value; }
        }
    }
}
