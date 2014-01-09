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
    public partial class CopyDocumentForm : Form
    {
        #region Поля
        // строка подключения к БД
        private string _connection;
        // таблица
        DataTable _listsTable;
        // биндинг сорс для таблицы
        BindingSource _listsBS;
        // адаптер для чтения данных из БД
        SQLiteDataAdapter _listsAdapter;
        //текущая организация
        private Org _org;
        //отчетный год
        private int RepYear;
        #endregion

        public CopyDocumentForm()
        {
            InitializeComponent();
        }

        public CopyDocumentForm(Org _org, int RepYear, string _connection)
            : this()
        {
            this._org = _org;
            this.RepYear = RepYear;
            this._connection = _connection;
        }

        private void MoveDocumentForm_Load(object sender, EventArgs e)
        {
            // иництализация таблиц
            _listsTable = Lists.CreatetTable();

            // инициализация биндинг сорса
            _listsBS = new BindingSource();
            _listsBS.DataSource = _listsTable;
            _listsBS.CurrentChanged += new EventHandler(_listsBS_CurrentChanged);

            //биндинг сорс как источник для выпадающего меню
            listsComboBox.DataSource = _listsBS;
            listsComboBox.ValueMember = Lists.id;
            listsComboBox.DisplayMember = Lists.id;

            //формирование строки запроса
            string commandStr = Lists.GetSelectText(_org.idVal, RepYear);

            //заполнение таблицы
            _listsAdapter = new SQLiteDataAdapter(commandStr, new SQLiteConnection(_connection));
            _listsAdapter.Fill(_listsTable);
        }

        void _listsBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }
            StajDohodForm.NewListId = (long)row[Lists.id];
        }
    }
}
