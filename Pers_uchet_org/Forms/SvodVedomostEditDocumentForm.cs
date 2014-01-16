using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Linq;
using System.IO;

namespace Pers_uchet_org
{
    public partial class SvodVedomostEditDocumentForm : Form
    {
        #region Поля
        Org _org;                  // организация
        Operator _oper;            // оператор
        string _connection;        // строка соединения с БД
        DataRow _mergeRow;         // строка Сводной ведомости
        DataTable _svodTable;      // таблица для взаимодействия с формой
        DataTable _mergeInfoTable; // таблица для взаимодействия с БД
        BindingSource _svodBS;     // бинд для таблицы для отображения в пользовательском интерфейсе
        #endregion

        #region Конструкторы и инициализатор
        public SvodVedomostEditDocumentForm(string connectionStr, Operator oper, Org org)
        {
            InitializeComponent();

            _connection = connectionStr;
            _oper = oper;
            _org = org;
            _mergeRow = null;
        }

        public SvodVedomostEditDocumentForm(string connectionStr, Operator oper, Org org, DataRow svodVedomostRow)
        {
            InitializeComponent();

            _connection = connectionStr;
            _oper = oper;
            _org = org;
            _mergeRow = svodVedomostRow;
        }

        private void SvodVedomostEditDocumentForm_Load(object sender, EventArgs e)
        {
            _svodBS = new BindingSource();
            _svodTable = MergeInfoTranspose.CreateTable();

            this.regnumBox.Text = _org.regnumVal;
            this.orgnameBox.Text = _org.nameVal;

            if (_mergeRow == null)
            {
                this.packetcountBox.Value = 0;
                this.documentcountBox.Value = 0;
                _mergeInfoTable = null;
            }
            else
            {
                this.yearBox.Text = _mergeRow[MergiesView.repYear].ToString();
                this.packetcountBox.Value = (int)_mergeRow[MergiesView.listCount];
                this.documentcountBox.Value = (int)_mergeRow[MergiesView.docCount];

                _mergeInfoTable = MergeInfo.CreateTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(MergeInfo.GetSelectText((long)_mergeRow[MergiesView.id]), _connection);
                adapter.Fill(_mergeInfoTable);
                MergeInfoTranspose.ConvertFromMergeInfo(_svodTable, _mergeInfoTable);

                this.sum1Box.Text = MergeInfo.GetSum(_mergeInfoTable, SalaryGroups.Column1).ToString("N2");
                this.sum2Box.Text = MergeInfo.GetSum(_mergeInfoTable, SalaryGroups.Column2).ToString("N2");
                this.sum3Box.Text = MergeInfo.GetSum(_mergeInfoTable, SalaryGroups.Column3).ToString("N2");
                this.sum4Box.Text = MergeInfo.GetSum(_mergeInfoTable, SalaryGroups.Column4).ToString("N2");
                this.sum5Box.Text = MergeInfo.GetSum(_mergeInfoTable, SalaryGroups.Column5).ToString("N2");

                if (!(bool)_mergeRow[MergiesView.actual])
                {
                    this.packetcountBox.Enabled = false;
                    this.documentcountBox.Enabled = false;
                    this.dataView.ReadOnly = true;
                    this.saveButton.Enabled = false;
                    this.autofillButton.Enabled = false;
                }
            }
            _svodBS.DataSource = _svodTable;
            this.dataView.AutoGenerateColumns = false;
            this.dataView.DataSource = _svodBS;

            _svodTable.ColumnChanged += new DataColumnChangeEventHandler(_svodTable_ColumnChanged);
            this.dataView.CellParsing += new DataGridViewCellParsingEventHandler(dataView_CellParsing);
            this.dataView.DataError += new DataGridViewDataErrorEventHandler(dataView_DataError);
        }
        #endregion

        #region Свойства
        public int RepYear
        {
            get { return int.Parse(this.yearBox.Text); }
            set { this.yearBox.Text = value.ToString(); }
        }
        #endregion

        #region Методы - свои
        /// <summary>
        /// Производит заполнение записи сводной ведомости из полей формы
        /// </summary>
        private void SetMergeData()
        {
            if (_mergeRow == null)
            {
                _mergeRow = MergiesView.CreateTable().NewRow();
                _mergeRow[MergiesView.newDate] = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                _mergeRow[MergiesView.id] = -1;
            }
            _mergeRow[MergiesView.listCount] = (int)this.packetcountBox.Value;
            _mergeRow[MergiesView.docCount] = (int)this.documentcountBox.Value;
            _mergeRow[MergiesView.repYear] = int.Parse(this.yearBox.Text);
            _mergeRow[MergiesView.orgID] = _org.idVal;
            _mergeRow[MergiesView.operName] = _oper.nameVal;
            _mergeRow[MergiesView.editDate] = DateTime.Now;
            _mergeRow.EndEdit();
        }
        /// <summary>
        /// Производит сохранение данных в БД созданной сводной ведомости
        /// </summary>
        private void SaveNew()
        {
            // создать виртуальную таблицу с пустыми записями
            _mergeInfoTable = MergeInfo.CreateTableWithRows();
            // скопировать данные из транспонированной таблицы в созданную
            MergeInfoTranspose.ConvertToMergeInfo(_svodTable, _mergeInfoTable);
            // просчитать суммы в созданной таблицйе
            MergeInfo.MathSums(_mergeInfoTable);
            // заполнение данными записи Сводной ведомости
            this.SetMergeData();
            // создание подключения к БД
            SQLiteConnection connection = new SQLiteConnection(_connection);
            // создание команд для вставки данных в БД
            SQLiteCommand mergeInsert = MergiesView.InsertCommand(_mergeRow);
            SQLiteCommand tableInsert = MergeInfo.CreateInsertCommand();
            SQLiteCommand fixdataReplace = new SQLiteCommand();
            SQLiteCommand setUnactual = new SQLiteCommand(MergiesView.GetChangeActualByOrgText(_org.idVal, (int)_mergeRow[MergiesView.repYear], false));
            // создание Адаптера для обработки таблицы
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.InsertCommand = tableInsert;
            // присвоение созданного подключения коммандам
            mergeInsert.Connection =
            tableInsert.Connection =
            fixdataReplace.Connection = 
            setUnactual.Connection = connection;
            // открытие соединения
            connection.Open();
            // начать транзакция
            SQLiteTransaction transaction = connection.BeginTransaction();
            // прикрепление транзакции 
            mergeInsert.Transaction =
            tableInsert.Transaction =
            fixdataReplace.Transaction =
            setUnactual.Transaction = transaction;
            // выполнить команду обнуления актуальности сводных ведомостей в выбранном году для текущей организаций
            setUnactual.ExecuteNonQuery();
            // выполнить команду вставки записи о новой сводной ведомости и назначения ее актуальной
            _mergeRow[MergiesView.id] = mergeInsert.ExecuteScalar();
            _mergeRow.EndEdit();
            // внести запись о факте создания записи сводной     ведомости
            fixdataReplace.CommandText = MergiesView.GetReplaceFixDataText(_mergeRow, FixData.FixType.New);
            fixdataReplace.ExecuteNonQuery();
            // заполнить соответствующий столбец таблицы ID сводной ведомости для их привязки к ней (сводной ведомости)
            MergeInfo.SetMergeID(_mergeInfoTable, (long)_mergeRow[MergiesView.id]);
            // ввыполнить вставку записей из таблицы программы в таблицу БД
            adapter.Update(_mergeInfoTable);
            // принять подключение
            transaction.Commit();
            // закрыть соединение
            connection.Close();
        }
        /// <summary>
        /// Производит сохранение данных в БД измененной сводной ведомости
        /// </summary>
        private void SaveEdited()
        {
            // скопировать данные с транспонированной таблицы в таблицу, соответствующую по структуре с таблицей БД
            MergeInfoTranspose.ConvertToMergeInfo(_svodTable, _mergeInfoTable);
            // просчитать суммы в созданной таблицйе
            MergeInfo.MathSums(_mergeInfoTable);
            // заполнение данными записи Сводной ведомости
            this.SetMergeData();
            // создание подключения к БД
            SQLiteConnection connection = new SQLiteConnection(_connection);
            // создание команд для вставки данных в БД
            SQLiteCommand mergeInsert = MergiesView.UpdateCommand(_mergeRow);
            SQLiteCommand tableInsert = MergeInfo.CreateUpdateCommand();
            SQLiteCommand fixdataReplace = new SQLiteCommand();
            // создание Адаптера для обработки таблицы
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.UpdateCommand = tableInsert;
            // присвоение созданного подключения коммандам
            mergeInsert.Connection =
            tableInsert.Connection =
            fixdataReplace.Connection = connection;
            // открытие соединения
            connection.Open();
            // начать транзакция
            SQLiteTransaction transaction = connection.BeginTransaction();
            // прикрепление транзакции 
            mergeInsert.Transaction =
            tableInsert.Transaction =
            fixdataReplace.Transaction = transaction;
            // выполнить обновление данных о сводной ведомости
            mergeInsert.ExecuteNonQuery();
            // выполнить вставку(обновление) данных о факте изменения сводной ведомости
            fixdataReplace.CommandText = MergiesView.GetReplaceFixDataText(_mergeRow, FixData.FixType.Edit);
            fixdataReplace.ExecuteNonQuery();
            // выполнить обносление данных по значениям выплат
            adapter.Update(_mergeInfoTable);
            // принять подключение
            transaction.Commit();
            // закрыть соединение
            connection.Close();
        }
        #endregion

        #region Методы - обработчики событий
        void dataView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            string valStr = e.Value as string;
            double valD;
            if (!double.TryParse(valStr, out valD))
            {
                valStr = valStr.Replace('.', ',');
                if (!double.TryParse(valStr, out valD))
                {
                    e.ParsingApplied = false;
                    return;
                }
            }
            if (Column6.Index == e.ColumnIndex)
                e.Value = (int)Math.Round(valD, 0);
            else
                e.Value = Math.Round(valD, 2);
            e.ParsingApplied = true;
        }

        void dataView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MainForm.ShowWarningMessage("Введены некорректные данные!", "Внимание");
        }

        void _svodTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {

            TextBox tmpSumBox = null;

            if (e.Column.ColumnName == MergeInfoTranspose.col1)
            {
                tmpSumBox = this.sum1Box;
            }
            else if (e.Column.ColumnName == MergeInfoTranspose.col2)
            {
                tmpSumBox = this.sum2Box;
            }
            else if (e.Column.ColumnName == MergeInfoTranspose.col3)
            {
                tmpSumBox = this.sum3Box;
            }
            else if (e.Column.ColumnName == MergeInfoTranspose.col4)
            {
                tmpSumBox = this.sum4Box;
            }
            else if (e.Column.ColumnName == MergeInfoTranspose.col5)
            {
                tmpSumBox = this.sum5Box;
            }
            if (tmpSumBox == null)
                return;
            double sum = 0.0;
            foreach (DataRow row in _svodTable.Rows)
            {
                sum += (double)row[e.Column];
            }
            tmpSumBox.Text = Math.Round(sum, 2).ToString("N2");
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            DataRow mergeRow = _mergeRow == null ? Mergies.CreateRow() : _mergeRow;
            //XmlDocument xml = Szv3Xml.GetXml(mergeRow, _svodTable);
            XmlDocument xml = new XmlDocument();
            MyPrinter.ShowWebPage(Szv3Xml.GetHTML(xml));
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (_mergeRow == null)
            {
                this.SaveNew();
            }
            else
            {
                this.SaveEdited();
            }
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void autofillButton_Click(object sender, EventArgs e)
        {
            Forms.SvodVedomostGetPacketsForm tmpForm = new Forms.SvodVedomostGetPacketsForm(_org, this.RepYear, _connection);
            DialogResult dRes = tmpForm.ShowDialog(this);
            if (dRes == DialogResult.OK)
            {
                long[] markedPacked = tmpForm.MarckedPackets;
                long[] doctypes = { 21, 22, 24 };
                DataTable salaryInfoTranspose = SalaryInfoTranspose.CreateTableWithRows();
                if (markedPacked.Length > 0)
                {
                    DataTable salaryInfoTable = SalaryInfo.CreateTable();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(SalaryInfo.GetSelectText(markedPacked, doctypes), _connection);
                    adapter.Fill(salaryInfoTable);
                    SalaryInfoTranspose.ConvertFromSalaryInfo(salaryInfoTranspose, salaryInfoTable);
                    this.packetcountBox.Value = markedPacked.Length;
                    this.documentcountBox.Value = (long)salaryInfoTable.Rows[0][SalaryInfo.docId];
                }
                else
                {
                    this.packetcountBox.Value = 0;
                    this.documentcountBox.Value = 0;
                }
                int i = 0;
                for (i = 0; i < 12; i++)
                {
                    _svodTable.Rows[i][SalaryGroups.Column1] = salaryInfoTranspose.Rows[i][SalaryGroups.Column1];
                    _svodTable.Rows[i][SalaryGroups.Column2] = salaryInfoTranspose.Rows[i][SalaryGroups.Column2];
                    _svodTable.Rows[i][SalaryGroups.Column3] = salaryInfoTranspose.Rows[i][SalaryGroups.Column3];
                    _svodTable.Rows[i][SalaryGroups.Column4] = salaryInfoTranspose.Rows[i][SalaryGroups.Column4];
                    _svodTable.Rows[i][SalaryGroups.Column5] = salaryInfoTranspose.Rows[i][SalaryGroups.Column5];
                    _svodTable.Rows[i].EndEdit();
                    _svodTable.AcceptChanges();
                }
            }
        }
        #endregion
    }
}
