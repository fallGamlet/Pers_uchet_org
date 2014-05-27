using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class CopyPacketOtherOrgForm : Form
    {
        #region Поля

        private const string ViewCol = "view_col";
        private string _connection;
        private long _listId;
        private Operator _operator;
        private DataTable _orgTable;
        private BindingSource _orgBS;

        #endregion

        public CopyPacketOtherOrgForm()
        {
            InitializeComponent();
        }

        public CopyPacketOtherOrgForm(Operator @operator, string connection, long listId)
            : this()
        {
            _operator = @operator;
            _connection = connection;
            _listId = listId;
        }

        private void CopyPacketForm_Load(object sender, EventArgs e)
        {
            label2.Text += _listId;
            _orgTable = Org.CreateTable();
            _orgTable.Columns.Add(ViewCol);
            _orgBS = new BindingSource();
            _orgBS.DataSource = _orgTable;

            orgsComboBox.DataSource = _orgBS;
            orgsComboBox.ValueMember = Org.id;
            orgsComboBox.DisplayMember = ViewCol;

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
                rowItem[ViewCol] = string.Format("{0}    {1}", rowItem[Org.regnum], rowItem[Org.name]);
            }
            _orgTable.AcceptChanges();
            if (_orgBS.Count < 1)
                copyButton.Enabled = false;
            //TODO: this.SetPrivilege();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            StajDohodForm.NewOrgId = (long)orgsComboBox.SelectedValue;
        }
    }
}