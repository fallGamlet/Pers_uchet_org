using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Xml;

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
        // Браузер для формирования оточета по незарегистрированным персонам
        WebBrowser _wbUnregisteredPewrsons;
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
            this.Text += " - " + _org.regnumVal;
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

            // получить код привилегии (уровня доступа) Оператора к Организации
            if (_operator.IsAdmin())
                _privilege = OperatorOrg.GetPrivilegeForAdmin();
            else
                _privilege = OperatorOrg.GetPrivilege(_operator.idVal, _org.idVal, _connection);
            // отобразить привилегию на форме для пользователя
            this.SetPrivilege(_privilege);

            this.workButton.Enabled = false;
            // отобразить работающих персон
            stateButton_Click(this.workButton, null);
        }
        #endregion

        #region Методы - свои
        private List<DataRowView> GetSelectedRows()
        {
            if (_personBS.Current != null)
                (_personBS.Current as DataRowView)[CHECK] = true;

            personView.EndEdit();
            List<DataRowView> list = _personBS.Cast<DataRowView>().Where(personRow => (bool)personRow[CHECK]).ToList();
            personView.Refresh();

            return list;
        }

        private void SetPrivilege(string privilegeCode)
        {
            int anketaAccess = OperatorOrg.GetAnketaDataAccesseCode(privilegeCode);
            bool canWrite = (anketaAccess == 2);
            this.addStripButton.Enabled = canWrite;
            this.editStripButton.Enabled = canWrite;
            this.delStripButton.Enabled = canWrite;
            this.dismissStripButton.Enabled = canWrite;
            this.restoreStripButton.Enabled = canWrite;
            this.attachToOrgButton.Enabled = canWrite;

            int anketaPrint = OperatorOrg.GetAnketaPrintAccesseCode(privilegeCode);
            this.printStripDropDownButton.Enabled = (anketaPrint > 0);
        }
        #endregion

        #region Методы - обработчики событий

        void _personBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = _personBS.Current as DataRowView;
            if (row == null)
            {
                this.restoreStripButton.Enabled = this.dismissStripButton.Enabled = false;
                return;
            }
            this.fioBox.Text = row[PersonView.fio] as string;
            this.datarojdBox.Text = row[PersonView.birthday] is DBNull ? "" : ((DateTime)row[PersonView.birthday]).ToShortDateString();
            this.grajdanstvoBox.Text = row[PersonView.citizen1] as string;
            this.grajdanstvoBox.Text += " " + row[PersonView.citizen2] as string;
            object sexObj = row[PersonView.sex];
            this.polBox.Text = sexObj is DBNull ? "не определено" : (int)sexObj == 1 ? "мужской" : "женский";
            this.mestorojdBox.Text = row[PersonView.bornAdress] as string;
            this.adrespropiskiBox.Text = row[PersonView.regAdress] as string;
            this.adressprojivBox.Text = row[PersonView.factAdress] as string;
            this.documentBox.Text = row[PersonView.docType] as string;
            this.docseriaBox.Text = row[PersonView.docSeries] as string;
            this.docnumBox.Text = row[PersonView.docNumber] as string;
            this.docdataBox.Text = row[PersonView.docDate] is DBNull ? "" : ((DateTime)row[PersonView.docDate]).ToShortDateString();
            this.docvidanBox.Text = row[PersonView.docOrg] as string;

            this.newdateBox.Text = row[PersonView.newDate] is DBNull ? "" : ((DateTime)row[PersonView.newDate]).ToShortDateString();
            this.editdateBox.Text = row[PersonView.editDate] is DBNull ? "" : ((DateTime)row[PersonView.editDate]).ToShortDateString();
            this.operatorBox.Text = row[PersonView.operName] as string;

            object stateObj = row[PersonView.state];
            this.dismissStripButton.Enabled = (stateObj is DBNull) || (int)stateObj == 1;
            this.restoreStripButton.Enabled = !this.dismissStripButton.Enabled;
        }

        void _personBS_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.countLabel.Text = _personBS.Count.ToString();
        }

        private void searchSocnumBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                searchFioBox.Text = "";
                _personBS.Filter = string.Format("{0} like '%{1}%'", PersonView.socNumber, this.searchSocnumBox.Text);
                //int pos;
                //string sval = this.searchSocnumBox.Text;
                //for (pos = 0; pos < _personBS.Count; pos++)
                //{
                //    DataRowView row = _personBS[pos] as DataRowView;
                //    if (row != null)
                //    {
                //        string sucnum = row[PersonView.socNumber] as string;
                //        if (sucnum != null && sucnum.Contains(sval))
                //        {
                //            _personBS.Position = pos;
                //            break;
                //        }
                //    }
                //}
            }
        }

        private void searchFioBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                searchSocnumBox.Text = "";
                _personBS.Filter = string.Format("{0} like '%{1}%'", PersonView.fio, this.searchFioBox.Text);
                //int pos;
                //string sval = this.searchFioBox.Text;
                //for (pos = 0; pos < _personBS.Count; pos++)
                //{
                //    DataRowView row = _personBS[pos] as DataRowView;
                //    if (row != null)
                //    {
                //        string fio = row[PersonView.fio] as string;
                //        if (fio != null && fio.Contains(sval))
                //        {
                //            _personBS.Position = pos;
                //            break;
                //        }
                //    }
                //}
            }
        }

        private void stateButton_Click(object sender, EventArgs e)
        {
            foreach (DataRowView personRow in _personBS.Cast<DataRowView>().Where(personRow => (bool)personRow[CHECK]))
            {
                personRow[CHECK] = false;
                personRow.EndEdit();
            }

            if (sender == this.dismissedButton)
            {
                _personBS.Filter = string.Format("{0} = {1}", PersonView.state, (int)PersonView.PersonState.Uvolen);
                this.dismissdateColumn.Visible = true;
                workButton.Enabled = true;
                dismissedButton.Enabled = false;
                addStripButton.Enabled = false;
            }
            else if (sender == this.workButton)
            {
                _personBS.Filter = string.Format("{0} is NULL OR {0} = {1}", PersonView.state, (int)PersonView.PersonState.Rabotaet);
                this.dismissdateColumn.Visible = false;
                workButton.Enabled = false;
                dismissedButton.Enabled = true;
                addStripButton.Enabled = true;
            }
        }

        private void addStripButton_Click(object sender, EventArgs e)
        {
            EditPersonForm tmpform = new EditPersonForm(_connection, _operator.nameVal, _org.idVal);
            tmpform.FormClosed += new FormClosedEventHandler(EditPersonForm_FormClosed);
            tmpform.Owner = this;
            tmpform.Show();
        }

        void EditPersonForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender != null)
            {
                if (sender is EditPersonForm)
                {
                    EditPersonForm tmpForm = sender as EditPersonForm;
                    if (tmpForm.DialogResult == DialogResult.OK)
                    {
                        long personID = tmpForm.PersonID;
                        _personTable.Rows.Clear();
                        _personAdapter.Fill(_personTable);
                        _personBS.Position = _personBS.Find(PersonInfo.id, personID);
                    }
                }


            }
        }

        private void editStripButton_Click(object sender, EventArgs e)
        {
            DataRowView person = _personBS.Current as DataRowView;
            if (person == null)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать запись!", "Ошибка выбора анкеты");
                return;
            }
            long personId = (long)person[PersonInfo.id];
            EditPersonForm tmpform = new EditPersonForm(_connection, _operator.nameVal, _org.idVal);
            tmpform.FormClosed += new FormClosedEventHandler(EditPersonForm_FormClosed);
            tmpform.Owner = this;
            tmpform.Show();

            tmpform.PersonID = personId;
        }

        private void delStripButton_Click(object sender, EventArgs e)
        {
            List<DataRowView> personList = this.GetSelectedRows();
            List<long> personIdList = new List<long>();
            StringBuilder personFios = new StringBuilder();
            StringBuilder personFiosNotDelete = new StringBuilder();

            bool breakDelete = false;
            foreach (DataRowView rowItem in personList)
            {
                long personId = (long)rowItem[PersonView.id];
                DataTable tmp = DocsView2.GetDocs(personId, _connection);
                if (tmp.Rows.Count < 1)
                {
                    personIdList.Add(personId);
                    personFios.AppendFormat("{0} {1}\n", rowItem[PersonView.fio].ToString(), rowItem[PersonView.socNumber].ToString());
                }
                else
                {
                    breakDelete = true;
                    personFiosNotDelete.AppendFormat("{0} {1}\n", rowItem[PersonView.fio].ToString(), rowItem[PersonView.socNumber].ToString());

                    foreach (DataRow row in tmp.Rows)
                    {
                        personFiosNotDelete.AppendFormat("\t{0} год {1}, пакет {2}\n", row[DocsView2.regNum], row[DocsView2.repYear], row[DocsView2.listId]);
                    }
                }
            }

            if (breakDelete)
            {
                MainForm.ShowWarningFlexMessage("Удаление анкетных данных невозможно, так как имеются документы СЗВ-1!\n\n" + personFiosNotDelete, "Удаление анкет(ы)");
                return;
            }

            if (personIdList.Count <= 0)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать запись!", "Ошибка выбора анкет(ы)");
                return;
            }

            if (MainForm.ShowQuestionFlexMessage("Вы действительно хотите удалить выбранные анкеты?\n\n" + personFios, "Удаление анкет(ы)") != DialogResult.Yes)
            {
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
            catch (Exception err)
            {
                MainForm.ShowWarningMessage(err.Message, "Внимание");
                _personBS.CancelEdit();
            }
        }

        private void dismissStripButton_Click(object sender, EventArgs e)
        {
            AnketaUvolitForm tmpform = new AnketaUvolitForm();
            tmpform.Owner = this;
            DialogResult dRes = tmpform.ShowDialog(this);
            if (dRes == DialogResult.OK)
            {
                List<DataRowView> persons = this.GetSelectedRows();
                List<long> personIdList = new List<long>();
                foreach (DataRowView rowItem in persons)
                    personIdList.Add((long)rowItem[PersonView.id]);

                PersonOrg.SetStateToUvolit(personIdList, _org.idVal, tmpform.DismissDate, _connection);
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

        private void restoreStripButton_Click(object sender, EventArgs e)
        {
            List<DataRowView> persons = this.GetSelectedRows();
            List<long> personIdList = new List<long>();
            foreach (DataRowView rowItem in persons)
                personIdList.Add((long)rowItem[PersonView.id]);

            PersonOrg.SetStateToRabotaet(personIdList, _org.idVal, _connection);
            foreach (DataRowView rowItem in persons)
            {
                rowItem[PersonView.state] = (int)PersonView.PersonState.Rabotaet;
                rowItem[PersonView.dismissDate] = DBNull.Value;
                rowItem[CHECK] = false;
                rowItem.EndEdit();
            }
            _personBS.MoveFirst();
        }

        private void printAnketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<DataRowView> selectedRowList = this.GetSelectedRows();

            if (selectedRowList.Count <= 0)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать запись!", "Ошибка выбора анкеты");
                return;
            }
            List<DataRow> rowList = new List<DataRow>(selectedRowList.Count);
            foreach (DataRowView rowItem in selectedRowList)
                rowList.Add(rowItem.Row);

            PersonView.Print(rowList, this);
        }

        private void printUnregisteredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<DataRowView> selectedRowList = new List<DataRowView>();
            foreach(DataRowView row in _personBS) {
                string socnum = row[PersonView.socNumber] as string;
                if (socnum == null || socnum.Length <= 0)
                {
                    selectedRowList.Add(row);
                }
            }

            if (selectedRowList.Count <= 0)
            {
                MainForm.ShowInfoMessage("У всех работников заполнены страховые свидетельства!", "Внимание");
                return;
            }
            //List<DataRow> rowList = new List<DataRow>(selectedRowList.Count);
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "windows-1251", null));
            XmlElement root = xml.CreateElement("root");
            XmlElement orgname = xml.CreateElement("org_name");
            XmlElement orgregnum = xml.CreateElement("org_regnum");
            XmlElement personlist = xml.CreateElement("person_list");

            orgname.InnerText = _org.nameVal;
            orgregnum.InnerText = _org.regnumVal;
            root.AppendChild(orgname);
            root.AppendChild(orgregnum);

            foreach (DataRowView rowItem in selectedRowList)
            {
                XmlElement person = xml.CreateElement("person");
                XmlElement fio = xml.CreateElement("fio");
                XmlElement bdate = xml.CreateElement("borndate");
                fio.InnerText = rowItem[PersonView.fio] as string;
                bdate.InnerText = string.Format("{0:dd.MM.yyyy}",rowItem[PersonView.birthday]);
                person.AppendChild(fio);
                person.AppendChild(bdate);
                personlist.AppendChild(person);                
            }

            root.AppendChild(personlist);
            xml.AppendChild(root);

            if (_wbUnregisteredPewrsons == null)
            {
                _wbUnregisteredPewrsons = new WebBrowser();
                _wbUnregisteredPewrsons.Visible = false;
                _wbUnregisteredPewrsons.Parent = this;
                _wbUnregisteredPewrsons.ScriptErrorsSuppressed = true;
                _wbUnregisteredPewrsons.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(_wbUnregisteredPewrsons_DocumentCompleted);
            }
            _wbUnregisteredPewrsons.Tag = xml;
            string file = System.IO.Path.GetFullPath(Properties.Settings.Default.report_unregistered_list);
            _wbUnregisteredPewrsons.Navigate(file);
        }

        void _wbUnregisteredPewrsons_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;
            if (wb == null)
            {
                return;
            }
            XmlDocument xml = wb.Tag as XmlDocument;
            if (xml == null)
            {
                MainForm.ShowInfoMessage("отчет не может быть отображен, та как нет входных данных данных!", "Внимание");
                return;
            }
            HtmlDocument htmlDoc = wb.Document;
            htmlDoc.InvokeScript("setAllData", new object[] { xml.InnerXml });
            //MyPrinter.ShowWebPage(wb);
            MyPrinter.ShowPrintPreviewWebPage(wb);
        }

        private void personView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                {
                    personView.EndEdit();
                    bool allchecked = _personBS.Cast<DataRowView>().All(row => (bool)row[CHECK]);
                    foreach (DataRowView row in _personBS)
                        row[CHECK] = !allchecked;
                }
                this.personView.Refresh();
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void personView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            editStripButton_Click(sender, e);
        }

        private void attachToOrgButton_Click(object sender, EventArgs e)
        {
            DataRowView curPersonRow = _personBS.Current as DataRowView;
            if (curPersonRow == null)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать запись!", "Внимание");
                return;
            }
            AnketaPersonOrgForm tmpform = new AnketaPersonOrgForm(curPersonRow.Row, _operator, _org.idVal, _connection);
            tmpform.FormClosed += new FormClosedEventHandler(AnketaPersonOrgForm_FormClosed);
            tmpform.Owner = this;
            tmpform.ShowDialog(this);
        }

        void AnketaPersonOrgForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender != null)
            {
                if (sender is AnketaPersonOrgForm)
                {
                    AnketaPersonOrgForm tmpForm = sender as AnketaPersonOrgForm;
                    if (tmpForm.DialogResult == DialogResult.OK)
                    {
                        if (_personBS.Current != null)
                        {
                            long personID = (long)(_personBS.Current as DataRowView)[PersonView.id];
                            _personTable.Rows.Clear();
                            _personAdapter.Fill(_personTable);
                            _personBS.Position = _personBS.Find(PersonInfo.id, personID);
                        }
                    }
                }
            }
        }

        private void personView_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void personView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Apps || (e.Shift && e.KeyCode == Keys.F10))
                {
                    DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
                    if (currentCell == null)
                        return;
                    ContextMenuStrip cms = cmsPerson;
                    if (cms == null)
                        return;
                    Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                    Point p = new Point(r.Left, r.Top);
                    cms.Show((sender as DataGridView), p);
                }

                if (e.KeyCode == Keys.Delete)
                {
                    delStripButton_Click(sender, e);
                }

                if (e.KeyCode == Keys.Enter)
                {
                    editStripButton_Click(sender, e);
                }

                if (e.KeyCode == Keys.Space)
                {
                    if ((sender as DataGridView).CurrentRow == null)
                        return;

                    (_personBS.Current as DataRowView)[CHECK] = !Convert.ToBoolean((_personBS.Current as DataRowView)[CHECK]);
                    (sender as DataGridView).EndEdit();
                    (sender as DataGridView).Refresh();
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void personView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
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

        private void personView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip menu = cmsPerson;
                    if (menu == null)
                        return;

                    DataGridView dataView = sender as DataGridView;
                    ToolStripItem[] items; //Массив в который возвращает элементы метод Find
                    List<string> menuItems = new List<string>(); //Список элементов которые нужно включать\выключать
                    menuItems.Add("editPersonMenuItem");
                    menuItems.Add("delpersonMenuItem");
                    menuItems.Add("dismissPersonMenuItem");
                    menuItems.Add("restorePersonMenuItem");
                    menuItems.Add("printAnketsMenuItem");
                    menuItems.Add("printUnregisteredMenuItem");

                    int currentMouseOverRow = dataView.HitTest(e.X, e.Y).RowIndex;
                    bool isEnabled = !(currentMouseOverRow < 0);
                    foreach (string t in menuItems)
                    {
                        items = menu.Items.Find(t, true);
                        if (items.Any())
                            items[0].Enabled = isEnabled;
                    }

                    menuItems = new List<string>(); //Список элементов которые нужно принудительно выключать
                    if(this.restoreStripButton.Enabled)
                        menuItems.Add("dismissPersonMenuItem");
                    else
                        menuItems.Add("restorePersonMenuItem");
                    foreach (string t in menuItems)
                    {
                        items = menu.Items.Find(t, true);
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

        private void addPersonMenuItem_Click(object sender, EventArgs e)
        {
            addStripButton_Click(sender, e);
        }

        private void editPersonMenuItem_Click(object sender, EventArgs e)
        {
            editStripButton_Click(sender, e);
        }

        private void delPersonMenuItem_Click(object sender, EventArgs e)
        {
            delStripButton_Click(sender, e);
        }

        private void dismissPersonMenuItem_Click(object sender, EventArgs e)
        {
            dismissStripButton_Click(sender, e);
        }

        private void restorePersonMenuItem_Click(object sender, EventArgs e)
        {
            restoreStripButton_Click(sender, e);
        }

        private void printAnketsMenuItem_Click(object sender, EventArgs e)
        {
            printAnketsToolStripMenuItem_Click(sender, e);
        }

        private void printUnregisteredMenuItem_Click(object sender, EventArgs e)
        {
            printUnregisteredToolStripMenuItem_Click(sender, e);
        }

        #endregion
    }
}
