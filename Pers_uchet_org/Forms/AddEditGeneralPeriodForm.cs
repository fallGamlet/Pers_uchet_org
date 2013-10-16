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
    public partial class AddEditGeneralPeriodForm : Form
    {
        private int RepYear;

        public DateTime Begin;
        public DateTime End;

        public AddEditGeneralPeriodForm()
        {
            InitializeComponent();
        }

        public AddEditGeneralPeriodForm(int RepYear):this()
        {
            this.RepYear = RepYear;
        }

        private void AddEditGeneralPeriodForm_Load(object sender, EventArgs e)
        {
            beginDateTimePicker.MinDate = DateTime.Parse(RepYear + "-01-01");
            beginDateTimePicker.MaxDate = DateTime.Parse(RepYear + "-12-31");
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
            endDateTimePicker.MaxDate = DateTime.Parse(RepYear + "-12-31");

            Begin = beginDateTimePicker.Value;
            End = endDateTimePicker.Value;
        }

        private void beginDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
            Begin = beginDateTimePicker.Value;
        }

        private void endDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            End = endDateTimePicker.Value;
        }
    }
}
