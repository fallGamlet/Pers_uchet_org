using System;
using System.Data;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class AddEditAdditionalPeriodForm : Form
    {
        #region Поля

        private int _repYear;
        private BindingSource _classpercentView500BS;
        private BindingSource _dopPeriodBS;
        private bool isNew;

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
            _repYear = repYear;
            _dopPeriodBS = periodBS;
            Begin = new DateTime(_repYear, DateTime.Now.Date.Month, DateTime.Now.Date.Day);
            End = new DateTime(_repYear, DateTime.Now.Date.Month, DateTime.Now.Date.Day);
            Code = 0;
            CodeName = "";
            isNew = true;
        }

        public AddEditAdditionalPeriodForm(int repYear, long classificatorId, BindingSource periodBS, DateTime beginDate,
            DateTime endDate)
            : this(repYear, periodBS)
        {
            Code = classificatorId;
            Begin = beginDate;
            End = endDate;
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
                _classpercentView500BS.Filter =
                    ClasspercentView.GetBindingSourceFilterFor500(DateTime.Parse(_repYear + "-01-01"));
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

        private void _classpercentView500BS_CurrentChanged(object sender, EventArgs e)
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
            foreach (DataRowView row in _dopPeriodBS)
            {
                if (row != (_dopPeriodBS.Current as DataRowView) || isNew)
                {
                    DateTime begin = (DateTime)row[DopPeriod.beginDate];
                    DateTime end = (DateTime)row[DopPeriod.endDate];
                    if (Begin <= end && Begin >= begin)
                        isAllRight = false;
                    if (End <= end && End >= begin)
                        isAllRight = false;
                    if (End >= end && Begin <= begin)
                        isAllRight = false;
                }
            }
            if (isAllRight)
                DialogResult = DialogResult.OK;
            else
            {
                MainForm.ShowWarningMessage("Указанный период пересекается с уже имеющимся периодом!",
                    "Ошибка добавления периода");
            }
        }

        #endregion
    }
}