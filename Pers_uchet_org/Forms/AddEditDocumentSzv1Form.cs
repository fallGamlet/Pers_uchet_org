using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Pers_uchet_org
{
    public partial class AddEditDocumentSzv1Form : Form
    {
        #region Поля
        private Org _org;
        private int RepYear;
        private long personId;
        //тип документа
        private int flagDoc;
        //новый документ или производится редактирование
        private bool isNew;
        private string _connection;
        // процент по выбранной категории
        private double currentPercent = 0;
        // объекты таблиц для хранения записей
        DataTable _citizenTable;            // таблица Country
        DataTable _classpercentViewTable;
        DataTable _salaryInfoTable;         //таблица зарплаты человека
        // объекты BindingSource для синхронизации таблиц и отображателей
        BindingSource _citizen1BS;
        BindingSource _citizen2BS;
        BindingSource _classpercentViewBS;
        BindingSource _salaryInfoTableBS;
        #endregion

        public AddEditDocumentSzv1Form()
        {
            InitializeComponent();
        }

        public AddEditDocumentSzv1Form(Org _org, int RepYear, long personId, int flagDoc, bool isNew, string _connection)
            : this()
        {
            this._org = _org;
            this.RepYear = RepYear;
            this.personId = personId;
            this.flagDoc = flagDoc;
            this.isNew = isNew;
            this._connection = _connection;
        }

        private void AddEditDocumentSzv1Form_Load(object sender, EventArgs e)
        {
            orgNameTextBox.Text = _org.nameVal;
            regNumTextBox.Text = _org.regnumVal;
            yearLabel.Text = RepYear.ToString();
            StringBuilder builder = new StringBuilder();

            if (isNew)
            {
                builder.Append("Добавление ");
            }
            else
            {
                builder.Append("Редактирование ");
            }
            builder.Append("документа \"СЗВ-1\"");
            switch (flagDoc)
            {
                case 21:
                    builder.Append(" - исходная форма");
                    break;
                case 22:
                    builder.Append(" - корректирующая форма");
                    break;
                case 23:
                    builder.Append(" - отменяющая форма");
                    break;
                case 24:
                    builder.Append(" - назначение пенсии");
                    break;
                default:
                    break;
            }
            this.Text = builder.ToString();

            //заполнение источников гражданства
            if (MainForm.CountryTable == null)
                MainForm.CountryTable = Country.CreateTable();
            _citizenTable = MainForm.CountryTable;

            _citizen1BS = new BindingSource();
            _citizen1BS.DataSource = _citizenTable;
            _citizen2BS = new BindingSource();
            _citizen2BS.DataSource = _citizenTable;

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(Country.GetSelectText(), _connection);
            if (_citizenTable.Rows.Count <= 0)
                adapter.Fill(_citizenTable);

            citizen1Box.DataSource = _citizen1BS;
            citizen1Box.DisplayMember = Country.name;
            citizen1Box.ValueMember = Country.id;
            citizen2Box.DataSource = _citizen2BS;
            citizen2Box.DisplayMember = Country.name;
            citizen2Box.ValueMember = Country.id;
            //заполнение источников для кодов категории застрахованного лица
            _classpercentViewTable = ClasspercentView.CreateTable();
            _classpercentViewBS = new BindingSource();
            _classpercentViewBS.DataSource = _classpercentViewTable;
            _classpercentViewBS.CurrentChanged += new EventHandler(_classpercentViewBS_CurrentChanged);

            adapter = new SQLiteDataAdapter(ClasspercentView.GetSelectText(), _connection);
            adapter.Fill(_classpercentViewTable);

            foreach (DataRow row in _classpercentViewTable.Rows)
            {
                if (row[ClasspercentView.privilegeName].ToString() != "---")
                {
                    row[ClasspercentView.code] = string.Format("{0} / {1}", row[ClasspercentView.code].ToString().Trim(), row[ClasspercentView.privilegeName]);
                }
                else
                {
                    row[ClasspercentView.code] = string.Format("{0}", row[ClasspercentView.code].ToString().Trim());
                }
            }
            _classpercentViewTable.AcceptChanges();

            _classpercentViewBS.Filter = "date_begin<='" + RepYear + "-01-01' AND (date_end >= '" + RepYear + "-01-01' OR date_end IS NULL) AND classificator_id >=100 AND classificator_id < 200";
            _classpercentViewBS.Sort = ClasspercentView.code;
            codeComboBox.DataSource = _classpercentViewBS;
            codeComboBox.ValueMember = ClasspercentView.id;
            codeComboBox.DisplayMember = ClasspercentView.code;
            //формирование источников для таблицы зарплат




            //int pos = _citizen1BS.Find(Country.name, "ПМР");
            //if (pos != -1)
            //    _citizen1BS.Position = pos;
            //else
            //    _citizen1BS.MoveFirst();

            //_citizen2BS.MoveFirst();
        }

        void _classpercentViewBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }

            currentPercent =(double) row[ClasspercentView.value];
            percentLabel.Text = (currentPercent * 100).ToString() + " %";
        }

        private void addGeneralPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditGeneralPeriodForm generalPeriodForm = new AddEditGeneralPeriodForm();
            if (generalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void editGeneralPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditGeneralPeriodForm generalPeriodForm = new AddEditGeneralPeriodForm();
            if (generalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void delGeneralPeriodButton_Click(object sender, EventArgs e)
        {

        }

        private void addAdditionalPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditAdditionalPeriodForm additionalPeriodForm = new AddEditAdditionalPeriodForm();
            if (additionalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void editAdditionalPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditAdditionalPeriodForm additionalPeriodForm = new AddEditAdditionalPeriodForm();
            if (additionalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void delAdditionalPeriodButton_Click(object sender, EventArgs e)
        {

        }

        private void addSpecialPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditSpecialPeriodForm specialPeriodForm = new AddEditSpecialPeriodForm();
            if (specialPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void editSpecialPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditSpecialPeriodForm specialPeriodForm = new AddEditSpecialPeriodForm();
            if (specialPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void delSpecialPeriodButton_Click(object sender, EventArgs e)
        {

        }

    }
}
