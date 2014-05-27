using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class StajDohodForm : Form
    {
        #region Поля

        // строка подключения к БД
        private string _connection;
        // активный оператор
        private Operator _operator;
        // активная организация
        private Org _organization;
        // привилегия
        private string _privilege;
        // таблица пакетов
        private DataTable _listsTable;
        // таблица документов
        private DataTable _docsTable;
        // биндинг сорс для таблиц
        private BindingSource _listsBS;
        private BindingSource _docsBS;
        // адаптер для чтения данных из БД
        private SQLiteDataAdapter _listsAdapter;
        private SQLiteDataAdapter _docsAdapter;
        // названия добавочного виртуального столбца
        private const string Check = "check";
        // переменная содержит текущий используемый год
        private int _repYear;
        // переменная содержит id текущего пакета
        private static long _currentListId;
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
                Text += " - " + _organization.regnumVal;

                // получить код привилегии (уровня доступа) Оператора к Организации
                if (_operator.IsAdmin())
                    _privilege = OperatorOrg.GetPrivilegeForAdmin();
                else
                    _privilege = OperatorOrg.GetPrivilege(_operator.idVal, _organization.idVal, _connection);

                _currentListId = 0;
                _repYear = MainForm.RepYear;
                yearBox.Value = _repYear;

                // инициализация таблиц
                _listsTable = ListsView.CreateTable();
                _docsTable = DocsView.CreateTable();
                // добавление виртуального столбца для возможности отмечать записи
                _docsTable.Columns.Add(Check, typeof(bool));
                _docsTable.Columns[Check].DefaultValue = false;

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
                listsView.AutoGenerateColumns = false;
                listsView.Columns["id"].DataPropertyName = ListsView.id;
                listsView.Columns["list_type"].DataPropertyName = ListsView.nameType;
                listsView.Columns["operatorRegColumn"].DataPropertyName = ListsView.operatorNameReg;
                listsView.Columns["datecreateColumn"].DataPropertyName = ListsView.regDate;
                listsView.Columns["operatorEditColumn"].DataPropertyName = ListsView.operatorNameChange;
                listsView.Columns["dateEditColumn"].DataPropertyName = ListsView.changeDate;
                listsView.DataSource = _listsBS;


                docView.AutoGenerateColumns = false;
                docView.Columns["checkColumn"].DataPropertyName = Check;
                docView.Columns["idColumn"].DataPropertyName = DocsView.id;
                docView.Columns["socNumColumn"].DataPropertyName = DocsView.socNumber;
                docView.Columns["fioColumn"].DataPropertyName = DocsView.fio;
                docView.Columns["typeformColumn"].DataPropertyName = DocsView.nameType;
                docView.Columns["categoryColumn"].DataPropertyName = DocsView.code;
                docView.Columns["operRegColumn"].DataPropertyName = DocsView.operNameReg;
                docView.Columns["regDateColumn"].DataPropertyName = DocsView.regDate;
                docView.Columns["operChangeColumn"].DataPropertyName = DocsView.operNameChange;
                docView.Columns["changeDateColumn"].DataPropertyName = DocsView.changeDate;


                docView.DataSource = _docsBS;

                SetPrivilege(_privilege);
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void SetPrivilege(string privilegeCode)
        {
            bool readOnly = OperatorOrg.GetStajDohodDataAccesseCode(privilegeCode) == 1;
            if (readOnly)
            {
                addListStripButton.Enabled = false;
                delListStripButton.Enabled = false;
                copyToYearListStripButton.Enabled = false;
                copyToOrgListStripButton.Enabled = false;
                moveToYearListStripButton.Enabled = false;
                moveToOrgListStripButton.Enabled = false;

                addDocStripButton.Enabled = false;
                editDocStripButton.Enabled = false;
                delDocStripButton.Enabled = false;
                copyToListDocStripButton.Enabled = false;
                moveToListDocStripButton.Enabled = false;
                changeTypeDocStripButton.Enabled = false;

                docView.CellDoubleClick -= documentView_CellDoubleClick;

                addListStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                delListStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                copyToYearListStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                copyToOrgListStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                moveToYearListStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                moveToOrgListStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                addDocStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                editDocStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                delDocStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                copyToListDocStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                moveToListDocStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                changeTypeDocStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
            }
        }

        private void StajDohodForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.RepYear = _repYear;
        }

        #endregion

        #region Методы - обработчики событий

        private void _listsBS_ListChanged(object sender, ListChangedEventArgs e)
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
                copyToOrgListStripButton.Enabled = isEnabled;

                if (!isEnabled && _docsTable != null)
                    _docsTable.Clear();

                SetPrivilege(_privilege);
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void _listsBS_CurrentChanged(object sender, EventArgs e)
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

                SetPrivilege(_privilege);
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void _docsBS_ListChanged(object sender, ListChangedEventArgs e)
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

                SetPrivilege(_privilege);
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
                    bool allchecked = _docsBS.Cast<DataRowView>().All(row => (bool)row[Check]);
                    foreach (DataRowView row in _docsBS)
                        row[Check] = !allchecked;
                }
                docView.Refresh();
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
                if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == MouseButtons.Right)
                {
                    DataGridViewRow r = (sender as DataGridView).Rows[e.RowIndex];
                    //if (!r.Selected)
                    //{
                    r.DataGridView.ClearSelection();
                    r.DataGridView.CurrentCell = r.Cells[0];
                    r.Selected = true;
                    //}
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
                    if (currentCell == null)
                        return;
                    ContextMenuStrip cms = cmsLists;
                    if (cms == null)
                        return;
                    Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex,
                        currentCell.RowIndex, false);
                    Point p = new Point(r.Left, r.Top);

                    DataGridView dataView = sender as DataGridView;
                    ToolStripItem[] items; //Массив в который возвращает элементы метод Find
                    List<string> menuItems = new List<string>
                    {
                        "viewOpisMenuItem",
                        "calcMenuItem",
                        "printListMenuItem",
                        "copyToOtherYearMenuItem",
                        "moveToOtherYearMenuItem",
                        "copyToOtherOrgMenuItem",
                        "moveToOtherOrgMenuItem",
                        "delListMenuItem"
                    }; //Список элементов которые нужно включать\выключать

                    int currentMouseOverRow = dataView.CurrentCell.RowIndex;
                    bool isEnabled = !(currentMouseOverRow < 0);
                    foreach (string t in menuItems)
                    {
                        items = cms.Items.Find(t, false);
                        if (items.Any())
                            items[0].Enabled = isEnabled;
                    }

                    menuItems = new List<string>(); //Список элементов которые нужно принудительно выключать

                    // Проверка прав и отключение пунктов
                    bool readOnly = OperatorOrg.GetStajDohodDataAccesseCode(_privilege) == 1;
                    if (readOnly)
                    {
                        menuItems.Add("addListMenuItem");
                        menuItems.Add("copyToOtherYearMenuItem");
                        menuItems.Add("moveToOtherYearMenuItem");
                        menuItems.Add("copyToOtherOrgMenuItem");
                        menuItems.Add("moveToOtherOrgMenuItem");
                        menuItems.Add("delListMenuItem");
                    }

                    foreach (string t in menuItems)
                    {
                        items = cms.Items.Find(t, false);
                        if (items.Any())
                            items[0].Enabled = false;
                    }

                    cms.Show((sender as DataGridView), p);
                }

                if (e.KeyCode == Keys.Delete)
                {
                    bool readOnly = OperatorOrg.GetStajDohodDataAccesseCode(_privilege) == 1;
                    if (readOnly)
                    {
                        MainForm.ShowInfoMessage("У вас недостаточно прав. Обратитесь к администратору.", "Внимание");
                        return;
                    }
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
                    List<string> menuItems = new List<string>
                    {
                        "viewOpisMenuItem",
                        "calcMenuItem",
                        "printListMenuItem",
                        "copyToOtherYearMenuItem",
                        "moveToOtherYearMenuItem",
                        "copyToOtherOrgMenuItem",
                        "moveToOtherOrgMenuItem",
                        "delListMenuItem"
                    }; //Список элементов которые нужно включать\выключать

                    int currentMouseOverRow = dataView.HitTest(e.X, e.Y).RowIndex;
                    bool isEnabled = !(currentMouseOverRow < 0);
                    foreach (string t in menuItems)
                    {
                        items = menu.Items.Find(t, false);
                        if (items.Any())
                            items[0].Enabled = isEnabled;
                    }

                    menuItems = new List<string>(); //Список элементов которые нужно принудительно выключать

                    // Проверка прав и отключение пунктов
                    bool readOnly = OperatorOrg.GetStajDohodDataAccesseCode(_privilege) == 1;
                    if (readOnly)
                    {
                        menuItems.Add("addListMenuItem");
                        menuItems.Add("copyToOtherYearMenuItem");
                        menuItems.Add("moveToOtherYearMenuItem");
                        menuItems.Add("copyToOtherOrgMenuItem");
                        menuItems.Add("moveToOtherOrgMenuItem");
                        menuItems.Add("delListMenuItem");
                    }

                    foreach (string t in menuItems)
                    {
                        items = menu.Items.Find(t, false);
                        if (items.Any())
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
                if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == MouseButtons.Right)
                {
                    DataGridViewRow r = (sender as DataGridView).Rows[e.RowIndex];
                    //if (!r.Selected)
                    //{
                    r.DataGridView.ClearSelection();
                    r.DataGridView.CurrentCell = r.Cells[1];
                    r.Selected = true;
                    //}
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
                    if (currentCell == null)
                        return;
                    ContextMenuStrip cms = cmsLists;
                    if (cms == null)
                        return;
                    Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex,
                        currentCell.RowIndex, false);
                    Point p = new Point(r.Left, r.Top);

                    DataGridView dataView = sender as DataGridView;
                    ToolStripItem[] items; //Массив в который возвращает элементы метод Find
                    List<string> menuItems = new List<string>
                    {
                        "editDocMenuItem",
                        "changeTypeDocMenuItem",
                        "previewDocMenuItem",
                        "copyToOtherListMenuItem",
                        "moveToOtherListMenuItem",
                        "delDocMenuItem"
                    }; //Список элементов которые нужно включать\выключать

                    int currentMouseOverRow = dataView.CurrentCell.RowIndex;
                    bool isEnabled = !(currentMouseOverRow < 0);
                    foreach (string t in menuItems)
                    {
                        items = cms.Items.Find(t, false);
                        if (items.Any())
                            items[0].Enabled = isEnabled;
                    }

                    menuItems = new List<string>(); //Список элементов которые нужно принудительно выключать
                    //menuItems.Add("copyToOtherListMenuItem");

                    // Проверка прав и отключение пунктов
                    bool readOnly = OperatorOrg.GetStajDohodDataAccesseCode(_privilege) == 1;
                    if (readOnly)
                    {
                        menuItems.Add("addDocMenuItem");
                        menuItems.Add("editDocMenuItem");
                        menuItems.Add("delDocMenuItem");
                        menuItems.Add("changeTypeDocMenuItem");
                        menuItems.Add("copyToOtherListMenuItem");
                        menuItems.Add("moveToOtherListMenuItem");
                    }

                    foreach (string t in menuItems)
                    {
                        items = cms.Items.Find(t, false);
                        if (items.Any())
                            items[0].Enabled = false;
                    }

                    cms.Show((sender as DataGridView), p);
                }

                if (e.KeyCode == Keys.Delete)
                {
                    bool readOnly = OperatorOrg.GetStajDohodDataAccesseCode(_privilege) == 1;
                    if (readOnly)
                    {
                        MainForm.ShowInfoMessage("У вас недостаточно прав. Обратитесь к администратору.", "Внимание");
                        return;
                    }
                    delDocStripButton_Click(sender, e);
                }

                if (e.KeyCode == Keys.Enter)
                {
                    bool readOnly = OperatorOrg.GetStajDohodDataAccesseCode(_privilege) == 1;
                    if (readOnly)
                    {
                        MainForm.ShowInfoMessage("У вас недостаточно прав. Обратитесь к администратору.", "Внимание");
                        return;
                    }
                    editDocStripButton_Click(sender, e);
                }

                if (e.KeyCode == Keys.Space)
                {
                    if ((sender as DataGridView).CurrentRow == null)
                        return;

                    (_docsBS.Current as DataRowView)[Check] =
                        !Convert.ToBoolean((_docsBS.Current as DataRowView)[Check]);
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
                    List<string> menuItems = new List<string>
                    {
                        "editDocMenuItem",
                        "changeTypeDocMenuItem",
                        "previewDocMenuItem",
                        "copyToOtherListMenuItem",
                        "moveToOtherListMenuItem",
                        "delDocMenuItem"
                    }; //Список элементов которые нужно включать\выключать

                    int currentMouseOverRow = dataView.HitTest(e.X, e.Y).RowIndex;
                    bool isEnabled = !(currentMouseOverRow < 0);
                    foreach (string t in menuItems)
                    {
                        items = menu.Items.Find(t, false);
                        if (items.Any())
                            items[0].Enabled = isEnabled;
                    }

                    menuItems = new List<string>(); //Список элементов которые нужно принудительно выключать
                    //menuItems.Add("copyToOtherListMenuItem");

                    // Проверка прав и отключение пунктов
                    bool readOnly = OperatorOrg.GetStajDohodDataAccesseCode(_privilege) == 1;
                    if (readOnly)
                    {
                        menuItems.Add("addDocMenuItem");
                        menuItems.Add("editDocMenuItem");
                        menuItems.Add("delDocMenuItem");
                        menuItems.Add("changeTypeDocMenuItem");
                        menuItems.Add("copyToOtherListMenuItem");
                        menuItems.Add("moveToOtherListMenuItem");
                    }

                    foreach (string t in menuItems)
                    {
                        items = menu.Items.Find(t, false);
                        if (items.Any())
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

        private void viewOpisMenuItem_Click(object sender, EventArgs e)
        {
            reestrListStripButton_Click(sender, e);
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

        private void copyToOtherOrgMenuItem_Click(object sender, EventArgs e)
        {
            copyToOrgListStripButton_Click(sender, e);
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

        private void previewDocMenuItem_Click(object sender, EventArgs e)
        {
            previewDocStripButton_Click(sender, e);
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
                if (
                    MainForm.ShowQuestionMessage("Вы действительно хотите создать\nновый пакет документов СЗВ-1?",
                        "Создание пакета") == DialogResult.No)
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

                            command.CommandText = FixData.GetReplaceText(Lists.tablename, FixData.FixType.New, listId,
                                _operator.nameVal, DateTime.Now);
                            if ((long)command.ExecuteScalar() < 1)
                            {
                                throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename +
                                                          ".");
                            }
                        }
                        transaction.Commit();
                    }
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
                if (
                    MainForm.ShowQuestionMessage(
                        "Вы действительно хотите удалить выбранный пакет\nдокументов СЗВ-1 и все документы в нём?",
                        "Удаление пакета") == DialogResult.No)
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
            try
            {
                MyPrinter.ShowWebPage(Szv2Xml.GetHtml(_currentListId, _connection));
            }
            catch (Exception exception)
            {
                MainForm.ShowErrorFlexMessage(exception.Message, "Ошибка открытия предварительного просмотра");
            }
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

                if (copyPacketOtherYear.ShowDialog() == DialogResult.OK)
                {
                    if (
                        MainForm.ShowQuestionMessage(
                            "Вы действительно хотите копировать выбранный пакет\nдокументов СЗВ-1 № " + listId +
                            " и все документы в нём?", "Копирование пакета") == DialogResult.No)
                        return;

                    using (SQLiteConnection connection = new SQLiteConnection(_connection))
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        using (SQLiteTransaction transaction = connection.BeginTransaction())
                        {
                            CopyListToOtherYear(listId, NewRepYear, connection, transaction);
                            transaction.Commit();
                        }
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
                if (movePacketOtherYear.ShowDialog() == DialogResult.OK)
                {
                    if (
                        MainForm.ShowQuestionMessage(
                            "Вы действительно хотите переместить выбранный пакет\nдокументов СЗВ-1 № " + listId +
                            " и все документы в нём?", "Перемещение пакета") == DialogResult.No)
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
                                command.CommandText = FixData.GetReplaceText(Lists.tablename, FixData.FixType.Edit,
                                    listId, _operator.nameVal, DateTime.Now);
                                if ((long)command.ExecuteScalar() < 1)
                                {
                                    throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename +
                                                              ".");
                                }
                            }
                            transaction.Commit();
                        }
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
            try
            {
                if (listsView.CurrentRow == null)
                    return;
                long listId = (long)listsView.CurrentRow.Cells[Lists.id].Value;
                CopyPacketOtherOrgForm movePacketForm = new CopyPacketOtherOrgForm(_operator, _connection, listId);
                if (movePacketForm.ShowDialog() == DialogResult.OK)
                {
                    if (_operator.candeleteVal != 0)
                    {
                        string code = OperatorOrg.GetPrivilege(_operator.idVal, NewOrgId, _connection);
                        if (int.Parse(code[2].ToString()) != 2)
                        {
                            MainForm.ShowWarningFlexMessage(
                                "У вас недостаточно прав для копирования пакетов в выбранную организацию. Обратитесь к администратору.", "Копирование пакета");
                            return;
                        }
                    }

                    if (
                        MainForm.ShowQuestionMessage(
                            "Вы действительно хотите копировать выбранный пакет\nдокументов СЗВ-1 № " + listId +
                            " и все документы в нём?", "Копирование пакета") == DialogResult.No)
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

                                NewListId = CopyListToOtherYear(listId, _repYear, connection, transaction);
                                long res = FixData.ExecReplaceText(Lists.tablename, FixData.FixType.New, NewListId,
                                    _operator.nameVal, DateTime.Now, connection, transaction);
                                if (res < 1)
                                {
                                    throw new SQLiteException("Невозможно создать документ. Таблица " +
                                                              FixData.tablename + ".");
                                }

                                MoveListToOrg(NewListId, NewOrgId, connection, transaction);
                                transaction.Commit();
                            }
                        }
                    }
                    //Перезагрузка данных
                    ReloadLists();
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка копирования пакета");
            }
        }

        private void moveToOrgListStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (listsView.CurrentRow == null)
                    return;
                long listId = (long)listsView.CurrentRow.Cells[Lists.id].Value;
                MovePacketOtherOrgForm movePacketForm = new MovePacketOtherOrgForm(_operator, _connection, listId);
                if (movePacketForm.ShowDialog() == DialogResult.OK)
                {
                    if (_operator.candeleteVal != 0)
                    {
                        string code = OperatorOrg.GetPrivilege(_operator.idVal, NewOrgId, _connection);
                        if (int.Parse(code[2].ToString()) != 2)
                        {
                            MainForm.ShowWarningFlexMessage(
                                "У вас недостаточно прав для перемещения пакетов в выбранную организацию. Обратитесь к администратору.", "Копирование пакета");
                            return;
                        }
                    }

                    if (
                        MainForm.ShowQuestionMessage(
                            "Вы действительно хотите переместить выбранный пакет\nдокументов СЗВ-1 № " + listId +
                            " и все документы в нём?", "Перемещение пакета") == DialogResult.No)
                        return;

                    using (SQLiteConnection connection = new SQLiteConnection(_connection))
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        using (SQLiteTransaction transaction = connection.BeginTransaction())
                        {
                            MoveListToOrg(listId, NewOrgId, connection, transaction);
                            transaction.Commit();
                        }
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
                if (_docsBS.Count >= 200)
                {
                    MainForm.ShowInfoFlexMessage(
                        "В данном пакете находится 200 документов СЗВ-1,\nдля добавления новых документов создайте новый пакет.",
                        "Добавление документа(ов)");
                    return;
                }
                ChoicePersonForm choicePersonForm = new ChoicePersonForm(_organization, _repYear, _currentListId,
                    _connection);
                if (choicePersonForm.ShowDialog() == DialogResult.OK)
                {
                    AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form(_organization, _operator,
                        _currentListId, _repYear, PersonId, FlagDoc, _connection);
                    if (szv1Form.ShowDialog() == DialogResult.OK)
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
                AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form(_organization, _operator, _currentListId,
                    _repYear, PersonId, FlagDoc, _connection, docId);
                if (szv1Form.ShowDialog() == DialogResult.OK)
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

                (_docsBS.Current as DataRowView)[Check] = true;
                docView.EndEdit();
                docView.Refresh();

                List<long> docIdList = GetSelectedDocIds();
                if (docIdList.Count < 1)
                    if (_docsBS.Current != null)
                    {
                        (_docsBS.Current as DataRowView)[Check] = true;
                        docIdList = GetSelectedDocIds();
                    }

                string listFio = GetFioForSelectedDocIds(docIdList);

                if (
                    MainForm.ShowQuestionFlexMessage(
                        "Вы действительно хотите удалить выбранные документы СЗВ-1?\nКоличество выбранных документов: " +
                        docIdList.Count + "\n" + listFio, "Удаление документа(ов)") == DialogResult.No)
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
                }
                //Перезагрузка данных
                int position;
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

            (_docsBS.Current as DataRowView)[Check] = true;
            docView.EndEdit();
            docView.Refresh();

            List<long> docIdList = GetSelectedDocIds();
            if (docIdList.Count < 1)
                if (_docsBS.Current != null)
                {
                    (_docsBS.Current as DataRowView)[Check] = true;
                    docIdList = GetSelectedDocIds();
                }

            ReplaceDocTypeForm replaceDocTypeForm = new ReplaceDocTypeForm(_connection);
            if (replaceDocTypeForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string listFio = GetFioForSelectedDocIds(docIdList);

                    if (
                        MainForm.ShowQuestionFlexMessage(
                            "Вы действительно хотите изменить тип у выбранных документов СЗВ-1?\nКоличество выбранных документов: " +
                            docIdList.Count + "\n" + listFio, "Замена типа формы документа(ов) СЗВ-1") ==
                        DialogResult.No)
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

                                Docs.UpdateDocTypeByDocId(docIdList, NewDocTypeId, connection, transaction);
                                foreach (long docId in docIdList)
                                {
                                    command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit,
                                        docId, _operator.nameVal, DateTime.Now);
                                    if ((long)command.ExecuteScalar() < 1)
                                    {
                                        throw new SQLiteException("Невозможно создать запись. Таблица " +
                                                                  FixData.tablename + ".");
                                    }
                                }
                            }
                            transaction.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MainForm.ShowErrorFlexMessage("Не удалось изменить тип документа(ов).\n" + ex.Message,
                        "Ошибка изменения типа документа(ов)");
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
            //PrintStajForm printStajForm = new PrintStajForm();
            //if (printStajForm.ShowDialog() == DialogResult.OK)
            //{ }
        }

        private void previewDocStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_docsBS.Current == null)
                    return;
                long docId = (long)(_docsBS.Current as DataRowView)[DocsView.id];
                MyPrinter.ShowWebPage(Szv1Xml.GetHtml(docId, _organization, _connection));
            }
            catch (Exception exception)
            {
                MainForm.ShowErrorFlexMessage(exception.Message, "Ошибка открытия предварительного просмотра");
            }
        }

        private void copyToListDocStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (docView.CurrentRow == null)
                    return;

                (_docsBS.Current as DataRowView)[Check] = true;
                docView.EndEdit();
                docView.Refresh();

                List<long> docIdList = GetSelectedDocIds();
                if (docIdList.Count < 1)
                    if (_docsBS.Current != null)
                    {
                        (_docsBS.Current as DataRowView)[Check] = true;
                        docIdList = GetSelectedDocIds();
                    }

                string listFio = GetFioForSelectedDocIds(docIdList);

                CopyDocumentForm copyDocForm = new CopyDocumentForm(_organization, _repYear, _connection);
                if (copyDocForm.ShowDialog() == DialogResult.OK)
                {
                    int docCount = DocsView.GetCountDocsInList(NewListId, _connection);
                    if (docIdList.Count + docCount > 200)
                    {
                        MainForm.ShowInfoFlexMessage(
                            "Невозможно скопировать выбранные документы СЗВ-1,\nтак как количество документов в пакете превысит 200!",
                            "Копирование документа(ов) СЗВ-1");
                        return;
                    }

                    if (
                        MainForm.ShowQuestionFlexMessage(
                            string.Format(
                                "Вы действительно хотите копировать выбранные документы СЗВ-1?\nКоличество выбранных документов: " +
                                docIdList.Count + "\n" + listFio), "Копирование документа(ов) СЗВ-1") == DialogResult.No)
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
                    }
                    MainForm.ShowInfoFlexMessage("Копирование документов успешно завершено!",
                        "Копирование документа(ов)");
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

                (_docsBS.Current as DataRowView)[Check] = true;
                docView.EndEdit();
                docView.Refresh();

                List<long> docIdList = GetSelectedDocIds();
                if (docIdList.Count < 1)
                    if (_docsBS.Current != null)
                    {
                        (_docsBS.Current as DataRowView)[Check] = true;
                        docIdList = GetSelectedDocIds();
                    }

                string listFio = GetFioForSelectedDocIds(docIdList);
                //long docId = 0;
                //DataRowView row = _docsBS.Current as DataRowView;
                //if (row == null)
                //    return;
                //docId = (long)row[Docs.id];

                MoveDocumentForm moveDocForm = new MoveDocumentForm(_organization, _repYear, _connection);
                if (moveDocForm.ShowDialog() == DialogResult.OK)
                {
                    int docCount = DocsView.GetCountDocsInList(NewListId, _connection);
                    if (docIdList.Count + docCount > 200)
                    {
                        MainForm.ShowInfoFlexMessage(
                            "Невозможно переместить выбранные документы СЗВ-1,\nтак как количество документов в пакете превысит 200!",
                            "Перемещение документа(ов) СЗВ-1");
                        return;
                    }

                    if (
                        MainForm.ShowQuestionFlexMessage(
                            "Вы действительно хотите переместить выбранные документы СЗВ-1?\nКоличество выбранных документов: " +
                            docIdList.Count + "\n" + listFio, "Перемещение документа(ов) СЗВ-1") == DialogResult.No)
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
                                    command.CommandText = FixData.GetReplaceText(Docs.tablename, FixData.FixType.Edit,
                                        docId, _operator.nameVal, DateTime.Now);
                                    if ((long)command.ExecuteScalar() < 1)
                                    {
                                        throw new SQLiteException("Невозможно создать запись. Таблица " +
                                                                  FixData.tablename + ".");
                                    }
                                }
                            }
                            transaction.Commit();
                        }
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
            docView.EndEdit();
            return (from DataRowView docRow in _docsBS where (bool)docRow[Check] select (long)docRow["id"]).ToList();
        }

        private string GetFioForSelectedDocIds(IEnumerable<long> docIdList)
        {
            StringBuilder builder = new StringBuilder();
            //Формирование списка выбранных документов
            foreach (var item in docIdList)
            {
                int position = _docsBS.Find(DocsView.id, item);
                builder.AppendFormat("\n{0} {1}", (_docsBS[position] as DataRowView)[DocsView.fio].ToString(),
                    (_docsBS[position] as DataRowView)[DocsView.socNumber].ToString());
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

            SetPrivilege(_privilege);
        }

        private void CopyDocsByDocId(IEnumerable<long> docIdList, long newListId, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            foreach (long oldDocId in docIdList)
            {
                //Сохранение в таблицу Doc
                long newDocId = Docs.CopyDocByDocId(oldDocId, newListId, connection, transaction);
                if (newDocId < 1)
                    throw new SQLiteException("Невозможно создать документ.");

                long res;
                //Сохранение в таблицу Fixdata
                res = FixData.ExecReplaceText(Docs.tablename, FixData.FixType.New, newDocId, _operator.nameVal,
                    DateTime.Now, connection, transaction);
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
                GeneralPeriod.CopyPeriodByDocId(oldDocId, newDocId, connection, transaction);
                //Сохранение в таблицу Dop_period
                DopPeriod.CopyPeriodByDocId(oldDocId, newDocId, connection, transaction);
                //Сохранение в таблицу Spec_period
                SpecialPeriod.CopyPeriodByDocId(oldDocId, newDocId, connection, transaction);
                //Сохранение в таблицу Salary_Info
                res = SalaryInfo.CopySalaryInfoByDocId(oldDocId, newDocId, connection, transaction);
                if (res < 1)
                {
                    throw new SQLiteException("Невозможно создать документ. Таблица " + SalaryInfo.tablename + ".");
                }
            }
        }

        private void MoveListToOrg(long listId, long newOrgId, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = Lists.GetSelectPersonIdsText(listId);
                List<long> personIdList = new List<long>();
                using (SQLiteDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        personIdList.Add((long)reader[Docs.personID]);
                    }
                //Привязка всех персон к новой организации
                PersonOrg.InsertPersonOrg(personIdList, newOrgId, connection, transaction);
                command.CommandText = Lists.GetUpdateOrgText(listId, newOrgId);
                if (command.ExecuteNonQuery() < 1)
                {
                    throw new SQLiteException("Не удалось переместить пакет.");
                }
                command.CommandText = FixData.GetReplaceText(Lists.tablename, FixData.FixType.Edit, listId,
                    _operator.nameVal, DateTime.Now);
                if ((long)command.ExecuteScalar() < 1)
                {
                    throw new SQLiteException("Невозможно создать запись. Таблица " + FixData.tablename + ".");
                }
            }
        }

        private long CopyListToOtherYear(long listId, int newRepYear, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            long newListId;
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                newListId = Lists.CopyListById(listId, connection, transaction);
                command.CommandText = Docs.GetSelectText(listId);

                List<long> docIdList = new List<long>();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    docIdList.Add(Convert.ToInt64(reader[Docs.id]));
                }

                CopyDocsByDocId(docIdList, newListId, connection, transaction);

                Lists.UpdateYear(newListId, newRepYear, connection, transaction);

                //Сохранение в таблицу Fixdata
                long res = FixData.ExecReplaceText(Lists.tablename, FixData.FixType.New, newListId, _operator.nameVal,
                    DateTime.Now, connection, transaction);
                if (res < 1)
                {
                    throw new SQLiteException("Невозможно создать документ. Таблица " + FixData.tablename + ".");
                }
            }
            return newListId;
        }

        #endregion
    }
}