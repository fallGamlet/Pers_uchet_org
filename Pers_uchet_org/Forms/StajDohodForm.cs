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
        Org _organization;
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
        int _repYear;
        // переменная содержит id текущего пакета
        static long _currentListId;
        // переменная содержит год, в который будет перемещен пакет
        public static int NewRepYear = 0;
        // переменная содержит id организации, в которую будет перемещен пакет
        public static long NewOrgId = 0;
        // переменная содержит новый тип для документа
        public static long NewDocTypeId;
        // переменная-флаг, означает для каких документов менять тип: 1 - выбранный документ, 2 - выбранные, 3 - все документы в пакете
        // или содержит id типа нового документа
        public static long FlagDoc = -1;
        // переменная содержит id пакета, в который будет перемещен документ
        public static long NewListId = 0;
        // переменная содержит id добавляемого человека
        public static long PersonId;

        #endregion

        #region Конструктор и инициализатор
        public StajDohodForm(Operator oper, Org organization, string connection)
        {
            InitializeComponent();
            _operator = oper;
            _organization = organization;
            _connection = connection;
        }

        private void StajDohodForm_Load(object sender, EventArgs e)
        {
            _currentListId = 0;
            _repYear = MainForm.RepYear;
            yearBox.Value = _repYear;

            // инициализация таблиц
            _listsTable = ListsView.CreatetTable();
            _docsTable = DocsView.CreatetTable();
            // добавление виртуального столбца для возможности отмечать записи
            _docsTable.Columns.Add(CHECK, typeof(bool));
            _docsTable.Columns[CHECK].DefaultValue = false;

            // инициализация биндинг сорса к таблице документов
            _docsBS = new BindingSource();
            _docsBS.ListChanged += new ListChangedEventHandler(_docsBS_ListChanged);
            _docsBS.DataSource = _docsTable;

            // инициализация биндинг сорса к таблице пакетов
            _listsBS = new BindingSource();
            _listsBS.CurrentChanged += new EventHandler(_listsBS_CurrentChanged);
            _listsBS.ListChanged += new ListChangedEventHandler(_listsBS_ListChanged);
            _listsBS.DataSource = _listsTable;

            // инициализация Адаптера для считывания пакетов из БД
            string commandStr = ListsView.GetSelectText(_organization.idVal, _repYear);
            _listsAdapter = new SQLiteDataAdapter(commandStr, _connection);
            // заполнение таблицы данными с БД
            _listsAdapter.Fill(_listsTable);

            // присвоение источника dataGrid
            this.listsView.AutoGenerateColumns = false;
            this.listsView.DataSource = _listsBS;
            this.docView.AutoGenerateColumns = false;
            this.docView.DataSource = _docsBS;

            // получить код привилегии (уровня доступа) Оператора к Организации
            if (_operator.IsAdmin())
                _privilege = OperatorOrg.GetPrivilegeForAdmin();
            else
                _privilege = OperatorOrg.GetPrivilege(_operator.idVal, _organization.idVal, _connection);

            //TODO: отобразить привилегию на форме для пользователя
            //this.SetPrivilege(_privilege);
        }

        private void StajDohodForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.RepYear = _repYear;
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
        }

        void _listsBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = (sender as BindingSource).Current as DataRowView;
            if (row == null)
            {
                _currentListId = 0;
                return;
            }
            if (_docsTable == null)
                return;
            // не вызывать событие изменения списка, пока не обновим данные
            if (_docsBS != null)
                _docsBS.RaiseListChangedEvents = false;
            _docsTable.Clear();
            _currentListId = (long)row[ListsView.id];
            string commandStr = DocsView.GetSelectTextByListId(_currentListId);
            _docsAdapter = new SQLiteDataAdapter(commandStr, _connection);
            _docsAdapter.Fill(_docsTable);
            // вызывать событие изменения списка и принудительный вызов
            if (_docsBS != null)
            {
                _docsBS.RaiseListChangedEvents = true;
                _docsBS.ResetBindings(false);
            }
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

        private void addListButton_Click(object sender, EventArgs e)
        {
            if (MainForm.ShowQuestionMessage("Вы действительно хотите создать\n новый пакет документов \"СЗВ-1\"?", "Создание пакета") == DialogResult.No)
                return;
            //TODO: Загружать список доступных типов пакета для текущего года, спрашивать пользователя 
            using (SQLiteConnection connection = new SQLiteConnection(_connection))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = Lists.GetInsertText(1, _organization.idVal, _repYear);
                        long listId = (long)command.ExecuteScalar();
                        if (listId < 1)
                        {
                            throw new SQLiteException("Не удалось создать новый пакет.");
                        }

                        command.CommandText = FixData.GetReplaceText(Lists.tablename, FixData.FixType.New, listId, _operator.nameVal, DateTime.Now.Date);
                        if ((long)command.ExecuteScalar() < 1)
                        {
                            throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                        }
                    }
                    transaction.Commit();
                }
                connection.Close();
            }
            //Перезагрузка данных
            ReloadLists();
            _listsBS.MoveLast();
        }

        private void removeListButton_Click(object sender, EventArgs e)
        {
            if (listsView.CurrentRow == null)
                return;
            if (MainForm.ShowQuestionMessage("Вы действительно хотите удалить выбранный пакет\n документов \"СЗВ-1\" и все документы в нём?", "Удаление пакета") == DialogResult.No)
                return;

            string commandText = Lists.GetDeleteText((long)listsView.CurrentRow.Cells["id"].Value);
            SQLiteCommand cmd = new SQLiteCommand(commandText, new SQLiteConnection(_connection));
            cmd.Connection.Open();
            if (cmd.ExecuteNonQuery() < 1)
            {
                cmd.Connection.Close();
                MainForm.ShowErrorMessage("Не удалось удалить пакет.", "Ошибка удаления пакета");
            }
            //Перезагрузка данных
            ReloadLists();
        }

        private void movePacketOrgButton_Click(object sender, EventArgs e)
        {
            if (listsView.CurrentRow == null)
                return;
            long listId = (long)listsView.CurrentRow.Cells[Lists.id].Value;
            MovePacketForm movePacketForm = new MovePacketForm(_operator, _connection, listId);
            if (movePacketForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (MainForm.ShowQuestionMessage("Вы действительно хотите переместить выбранный пакет\n документов \"СЗВ-1\" № " + listId + " и все документы в нём?", "Перемещение пакета") == DialogResult.No)
                    return;
                List<long> personIdList = new List<long>();
                using (SQLiteConnection connection = new SQLiteConnection(_connection))
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        using (SQLiteCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = Lists.GetSelectPersonIdsText(listId);
                            using (SQLiteDataReader reader = command.ExecuteReader())
                                while (reader.Read())
                                {
                                    personIdList.Add((long)reader[Docs.personID]);
                                }
                            //Привязка всех персон к новой организации
                            PersonOrg.InsertPersonOrg(personIdList, NewOrgId, connection, transaction);
                            command.CommandText = Lists.GetUpdateOrgText(listId, NewOrgId);
                            if (command.ExecuteNonQuery() < 1)
                            {
                                throw new SQLiteException("Не удалось переместить пакет.");
                            }
                            command.CommandText = FixData.GetReplaceText(Lists.tablename, FixData.FixType.Edit, listId, _operator.nameVal, DateTime.Now.Date);
                            if ((long)command.ExecuteScalar() < 1)
                            {
                                throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                            }
                        }
                        transaction.Commit();
                    }
                    connection.Close();
                }
                //Перезагрузка данных
                ReloadLists();
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
                if (MainForm.ShowQuestionMessage("Вы действительно хотите переместить выбранный пакет\n документов \"СЗВ-1\" № " + listId + " и все документы в нём?", "Перемещение пакета") == DialogResult.No)
                    return;
                using (SQLiteConnection connection = new SQLiteConnection(_connection))
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        using (SQLiteCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = Lists.GetUpdateYearText(listId, NewRepYear);
                            if (command.ExecuteNonQuery() < 1)
                            {
                                throw new SQLiteException("Не удалось переместить пакет.");
                            }




                            command.CommandText = FixData.GetReplaceText(Lists.tablename, FixData.FixType.Edit, listId, _operator.nameVal, DateTime.Now.Date);
                            if ((long)command.ExecuteScalar() < 1)
                            {
                                throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                            }
                        }
                        transaction.Commit();
                    }
                    connection.Close();
                }
                //Перезагрузка данных
                ReloadLists();
            }
        }

        private void addDocButton_Click(object sender, EventArgs e)
        {
            ChoicePersonForm choicePersonForm = new ChoicePersonForm(_organization, _repYear, _connection);
            if (choicePersonForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form(_organization, _operator, _currentListId, _repYear, PersonId, FlagDoc, _connection);
                if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _listsBS_CurrentChanged(_listsBS, new EventArgs());
                }
                //Перезагрузка данных
                //ReloadDataAfterChanges();
            }
        }

        private void editDocButton_Click(object sender, EventArgs e)
        {
            if (_docsBS.Current == null)
                return;
            long docId = (long)(_docsBS.Current as DataRowView)[DocsView.id];
            PersonId = (long)(_docsBS.Current as DataRowView)[DocsView.personID];
            FlagDoc = (int)(_docsBS.Current as DataRowView)[DocsView.docTypeId];
            AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form(_organization, _operator, _currentListId, _repYear, PersonId, FlagDoc, _connection, docId);
            if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _listsBS_CurrentChanged(_listsBS, new EventArgs());
            }
        }

        private void removeDocButton_Click(object sender, EventArgs e)
        {
            if (docView.CurrentRow == null)
                return;
            if (MainForm.ShowQuestionMessage("Вы действительно хотите удалить выбранный документ \"СЗВ-1\"?", "Удаление документа") == DialogResult.No)
                return;
            long doc_id = (long)docView.CurrentRow.Cells["idColumn"].Value;
            string commandText = Docs.GetDeleteText(doc_id);
            SQLiteCommand cmd = new SQLiteCommand(commandText, new SQLiteConnection(_connection));
            cmd.Connection.Open();
            if (cmd.ExecuteNonQuery() < 1)
            {
                cmd.Connection.Close();
                throw new SQLiteException("Не удалось удалить документ.");
            }
            //Перезагрузка данных
            _listsBS_CurrentChanged(_listsBS, new EventArgs());
            //ReloadDataAfterChanges();
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
            MoveDocumentForm moveDocForm = new MoveDocumentForm(_organization, _repYear, docId, _connection);
            if (moveDocForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (MainForm.ShowQuestionMessage("Вы действительно хотите переместить выбранный документ \"СЗВ-1\"?", "Перемещение документа") == DialogResult.No)
                    return;
                using (SQLiteConnection connection = new SQLiteConnection(_connection))
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        using (SQLiteCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            if (Docs.UpdateListId(docId, NewListId, connection, transaction) < 1)
                            {
                                throw new SQLiteException("Не удалось переместить документ.");
                            }
                            command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit, docId, _operator.nameVal, DateTime.Now.Date);
                            if ((long)command.ExecuteScalar() < 1)
                            {
                                throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                            }
                        }
                        transaction.Commit();
                    }
                    connection.Close();
                }
                //Перезагрузка данных
                _listsBS_CurrentChanged(_listsBS, new EventArgs());
                //ReloadDataAfterChanges();
            }
        }

        private void docView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 1)
            {
                docView.EndEdit();
                bool allchecked = true;
                foreach (DataRowView row in _docsBS)
                    if (!(bool)row[CHECK])
                    {
                        allchecked = false;
                        break;
                    }
                foreach (DataRowView row in _docsBS)
                    row[CHECK] = !allchecked;
                this.docView.Refresh();
            }
        }

        private void documentView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            //AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form();
            //if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{ }
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
                    using (SQLiteConnection connection = new SQLiteConnection(_connection))
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        using (SQLiteTransaction transaction = connection.BeginTransaction())
                        {
                            using (SQLiteCommand command = connection.CreateCommand())
                            {
                                command.Transaction = transaction;
                                List<long> docIdList = new List<long>();
                                switch (FlagDoc)
                                {
                                    case 1:
                                        DataRowView row = _docsBS.Current as DataRowView;
                                        if (row != null)
                                            docIdList.Add((long)row[Docs.id]);
                                        Docs.UpdateDocTypeByDocId(docIdList, NewDocTypeId, connection, transaction);

                                        command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit, (long)row[Docs.id], _operator.nameVal, DateTime.Now.Date);
                                        if ((long)command.ExecuteScalar() < 1)
                                        {
                                            throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                                        }
                                        break;
                                    case 2:
                                        docIdList = GetSelectedDocIds();
                                        Docs.UpdateDocTypeByDocId(docIdList, NewDocTypeId, connection, transaction);
                                        foreach (long docId in docIdList)
                                        {
                                            command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit, docId, _operator.nameVal, DateTime.Now.Date);
                                            if ((long)command.ExecuteScalar() < 1)
                                            {
                                                throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                                            }
                                        }
                                        break;
                                    case 3:
                                        Docs.UpdateDocTypeByListId((long)listsView.CurrentRow.Cells["id"].Value, NewDocTypeId, connection, transaction);
                                        foreach (DataRowView rowDoc in _docsBS)
                                        {
                                            command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit, (long)rowDoc[Docs.id], _operator.nameVal, DateTime.Now.Date);
                                            if ((long)command.ExecuteScalar() < 1)
                                            {
                                                throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                                            }
                                        }
                                        break;
                                    default:
                                        throw new Exception("Не указан документ у которого необходимо изменить тип.");
                                }
                            }
                            transaction.Commit();
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MainForm.ShowErrorMessage("Не удалось изменить тип документа(ов).\n" + ex.Message, "Ошибка изменения типа документа(ов)");
                }

                //Перезагрузка данных
                _listsBS_CurrentChanged(_listsBS, new EventArgs());
                //ReloadDataAfterChanges();
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
            _repYear = (int)yearBox.Value;
            MainForm.RepYear = _repYear;
            ReloadLists();
        }
        #endregion

        #region Методы - свои
        private List<long> GetSelectedDocIds()
        {
            List<long> list = new List<long>();
            foreach (DataRowView docRow in _docsBS)
                if ((bool)docRow[CHECK])
                    list.Add((long)docRow["id"]);
            return list;
        }

        private void ReloadLists()
        {
            if (_listsBS == null)
                return;
            if (_listsTable == null)
                return;
            //if (_docsBS == null)
            //return;

            //отключение события, что б не мерцали кнопки при обновлении
            //_listsBS.ListChanged -= new ListChangedEventHandler(_listsBS_ListChanged);
            _listsBS.RaiseListChangedEvents = false;
            
            //отключение события, что б не мерцали кнопки при обновлении
            //_docsBS.ListChanged -= new ListChangedEventHandler(_docsBS_ListChanged);
            //_docsBS.RaiseListChangedEvents = false;
            
            _listsTable.Clear();
            string commandStr = ListsView.GetSelectText(_organization.idVal, _repYear);
            _listsAdapter = new SQLiteDataAdapter(commandStr, _connection);
            _listsAdapter.Fill(_listsTable);

            //добавление событий и их запуск
            //_listsBS.ListChanged += new ListChangedEventHandler(_listsBS_ListChanged);
            //_listsBS_ListChanged(null, new ListChangedEventArgs(ListChangedType.Reset, -1));
            _listsBS.RaiseListChangedEvents = true;
            _listsBS.ResetBindings(false);

            //_docsBS.ListChanged += new ListChangedEventHandler(_docsBS_ListChanged);
            //_docsBS_ListChanged(null, new ListChangedEventArgs(ListChangedType.Reset, -1));
            //_docsBS.RaiseListChangedEvents = true;
            //_docsBS.ResetBindings(false);
        }
        #endregion
    }
}
