using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Pers_uchet_org.Forms
{
    public partial class SvodVedomostGetPacketsForm : Form
    {
        #region Поля

        private DataTable _packetTable;
        private BindingSource _packetBS;
        private string _connection;
        private Org _org;
        private int _repYear;
        private const string Check = "check";

        #endregion

        #region Конструктор и инициализотор

        public SvodVedomostGetPacketsForm(Org org, int repYear, string connectionStr)
        {
            InitializeComponent();

            _org = org;
            _connection = connectionStr;
            _repYear = repYear;
        }

        private void SvodVedomostGetPacketsForm_Load(object sender, EventArgs e)
        {
            orgnameBox.Text = _org.nameVal;
            regnumBox.Text = _org.regnumVal;
            yearBox.Text = _repYear.ToString();

            _packetTable = ListsView2.CreateTable();
            _packetTable.Columns.Add(Check, typeof (bool)).DefaultValue = false;
            SQLiteDataAdapter adapter =
                new SQLiteDataAdapter(ListsView2.GetSelectText(_org.idVal, _repYear, ListTypes.PersonalInfo),
                    _connection);
            adapter.Fill(_packetTable);

            _packetBS = new BindingSource();
            _packetBS.DataSource = _packetTable;

            // присвоение источника dataGrid
            packetView.AutoGenerateColumns = false;
            packetView.Columns["checkColumn"].DataPropertyName = Check;
            packetView.Columns["packetNumColumn"].DataPropertyName = ListsView2.id;
            packetView.Columns["docCountColumn"].DataPropertyName = ListsView2.countDocs;
            packetView.DataSource = _packetBS;
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
                    if ((bool) rowItem[Check])
                    {
                        markedPackets.AddLast((long) rowItem[ListsView.id]);
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
            if (DialogResult == DialogResult.OK && MarckedPackets.Length == 0)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать хотя бы один пакет", "Внимание");
                e.Cancel = true;
                return;
            }

            if (DialogResult == DialogResult.OK &&
                MainForm.ShowQuestionFlexMessage("Вы собираетесь заполнить документ \"СЗВ-З - Сводная ведомость\"\n" +
                                                 "расчётными данными, полученными в результате суммирования\n" +
                                                 "введённых в программе сведений по работникам Вашей организации!\n\n" +
                                                 "Напоминаем, что форма \"СЗВ-З - Сводная ведомость\" должна\n" +
                                                 "содержать сведения в целом по организации за отчётный год\n" +
                                                 "такие же, какие были предоставлены в налоговую инспекцию!\n\n" +
                                                 "Вы уверенны, что хотите сформировать \"СЗВ-З - Сводная ведомость\"\n" +
                                                 "на основе расчётных данных?", "Внимание") != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void packetView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                {
                    (sender as DataGridView).EndEdit();
                    bool allchecked = _packetBS.Cast<DataRowView>().All(row => (bool) row[Check]);
                    foreach (DataRowView row in _packetBS)
                        row[Check] = !allchecked;
                }
                (sender as DataGridView).Refresh();
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void packetView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    if ((sender as DataGridView).CurrentRow == null)
                        return;

                    (_packetBS.Current as DataRowView)[Check] =
                        !Convert.ToBoolean((_packetBS.Current as DataRowView)[Check]);
                    (sender as DataGridView).EndEdit();
                    (sender as DataGridView).Refresh();
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        #endregion
    }
}