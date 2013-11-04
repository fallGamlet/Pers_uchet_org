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
    public partial class AnketaPersonOrgForm : Form
    {
        #region Поля
        DataRow _personRow;
        Operator _operator;
        long _orgID;
        DataTable _orgTable;
        BindingSource _orgBS;

        const string CHECK = "check";
        string _connection;
        #endregion

        #region Свойства
        #endregion

        #region Конструктор и инициализатор
        public AnketaPersonOrgForm(DataRow personRow, Operator oper, long org_id, string connectionStr)
        {
            InitializeComponent();

            _personRow = personRow;
            _operator = oper;
            _connection = connectionStr;
            _orgID = org_id;
        }

        private void AnketaPersonOrgForm_Load(object sender, EventArgs e)
        {
            // инициализация таблицы организаций
            _orgTable = Org.CreateTable();
            // добавление столбца для отметок (пометок)
            _orgTable.Columns.Add(CHECK, typeof(bool));
            _orgTable.Columns[CHECK].DefaultValue = false;
            // определение бинда
            _orgBS = new BindingSource();
            // привязка к источнику
            _orgBS.DataSource = _orgTable;
            // запрет на автогенерацию столбцов вьюшки
            this.orgView.AutoGenerateColumns = false;
            // привязка к источнику
            this.orgView.DataSource = _orgBS;
            this.orgView.Sorted += new EventHandler(orgView_Sorted);
            // если оператор определен и соединение определено
            if (_operator != null && _connection != null)
            {
                // определение адаптера для считывания записей с данными об организациях
                string selectcommand;
                // если оператор - Администратор, то выбрать команду для выбора всех имеющихся организациях
                if (_operator.IsAdmin())
                    selectcommand = Org.GetSelectCommandText();
                else // иначе выбрать толькоте организации, к которым оператор имеет доступ для редактирования
                    selectcommand = Org.GetSelectTextByOperatorAccess(_operator.idVal);
                // инизаализация адаптера
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectcommand, _connection);
                // заполнение таблицы данными из БД
                adapter.Fill(_orgTable);
                // получение списка ID организаций, в которых числиться выбранная Персона (физическое лицо)
                long[] orgIDArray = PersonOrg.GetOrgID((long)_personRow[PersonView.id], _connection);
                int i;
                DataRowView tmpRow;
                // отметить из выбранных Организаций те, к которым привязана Персона
                foreach(long id in orgIDArray)
                {
                    i = _orgBS.Find(Org.id, id);
                    if(i>=0)
                    {
                        tmpRow = _orgBS[i] as DataRowView;
                        tmpRow[CHECK] = true;
                    }
                }
                // принять изменения (отметки)
                _orgBS.EndEdit();

                // выделить струку с текущей Организацией (усьановить задний фон)
                this.MarkCurOrgRow();
                // отобразить на форме страховой номер и фио выбранной Персоны
                personDataLabel.Text = string.Format("{0}  {1}", _personRow[PersonView.socNumber], _personRow[PersonView.fio]);
            }
        }
        #endregion

        #region Методы - свои
        private void MarkCurOrgRow()
        {
            int i = _orgBS.Find(Org.id, _orgID);
            if (i != -1)
            {
                orgView.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }
        #endregion

        #region Методы - обработчики событий
        void orgView_Sorted(object sender, EventArgs e)
        {
            this.MarkCurOrgRow();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            List<long> checkedOrgList, uncheckedOrgList;
            checkedOrgList = new List<long>();
            uncheckedOrgList = new List<long>();
            // разделить организации на отмеченные и неотмеченные
            foreach (DataRowView rowItem in _orgBS)
            {
                if ((bool)rowItem[CHECK])
                    checkedOrgList.Add((long)rowItem[Org.id]);
                else
                    uncheckedOrgList.Add((long)rowItem[Org.id]);
            }
            // сменить контейнеры хранения ID организаций
            long[] checkedOrgs = checkedOrgList.ToArray();
            long[] uncheckedOrgs = uncheckedOrgList.ToArray();
            // получить ID Организаций из БД, за которыми сейчас закреплена Перcона
            long[] orgs = PersonOrg.GetOrgID((long)_personRow[PersonView.id], _connection);
            // объединить множества ID Организаций выбранных из БД и отмеченных пользователем
            orgs = (long[])orgs.Union(checkedOrgs).Distinct().ToArray();
            // получить пересечение множеств ID Организаций, к которым в общем закреплена Персона
            // и ID Организаций, с которых Персона должна быть откреплена
            long[] intersect = (long[])orgs.Intersect(uncheckedOrgs).Distinct().ToArray();
            // если множество пересечения больше множества, к которым в общем прикреплена Персона,
            // то вывести сообщение и прекратить выполнение сохранения в БД
            if (intersect.Length >= orgs.Length)
            {
                MainForm.ShowWarningMessage("Анкетные данные должны быть привязаны хотя бы к одной организации!","Внимание");
                return;
            }
            // Вставить записи с отмеченными Организациями и выбранным Пользователем
            PersonOrg.InsertPersonOrg((long)_personRow[PersonView.id], checkedOrgs, _connection);
            // Удалить записи с неотмеченными Организациями и выбранным Пользователем
            PersonOrg.DeletePersonOrg((long)_personRow[PersonView.id], uncheckedOrgs, _connection);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
