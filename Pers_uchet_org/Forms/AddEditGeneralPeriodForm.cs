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
        #region Поля
        int _repYear;
        BindingSource _generalPeriodBS;
        BindingSource _specPeriodBS;
        bool _isNew;

        public DateTime Begin;
        public DateTime End;
        #endregion

        #region Конструктор и инициализатор
        private AddEditGeneralPeriodForm()
        {
            InitializeComponent();
            _repYear = DateTime.Now.Year;
            _isNew = true;
        }

        public AddEditGeneralPeriodForm(int repYear, BindingSource generalPeriodBS, BindingSource specialPeriodBS)
            : this()
        {
            this._repYear = repYear;
            this._generalPeriodBS = generalPeriodBS;
            this._specPeriodBS = specialPeriodBS;
            this.Begin = DateTime.Parse(_repYear + "-01-01");
            this.End = DateTime.Parse(_repYear + "-12-31");
            _isNew = true;
        }

        public AddEditGeneralPeriodForm(int repYear, BindingSource generalPeriodBS, BindingSource specialPeriodBS, DateTime beginDate, DateTime endDate)
            : this(repYear, generalPeriodBS, specialPeriodBS)
        {
            this.Begin = beginDate;
            this.End = endDate;
            _isNew = false;
        }


        private void AddEditGeneralPeriodForm_Load(object sender, EventArgs e)
        {
            beginDateTimePicker.MinDate = DateTime.Parse(_repYear + "-01-01");
            beginDateTimePicker.MaxDate = DateTime.Parse(_repYear + "-12-31");
            endDateTimePicker.MaxDate = DateTime.Parse(_repYear + "-12-31");

            beginDateTimePicker.Value = Begin;
            endDateTimePicker.Value = End;
        }
        #endregion

        #region Методы - обработчики событий
        private void beginDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Begin = beginDateTimePicker.Value;
            End = endDateTimePicker.Value;

            bool isAllRight = true;
            bool isPeriodCross = false;
            DateTime begin;
            DateTime end;
            foreach (DataRowView row in _generalPeriodBS)
            {
                if (row != (_generalPeriodBS.Current as DataRowView) || _isNew)
                {
                    begin = (DateTime)row[GeneralPeriod.beginDate];
                    end = (DateTime)row[GeneralPeriod.endDate];
                    if (this.Begin <= end && this.Begin >= begin)
                        isPeriodCross = true;
                    if (this.End <= end && this.End >= begin)
                        isPeriodCross = true;
                    if (this.End >= end && this.Begin <= begin)
                        isPeriodCross = true;
                }
            }
            if (isPeriodCross)
            {
                MainForm.ShowWarningMessage("Указанный период пересекается с уже имеющимся периодом!", "Ошибка добавления периода");
                isAllRight = false;
            }

            if (!_isNew)
            {
                isPeriodCross = false;
                DateTime beginCurrent = (DateTime)(_generalPeriodBS.Current as DataRowView)[GeneralPeriod.beginDate];
                DateTime endCurrent = (DateTime)(_generalPeriodBS.Current as DataRowView)[GeneralPeriod.endDate];
                foreach (DataRowView row in _specPeriodBS)
                {
                    begin = (DateTime)row[SpecialPeriod.beginDate];
                    end = (DateTime)row[SpecialPeriod.endDate];
                    if (begin >= beginCurrent && end <= endCurrent)
                    {
                        if (this.Begin > begin || this.End < end)
                            isPeriodCross = true;
                    }
                }
                if (isPeriodCross)
                {
                    MainForm.ShowWarningMessage("Среди записей специального стажа есть периоды выходящие за указанные границы!", "Ошибка добавления периода");
                    isAllRight = false;
                }
            }

            if (isAllRight)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        #endregion
    }
}
