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
            try
            {
                _currentListId = 0;
                _repYear = MainForm.RepYear;
                yearBox.Value = _repYear;

                // инициализация таблиц
                _listsTable = ListsView.CreateTable();
                _docsTable = DocsView.CreateTable();
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
                this.listsView.Columns["id"].DataPropertyName = ListsView.id;
                this.listsView.Columns["list_type"].DataPropertyName = ListsView.nameType;
                this.listsView.Columns["operatorRegColumn"].DataPropertyName = ListsView.operatorNameReg;
                this.listsView.Columns["datecreateColumn"].DataPropertyName = ListsView.regDate;
                this.listsView.Columns["operatorEditColumn"].DataPropertyName = ListsView.operatorNameChange;
                this.listsView.Columns["dateEditColumn"].DataPropertyName = ListsView.changeDate;
                this.listsView.DataSource = _listsBS;


                this.docView.AutoGenerateColumns = false;
                this.docView.Columns["checkColumn"].DataPropertyName = CHECK;
                this.docView.Columns["idColumn"].DataPropertyName = DocsView.id;
                this.docView.Columns["socNumColumn"].DataPropertyName = DocsView.socNumber;
                this.docView.Columns["fioColumn"].DataPropertyName = DocsView.fio;
                this.docView.Columns["typeformColumn"].DataPropertyName = DocsView.nameType;
                this.docView.Columns["categoryColumn"].DataPropertyName = DocsView.code;
                this.docView.Columns["operRegColumn"].DataPropertyName = DocsView.operNameReg;
                this.docView.Columns["regDateColumn"].DataPropertyName = DocsView.regDate;
                this.docView.Columns["operChangeColumn"].DataPropertyName = DocsView.operNameChange;
                this.docView.Columns["changeDateColumn"].DataPropertyName = DocsView.changeDate;


                this.docView.DataSource = _docsBS;

                // получить код привилегии (уровня доступа) Оператора к Организации
                if (_operator.IsAdmin())
                    _privilege = OperatorOrg.GetPrivilegeForAdmin();
                else
                    _privilege = OperatorOrg.GetPrivilege(_operator.idVal, _organization.idVal, _connection);

                //TODO: отобразить привилегию на форме для пользователя
                //this.SetPrivilege(_privilege);
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void StajDohodForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.RepYear = _repYear;
        }
        #endregion

        #region Методы - обработчики событий
        void _listsBS_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                packetcountBox.Text = _listsBS.Count.ToString();
                bool isEnabled = !(_listsBS.Count < 1);

                delListStripButton.Enabled = isEnabled;
                moveToOrgListStripButton.Enabled = isEnabled;
                moveToYearListStripButton.Enabled = isEnabled;
                reestrListStripButton.Enabled = isEnabled;
                calcListStripButton.Enabled = isEnabled;
                printFioListStripButton.Enabled = isEnabled;
                addDocStripButton.Enabled = isEnabled;

                if (!isEnabled && _docsTable != null)
                    _docsTable.Clear();
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        void _listsBS_CurrentChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        void _docsBS_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                docCountBox.Text = _docsBS.Count.ToString();
                bool isEnabled = !(_docsBS.Count < 1);
                delDocStripButton.Enabled = isEnabled;
                editDocStripButton.Enabled = isEnabled;
                moveToListDocStripButton.Enabled = isEnabled;
                copyToListDocStripButton.Enabled = isEnabled;
                printDocStripButton.Enabled = isEnabled;
                changeTypeDocStripButton.Enabled = isEnabled;
                previewDocStripButton.Enabled = isEnabled;

            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void docView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
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
                }
                this.docView.Refresh();
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void documentView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            editDocStripButton_Click(sender, e);
        }

        private void yearBox_ValueChanged(object sender, EventArgs e)
        {
            _repYear = (int)yearBox.Value;
            MainForm.RepYear = _repYear;
            ReloadLists();
        }

        private void listsView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    DataGridViewRow r = (sender as DataGridView).Rows[e.RowIndex];
                    if (!r.Selected)
                    {
                        r.DataGridView.ClearSelection();
                        r.DataGridView.CurrentCell = r.Cells[0];
                        r.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void listsView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Apps || (e.Shift && e.KeyCode == Keys.F10))
                {
                    DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
                    if (currentCell != null)
                    {
                        ContextMenuStrip cms = cmsLists;
                        if (cms != null)
                        {
                            Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                            Point p = new Point(r.Left, r.Top);
                            cms.Show((sender as DataGridView), p);
                        }
                    }
                }

                if (e.KeyCode == Keys.Delete)
                {
                    delListStripButton_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void listsView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip menu = cmsLists;
                    if (menu == null)
                        return;

                    DataGridView dataView = sender as DataGridView;
                    ToolStripItem[] items; //Массив в который возвращает элементы метод Find
                    List<string> menuItems = new List<string>(); //Список элементов которые нужно включать\выключать
                    menuItems.Add("viewOpisMenuItem");
                    menuItems.Add("calcMenuItem");
                    menuItems.Add("printListMenuItem");
                    menuItems.Add("copyToOtherYearMenuItem");
                    menuItems.Add("moveToOtherYearMenuItem");
                    menuItems.Add("copyToOtherOrgMenuItem");
                    menuItems.Add("moveToOtherOrgMenuItem");
                    menuItems.Add("delListMenuItem");

                    int currentMouseOverRow = dataView.HitTest(e.X, e.Y).RowIndex;
                    bool isEnabled = !(currentMouseOverRow < 0);
                    for (int i = 0; i < menuItems.Count; i++)
                    {
                        items = menu.Items.Find(menuItems[i].ToString(), false);
                        if (items.Count() > 0)
                            items[0].Enabled = isEnabled;
                    }

                    menuItems = new List<string>(); //Список элементов которые нужно принудительно выключать
                    //menuItems.Add("copyToOtherYearMenuItem");
                    menuItems.Add("copyToOtherOrgMenuItem");
                    for (int i = 0; i < menuItems.Count; i++)
                    {
                        items = menu.Items.Find(menuItems[i].ToString(), false);
                        if (items.Count() > 0)
                            items[0].Enabled = false;
                    }

                    menu.Show(dataView, e.Location);
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void docView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    DataGridViewRow r = (sender as DataGridView).Rows[e.RowIndex];
                    if (!r.Selected)
                    {
                        r.DataGridView.ClearSelection();
                        r.DataGridView.CurrentCell = r.Cells[1];
                        r.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void docView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Apps || (e.Shift && e.KeyCode == Keys.F10))
                {
                    DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
                    if (currentCell != null)
                    {
                        ContextMenuStrip cms = cmsLists;
                        if (cms != null)
                        {
                            Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                            Point p = new Point(r.Left, r.Top);
                            cms.Show((sender as DataGridView), p);
                        }
                    }
                }

                if (e.KeyCode == Keys.Delete)
                {
                    delDocStripButton_Click(sender, e);
                }

                if (e.KeyCode == Keys.Enter)
                {
                    editDocStripButton_Click(sender, e);
                }

                if (e.KeyCode == Keys.Space)
                {
                    if ((sender as DataGridView).CurrentRow == null)
                        return;

                    (_docsBS.Current as DataRowView)[CHECK] = !Convert.ToBoolean((_docsBS.Current as DataRowView)[CHECK]);
                    (sender as DataGridView).EndEdit();
                    (sender as DataGridView).Refresh();
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void docView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip menu = cmsDocs;
                    if (menu == null)
                        return;

                    DataGridView dataView = sender as DataGridView;
                    ToolStripItem[] items; //Массив в который возвращает элементы метод Find
                    List<string> menuItems = new List<string>(); //Список элементов которые нужно включать\выключать
                    menuItems.Add("editDocMenuItem");
                    menuItems.Add("changeTypeDocMenuItem");
                    menuItems.Add("previewDocMenuItem");
                    menuItems.Add("copyToOtherListMenuItem");
                    menuItems.Add("moveToOtherListMenuItem");
                    menuItems.Add("delDocMenuItem");

                    int currentMouseOverRow = dataView.HitTest(e.X, e.Y).RowIndex;
                    bool isEnabled = !(currentMouseOverRow < 0);
                    for (int i = 0; i < menuItems.Count; i++)
                    {
                        items = menu.Items.Find(menuItems[i].ToString(), false);
                        if (items.Count() > 0)
                            items[0].Enabled = isEnabled;
                    }

                    menuItems = new List<string>(); //Список элементов которые нужно принудительно выключать
                    //menuItems.Add("copyToOtherListMenuItem");
                    for (int i = 0; i < menuItems.Count; i++)
                    {
                        items = menu.Items.Find(menuItems[i].ToString(), false);
                        if (items.Count() > 0)
                            items[0].Enabled = false;
                    }

                    menu.Show(dataView, e.Location);
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void addListMenuItem_Click(object sender, EventArgs e)
        {
            addListStripButton_Click(sender, e);
        }

        private void copyToOtherYearMenuItem_Click(object sender, EventArgs e)
        {
            copyToYearListStripButton_Click(sender, e);
        }

        private void moveToOtherYearMenuItem_Click(object sender, EventArgs e)
        {
            moveToYearListStripButton_Click(sender, e);
        }

        private void moveToOtherOrgMenuItem_Click(object sender, EventArgs e)
        {
            moveToOrgListStripButton_Click(sender, e);
        }

        private void delListMenuItem_Click(object sender, EventArgs e)
        {
            delListStripButton_Click(sender, e);
        }

        private void addDocMenuItem_Click(object sender, EventArgs e)
        {
            addDocStripButton_Click(sender, e);
        }

        private void editDocMenuItem_Click(object sender, EventArgs e)
        {
            editDocStripButton_Click(sender, e);
        }

        private void changeTypeDocMenuItem_Click(object sender, EventArgs e)
        {
            changeTypeDocStripButton_Click(sender, e);
        }

        private void moveToOtherListMenuItem_Click(object sender, EventArgs e)
        {
            moveToListDocStripButton_Click(sender, e);
        }

        private void copyToOtherListMenuItem_Click(object sender, EventArgs e)
        {
            copyToListDocStripButton_Click(sender, e);
        }

        private void delDocMenuItem_Click(object sender, EventArgs e)
        {
            delDocStripButton_Click(sender, e);
        }

        private void addListStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.ShowQuestionMessage("Вы действительно хотите создать\nновый пакет документов СЗВ-1?", "Создание пакета") == DialogResult.No)
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

                            command.CommandText = FixData.GetReplaceText(Lists.tablename, FixData.FixType.New, listId, _operator.nameVal, DateTime.Now);
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
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка создания пакета");
            }
        }

        private void delListStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (listsView.CurrentRow == null)
                    return;
                if (MainForm.ShowQuestionMessage("Вы действительно хотите удалить выбранный пакет\nдокументов СЗВ-1 и все документы в нём?", "Удаление пакета") == DialogResult.No)
                    return;

                string commandText = Lists.GetDeleteText(_currentListId);
                SQLiteCommand cmd = new SQLiteCommand(commandText, new SQLiteConnection(_connection));
                cmd.Connection.Open();
                if (cmd.ExecuteNonQuery() < 1)
                {
                    cmd.Connection.Close();
                    throw new SQLiteException("Не удалось удалить пакет.");
                }
                //Перезагрузка данных
                ReloadLists();
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка удаления пакета");
            }
        }

        private void reestrListStripButton_Click(object sender, EventArgs e)
        {

        }

        private void calcListStripButton_Click(object sender, EventArgs e)
        {

        }

        private void printFioListStripButton_Click(object sender, EventArgs e)
        {

        }

        private void copyToYearListStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (listsView.CurrentRow == null)
                    return;
                long listId = (long)listsView.CurrentRow.Cells["id"].Value;
                CopyPacketOtherYearForm copyPacketOtherYear = new CopyPacketOtherYearForm(listId);

                if (copyPacketOtherYear.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (MainForm.ShowQuestionMessage("Вы действительно хотите копировать выбранный пакет\nдокументов СЗВ-1 № " + listId + " и все документы в нём?", "Копирование пакета") == DialogResult.No)
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

                                long newListId = -1;
                                newListId = Lists.CopyListById(listId, connection, transaction);
                                command.CommandText = Docs.GetSelectText(listId);

                                List<long> docIdList = new List<long>();
                                SQLiteDataReader reader = command.ExecuteReader();
                                while (reader.Read())
                                {
                                    docIdList.Add(Convert.ToInt64(reader[Docs.id]));
                                }

                                CopyDocsByDocId(docIdList, newListId, connection, transaction);
                                
                                Lists.UpdateYear(newListId, NewRepYear, connection, transaction);

                                long res = 0;
                                //Сохранение в таблицу Fixdata
                                res = FixData.ExecReplaceText(Lists.tablename, FixData.FixType.New, listId, _operator.nameVal, DateTime.Now, connection, transaction);
                                if (res < 1)
                                {
                                    throw new SQLiteException("Невозможно создать документ. Таблица " + FixData.tablename + ".");
                                }
                            }
                            transaction.Commit();
                        }
                        connection.Close();
                    }
                    MainForm.ShowInfoFlexMessage("Копирование пакета успешно завершено!", "Копирование пакета");
                    //Перезагрузка данных
                    ReloadLists();
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка копирования пакета");
            }
        }

        private void moveToYearListStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (listsView.CurrentRow == null)
                    return;
                long listId = (long)listsView.CurrentRow.Cells["id"].Value;
                MovePacketOtherYearForm movePacketOtherYear = new MovePacketOtherYearForm(listId);
                if (movePacketOtherYear.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (MainForm.ShowQuestionMessage("Вы действительно хотите переместить выбранный пакет\nдокументов СЗВ-1 № " + listId + " и все документы в нём?", "Перемещение пакета") == DialogResult.No)
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
                                command.CommandText = FixData.GetReplaceText(Lists.tablename, FixData.FixType.Edit, listId, _operator.nameVal, DateTime.Now);
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
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка перемещения пакета");
            }
        }

        private void copyToOrgListStripButton_Click(object sender, EventArgs e)
        {

        }

        private void moveToOrgListStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (listsView.CurrentRow == null)
                    return;
                long listId = (long)listsView.CurrentRow.Cells[Lists.id].Value;
                MovePacketForm movePacketForm = new MovePacketForm(_operator, _connection, listId);
                if (movePacketForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (MainForm.ShowQuestionMessage("Вы действительно хотите переместить выбранный пакет\nдокументов СЗВ-1 № " + listId + " и все документы в нём?", "Перемещение пакета") == DialogResult.No)
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
                                command.CommandText = FixData.GetReplaceText(Lists.tablename, FixData.FixType.Edit, listId, _operator.nameVal, DateTime.Now);
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
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка перемещения пакета");
            }
        }

        private void addDocStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                ChoicePersonForm choicePersonForm = new ChoicePersonForm(_organization, _repYear, _connection);
                if (choicePersonForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form(_organization, _operator, _currentListId, _repYear, PersonId, FlagDoc, _connection);
                    if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        _listsBS_CurrentChanged(_listsBS, new EventArgs());

                        _docsBS.Position = _docsBS.Find(DocsView.id, szv1Form.CurrentDocId);
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка создания документа");
            }
        }

        private void editDocStripButton_Click(object sender, EventArgs e)
        {
            try
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

                    _docsBS.Position = _docsBS.Find(DocsView.id, szv1Form.CurrentDocId);
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка изменения документа");
            }
        }

        private void delDocStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (docView.CurrentRow == null)
                    return;

                (_docsBS.Current as DataRowView)[CHECK] = true;
                docView.EndEdit();
                docView.Refresh();

                List<long> docIdList = new List<long>();
                docIdList = GetSelectedDocIds();
                if (docIdList.Count < 1)
                    if (_docsBS.Current != null)
                    {
                        (_docsBS.Current as DataRowView)[CHECK] = true;
                        docIdList = GetSelectedDocIds();
                    }

                string listFio = GetFioForSelectedDocIds(docIdList);

                if (MainForm.ShowQuestionFlexMessage("Вы действительно хотите удалить выбранные документы СЗВ-1?\nКоличество выбранных документов: " + docIdList.Count + "\n" + listFio, "Удаление документа(ов)") == DialogResult.No)
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
                            foreach (long docId in docIdList)
                            {
                                command.CommandText = Docs.GetDeleteText(docId);
                                if (connection.State != ConnectionState.Open)
                                    connection.Open();
                                if (command.ExecuteNonQuery() < 1)
                                {
                                    throw new SQLiteException("Не удалось удалить документ.");
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    connection.Close();
                }
                //Перезагрузка данных
                int position = -1;
                position = _docsBS.Find(DocsView.id, docIdList[0]);

                _listsBS_CurrentChanged(_listsBS, new EventArgs());

                _docsBS.Position = position - 1;

            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка удаления документа");
            }
        }

        private void changeTypeDocStripButton_Click(object sender, EventArgs e)
        {
            if (docView.CurrentRow == null)
                return;

            (_docsBS.Current as DataRowView)[CHECK] = true;
            docView.EndEdit();
            docView.Refresh();

            List<long> docIdList = new List<long>();
            docIdList = GetSelectedDocIds();
            if (docIdList.Count < 1)
                if (_docsBS.Current != null)
                {
                    (_docsBS.Current as DataRowView)[CHECK] = true;
                    docIdList = GetSelectedDocIds();
                }

            ReplaceDocTypeForm replaceDocTypeForm = new ReplaceDocTypeForm(_connection);
            if (replaceDocTypeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string listFio = GetFioForSelectedDocIds(docIdList);

                    if (MainForm.ShowQuestionFlexMessage("Вы действительно хотите изменить тип у выбранных документов СЗВ-1?\nКоличество выбранных документов: " + docIdList.Count + "\n" + listFio, "Замена типа формы документа(ов) СЗВ-1") == DialogResult.No)
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


                                //switch (FlagDoc)
                                //{
                                //    case 1:
                                //        DataRowView row = _docsBS.Current as DataRowView;
                                //        if (row != null)
                                //            docIdList.Add((long)row[DocsView.id]);
                                //        Docs.UpdateDocTypeByDocId(docIdList, NewDocTypeId, connection, transaction);

                                //        command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit, (long)row[DocsView.id], _operator.nameVal, DateTime.Now);
                                //        if ((long)command.ExecuteScalar() < 1)
                                //        {
                                //            throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                                //        }
                                //        break;
                                //    case 2:

                                Docs.UpdateDocTypeByDocId(docIdList, NewDocTypeId, connection, transaction);
                                foreach (long docId in docIdList)
                                {
                                    command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit, docId, _operator.nameVal, DateTime.Now);
                                    if ((long)command.ExecuteScalar() < 1)
                                    {
                                        throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                                    }
                                }
                                //break;
                                //    case 3:
                                //        Docs.UpdateDocTypeByListId((long)listsView.CurrentRow.Cells["id"].Value, NewDocTypeId, connection, transaction);
                                //        foreach (DataRowView rowDoc in _docsBS)
                                //        {
                                //            command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit, (long)rowDoc[DocsView.id], _operator.nameVal, DateTime.Now);
                                //            if ((long)command.ExecuteScalar() < 1)
                                //            {
                                //                throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                                //            }
                                //        }
                                //        break;
                                //    default:
                                //        throw new Exception("Не указан документ у которого необходимо изменить тип.");
                                //}
                            }
                            transaction.Commit();
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MainForm.ShowErrorFlexMessage("Не удалось изменить тип документа(ов).\n" + ex.Message, "Ошибка изменения типа документа(ов)");
                }

                //Перезагрузка данных
                long curDocId = -1;
                if (_docsBS.Current != null)
                    curDocId = (long)(_docsBS.Current as DataRowView)[DocsView.id];

                _listsBS_CurrentChanged(_listsBS, new EventArgs());

                _docsBS.Position = _docsBS.Find(DocsView.id, curDocId);
            }
        }

        private void printDocStripButton_Click(object sender, EventArgs e)
        {
            PrintStajForm printStajForm = new PrintStajForm();
            if (printStajForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void previewDocStripButton_Click(object sender, EventArgs e)
        {

        }

        private void copyToListDocStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (docView.CurrentRow == null)
                    return;

                (_docsBS.Current as DataRowView)[CHECK] = true;
                docView.EndEdit();
                docView.Refresh();

                List<long> docIdList = new List<long>();
                docIdList = GetSelectedDocIds();
                if (docIdList.Count < 1)
                    if (_docsBS.Current != null)
                    {
                        (_docsBS.Current as DataRowView)[CHECK] = true;
                        docIdList = GetSelectedDocIds();
                    }

                string listFio = GetFioForSelectedDocIds(docIdList);

                CopyDocumentForm moveDocForm = new CopyDocumentForm(_organization, _repYear, _connection);
                if (moveDocForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (MainForm.ShowQuestionFlexMessage("Вы действительно хотите копировать выбранные документы СЗВ-1?\nКоличество выбранных документов: " + docIdList.Count + "\n" + listFio, "Копирование документа(ов) СЗВ-1") == DialogResult.No)
                        return;
                    using (SQLiteConnection connection = new SQLiteConnection(_connection))
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        using (SQLiteTransaction transaction = connection.BeginTransaction())
                        {
                            CopyDocsByDocId(docIdList, NewListId, connection, transaction);
                            transaction.Commit();
                        }
                        connection.Close();
                    }
                    MainForm.ShowInfoFlexMessage("Копирование документов успешно завершено!", "Копирование документа(ов)");
                    _listsBS_CurrentChanged(_listsBS, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка копирования документа(ов)");
            }
        }
        
        private void moveToListDocStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (docView.CurrentRow == null)
                    return;

                (_docsBS.Current as DataRowView)[CHECK] = true;
                docView.EndEdit();
                docView.Refresh();

                List<long> docIdList = new List<long>();
                docIdList = GetSelectedDocIds();
                if (docIdList.Count < 1)
                    if (_docsBS.Current != null)
                    {
                        (_docsBS.Current as DataRowView)[CHECK] = true;
                        docIdList = GetSelectedDocIds();
                    }

                string listFio = GetFioForSelectedDocIds(docIdList);
                //long docId = 0;
                //DataRowView row = _docsBS.Current as DataRowView;
                //if (row == null)
                //    return;
                //docId = (long)row[Docs.id];

                MoveDocumentForm moveDocForm = new MoveDocumentForm(_organization, _repYear, _connection);
                if (moveDocForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (MainForm.ShowQuestionFlexMessage("Вы действительно хотите переместить выбранные документы СЗВ-1?\nКоличество выбранных документов: " + docIdList.Count + "\n" + listFio, "Перемещение документа(ов) СЗВ-1") == DialogResult.No)
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
                                foreach (long docId in docIdList)
                                {
                                    if (Docs.UpdateListId(docId, NewListId, connection, transaction) < 1)
                                    {
                                        throw new SQLiteException("Не удалось переместить документ.");
                                    }
                                    command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit, docId, _operator.nameVal, DateTime.Now);
                                    if ((long)command.ExecuteScalar() < 1)
                                    {
                                        throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                                    }
                                }
                            }
                            transaction.Commit();
                        }
                        connection.Close();
                    }
                    //Перезагрузка данных
                    //int position = -1;
                    //position = _docsBS.Find(DocsView.id, docId);

                    _listsBS_CurrentChanged(_listsBS, new EventArgs());

                    //_docsBS.Position = position - 1;
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка перемещения документа");
            }
        }
        #endregion

        #region Методы - свои
        private List<long> GetSelectedDocIds()
        {
            List<long> list = new List<long>();
            docView.EndEdit();
            foreach (DataRowView docRow in _docsBS)
                if ((bool)docRow[CHECK])
                    list.Add((long)docRow["id"]);
            return list;
        }

        private string GetFioForSelectedDocIds(List<long> docIdList)
        {
            string listFio = String.Empty;
            StringBuilder builder = new StringBuilder();
            int position = 0;
            //Формирование списка выбранных документов
            foreach (var item in docIdList)
            {
                position = _docsBS.Find(DocsView.id, item);
                builder.AppendFormat("\n{0} {1}", (_docsBS[position] as DataRowView)[DocsView.fio].ToString(), (_docsBS[position] as DataRowView)[DocsView.socNumber].ToString());
            }
            return builder.ToString();
        }

        private void ReloadLists()
        {
            if (_listsBS == null)
                return;
            if (_listsTable == null)
                return;

            //отключение события ListChangedEventHandler , что б не мерцали кнопки при обновлении
            _listsBS.RaiseListChangedEvents = false;

            _listsTable.Clear();
            string commandStr = ListsView.GetSelectText(_organization.idVal, _repYear);
            _listsAdapter = new SQLiteDataAdapter(commandStr, _connection);
            _listsAdapter.Fill(_listsTable);

            //включение события ListChangedEventHandler 
            _listsBS.RaiseListChangedEvents = true;
            _listsBS.ResetBindings(false);
        }

        private void CopyDocsByDocId(List<long> docIdList, long newListId, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            long newDocId = -1;
            foreach (long oldDocId in docIdList)
            {
                //Сохранение в таблицу Doc
                newDocId = Docs.CopyDocByDocId(oldDocId, newListId, connection, transaction);
                if (newDocId < 1)
                    throw new SQLiteException("Невозможно создать документ.");

                long res = 0;
                //Сохранение в таблицу Fixdata
                res = FixData.ExecReplaceText(Docs.tablename, FixData.FixType.New, newDocId, _operator.nameVal, DateTime.Now, connection, transaction);
                if (res < 1)
                {
                    throw new SQLiteException("Невозможно создать документ. Таблица " + FixData.tablename + ".");
                }
                //Сохранение в таблицу IndDoc
                res = IndDocs.CopyIndDocByDocId(oldDocId, newDocId, connection, transaction);
                if (res < 1)
                {
                    throw new SQLiteException("Невозможно создать документ. Таблица " + IndDocs.tablename + ".");
                }
                //Сохранение в таблицу Gen_period
                res = GeneralPeriod.CopyPeriodByDocId(oldDocId, newDocId, connection, transaction);
                //Сохранение в таблицу Dop_period
                res = DopPeriod.CopyPeriodByDocId(oldDocId, newDocId, connection, transaction);
                //Сохранение в таблицу Spec_period
                res = SpecialPeriod.CopyPeriodByDocId(oldDocId, newDocId, connection, transaction);
                //Сохранение в таблицу Salary_Info
                res = SalaryInfo.CopySalaryInfoByDocId(oldDocId, newDocId, connection, transaction);
                if (res < 1)
                {
                    throw new SQLiteException("Невозможно создать документ. Таблица " + SalaryInfo.tablename + ".");
                }
            }
        }
        #endregion
    }
}
