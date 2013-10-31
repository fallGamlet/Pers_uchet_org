﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pers_uchet_org
{
    public partial class AddEditSpecialPeriodForm : Form
    {
        #region Поля
        int _repYear;
        bool _isNew;
        BindingSource _classpercentView200BS;
        BindingSource _classpercentView300BS;
        BindingSource _classpercentView400BS;
        BindingSource _generalPeriodBS;

        public int TypePeriod; //Тип периода, зависит от выбранного radioButton
        public long Code;
        public string CodeName;
        public DateTime Begin;
        public DateTime End;
        public int Month;
        public int Day;
        public int Hour;
        public int Minute;
        public string Profession;
        #endregion

        #region Конструктор и инициализатор
        private AddEditSpecialPeriodForm()
        {
            InitializeComponent();
            _repYear = DateTime.Now.Year;
            TypePeriod = 1;
            Code = 0;
            CodeName = "";
            Begin = DateTime.Now.Date; ;
            End = DateTime.Now.Date; ;
            Month = 0;
            Day = 0;
            Hour = 0;
            Minute = 0;
            Profession = "";
        }

        public AddEditSpecialPeriodForm(int repYear, BindingSource generalPeriodBS)
            : this()
        {
            this._repYear = repYear;
            this._generalPeriodBS = generalPeriodBS;
            this._isNew = true;
        }

        public AddEditSpecialPeriodForm(int repYear, BindingSource generalPeriodBS, int typePeriod, long classificatorId, DateTime beginDate, DateTime endDate, int month, int day, int hour, int minute, string profession)
            : this(repYear, generalPeriodBS)
        {
            this.TypePeriod = typePeriod;
            this.Code = classificatorId;
            this.Begin = beginDate;
            this.End = endDate;
            this.Month = month;
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;
            this.Profession = profession;
            this._isNew = false;
        }

        private void AddEditSpecialPeriodForm_Load(object sender, EventArgs e)
        {
            beginDateTimePicker.MinDate = DateTime.Parse(_repYear + "-01-01");
            beginDateTimePicker.MaxDate = DateTime.Parse(_repYear + "-12-31");
            beginDateTimePicker.Value = Begin;
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
            endDateTimePicker.MaxDate = DateTime.Parse(_repYear + "-12-31");
            endDateTimePicker.Value = End;

            if (MainForm.ClasspercentViewTable != null)
            {
                _classpercentView200BS = new BindingSource();
                _classpercentView300BS = new BindingSource();
                _classpercentView400BS = new BindingSource();
                _classpercentView200BS.DataSource = new DataView(MainForm.ClasspercentViewTable);
                _classpercentView300BS.DataSource = new DataView(MainForm.ClasspercentViewTable);
                _classpercentView400BS.DataSource = new DataView(MainForm.ClasspercentViewTable);
                _classpercentView200BS.Filter = ClasspercentView.GetBindingSourceFilterFor200(DateTime.Parse(_repYear + "-01-01"));
                _classpercentView200BS.Sort = ClasspercentView.code;
                _classpercentView200BS.CurrentChanged += new EventHandler(_classpercentView200BS_CurrentChanged);
                _classpercentView300BS.Filter = ClasspercentView.GetBindingSourceFilterFor300(DateTime.Parse(_repYear + "-01-01"));
                _classpercentView300BS.Sort = ClasspercentView.code;
                _classpercentView300BS.CurrentChanged += new EventHandler(_classpercentView300BS_CurrentChanged);
                _classpercentView400BS.Filter = ClasspercentView.GetBindingSourceFilterFor400(DateTime.Parse(_repYear + "-01-01"));
                _classpercentView400BS.Sort = ClasspercentView.code;
                _classpercentView400BS.CurrentChanged += new EventHandler(_classpercentView400BS_CurrentChanged);

                partConditionComboBox.DataSource = _classpercentView200BS;
                partConditionComboBox.ValueMember = ClasspercentView.id;
                partConditionComboBox.DisplayMember = ClasspercentView.code;
                stajBaseComboBox.DataSource = _classpercentView300BS;
                stajBaseComboBox.ValueMember = ClasspercentView.id;
                stajBaseComboBox.DisplayMember = ClasspercentView.code;
                servYearBaseComboBox.DataSource = _classpercentView400BS;
                servYearBaseComboBox.ValueMember = ClasspercentView.id;
                servYearBaseComboBox.DisplayMember = ClasspercentView.code;
            }

            long tmpCode = Code; //при смене radioButton меняется и код, поэтому текущее значение сохраняем
            switch (TypePeriod)
            {
                case 1:
                    radioButton1.Checked = true;
                    if (tmpCode > 0)
                        partConditionComboBox.SelectedValue = tmpCode;
                    break;
                case 2:
                    radioButton2.Checked = true;
                    if (tmpCode > 0)
                        stajBaseComboBox.SelectedValue = tmpCode;
                    break;
                case 3:
                    radioButton3.Checked = true;
                    if (tmpCode > 0)
                        servYearBaseComboBox.SelectedValue = tmpCode;
                    break;
                default:
                    radioButton1.Checked = true;
                    break;
            }

            monthsNumUpDown.Value = Month;
            daysNumUpDown.Value = Day;
            hoursNumUpDown.Value = Hour;
            minutesNumUpDown.Value = Minute;
            professionRichTextBox.Text = Profession;
        }
        #endregion

        #region Методы - обработчики событий
        void _classpercentView400BS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }

            Code = (long)row[ClasspercentView.id];
            CodeName = row[ClasspercentView.code].ToString();
        }

        void _classpercentView300BS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }

            Code = (long)row[ClasspercentView.id];
            CodeName = row[ClasspercentView.code].ToString();
        }

        void _classpercentView200BS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }

            Code = (long)row[ClasspercentView.id];
            CodeName = row[ClasspercentView.code].ToString();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            switch (radio.Name)
            {
                case "radioButton1":
                    partConditionComboBox.Enabled = true;
                    stajBaseComboBox.Enabled = false;
                    servYearBaseComboBox.Enabled = false;
                    hoursNumUpDown.Value = 0;
                    hoursNumUpDown.Enabled = false;
                    minutesNumUpDown.Value = 0;
                    minutesNumUpDown.Enabled = false;
                    professionRichTextBox.Enabled = true;
                    TypePeriod = 1;
                    if (partConditionComboBox.SelectedValue != null)
                    {
                        Code = (long)partConditionComboBox.SelectedValue;
                        CodeName = partConditionComboBox.Text;
                        saveButton.Enabled = true;
                    }
                    else
                    {
                        Code = 0;
                        CodeName = "";
                        saveButton.Enabled = false;
                    }
                    break;
                case "radioButton2":
                    partConditionComboBox.Enabled = false;
                    stajBaseComboBox.Enabled = true;
                    servYearBaseComboBox.Enabled = false;
                    hoursNumUpDown.Enabled = true;
                    minutesNumUpDown.Enabled = true;
                    professionRichTextBox.Text = "";
                    professionRichTextBox.Enabled = false;
                    TypePeriod = 2;
                    if (stajBaseComboBox.SelectedValue != null)
                    {
                        Code = (long)stajBaseComboBox.SelectedValue;
                        CodeName = stajBaseComboBox.Text;
                        saveButton.Enabled = true;
                    }
                    else
                    {
                        Code = 0;
                        CodeName = "";
                        saveButton.Enabled = false;
                    }
                    break;
                case "radioButton3":
                    partConditionComboBox.Enabled = false;
                    stajBaseComboBox.Enabled = false;
                    servYearBaseComboBox.Enabled = true;
                    hoursNumUpDown.Enabled = true;
                    minutesNumUpDown.Enabled = true;
                    professionRichTextBox.Enabled = true;
                    TypePeriod = 3;
                    if (servYearBaseComboBox.SelectedValue != null)
                    {
                        Code = (long)servYearBaseComboBox.SelectedValue;
                        CodeName = servYearBaseComboBox.Text;
                        saveButton.Enabled = true;
                    }
                    else
                    {
                        Code = 0;
                        CodeName = "";
                        saveButton.Enabled = false;
                    }
                    break;
                default:
                    partConditionComboBox.Enabled = false;
                    stajBaseComboBox.Enabled = false;
                    servYearBaseComboBox.Enabled = false;
                    TypePeriod = 0;
                    break;
            }
        }

        private void beginDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Begin = beginDateTimePicker.Value;
            End = endDateTimePicker.Value;
            Month = (int)monthsNumUpDown.Value;
            Day = (int)daysNumUpDown.Value;
            Hour = (int)hoursNumUpDown.Value;
            Minute = (int)minutesNumUpDown.Value;
            Profession = professionRichTextBox.Text;

            bool isAllRight = true;

            if (Month == 0 && Day == 0 && Hour == 0 && Minute == 0)
            {
                MainForm.ShowWarningMessage("Необходимо указать количество месяцев/дней (или часов/минут)!", "Ошибка добавления периода");
                isAllRight = false;
            }
            if (String.IsNullOrEmpty(Profession.Trim()) && TypePeriod != 2)
            {
                MainForm.ShowWarningMessage("Необходимо заполнить поле \"Должность\"", "Ошибка добавления периода");
                isAllRight = false;
            }

            DateTime begin;
            DateTime end;
            bool isPeriodCross = true;
            foreach (DataRowView row in _generalPeriodBS)
            {
                begin = (DateTime)row[GeneralPeriod.beginDate];
                end = (DateTime)row[GeneralPeriod.endDate];
                if (this.Begin >= begin && this.End <= end)
                {
                    isPeriodCross = false;
                }
            }

            if (_generalPeriodBS.Count < 1)
            {
                MainForm.ShowWarningMessage("Указанный период не попадает\nне в один из периодов основного стажа!\nНет записей о периодах основного стажа!", "Ошибка добавления периода");
                isAllRight = false;
                isPeriodCross = false;
            }

            if (isPeriodCross)
            {
                MainForm.ShowWarningMessage("Указанный период не попадает не в один из периодов основного стажа!", "Ошибка добавления периода");
                isAllRight = false;
            }
            

            if (isAllRight)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        #endregion
    }
}
