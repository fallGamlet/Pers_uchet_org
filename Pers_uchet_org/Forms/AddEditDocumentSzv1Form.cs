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
        int _repYear;
        long _personId;
        string _connection;
        //id документа, если производится редактирование
        long _idDoc;
        //тип документа
        int _flagDoc;

        //процент по выбранной категории
        double _currentPercent;
        double _currentObligatory;
        //значение ячейки в подсказке
        double _hintValue;
        SQLiteConnection _con;
        SQLiteCommand _command;
        //объекты таблиц для хранения записей
        DataTable _salaryInfoTable;         //таблица зарплаты человека
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
        public AddEditDocumentSzv1Form()
        {
            InitializeComponent();
            _idDoc = -1;
            _currentPercent = 0;
            _currentObligatory = 0;
            _hintValue = 0;
        }

        public AddEditDocumentSzv1Form(Org org, int repYear, long personId, int flagDoc, string connection, long idDoc = -1)
            : this()
        {
            this._org = org;
            this._repYear = repYear;
            this._personId = personId;
            this._flagDoc = flagDoc;

            this._connection = connection;
            this._idDoc = idDoc;

        }

        private void AddEditDocumentSzv1Form_Load(object sender, EventArgs e)
        {
            orgNameTextBox.Text = _org.nameVal;
            regNumTextBox.Text = _org.regnumVal;
            yearLabel.Text = _repYear.ToString();

            GenerateFormText();

            //заполнение источников гражданства
            if (MainForm.CountryTable == null)
                MainForm.CountryTable = Country.CreateTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(Country.GetSelectText(), _connection);
            if (MainForm.CountryTable.Rows.Count <= 0)
                adapter.Fill(MainForm.CountryTable);

            _citizen1BS = new BindingSource();
            _citizen1BS.DataSource = new DataView(MainForm.CountryTable);
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
            _salaryInfoTable = SalaryInfo.CreatetTable();
            if (_idDoc > 0)
            {
                //TODO: Переделать заполнение данными из базы
                //adapter = new SQLiteDataAdapter(SalaryInfo.GetSelectText(_idDoc), _connection);
                //adapter.Fill(_salaryInfoTable);
                //_salaryInfoTable.Columns.Remove(SalaryInfo.id);
                //_salaryInfoTable.Columns.Remove(SalaryInfo.docId);
                //_salaryInfoTable = SalaryInfo.TransposeDataTable(_salaryInfoTable, 0);
            }
            else
            {
                _salaryInfoTable = SalaryInfo.CreatetTransposeTable();
                for (int i = 1; i < 13; i++)
                {
                    DataRow row = _salaryInfoTable.NewRow();
                    row["months"] = i;
                    row[1] = 0.0;
                    row[2] = 0.0;
                    row[3] = 0.0;
                    row[4] = 0.0;
                    row[5] = 0.0;
                    row[6] = 0.0;
                    _salaryInfoTable.Rows.Add(row);
                }

            }
            _salaryInfoBS = new BindingSource();
            _salaryInfoBS.DataSource = _salaryInfoTable;
            dataViewProfit.CellParsing += new DataGridViewCellParsingEventHandler(dataViewProfit_CellParsing);
            dataViewProfit.DataSource = _salaryInfoBS;

            _con = new SQLiteConnection(_connection);
            _command = new SQLiteCommand(PersonView.GetSelectText(_org.idVal, _personId), _con);
            if (_con.State != ConnectionState.Open)
                _con.Open();
            SQLiteDataReader reader = _command.ExecuteReader();
            if (reader.Read())
            {
                textBoxAnketaName.Text = reader[PersonView.fio].ToString();
                textBoxInsNum.Text = reader[PersonView.socNumber].ToString();
                if (_idDoc > 0)
                {
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

            _generalPeriodTable = GeneralPeriod.CreatetTable();
            if (_idDoc > 0)
            {
            }
            else
            {
                _generalPeriodBS = new BindingSource();
                _generalPeriodBS.DataSource = _generalPeriodTable;
                generalPeriodDataGridView.AutoGenerateColumns = false;
                generalPeriodDataGridView.DataSource = _generalPeriodBS;
                generalPeriodDataGridView.Columns["beginGeneralPeriodColumn"].DataPropertyName = GeneralPeriod.beginDate;
                generalPeriodDataGridView.Columns["endGeneralPeriodColumn"].DataPropertyName = GeneralPeriod.endDate;
            }

            _dopPeriodTable = DopPeriodView.CreatetTable();
            if (_idDoc > 0)
            {
            }
            else
            {
                _dopPeriodBS = new BindingSource();
                _dopPeriodBS.DataSource = _dopPeriodTable;
                additionalPeriodDataGridView.AutoGenerateColumns = false;
                additionalPeriodDataGridView.DataSource = _dopPeriodBS;
                additionalPeriodDataGridView.Columns["codeAdditionalPeriodColumn"].DataPropertyName = DopPeriodView.code;
                additionalPeriodDataGridView.Columns["beginAdditionalPeriodColumn"].DataPropertyName = DopPeriodView.beginDate;
                additionalPeriodDataGridView.Columns["endAdditionalPeriodColumn"].DataPropertyName = DopPeriodView.endDate;
            }

            _specPeriodTable = SpecialPeriodView.CreatetTable();
            if (_idDoc > 0)
            {
            }
            else
            {
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
            }

            //int pos = _citizen1BS.Find(Country.name, "ПМР");
            //if (pos != -1)
            //    _citizen1BS.Position = pos;
            //else
            //    _citizen1BS.MoveFirst();

            //_citizen2BS.MoveFirst();
        }
        #endregion

        #region Методы - обработчики событий
        void dataViewProfit_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            DataGridView view = sender as DataGridView;

            if (String.IsNullOrEmpty(e.Value.ToString().Trim()))
            {
                if (view.Columns[e.ColumnIndex].DataPropertyName == "10")
                    e.Value = 0;
                else
                    e.Value = 0.0;
                e.ParsingApplied = true;
                return;
            }

            double result = 0;
            if (view.Columns[e.ColumnIndex].DataPropertyName == "10")
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
            {
                return;
            }

            _currentPercent = (double)row[ClasspercentView.value];
            percentLabel.Text = (_currentPercent * 100).ToString() + " %";

            if (int.Parse(row[ClasspercentView.obligatoryIsEnabled].ToString()) != 0)
                _currentObligatory = ObligatoryPercent.GetValue(_repYear, _connection);
            else
                _currentObligatory = 0;

            dataViewProfit_CellValidated(null, null);
        }

        private void addGeneralPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditGeneralPeriodForm generalPeriodForm = new AddEditGeneralPeriodForm(_repYear);
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
            AddEditGeneralPeriodForm generalPeriodForm = new AddEditGeneralPeriodForm(_repYear, (DateTime)row[GeneralPeriod.beginDate], (DateTime)row[GeneralPeriod.endDate]);
            if (generalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                row[GeneralPeriod.beginDate] = generalPeriodForm.Begin;
                row[GeneralPeriod.endDate] = generalPeriodForm.End;
            }
        }

        private void delGeneralPeriodButton_Click(object sender, EventArgs e)
        {
            if (_generalPeriodBS.Current == null)
                return;
            DateTime begin = (DateTime)(_generalPeriodBS.Current as DataRowView)[GeneralPeriod.beginDate];
            DateTime end = (DateTime)(_generalPeriodBS.Current as DataRowView)[GeneralPeriod.endDate];
            if (MainForm.ShowQuestionMessage(string.Format("Вы действительно хотите удалить период с {0} по {1}?", begin.ToShortDateString(), end.ToShortDateString()), "Удаление периода основного стажа") != System.Windows.Forms.DialogResult.Yes)
                return;
            _generalPeriodBS.RemoveCurrent();

        }

        private void addAdditionalPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditAdditionalPeriodForm additionalPeriodForm = new AddEditAdditionalPeriodForm(_repYear);
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


            AddEditAdditionalPeriodForm additionalPeriodForm = new AddEditAdditionalPeriodForm(_repYear, (long)row[DopPeriodView.classificatorId], (DateTime)row[DopPeriodView.beginDate], (DateTime)row[DopPeriodView.endDate]);
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
            if (_dopPeriodBS.Current == null)
                return;
            DateTime begin = (DateTime)(_dopPeriodBS.Current as DataRowView)[DopPeriodView.beginDate];
            DateTime end = (DateTime)(_dopPeriodBS.Current as DataRowView)[DopPeriodView.endDate];
            if (MainForm.ShowQuestionMessage(string.Format("Вы действительно хотите удалить период с {0} по {1}?", begin.ToShortDateString(), end.ToShortDateString()), "Удаление периода дополнительного стажа") != System.Windows.Forms.DialogResult.Yes)
                return;
            _dopPeriodBS.RemoveCurrent();
        }

        private void addSpecialPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditSpecialPeriodForm specialPeriodForm = new AddEditSpecialPeriodForm(_repYear);
            if (specialPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                DataRowView row = _specPeriodBS.AddNew() as DataRowView;
                switch (specialPeriodForm.TypePeriod)
                {
                    case 1:
                        row[SpecialPeriodView.partCondition] = specialPeriodForm.Code;
                        row[SpecialPeriodView.partCode] = specialPeriodForm.CodeName;
                        break;
                    case 2:
                        row[SpecialPeriodView.stajBase] = specialPeriodForm.Code;
                        row[SpecialPeriodView.stajCode] = specialPeriodForm.CodeName;
                        break;
                    case 3:
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
            AddEditSpecialPeriodForm specialPeriodForm = new AddEditSpecialPeriodForm();
            if (specialPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void delSpecialPeriodButton_Click(object sender, EventArgs e)
        {

        }

        private void dataViewProfit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MainForm.ShowErrorMessage("Неверный формат данных.", "Ошибка");
            e.Cancel = true;
        }

        private void dataViewProfit_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            dataViewProfit.EndEdit();
            double[] sum = new double[6];

            if (_salaryInfoTable == null)
                return;
            foreach (DataRow row in _salaryInfoTable.Rows)
            {
                sum[0] += Convert.ToDouble(row[1].ToString());
                sum[1] += Convert.ToDouble(row[2].ToString());
                sum[2] += Convert.ToDouble(row[3].ToString());
                sum[3] += Convert.ToDouble(row[4].ToString());
                sum[4] += Convert.ToDouble(row[5].ToString());
                sum[5] += Convert.ToDouble(row[6].ToString());
            }
            sum1Box.Text = sum[0].ToString("N2");
            sum2Box.Text = sum[1].ToString("N2");
            sum3Box.Text = sum[2].ToString("N2");
            sum4Box.Text = sum[3].ToString("N2");
            sum5Box.Text = sum[4].ToString("N2");
            sum6Box.Text = sum[5].ToString("N2");

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
        private void GenerateFormText()
        {
            StringBuilder builder = new StringBuilder();

            if (_idDoc > 0)
            {
                builder.Append("Редактирование ");
            }
            else
            {
                builder.Append("Добавление ");
            }
            builder.Append("документа \"СЗВ-1\"");
            switch (_flagDoc)
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
        }

        private void ShowCellHint()
        {
            if (dataViewProfit.CurrentCell == null)
                return;
            DataGridViewCell cell = dataViewProfit.CurrentCell;

            if (cell.OwningColumn.DataPropertyName == "3")
            {
                Rectangle cellRectangle = dataViewProfit.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
                if (cellRectangle.Bottom != 0)
                {
                    panelHint.Left = dataViewProfit.Left + (cellRectangle.Left + cellRectangle.Width / 2) - panelHint.Width / 2;
                    panelHint.Top = dataViewProfit.Top + cellRectangle.Bottom;
                    _hintValue = Math.Round((double)(_salaryInfoBS.Current as DataRowView)["1"] * _currentPercent, 2);
                    textBoxHint.Text = _hintValue.ToString("N2");
                    panelHint.Visible = true;
                }
                else
                {
                    panelHint.Visible = false;
                }
            }
            else
                if (cell.OwningColumn.DataPropertyName == "5")
                {
                    Rectangle cellRectangle = dataViewProfit.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
                    if (cellRectangle.Bottom != 0)
                    {
                        panelHint.Left = dataViewProfit.Left + (cellRectangle.Left + cellRectangle.Width / 2) - panelHint.Width / 2;
                        panelHint.Top = dataViewProfit.Top + cellRectangle.Bottom;
                        _hintValue = Math.Round((double)(_salaryInfoBS.Current as DataRowView)["1"] * _currentObligatory, 2);
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
        #endregion
    }
}
