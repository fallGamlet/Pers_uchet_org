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
        public DateTime Begin;
        public DateTime End;

        private int _repYear;

        public AddEditGeneralPeriodForm()
        {
            InitializeComponent();
        }

        public AddEditGeneralPeriodForm(int repYear)
            : this()
        {
            this._repYear = repYear;
            this.Begin = DateTime.Now.Date;
            this.End = DateTime.Now.Date;
        }

        public AddEditGeneralPeriodForm(int repYear, DateTime beginDate, DateTime endDate)
            : this()
        {
            this._repYear = repYear;
            this.Begin = beginDate;
            this.End = endDate;
        }


        private void AddEditGeneralPeriodForm_Load(object sender, EventArgs e)
        {
            beginDateTimePicker.MinDate = DateTime.Parse(_repYear + "-01-01");
            beginDateTimePicker.MaxDate = DateTime.Parse(_repYear + "-12-31");
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
            endDateTimePicker.MaxDate = DateTime.Parse(_repYear + "-12-31");


            beginDateTimePicker.Value = Begin;
            endDateTimePicker.Value = End;
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
