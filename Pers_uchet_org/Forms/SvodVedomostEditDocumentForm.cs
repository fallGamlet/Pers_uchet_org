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
    public partial class SvodVedomostEditDocumentForm : Form
    {
        #region Поля
        Org _org;
        Operator _oper;
        string _connection;
        DataRow _svodRow;
        DataTable _svodTable;
        BindingSource _svodBS;
        bool _isEditMode;
        #endregion

        #region Конструкторы и инициализатор
        public SvodVedomostEditDocumentForm(string connectionStr, Operator oper, Org org)
        {
            InitializeComponent();

            _connection = connectionStr;
            _oper = oper;
            _org = org;
            _svodRow = null;
            _isEditMode = false;
        }

        public SvodVedomostEditDocumentForm(string connectionStr, Operator oper, Org org, DataRow svodVedomostRow)
        {
            InitializeComponent();

            _connection = connectionStr;
            _oper = oper;
            _org = org;
            _svodRow = svodVedomostRow;
            _isEditMode = true;
        }
        
        private void SvodVedomostEditDocumentForm_Load(object sender, EventArgs e)
        {
            _svodBS = new BindingSource();
            _svodTable = MergeInfoTranspose.CreateTable();

            this.regnumBox.Text = _org.regnumVal;
            this.orgnameBox.Text = _org.nameVal;

            if (_svodRow == null)
            {
                this.packetcountBox.Value = 0;
                this.documentcountBox.Value = 0;
            }
            else
            {
                this.yearBox.Text = _svodRow[MergiesView.repYear].ToString();
                this.packetcountBox.Value = (int)_svodRow[MergiesView.listCount];
                this.documentcountBox.Value = (int)_svodRow[MergiesView.docCount];

                DataTable mergeInfoTable = MergeInfo.CreateTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(MergeInfo.GetSelectText((long)_svodRow[MergiesView.id]), _connection);
                adapter.Fill(mergeInfoTable);
                MergeInfoTranspose.ConvertFromMergeInfo(_svodTable, mergeInfoTable);
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
        private void SaveNew()
        {
            DataTable mergieInfo = MergeInfo.CreateTableWithRows();

            MergeInfoTranspose.ConvertToMergeInfo(_svodTable, mergieInfo);

        }

        private void SaveEdited()
        {

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

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (_svodRow == null)
            {
                this.SaveNew();
            }
            else
            {

            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        
    }
}
