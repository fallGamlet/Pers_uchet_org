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
    public partial class MoveDocumentForm : Form
    {
        #region Поля
        // строка подключения к БД
        private string _connection;
        //id выбранного документа
        private long docId;
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

        public MoveDocumentForm()
        {
            InitializeComponent();
        }

        public MoveDocumentForm(Org _org, int RepYear, long docId, string _connection)
            : this()
        {
            this._org = _org;
            this.RepYear = RepYear;
            this.docId = docId;
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

            commandStr = DocsView.GetSelectTextByDocId(docId);
            SQLiteCommand cmd = new SQLiteCommand(commandStr, new SQLiteConnection(_connection));
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
                if (reader.Read())
                {
                    fioLabel.Text = reader[DocsView.fio].ToString();
                    regNumLabel.Text += " " + reader[DocsView.socNumber].ToString();
                }
            cmd.Connection.Close();
        }

        void _listsBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }
            StajDohodForm.newListId = (long)row[Lists.id];
        }
    }
}
