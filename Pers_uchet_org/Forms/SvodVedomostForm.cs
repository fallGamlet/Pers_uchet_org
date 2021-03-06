﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class SvodVedomostForm : Form
    {
        #region Поля

        private DataTable _mergeTable;
        private BindingSource _mergeBS;

        private string _connection;
        private Org _org;
        private Operator _operator;
        // привилегия
        private string _privilege;

        private const string ViewStateText = "Просмотреть";
        private const string EditStateText = "Изменить";

        // веб браузер для формирования отчета СЗВ-3
        WebBrowser _wb;
        #endregion

        #region Конструктор и Инициализатор

        public SvodVedomostForm(Org org, Operator oper, string connectionStr)
        {
            InitializeComponent();
            _connection = connectionStr;
            _org = org;
            _operator = oper;
        }

        private void SvodVedomostForm_Load(object sender, EventArgs e)
        {
            Text += " - " + _org.regnumVal;

            // получить код привилегии (уровня доступа) Оператора к Организации
            if (_operator.IsAdmin())
                _privilege = OperatorOrg.GetPrivilegeForAdmin();
            else
                _privilege = OperatorOrg.GetPrivilege(_operator.idVal, _org.idVal, _connection);

            yearBox.Value = MainForm.RepYear;
            RefillData(MainForm.RepYear);
            mergeView.Sorted += new EventHandler(mergeView_Sorted);
            _mergeBS.CurrentChanged += new EventHandler(_mergeBS_CurrentChanged);
            _mergeBS.MoveLast();

            SetPrivilege(_privilege);
        }

        #endregion

        #region Методы - свои

        public void RefillData(int repYear)
        {
            if (_mergeTable == null)
                _mergeTable = MergiesView.CreateTable();
            else
                _mergeTable.Rows.Clear();

            if (_mergeBS == null)
                _mergeBS = new BindingSource();

            string commText = MergiesView.GetSelectText(_org.idVal, repYear);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(commText, _connection);
            adapter.Fill(_mergeTable);

            mergeView.AutoGenerateColumns = false;
            mergeView.DataSource = _mergeBS;
            _mergeBS.DataSource = _mergeTable;

            MarkActualRow();

            SetPrivilege(_privilege);
        }

        private void MarkActualRow()
        {
            int icur = _mergeBS.Find(MergiesView.actual, true);
            if (icur != -1)
            {
                mergeView.Rows[icur].DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }

        private void SetPrivilege(string privilegeCode)
        {
            bool readOnly = OperatorOrg.GetStajDohodDataAccesseCode(privilegeCode) == 1;
            if (readOnly)
            {
                addStripButton.Enabled = false;
                editStripButton.Enabled = false;
                delStripButton.Enabled = false;

                mergeView.CellDoubleClick -= mergeView_CellDoubleClick;

                addStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                editStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
                delStripButton.ToolTipText = "У вас недостаточно прав. Обратитесь к администратору";
            }
        }

        #endregion

        #region Методы - обработчики событий

        private void yearBox_ValueChanged(object sender, EventArgs e)
        {
            MainForm.RepYear = (int)yearBox.Value;
            RefillData((int)yearBox.Value);
        }

        private void mergeView_Sorted(object sender, EventArgs e)
        {
            MarkActualRow();
        }

        private void _mergeBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView curRow = _mergeBS.Current as DataRowView;
            if (curRow == null)
            {
                editStripButton.Enabled = false;
                delStripButton.Enabled = false;
                printStripButton.Enabled = false;
                return;
            }

            editStripButton.Enabled = true;
            delStripButton.Enabled = true;
            printStripButton.Enabled = true;

            if ((bool)curRow[MergiesView.actual])
            {
                editStripButton.Text = EditStateText;
            }
            else
            {
                editStripButton.Text = ViewStateText;
            }

            SetPrivilege(_privilege);
        }

        private void addStripButton_Click(object sender, EventArgs e)
        {
            SvodVedomostEditDocumentForm tmpform = new SvodVedomostEditDocumentForm(_connection, _operator, _org);
            tmpform.Owner = this;
            tmpform.RepYear = (int)yearBox.Value;
            DialogResult dRes = tmpform.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                RefillData((int)yearBox.Value);
            }
        }

        private void editStripButton_Click(object sender, EventArgs e)
        {
            DataRowView curRow = _mergeBS.Current as DataRowView;
            if (curRow == null)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать запись!", "Ошибка выбора сводной");
                return;
            }
            SvodVedomostEditDocumentForm tmpform = new SvodVedomostEditDocumentForm(_connection, _operator, _org,
                curRow.Row);
            tmpform.Owner = this;
            DialogResult dRes = tmpform.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                RefillData((int)yearBox.Value);
            }
        }

        private void delStripButton_Click(object sender, EventArgs e)
        {
            DataRowView curRow = _mergeBS.Current as DataRowView;
            if (curRow == null)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать запись!", "Ошибка выбора сводной");
                return;
            }

            if (
                MainForm.ShowQuestionMessage("Вы действительно хотите удалить сводную ведомость СЗВ-3?",
                    "Удаление сводной") != DialogResult.Yes)
            {
                return;
            }
            Mergies.DeleteExecute(curRow.Row, _connection);
            RefillData((int)yearBox.Value);
        }

        private void printStripButton_Click(object sender, EventArgs e)
        {
            DataRowView curRow = _mergeBS.Current as DataRowView;
            if (curRow == null)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать запись!", "Ошибка выбора сводной");
                return;
            }

            SvodVedomostPrintForm tmpform = new SvodVedomostPrintForm();
            tmpform.Owner = this;
            DialogResult dRes = tmpform.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                if (_wb == null)
                {
                    _wb = new WebBrowser();
                    _wb.Visible = false;
                    _wb.Parent = this;
                    _wb.ScriptErrorsSuppressed = true;
                    _wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(_wb_DocumentCompleted);
                }
                _wb.Tag = new object[] {
                                    (long)curRow[Mergies.id],
                                    tmpform.Performer,
                                    tmpform.PrintDate
                                    };
                string file = System.IO.Path.GetFullPath(Properties.Settings.Default.report_szv3);
                _wb.Navigate(file);
            }
        }

        void _wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;
            if(wb == null)
            {
                return;
            }
            object[] tags = wb.Tag as object[];
            long merge_id = (long)tags[0];
            string performer = tags[1] as string;
            DateTime printDate = (DateTime)tags[2];
            System.Xml.XmlDocument xml = Szv3Xml.GetXml(merge_id, _connection);
            HtmlDocument htmlDoc = wb.Document;
            string repYear = this.yearBox.Value.ToString();
            htmlDoc.InvokeScript("setRegnum", new object[] { _org.regnumVal });
            htmlDoc.InvokeScript("setOrgName", new object[] { _org.nameVal });
            htmlDoc.InvokeScript("setYear", new object[] { repYear });
            htmlDoc.InvokeScript("setSzv3Xml", new object[] { xml.InnerXml });
            htmlDoc.InvokeScript("setPrintDate", new object[] { printDate.ToString("dd.MM.yyyy") });
            htmlDoc.InvokeScript("setPerformer", new object[] { performer });
            htmlDoc.InvokeScript("setChiefPost", new object[] { _org.chiefpostVal });
            //MyPrinter.ShowWebPage(wb);
            MyPrinter.ShowPrintPreviewWebPage(wb);
        }

        private void mergeView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editStripButton_Click(sender, e);
        }

        private void mergeView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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

        private void mergeView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Apps || (e.Shift && e.KeyCode == Keys.F10))
                {
                    DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
                    if (currentCell == null)
                        return;
                    ContextMenuStrip cms = cmsSvod;
                    if (cms == null)
                        return;
                    Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex,
                        currentCell.RowIndex, false);
                    Point p = new Point(r.Left, r.Top);

                    DataGridView dataView = sender as DataGridView;
                    ToolStripItem[] items; //Массив в который возвращает элементы метод Find
                    List<string> menuItems = new List<string>
                    {
                        "editSvodMenuItem",
                        "delSvodMenuItem",
                        "printSvodMenuItem"
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
                        menuItems.Add("addSvodMenuItem");
                        menuItems.Add("editSvodMenuItem");
                        menuItems.Add("delSvodMenuItem");
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
                    delStripButton_Click(sender, e);
                }

                if (e.KeyCode == Keys.Enter)
                {
                    bool readOnly = OperatorOrg.GetStajDohodDataAccesseCode(_privilege) == 1;
                    if (readOnly)
                    {
                        MainForm.ShowInfoMessage("У вас недостаточно прав. Обратитесь к администратору.", "Внимание");
                        return;
                    }
                    editSvodMenuItem_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void mergeView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip menu = cmsSvod;
                    if (menu == null)
                        return;

                    DataGridView dataView = sender as DataGridView;
                    ToolStripItem[] items; //Массив в который возвращает элементы метод Find
                    List<string> menuItems = new List<string>
                    {
                        "editSvodMenuItem",
                        "delSvodMenuItem",
                        "printSvodMenuItem"
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
                        menuItems.Add("addSvodMenuItem");
                        menuItems.Add("editSvodMenuItem");
                        menuItems.Add("delSvodMenuItem");
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

        private void addSvodMenuItem_Click(object sender, EventArgs e)
        {
            addStripButton_Click(sender, e);
        }

        private void editSvodMenuItem_Click(object sender, EventArgs e)
        {
            editStripButton_Click(sender, e);
        }

        private void delSvodMenuItem_Click(object sender, EventArgs e)
        {
            delStripButton_Click(sender, e);
        }

        private void printSvodMenuItem_Click(object sender, EventArgs e)
        {
            printStripButton_Click(sender, e);
        }

        #endregion
    }
}