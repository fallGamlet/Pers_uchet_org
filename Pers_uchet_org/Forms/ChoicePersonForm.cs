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
        // идентификатор текущего пакета
        private long _listId;
        // таблица
        DataTable _personTable;
        // биндинг сорс для таблицы
        BindingSource _personBS;
        // адаптер для чтения данных из БД
        SQLiteDataAdapter _personAdapter;
        //переменнная содержит тип документа
        long flag = 0;
        #endregion

        public ChoicePersonForm()
        {
            InitializeComponent();
        }

        public ChoicePersonForm(Org _org, int RepYear, long listId, string _connection)
            : this()
        {
            this._org = _org;
            this.RepYear = RepYear;
            this._connection = _connection;
            this._listId = listId;
        }

        private void ChoicePersonForm_Load(object sender, EventArgs e)
        {
            try
            {
                // инициализация таблицы персон (записи с анкетными данными)
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

                StajDohodForm.PersonId = 0;
                allAnketsButton.Enabled = false;
                rawAnketsButton.Enabled = true;

                SQLiteConnection connection = new SQLiteConnection(_connection);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                long countPensDocsInList = Docs.CountDocsInList(_listId, DocTypes.GrantingPensionId, connection);
                long countOtherDocsInList = 0;
                countOtherDocsInList += Docs.CountDocsInList(_listId, DocTypes.InitialFormId, connection);
                countOtherDocsInList += Docs.CountDocsInList(_listId, DocTypes.CorrectionFormId, connection);
                countOtherDocsInList += Docs.CountDocsInList(_listId, DocTypes.CancelingFormId, connection);
                connection.Close();
                if (countPensDocsInList == 0 && countOtherDocsInList == 0)
                {
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = true;
                    radioButton3.Enabled = true;
                    radioButton4.Enabled = true;

                    radioButton1.Checked = true;
                }
                else if (countPensDocsInList > 0 && countOtherDocsInList == 0)
                {
                    radioButton1.Enabled = false;
                    radioButton2.Enabled = false;
                    radioButton3.Enabled = false;
                    radioButton4.Enabled = true;

                    radioButton4.Checked = true;
                }
                else
                    if (countPensDocsInList == 0 && countOtherDocsInList > 0)
                    {
                        radioButton1.Enabled = true;
                        radioButton2.Enabled = true;
                        radioButton3.Enabled = true;
                        radioButton4.Enabled = false;

                        radioButton1.Checked = true;
                    }
                    else if (countPensDocsInList > 0 && countOtherDocsInList > 0)
                    {
                        radioButton1.Enabled = false;
                        radioButton2.Enabled = false;
                        radioButton3.Enabled = false;
                        radioButton4.Enabled = false;

                        //radioButton1.Checked = true;
                    }

                ToolTip toolTip1 = new ToolTip();
                toolTip1.SetToolTip(radioButton1,"Создается для застрахованного лица, на которое данные еще не передавались в фонд");
                toolTip1.SetToolTip(radioButton2, "Создается для застрахованного лица, если данные уже сданы в фонд и требуют корректировки");
                toolTip1.SetToolTip(radioButton3, "Создается для застрахованного лица, если данные уже сданы в фонд и их необходимо отменить (удалить)");
                toolTip1.SetToolTip(radioButton4, "Документы этого типа должны находиться в отдельном пакете и сдаются в бумажном виде");
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка заполнения формы");
                choiceButton.Enabled = false;
            }
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
                        flag = DocTypes.InitialFormId;
                        break;
                    case "radioButton2":
                        flag = DocTypes.CorrectionFormId;
                        break;
                    case "radioButton3":
                        flag = DocTypes.CancelingFormId;
                        break;
                    case "radioButton4":
                        flag = DocTypes.GrantingPensionId;
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

            if (flag == 0)
            {
                MainForm.ShowInfoFlexMessage("В текущем пакете содержаться формы разных типов!\nДокументы типа \"Назначение пенсии\" должны находится в отдельном пакете!", "Ошибка выбора анкеты");
                return;
            }
            SQLiteConnection connection = new SQLiteConnection(_connection);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            long countDocsForPerson = Docs.CountDocsByYear(RepYear, (long)row[PersonView2.id], connection);
            connection.Close();

            if (countDocsForPerson > 0)
            {
                if (MainForm.ShowQuestionFlexMessage("За выбранный отчетный год уже имеются сведения\nо стаже и заработке по застрахованному лицу.\n\nВы действительно желаете ввести еще один документ?", "Ошибка выбора анкеты") == DialogResult.No)
                    return;
            }

            StajDohodForm.PersonId = (long)row[PersonView2.id];
            StajDohodForm.FlagDoc = flag;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void personView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            choiceButton_Click(sender, new EventArgs());
        }

        private void personView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (personView.CurrentCell != null)
                    choiceButton_Click(sender, new EventArgs());
            }
        }

    }
}
