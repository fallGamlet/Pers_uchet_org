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
    public partial class AnketadataForm : Form
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
        // таблица
        DataTable _personTable;
        // биндинг сорс для таблицы
        BindingSource _personBS;
        // адаптер для чтения данных из БД
        SQLiteDataAdapter _personAdapter;
        // названия добавочного виртуального столбца
        const string CHECK = "check";
        #endregion

        #region Конструктор и инициализатор
        public AnketadataForm(Operator oper, Org org, string connection)
        {
            InitializeComponent();

            _operator = oper;
            _org = org;
            _connection = connection;
        }

        private void AnketadataForm_Load(object sender, EventArgs e)
        {
            // иництализация таблицы персон (записи с анкетными данными)
            _personTable = PersonView.CreatetTable();
            // добавление виртуального столбца для возможности отмечать записи
            _personTable.Columns.Add(CHECK, typeof(bool));
            _personTable.Columns[CHECK].DefaultValue = false;

            // инициализация биндинг сорса к таблице персон
            _personBS = new BindingSource();
            _personBS.CurrentChanged += new EventHandler(_personBS_CurrentChanged);
            _personBS.ListChanged += new ListChangedEventHandler(_personBS_ListChanged);
            _personBS.DataSource = _personTable;
            // присвоение источника вьюшке
            this.personView.AutoGenerateColumns = false;
            this.personView.DataSource = _personBS;
            // инициализация Адаптера для считывания персон из БД
            string commandStr = PersonView.GetSelectText(_org.idVal);
            _personAdapter = new SQLiteDataAdapter(commandStr, _connection);
            // запосление таблицы данными с БД
            _personAdapter.Fill(_personTable);

            // получитьм код привилегии (уровня доступа) Оператора к Организации
            if (_operator.IsAdmin())
                _privilege = OperatorOrg.GetPrivilegeForAdmin();
            else
                _privilege = OperatorOrg.GetPrivilege(_operator.idVal, _org.idVal, _connection);
            // отобразить привилегию на форме для пользователя
            this.SetPrivilege(_privilege);
            // отобразить работающий персон
            this.rabotaRButton.Checked = true;
            //this.stateRButton_CheckedChanged(null, null);
        }
        #endregion

        #region Методы - свои
        private List<DataRowView> GetSelectedRows()
        {
            List<DataRowView> list = new List<DataRowView>();
            foreach (DataRowView personRow in _personBS)
                if ((bool)personRow[CHECK])
                    list.Add(personRow);
            if (list.Count <= 0 && _personBS.Current != null)
                list.Add(_personBS.Current as DataRowView);
            return list;
        }

        private void SetPrivilege(string privilegeCode)
        {
            int anketaAccess = OperatorOrg.GetAnketaDataAccesseCode(privilegeCode);
            bool canWrite = (anketaAccess == 2);
            this.addButton.Enabled = canWrite;
            this.editButton.Enabled = canWrite;
            this.removeButton.Enabled = canWrite;
            this.uvolitButton.Enabled = canWrite;
            this.vostanovitButton.Enabled = canWrite;
            this.zakrepButton.Enabled = canWrite;

            int anketaPrint = OperatorOrg.GetAnketaPrintAccesseCode(privilegeCode);
            this.printButton.Enabled = (anketaPrint > 0);
        }
        #endregion

        #region Методы - обработчики событий
        void tmpform_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender != null && sender is EditPersonForm)
            {
                EditPersonForm tmpForm = sender as EditPersonForm;
                if(tmpForm.DialogResult == DialogResult.OK)
                {
                    long personID = tmpForm.PersonID;
                    _personTable.Rows.Clear();
                    _personAdapter.Fill(_personTable);
                    _personBS.Position = _personBS.Find(PersonInfo.id, personID);
                }
            }
        }

        void _personBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = _personBS.Current as DataRowView;
            if (row == null)
            {
                this.vostanovitButton.Enabled = this.uvolitButton.Enabled = false;
                return;
            }
            this.fioBox.Text = row[PersonView.fio] as string;
            this.datarojdBox.Text = row[PersonView.birthday] is DBNull ? "" : ((DateTime)row[PersonView.birthday]).ToShortDateString();
            this.grajdanstvoBox.Text = row[PersonView.citizen1] as string;
            this.grajdanstvoBox.Text += " "+ row[PersonView.citizen2] as string;
            object sexObj = row[PersonView.sex];
            this.polBox.Text = sexObj is DBNull? "не определено" : (int)sexObj == 1 ? "мужской" : "женский";
            this.mestorojdBox.Text = row[PersonView.bornAdress] as string;
            this.adrespropiskiBox.Text = row[PersonView.regAdress] as string;
            this.adressprojivBox.Text = row[PersonView.factAdress] as string;
            this.documentBox.Text = row[PersonView.docType] as string;
            this.docseriaBox.Text = row[PersonView.docSeries] as string;
            this.docnumBox.Text = row[PersonView.docNumber] as string;
            this.docdataBox.Text = row[PersonView.docDate] is DBNull ? "" : ((DateTime)row[PersonView.docDate]).ToShortDateString();
            this.docvidanBox.Text = row[PersonView.docOrg] as string;

            this.newdateBox.Text = row[PersonView.newDate] is DBNull ? "":((DateTime)row[PersonView.newDate]).ToShortDateString();
            this.editdateBox.Text = row[PersonView.editDate] is DBNull ? "" : ((DateTime)row[PersonView.editDate]).ToShortDateString();
            this.operatorBox.Text = row[PersonView.operName] as string;

            object stateObj = row[PersonView.state];
            this.uvolitButton.Enabled = (stateObj is DBNull) || (int)stateObj == 1 ? true : false;
            this.vostanovitButton.Enabled = !this.uvolitButton.Enabled;
        }

        void _personBS_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.countBox.Text = _personBS.Count.ToString();
        }

        private void checkallButton_Click(object sender, EventArgs e)
        {
            bool allchecked = true;
            foreach (DataRowView row in _personBS)
                if (!(bool)row[CHECK])
                {
                    allchecked = false;
                    break;
                }
            foreach (DataRowView row in _personBS)
                row[CHECK] = !allchecked;
            this.personView.Refresh();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            EditPersonForm tmpform = new EditPersonForm(_connection, _operator.nameVal, _org.idVal);
            tmpform.FormClosed += new FormClosedEventHandler(tmpform_FormClosed);
            tmpform.Owner = this;
            tmpform.Show();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            DataRowView person = _personBS.Current as DataRowView;
            if(person == null)
            {
                MainForm.ShowInfoMessage("Сначала необходимо выбрать запись!", "Не выбрана запись");
                return;
            }
            long person_id = (long)person[PersonInfo.id];
            EditPersonForm tmpform = new EditPersonForm(_connection, _operator.nameVal, _org.idVal);
            tmpform.FormClosed += new FormClosedEventHandler(tmpform_FormClosed);
            tmpform.Owner = this;
            tmpform.Show();

            tmpform.PersonID = person_id;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            List<long> personIdList = new List<long>();
            List<DataRowView> personList = new List<DataRowView>();
            foreach (DataRowView personRow in _personBS)
                if ((bool)personRow[CHECK])
                {
                    personList.Add(personRow);
                    personIdList.Add((long)personRow[PersonView.id]);
                }
            if (personIdList.Count <= 0)
            {
               MainForm.ShowInfoMessage("Необходимо сначала отметить записи, которые необходимо удалить", 
                                        "Нет помеченных записей для удаления");
                return;
            }
            try
            {
                PersonInfo.Delete(personIdList, _connection);
                foreach (DataRowView rowItem in personList)
                {
                    rowItem[CHECK] = false;
                    rowItem.Delete();
                }
            }
            catch(Exception err)
            {
                MainForm.ShowWarningMessage(err.Message, "Внимание");
                _personBS.CancelEdit();
            }
        }

        private void uvolitButton_Click(object sender, EventArgs e)
        {
            AnketaUvolitForm tmpform = new AnketaUvolitForm();
            tmpform.Owner = this;
            DialogResult dRes = tmpform.ShowDialog(this);
            if (dRes == DialogResult.OK)
            {
                List<DataRowView> persons = this.GetSelectedRows();
                List<long> personIDList = new List<long>();
                foreach(DataRowView rowItem in persons)
                    personIDList.Add((long)rowItem[PersonView.id]);
                PersonOrg.SetStateToUvolit(personIDList, _org.idVal, tmpform.DismissDate, _connection);
                foreach (DataRowView rowItem in persons)
                {
                    rowItem[PersonView.state] = (int)PersonView.PersonState.Uvolen;
                    rowItem[PersonView.dismissDate] = tmpform.DismissDate;
                    rowItem[CHECK] = false;
                    rowItem.EndEdit();
                }
                _personBS.MoveFirst();
            }
        }

        private void vostanovitButton_Click(object sender, EventArgs e)
        {
            List<DataRowView> persons = this.GetSelectedRows();
            List<long> personIDList = new List<long>();
            foreach (DataRowView rowItem in persons)
                personIDList.Add((long)rowItem[PersonView.id]);
            PersonOrg.SetStateToRabotaet(personIDList, _org.idVal, _connection);
            foreach (DataRowView rowItem in persons)
            {
                rowItem[PersonView.state] = (int)PersonView.PersonState.Rabotaet;
                rowItem[PersonView.dismissDate] = DBNull.Value;
                rowItem[CHECK] = false;
                rowItem.EndEdit();
            }
            _personBS.MoveFirst();
        }

        private void zakrepButton_Click(object sender, EventArgs e)
        {
            DataRowView curPersonRow = _personBS.Current as DataRowView;
            if (curPersonRow == null)
            {
                MainForm.ShowInfoMessage("Необходимо указать(выделить) запись.","Внимание");
                return;
            }
            AnketaPersonOrgForm tmpform = new AnketaPersonOrgForm(curPersonRow.Row, _operator, _org.idVal,_connection);
            tmpform.Owner = this;
            tmpform.ShowDialog(this);
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            List<DataRowView> selectedRowList = this.GetSelectedRows();
            if (selectedRowList.Count <= 0)
            {
                DataRowView personRow = _personBS.Current as DataRowView;
                if (personRow != null)
                {
                    selectedRowList.Add(personRow);
                }
            }
            if (selectedRowList.Count <= 0)
            {
                MainForm.ShowInfoMessage("Необходимо отметить или выделить хотя бы одну запись!", "Внимание");
                return;
            }
            List<DataRow> rowList = new List<DataRow>(selectedRowList.Count);
            foreach (DataRowView rowItem in selectedRowList)
                rowList.Add(rowItem.Row);

            PersonView.Print(rowList);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void searchSocnumBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                int pos;
                string sval = this.searchSocnumBox.Text;
                for (pos = 0; pos < _personBS.Count; pos++)
                {
                    DataRowView row = _personBS[pos] as DataRowView;
                    if (row != null)
                    {
                        string sucnum = row[PersonView.socNumber] as string;
                        if (sucnum != null && sucnum.Contains(sval))
                        {
                            _personBS.Position = pos;
                            break;
                        }
                    }
                }
            }
        }

        private void searchFioBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                int pos;
                string sval = this.searchFioBox.Text;
                for (pos = 0; pos < _personBS.Count; pos++)
                {
                    DataRowView row = _personBS[pos] as DataRowView;
                    if (row != null)
                    {
                        string fio = row[PersonView.fio] as string;
                        if (fio != null && fio.Contains(sval))
                        {
                            _personBS.Position = pos;
                            break;
                        }
                    }
                }
            }
        }

        private void stateRButton_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRowView personRow in _personBS)
                if ((bool)personRow[CHECK])
                {
                    personRow[CHECK] = false;
                    personRow.EndEdit();
                }
            if (sender == this.uvolenRButton)
            {
                _personBS.Filter = string.Format("{0} = {1}",PersonView.state, (int)PersonView.PersonState.Uvolen);
                this.dismissdateColumn.Visible = true;
            }
            else if (sender == this.rabotaRButton)
            {
                _personBS.Filter = string.Format("{0} is NULL OR {0} = {1}",PersonView.state, (int)PersonView.PersonState.Rabotaet);
                this.dismissdateColumn.Visible = false;
            }
        }
        #endregion
    }
}
