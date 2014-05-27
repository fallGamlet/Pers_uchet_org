using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class MoveDocumentForm : Form
    {
        #region Поля

        // строка подключения к БД
        private string _connection;
        // таблица
        private DataTable _listsTable;
        // биндинг сорс для таблицы
        private BindingSource _listsBS;
        // адаптер для чтения данных из БД
        private SQLiteDataAdapter _listsAdapter;
        //текущая организация
        private Org _org;
        //отчетный год
        private int _repYear;

        #endregion

        public MoveDocumentForm()
        {
            InitializeComponent();
        }

        public MoveDocumentForm(Org org, int repYear, string connection)
            : this()
        {
            _org = org;
            _repYear = repYear;
            _connection = connection;
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
            string commandStr = Lists.GetSelectText(_org.idVal, _repYear);

            //заполнение таблицы
            _listsAdapter = new SQLiteDataAdapter(commandStr, new SQLiteConnection(_connection));
            _listsAdapter.Fill(_listsTable);
        }

        private void _listsBS_CurrentChanged(object sender, EventArgs e)
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