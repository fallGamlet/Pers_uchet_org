﻿using System;
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
         * только чтение = 1
         * полный доступ = 2
         */

        #region Поля
        // Адаптеры
        SQLiteDataAdapter _orgAdapter;
        SQLiteDataAdapter _operatorAdapter;
        SQLiteDataAdapter _operatororgAdapter;

        // тиблицы с данными
        DataTable _operatorTable;
        DataTable _orgTable;
        DataTable _operOrgTable;
        // чисто виртуальная таблица, без соответствия в БД (введена для удобства)
        DataTable _accessTable;

        // контролеры для источников данных (таблиц)
        BindingSource _operatorBS;
        BindingSource _orgBS;
        BindingSource _operOrgBS;
        BindingSource _anketaAccessBS;
        BindingSource _paystajAccessBS;

        // названия столбцов для удобства использования
        const string check = "check";
        const string aNum = "access_num";
        const string aName = "access_name";
        // количество цифр в коде приввелегии (расшифровка кода сверху в комментариях)
        const int codeLength = 6;
        string adminCode = OperatorOrg.GetPrivilegeForAdmin();//"212111";
        // строка подключения к БД
        string _connectionStr;
        #endregion

        #region Конструктор и инициализатор
        public OperatorsForm(string connection)
        {
            InitializeComponent();
            _connectionStr = connection;
        }

        private void OperatorsForm_Load(object sender, EventArgs e)
        {
            // создание объектов таблиц
            _operatorTable = Operator.CreateTable();
            _orgTable = Org.CreateTable();
            // добавление виртуального столбца, для возможности отмечать
            _orgTable.Columns.Add(check, typeof(bool));
            _orgTable.Columns[check].DefaultValue = false;
            _operOrgTable = OperatorOrg.CreateTable();
            // добавление виртуального столбца, для возможности отмечать
            _operOrgTable.Columns.Add(check, typeof(bool));
            _operOrgTable.Columns[check].DefaultValue = false;

            // создание чисто виртуальной таблицы для хранения уровня
            _accessTable = new DataTable();
            // добавление необходимых столбцов
            _accessTable.Columns.Add(aNum, typeof(int));
            _accessTable.Columns.Add(aName, typeof(string));
            // заполнение данными виртуальной таблицы привелегий
            DataRow row;
            row = _accessTable.NewRow(); row[aNum] = 0; row[aName] = "Без доступа"; row.EndEdit(); _accessTable.Rows.Add(row);
            row = _accessTable.NewRow(); row[aNum] = 1; row[aName] = "Только чтение"; row.EndEdit(); _accessTable.Rows.Add(row);
            row = _accessTable.NewRow(); row[aNum] = 2; row[aName] = "Полный доступ"; row.EndEdit(); _accessTable.Rows.Add(row);
            _accessTable.AcceptChanges();

            // создание объектов контроллеров
            _operatorBS = new BindingSource();
            _orgBS = new BindingSource();
            _operOrgBS = new BindingSource();
            // два контроллера для одного источника данных
            // по одному для каждого выпадающего списка
            _anketaAccessBS = new BindingSource();
            _paystajAccessBS = new BindingSource();

            // инициализация адаптеров для выполнения запросов нахаполнения таблиц
            _operatorAdapter = Operator.CreateAdapter(_connectionStr);
            _orgAdapter = Org.CreateAdapter(_connectionStr);
            _operatororgAdapter = OperatorOrg.CreateAdapter(_connectionStr);
            // заполение таблиц данными из БД
            _operatorAdapter.Fill(_operatorTable);
            _orgAdapter.Fill(_orgTable);
            _operatororgAdapter.Fill(_operOrgTable);

            // привязка таблиц к контроллерав (биндинг сорсам)
            _operatorBS.DataSource = _operatorTable;
            _orgBS.DataSource = _orgTable;
            _operOrgBS.DataSource = _operOrgTable;
            _anketaAccessBS.DataSource = _accessTable;
            _paystajAccessBS.DataSource = _accessTable;

            // привязка контроллеров к вьюшкам
            // привязка к выпадающему списку операторов 
            this.operatorBox.DataSource = _operatorBS;
            this.operatorBox.DisplayMember = Operator.name;
            // привязка к таблице органазаций
            this.orgView.AutoGenerateColumns = false;
            this.orgView.DataSource = _orgBS;
            // привязка к выпадающим спскам уровня доступа
            this.anketaaccesslevelBox.DataSource = _anketaAccessBS;
            this.anketaaccesslevelBox.DisplayMember = aName;
            this.paystajaccesslevelBox.DataSource = _paystajAccessBS;
            this.paystajaccesslevelBox.DisplayMember = aName;
            // инициализация обработчиков событий смены оператора и выбора организации
            _operatorBS.CurrentChanged += new EventHandler(_operatorBS_CurrentChanged);
            _orgBS.CurrentChanged += new EventHandler(_orgBS_CurrentChanged);
            // 
            foreach (DataRowView itemRow in _operOrgBS)
            {
                if ((itemRow[OperatorOrg.code] as string).Replace("0", "").Length > 0)
                {
                    itemRow[check] = true;
                    itemRow.EndEdit();
                }
            }
            // выбирается первый оператор
            _operatorBS.MoveFirst();

            _operatorBS_CurrentChanged(null, null);
            anketaaccessCheckBox_CheckedChanged(null, null);
            paystajaccessCheckBox_CheckedChanged(null, null);
        }
        #endregion

        #region Свойства
        #endregion

        #region Методы - свои
        /// <summary>
        /// Получить наибольшее абсолютное значение ключевого поля 
        /// </summary>
        /// <param name="table">Таблица, в которой будет производиться поиск</param>
        /// <param name="idname">Название ключевого столбца</param>
        /// <returns></returns>
        long GetMaxID(DataTable table, string idname)
        {
            long res = -1;
            long val;
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    val = Math.Abs((long)row[idname]);
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
        string GetPrivilegeCode()
        {
            char[] res = new char[codeLength];
            bool b0, b1, b2, b3, b4, b5;
            int a0, a2;

            b0 = this.anketaaccessCheckBox.Checked;
            b1 = this.anketaprintCheckBox.Checked;
            a0 = _anketaAccessBS.Position;
            b2 = this.paystajaccessCheckBox.Checked;
            b3 = this.paystajprintCheckBox.Checked;
            a2 = _paystajAccessBS.Position;
            b4 = this.exchangedataCheckBox.Checked;
            b5 = this.importanketaCheckBox.Checked;

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
        void setPrivilegeCode(string accessCode)
        {
            accessCode = accessCode.Trim();
            if (accessCode.Length < codeLength)
            {
                this.anketaaccessCheckBox.Checked = false;
                this.anketaprintCheckBox.Checked = false;
                _anketaAccessBS.Position = 0;
                this.paystajaccessCheckBox.Checked = false;
                this.paystajprintCheckBox.Checked = false;
                _paystajAccessBS.Position = 0;
                this.exchangedataCheckBox.Checked = false;
                this.importanketaCheckBox.Checked = false;
                return;
            }

            bool b0, b1, b2, b3, b4, b5;
            int a0, a2;
            a0 = int.Parse(accessCode[0].ToString());
            a2 = int.Parse(accessCode[2].ToString());
            b0 = Convert.ToBoolean(a0);
            b1 = Convert.ToBoolean(int.Parse(accessCode[1].ToString()));
            b2 = Convert.ToBoolean(a2);
            b3 = Convert.ToBoolean(int.Parse(accessCode[3].ToString()));
            b4 = Convert.ToBoolean(int.Parse(accessCode[4].ToString()));
            b5 = Convert.ToBoolean(int.Parse(accessCode[5].ToString()));

            this.anketaaccessCheckBox.Checked = b0;
            this.anketaprintCheckBox.Checked = b1;
            _anketaAccessBS.Position = a0;
            this.paystajaccessCheckBox.Checked = b2;
            this.paystajprintCheckBox.Checked = b3;
            _paystajAccessBS.Position = a2;
            this.exchangedataCheckBox.Checked = b4;
            this.importanketaCheckBox.Checked = b5;
        }

        /// <summary>
        /// Добавляет отношения Оператор-Организация,которых не хватает до полноты
        /// </summary>
        /// <param name="operRow">Строка с данными об опрераторе</param>
        /// <param name="orgBS"> Биндинг сорс с привязкой к таблице организацый</param>
        /// <param name="operOrgRelationBS">Биндинг сорс с привязкой к к таблице с данными отношения Операторов и Организаций</param>
        void addRelationsIfNeed(DataRowView operRow, BindingSource orgBS, BindingSource operOrgRelationBS)
        {
            string oldFilter = operOrgRelationBS.Filter;
            int oldPos = operOrgRelationBS.Position;
            operOrgRelationBS.Filter = string.Format("{0} = {1}", OperatorOrg.operatorID, operRow[Operator.id]);
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
                    newRow[OperatorOrg.code] = new string('0', codeLength);
                    newRow.EndEdit();
                }
            operOrgRelationBS.Filter = oldFilter;
            operOrgRelationBS.Position = oldPos;
        }

        /// <summary>
        /// Установить новый идентификатор (ID) для оператора
        /// в записи об операторе и связующей таблице между операторами и организациями
        /// </summary>
        /// <param name="newOperatorID">Новый идентификатор оператора</param>
        /// <param name="operRow">Строка записи оператора</param>
        /// <param name="operOrgRelationBS">Биндинг связующей таблицы между оператами и организациями</param>
        void ChangeOperatorId(long newOperatorID, DataRowView operRow, BindingSource operOrgRelationBS)
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
        /// Проставить галочки в поле Check для организаций
        /// </summary>
        void SetOrgsChecked()
        {
            int ipos;
            // проставить галочки рядом с названиями организациями
            foreach (DataRowView row in _orgBS)
            {
                ipos = _operOrgBS.Find(OperatorOrg.orgID, row[Org.id]);
                // убрать галочку, если привилегии на организацию нет
                if (ipos <= -1)
                    row[check] = false;
                else
                {
                    DataRowView operOrgRow = _operOrgBS[ipos] as DataRowView;
                    row[check] = operOrgRow[check];
                    row.EndEdit();
                }
            }
        }

        void SetOrgsCheckedAll()
        {
            // проставить галочки рядом с названиями организациями
            foreach (DataRowView row in _orgBS)
            {
                row[check] = true;
            }
            _orgBS.EndEdit();
        }

        void SetOrgsUncheckedAll()
        {
            // проставить галочки рядом с названиями организациями
            foreach (DataRowView row in _orgBS)
            {
                row[check] = false;
            }
            _orgBS.EndEdit();
        }

        /// <summary>
        /// Записать код привелегии в текущую запись OperOrg
        /// </summary>
        void OperOrgRowCurrentEndEdit()
        {
            DataRowView operOrgRow = _operOrgBS.Current as DataRowView;
            // если запись не нулевая и соответствует другому оператору, то нужно сохранить изменения
            if (operOrgRow != null)
            {
                // считывание и получение кода
                string codeStr = this.GetPrivilegeCode();
                // запись кода
                operOrgRow[OperatorOrg.code] = codeStr;
                operOrgRow.EndEdit();
            }
        }

        void SetOperOrgPosition(long orgID)
        {
            DataRowView operorgRow;
            string code;
            // получить позицию записи с приаелегией для текущей записи организации
            int ipos = _operOrgBS.Find(OperatorOrg.orgID, orgID);
            // установить полученную позицию
            _operOrgBS.Position = ipos;
            // получение ссылки на новую активную запись с кодом привелегии
            operorgRow = _operOrgBS.Current as DataRowView;
            // если ссылка не пустая, получить сам код привелегии
            if (operorgRow != null)
                code = operorgRow[OperatorOrg.code] as string;
            else // иначе создать код привелегии указывающий об отсутствии доступа
                code = new string('0', codeLength);
            // отобразить код привелегии на для форме
            this.setPrivilegeCode(code);
            this.accessGroupBox.Enabled = (bool)operorgRow[check];
        }

        bool IsAdminCurrent()
        {
            DataRowView operRow = _operatorBS.Current as DataRowView;
            if (operRow != null && (int)operRow[Operator.candelete] == 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Методы - обработчики событий
        void _operatorBS_CurrentChanged(object sender, EventArgs e)
        {
            // получение ссылки на текущую запись оператора
            DataRowView operRow = _operatorBS.Current as DataRowView;
            // если запись не выбрана, то выйти из метода
            if (operRow == null)
            {
                this.orgView.Enabled = false;
                return;
            }
            // получить текущую запись о пересечении выбранного оператора и выбранной организации
            DataRowView operOrgRow = _operOrgBS.Current as DataRowView;
            // если запись не нулевая и соответствует другому оператору, то нужно сохранить изменения
            if (operOrgRow != null && (long)operOrgRow[OperatorOrg.operatorID] != (long)operRow[Operator.id])
            {
                this.OperOrgRowCurrentEndEdit();
            }

            if (IsAdminCurrent())
            {
                SetOrgsCheckedAll();
                this.setPrivilegeCode(adminCode);
                this.orgView.ReadOnly = true;
                this.accessGroupBox.Enabled = false;
                this.removeButton.Enabled = false;
                this.editButton.Enabled = false;
                return;
            }

            this.orgView.ReadOnly = false;
            this.regnumColumn.ReadOnly = true;
            this.orgnameColumn.ReadOnly = true;
            this.removeButton.Enabled = true;
            this.editButton.Enabled = true;
            // добавить записи пересечений операторов и организаций для указания привелегий.
            this.addRelationsIfNeed(operRow, _orgBS, _operOrgBS);
            // установить фильтр по оператору
            _operOrgBS.Filter = string.Format("{0} = {1}", OperatorOrg.operatorID, operRow[Operator.id]);
            // получить выбранную запись организации
            DataRowView orgRow = _orgBS.Current as DataRowView;
            // если текущая выбранная запись организации не пустая, то нужно отобразить значение привелегии на форме
            if (orgRow != null)
                this.SetOperOrgPosition((long)orgRow[Org.id]);

            // проставить галочки рядом с названиями организациями
            this.SetOrgsChecked();
        }

        void _orgBS_CurrentChanged(object sender, EventArgs e)
        {
            if (IsAdminCurrent())
            {
                this.setPrivilegeCode(adminCode);
                this.accessGroupBox.Enabled = false;
                return;
            }
            // получить ссылку на запись текущей выбранной организации
            DataRowView orgRow = _orgBS.Current as DataRowView;
            // если ссылка пуста, то завершить метод
            if (orgRow == null)
            {
                this.accessGroupBox.Enabled = false;
                return;
            }
            // установить флаг активности в соответствии с флагом огранизации
            this.accessGroupBox.Enabled = (_orgBS.Current as DataRowView) == null ? false : (bool)(_orgBS.Current as DataRowView)[check];

            this.OperOrgRowCurrentEndEdit();

            this.SetOperOrgPosition((long)orgRow[Org.id]);
        }

        private void orgView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // проверить, является ли кликнутая ячейка, ячейкой с флагом
            if (e.ColumnIndex == org_checkColumn.Index)
            {
                // если текущий оператор - Администратор, то ничего не делать и прекратить выполнение метода
                if (IsAdminCurrent())
                    return;
                // принять изменение ячейки, чтобы флаг поменялся и послались изменения в контролер.
                orgView.EndEdit();
                // установить флаг активности в соответствии с флагом огранизации
                bool b = (_orgBS.Current as DataRowView) == null ? false : (bool)(_orgBS.Current as DataRowView)[check];
                this.accessGroupBox.Enabled = b;
                DataRowView operorgRow = _operOrgBS.Current as DataRowView;
                if (operorgRow != null)
                {
                    operorgRow[check] = b;
                    operorgRow.EndEdit();
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
                    MainForm.ShowInfoMessage("Пользователь с таким именем уже существует!", "Ошибка добавления оператора");
                    return;
                }

                // парольт пока пустой
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
                }
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
                // сделать активной добаленную запись
                _operatorBS.Position = _operatorBS.Find(Operator.id, newOperRow[Operator.id]);
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
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // создание адаптера для записей операторов
            SQLiteDataAdapter adapter = Operator.CreateAdapter(_connectionStr);
            SQLiteCommand command;
            SQLiteConnection connection;

            command = adapter.InsertCommand;
            connection = command.Connection;
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
                    object rowID = adapter.InsertCommand.ExecuteScalar();
                    // закрыть соединение
                    connection.Close();

                    // присвоить полоученный ID всем привелегиям с всталяемым оператором и самому оператору
                    long idVal = (long)rowID;
                    ChangeOperatorId(idVal, operRow, _operOrgBS);
                    operRow.Row.AcceptChanges();
                }
            }
            // отправить оставшиеся записи операторов на обработку в Адаптер
            int rescount = adapter.Update(_operatorTable);

            // получить ссылку на текущую запись с привелегией
            DataRowView operorgRow = _operOrgBS.Current as DataRowView;
            // если ссылка не пуста, то записать привелегию, считав ее сформы, и преобразовав в код
            if (operorgRow != null)
            {
                // считывание и получение кода
                string codeStr = this.GetPrivilegeCode();
                // запись кода
                operorgRow[OperatorOrg.code] = codeStr;
                operorgRow.EndEdit();
            }

            // запомнить значение фильтра
            string filter = _operOrgBS.Filter;
            // запомнить позицию активной записи
            int pos = _operOrgBS.Position;
            // снять фильтр
            _operOrgBS.RemoveFilter();
            // создание смоединения
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
                    // если стврока с привелегией содержит "пустой" код привелегии, то пометить ее как удаленную
                    if ((itemRow[OperatorOrg.code] as string).Replace("0", "").Length <= 0 || !(bool)itemRow[check])
                        itemRow.Row.Delete();
                    else
                    {

                        // если строка новая, то произвести ее вставку в БД
                        if (itemRow.Row.RowState == DataRowState.Added && (bool)itemRow[check])
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
            // обновить данные (изменение, удаление) в БД для записей приведегий
            _operatororgAdapter.Update(_operOrgTable);
            // восстановить фильтр
            _operOrgBS.Filter = filter;
            // восстановить позицию
            _operOrgBS.Position = pos;

            MainForm.ShowInfoMessage("Данные успешно сохранены", "Созранение");
        }

        private void anketaaccessCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool b = this.anketaaccessCheckBox.Checked;
            this.anketaaccesslevelBox.Enabled = b;
            this.anketaprintCheckBox.Enabled = b;
        }

        private void paystajaccessCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool b = this.paystajaccessCheckBox.Checked;
            this.paystajaccesslevelBox.Enabled = b;
            this.paystajprintCheckBox.Enabled = b;
        }
        #endregion
    }
}
