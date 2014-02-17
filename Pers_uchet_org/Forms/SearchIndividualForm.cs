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
    public partial class SearchIndividualForm : Form
    {
        #region Поля
        // строка подключения к БД
        string _connection;
        // таблицы, для хранения данных
        DataTable _personTable;
        DataTable _orgTable;
        // контроллеры
        BindingSource _personBS;
        BindingSource _orgBS;
        // адаптеры
        SQLiteDataAdapter _personAdapter;
        SQLiteDataAdapter _orgAdapter;
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

            this.personView.AutoGenerateColumns = false;
            this.personView.DataSource = _personBS;

            this.orgView.AutoGenerateColumns = false;
            this.orgView.DataSource = _orgBS;

            _personAdapter = new SQLiteDataAdapter("", _connection);
            _orgAdapter = new SQLiteDataAdapter("", _connection);
        }

        #endregion

        #region Методы - обработчики событий
        private void searchButton_Click(object sender, EventArgs e)
        {
            string socnum, fname, mname, lname;
            // получить скорректированный номер соц. страхования
            socnum = PersonInfo.CorrectSocnumberRusToEn(this.socnumBox.Text.Trim());
            // получить имя
            fname = this.fnameBox.Text.Trim();
            // получить отчество
            mname = this.mnameBox.Text.Trim();
            // получить фамилию
            lname = this.lnameBox.Text.Trim();

            if ((socnum.Length + fname.Length + mname.Length + lname.Length) == 0)
            {
                MainForm.ShowInfoMessage("Необходимо ввести хотябы одно значение для поиска!", "Внимание");
                return;
            }
            _personAdapter.SelectCommand.CommandText = PersonShortView.GetSelectText(socnum, fname, mname, lname);
            _personTable.Rows.Clear();
            _personAdapter.Fill(_personTable);
        }

        void _personBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView curPerson = _personBS.Current as DataRowView;
            if (curPerson == null)
            {
                return;
            }
            _orgAdapter.SelectCommand.CommandText = Org.GetSelectByPersonText((long)curPerson[PersonShortView.id]);
            _orgTable.Rows.Clear();
            _orgAdapter.Fill(_orgTable);
        }
        #endregion
    }
}
