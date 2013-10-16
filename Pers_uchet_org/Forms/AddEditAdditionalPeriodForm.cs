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
    public partial class AddEditAdditionalPeriodForm : Form
    {
        private int RepYear;
        BindingSource _classpercentView500BS;

        public long Code = 0;
        public DateTime Begin;
        public DateTime End;

        public AddEditAdditionalPeriodForm()
        {
            InitializeComponent();
        }

        public AddEditAdditionalPeriodForm(int RepYear)
            : this()
        {
            this.RepYear = RepYear;
        }

        private void AddEditAdditionalPeriodForm_Load(object sender, EventArgs e)
        {
            beginDateTimePicker.MinDate = DateTime.Parse(RepYear + "-01-01");
            beginDateTimePicker.MaxDate = DateTime.Parse(RepYear + "-12-31");
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
            endDateTimePicker.MaxDate = DateTime.Parse(RepYear + "-12-31");
            if (MainForm.ClasspercentViewTable != null)
            {
                _classpercentView500BS = new BindingSource();
                _classpercentView500BS.DataSource = new DataView(MainForm.ClasspercentViewTable);
                _classpercentView500BS.Filter = ClasspercentView.GetBindingSourceFilterFor500(DateTime.Parse(RepYear + "-01-01"));
                _classpercentView500BS.Sort = ClasspercentView.code;
                _classpercentView500BS.CurrentChanged += new EventHandler(_classpercentView500BS_CurrentChanged);
                codeComboBox.DataSource = _classpercentView500BS;
                codeComboBox.ValueMember = ClasspercentView.id;
                codeComboBox.DisplayMember = ClasspercentView.code;
                Code = (codeComboBox.SelectedValue != null) ? (long)codeComboBox.SelectedValue : 0;
                Begin = beginDateTimePicker.Value;
                End = endDateTimePicker.Value;
            }
        }

        void _classpercentView500BS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }

            DateTime date;
            if (!String.IsNullOrEmpty(row[ClasspercentView.dateBegin].ToString()))
            {
                date = (DateTime)row[ClasspercentView.dateBegin];
                if (date > DateTime.Parse(RepYear + "-01-01"))
                {
                    beginDateTimePicker.MinDate = date;
                }
            }

            if (!String.IsNullOrEmpty(row[ClasspercentView.dateEnd].ToString()))
            {
                date = (DateTime)row[ClasspercentView.dateEnd];
                beginDateTimePicker.MaxDate = date;
                endDateTimePicker.MaxDate = date;
            }

            Code = (long)row[ClasspercentView.id];
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
