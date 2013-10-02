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
    public partial class ChoicePersonForm : Form
    {


        #region Поля
        // строка подключения к БД
        string _connection;
        // текущая организация
        private Org _org;
        // текущий отчетный год
        private int RepYear;
        // таблица
        DataTable _personTable;
        // биндинг сорс для таблицы
        BindingSource _personBS;
        // адаптер для чтения данных из БД
        SQLiteDataAdapter _personAdapter;
        //переменнная содержит тип документа
        int flag = 0;
        #endregion

        public ChoicePersonForm()
        {
            InitializeComponent();
        }

        public ChoicePersonForm(Org _org, int RepYear, string _connection)
            : this()
        {
            this._org = _org;
            this.RepYear = RepYear;
            this._connection = _connection;
        }

        private void ChoicePersonForm_Load(object sender, EventArgs e)
        {
            // иництализация таблицы персон (записи с анкетными данными)
            _personTable = PersonView2.CreatetTable();

            // инициализация биндинг сорса к таблице персон
            _personBS = new BindingSource();
            _personBS.CurrentChanged += new EventHandler(_personBS_CurrentChanged);
            _personBS.ListChanged += new ListChangedEventHandler(_personBS_ListChanged);
            _personBS.DataSource = _personTable;

            // присвоение источника вьюшке
            this.personView.AutoGenerateColumns = false;
            this.personView.DataSource = _personBS;

            // инициализация Адаптера для считывания персон из БД
            string commandStr = PersonView2.GetSelectText(_org.idVal, RepYear);
            _personAdapter = new SQLiteDataAdapter(commandStr, _connection);
            // запосление таблицы данными с БД
            _personAdapter.Fill(_personTable);

            allAnketsButton.Enabled = false;
            rawAnketsButton.Enabled = true;
            radioButton1.Checked = true;
            StajDohodForm.personId = 0;
        }

        void _personBS_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.countBox.Text = _personBS.Count.ToString();
            if (_personBS.Count < 1)
                choiceButton.Enabled = false;
            else
            {
                choiceButton.Enabled = true;
            }
        }

        void _personBS_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void allAnketsButton_Click(object sender, EventArgs e)
        {
            // очистка таблицы персон (записи с анкетными данными)
            _personTable.Clear();
            // отключаем фильтр
            searchFioTextBox.Text = "";
            searchNumTextBox.Text = "";
            _personBS.Filter = "";

            // инициализация Адаптера для считывания персон из БД
            string commandStr = PersonView2.GetSelectText(_org.idVal, RepYear);
            _personAdapter = new SQLiteDataAdapter(commandStr, _connection);
            // запосление таблицы данными с БД
            _personAdapter.Fill(_personTable);

            allAnketsButton.Enabled = false;
            rawAnketsButton.Enabled = true;
        }

        private void rawAnketsButton_Click(object sender, EventArgs e)
        {
            // очистка таблицы персон (записи с анкетными данными)
            _personTable.Clear();
            
            // отключаем фильтр
            searchFioTextBox.Text = "";
            searchNumTextBox.Text = "";
            _personBS.Filter = "";

            // инициализация Адаптера для считывания персон из БД
            string commandStr = PersonView2.GetSelectRawPersonsText(_org.idVal, RepYear);
            _personAdapter = new SQLiteDataAdapter(commandStr, _connection);
            // запосление таблицы данными с БД
            _personAdapter.Fill(_personTable);

            allAnketsButton.Enabled = true;
            rawAnketsButton.Enabled = false;
        }

        private void searchNumTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                searchFioTextBox.Text = "";
                _personBS.Filter = string.Format("{0} like '%{1}%'", PersonView.socNumber, this.searchNumTextBox.Text);
            }
        }

        private void searchFioTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                searchNumTextBox.Text = "";
                _personBS.Filter = string.Format("{0} like '%{1}%'", PersonView.fio, this.searchFioTextBox.Text);
            }
        }

        private void DocRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton current = (RadioButton)sender;
            if (current.Checked)
                switch (current.Name)
                {
                    case "radioButton1":
                        flag = 21;
                        break;
                    case "radioButton2":
                        flag = 22;
                        break;
                    case "radioButton3":
                        flag = 23;
                        break;
                    case "radioButton4":
                        flag = 24;
                        break;
                    default:
                        flag = 0;
                        break;
                }
        }

        private void choiceButton_Click(object sender, EventArgs e)
        {
            DataRowView row = _personBS.Current as DataRowView;
            if (row == null)
            {
                MainForm.ShowWarningMessage("Не выбрана ни одна анкета.", "Ошибка выбора анкеты");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                return;
            }
            StajDohodForm.personId = (long)row[PersonView2.id];
            StajDohodForm.flagDoc = flag;
        }
    }
}
