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
    public partial class ReplaceDocTypeForm : Form
    {
        #region Поля
        // строка подключения к БД
        private string _connection;
        // таблица
        DataTable _docTypesTable;
        // биндинг сорс для таблицы
        BindingSource _docTypesBS;
        // адаптер для чтения данных из БД
        SQLiteDataAdapter _docTypesAdapter;
        //переменнная указывает каким документам менять тип
        int flag = 0;
        #endregion


        public ReplaceDocTypeForm()
        {
            InitializeComponent();
        }

        public ReplaceDocTypeForm(string _connection)
            : this()
        {
            this._connection = _connection;
        }

        private void ReplaceDocTypeForm_Load(object sender, EventArgs e)
        {
            // иництализация таблиц
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

        void _docTypesBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }
            StajDohodForm.newDocTypeId = (long)row[DocTypes.id];
        }

        private void DocRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton current = (RadioButton)sender;
            if (current.Checked)
                switch (current.Name)
                {
                    case "curDocRadioButton":
                        flag = 1;
                        break;
                    case "checkedDocsRadioButton":
                        flag = 2;
                        break;
                    case "allDocsRadioButton":
                        flag = 3;
                        break;
                    default:
                        flag = 0;
                        break;
                }
            StajDohodForm.flagDoc = flag;
        }
    }
}
