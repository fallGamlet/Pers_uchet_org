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
        string _connection;
        //id документа, если производится редактирование
        long _currentDocId;
        //тип документа
        long _flagDocType;
        //процент по выбранной категории
        long _currentClassPercent;
        double _currentPercent;
        double _currentObligatory;
        //идентификаторы выбранных гражданств
        long _currentCitizen1Id;
        long _currentCitizen2Id;
        //значение ячейки в подсказке
        double _hintValue;
        //суммы в по столбцам
        double[] sum;

        SQLiteConnection _con;
        SQLiteCommand _command;
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

        #endregion

        #region Конструктор и инициализатор
        private AddEditDocumentSzv1Form()
        {
            InitializeComponent();
            _currentDocId = -1;
            _currentPercent = 0;
            _currentObligatory = 0;
            _hintValue = 0;
            _personId = 0;
            _currentClassPercent = 0;
            _currentCitizen1Id = 174; //ПМР
            _currentCitizen2Id = 0;
            sum = new double[6];
        }

        public AddEditDocumentSzv1Form(Org org, Operator _operator, long currentListId, int repYear, long personId, int flagDocType, string connection, long idDoc = -1)
            : this()
        {
            this._org = org;
            this._operator = _operator;
            this._currentListId = currentListId;
            this._repYear = repYear;
            this._personId = personId;
            this._flagDocType = flagDocType;
            this._connection = connection;
            this._currentDocId = idDoc;
        }

        private void AddEditDocumentSzv1Form_Load(object sender, EventArgs e)
        {
            orgNameTextBox.Text = _org.nameVal;
            regNumTextBox.Text = _org.regnumVal;
            yearLabel.Text = _repYear.ToString();
            saveButton.Enabled = false;
            this.Text = GenerateFormText(_currentDocId, _flagDocType);

            //заполнение источников гражданства
            if (MainForm.CountryTable == null)
                MainForm.CountryTable = Country.CreateTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(Country.GetSelectText(), _connection);
            if (MainForm.CountryTable.Rows.Count <= 0)
                adapter.Fill(MainForm.CountryTable);

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
            if (MainForm.ClasspercentViewTable == null)
                MainForm.ClasspercentViewTable = ClasspercentView.CreateTable();
            if (MainForm.ClasspercentViewTable.Rows.Count < 1)
            {
                adapter = new SQLiteDataAdapter(ClasspercentView.GetSelectText(), _connection);
                adapter.Fill(MainForm.ClasspercentViewTable);

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
            _classpercentView100BS = new BindingSource();
            _classpercentView100BS.DataSource = MainForm.ClasspercentViewTable;
            _classpercentView100BS.CurrentChanged += new EventHandler(_classpercentViewBS_CurrentChanged);
            _classpercentView100BS.Filter = ClasspercentView.GetBindingSourceFilterFor100(DateTime.Parse(_repYear + "-01-01"));
            _classpercentView100BS.Sort = ClasspercentView.code;
            codeComboBox.DataSource = _classpercentView100BS;
            codeComboBox.ValueMember = ClasspercentView.id;
            codeComboBox.DisplayMember = ClasspercentView.code;


            //формирование источников для таблицы зарплат
            //_salaryInfoTable = SalaryInfo.CreatetTable();

            _salaryInfoTableTranspose = SalaryInfo.CreateTransposeTable();
            for (int i = 1; i < 13; i++)
            {
                DataRow row = _salaryInfoTableTranspose.NewRow();
                row["months"] = i;
                row[1] = 0.0;
                row[2] = 0.0;
                row[3] = 0.0;
                row[4] = 0.0;
                row[5] = 0.0;
                row[6] = 0.0;
                _salaryInfoTableTranspose.Rows.Add(row);
            }

            if (_currentDocId > 0)
            {
                //TODO: Переделать заполнение данными из базы
            }

            _salaryInfoBS = new BindingSource();
            _salaryInfoBS.DataSource = _salaryInfoTableTranspose;
            dataViewProfit.CellParsing += new DataGridViewCellParsingEventHandler(dataViewProfit_CellParsing);
            dataViewProfit.DataSource = _salaryInfoBS;

            //Выбор ФИО, страхового номера, гражданства из базы
            _con = new SQLiteConnection(_connection);
            _command = new SQLiteCommand(PersonView.GetSelectText(_org.idVal, _personId), _con);
            if (_con.State != ConnectionState.Open)
                _con.Open();
            SQLiteDataReader reader = _command.ExecuteReader();
            if (reader.Read())
            {
                textBoxAnketaName.Text = reader[PersonView.fio].ToString();
                textBoxInsNum.Text = reader[PersonView.socNumber].ToString();
                if (_currentDocId > 0)
                {
                    //TODO: Переделать заполнение данными из базы
                }
                else
                {
                    long citizen = (long)reader[PersonView.citizen1ID];
                    _citizen1BS.Position = _citizen1BS.Find(Country.id, citizen);
                    citizen = (long)reader[PersonView.citizen2ID];
                    _citizen2BS.Position = _citizen2BS.Find(Country.id, citizen);
                }
            }
            reader.Close();
            _con.Close();

            //Заполнение таблиц с периодами
            _generalPeriodTable = GeneralPeriod.CreatetTable();
            if (_currentDocId > 0)
            {
                //TODO: Переделать заполнение данными из базы
            }
            _generalPeriodBS = new BindingSource();
            _generalPeriodBS.DataSource = _generalPeriodTable;
            generalPeriodDataGridView.AutoGenerateColumns = false;
            generalPeriodDataGridView.DataSource = _generalPeriodBS;
            generalPeriodDataGridView.Columns["beginGeneralPeriodColumn"].DataPropertyName = GeneralPeriod.beginDate;
            generalPeriodDataGridView.Columns["endGeneralPeriodColumn"].DataPropertyName = GeneralPeriod.endDate;

            _dopPeriodTable = DopPeriodView.CreatetTable();
            if (_currentDocId > 0)
            {
                //TODO: Переделать заполнение данными из базы
            }
            _dopPeriodBS = new BindingSource();
            _dopPeriodBS.DataSource = _dopPeriodTable;
            additionalPeriodDataGridView.AutoGenerateColumns = false;
            additionalPeriodDataGridView.DataSource = _dopPeriodBS;
            additionalPeriodDataGridView.Columns["codeAdditionalPeriodColumn"].DataPropertyName = DopPeriodView.code;
            additionalPeriodDataGridView.Columns["beginAdditionalPeriodColumn"].DataPropertyName = DopPeriodView.beginDate;
            additionalPeriodDataGridView.Columns["endAdditionalPeriodColumn"].DataPropertyName = DopPeriodView.endDate;

            _specPeriodTable = SpecialPeriodView.CreatetTable();
            if (_currentDocId > 0)
            {
                //TODO: Переделать заполнение данными из базы
            }
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

            saveButton.Enabled = true;
        }
        #endregion

        #region Методы - обработчики событий
        void dataViewProfit_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            DataGridView view = sender as DataGridView;

            if (String.IsNullOrEmpty(e.Value.ToString().Trim()))
            {
                if (view.Columns[e.ColumnIndex].DataPropertyName == SalaryGroups.Column10.ToString())
                    e.Value = 0;
                else
                    e.Value = 0.0;
                e.ParsingApplied = true;
                return;
            }

            double result = 0;
            if (view.Columns[e.ColumnIndex].DataPropertyName == SalaryGroups.Column10.ToString())
            {
                if (Double.TryParse(e.Value.ToString(), out result) || Double.TryParse(e.Value.ToString().Replace('.', ','), out result))
                {
                    e.Value = Convert.ToInt32(result);
                    e.ParsingApplied = true;
                    return;
                }
            }

            if (Double.TryParse(e.Value.ToString(), out result) || Double.TryParse(e.Value.ToString().Replace('.', ','), out result))
            {
                e.Value = Math.Round(result, 2);
                e.ParsingApplied = true;
            }
        }

        void _classpercentViewBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
                return;

            _currentPercent = (double)row[ClasspercentView.value];
            _currentClassPercent = (long)row[ClasspercentView.id];
            percentLabel.Text = (_currentPercent * 100).ToString() + " %";

            if (int.Parse(row[ClasspercentView.obligatoryIsEnabled].ToString()) != 0)
                _currentObligatory = ObligatoryPercent.GetValue(_repYear, _connection);
            else
                _currentObligatory = 0;

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
            MainForm.ShowErrorMessage("Неверный формат данных.", "Ошибка");
            e.Cancel = true;
        }

        private void dataViewProfit_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            dataViewProfit.EndEdit();
            sum = new double[6];

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
        #endregion

        #region Методы
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
                if (_currentDocId > 0)
                {
                    //TODO: Сохранение изменений
                }
                else
                {
                    using (_con = new SQLiteConnection(_connection))
                    {
                        _con.Open();
                        using (SQLiteTransaction transaction = _con.BeginTransaction())
                        {
                            using (_command = _con.CreateCommand())
                            {
                                _command.Transaction = transaction;
                                //Сохранение в таблицу Doc
                                _command.CommandText = Docs.GetInsertText(_flagDocType, _currentListId, _personId);
                                _currentDocId = (long)_command.ExecuteScalar();
                                if (_currentDocId < 1)
                                {
                                    throw new SQLiteException("Невозможно создать документ.");
                                }
                                //Сохранение в таблицу IndDoc
                                if (_citizen1BS.Current != null)
                                {
                                    _currentCitizen1Id = (long)(_citizen1BS.Current as DataRowView)[Country.id];
                                }
                                if (_citizen2BS.Current != null)
                                {
                                    _currentCitizen2Id = (long)(_citizen2BS.Current as DataRowView)[Country.id];
                                }
                                _command.CommandText = IndDocs.GetInsertText(_currentDocId, _currentClassPercent, additionalRadioButton.Checked ? 2 : 1, _currentCitizen1Id, _currentCitizen2Id);
                                if ((long)_command.ExecuteScalar() < 1)
                                {
                                    throw new SQLiteException("Невозможно создать документ. Таблица " + IndDocs.tablename + ".");
                                }
                                //Сохранение в таблицу Gen_period
                                foreach (DataRowView row in _generalPeriodBS)
                                {
                                    _command.CommandText = GeneralPeriod.GetInsertText(_currentDocId, (DateTime)row[GeneralPeriod.beginDate], (DateTime)row[GeneralPeriod.endDate]);
                                    if ((long)_command.ExecuteScalar() < 1)
                                    {
                                        throw new SQLiteException("Невозможно создать документ. Таблица " + GeneralPeriod.tablename + ".");
                                    }
                                }
                                //Сохранение в таблицу Dop_period
                                foreach (DataRowView row in _dopPeriodBS)
                                {
                                    _command.CommandText = DopPeriod.GetInsertText(_currentDocId, (long)row[DopPeriodView.classificatorId], (DateTime)row[DopPeriodView.beginDate], (DateTime)row[DopPeriodView.endDate]);
                                    if ((long)_command.ExecuteScalar() < 1)
                                    {
                                        throw new SQLiteException("Невозможно создать документ. Таблица " + DopPeriod.tablename + ".");
                                    }
                                }
                                //Сохранение в таблицу Spec_period
                                foreach (DataRowView row in _specPeriodBS)
                                {
                                    _command.CommandText = SpecialPeriod.GetInsertText(_currentDocId, (long)row[SpecialPeriodView.partCondition],
                                                                                                      (long)row[SpecialPeriodView.stajBase],
                                                                                                      (long)row[SpecialPeriodView.servYearBase],
                                                                                                      (DateTime)row[SpecialPeriodView.beginDate],
                                                                                                      (DateTime)row[SpecialPeriodView.endDate],
                                                                                                      (int)row[SpecialPeriodView.month],
                                                                                                      (int)row[SpecialPeriodView.day],
                                                                                                      (int)row[SpecialPeriodView.hour],
                                                                                                      (int)row[SpecialPeriodView.minute],
                                                                                                      row[SpecialPeriodView.profession].ToString());
                                    if ((long)_command.ExecuteScalar() < 1)
                                    {
                                        throw new SQLiteException("Невозможно создать документ. Таблица " + SpecialPeriod.tablename + ".");
                                    }
                                }
                                //Сохранение в таблицу Salary_Info
                                _command = SalaryInfo.CreateInsertCommand();
                                _command.Connection = _con;
                                _command.Transaction = transaction;
                                foreach (DataColumn column in _salaryInfoTableTranspose.Columns)
                                {
                                    if (column.ColumnName == SalaryGroups.Column1.ToString() || column.ColumnName == SalaryGroups.Column2.ToString() || column.ColumnName == SalaryGroups.Column3.ToString() || column.ColumnName == SalaryGroups.Column4.ToString() || column.ColumnName == SalaryGroups.Column5.ToString() || column.ColumnName == SalaryGroups.Column10.ToString())
                                    {
                                        _command.Parameters[SalaryInfo.pDocId].Value = _currentDocId;
                                        _command.Parameters[SalaryInfo.pSalaryGroupsId].Value = Int64.Parse(column.ColumnName);
                                        _command.Parameters[SalaryInfo.pJanuary].Value = _salaryInfoTableTranspose.Rows[0][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pFebruary].Value = _salaryInfoTableTranspose.Rows[1][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pMarch].Value = _salaryInfoTableTranspose.Rows[2][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pApril].Value = _salaryInfoTableTranspose.Rows[3][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pMay].Value = _salaryInfoTableTranspose.Rows[4][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pJune].Value = _salaryInfoTableTranspose.Rows[5][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pJuly].Value = _salaryInfoTableTranspose.Rows[6][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pAugust].Value = _salaryInfoTableTranspose.Rows[7][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pSeptember].Value = _salaryInfoTableTranspose.Rows[8][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pOctober].Value = _salaryInfoTableTranspose.Rows[9][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pNovember].Value = _salaryInfoTableTranspose.Rows[10][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pDecember].Value = _salaryInfoTableTranspose.Rows[11][column.ColumnName];
                                        _command.Parameters[SalaryInfo.pSum].Value = sum[column.Ordinal - 1];
                                        //_command.Parameters[SalaryInfo.pSum].Value = Double.Parse((this.Controls.Find("sum" + column.ColumnName + "Box", true)[0] as TextBox).Text.Replace('.', ','));

                                        if ((long)_command.ExecuteScalar() < 1)
                                        {
                                            throw new SQLiteException("Невозможно создать документ. Таблица " + SalaryInfo.tablename + ".");
                                        }
                                    }
                                }

                                //Сохранение в таблицу Fixdata
                                //_command = _con.CreateCommand();
                                //_command.Transaction = transaction;
                                //_command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.New, _currentDocId, _operator.nameVal, DateTime.Now.Date);
                                //if ((long)_command.ExecuteScalar() < 1)
                                //{
                                //    throw new SQLiteException("Невозможно создать документ. Таблица " + FixData.tablename + ".");
                                //}
                            }
                            transaction.Commit();
                        }
                        _con.Close();
                    }
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
        #endregion
    }
}
