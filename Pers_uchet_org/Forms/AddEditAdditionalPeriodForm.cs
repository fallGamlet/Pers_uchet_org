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
        #region Поля
        int _repYear;
        BindingSource _classpercentView500BS;
        BindingSource _dopPeriodBS;
        bool isNew;

        public long Code;
        public string CodeName;
        public DateTime Begin;
        public DateTime End;
         #endregion

        #region Конструктор и инициализатор
        public AddEditAdditionalPeriodForm()
        {
            InitializeComponent();
        }

        public AddEditAdditionalPeriodForm(int repYear, BindingSource periodBS)
            : this()
        {
            this._repYear = repYear;
            this._dopPeriodBS = periodBS;
            this.Begin =  new DateTime(_repYear, DateTime.Now.Date.Month, DateTime.Now.Date.Day);
            this.End = new DateTime(_repYear, DateTime.Now.Date.Month, DateTime.Now.Date.Day);
            this.Code = 0;
            this.CodeName = "";
            isNew = true;
        }

        public AddEditAdditionalPeriodForm(int repYear, long classificatorId, BindingSource periodBS, DateTime beginDate, DateTime endDate)
            : this(repYear, periodBS)
        {
            this.Code = classificatorId;
            this.Begin = beginDate;
            this.End = endDate;
            isNew = false;
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
        #endregion

        #region Методы - обработчики событий
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
        }

        private void beginDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Code = (long)codeComboBox.SelectedValue;
            CodeName = codeComboBox.Text;
            Begin = beginDateTimePicker.Value;
            End = endDateTimePicker.Value;

            bool isAllRight = true;
            DateTime begin;
            DateTime end;
            foreach (DataRowView row in _dopPeriodBS)
            {
                if (row != (_dopPeriodBS.Current as DataRowView) || isNew)
                {
                    begin = (DateTime)row[DopPeriod.beginDate];
                    end = (DateTime)row[DopPeriod.endDate];
                    if (this.Begin <= end && this.Begin >= begin)
                        isAllRight = false;
                    if (this.End <= end && this.End >= begin)
                        isAllRight = false;
                    if (this.End >= end && this.Begin <= begin)
                        isAllRight = false;
                }
            }
            if (isAllRight)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            else
            {
                MainForm.ShowWarningMessage("Указанный период пересекается с уже имеющимся периодом!", "Ошибка добавления периода");
            }
        }
        #endregion
    }
}
