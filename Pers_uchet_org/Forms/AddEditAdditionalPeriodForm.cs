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
        int _repYear;
        BindingSource _classpercentView500BS;

        public long Code;
        public string CodeName;
        public DateTime Begin;
        public DateTime End;

        public AddEditAdditionalPeriodForm()
        {
            InitializeComponent();
        }

        public AddEditAdditionalPeriodForm(int repYear)
            : this()
        {
            this._repYear = repYear;
            this.Begin = DateTime.Now.Date;
            this.End = DateTime.Now.Date;
            this.Code = 0;
            this.CodeName = "";
        }

        public AddEditAdditionalPeriodForm(int repYear, long classificatorId, DateTime beginDate, DateTime endDate)
            : this()
        {
            this._repYear = repYear;
            this.Code = classificatorId;
            this.Begin = beginDate;
            this.End = endDate;
        }

        private void AddEditAdditionalPeriodForm_Load(object sender, EventArgs e)
        {
            saveButton.Enabled = false;
            beginDateTimePicker.MinDate = DateTime.Parse(_repYear + "-01-01");
            beginDateTimePicker.MaxDate = DateTime.Parse(_repYear + "-12-31");
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
            endDateTimePicker.MaxDate = DateTime.Parse(_repYear + "-12-31");
            if (MainForm.ClasspercentViewTable != null)
            {
                _classpercentView500BS = new BindingSource();
                _classpercentView500BS.DataSource = new DataView(MainForm.ClasspercentViewTable);
                _classpercentView500BS.Filter = ClasspercentView.GetBindingSourceFilterFor500(DateTime.Parse(_repYear + "-01-01"));
                _classpercentView500BS.Sort = ClasspercentView.code;
                _classpercentView500BS.CurrentChanged += new EventHandler(_classpercentView500BS_CurrentChanged);
                codeComboBox.DataSource = _classpercentView500BS;
                codeComboBox.ValueMember = ClasspercentView.id;
                codeComboBox.DisplayMember = ClasspercentView.code;
                if (Code > 0)
                    codeComboBox.SelectedValue = Code;

                if (codeComboBox.SelectedValue != null)
                {
                    Code = (long)codeComboBox.SelectedValue;
                    CodeName = codeComboBox.Text;
                    saveButton.Enabled = true;
                }

                beginDateTimePicker.Value = Begin;
                endDateTimePicker.Value = End;
            }
        }

        void _classpercentView500BS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
                return;

            DateTime date;
            if (!String.IsNullOrEmpty(row[ClasspercentView.dateBegin].ToString()))
            {
                date = (DateTime)row[ClasspercentView.dateBegin];
                if (date > DateTime.Parse(_repYear + "-01-01"))
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
            CodeName = codeComboBox.Text;
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
