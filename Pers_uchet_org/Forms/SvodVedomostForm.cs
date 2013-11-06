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
        #endregion

        #region Конструктор и Инициализатор
        public SvodVedomostForm(Org org, Operator oper,string connectionStr)
        {
            InitializeComponent();
            _connection = connectionStr;
            _org = org;
            _operator = oper;
        }

        private void SvodVedomostForm_Load(object sender, EventArgs e)
        {
            this.yearBox.Value = MainForm.RepYear;

            this.RefillData(MainForm.RepYear);
            this.mergeView.Sorted += new EventHandler(mergeView_Sorted);
        }

        void mergeView_Sorted(object sender, EventArgs e)
        {
            this.MarkActualRow();
        }
        #endregion

        #region Методы - свои
        public void RefillData(int rep_year)
        {
            if (_mergeTable == null)
                _mergeTable = MergiesView.CreateTable();
            else
                _mergeTable.Rows.Clear();

            if(_mergeBS == null)
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
        private void addButton_Click(object sender, EventArgs e)
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

        private void editButton_Click(object sender, EventArgs e)
        {
            DataRowView curRow = _mergeBS.Current as DataRowView;
            if (curRow == null)
            {
                MainForm.ShowInfoMessage("Необходимо сначала выбрать запись для редактирования!", "Внимание");
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

        private void removeButton_Click(object sender, EventArgs e)
        {

        }

        private void printButton_Click(object sender, EventArgs e)
        {
            SvodVedomostPrintForm tmpform = new SvodVedomostPrintForm();
            tmpform.Owner = this;
            tmpform.ShowDialog();
        }

        private void yearBox_ValueChanged(object sender, EventArgs e)
        {
            MainForm.RepYear = (int)yearBox.Value;
            this.RefillData((int)yearBox.Value);
        }
        #endregion
    }
}
