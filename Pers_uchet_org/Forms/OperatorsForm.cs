using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class OperatorsForm : Form
    {
        /*
         * Уровень доступа оператора к данным определяется строкой из цифр, где
         * 0 цифра - уровень доступа к анкетным данным (0,1,2)
         * 1 цифра - доступ к печати анкетных данных (0,1)
         * 2 цифра - уровень доступа к стажу и доходу (0,1,2)
         * 3 цифра - доступ к печати стажа и дохода (0,1)
         * 4 цифра - доступ к электронному обмену с ЕГФСС (0,1)
         * 5 цифра - доступ к импорту анкет из других организаций, даже из тех, к которым оператор не имеет полного досупа (0,1)
         * 
         * Уровень досупа бывает:
         * без доступа = 0
         * только чтение \ есть доступ = 1
         * полный доступ = 2
         */

        #region Поля

        // строка подключения к БД
        private string _connectionStr;

        // Адаптеры
        private SQLiteDataAdapter _orgAdapter;
        private SQLiteDataAdapter _operatorAdapter;
        private SQLiteDataAdapter _operatororgAdapter;

        // Таблицы с данными
        private DataTable _operatorTable;
        private DataTable _orgTable;
        private DataTable _operOrgTable;

        // чисто виртуальная таблица, без соответствия в БД (введена для удобства)
        private DataTable _accessTable;

        // контролеры для источников данных (таблиц)
        private BindingSource _operatorBS;
        private BindingSource _orgBS;
        private BindingSource _operOrgBS;

        private BindingSource _anketaAccessBS;
        private BindingSource _paystajAccessBS;

        // названия столбцов для удобства использования
        private const string Check = "check";
        private const string ANumber = "access_number";
        private const string AName = "access_name";

        // количество цифр в коде приввелегии (расшифровка кода сверху в комментариях)
        private const int CodeLength = 6;
        private string _adminCode;
        private string _emptyCode;

        #endregion

        #region Конструктор и инициализатор

        public OperatorsForm(string connection)
        {
            InitializeComponent();
            _connectionStr = connection;

            _adminCode = OperatorOrg.GetPrivilegeForAdmin(); //"212111";
            _emptyCode = new string('0', CodeLength);

            // создание объектов таблиц
            _operatorTable = Operator.CreateTable();

            _orgTable = Org.CreateTable();
            // добавление виртуального столбца, для возможности отмечать
            _orgTable.Columns.Add(Check, typeof(bool));
            _orgTable.Columns[Check].DefaultValue = false;

            _operOrgTable = OperatorOrg.CreateTable();
            // добавление виртуального столбца, для возможности отмечать
            _operOrgTable.Columns.Add(Check, typeof(bool));
            _operOrgTable.Columns[Check].DefaultValue = false;

            // создание виртуальной таблицы для хранения уровня
            _accessTable = new DataTable();
            // добавление необходимых столбцов
            _accessTable.Columns.Add(ANumber, typeof(int));
            _accessTable.Columns.Add(AName, typeof(string));
            // заполнение данными виртуальной таблицы привелегий
            DataRow row;
            //row = _accessTable.NewRow();
            //row[aNumber] = 0;
            //row[aName] = "Без доступа";
            //row.EndEdit();
            //_accessTable.Rows.Add(row);

            row = _accessTable.NewRow();
            row[ANumber] = 1;
            row[AName] = "Только чтение";
            row.EndEdit();
            _accessTable.Rows.Add(row);

            row = _accessTable.NewRow();
            row[ANumber] = 2;
            row[AName] = "Полный доступ";
            row.EndEdit();
            _accessTable.Rows.Add(row);

            _accessTable.AcceptChanges();

            // создание объектов контроллеров
            _operatorBS = new BindingSource();
            _orgBS = new BindingSource();
            _operOrgBS = new BindingSource();
            _anketaAccessBS = new BindingSource();
            _paystajAccessBS = new BindingSource();

            // инициализация адаптеров для выполнения запросов нахаполнения таблиц
            _operatorAdapter = Operator.CreateAdapter(_connectionStr);
            _orgAdapter = Org.CreateAdapter(_connectionStr);
            _operatororgAdapter = OperatorOrg.CreateAdapter(_connectionStr);

            // привязка таблиц к контроллерам (биндинг сорсам)
            _operatorBS.DataSource = _operatorTable;
            _orgBS.DataSource = _orgTable;
            _operOrgBS.DataSource = _operOrgTable;
            _anketaAccessBS.DataSource = new DataView(_accessTable);
            _paystajAccessBS.DataSource = new DataView(_accessTable);
        }

        private void OperatorsForm_Load(object sender, EventArgs e)
        {
            // заполение таблиц данными из БД
            _operatorAdapter.Fill(_operatorTable);
            _orgAdapter.Fill(_orgTable);
            _operatororgAdapter.Fill(_operOrgTable);

            // добавить записи пересечений операторов и организаций для указания привелегий, если по каким-то причином они не созданы
            foreach (DataRowView itemRow in _operatorBS)
            {
                AddRelationsIfNeed(itemRow, _orgBS, _operOrgBS);
            }

            // Устанавливаем поле CHECK в значение true если код не пустой и наоборот
            foreach (DataRow itemRow in _operOrgTable.Rows)
            {
                itemRow[Check] = (itemRow[OperatorOrg.code] as string).Replace("0", "").Length > 0;
                itemRow.EndEdit();
            }

            // инициализация обработчиков событий смены оператора и выбора организации
            _operatorBS.CurrentChanged += new EventHandler(_operatorBS_CurrentChanged);
            _orgBS.CurrentChanged += new EventHandler(_orgBS_CurrentChanged);

            // привязка контроллеров к вьюшкам
            // привязка к выпадающему списку операторов 
            operatorBox.DataSource = _operatorBS;
            operatorBox.DisplayMember = Operator.name;
            // привязка к таблице органазаций
            orgView.AutoGenerateColumns = false;
            orgView.DataSource = _orgBS;
            // привязка к выпадающим спискам уровня доступа
            anketaAccessLevelBox.DataSource = _anketaAccessBS;
            anketaAccessLevelBox.DisplayMember = AName;
            payStajAccessLevelBox.DataSource = _paystajAccessBS;
            payStajAccessLevelBox.DisplayMember = AName;

            //// выбирается первый оператор
            //_operatorBS.MoveLast();
            //_operatorBS.MoveFirst();

            _operatorBS_CurrentChanged(null, null);
            //anketaaccessCheckBox_CheckedChanged(null, null);
            //paystajaccessCheckBox_CheckedChanged(null, null);
        }

        #endregion

        #region Методы - свои

        /// <summary>
        /// Получить наибольшее абсолютное значение ключевого поля
        /// </summary>
        /// <param name="table">Таблица, в которой будет производиться поиск</param>
        /// <param name="idname">Название ключевого столбца</param>
        /// <returns></returns>
        private long GetMaxID(DataTable table, string idname)
        {
            long res = -1;
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    long val = Math.Abs((long)row[idname]);
                    if (res < val)
                        res = val;
                }
            }
            return res;
        }

        /// <summary>
        /// Получить код превилегий по значениям полей формы (чекбоксов и выпадающих списков)
        /// </summary>
        /// <returns></returns>
        private string GetPrivilegeCode()
        {
            char[] res = new char[CodeLength];

            bool b0 = anketaAccessCheckBox.Checked;
            //b1 = this.anketaPrintCheckBox.Checked;
            bool b1 = true;
            int a0 = _anketaAccessBS.Position + 1;
            bool b2 = payStajAccessCheckBox.Checked;
            //b3 = this.payStajPrintCheckBox.Checked;
            bool b3 = true;
            int a2 = _paystajAccessBS.Position + 1;
            bool b4 = exchangeDataCheckBox.Checked;
            bool b5 = importAnketaCheckBox.Checked;

            if (!b0)
            {
                a0 = 0;
                b1 = false;
            }
            if (!b2)
            {
                a2 = 0;
                b3 = false;
            }

            res[0] = a0.ToString()[0];
            res[1] = Convert.ToInt32(b1).ToString()[0];
            res[2] = a2.ToString()[0];
            res[3] = Convert.ToInt32(b3).ToString()[0];
            res[4] = Convert.ToInt32(b4).ToString()[0];
            res[5] = Convert.ToInt32(b5).ToString()[0];

            return new string(res);
        }

        /// <summary>
        /// Установить значения полей в соответствии с указанным кодом превилегии
        /// </summary>
        /// <param name="accessCode">Код превилегии (длина должна быть 6 или более)</param>
        private void SetPrivilegeCode(string accessCode)
        {
            accessCode = accessCode.Trim();
            if (accessCode.Length < CodeLength)
            {
                anketaAccessCheckBox.Checked = false;
                anketaPrintCheckBox.Checked = false;
                _anketaAccessBS.Position = 0;
                payStajAccessCheckBox.Checked = false;
                payStajPrintCheckBox.Checked = false;
                _paystajAccessBS.Position = 0;
                exchangeDataCheckBox.Checked = false;
                importAnketaCheckBox.Checked = false;
                return;
            }

            int a0 = int.Parse(accessCode[0].ToString());
            int a2 = int.Parse(accessCode[2].ToString());

            anketaAccessCheckBox.Checked = a0 > 0;
            _anketaAccessBS.Position = a0 - 1;
            anketaPrintCheckBox.Checked = int.Parse(accessCode[1].ToString()) > 0;
            payStajAccessCheckBox.Checked = a2 > 0;
            _paystajAccessBS.Position = a2 - 1;
            payStajPrintCheckBox.Checked = int.Parse(accessCode[3].ToString()) > 0;
            exchangeDataCheckBox.Checked = int.Parse(accessCode[4].ToString()) > 0;
            importAnketaCheckBox.Checked = int.Parse(accessCode[5].ToString()) > 0;
        }

        /// <summary>
        /// Добавляет отношения Оператор-Организация,которых не хватает до полноты
        /// </summary>
        /// <param name="operRow">Строка с данными об опрераторе</param>
        /// <param name="orgBS"> Биндинг сорс с привязкой к таблице организацый</param>
        /// <param name="operOrgRelationBS">Биндинг сорс с привязкой к к таблице с данными отношения Операторов и Организаций</param>
        private void AddRelationsIfNeed(DataRowView operRow, BindingSource orgBS, BindingSource operOrgRelationBS)
        {
            string oldFilter = operOrgRelationBS.Filter;
            int oldPos = operOrgRelationBS.Position;
            operOrgRelationBS.Filter = string.Format("{0} = {1}", OperatorOrg.operatorID, operRow[Operator.id]);
            string priveledgeCode = IsAdminCurrent(operRow) ? _adminCode : new string('0', CodeLength);
            foreach (DataRowView orgRow in orgBS)
                if (operOrgRelationBS.Find(OperatorOrg.orgID, orgRow[Org.id]) <= -1)
                {
                    long id = GetMaxID(operOrgRelationBS.DataSource as DataTable, OperatorOrg.id);
                    id = (id == -1) ? 1 : id + 1;
                    DataRowView newRow = operOrgRelationBS.AddNew() as DataRowView;
                    newRow.BeginEdit();
                    newRow[OperatorOrg.id] = id;
                    newRow[OperatorOrg.operatorID] = operRow[Operator.id];
                    newRow[OperatorOrg.orgID] = orgRow[Org.id];
                    newRow[OperatorOrg.code] = priveledgeCode;
                    newRow.EndEdit();
                }
            operOrgRelationBS.Filter = oldFilter;
            operOrgRelationBS.Position = oldPos;
        }

        /// <summary>
        /// Проставить галочки в поле Check для организаций, для указанного оператора
        /// </summary>
        private void SetOrgsChecked(DataRowView operRow)
        {
            string oldFilter = _operOrgBS.Filter;
            int oldPos = _operOrgBS.Position;
            _operOrgBS.RemoveFilter();
            //int ipos;
            // проставить галочки рядом с названиями организациями
            foreach (DataRowView row in _orgBS)
            {
                //ipos = _operOrgBS.Find(OperatorOrg.orgID, row[Org.id]);
                //// убрать галочку, если привилегии на организацию нет
                //if (ipos <= -1)
                //    row[CHECK] = false;
                //else
                //{
                //    DataRowView operOrgRow = _operOrgBS[ipos] as DataRowView;
                //    row[CHECK] = operOrgRow[CHECK];
                //    row.EndEdit();
                //}
                row[Check] = false;
                foreach (DataRowView operatorOrgRow in _operOrgBS)
                {
                    if (operatorOrgRow[OperatorOrg.orgID].ToString() == row[Org.id].ToString()
                        && operatorOrgRow[OperatorOrg.operatorID].ToString() == operRow[Operator.id].ToString())
                    {
                        bool isChecked = false;
                        //isChecked |= (row[OperatorOrg.code] as string).Replace("0", "").Length > 0;
                        isChecked |= (bool)operatorOrgRow[Check];
                        row[Check] = isChecked;
                        row.EndEdit();
                    }
                }
            }
            _operOrgBS.Filter = oldFilter;
            _operOrgBS.Position = oldPos;
        }

        private void SetOrgsCheckedAll()
        {
            // проставить галочки рядом с названиями организациями
            foreach (DataRowView row in _orgBS)
            {
                row[Check] = true;
            }
            _orgBS.EndEdit();
        }

        private void SetOrgsUncheckedAll()
        {
            // проставить галочки рядом с названиями организациями
            foreach (DataRowView row in _orgBS)
            {
                row[Check] = false;
            }
            _orgBS.EndEdit();
        }

        /// <summary>
        /// Установить новый идентификатор (ID) для оператора
        /// в записи об операторе и связующей таблице между операторами и организациями
        /// </summary>
        /// <param name="newOperatorID">Новый идентификатор оператора</param>
        /// <param name="operRow">Строка записи оператора</param>
        /// <param name="operOrgRelationBS">Биндинг связующей таблицы между оператами и организациями</param>
        private void ChangeOperatorId(long newOperatorID, DataRowView operRow, BindingSource operOrgRelationBS)
        {
            int oldPos = operOrgRelationBS.Position;
            string oldFilter = operOrgRelationBS.Filter;

            operOrgRelationBS.Filter = string.Format("{0} = {1}", OperatorOrg.operatorID, operRow[Operator.id]);

            operRow.BeginEdit();
            operRow[Operator.id] = newOperatorID;
            operRow.EndEdit();

            foreach (DataRowView itemRow in operOrgRelationBS)
            {
                itemRow.BeginEdit();
                itemRow[OperatorOrg.operatorID] = newOperatorID;
                itemRow.EndEdit();
            }
            operOrgRelationBS.Filter = oldFilter;
            operOrgRelationBS.Position = oldPos;
        }

        /// <summary>
        /// Записать код привелегии в текущую запись OperOrg
        /// </summary>
        private void OperOrgRowCurrentEndEdit()
        {
            DataRowView operOrgRow = _operOrgBS.Current as DataRowView;
            // если запись не нулевая , то нужно сохранить изменения
            if (operOrgRow != null)
            {
                // считывание и получение кода
                string codeStr = GetPrivilegeCode();
                // запись кода
                operOrgRow[OperatorOrg.code] = codeStr;
                operOrgRow.EndEdit();
            }
        }

        private void SetOperOrgPosition(long orgID, long operId)
        {
            // установить фильтр по оператору
            _operOrgBS.Filter = string.Format("{0} = {1}", OperatorOrg.operatorID, operId);
            // получить позицию записи с приаелегией для текущей записи организации
            int ipos = _operOrgBS.Find(OperatorOrg.orgID, orgID);
            // установить полученную позицию
            _operOrgBS.Position = ipos;
            // получение ссылки на новую активную запись с кодом привелегии
            DataRowView operOrgRow = _operOrgBS.Current as DataRowView;
            // если ссылка не пустая, получить сам код привелегии
            string code;
            if (operOrgRow != null)
                code = operOrgRow[OperatorOrg.code] as string;
            else // иначе создать код привелегии указывающий об отсутствии доступа
                code = _emptyCode;
            // отобразить код привелегии на форме
            SetPrivilegeCode(code);
            accessGroupBox.Enabled = (bool)operOrgRow[Check];
        }

        private bool IsAdminCurrent(DataRowView operRow)
        {
            return (operRow != null && (int)operRow[Operator.candelete] == 0);
        }

        #endregion

        #region Методы - обработчики событий

        private void _operatorBS_CurrentChanged(object sender, EventArgs e)
        {
            // получение ссылки на текущую запись оператора
            DataRowView operRow = _operatorBS.Current as DataRowView;
            // если запись не выбрана, то выйти из метода
            if (operRow == null)
            {
                orgView.Enabled = false;
                return;
            }
            // получить текущую запись о пересечении выбранного оператора и выбранной организации
            DataRowView operOrgRow = _operOrgBS.Current as DataRowView;
            // если запись не нулевая и соответствует другому оператору, то нужно сохранить изменения
            if (operOrgRow != null && (long)operOrgRow[OperatorOrg.operatorID] != (long)operRow[Operator.id])
            {
                OperOrgRowCurrentEndEdit();
            }

            // добавить записи пересечений операторов и организаций для указания привелегий.
            AddRelationsIfNeed(operRow, _orgBS, _operOrgBS);

            // получить выбранную запись организации
            DataRowView orgRow = _orgBS.Current as DataRowView;
            // если текущая выбранная запись организации не пустая, то нужно отобразить значение привелегии на форме
            if (orgRow != null)
            {
                SetOperOrgPosition((long)orgRow[Org.id], (long)operRow[Operator.id]);
            }
            // проставить галочки рядом с названиями организациями
            SetOrgsChecked(operRow);

            if (IsAdminCurrent(operRow))
            {
                //SetOrgsCheckedAll();
                //this.setPrivilegeCode(adminCode);
                orgView.ReadOnly = true;
                accessGroupBox.Enabled = false;
                removeButton.Enabled = false;
                editButton.Enabled = false;
            }
            else
            {
                orgView.ReadOnly = false;
                regNumColumn.ReadOnly = true;
                orgNameColumn.ReadOnly = true;
                removeButton.Enabled = true;
                editButton.Enabled = true;
            }
        }

        private void _orgBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView operRow = _operatorBS.Current as DataRowView;

            // получить ссылку на запись текущей выбранной организации
            DataRowView orgRow = _orgBS.Current as DataRowView;
            // если ссылка пуста, то завершить метод
            if (orgRow == null)
            {
                accessGroupBox.Enabled = false;
                return;
            }
            // установить флаг активности в соответствии с флагом огранизации
            accessGroupBox.Enabled = (bool)(_orgBS.Current as DataRowView)[Check];

            OperOrgRowCurrentEndEdit();

            SetOperOrgPosition((long)orgRow[Org.id], (long)operRow[Operator.id]);

            if (IsAdminCurrent(operRow))
            {
                //this.setPrivilegeCode(adminCode);
                accessGroupBox.Enabled = false;
                //return;
            }
        }

        private void orgView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // проверить, является ли кликнутая ячейка, ячейкой с флагом
            if (e.ColumnIndex == orgCheckColumn.Index)
            {
                DataRowView operRow = _operatorBS.Current as DataRowView;
                // если текущий оператор - Администратор, то ничего не делать и прекратить выполнение метода
                if (IsAdminCurrent(operRow))
                    return;
                // принять изменение ячейки, чтобы флаг поменялся и послались изменения в контролер.
                orgView.EndEdit();
                // установить флаг активности в соответствии с флагом огранизации
                bool b = (_orgBS.Current as DataRowView) != null && (bool)(_orgBS.Current as DataRowView)[Check];
                accessGroupBox.Enabled = b;
                DataRowView operOrgRow = _operOrgBS.Current as DataRowView;
                if (operOrgRow != null)
                {
                    operOrgRow[Check] = b;
                    operOrgRow[OperatorOrg.code] = GetPrivilegeCode();
                    operOrgRow.EndEdit();
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // создать форму для внесения данных об операторе (Имя оператора)
            OperatorsEditPersonForm tmpForm = new OperatorsEditPersonForm();
            tmpForm.Owner = this;
            // отображаем форму как диалог
            DialogResult dRes = tmpForm.ShowDialog(this);
            // если пользователь ввел имя (завершил работу с диалогом с утвердительным ответом)
            if (dRes == DialogResult.OK)
            {
                // получение данных с формы-диалога
                string operName = tmpForm.OperatorName.Trim();
                if (_operatorBS.Find(Operator.name, operName) > 0)
                {
                    MainForm.ShowInfoMessage("Пользователь с таким именем уже существует!",
                        "Ошибка добавления оператора");
                    return;
                }

                // пароль пока пустой
                string operPassword = "";
                //создание формы для указания пароля
                ChangePasswordForm changeForm = new ChangePasswordForm();
                changeForm.Owner = this;
                changeForm.PasswordOld = operPassword;
                // отобразить форму для диалога
                dRes = changeForm.ShowDialog(this);
                // если пользователь ввел пароль (завершил работу с диалогом с утвердительным ответом)
                if (dRes == DialogResult.OK)
                {
                    // считывается введенный пароль
                    operPassword = changeForm.Password;
                    // создание новой строки оператора
                    DataRow newOperRow = _operatorTable.NewRow();
                    //начать редактирование
                    newOperRow.BeginEdit();
                    // введение данных в запись
                    newOperRow[Operator.id] = -(GetMaxID(_operatorTable, Operator.id) + 1);
                    newOperRow[Operator.name] = operName;
                    newOperRow[Operator.password] = operPassword;
                    newOperRow[Operator.candelete] = 1;
                    // принять редактирование
                    newOperRow.EndEdit();
                    // добавить запись в таблицу
                    _operatorTable.Rows.Add(newOperRow);
                    // сделать активной добавленную запись
                    _operatorBS.Position = _operatorBS.Find(Operator.id, newOperRow[Operator.id]);
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            // получить ссылку на активную запись оператора
            DataRowView oper = _operatorBS.Current as DataRowView;
            // если ссылка пустая, то выдать сообщение и завершить метод
            if (oper == null)
            {
                MainForm.ShowInfoMessage("Сначала необходимо выбрать оператора", "Неопределен оператор");
                return;
            }
            // если оператор является Администратором, выдать сообщение и прекратить выполнение
            if (0 == (int)oper[Operator.candelete])
            {
                MainForm.ShowInfoMessage("Запрешено изменять имя оператора!", "Внимание");
                return;
            }
            // создать форму ввода данных об операторе (имя)
            OperatorsEditPersonForm tmpForm = new OperatorsEditPersonForm();
            tmpForm.Owner = this;
            // открыть форму
            DialogResult dRes = tmpForm.ShowDialog(this);
            // если введены данные (результат диалога утвердительный)
            if (dRes == DialogResult.OK)
            {
                // поменять имя оператора
                oper[Operator.name] = tmpForm.OperatorName;
                oper.EndEdit();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            // получить ссылку на активную запись об операторе
            DataRowView operRow = _operatorBS.Current as DataRowView;
            // если ссылка пустая, выдать сообщение и прекратить выполнение
            if (operRow == null)
            {
                MainForm.ShowInfoMessage("Сначала необходимо выбрать оператора!", "Внимание");
                return;
            }
            // если оператор является Администратором, выдать сообщение и прекратить выполнение
            if (0 == (int)operRow[Operator.candelete])
            {
                MainForm.ShowInfoMessage("Запрещено удалять администратора!", "Внимание");
                return;
            }
            // применить фильтр по оператору к записям с привелегиями
            _operOrgBS.Filter = string.Format("{0} = {1}", OperatorOrg.operatorID, operRow[Operator.id]);
            // пометить как удаленные все отфильтрованные записи
            foreach (DataRowView item in _operOrgBS)
            {
                item.Delete();
            }
            // пометить как удаленную запись об операторе
            operRow.Delete();
        }

        private void changepasswordButton_Click(object sender, EventArgs e)
        {
            // получить ссылку на активную запись "Оператора"
            DataRowView oper = _operatorBS.Current as DataRowView;
            // если ссылка пустая, выдать сообщение и прекратить выполнение метода
            if (oper == null)
            {
                MainForm.ShowInfoMessage("Сначала необходимо выбрать оператора", "Неопределен оператор");
                return;
            }

            ChangePasswordForm tmpForm = new ChangePasswordForm();
            // если оператор является Администратором, передать текущий пароль диалоговой форме
            if (0 == (int)oper[Operator.candelete])
                tmpForm.PasswordOld = oper[Operator.password] as string;
            else
                // иначе можно менять пароль пользователя не зная текущего пароля этого пользователя
                tmpForm.PasswordOld = "";
            tmpForm.Owner = this;
            // отобразить диалоговое окно для пользователя
            DialogResult dRes = tmpForm.ShowDialog(this);
            // если диалог закрыт с утвердительным ответом
            if (dRes == DialogResult.OK)
            {
                // принять новый пароль
                oper[Operator.password] = tmpForm.Password;
                oper.EndEdit();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            OperOrgRowCurrentEndEdit();
            // создание адаптера для записей операторов
            SQLiteDataAdapter adapter = Operator.CreateAdapter(_connectionStr);

            SQLiteCommand command = adapter.InsertCommand;
            SQLiteConnection connection = command.Connection;
            // обработать все записи операторов на вставку и пометить как обработанные
            foreach (DataRowView operRow in _operatorBS)
            {
                // если запись новая, то добавить ее в БД
                if (operRow.Row.RowState == DataRowState.Added)
                {
                    // открыть соединение
                    connection.Open();
                    // ввод параметров для команды
                    command.Parameters[Operator.pName].Value = operRow[Operator.name];
                    command.Parameters[Operator.pPassword].Value = operRow[Operator.password];
                    // выполнение запроса на исполнение и получение ID записи
                    object rowId = adapter.InsertCommand.ExecuteScalar();
                    // закрыть соединение
                    connection.Close();

                    // присвоить полоученный ID всем привелегиям с всталяемым оператором и самому оператору
                    long idVal = (long)rowId;
                    ChangeOperatorId(idVal, operRow, _operOrgBS);
                    operRow.Row.AcceptChanges();
                }
            }
            // отправить оставшиеся записи операторов на обработку в Адаптер
            adapter.Update(_operatorTable);

            // получить ссылку на текущую запись с привелегией
            DataRowView operOrgRow = _operOrgBS.Current as DataRowView;
            // если ссылка не пуста, то записать привелегию, считав ее сформы, и преобразовав в код
            if (operOrgRow != null)
            {
                // считывание и получение кода
                string codeStr = GetPrivilegeCode();
                // запись кода
                operOrgRow[OperatorOrg.code] = codeStr;
                operOrgRow.EndEdit();
            }

            // запомнить значение фильтра
            string filter = _operOrgBS.Filter;
            // запомнить позицию активной записи
            int pos = _operOrgBS.Position;
            // снять фильтр
            _operOrgBS.RemoveFilter();
            // создание соединения
            connection = new SQLiteConnection(_connectionStr);
            // создание команды для вставки записи с привелегией в БД
            command = OperatorOrg.CreateInsertCommand();
            command.Connection = connection;
            // вставить в БД все новые записи с привелегиями
            foreach (DataRowView itemRow in _operOrgBS)
            {
                // если строка не помечена как удаленная
                if (itemRow.Row.RowState != DataRowState.Deleted)
                {
                    // если строка с привелегией содержит "пустой" код привелегии, то пометить ее как удаленную
                    if ((itemRow[OperatorOrg.code] as string).Replace("0", "").Length <= 0 || !(bool)itemRow[Check])
                        itemRow.Row.Delete();
                    else
                    {
                        // если строка новая, то произвести ее вставку в БД
                        if (itemRow.Row.RowState == DataRowState.Added && (bool)itemRow[Check])
                        {
                            // открыть соединение
                            connection.Open();
                            // вставить значения параметров запроса
                            command.Parameters[OperatorOrg.pOperatorID].Value = itemRow[OperatorOrg.operatorID];
                            command.Parameters[OperatorOrg.pOrgID].Value = itemRow[OperatorOrg.orgID];
                            command.Parameters[OperatorOrg.pCode].Value = itemRow[OperatorOrg.code];
                            // выполнить запрос и получить значение ID записи
                            object rowID = command.ExecuteScalar();
                            // закрыть соединение
                            connection.Close();
                            // записать полученный ID в запись (синхронизация с БД)
                            long idVal = (long)rowID;
                            itemRow[OperatorOrg.id] = idVal;
                            itemRow.EndEdit();
                            itemRow.Row.AcceptChanges();
                        }
                    }
                }
            }
            // обновить данные (изменение, удаление) в БД для записей привелегий
            _operatororgAdapter.Update(_operOrgTable);
            // восстановить фильтр
            _operOrgBS.Filter = filter;
            // восстановить позицию
            _operOrgBS.Position = pos;

            MainForm.ShowInfoMessage("Данные успешно сохранены", "Сохранение");
            DialogResult = DialogResult.OK;
        }

        private void anketaaccessCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool b = anketaAccessCheckBox.Checked;
            anketaAccessLevelBox.Enabled = b;
            anketaPrintCheckBox.Enabled = b;
        }

        private void paystajaccessCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool b = payStajAccessCheckBox.Checked;
            payStajAccessLevelBox.Enabled = b;
            payStajPrintCheckBox.Enabled = b;
        }

        private void orgView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                DataRowView operRow = _operatorBS.Current as DataRowView;
                // если текущий оператор - Администратор, то ничего не делать и прекратить выполнение метода
                if (IsAdminCurrent(operRow))
                    return;
                // принять изменение ячейки, чтобы флаг поменялся и послались изменения в контролер.
                orgView.EndEdit();
                // установить флаг активности в соответствии с флагом огранизации
                bool b = false;
                if (_orgBS.Current != null)
                {
                    b = (bool)(_orgBS.Current as DataRowView)[Check];
                    (_orgBS.Current as DataRowView)[Check] = !b;
                }
                accessGroupBox.Enabled = b;
                DataRowView operOrgRow = _operOrgBS.Current as DataRowView;
                if (operOrgRow != null)
                {
                    operOrgRow[Check] = b;
                    operOrgRow.EndEdit();
                }
                orgView.Refresh();
            }
        }

        #endregion
    }
}