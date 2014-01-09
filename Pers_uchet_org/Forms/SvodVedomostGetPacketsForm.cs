using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Pers_uchet_org.Forms
{
    public partial class SvodVedomostGetPacketsForm : Form
    {
        #region Поля
        DataTable _packetTable;
        BindingSource _packetBS;
        string _connection;
        Org _org;
        int _repYear;
        const string CHECK = "check";
        #endregion

        #region Конструктор и инициализотор
        public SvodVedomostGetPacketsForm(Org org, int rep_year, string connectionStr)
        {
            InitializeComponent();

            _org = org;
            _connection = connectionStr;
            _repYear = rep_year;
        }

        private void SvodVedomostGetPacketsForm_Load(object sender, EventArgs e)
        {
            this.orgnameBox.Text = _org.nameVal;
            this.regnumBox.Text = _org.regnumVal;
            this.yearBox.Text = _repYear.ToString();

            _packetTable = ListsView.CreateTable();
            _packetTable.Columns.Add(CHECK, typeof(bool)).DefaultValue = false;

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(ListsView.GetSelectText(_org.idVal, _repYear), _connection);
            adapter.Fill(_packetTable);

            this.packetView.AutoGenerateColumns = false;
            _packetBS = new BindingSource();
            this.packetView.DataSource = _packetBS;
            _packetBS.DataSource = _packetTable;
        }
        #endregion

        #region Свойства
        public long[] MarckedPackets
        {
            get
            {
                LinkedList<long> markedPackets = new LinkedList<long>();
                foreach (DataRowView rowItem in _packetBS)
                {
                    if ((bool)rowItem[CHECK])
                    {
                        markedPackets.AddLast((long)rowItem[ListsView.id]);
                    }
                }
                return markedPackets.ToArray();
            }
        }
        #endregion

        #region Методы - обработчики событий
        private void saveButton_Click(object sender, EventArgs e)
        {
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        private void SvodVedomostGetPacketsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK && this.MarckedPackets.Length == 0)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать хотя бы один пакет", "Внимание");
                e.Cancel = true;
            }
        }
        #endregion
    }
}
