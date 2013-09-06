using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

using System.Threading;

namespace Pers_uchet_org
{
    public partial class DoctypeForm : Form
    {
        #region Поля
        DataTable _doctypeTable; // таблица типов документов
        BindingSource _doctypeBS;                   // связыватель таблицы документов и просмотрщика (GridView)
        string _connection;
        delegate void MyUpdate();
        #endregion

        #region Конструкторы
        public DoctypeForm(string connectionStr)
        {
            InitializeComponent();

            _connection = connectionStr;
            _doctypeBS = new BindingSource();
            this.idoc_typeView.AutoGenerateColumns = false;
            this.idoc_typeView.DataSource = _doctypeBS;
        }
        #endregion

        #region Свойства
        public DataTable DoctypeTable
        {
            get { return this._doctypeTable; }
            set
            {
                this._doctypeTable = value;
                if (value != null && value.Rows.Count <= 0)
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(IDocType.GetSelectText(), _connection);
                    adapter.Fill(this._doctypeTable);
                }
                _doctypeBS.DataSource = this._doctypeTable;
            }
        }
        #endregion

        #region Свои методы

        
        #endregion

        #region Методы - обработчики событий
        #endregion
    }
}
