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
    public partial class AddEditSpecialPeriodForm : Form
    {
        int _repYear;
        BindingSource _classpercentView200BS;
        BindingSource _classpercentView300BS;
        BindingSource _classpercentView400BS;

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


        public AddEditSpecialPeriodForm()
        {
            InitializeComponent();
            _repYear = DateTime.Now.Year;
        }

        public AddEditSpecialPeriodForm(int repYear)
            : this()
        {
            this._repYear = repYear;
        }

        private void AddEditSpecialPeriodForm_Load(object sender, EventArgs e)
        {
            Code = 0;
            CodeName = "";
            beginDateTimePicker.MinDate = DateTime.Parse(_repYear + "-01-01");
            beginDateTimePicker.MaxDate = DateTime.Parse(_repYear + "-12-31");
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
            endDateTimePicker.MaxDate = DateTime.Parse(_repYear + "-12-31");

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
            radioButton1.Checked = true;
            TypePeriod = 1;
            Begin = beginDateTimePicker.Value;
            End = endDateTimePicker.Value;
            Month = (int)monthsNumUpDown.Value;
            Day = (int)daysNumUpDown.Value;
            Hour = (int)hoursNumUpDown.Value;
            Minute = (int)minutesNumUpDown.Value;
            Profession = professionRichTextBox.Text;
        }

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
                    professionRichTextBox.Text = "";
                    hoursNumUpDown.Enabled = true;
                    minutesNumUpDown.Enabled = true;
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
        }
    }
}
