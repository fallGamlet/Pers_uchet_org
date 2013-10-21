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
        public static int newRepYear = 0;
        // переменная содержит id организации, в которую будет перемещен пакет
        public static long newOrgId = 0;
        // переменная содержит новый тип для документа
        public static long newDocTypeId;
        // переменная-флаг, означает для каких документов менять тип: 1 - текущий документ, 2 - выбранные, 3 - все документы в пакете
        // или содержит id типа нового документа
        public static int flagDoc = -1;
        // переменная содержит id пакета, в который будет перемещен документ
        public static long newListId = 0;
        // переменная содержит id добавляемого человека
        public static long personId;


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

                if (_docsTable != null)
                    //очистка таблицы 
                    _docsTable.Clear();
                addDocButton.Enabled = false;
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

                addDocButton.Enabled = true;
            }

            //throw new NotImplementedException();
        }

        void _listsBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                return;
            }

            if (_docsTable != null)
                //очистка таблицы перед заполнением новыми данными
                _docsTable.Clear();
            else
                return;

            long listId = (long)row[ListsView.id];

            // инициализация Адаптера для считывания документов из БД
            string commandStr = DocsView.GetSelectTextByListId(listId);
            _docsAdapter = new SQLiteDataAdapter(commandStr, _connection);
            // заполнение таблицы данными из БД
            _docsAdapter.Fill(_docsTable);
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
            //Перезагрузка данных
            ReloadDataAfterChanges();
            _listsBS.MoveLast();
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
            long listId = (long)listsView.CurrentRow.Cells[Lists.id].Value;
            MovePacketForm movePacketForm = new MovePacketForm(_operator, _connection, listId);
            if (movePacketForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ShowQuestionMessage("Вы действительно хотите переместить текущий пакет\n документов \"СЗВ-1\" № " + listId + " и все документы в нём?", "Перемещение пакета") == DialogResult.No)
                    return;
                List<long> personIdList = new List<long>();
                long list_id = (long)listsView.CurrentRow.Cells[Lists.id].Value;
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
            ChoicePersonForm choicePersonForm = new ChoicePersonForm(_org, RepYear, _connection);
            if (choicePersonForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bool isNew = true;
                AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form(_org, RepYear, personId, flagDoc, _connection);
                if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                { }
            }
        }

        private void editDocButton_Click(object sender, EventArgs e)
        {
            AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form();
            if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void removeDocButton_Click(object sender, EventArgs e)
        {
            if (docView.CurrentRow == null)
                return;
            if (ShowQuestionMessage("Вы действительно хотите удалить текущий документ \"СЗВ-1\"?", "Удаление документа") == DialogResult.No)
                return;
            long doc_id = (long)docView.CurrentRow.Cells["id"].Value;
            string commandText = Docs.GetDeleteText(doc_id);
            SQLiteCommand cmd = new SQLiteCommand(commandText, new SQLiteConnection(_connection));
            cmd.Connection.Open();
            if (cmd.ExecuteNonQuery() < 1)
            {
                cmd.Connection.Close();
                ShowErrorMessage("Не удалось удалить документ.", "Ошибка удаления документа");
            }
            //Перезагрузка данных
            ReloadDataAfterChanges();
        }

        private void moveDocButton_Click(object sender, EventArgs e)
        {
            if (docView.CurrentRow == null)
                return;
            long docId = 0;
            DataRowView row = _docsBS.Current as DataRowView;
            if (row == null)
                return;
            docId = (long)row[Docs.id];
            MoveDocumentForm moveDocForm = new MoveDocumentForm(_org, RepYear, docId, _connection);
            if (moveDocForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ShowQuestionMessage("Вы действительно хотите переместить текущий документ \"СЗВ-1\"?", "Перемещение документа") == DialogResult.No)
                    return;

                if (Docs.UpdateListId(docId, newListId, _connection) < 1)
                {
                    ShowErrorMessage("Не удалось переместить документ.", "Ошибка перемещения документа");
                }
                //Перезагрузка данных
                ReloadDataAfterChanges();
            }
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
            if (docView.CurrentRow == null)
                return;
            ReplaceDocTypeForm replaceDocTypeForm = new ReplaceDocTypeForm(_connection);
            if (replaceDocTypeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    List<long> docIdList = new List<long>();
                    switch (flagDoc)
                    {
                        case 1:
                            DataRowView row = _docsBS.Current as DataRowView;
                            if (row != null)
                                docIdList.Add((long)row[Docs.id]);
                            Docs.UpdateDocTypeByDocId(docIdList, newDocTypeId, _connection);
                            break;
                        case 2:
                            docIdList = GetSelectedDocIds();
                            Docs.UpdateDocTypeByDocId(docIdList, newDocTypeId, _connection);
                            break;
                        case 3:
                            Docs.UpdateDocTypeByListId((long)listsView.CurrentRow.Cells["id"].Value, newDocTypeId, _connection);
                            break;
                        default:
                            throw new Exception("Не указан документ у которого необходимо изменить тип.");
                            return;
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("Не удалось изменить тип документа(ов).\n" + ex.Message, "Ошибка изменения типа документа(ов)");
                }

                //Перезагрузка данных
                ReloadDataAfterChanges();
            }
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

        private List<long> GetSelectedDocIds()
        {
            List<long> list = new List<long>();
            foreach (DataRowView docRow in _docsBS)
                if ((bool)docRow[CHECK])
                    list.Add((long)docRow["id"]);
            return list;
        }

        private void ReloadDataAfterChanges()
        {
            if (_listsBS == null)
                return;
            //отключение события, что б не мерцали кнопки при обновлении
            _listsBS.ListChanged -= new ListChangedEventHandler(_listsBS_ListChanged);

            if (_docsBS == null)
                return;
            //отключение события, что б не мерцали кнопки при обновлении
            _docsBS.ListChanged -= new ListChangedEventHandler(_docsBS_ListChanged);

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

            //добавление событий и их запуск
            _listsBS.ListChanged += new ListChangedEventHandler(_listsBS_ListChanged);
            _listsBS_ListChanged(null, new ListChangedEventArgs(ListChangedType.Reset, -1));
            _docsBS.ListChanged += new ListChangedEventHandler(_docsBS_ListChanged);
            _docsBS_ListChanged(null, new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        #endregion


    }
}
