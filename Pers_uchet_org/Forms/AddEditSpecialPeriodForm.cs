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
        int RepYear;
        BindingSource _classpercentView200BS;
        BindingSource _classpercentView300BS;
        BindingSource _classpercentView400BS;

        public long Code;
        public DateTime Begin;
        public DateTime End;

        public AddEditSpecialPeriodForm()
        {
            InitializeComponent();
        }

        public AddEditSpecialPeriodForm(int RepYear)
            : this()
        {
            this.RepYear = RepYear;
        }

        private void AddEditSpecialPeriodForm_Load(object sender, EventArgs e)
        {
            Code = 0;
            beginDateTimePicker.MinDate = DateTime.Parse(RepYear + "-01-01");
            beginDateTimePicker.MaxDate = DateTime.Parse(RepYear + "-12-31");
            endDateTimePicker.MinDate = beginDateTimePicker.Value;
            endDateTimePicker.MaxDate = DateTime.Parse(RepYear + "-12-31");

            if (MainForm.ClasspercentViewTable != null)
            {
                _classpercentView200BS = new BindingSource();
                _classpercentView300BS = new BindingSource();
                _classpercentView400BS = new BindingSource();
                _classpercentView200BS.DataSource = new DataView(MainForm.ClasspercentViewTable);
                _classpercentView300BS.DataSource = new DataView(MainForm.ClasspercentViewTable);
                _classpercentView400BS.DataSource = new DataView(MainForm.ClasspercentViewTable);
                _classpercentView200BS.Filter = ClasspercentView.GetBindingSourceFilterFor200(DateTime.Parse(RepYear + "-01-01"));
                _classpercentView200BS.Sort = ClasspercentView.code;
                _classpercentView200BS.CurrentChanged += new EventHandler(_classpercentView200BS_CurrentChanged);
                _classpercentView300BS.Filter = ClasspercentView.GetBindingSourceFilterFor300(DateTime.Parse(RepYear + "-01-01"));
                _classpercentView300BS.Sort = ClasspercentView.code;
                _classpercentView300BS.CurrentChanged += new EventHandler(_classpercentView300BS_CurrentChanged);
                _classpercentView400BS.Filter = ClasspercentView.GetBindingSourceFilterFor400(DateTime.Parse(RepYear + "-01-01"));
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
            Begin = beginDateTimePicker.Value;
            End = endDateTimePicker.Value;
        }

        void _classpercentView400BS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }

            Code = (long)row[ClasspercentView.id];
        }

        void _classpercentView300BS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }

            Code = (long)row[ClasspercentView.id];
        }

        void _classpercentView200BS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }

            Code = (long)row[ClasspercentView.id];
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
                    Code = (partConditionComboBox.SelectedValue != null) ? (long)partConditionComboBox.SelectedValue : 0;
                    break;
                case "radioButton2":
                    partConditionComboBox.Enabled = false;
                    stajBaseComboBox.Enabled = true;
                    servYearBaseComboBox.Enabled = false;
                    professionRichTextBox.Text = "";
                    hoursNumUpDown.Enabled = true;
                    minutesNumUpDown.Enabled = true;
                    professionRichTextBox.Enabled = false;
                    Code = (stajBaseComboBox.SelectedValue != null) ? (long)stajBaseComboBox.SelectedValue : 0;
                    break;
                case "radioButton3":
                    partConditionComboBox.Enabled = false;
                    stajBaseComboBox.Enabled = false;
                    servYearBaseComboBox.Enabled = true;
                    hoursNumUpDown.Enabled = true;
                    minutesNumUpDown.Enabled = true;
                    professionRichTextBox.Enabled = true;
                    Code = (servYearBaseComboBox.SelectedValue != null) ? (long)servYearBaseComboBox.SelectedValue : 0;
                    break;
                default:
                    partConditionComboBox.Enabled = false;
                    stajBaseComboBox.Enabled = false;
                    servYearBaseComboBox.Enabled = false;
                    break;
            }
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
