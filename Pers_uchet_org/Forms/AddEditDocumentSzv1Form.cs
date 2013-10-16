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
        private string _connection;
        //id документа, если производится редактирование
        long idDoc = -1;
        //тип документа
        private int flagDoc;
        //новый документ или производится редактирование
        private bool isNew;
        //процент по выбранной категории
        private double currentPercent;
        private double currentObligatory;
        //значение ячейки в подсказке
        private double hintValue;
        private SQLiteConnection con;
        private SQLiteCommand command;
        //объекты таблиц для хранения записей
        DataTable _salaryInfoTable;         //таблица зарплаты человека
        //объекты BindingSource для синхронизации таблиц и отображателей
        BindingSource _citizen1BS;
        BindingSource _citizen2BS;
        BindingSource _classpercentView100BS;
        //BindingSource _classpercentView200BS;
        //public BindingSource _classpercentView300BS;
        //public BindingSource _classpercentView400BS;
        //public BindingSource _classpercentView500BS;
        BindingSource _salaryInfoBS;
        #endregion

        public AddEditDocumentSzv1Form()
        {
            InitializeComponent();
        }

        public AddEditDocumentSzv1Form(Org _org, int RepYear, long personId, int flagDoc, bool isNew, string _connection, long idDoc = -1)
            : this()
        {
            this._org = _org;
            this.RepYear = RepYear;
            this.personId = personId;
            this.flagDoc = flagDoc;
            this.isNew = isNew;
            this._connection = _connection;
            this.idDoc = idDoc;
            this.dataViewProfit.CellParsing += new DataGridViewCellParsingEventHandler(dataViewProfit_CellParsing);
        }

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

        private void AddEditDocumentSzv1Form_Load(object sender, EventArgs e)
        {
            orgNameTextBox.Text = _org.nameVal;
            regNumTextBox.Text = _org.regnumVal;
            yearLabel.Text = RepYear.ToString();
            currentPercent = 0;
            currentObligatory = 0;
            hintValue = 0;
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

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(Country.GetSelectText(), _connection);
            if (MainForm.CountryTable.Rows.Count <= 0)
                adapter.Fill(MainForm.CountryTable);

            _citizen1BS = new BindingSource();
            _citizen1BS.DataSource = MainForm.CountryTable;
            _citizen2BS = new BindingSource();
            _citizen2BS.DataSource = MainForm.CountryTable;

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
            _classpercentView100BS.Filter = ClasspercentView.GetBindingSourceFilterFor100(DateTime.Parse(RepYear + "-01-01"));
            _classpercentView100BS.Sort = ClasspercentView.code;
            codeComboBox.DataSource = _classpercentView100BS;
            codeComboBox.ValueMember = ClasspercentView.id;
            codeComboBox.DisplayMember = ClasspercentView.code;

            //формирование источников для таблицы зарплат
            _salaryInfoTable = SalaryInfo.CreatetTable();
            if (idDoc != -1)
            {
                adapter = new SQLiteDataAdapter(SalaryInfo.GetSelectText(idDoc), _connection);
                adapter.Fill(_salaryInfoTable);
                _salaryInfoTable.Columns.Remove(SalaryInfo.id);
                _salaryInfoTable.Columns.Remove(SalaryInfo.docId);
                _salaryInfoTable = SalaryInfo.TransposeDataTable(_salaryInfoTable, 0);
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
            dataViewProfit.DataSource = _salaryInfoBS;

            if (isNew)
            {
                con = new SQLiteConnection(_connection);
                command = new SQLiteCommand(PersonView.GetSelectText(_org.idVal, personId), con);
                if (con.State != ConnectionState.Open)
                    con.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBoxAnketaName.Text = reader[PersonView.fio].ToString();
                    textBoxInsNum.Text = reader[PersonView.socNumber].ToString();
                    long citizen = (long)reader[PersonView.citizen1Id];
                    _citizen1BS.Position = _citizen1BS.Find(Country.id, citizen);
                    citizen = (long)reader[PersonView.citizen2Id];
                    _citizen2BS.Position = _citizen2BS.Find(Country.id, citizen);

                }
                reader.Close();
                con.Close();
            }
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

            currentPercent = (double)row[ClasspercentView.value];
            percentLabel.Text = (currentPercent * 100).ToString() + " %";

            if (int.Parse(row[ClasspercentView.obligatoryIsEnabled].ToString()) != 0)
                currentObligatory = ObligatoryPercent.GetValue(RepYear, _connection);
            else
                currentObligatory = 0;

            dataViewProfit_CellValidated(null, null);
        }

        private void addGeneralPeriodButton_Click(object sender, EventArgs e)
        {
            AddEditGeneralPeriodForm generalPeriodForm = new AddEditGeneralPeriodForm(RepYear);
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
            AddEditAdditionalPeriodForm additionalPeriodForm = new AddEditAdditionalPeriodForm(RepYear);
            if (additionalPeriodForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(additionalPeriodForm.Code.ToString());
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
            AddEditSpecialPeriodForm specialPeriodForm = new AddEditSpecialPeriodForm(RepYear);
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

            sumCalc3Box.Text = (sum[0] * currentPercent).ToString("N2");
            sumCalc5Box.Text = (sum[0] * currentObligatory).ToString("N2");

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
                    hintValue = Math.Round((double)(_salaryInfoBS.Current as DataRowView)["1"] * currentPercent, 2);
                    textBoxHint.Text = hintValue.ToString("N2");
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
                        hintValue = Math.Round((double)(_salaryInfoBS.Current as DataRowView)["1"] * currentObligatory, 2);
                        textBoxHint.Text = hintValue.ToString("N2");
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
    }
}
