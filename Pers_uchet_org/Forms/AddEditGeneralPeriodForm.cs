using System;
using System.Data;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class AddEditGeneralPeriodForm : Form
    {
        #region Поля

        private int _repYear;
        private BindingSource _generalPeriodBS;
        private BindingSource _specPeriodBS;
        private bool _isNew;

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
            _repYear = repYear;
            _generalPeriodBS = generalPeriodBS;
            _specPeriodBS = specialPeriodBS;
            Begin = DateTime.Parse(_repYear + "-01-01");
            End = DateTime.Parse(_repYear + "-12-31");
            _isNew = true;
        }

        public AddEditGeneralPeriodForm(int repYear, BindingSource generalPeriodBS, BindingSource specialPeriodBS,
            DateTime beginDate, DateTime endDate)
            : this(repYear, generalPeriodBS, specialPeriodBS)
        {
            Begin = beginDate;
            End = endDate;
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
                    if (Begin <= end && Begin >= begin)
                        isPeriodCross = true;
                    if (End <= end && End >= begin)
                        isPeriodCross = true;
                    if (End >= end && Begin <= begin)
                        isPeriodCross = true;
                }
            }
            if (isPeriodCross)
            {
                MainForm.ShowWarningMessage("Указанный период пересекается с уже имеющимся периодом!",
                    "Ошибка добавления периода");
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
                        if (Begin > begin || End < end)
                            isPeriodCross = true;
                    }
                }
                if (isPeriodCross)
                {
                    MainForm.ShowWarningMessage(
                        "Среди записей специального стажа есть периоды выходящие за указанные границы!",
                        "Ошибка добавления периода");
                    isAllRight = false;
                }
            }

            if (isAllRight)
                DialogResult = DialogResult.OK;
        }

        #endregion
    }
}