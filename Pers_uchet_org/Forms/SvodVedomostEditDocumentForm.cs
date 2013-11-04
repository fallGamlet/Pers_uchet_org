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

            DataTable mergeInfoTable = MergeInfo.CreateTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(MergeInfo.GetSelectText(6), _connection);
            adapter.Fill(mergeInfoTable);
            MergeInfoTranspose.ConvertFromMergeInfo(_svodTable, mergeInfoTable);

        }
        #endregion

        #region Методы - свои
        #endregion

        #region Методы - обработчики событий
        private void printButton_Click(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        
    }
}
