using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class ReplaceDocTypeForm : Form
    {
        #region Поля

        // строка подключения к БД
        private string _connection;
        // таблица
        private DataTable _docTypesTable;
        // биндинг сорс для таблицы
        private BindingSource _docTypesBS;
        // адаптер для чтения данных из БД
        private SQLiteDataAdapter _docTypesAdapter;
        //переменнная указывает каким документам менять тип
        private int _flag;

        #endregion

        public ReplaceDocTypeForm()
        {
            InitializeComponent();
        }

        public ReplaceDocTypeForm(string connection)
            : this()
        {
            _connection = connection;
        }

        private void ReplaceDocTypeForm_Load(object sender, EventArgs e)
        {
            // инициализация таблиц
            _docTypesTable = DocTypes.CreatetTable();

            // инициализация биндинг сорса
            _docTypesBS = new BindingSource();
            _docTypesBS.DataSource = _docTypesTable;
            _docTypesBS.CurrentChanged += new EventHandler(_docTypesBS_CurrentChanged);

            //биндинг сорс как источник для выпадающего меню
            docTypesComboBox.DataSource = _docTypesBS;
            docTypesComboBox.ValueMember = DocTypes.id;
            docTypesComboBox.DisplayMember = DocTypes.name;

            //формирование строки запроса, где 2 - тип пакета из таблицы List_Types
            string commandStr = DocTypes.GetSelectText(2);

            //заполнение таблицы
            _docTypesAdapter = new SQLiteDataAdapter(commandStr, new SQLiteConnection(_connection));
            _docTypesAdapter.Fill(_docTypesTable);

            curDocRadioButton.Checked = true;
        }

        private void _docTypesBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
                return;
            StajDohodForm.NewDocTypeId = (long) row[DocTypes.id];
        }

        private void DocRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton current = (RadioButton) sender;
            if (current.Checked)
                switch (current.Name)
                {
                    case "curDocRadioButton":
                        _flag = 1;
                        break;
                    case "checkedDocsRadioButton":
                        _flag = 2;
                        break;
                    case "allDocsRadioButton":
                        _flag = 3;
                        break;
                    default:
                        _flag = 0;
                        break;
                }
            StajDohodForm.FlagDoc = _flag;
        }
    }
}