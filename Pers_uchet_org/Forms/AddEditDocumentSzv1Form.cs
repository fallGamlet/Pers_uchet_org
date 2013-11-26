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
        Org _org;
        Operator _operator;
        long _currentListId;
        int _repYear;
        long _personId;
        string _connectionStr;
        //id документа, если производится редактирование
        long _currentDocId;
        //тип документа
        long _flagDocType;
        //процент по выбранной категории
        long _currentClassPercentId;
        double _currentPercent;
        double _currentObligatory;
        int _isAgriculture;

        SQLiteConnection _connection;
        SQLiteCommand _command;
        SQLiteDataReader _reader;
        //объекты таблиц для хранения записей
        DataTable _salaryInfoTable;
        DataTable _salaryInfoTableTranspose;
        DataTable _generalPeriodTable;
        DataTable _dopPeriodTable;
        DataTable _specPeriodTable;
        //объекты BindingSource для синхронизации таблиц и отображателей
        BindingSource _citizen1BS;
        BindingSource _citizen2BS;
        BindingSource _classpercentView100BS;
        BindingSource _salaryInfoBS;
        BindingSource _generalPeriodBS;
        BindingSource _dopPeriodBS;
        BindingSource _specPeriodBS;
        // адаптер для чтения данных из БД
        SQLiteDataAdapter _adapter;
        #endregion

        private AddEditDocumentSzv1Form()
        {
            InitializeComponent();
            _currentDocId = -1;
            _currentPercent = 0;
            _currentObligatory = 0;
            _isAgriculture = 0;
            _personId = 0;
            _currentClassPercentId = 0;
        }

        public AddEditDocumentSzv1Form(Org org, Operator _operator, long currentListId, int repYear, long personId, long flagDocType, string connectionStr)
            : this()
        {
            this._org = org;
            this._operator = _operator;
            this._currentListId = currentListId;
            this._repYear = repYear;
            this._personId = personId;
            this._flagDocType = flagDocType;
            this._connectionStr = connectionStr;
        }

        public AddEditDocumentSzv1Form(Org org, Operator _operator, long currentListId, int repYear, long personId, long flagDocType, string connectionStr, long idDoc)
            : this()
        {
            this._org = org;
            this._operator = _operator;
            this._currentListId = currentListId;
            this._repYear = repYear;
            this._personId = personId;
            this._flagDocType = flagDocType;
            this._connectionStr = connectionStr;
            this._currentDocId = idDoc;
        }

        private void AddEditDocumentSzv1Form_Load(object sender, EventArgs e)
        {
            orgNameTextBox.Text = _org.nameVal;
            regNumTextBox.Text = _org.regnumVal;
            yearLabel.Text = _repYear.ToString();
            saveButton.Enabled = false;
            this.Text = GenerateFormText(_currentDocId, _flagDocType);

            _connection = new SQLiteConnection(_connectionStr);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            _command = _connection.CreateCommand();

            //заполнение источников гражданства
            if (MainForm.CountryTable == null)
                MainForm.CountryTable = Country.CreateTable();
            if (MainForm.CountryTable.Rows.Count <= 0)
            {
                _command.CommandText = Country.GetSelectText();
                _adapter = new SQLiteDataAdapter(_command);
                _adapter.Fill(MainForm.CountryTable);
            }

            _citizen1BS = new BindingSource();
            _citizen1BS.DataSource = new DataView(MainForm.CountryTable);
            _citizen1BS.Filter = string.Format("{0}<>0", Country.id);
            _citizen2BS = new BindingSource();
            _citizen2BS.DataSource = new DataView(MainForm.CountryTable);
            citizen1Box.DataSource = _citizen1BS;
            citizen1Box.DisplayMember = Country.name;
            citizen1Box.ValueMember = Country.id;
            citizen2Box.DataSource = _citizen2BS;
            citizen2Box.DisplayMember = Country.name;
            citizen2Box.ValueMember = Country.id;

            //заполнение источников для кодов категории застрахованного лица
            FillClassPercentViewTable();
            _classpercentView100BS = new BindingSource();
            _classpercentView100BS.CurrentChanged += new EventHandler(_classpercentViewBS_CurrentChanged);
            _classpercentView100BS.DataSource = MainForm.ClasspercentViewTable;
            _classpercentView100BS.Filter = ClasspercentView.GetBindingSourceFilterFor100(DateTime.Parse(_repYear + "-01-01"));
            _classpercentView100BS.Sort = ClasspercentView.code;
            codeComboBox.DataSource = _classpercentView100BS;
            codeComboBox.ValueMember = ClasspercentView.id;
            codeComboBox.DisplayMember = ClasspercentView.code;

            //формирование источников для таблицы зарплат
            FillSalaryInfoTable();
            _salaryInfoBS = new BindingSource();
            _salaryInfoBS.DataSource = _salaryInfoTableTranspose;
            dataViewProfit.AutoGenerateColumns = false;
            dataViewProfit.CellParsing += new DataGridViewCellParsingEventHandler(dataViewProfit_CellParsing);
            dataViewProfit.DataSource = _salaryInfoBS;

            //Выбор ФИО, страхового номера, гражданства из базы
            _command.CommandText = PersonView.GetSelectText(_org.idVal, _personId);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            _reader = _command.ExecuteReader();
            if (_reader.Read())
            {
                textBoxAnketaName.Text = _reader[PersonView.fio].ToString();
                textBoxInsNum.Text = _reader[PersonView.socNumber].ToString();
                long citizen = (long)_reader[PersonView.citizen1ID];
                _citizen1BS.Position = _citizen1BS.Find(Country.id, citizen);
                citizen = (long)_reader[PersonView.citizen2ID];
                _citizen2BS.Position = _citizen2BS.Find(Country.id, citizen);
            }
            _reader.Close();

            //Выбор гражданства, места работы и кода котегории документа из базы
            if (_currentDocId > 0)
            {
                _command.CommandText = IndDocs.GetSelectText(_currentDocId);
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                _reader = _command.ExecuteReader();
                if (_reader.Read())
                {
                    long citizen = (long)_reader[IndDocs.citizen1Id];
                    _citizen1BS.Position = _citizen1BS.Find(Country.id, citizen);
                    citizen = (long)_reader[IndDocs.citizen2Id];
                    _citizen2BS.Position = _citizen2BS.Find(Country.id, citizen);
                    long code = (long)_reader[IndDocs.classpercentId];
                    _classpercentView100BS.Position = _classpercentView100BS.Find(ClasspercentView.id, code);
                    if ((long)_reader[IndDocs.isGeneral] == (long)IndDocs.Job.Second)
                    {
                        additionalRadioButton.Checked = true;
                    }
                }
                _reader.Close();
            }

            //Заполнение таблиц с периодами
            FillPeriodTables();
            _generalPeriodBS = new BindingSource();
            _generalPeriodBS.DataSource = _generalPeriodTable;
            generalPeriodDataGridView.AutoGenerateColumns = false;
            generalPeriodDataGridView.DataSource = _generalPeriodBS;
            generalPeriodDataGridView.Columns["beginGeneralPeriodColumn"].DataPropertyName = GeneralPeriod.beginDate;
            generalPeriodDataGridView.Columns["endGeneralPeriodColumn"].DataPropertyName = GeneralPeriod.endDate;

            _dopPeriodBS = new BindingSource();
            _dopPeriodBS.DataSource = _dopPeriodTable;
            additionalPeriodDataGridView.AutoGenerateColumns = false;
            additionalPeriodDataGridView.DataSource = _dopPeriodBS;
            additionalPeriodDataGridView.Columns["codeAdditionalPeriodColumn"].DataPropertyName = DopPeriodView.code;
            additionalPeriodDataGridView.Columns["beginAdditionalPeriodColumn"].DataPropertyName = DopPeriodView.beginDate;
            additionalPeriodDataGridView.Columns["endAdditionalPeriodColumn"].DataPropertyName = DopPeriodView.endDate;

            _specPeriodBS = new BindingSource();
            _specPeriodBS.DataSource = _specPeriodTable;
            specialPeriodDataGridView.AutoGenerateColumns = false;
            specialPeriodDataGridView.DataSource = _specPeriodBS;
            specialPeriodDataGridView.Columns["beginSpecialPeriodColumn"].DataPropertyName = SpecialPeriodView.beginDate;
            specialPeriodDataGridView.Columns["endSpecialPeriodColumn"].DataPropertyName = SpecialPeriodView.endDate;
            specialPeriodDataGridView.Columns["partConditionColumn"].DataPropertyName = SpecialPeriodView.partCode;
            specialPeriodDataGridView.Columns["stajBaseColumn"].DataPropertyName = SpecialPeriodView.stajCode;
            specialPeriodDataGridView.Columns["servYearColumn"].DataPropertyName = SpecialPeriodView.servCode;
            specialPeriodDataGridView.Columns["monthsColumn"].DataPropertyName = SpecialPeriodView.month;
            specialPeriodDataGridView.Columns["daysColumn"].DataPropertyName = SpecialPeriodView.day;
            specialPeriodDataGridView.Columns["hoursColumn"].DataPropertyName = SpecialPeriodView.hour;
            specialPeriodDataGridView.Columns["minutesColumn"].DataPropertyName = SpecialPeriodView.minute;
            specialPeriodDataGridView.Columns["professionColumn"].DataPropertyName = SpecialPeriodView.profession;
            _connection.Close();

            saveButton.Enabled = true;
            if (_flagDocType == (long)DocTypes.CancelingFormId)
            {
                tabControlMain.TabPages.Remove(tabPage2);
                tabControlMain.TabPages.Remove(tabPage3);
            }
        }

        private void FillPeriodTables()
        {
            _generalPeriodTable = GeneralPeriod.CreatetTable();
            _dopPeriodTable = DopPeriodView.CreateTable();
            _specPeriodTable = SpecialPeriodView.CreatetTable();

            if (_currentDocId > 0)
            {
                _adapter = GeneralPeriod.CreateAdapter(_connection, null);
                _adapter.SelectCommand.Parameters[GeneralPeriod.pDocId].Value = _currentDocId;
                _adapter.Fill(_generalPeriodTable);

                _adapter = DopPeriod.CreateAdapter(_connection, null);
                _adapter.SelectCommand.Parameters[DopPeriod.pDocId].Value = _currentDocId;
                _adapter.Fill(_dopPeriodTable);

                _command.CommandText = SpecialPeriodView.GetSelectText(_currentDocId);
                _adapter = new SQLiteDataAdapter(_command);
                _adapter.Fill(_specPeriodTable);
            }
        }

        private void FillSalaryInfoTable()
        {
            _salaryInfoTableTranspose = SalaryInfoTranspose.CreateTableWithRows();
            if (_currentDocId > 0)
            {
                _salaryInfoTable = SalaryInfo.CreateTable();
                _command.CommandText = SalaryInfo.GetSelectText(_currentDocId);
                _adapter = new SQLiteDataAdapter(_command);
                _adapter.Fill(_salaryInfoTable);
                SalaryInfoTranspose.ConvertFromSalaryInfo(_salaryInfoTableTranspose, _salaryInfoTable);

                sum1Box.Text = ((double)_salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column1)][SalaryInfo.sum]).ToString("N2");
                sum2Box.Text = ((double)_salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column2)][SalaryInfo.sum]).ToString("N2");
                sum3Box.Text = ((double)_salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column3)][SalaryInfo.sum]).ToString("N2");
                sum4Box.Text = ((double)_salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column4)][SalaryInfo.sum]).ToString("N2");
                sum5Box.Text = ((double)_salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column5)][SalaryInfo.sum]).ToString("N2");
                sum10Box.Text = ((double)_salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column10)][SalaryInfo.sum]).ToString("N2");
            }
        }

        private void FillClassPercentViewTable()
        {
            if (MainForm.ClasspercentViewTable == null)
                MainForm.ClasspercentViewTable = ClasspercentView.CreateTable();
            if (MainForm.ClasspercentViewTable.Rows.Count < 1)
            {
                _command.CommandText = ClasspercentView.GetSelectText();
                _adapter = new SQLiteDataAdapter(_command);
                _adapter.Fill(MainForm.ClasspercentViewTable);

                //формирование строк в виде "ОРГАНИЗАЦИЯ / ЛЬГОТА"
                foreach (DataRow row in MainForm.ClasspercentViewTable.Rows)
                {
                    if (row[ClasspercentView.privilegeName].ToString() != "---")
                    {
                        row[ClasspercentView.code] = string.Format("{0} / {1}", row[ClasspercentView.code].ToString().Trim(), row[ClasspercentView.privilegeName].ToString().Trim());
                    }
                    else
                    {
                        row[ClasspercentView.code] = string.Format("{0}", row[ClasspercentView.code].ToString().Trim());
                    }
                }
                MainForm.ClasspercentViewTable.AcceptChanges();
            }
        }

        private void dataViewProfit_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            DataGridView view = sender as DataGridView;

            if (String.IsNullOrEmpty(e.Value.ToString().Trim()) || e.Value.ToString().Trim() == System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
            {
                if (view.Columns[e.ColumnIndex].DataPropertyName == SalaryGroups.Column10.ToString())
                    e.Value = 0;
                else
                    e.Value = 0.0;
                e.ParsingApplied = true;
                return;
            }

            double result = 0;
            if (!Double.TryParse(e.Value.ToString(), out result))
            {
                if (!Double.TryParse(e.Value.ToString().Replace('.', ','), out result))
                {
                    e.ParsingApplied = false;
                    return;
                }
            }

            if (view.Columns[e.ColumnIndex].DataPropertyName == SalaryGroups.Column10.ToString())
            {
                e.Value = Convert.ToInt32(result);
                e.ParsingApplied = true;
                return;
            }

            e.Value = Math.Round(result, 2);
            e.ParsingApplied = true;
        }

        private void _classpercentViewBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
                return;

            _currentPercent = (double)row[ClasspercentView.value];
            _currentClassPercentId = (long)row[ClasspercentView.id];
            percentLabel.Text = (_currentPercent * 100).ToString() + " %";

            if (int.Parse(row[ClasspercentView.obligatoryIsEnabled].ToString()) != 0)
                _currentObligatory = ObligatoryPercent.GetValue(_repYear, _connectionStr);
            else
                _currentObligatory = 0;
            _isAgriculture = int.Parse(row[ClasspercentView.isAgriculture].ToString());
            if (_isAgriculture == 1 || _repYear > 2012)
            {
                _currentPercent = 0;
                dataViewProfit.Columns["Column3"].ReadOnly = true;
                dataViewProfit.Columns["Column3"].DefaultCellStyle.BackColor = SystemColors.Control;
                dataViewProfit.Columns["Column4"].ReadOnly = true;
                dataViewProfit.Columns["Column4"].DefaultCellStyle.BackColor = SystemColors.Control;
            }
            else
            {
                dataViewProfit.Columns["Column3"].ReadOnly = false;
                dataViewProfit.Columns["Column3"].DefaultCellStyle.BackColor = SystemColors.Window;
                dataViewProfit.Columns["Column4"].ReadOnly = false;
                dataViewProfit.Columns["Column4"].DefaultCellStyle.BackColor = SystemColors.Window;
            }

            if (_isAgriculture == 1)
            {
                sum3Box.ReadOnly = false;
                sum3Box.TabStop = true;
                sum4Box.ReadOnly = false;
                sum4Box.TabStop = true;
            }
            else
            {
                sum3Box.ReadOnly = true;
                sum3Box.TabStop = false;
                sum4Box.ReadOnly = true;
                sum4Box.TabStop = false;
            }

            dataViewProfit_CellValidated(null, null);
        }

        private void addGeneralPeriodButton_Click(object sender, EventArgs e)
        {
            if (_generalPeriodBS == null || _generalPeriodBS.DataSource == null)
            {
                MainForm.ShowErrorMessage("Источник данных для основного периода не задан!", "Ошибка");
                return;
            }

            AddEditGeneralPeriodForm generalPeriodForm = new AddEditGeneralPeriodForm(_repYear, _generalPeriodBS, _specPeriodBS);
            if (generalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataRowView row = _generalPeriodBS.AddNew() as DataRowView;
                row[GeneralPeriod.beginDate] = generalPeriodForm.Begin.Date;
                row[GeneralPeriod.endDate] = generalPeriodForm.End.Date;
                _generalPeriodBS.EndEdit();
            }
        }

        private void editGeneralPeriodButton_Click(object sender, EventArgs e)
        {
            DataRowView row = _generalPeriodBS.Current as DataRowView;
            if (row == null)
                return;
            AddEditGeneralPeriodForm generalPeriodForm = new AddEditGeneralPeriodForm(_repYear, _generalPeriodBS, _specPeriodBS, (DateTime)row[GeneralPeriod.beginDate], (DateTime)row[GeneralPeriod.endDate]);
            if (generalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                row[GeneralPeriod.beginDate] = generalPeriodForm.Begin;
                row[GeneralPeriod.endDate] = generalPeriodForm.End;
                _generalPeriodBS.EndEdit();
            }
        }

        private void delGeneralPeriodButton_Click(object sender, EventArgs e)
        {
            DataRowView row = _generalPeriodBS.Current as DataRowView;
            if (row == null)
                return;

            bool isPeriodCross = false;
            DateTime beginCurrent = (DateTime)row[GeneralPeriod.beginDate];
            DateTime endCurrent = (DateTime)row[GeneralPeriod.endDate];
            DateTime begin;
            DateTime end;
            foreach (DataRowView item in _specPeriodBS)
            {
                begin = (DateTime)item[SpecialPeriod.beginDate];
                end = (DateTime)item[SpecialPeriod.endDate];

                if (begin >= beginCurrent && end <= endCurrent)
                {
                    isPeriodCross = true;
                }
            }
            if (isPeriodCross)
            {
                MainForm.ShowWarningMessage("Среди записей специального стажа есть периоды принадлежащие текущему основному периоду!\nПри удалении текущего периода будут удалены и записи о специальном стаже!", "Удаление периода основного стажа");
            }

            if (MainForm.ShowQuestionMessage(string.Format("Вы действительно хотите удалить период с {0} по {1}?", beginCurrent.ToShortDateString(), endCurrent.ToShortDateString()), "Удаление периода основного стажа") != System.Windows.Forms.DialogResult.Yes)
                return;

            for (int i = 0; i < _specPeriodBS.Count; i++)
            {

                begin = (DateTime)(_specPeriodBS[i] as DataRowView)[SpecialPeriod.beginDate];
                end = (DateTime)(_specPeriodBS[i] as DataRowView)[SpecialPeriod.endDate];

                if (begin >= beginCurrent && end <= endCurrent)
                {
                    _specPeriodBS.List.RemoveAt(i);
                }
            }
            _generalPeriodBS.RemoveCurrent();

        }

        private void addAdditionalPeriodButton_Click(object sender, EventArgs e)
        {
            if (_dopPeriodBS == null || _dopPeriodBS.DataSource == null)
            {
                MainForm.ShowErrorMessage("Источник данных для дополнительного периода не задан!", "Ошибка");
                return;
            }

            AddEditAdditionalPeriodForm additionalPeriodForm = new AddEditAdditionalPeriodForm(_repYear, _dopPeriodBS);
            if (additionalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataRowView row = _dopPeriodBS.AddNew() as DataRowView;
                row[DopPeriodView.classificatorId] = additionalPeriodForm.Code;
                row[DopPeriodView.code] = additionalPeriodForm.CodeName;
                row[DopPeriodView.beginDate] = additionalPeriodForm.Begin.Date;
                row[DopPeriodView.endDate] = additionalPeriodForm.End.Date;
                _dopPeriodBS.EndEdit();
            }
        }

        private void editAdditionalPeriodButton_Click(object sender, EventArgs e)
        {
            DataRowView row = _dopPeriodBS.Current as DataRowView;
            if (row == null)
                return;

            AddEditAdditionalPeriodForm additionalPeriodForm = new AddEditAdditionalPeriodForm(_repYear, (long)row[DopPeriodView.classificatorId], _dopPeriodBS, (DateTime)row[DopPeriodView.beginDate], (DateTime)row[DopPeriodView.endDate]);
            if (additionalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                row[DopPeriodView.classificatorId] = additionalPeriodForm.Code;
                row[DopPeriodView.code] = additionalPeriodForm.CodeName;
                row[DopPeriodView.beginDate] = additionalPeriodForm.Begin.Date;
                row[DopPeriodView.endDate] = additionalPeriodForm.End.Date;
                _dopPeriodBS.EndEdit();
            }
        }

        private void delAdditionalPeriodButton_Click(object sender, EventArgs e)
        {
            DataRowView row = _dopPeriodBS.Current as DataRowView;
            if (row == null)
                return;

            DateTime begin = (DateTime)row[DopPeriodView.beginDate];
            DateTime end = (DateTime)row[DopPeriodView.endDate];
            if (MainForm.ShowQuestionMessage(string.Format("Вы действительно хотите удалить период с {0} по {1}?", begin.ToShortDateString(), end.ToShortDateString()), "Удаление периода дополнительного стажа") != System.Windows.Forms.DialogResult.Yes)
                return;
            _dopPeriodBS.RemoveCurrent();
        }

        private void addSpecialPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditSpecialPeriodForm specialPeriodForm = new AddEditSpecialPeriodForm(_repYear, _generalPeriodBS);
            if (specialPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                DataRowView row = _specPeriodBS.AddNew() as DataRowView;
                switch (specialPeriodForm.TypePeriod)
                {
                    case 1:
                        row[SpecialPeriodView.partCondition] = specialPeriodForm.Code;
                        row[SpecialPeriodView.partCode] = specialPeriodForm.CodeName;
                        row[SpecialPeriodView.stajBase] = 0;
                        row[SpecialPeriodView.stajCode] = 0;
                        row[SpecialPeriodView.servYearBase] = 0;
                        row[SpecialPeriodView.servCode] = 0;
                        break;
                    case 2:
                        row[SpecialPeriodView.partCondition] = 0;
                        row[SpecialPeriodView.partCode] = 0;
                        row[SpecialPeriodView.stajBase] = specialPeriodForm.Code;
                        row[SpecialPeriodView.stajCode] = specialPeriodForm.CodeName;
                        row[SpecialPeriodView.servYearBase] = 0;
                        row[SpecialPeriodView.servCode] = 0;
                        break;
                    case 3:
                        row[SpecialPeriodView.partCondition] = 0;
                        row[SpecialPeriodView.partCode] = 0;
                        row[SpecialPeriodView.stajBase] = 0;
                        row[SpecialPeriodView.stajCode] = 0;
                        row[SpecialPeriodView.servYearBase] = specialPeriodForm.Code;
                        row[SpecialPeriodView.servCode] = specialPeriodForm.CodeName;
                        break;
                    default:
                        break;
                }
                row[SpecialPeriodView.beginDate] = specialPeriodForm.Begin;
                row[SpecialPeriodView.endDate] = specialPeriodForm.End;
                row[SpecialPeriodView.month] = specialPeriodForm.Month;
                row[SpecialPeriodView.day] = specialPeriodForm.Day;
                row[SpecialPeriodView.hour] = specialPeriodForm.Hour;
                row[SpecialPeriodView.minute] = specialPeriodForm.Minute;
                row[SpecialPeriodView.profession] = specialPeriodForm.Profession;
                _specPeriodBS.EndEdit();
            }
            specialPeriodForm.Dispose();
        }

        private void editSpecialPeriodButton_Click(object sender, EventArgs e)
        {
            DataRowView row = _specPeriodBS.Current as DataRowView;
            if (row == null)
                return;

            int typePeriod = 0;
            long code = 0;
            if (row[SpecialPeriodView.partCondition] != DBNull.Value && (long)row[SpecialPeriodView.partCondition] > 0)
            {
                typePeriod = 1;
                code = (long)row[SpecialPeriodView.partCondition];
            }
            else
                if (row[SpecialPeriodView.stajBase] != DBNull.Value && (long)row[SpecialPeriodView.stajBase] > 0)
                {
                    typePeriod = 2;
                    code = (long)row[SpecialPeriodView.stajBase];
                }
                else
                    if (row[SpecialPeriodView.servYearBase] != DBNull.Value && (long)row[SpecialPeriodView.servYearBase] > 0)
                    {
                        typePeriod = 3;
                        code = (long)row[SpecialPeriodView.servYearBase];
                    }

            DateTime begin = (DateTime)row[SpecialPeriodView.beginDate];
            DateTime end = (DateTime)row[SpecialPeriodView.endDate];
            int month = (int)row[SpecialPeriodView.month];
            int day = (int)row[SpecialPeriodView.day];
            int hour = (int)row[SpecialPeriodView.hour];
            int minute = (int)row[SpecialPeriodView.minute];
            String profession = (string)row[SpecialPeriodView.profession];

            AddEditSpecialPeriodForm specialPeriodForm = new AddEditSpecialPeriodForm(_repYear, _generalPeriodBS, typePeriod, code, begin, end, month, day, hour, minute, profession);
            if (specialPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (specialPeriodForm.TypePeriod == 1)
                {
                    row[SpecialPeriodView.partCondition] = specialPeriodForm.Code;
                    row[SpecialPeriodView.partCode] = specialPeriodForm.CodeName;
                    row[SpecialPeriodView.stajBase] = 0;
                    row[SpecialPeriodView.stajCode] = 0;
                    row[SpecialPeriodView.servYearBase] = 0;
                    row[SpecialPeriodView.servCode] = 0;
                }
                else
                    if (specialPeriodForm.TypePeriod == 2)
                    {
                        row[SpecialPeriodView.partCondition] = 0;
                        row[SpecialPeriodView.partCode] = 0;
                        row[SpecialPeriodView.stajBase] = specialPeriodForm.Code;
                        row[SpecialPeriodView.stajCode] = specialPeriodForm.CodeName;
                        row[SpecialPeriodView.servYearBase] = 0;
                        row[SpecialPeriodView.servCode] = 0;
                    }
                    else
                        if (specialPeriodForm.TypePeriod == 3)
                        {
                            row[SpecialPeriodView.partCondition] = 0;
                            row[SpecialPeriodView.partCode] = 0;
                            row[SpecialPeriodView.stajBase] = 0;
                            row[SpecialPeriodView.stajCode] = 0;
                            row[SpecialPeriodView.servYearBase] = specialPeriodForm.Code;
                            row[SpecialPeriodView.servCode] = specialPeriodForm.CodeName;
                        }
                        else
                        {
                            MainForm.ShowErrorMessage("Не выбран тип стажа!", "Ошибка изменения периода специального стажа");
                        }

                row[SpecialPeriodView.beginDate] = specialPeriodForm.Begin;
                row[SpecialPeriodView.endDate] = specialPeriodForm.End;
                row[SpecialPeriodView.month] = specialPeriodForm.Month;
                row[SpecialPeriodView.day] = specialPeriodForm.Day;
                row[SpecialPeriodView.hour] = specialPeriodForm.Hour;
                row[SpecialPeriodView.minute] = specialPeriodForm.Minute;
                row[SpecialPeriodView.profession] = specialPeriodForm.Profession;
                _specPeriodBS.EndEdit();
            }
        }

        private void delSpecialPeriodButton_Click(object sender, EventArgs e)
        {
            DataRowView row = _specPeriodBS.Current as DataRowView;
            if (row == null)
                return;
            DateTime begin = (DateTime)row[SpecialPeriodView.beginDate];
            DateTime end = (DateTime)row[SpecialPeriodView.endDate];
            if (MainForm.ShowQuestionMessage(string.Format("Вы действительно хотите удалить период с {0} по {1}?", begin.ToShortDateString(), end.ToShortDateString()), "Удаление периода специального стажа") != System.Windows.Forms.DialogResult.Yes)
                return;
            _specPeriodBS.RemoveCurrent();
        }

        private void dataViewProfit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MainForm.ShowWarningMessage("Неверный формат данных.", "Ошибка");
            e.Cancel = true;
        }

        private void dataViewProfit_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            dataViewProfit.EndEdit();
            double[] sum = new double[6];

            if (_salaryInfoTableTranspose == null)
                return;
            foreach (DataRow row in _salaryInfoTableTranspose.Rows)
            {
                sum[0] += Convert.ToDouble(row[SalaryGroups.Column1.ToString()].ToString());
                sum[1] += Convert.ToDouble(row[SalaryGroups.Column2.ToString()].ToString());
                sum[2] += Convert.ToDouble(row[SalaryGroups.Column3.ToString()].ToString());
                sum[3] += Convert.ToDouble(row[SalaryGroups.Column4.ToString()].ToString());
                sum[4] += Convert.ToDouble(row[SalaryGroups.Column5.ToString()].ToString());
                sum[5] += Convert.ToDouble(row[SalaryGroups.Column10.ToString()].ToString());
            }

            if (_isAgriculture == 1 || _repYear > 2012)
            {
                sum[2] = Convert.ToDouble(sum3Box.Text);
                sum[3] = Convert.ToDouble(sum4Box.Text);
            }

            sum1Box.Text = sum[0].ToString("N2");
            sum2Box.Text = sum[1].ToString("N2");
            sum3Box.Text = sum[2].ToString("N2");
            sum4Box.Text = sum[3].ToString("N2");
            sum5Box.Text = sum[4].ToString("N2");
            sum10Box.Text = sum[5].ToString("N2");

            sumCalc3Box.Text = (sum[0] * _currentPercent).ToString("N2");
            sumCalc5Box.Text = (sum[0] * _currentObligatory).ToString("N2");

            sum3TextBox.Text = sum3Box.Text;
            sum5TextBox.Text = sum5Box.Text;
        }

        private void dataViewProfit_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ShowCellHint();
        }

        private void dataViewProfit_Scroll(object sender, ScrollEventArgs e)
        {
            ShowCellHint();
        }

        private void dataViewProfit_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ShowCellHint();
        }

        private void dataViewProfit_SizeChanged(object sender, EventArgs e)
        {
            ShowCellHint();
        }

        private void dataViewProfit_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 1)
                return;
            double val = Convert.ToDouble(e.Value);
            bool flag = val < 0;
            //flag |= (sender as DataGridView).Columns[e.ColumnIndex].DataPropertyName == "3" && val != hintValue;
            //flag |= (sender as DataGridView).Columns[e.ColumnIndex].DataPropertyName == "5" && val != hintValue;

            if (flag)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
            else
            {
                e.CellStyle.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
            }
        }

        private string GenerateFormText(long idDoc, long flagDoc)
        {
            StringBuilder builder = new StringBuilder();

            if (idDoc > 0)
            {
                builder.Append("Редактирование ");
            }
            else
            {
                builder.Append("Добавление ");
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
            return builder.ToString();
        }

        private void ShowCellHint()
        {
            if (dataViewProfit.CurrentCell == null)
                return;
            DataGridViewCell cell = dataViewProfit.CurrentCell;

            Rectangle cellRectangle = dataViewProfit.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
            panelHint.Left = dataViewProfit.Left + (cellRectangle.Left + cellRectangle.Width / 2) - panelHint.Width / 2;
            panelHint.Top = dataViewProfit.Top + cellRectangle.Bottom;
            double _hintValue = 0;

            if (cell.OwningColumn.DataPropertyName == SalaryGroups.Column3.ToString())
            {
                if (cellRectangle.Bottom != 0)
                {
                    _hintValue = Math.Round((double)(_salaryInfoBS.Current as DataRowView)[SalaryGroups.Column1] * _currentPercent, 2);
                    textBoxHint.Text = _hintValue.ToString("N2");
                    panelHint.Visible = true;
                }
                else
                {
                    panelHint.Visible = false;
                }
            }
            else
                if (cell.OwningColumn.DataPropertyName == SalaryGroups.Column5.ToString())
                {
                    if (cellRectangle.Bottom != 0)
                    {
                        _hintValue = Math.Round((double)(_salaryInfoBS.Current as DataRowView)[SalaryGroups.Column1] * _currentObligatory, 2);
                        textBoxHint.Text = _hintValue.ToString("N2");
                        panelHint.Visible = true;
                    }
                    else
                    {
                        panelHint.Visible = false;
                    }
                }
                else
                {
                    panelHint.Visible = false;
                }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                long docId = -1;
                using (_connection = new SQLiteConnection(_connectionStr))
                {
                    _connection.Open();
                    using (SQLiteTransaction transaction = _connection.BeginTransaction())
                    {
                        using (_command = _connection.CreateCommand())
                        {
                            _command.Transaction = transaction;

                            if (_currentDocId > 0)
                            {
                                docId = _currentDocId;
                                //Сохранение в таблицу Fixdata
                                _command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit, docId, _operator.nameVal, DateTime.Now.Date);
                            }
                            else
                            {
                                //Сохранение в таблицу Doc
                                _command.CommandText = Docs.GetInsertText(_flagDocType, _currentListId, _personId);
                                docId = (long)_command.ExecuteScalar();
                                if (docId < 1)
                                    throw new SQLiteException("Невозможно создать документ.");

                                //Сохранение в таблицу Fixdata
                                _command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.New, docId, _operator.nameVal, DateTime.Now.Date);
                            }

                            if ((long)_command.ExecuteScalar() < 1)
                            {
                                throw new SQLiteException("Невозможно создать документ. Таблица " + FixData.tablename + ".");
                            }

                            //Сохранение в таблицу IndDoc
                            //идентификаторы выбранных гражданств
                            long _currentCitizen1Id = 174;
                            long _currentCitizen2Id = 0;
                            if (_citizen1BS.Current != null)
                            {
                                _currentCitizen1Id = (long)(_citizen1BS.Current as DataRowView)[Country.id];
                            }
                            if (_citizen2BS.Current != null)
                            {
                                _currentCitizen2Id = (long)(_citizen2BS.Current as DataRowView)[Country.id];
                            }
                            _command.CommandText = IndDocs.GetReplaceText(docId, _currentClassPercentId, additionalRadioButton.Checked ? (int)IndDocs.Job.Second : (int)IndDocs.Job.General, _currentCitizen1Id, _currentCitizen2Id);
                            if ((long)_command.ExecuteScalar() < 1)
                            {
                                throw new SQLiteException("Невозможно изменить документ. Таблица " + IndDocs.tablename + ".");
                            }

                            //Сохранение в таблицу Gen_period
                            if (_generalPeriodBS == null || _generalPeriodBS.DataSource == null)
                            {
                                throw new ObjectDisposedException("Источник данных для основного периода не задан!", "Ошибка");
                            }
                            if (_generalPeriodBS.Count < 1)
                            {
                                DataRowView row = _generalPeriodBS.AddNew() as DataRowView;
                                row[GeneralPeriod.beginDate] = DateTime.Parse(_repYear + "-01-01");
                                row[GeneralPeriod.endDate] = DateTime.Parse(_repYear + "-12-31");
                                _generalPeriodBS.EndEdit();
                            }
                            GeneralPeriod.SetDocId(_generalPeriodTable, docId);
                            _adapter = GeneralPeriod.CreateAdapter(_connection, transaction);
                            _adapter.Update(_generalPeriodTable);
                            //Сохранение в таблицу Dop_period
                            DopPeriod.SetDocId(_dopPeriodTable, docId);
                            _adapter = DopPeriod.CreateAdapter(_connection, transaction);
                            _adapter.Update(_dopPeriodTable);
                            //Сохранение в таблицу Spec_period
                            SpecialPeriodView.SetDocId(_specPeriodTable, docId);
                            _adapter = SpecialPeriodView.CreateAdapter(_connection, transaction);
                            _adapter.Update(_specPeriodTable);
                            //Сохранение в таблицу Salary_Info
                            if (_salaryInfoTable == null)
                                _salaryInfoTable = SalaryInfo.CreateTableWithRows();
                            SalaryInfoTranspose.ConvertToSalaryInfo(_salaryInfoTableTranspose, _salaryInfoTable);
                            _salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column1)][SalaryInfo.sum] = Convert.ToDouble(sum1Box.Text);
                            _salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column2)][SalaryInfo.sum] = Convert.ToDouble(sum2Box.Text);
                            _salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column3)][SalaryInfo.sum] = Convert.ToDouble(sum3Box.Text);
                            _salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column4)][SalaryInfo.sum] = Convert.ToDouble(sum4Box.Text);
                            _salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column5)][SalaryInfo.sum] = Convert.ToDouble(sum5Box.Text);
                            _salaryInfoTable.Rows[SalaryInfo.FindRowIndex(_salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column10)][SalaryInfo.sum] = Convert.ToDouble(sum10Box.Text);
                            SalaryInfo.SetDocId(_salaryInfoTable, docId);
                            _adapter = SalaryInfo.CreateAdapter(_connection, transaction);
                            _adapter.Update(_salaryInfoTable);
                        }
                        transaction.Commit();
                        _currentDocId = docId;
                    }
                    _connection.Close();
                }

                MainForm.ShowInfoMessage("Данные о стаже и доходе успешно сохранены!", "Сохранение");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorMessage(ex.Message, "Ошибка сохранения документа");
            }
            finally
            {
            }
        }

        private void sumBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox text = sender as TextBox;
            String torZ = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            if (Char.IsDigit(e.KeyChar) == true)
                return;
            if (Char.IsControl(e.KeyChar))
                return;

            if (e.KeyChar.ToString() != torZ && (e.KeyChar.ToString() == "." || e.KeyChar.ToString() == ","))
            {
                e.KeyChar = torZ[0];
            }
            if (e.KeyChar.ToString() == torZ)
            {
                if (text.Text.IndexOf(torZ) != -1)// Разделительный знак найден 
                    e.Handled = true;
                return;
            }

            if (e.KeyChar == '-')
            {
                if (text.Text.IndexOf('-') != -1)
                    e.Handled = true;
                return;
            }
            e.Handled = true;

        }

        private void sumBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Text = text.Text.Trim();
            if (String.IsNullOrEmpty(text.Text) || text.Text == System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
                text.Text = "0";
            double result = 0;
            if (!Double.TryParse(text.Text.ToString(), out result))
            {
                if (!Double.TryParse(text.Text.ToString().Replace('.', ','), out result))
                {
                    e.Cancel = true;
                    MainForm.ShowWarningMessage("Неверный формат данных.", "Ошибка");
                    return;
                }
            }
            text.Text = Math.Round(result, 2).ToString("N2");
        }

        private void generalPeriodDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            editGeneralPeriodButton_Click(null, null);
        }

        private void additionalPeriodDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            editAdditionalPeriodButton_Click(null, null);
        }

        private void specialPeriodDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            editSpecialPeriodButton_Click(null, null);
        }

        private void PeriodDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewRow r = (sender as DataGridView).Rows[e.RowIndex];
                if (!r.Selected)
                {
                    r.DataGridView.ClearSelection();
                    r.DataGridView.CurrentCell = r.Cells[0];
                    r.Selected = true;
                }
            }
        }

        private void generalPeriodDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Apps)
            {
                DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
                if (currentCell != null)
                {
                    ContextMenuStrip cms = (sender as DataGridView).ContextMenuStrip;
                    if (cms != null)
                    {

                        Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                        Point p = new Point(r.X, r.Y);
                        cms.Show((sender as DataGridView), p);
                    }
                }
            }
        }

        private void generalPeriodDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Windows.Forms.ContextMenuStrip menu = cmsGeneralPeriod;
                if (menu == null)
                    return;

                DataGridView dataView = sender as DataGridView;
                ToolStripItem[] items;

                int currentMouseOverRow = dataView.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow < 0)
                {
                    items = menu.Items.Find("editGeneralPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = false;
                    items = menu.Items.Find("delGeneralPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = false;
                }
                else
                {
                    items = menu.Items.Find("editGeneralPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = true;
                    items = menu.Items.Find("delGeneralPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = true;
                }
                menu.Show(dataView, e.Location);
            }
        }

        private void additionalPeriodDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Windows.Forms.ContextMenuStrip menu = cmsAdditionalPeriod;
                if (menu == null)
                    return;

                DataGridView dataView = sender as DataGridView;
                ToolStripItem[] items;

                int currentMouseOverRow = dataView.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow < 0)
                {
                    items = menu.Items.Find("editAdditionalPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = false;
                    items = menu.Items.Find("delAdditionalPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = false;
                }
                else
                {
                    items = menu.Items.Find("editAdditionalPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = true;
                    items = menu.Items.Find("delAdditionalPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = true;
                }
                menu.Show(dataView, e.Location);
            }
        }

        private void specialPeriodDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Windows.Forms.ContextMenuStrip menu = cmsSpecialPeriod;
                if (menu == null)
                    return;

                DataGridView dataView = sender as DataGridView;
                ToolStripItem[] items;

                int currentMouseOverRow = dataView.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow < 0)
                {
                    items = menu.Items.Find("editSpecialPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = false;
                    items = menu.Items.Find("delSpecialPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = false;
                }
                else
                {
                    items = menu.Items.Find("editSpecialPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = true;
                    items = menu.Items.Find("delSpecialPeriodMenuItem", false);
                    if (items.Count() > 0)
                        items[0].Enabled = true;
                }
                menu.Show(dataView, e.Location);
            }
        }

        private void sum3Box_TextChanged(object sender, EventArgs e)
        {
            sum3TextBox.Text = sum3Box.Text;
        }

        private void sum5Box_TextChanged(object sender, EventArgs e)
        {
            sum5TextBox.Text = sum5Box.Text;
        }
    }
}
