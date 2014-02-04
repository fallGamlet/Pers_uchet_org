using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Diagnostics;
using OpenMcdf;
using System.Data.SQLite;

namespace Pers_uchet_org
{
    public partial class ExchangeForm : Form
    {
        #region Поля
        private Operator _operator;
        private Org _organization;
        private string _connection;
        // таблица пакетов
        DataTable _listsTable;
        // биндинг сорс для таблиц
        BindingSource _listsBS;
        // адаптер для чтения данных из БД
        SQLiteDataAdapter _listsAdapter;
        // названия добавочного виртуального столбца
        const string CHECK = "check";
        // переменная содержит текущий используемый год
        int _repYear;

        // количество выбранных документов
        long _checkedCountDocs;
        // количество выбранных пакетов
        long _checkedCountLists;

        // количество документов по сводной
        long _mergiesCountDocs;
        // количество пакетов по сводной
        long _mergiesCountLists;
        // label с сообщением о ненайденной сводной

        CompoundFile _container;
        byte[] _diskKey;
        byte[] _diskTable;
        #endregion

        #region Конструктор и инициализатор
        private ExchangeForm()
        {
            InitializeComponent();
            szv3WarningLabel = new System.Windows.Forms.Label();
        }

        public ExchangeForm(Operator _operator, Org org, string mainConnection)
            : this()
        {
            this._operator = _operator;
            this._organization = org;
            this._connection = mainConnection;
        }

        private void ExchangeForm_Load(object sender, EventArgs e)
        {
            yearBox.Value = MainForm.RepYear;
            tabControl1.SelectedTab = tabPage2;
            tabControl1.TabPages.Remove(tabPage1);

            // инициализация таблиц
            _listsTable = ListsView2.CreateTable();
            _listsTable.Columns.Add(CHECK, typeof(bool));
            _listsTable.Columns[CHECK].DefaultValue = false;

            // инициализация биндинг сорса к таблице пакетов
            _listsBS = new BindingSource();
            //_listsBS.CurrentChanged += new EventHandler(_listsBS_CurrentChanged);
            //_listsBS.ListChanged += new ListChangedEventHandler(_listsBS_ListChanged);
            _listsBS.DataSource = _listsTable;

            // присвоение источника dataGrid
            this.packetsView.AutoGenerateColumns = false;
            this.packetsView.Columns["checkColumn"].DataPropertyName = CHECK;
            this.packetsView.Columns["packetNumColumn"].DataPropertyName = ListsView2.id;
            this.packetsView.Columns["docCountColumn"].DataPropertyName = ListsView2.countDocs;
            this.packetsView.DataSource = _listsBS;

            ReloadLists();

            driveBox_Click(sender, e);
            flashBox_Click(sender, e);
        }
        #endregion

        #region Методы - свои
        private void DisableControlsBeforeCreateFile()
        {
            yearGroupBox.Enabled = false;
            tabControl1.Enabled = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            xmlPathButton.Enabled = false;
            xmlPathTextBox.Enabled = false;
            packetsView.Enabled = false;
        }

        private void EnableControlsAfterCreateFile()
        {
            yearGroupBox.Enabled = true;
            tabControl1.Enabled = true;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            flashRButton_CheckedChanged(new object(), new EventArgs());
            xmlPathButton.Enabled = true;
            xmlPathTextBox.Enabled = true;
            packetsView.Enabled = true;
        }

        private void ReloadLists()
        {
            if (_listsBS == null)
                return;
            if (_listsTable == null)
                return;

            //отключение события ListChangedEventHandler , что б не мерцали кнопки при обновлении
            _listsBS.RaiseListChangedEvents = false;

            _listsTable.Clear();
            string commandStr = ListsView2.GetSelectText(_organization.idVal, _repYear, ListTypes.PersonalInfo);
            _listsAdapter = new SQLiteDataAdapter(commandStr, _connection);
            _listsAdapter.Fill(_listsTable);

            //включение события ListChangedEventHandler 
            _listsBS.RaiseListChangedEvents = true;
            _listsBS.ResetBindings(false);

            _checkedCountLists = 0;
            _checkedCountDocs = 0;

            checkedPacketCountBox.Text = _checkedCountLists.ToString();
            chekedDocCountBox.Text = _checkedCountDocs.ToString();

            DisableCheckBoxInPacketView();
        }

        private void DisableCheckBoxInPacketView()
        {
            for (int i = 0; i < packetsView.Rows.Count; i++)
            {
                if (Convert.ToInt32((_listsBS[i] as DataRowView)[ListsView2.countPens]) > 0)
                    packetsView["checkColumn", i].ReadOnly = true;
            }

        }

        private long[] MarkedLists()
        {
            List<long> lists = new List<long>();
            foreach (DataRowView row in _listsBS)
            {
                if((bool)row[CHECK])
                    lists.Add((long)row[ListsView2.id]);
            }
            return lists.ToArray();
        }

        private void SumCheckedLists()
        {
            packetsView.EndEdit();
            _checkedCountLists = 0;
            _checkedCountDocs = 0;
            foreach (DataRowView row in _listsBS.Cast<DataRowView>().Where(row => Convert.ToBoolean(row[CHECK])))
            {
                _checkedCountLists++;
                _checkedCountDocs += Convert.ToInt32(row[ListsView2.countDocs]);
            }
            checkedPacketCountBox.Text = _checkedCountLists.ToString();
            chekedDocCountBox.Text = _checkedCountDocs.ToString();
        }

        private void GetCountsFromMergies()
        {
            _mergiesCountLists = 0;
            _mergiesCountDocs = 0;

            using (SQLiteConnection connection = new SQLiteConnection(_connection))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = MergiesView.GetSelectActualText(_organization.idVal, _repYear);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        _mergiesCountLists = Convert.ToInt32(reader[MergiesView.listCount]);
                        _mergiesCountDocs = Convert.ToInt32(reader[MergiesView.docCount]);
                        szvGroupBox.Visible = true;
                        this.tabPage2.Controls.Remove(this.szv3WarningLabel);

                    }
                    else
                    {
                        _mergiesCountLists = -1;
                        _mergiesCountDocs = -1;

                        szvGroupBox.Visible = false;
                        // 
                        // szv3WarningLabel
                        // 
                        this.szv3WarningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        this.szv3WarningLabel.ForeColor = System.Drawing.Color.Red;
                        this.szv3WarningLabel.Location = new System.Drawing.Point(6, 342);
                        this.szv3WarningLabel.Name = "szv3WarningLabel";
                        this.szv3WarningLabel.Size = new System.Drawing.Size(286, 53);
                        this.szv3WarningLabel.TabIndex = 12;
                        this.szv3WarningLabel.Text = "Сводная ведомость (СЗВ-3) не обнаружена!\r\nФормирование электронных данных невозможно!";
                        this.szv3WarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                        this.tabPage2.Controls.Add(this.szv3WarningLabel);
                    }
                }
            }

            packetCountBox.Text = _mergiesCountLists.ToString();
            docCountBox.Text = _mergiesCountDocs.ToString();
        }
        #endregion

        #region Методы - обработчики событий
        private void driveBox_Click(object sender, EventArgs e)
        {
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            driveBox.Items.Clear();
            foreach (DriveInfo di in driveInfo)
            {
                if ((di.DriveType == DriveType.CDRom || (di.DriveType == DriveType.Removable && di.Name == "A:\\")) && di.IsReady)
                    driveBox.Items.Add(di.Name + " - " + di.VolumeLabel);
            }
            if (driveBox.Items.Count > 0)
                driveBox.SelectedItem = driveBox.Items[0];
        }

        private void flashBox_Click(object sender, EventArgs e)
        {
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            flashBox.Items.Clear();
            foreach (DriveInfo di in driveInfo)
            {
                if (di.DriveType == DriveType.Removable && di.Name != "A:\\" && di.Name != "B:\\" && di.IsReady)
                    flashBox.Items.Add(di.Name + " - " + di.VolumeLabel);
            }
            if (flashBox.Items.Count > 0)
                flashBox.SelectedItem = flashBox.Items[0];
        }

        private void createDataFileButton_Click(object sender, EventArgs e)
        {
            DisableControlsBeforeCreateFile();

            try
            {
                if (tabControl1.SelectedTab == tabPage2)
                {
                    if (!CheckTabPage2())
                        return;
                }

                if (driveBox.SelectedItem == null)
                {
                    throw new DriveNotFoundException("Не найден диск с ключевой информацией.\nФормирование файла невозможно.\nВозможно Вы:\n\t- не указали диск с ключевой информацией (шаг 1).");
                }

                if (flashBox.SelectedItem == null)
                {
                    throw new DriveNotFoundException("Не найден флеш накопитель.\nФормирование файла невозможно.\nВозможно Вы:\n\t- не указали накопитель (шаг 2).");
                }

                if (String.IsNullOrEmpty(driveBox.Text))
                {
                    throw new DriveNotFoundException("Не найден диск с ключевой информацией.\nФормирование файла невозможно.\nВозможно Вы:\n\t- не указали диск с ключевой информацией (шаг 1).");
                }

                _diskKey = ReadKey.Read(driveBox.Text.Substring(0, 2), ReadKey.DeviceType.CD, ReadKey.DataType.Key);
                _diskTable = ReadKey.Read(driveBox.Text.Substring(0, 2), ReadKey.DeviceType.CD, ReadKey.DataType.Table);

                DateTime beginDate, endDate;
                ReadKey.ReadDates(driveBox.Text.Substring(0, 2), ReadKey.DeviceType.CD, out beginDate, out endDate);

                this.keyDateLabel.Text = string.Format(keyDateLabel.Tag.ToString(), beginDate.ToShortDateString(), endDate.ToShortDateString());

                if (DateTime.Compare(DateTime.Now.Date, beginDate) == -1)
                {
                    throw new Exception("Вы не можете формировать электронные данные,\nт.к. период действия ключа ещё не наступил!");
                }
                if (DateTime.Compare(DateTime.Now.Date, endDate) == 1)
                {
                    throw new Exception("Вы не можете формировать электронные данные,\nт.к. истёк срок действия ключа!");
                }

                DateTime createStart, createEnd;
                
                System.Xml.XmlDocument mapXml, szv3Xml;
                IEnumerable<System.Xml.XmlDocument> szv2Array;
                IEnumerable<IEnumerable<System.Xml.XmlDocument>> szv1Array;
                long[] markedLists = MarkedLists();
                createStart = DateTime.Now;
                Storage.MakeXml(_repYear, _organization, markedLists, _connection, 
                                out mapXml, out szv3Xml, out szv2Array, out szv1Array);
                createEnd = DateTime.Now;
                OrgPropXml orgProp = new OrgPropXml();
                orgProp.orgName = _organization.nameVal;
                orgProp.orgRegnum = _organization.regnumVal;
                orgProp.directorType = _organization.chiefpostVal;
                orgProp.directorFIO = _organization.chieffioVal;
                orgProp.bookkeeperFIO = _organization.bookerfioVal;
                orgProp.operatorName = _operator.nameVal;
                
                if (_container != null)
                    _container.Close();

                _container = Storage.MakeContainer(orgProp.GetXml(), mapXml, szv3Xml, szv2Array, szv1Array,
                                            _diskTable, _diskKey);

                string flashRoot = flashBox.Text.Substring(0,1);
                DirectoryInfo dir = Directory.CreateDirectory(string.Format(
                                                            @"{0}:\\Государственный пенсионный фонд ПМР\{1}.{2}",
                                                            flashRoot,
                                                            _organization.regnumVal,
                                                            _repYear));
                _container.Save(dir.FullName + @"\container.pfs");
                _container.Close();

                TimeSpan createSpan;
                createSpan = new TimeSpan(createEnd.Ticks - createStart.Ticks);
                MainForm.ShowInfoMessage(string.Format("Файл с электронными данными успешно сформирован и готов к предоставлению в Фонд.\nДлительность операции: {0:0.00} секунд(ы)", createSpan.TotalSeconds), "Формирование завершено");

            }
            catch (DriveNotFoundException drvExc)
            {
                MessageBox.Show(drvExc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControlsAfterCreateFile();
            }
        }

        private bool CheckTabPage2()
        {
            bool result = true;

            if (_mergiesCountLists < 0)
            {
                MessageBox.Show("Сводная ведомость (СЗВ-3) не обнаружена!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (_mergiesCountLists < 1)
            {
                MessageBox.Show("Количество пакетов документов в сводной ведомости: " + _mergiesCountLists +
                    "\nФормирование электронных данных невозможно!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (_mergiesCountDocs < 1)
            {
                MessageBox.Show("Количество документов \"СЗВ-1\" в сводной ведомости: " + _mergiesCountDocs +
                    "\nФормирование электронных данных невозможно!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (_checkedCountLists < 1)
            {
                MessageBox.Show("Количество выбранных пакетов документов: " + _checkedCountLists +
                    "\nФормирование электронных данных невозможно!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (_checkedCountDocs < 1)
            {
                MessageBox.Show("Количество выбранных документов \"СЗВ-1\": " + _checkedCountDocs +
                    "\nФормирование электронных данных невозможно!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (_checkedCountLists > _mergiesCountLists)
            {
                MessageBox.Show(
                    "Количество выбранных пакетов документов: " + _checkedCountLists +
                    "\nбольше, чем указано в сводной ведомости: " + _mergiesCountLists, "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }

            if (_checkedCountDocs > _mergiesCountDocs)
            {
                MessageBox.Show(
                    "Количество выбранных документов \"СЗВ-1\": " + _checkedCountDocs +
                    "\nбольше, чем указано в сводной ведомости: " + _mergiesCountDocs, "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }


            if (_checkedCountLists < _mergiesCountLists)
            {
                result = (MessageBox.Show(
                        "Количество выбранных пакетов документов: " + _checkedCountLists +
                        "\nменьше, чем указано в сводной ведомости: " + _mergiesCountLists +
                        ".\n\nПродолжить формирование электронных данных?", "Предупреждение", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation) == DialogResult.Yes);
            }

            if (_checkedCountDocs < _mergiesCountDocs)
            {
                result = (MessageBox.Show(
                        "Количество выбранных документов \"СЗВ-1\": " + _checkedCountDocs +
                        "\nменьше, чем указано в сводной ведомости: " + _mergiesCountDocs +
                        ".\n\nПродолжить формирование электронных данных?", "Предупреждение", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation) == DialogResult.Yes);

            }
            return result;
        }

        private void flashRButton_CheckedChanged(object sender, EventArgs e)
        {
            if (flashRButton.Checked)
            {
                flashBox.Enabled = true;
                groupBox5.Enabled = false;
            }
            else
            {
                flashBox.Enabled = false;
                groupBox5.Enabled = true;
            }
        }

        private void xmlPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog xmlFolderBrowser = new FolderBrowserDialog();
            xmlFolderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            xmlFolderBrowser.ShowNewFolderButton = false;
            if (xmlFolderBrowser.ShowDialog() == DialogResult.OK)
                xmlPathTextBox.Text = xmlFolderBrowser.SelectedPath;
        }

        private void viewDataButton_Click(object sender, EventArgs e)
        {
            if (_diskKey == null || _diskTable == null)
                return;
            string flashRoot = flashBox.Text.Substring(0, 1);
            string filename = string.Format(@"{0}:\\Государственный пенсионный фонд ПМР\{1}.{2}\container.pfs",
                                            flashRoot, _organization.regnumVal, _repYear);
            if (!File.Exists(filename))
            {
                MainForm.ShowInfoMessage("Сначала необходимо сформировать электронный файл для обмена с ЕГФСС", "Внимание");
                return;
            }
            _container = new CompoundFile(filename);
            CFStream mapStream = _container.RootStorage.GetStream("map");
            byte[] mapBytes = Storage.DecryptStream(mapStream, _diskKey, _diskTable);
            CFStorage stylesDir = _container.RootStorage.GetStorage("styles");
            CFStream mapStyleStream = stylesDir.GetStream("map_style");
            byte[] mapStyleBytes = Storage.DecryptStream(mapStyleStream, _diskKey, _diskTable);

            string htmlStr = MapXml.GetHTML(mapBytes, mapStyleBytes);
            WebBrowser reportWB = new WebBrowser();
            reportWB.DocumentText = htmlStr;
            MyPrinter.ShowWebPage(reportWB);
            reportWB.Navigating += new WebBrowserNavigatingEventHandler(reportWB_Navigating);
            //_container.Close();
        }

        void reportWB_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string uri =  e.Url.ToString();
            if (uri != "about:blank")
            {
                e.Cancel = true;
                if (_diskKey != null && _diskTable != null)
                {
                    string html = Storage.GetHTML(_container, uri, _diskKey, _diskTable);
                    if (html == null)
                        return;
                    MyPrinter.ShowWebPage(html);
                }
            }
        }

        private void sendDataButton_Click(object sender, EventArgs e)
        {

        }

        private void checkAllButton_Click(object sender, EventArgs e)
        {
            this.packetsView.EndEdit();
            bool allchecked = true;

            foreach (DataRowView row in _listsBS.Cast<DataRowView>().Where(row => Convert.ToInt32(row[ListsView2.countPens]) <= 0))
            {
                allchecked &= Convert.ToBoolean(row[CHECK]);
            }

            foreach (DataRowView row in _listsBS.Cast<DataRowView>().Where(row => Convert.ToInt32(row[ListsView2.countPens]) <= 0))
            {
                row[CHECK] = !allchecked;
            }

            this.packetsView.Refresh();
        }

        private void packetsView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                {
                    checkAllButton_Click(sender, e);
                }

                if (_listsBS.Current == null)
                    return;
                if (e.RowIndex >= 0 && e.ColumnIndex == 0 &&
                    Convert.ToInt32((_listsBS.Current as DataRowView)[ListsView2.countPens]) > 0)
                {
                    MainForm.ShowInfoFlexMessage(
                        "В текущем пакете содержаться формы разных типов!\nДокументы типа \"Назначение пенсии\" должны находится в отдельном пакете!",
                        "Ошибка выбора пакета");
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void packetsView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    if (_listsBS.Current == null)
                        return;

                    if (Convert.ToInt32((_listsBS.Current as DataRowView)[ListsView2.countPens]) > 0)
                    {
                        MainForm.ShowInfoFlexMessage(
                            "В текущем пакете содержаться формы разных типов!\nДокументы типа \"Назначение пенсии\" должны находится в отдельном пакете!",
                            "Предупреждение");
                    }
                    else
                    {
                        (_listsBS.Current as DataRowView)[CHECK] =
                            !Convert.ToBoolean((_listsBS.Current as DataRowView)[CHECK]);
                        (_listsBS.Current as DataRowView).EndEdit();
                        SumCheckedLists();
                    }
                    (sender as DataGridView).Refresh();
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorFlexMessage(ex.Message, "Непредвиденная ошибка");
            }
        }

        private void packetsView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            (sender as DataGridView).EndEdit();
            (sender as DataGridView).Refresh();

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                SumCheckedLists();
            }
        }

        private void packetsView_Sorted(object sender, EventArgs e)
        {
            DisableCheckBoxInPacketView();
        }

        private void yearBox_ValueChanged(object sender, EventArgs e)
        {
            _repYear = (int)yearBox.Value;
            MainForm.RepYear = _repYear;
            ReloadLists();
            GetCountsFromMergies();
        }

        private void ExchangeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_container != null)
                _container.Close();
        }
        #endregion
    }
}
