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
    public partial class StajDohodForm : Form
    {
        #region Поля
        // строка подключения к БД
        string _connection;
        // активный оператор
        Operator _operator;
        // активная организация
        Org _org;
        // привилегия
        string _privilege;
        // таблица пакетов
        DataTable _listsTable;
        // таблица документов
        DataTable _docsTable;
        // биндинг сорс для таблиц
        BindingSource _listsBS;
        BindingSource _docsBS;
        // адаптер для чтения данных из БД
        SQLiteDataAdapter _listsAdapter;
        SQLiteDataAdapter _docsAdapter;
        // названия добавочного виртуального столбца
        const string CHECK = "check";
        // переменная содержит текущий используемый год
        int RepYear;
        // переменная содержит год, в который будет перемещен пакет
        public static int newRepYear;
        // переменная содержит id организации, в которую будет перемещен пакет
        public static long newOrgId;

        #endregion

        #region Конструктор и инициализатор
        public StajDohodForm(Operator oper, Org org, string connection)
        {
            InitializeComponent();
            _operator = oper;
            _org = org;
            _connection = connection;
        }

        private void StajDohodForm_Load(object sender, EventArgs e)
        {
            RepYear = MainForm.RepYear;
            yearBox.Maximum = RepYear + 10;
            yearBox.Value = RepYear;

            // иництализация таблиц
            _listsTable = ListsView.CreatetTable();
            _docsTable = DocsView.CreatetTable();
            // добавление виртуального столбца для возможности отмечать записи
            _docsTable.Columns.Add(CHECK, typeof(bool));
            _docsTable.Columns[CHECK].DefaultValue = false;

            // инициализация биндинг сорса к таблице пакетов
            _listsBS = new BindingSource();
            _listsBS.CurrentChanged += new EventHandler(_listsBS_CurrentChanged);
            _listsBS.ListChanged += new ListChangedEventHandler(_listsBS_ListChanged);
            _listsBS.DataSource = _listsTable;

            // инициализация биндинг сорса к таблице документов
            _docsBS = new BindingSource();
            _docsBS.CurrentChanged += new EventHandler(_docsBS_CurrentChanged);
            _docsBS.ListChanged += new ListChangedEventHandler(_docsBS_ListChanged);
            _docsBS.DataSource = _docsTable;

            // присвоение источника dataGrid
            this.listsView.AutoGenerateColumns = false;
            this.listsView.DataSource = _listsBS;
            this.docView.AutoGenerateColumns = false;
            this.docView.DataSource = _docsBS;

            // инициализация Адаптера для считывания пакетов из БД
            string commandStr = ListsView.GetSelectText(_org.idVal, RepYear);
            _listsAdapter = new SQLiteDataAdapter(commandStr, _connection);

            // заполнение таблицы данными с БД
            _listsAdapter.Fill(_listsTable);

            // получить код привилегии (уровня доступа) Оператора к Организации
            if (_operator.IsAdmin())
                _privilege = OperatorOrg.GetPrivilegeForAdmin();
            else
                _privilege = OperatorOrg.GetPrivilege(_operator.idVal, _org.idVal, _connection);

            //TODO: отобразить привилегию на форме для пользователя
            //this.SetPrivilege(_privilege);
        }

        private void StajDohodForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.RepYear = RepYear;
        }
        #endregion

        #region Методы - обработчики событий
        void _listsBS_ListChanged(object sender, ListChangedEventArgs e)
        {
            packetcountBox.Text = _listsBS.Count.ToString();
            if (_listsBS.Count < 1)
            {
                removeButton.Enabled = false;
                movePacketOrgButton.Enabled = false;
                movePacketYearButton.Enabled = false;
                reestrButton.Enabled = false;
                calculateButton.Enabled = false;
                printButton.Enabled = false;
                return;
            }
            else
            {
                removeButton.Enabled = true;
                movePacketOrgButton.Enabled = true;
                movePacketYearButton.Enabled = true;
                reestrButton.Enabled = true;
                calculateButton.Enabled = true;
                printButton.Enabled = true;
            }

            //throw new NotImplementedException();
        }

        void _listsBS_CurrentChanged(object sender, EventArgs e)
        {
            if (_docsTable != null)
                //очистка таблицы перед заполнением новыми данными
                _docsTable.Clear();
            else
                return;

            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }

            long listId = (long)row[ListsView.id];

            // инициализация Адаптера для считывания документов из БД
            string commandStr = DocsView.GetSelectText(listId);
            _docsAdapter = new SQLiteDataAdapter(commandStr, _connection);

            // заполнение таблицы данными из БД
            _docsAdapter.Fill(_docsTable);
            //throw new NotImplementedException();
        }

        void _docsBS_ListChanged(object sender, ListChangedEventArgs e)
        {
            docCountBox.Text = _docsBS.Count.ToString();
            if (_docsBS.Count < 1)
            {
                removeDocButton.Enabled = false;
                editDocButton.Enabled = false;
                moveDocButton.Enabled = false;
                printFormButton.Enabled = false;
                changeTypedDocButton.Enabled = false;
                return;
            }
            else
            {
                removeDocButton.Enabled = true;
                editDocButton.Enabled = true;
                moveDocButton.Enabled = true;
                printFormButton.Enabled = true;
                changeTypedDocButton.Enabled = true;
            }
        }

        void _docsBS_CurrentChanged(object sender, EventArgs e)
        {
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            if (ShowQuestionMessage("Вы действительно хотите создать\n новый пакет документов \"СЗВ-1\"?", "Создание пакета") == DialogResult.No)
                return;
            //TODO: Загружать список доступных типов пакета для текущего года, спрашивать пользователя 
            string commandText = Lists.GetInsertText(1, _org.idVal, _operator.idVal, string.Format("{0:dd/MM/yyyy}", DateTime.Now), _operator.idVal, string.Format("{0:dd/MM/yyyy}", DateTime.Now), RepYear);
            SQLiteCommand cmd = new SQLiteCommand(commandText, new SQLiteConnection(_connection));
            cmd.Connection.Open();
            if (cmd.ExecuteNonQuery() < 1)
            {
                cmd.Connection.Close();
                ShowErrorMessage("Не удалось создать новый пакет.", "Ошибка создания пакета");
                return;
            }
            //Сохраняем текущую позицию в listsView, для возвращения в неё после перезагрузки данных
            long currentListId = -1;
            if (listsView.CurrentRow != null)
            {
                currentListId = (long)listsView.CurrentRow.Cells["id"].Value;
            }
            //Перезагрузка данных
            ReloadDataAfterChanges();
            //Возвращаемся в позицию в которой были до перезагрузки данных
            int index = _listsBS.Find("id", currentListId);
            if (index > 0)
                _listsBS.Position = index;

        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (listsView.CurrentRow == null)
                return;
            if (ShowQuestionMessage("Вы действительно хотите удалить текущий пакет\n документов \"СЗВ-1\" и все документы в нём?", "Удаление пакета") == DialogResult.No)
                return;

            string commandText = Lists.GetDeleteText((long)listsView.CurrentRow.Cells["id"].Value);
            SQLiteCommand cmd = new SQLiteCommand(commandText, new SQLiteConnection(_connection));
            cmd.Connection.Open();
            if (cmd.ExecuteNonQuery() < 1)
            {
                cmd.Connection.Close();
                ShowErrorMessage("Не удалось удалить пакет.", "Ошибка удаления пакета");
            }
            //Перезагрузка данных
            ReloadDataAfterChanges();
        }

        private void movePacketOrgButton_Click(object sender, EventArgs e)
        {
            if (listsView.CurrentRow == null)
                return;
            long listId = (long)listsView.CurrentRow.Cells["id"].Value;
            MovePacketForm movePacketForm = new MovePacketForm(_operator, _connection, listId);
            if (movePacketForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ShowQuestionMessage("Вы действительно хотите переместить текущий пакет\n документов \"СЗВ-1\" № " + listId + " и все документы в нём?", "Перемещение пакета") == DialogResult.No)
                    return;
                List<long> personIdList = new List<long>();
                long list_id = (long)listsView.CurrentRow.Cells["id"].Value;
                string commandText = Lists.GetSelectPersonIdsText(list_id);
                SQLiteCommand cmd = new SQLiteCommand(commandText, new SQLiteConnection(_connection));
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        personIdList.Add((long)reader[Docs.personID]);
                    }
                cmd.Connection.Close();
                PersonOrg.InsertPersonOrg(personIdList, newOrgId, _connection);
                commandText = Lists.GetUpdateOrgText(list_id, newOrgId);
                cmd = new SQLiteCommand(commandText, new SQLiteConnection(_connection));
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                if (cmd.ExecuteNonQuery() < 1)
                {
                    cmd.Connection.Close();
                    ShowErrorMessage("Не удалось переместить пакет.", "Ошибка перемещения пакета");
                }
                //Перезагрузка данных
                ReloadDataAfterChanges();
            }
        }

        private void movePacketYearButton_Click(object sender, EventArgs e)
        {
            if (listsView.CurrentRow == null)
                return;
            long listId = (long)listsView.CurrentRow.Cells["id"].Value;
            MovePacketOtherYearForm movePacketOtherYear = new MovePacketOtherYearForm(listId);
            if (movePacketOtherYear.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ShowQuestionMessage("Вы действительно хотите переместить текущий пакет\n документов \"СЗВ-1\" № " + listId + " и все документы в нём?", "Перемещение пакета") == DialogResult.No)
                    return;

                string commandText = Lists.GetUpdateYearText(listId, newRepYear);
                SQLiteCommand cmd = new SQLiteCommand(commandText, new SQLiteConnection(_connection));
                cmd.Connection.Open();
                if (cmd.ExecuteNonQuery() < 1)
                {
                    cmd.Connection.Close();
                    ShowErrorMessage("Не удалось переместить пакет.", "Ошибка перемещения пакета");
                    //return;
                }
                //Перезагрузка данных
                ReloadDataAfterChanges();
            }
        }

        private void addDocButton_Click(object sender, EventArgs e)
        {
            ChoicePersonForm choicePersonForm = new ChoicePersonForm();
            if (choicePersonForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void editDocButton_Click(object sender, EventArgs e)
        {
            AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form();
            if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void removeDocButton_Click(object sender, EventArgs e)
        {

        }

        private void moveDocButton_Click(object sender, EventArgs e)
        {
            MoveDocumentForm moveDocForm = new MoveDocumentForm();
            if (moveDocForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void packetView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            PacketDopInfoForm packetDopInfoForm = new PacketDopInfoForm();
            if (packetDopInfoForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void documentView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form();
            if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void changeTypedDocButton_Click(object sender, EventArgs e)
        {
            ReplaceDocTypeForm replaceDocTypeForm = new ReplaceDocTypeForm();
            if (replaceDocTypeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void printFormButton_Click(object sender, EventArgs e)
        {
            PrintStajForm printStajForm = new PrintStajForm();
            if (printStajForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            PacketDopInfoForm packetDopInfoForm = new PacketDopInfoForm();
            if (packetDopInfoForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void yearBox_ValueChanged(object sender, EventArgs e)
        {
            RepYear = (int)yearBox.Value;
            MainForm.RepYear = RepYear;
            ReloadDataAfterChanges();
        }
        #endregion

        #region Методы - свои
        static public DialogResult ShowQuestionMessage(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        static public void ShowErrorMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ReloadDataAfterChanges()
        {
            if (_listsTable != null)
                //очистка таблицы перед заполнением новыми данными
                _listsTable.Clear();
            else
                return;

            // инициализация Адаптера для считывания пакетов из БД
            string commandStr = ListsView.GetSelectText(_org.idVal, RepYear);
            _listsAdapter = new SQLiteDataAdapter(commandStr, _connection);

            // заполнение таблицы данными с БД
            _listsAdapter.Fill(_listsTable);
        }
        #endregion

       
    }
}
