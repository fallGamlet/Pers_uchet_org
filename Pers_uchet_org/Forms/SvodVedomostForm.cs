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
    public partial class SvodVedomostForm : Form
    {
        #region Поля
        DataTable _mergeTable;
        BindingSource _mergeBS;

        string _connection;
        Org _org;
        Operator _operator;

        const string viewStateText = "Просмотреть";
        const string editStateText = "Изменить";
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
            this.Text += " - " + _org.regnumVal;

            this.yearBox.Value = MainForm.RepYear;
            this.RefillData(MainForm.RepYear);
            this.mergeView.Sorted += new EventHandler(mergeView_Sorted);
            _mergeBS.CurrentChanged += new EventHandler(_mergeBS_CurrentChanged);
            _mergeBS.MoveLast();
        }
        #endregion

        #region Методы - свои
        public void RefillData(int rep_year)
        {
            if (_mergeTable == null)
                _mergeTable = MergiesView.CreateTable();
            else
                _mergeTable.Rows.Clear();

            if (_mergeBS == null)
                _mergeBS = new BindingSource();

            string commText = MergiesView.GetSelectText(_org.idVal, rep_year);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(commText, _connection);
            adapter.Fill(_mergeTable);

            this.mergeView.AutoGenerateColumns = false;
            this.mergeView.DataSource = _mergeBS;
            _mergeBS.DataSource = _mergeTable;

            this.MarkActualRow();
        }

        private void MarkActualRow()
        {
            int icur = _mergeBS.Find(MergiesView.actual, true);
            if (icur != -1)
            {
                this.mergeView.Rows[icur].DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }
        #endregion

        #region Методы - обработчики событий
        private void yearBox_ValueChanged(object sender, EventArgs e)
        {
            MainForm.RepYear = (int)yearBox.Value;
            this.RefillData((int)yearBox.Value);
        }

        void mergeView_Sorted(object sender, EventArgs e)
        {
            this.MarkActualRow();
        }

        void _mergeBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView curRow = _mergeBS.Current as DataRowView;
            if (curRow == null)
            {
                this.editStripButton.Enabled = false;
                this.delStripButton.Enabled = false;
                this.printStripButton.Enabled = false;
                return;
            }

            this.editStripButton.Enabled = true;
            this.delStripButton.Enabled = true;
            this.printStripButton.Enabled = true;

            if ((bool)curRow[MergiesView.actual])
            {
                this.editStripButton.Text = editStateText;
            }
            else
            {
                this.editStripButton.Text = viewStateText;
            }
        }

        private void addStripButton_Click(object sender, EventArgs e)
        {
            SvodVedomostEditDocumentForm tmpform = new SvodVedomostEditDocumentForm(_connection, _operator, _org);
            tmpform.Owner = this;
            tmpform.RepYear = (int)this.yearBox.Value;
            DialogResult dRes = tmpform.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                RefillData((int)this.yearBox.Value);
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
            SvodVedomostEditDocumentForm tmpform = new SvodVedomostEditDocumentForm(_connection, _operator, _org, curRow.Row);
            tmpform.Owner = this;
            DialogResult dRes = tmpform.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                RefillData((int)this.yearBox.Value);
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

            if (MainForm.ShowQuestionMessage("Вы действительно хотите удалить сводную ведомость СЗВ-3?", "Удаление сводной") != DialogResult.Yes)
            {
               return;
            }
            Mergies.DeleteExecute(curRow.Row, _connection);
            RefillData((int)this.yearBox.Value);
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
            tmpform.ShowDialog();
        }

        private void mergeView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editStripButton_Click(sender, e);
        }

        private void mergeView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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
                    Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                    Point p = new Point(r.Left, r.Top);
                    cms.Show((sender as DataGridView), p);
                }

                if (e.KeyCode == Keys.Delete)
                {
                    delStripButton_Click(sender, e);
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
                    List<string> menuItems = new List<string>(); //Список элементов которые нужно включать\выключать
                    menuItems.Add("editSvodMenuItem");
                    menuItems.Add("delSvodMenuItem");
                    menuItems.Add("printSvodMenuItem");

                    int currentMouseOverRow = dataView.HitTest(e.X, e.Y).RowIndex;
                    bool isEnabled = !(currentMouseOverRow < 0);
                    foreach (string t in menuItems)
                    {
                        items = menu.Items.Find(t, false);
                        if (items.Any())
                            items[0].Enabled = isEnabled;
                    }

                    menuItems = new List<string>(); //Список элементов которые нужно принудительно выключать
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
