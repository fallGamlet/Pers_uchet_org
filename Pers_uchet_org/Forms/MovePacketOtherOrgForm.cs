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
    public partial class MovePacketForm : Form
    {
        #region Поля
        const string viewCol = "view_col";
        private string _connection;
        private long listId;
        private Operator _operator;
        private DataTable _orgTable;
        private BindingSource _orgBS;
        
        
        #endregion

        public MovePacketForm()
        {
            InitializeComponent();
        }

        public MovePacketForm(Operator _operator, string _connection, long listId)
            : this()
        {
            this._operator = _operator;
            this._connection = _connection;
            this.listId = listId;
        }

        private void MovePacketForm_Load(object sender, EventArgs e)
        {
            label1.Text += listId;
            _orgTable = Org.CreateTable();
            _orgTable.Columns.Add(viewCol);
            _orgBS = new BindingSource();
            _orgBS.DataSource = _orgTable;

            orgsComboBox.DataSource = _orgBS;
            orgsComboBox.ValueMember = Org.id;
            orgsComboBox.DisplayMember = viewCol;

            if (_operator == null)
            {
                MainForm.ShowErrorMessage("Пользователь не найден", "Ошибка");
                this.Close();
                return;
            }

            string selectText;
            if (_operator.candeleteVal == 0)
            {
                selectText = Org.GetSelectCommandText();
            }
            else
            {
                selectText = Org.GetSelectTextByOperator(_operator.idVal);
            }

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectText, _connection);
            _orgTable.Rows.Clear();

            adapter.Fill(_orgTable);
            foreach (DataRow rowItem in _orgTable.Rows)
            {
                rowItem[viewCol] = string.Format("{0}    {1}", rowItem[Org.regnum], rowItem[Org.name]);
            }
            _orgTable.AcceptChanges();
            if (_orgBS.Count < 1)
                moveButton.Enabled = false;
            //TODO: this.SetPrivilege();
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            StajDohodForm.NewOrgId = (long)orgsComboBox.SelectedValue;
        }
    }
}
