using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class SearchIndividualForm : Form
    {
        #region Поля

        // строка подключения к БД
        private string _connection;
        // таблицы, для хранения данных
        private DataTable _personTable;
        private DataTable _orgTable;
        // контроллеры
        private BindingSource _personBS;
        private BindingSource _orgBS;
        // адаптеры
        private SQLiteDataAdapter _personAdapter;
        private SQLiteDataAdapter _orgAdapter;

        #endregion

        #region Конструктор и инициализатор

        public SearchIndividualForm(string connectionStr)
        {
            InitializeComponent();

            _connection = connectionStr;
        }

        private void SearchIndividualForm_Load(object sender, EventArgs e)
        {
            _personTable = PersonShortView.CreateTable();
            _orgTable = Org.CreateTable();

            _personBS = new BindingSource();
            _personBS.DataSource = _personTable;
            _personBS.CurrentChanged += new EventHandler(_personBS_CurrentChanged);
            _orgBS = new BindingSource();
            _orgBS.DataSource = _orgTable;

            personView.AutoGenerateColumns = false;
            personView.DataSource = _personBS;

            orgView.AutoGenerateColumns = false;
            orgView.DataSource = _orgBS;

            _personAdapter = new SQLiteDataAdapter("", _connection);
            _orgAdapter = new SQLiteDataAdapter("", _connection);
        }

        #endregion

        #region Методы - обработчики событий

        private void searchButton_Click(object sender, EventArgs e)
        {
            // получить скорректированный номер соц. страхования
            string socnum = PersonInfo.CorrectSocnumberRusToEn(socnumBox.Text.Trim().ToUpper());
            // получить имя
            string fname = fnameBox.Text.Trim();
            // получить отчество
            string mname = mnameBox.Text.Trim();
            // получить фамилию
            string lname = lnameBox.Text.Trim();

            if ((socnum.Length + fname.Length + mname.Length + lname.Length) == 0)
            {
                MainForm.ShowInfoMessage("Необходимо ввести хотябы одно значение для поиска!", "Внимание");
                return;
            }
            _personAdapter.SelectCommand.CommandText = PersonShortView.GetSelectText(socnum, fname, mname, lname);
            _personTable.Rows.Clear();
            _orgTable.Rows.Clear();
            _personAdapter.Fill(_personTable);
        }

        private void _personBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView curPerson = _personBS.Current as DataRowView;
            if (curPerson == null)
            {
                return;
            }
            _orgAdapter.SelectCommand.CommandText = Org.GetSelectByPersonText((long) curPerson[PersonShortView.id]);
            _orgTable.Rows.Clear();
            _orgAdapter.Fill(_orgTable);
        }

        #endregion
    }
}