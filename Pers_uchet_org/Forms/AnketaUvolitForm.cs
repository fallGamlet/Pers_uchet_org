using System;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
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