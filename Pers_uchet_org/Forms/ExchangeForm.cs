using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using OpenMcdf;
using Pers_uchet_org.Properties;

namespace Pers_uchet_org.Forms
{
    public partial class ExchangeForm : Form
    {
        #region Поля

        private Operator _operator;
        private Org _organization;
        private string _connection;
        // таблица пакетов
        private DataTable _listsTable;
        // биндинг сорс для таблиц
        private BindingSource _listsBS;
        // адаптер для чтения данных из БД
        private SQLiteDataAdapter _listsAdapter;
        // названия добавочного виртуального столбца
        private const string Check = "check";
        // переменная содержит текущий используемый год
        private int _repYear;

        // количество выбранных документов
        private long _checkedCountDocs;
        // количество выбранных пакетов
        private long _checkedCountLists;

        // количество документов по сводной
        private long _mergiesCountDocs;
        // количество пакетов по сводной
        private long _mergiesCountLists;
        // label с сообщением о ненайденной сводной

        private CompoundFile _container;
        private byte[] _diskKey;
        private byte[] _diskTable;

        private BackgroundWorker _worker;

        #endregion

        #region Конструктор и инициализатор

        private ExchangeForm()
        {
            InitializeComponent();
            szv3WarningLabel = new Label();
        }

        public ExchangeForm(Operator _operator, Org org, string mainConnection)
            : this()
        {
            this._operator = _operator;
            _organization = org;
            _connection = mainConnection;
        }

        private void ExchangeForm_Load(object sender, EventArgs e)
        {
            Text += " - " + _organization.regnumVal;

            yearBox.Value = MainForm.RepYear;
            _repYear = (int)yearBox.Value;
            ExchangeTabControl.SelectedTab = tabPageDB;
            ExchangeTabControl.TabPages.Remove(tabPage1);

            // инициализация таблиц
            _listsTable = ListsView2.CreateTable();
            _listsTable.Columns.Add(Check, typeof(bool));
            _listsTable.Columns[Check].DefaultValue = false;

            // инициализация биндинг сорса к таблице пакетов
            _listsBS = new BindingSource();
            //_listsBS.CurrentChanged += new EventHandler(_listsBS_CurrentChanged);
            //_listsBS.ListChanged += new ListChangedEventHandler(_listsBS_ListChanged);
            _listsBS.DataSource = _listsTable;

            // присвоение источника dataGrid
            packetsView.AutoGenerateColumns = false;
            packetsView.Columns["checkColumn"].DataPropertyName = Check;
            packetsView.Columns["packetNumColumn"].DataPropertyName = ListsView2.id;
            packetsView.Columns["docCountColumn"].DataPropertyName = ListsView2.countDocs;
            packetsView.DataSource = _listsBS;

            ReloadLists();
            GetCountsFromMergies();

            cdRButton.Checked = true;
            cdOrKeyfileRButton_CheckedChanged(cdRButton, e);

            driveBox_Click(sender, e);
            flashBox_Click(sender, e);

            _worker = new BackgroundWorker();
            _worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            _worker.WorkerSupportsCancellation = true;
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        #endregion

        #region Методы - свои

        private void DisableControlsBeforeCreateFile()
        {
            yearGroupBox.Enabled = false;
            ExchangeTabControl.Enabled = false;
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
            ExchangeTabControl.Enabled = true;
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
                if ((bool)row[Check])
                    lists.Add((long)row[ListsView2.id]);
            }
            return lists.ToArray();
        }

        private void SumCheckedLists()
        {
            packetsView.EndEdit();
            _checkedCountLists = 0;
            _checkedCountDocs = 0;
            foreach (DataRowView row in _listsBS.Cast<DataRowView>().Where(row => Convert.ToBoolean(row[Check])))
            {
                _checkedCountLists++;
                _checkedCountDocs += Convert.ToInt32(row[ListsView2.countDocs]);
            }
            checkedPacketCountBox.Text = _checkedCountLists.ToString();
            chekedDocCountBox.Text = _checkedCountDocs.ToString();
        }

        private bool CheckTabPageXML(out string errMessage)
        {
            bool res = true;
            errMessage = "";
            string path = xmlPathTextBox.Text;
            if (!Directory.Exists(path))
            {
                errMessage += "\nНе найден указанный каталог.";
                return false;
            }

            OrgPropXml orgProperty = GetOrgProperties();

            string rootDirStr = string.Format(@"{0}\{1}\{2}", path, orgProperty.orgRegnum, orgProperty.repYear);
            // если в корневой директории (папке) нет подпапки с Рег номером организации, 
            // в которой в свою очередь нет папки с отчетным годом (RepYear),
            // то процесс импорта невозможен
            if (!Directory.Exists(rootDirStr))
            {
                errMessage += string.Format("\nВ корневом каталоге не найдены необходимые директории: '{0}\\{1}'",
                    orgProperty.orgRegnum, orgProperty.repYear);
                return false;
            }
            string szv3Filename = rootDirStr + @"\сводная.xml";
            // если нет файла сводной ведомости (СЗВ-3) импорт не может быть закончен
            if (!File.Exists(szv3Filename))
            {
                errMessage += "\nНе найден файл сводной ведомости СЗВ-3 (сводная.xml).";
                res = false;
            }
            DirectoryInfo rootDir = new DirectoryInfo(rootDirStr);
            DirectoryInfo[] packetDirs = rootDir.GetDirectories("Пакет_Z???");
            FileInfo[] opisFiles = rootDir.GetFiles("z_опись_???.xml");
            FileInfo[][] szv1FilesArr = new FileInfo[packetDirs.Count()][];
            // если количество директорий (папок) с документами СЗВ-1 
            // отличается от количества файлов с описями (документы СЗВ-2)
            // процесс импорта следует прекратить
            if (!packetDirs.Any())
            {
                errMessage +=
                    string.Format("\nКоличество директорий (папок) ({0}) с документами СЗВ-1 не может быть меньше 1.",
                        packetDirs.Count());
                res = false;
            }

            if (!opisFiles.Any())
            {
                errMessage += string.Format("\nКоличество описей СЗВ-2 ({0}) не может быть меньше 1.",
                    opisFiles.Count());
                res = false;
            }
            if (packetDirs.Count() != opisFiles.Count())
            {
                errMessage +=
                    string.Format(
                        "\nКоличество директорий (папок) ({0}) с документами СЗВ-1 отличается от количества файлов с описями (документы СЗВ-2) ({1}).",
                        packetDirs.Count(), opisFiles.Count());
                res = false;
            }

            string[] dirNumArr = new string[packetDirs.Count()];
            string[] fileNumArr = new string[opisFiles.Count()];
            int i;
            string tmpStr;
            for (i = 0; i < dirNumArr.Length; i++)
            {
                tmpStr = packetDirs[i].Name;
                dirNumArr[i] = tmpStr.Substring(tmpStr.Length - 3, 3);
            }
            for (i = 0; i < fileNumArr.Length; i++)
            {
                tmpStr = opisFiles[i].Name;
                fileNumArr[i] = tmpStr.Substring(tmpStr.Length - 7, 3);
            }
            int count = 0;

            if (dirNumArr.Length == fileNumArr.Length)
            {
                for (i = 0; i < dirNumArr.Length; i++)
                {
                    if (dirNumArr.Contains(fileNumArr[i]))
                    {
                        count++;
                    }
                }
                // не всем файлам описей (СЗВ-2) найдены 
                // соответствующие директории (папки) пакетов
                if (count < dirNumArr.Length)
                {
                    errMessage += "\nНе всем файлам описей СЗВ-2 найдены соответствующие директории (папки) пакетов";
                    res = false;
                }
            }

            count = 0;
            for (i = 0; i < dirNumArr.Length; i++)
            {
                szv1FilesArr[i] = packetDirs[i].GetFiles(string.Format("z_документ_L{0}_D???.xml", dirNumArr[i]));
                // если в директории (папке) есть файлы удовлетворяющие фильиру, 
                // то увеличиваем счетчик
                if (szv1FilesArr[i].Length > 0)
                {
                    count++;
                }
            }
            // если есть директории пакетов без файлов документов СЗВ-1,
            // то импорт невозможен
            if (count < dirNumArr.Length)
            {
                errMessage += "\nЕсть директории пакетов без файлов документов СЗВ-1";
                res = false;
            }

            if (errMessage.Length == 0)
            {
                errMessage = null;
            }
            //
            return res;
        }

        private bool CheckTabPageDB(out string errMessage)
        {
            bool result = true;
            errMessage = "";
            if (_mergiesCountLists < 0)
            {
                errMessage += "\nСводная ведомость СЗВ-3 не обнаружена!";
                result = false;
            }
            else
            {
                if (_mergiesCountLists < 1)
                {
                    errMessage += string.Format("\nКоличество пакетов документов в сводной ведомости: {0}",
                        _mergiesCountLists);
                    result = false;
                }

                if (_mergiesCountDocs < 1)
                {
                    errMessage += string.Format("\nКоличество документов СЗВ-1 в сводной ведомости: {0}",
                        _mergiesCountDocs);
                    result = false;
                }
            }

            if (_checkedCountLists < 1)
            {
                errMessage += string.Format("\nКоличество выбранных пакетов документов: {0}.", _checkedCountLists);
                result = false;
            }

            if (_checkedCountDocs < 1)
            {
                errMessage += string.Format("\nКоличество выбранных документов СЗВ-1: {0}.", _checkedCountDocs);
                result = false;
            }

            if (_mergiesCountLists > 0)
            {
                if (_checkedCountLists > _mergiesCountLists)
                {
                    errMessage +=
                        string.Format(
                            "\nКоличество выбранных пакетов документов ({0}) больше, чем указано в сводной ведомости: {1}.",
                            _checkedCountLists, _mergiesCountLists);
                    result = false;
                }

                if (_checkedCountDocs > _mergiesCountDocs)
                {
                    errMessage +=
                        string.Format(
                            "\nКоличество выбранных документов СЗВ-1 ({0}) больше, чем указано в сводной ведомости: {1}.",
                            _checkedCountDocs, _mergiesCountDocs);
                    result = false;
                }

                if (_checkedCountLists < _mergiesCountLists && result)
                {
                    errMessage +=
                        string.Format(
                            "\nКоличество выбранных пакетов документов ({0}) меньше, чем указано в сводной ведомости: {1}.",
                            _checkedCountLists, _mergiesCountLists);
                    result = true;
                }

                if (_checkedCountDocs < _mergiesCountDocs && result)
                {
                    errMessage +=
                        string.Format(
                            "\nКоличество выбранных документов СЗВ-1 ({0}) меньше, чем указано в сводной ведомости: {1}."
                            , _checkedCountDocs, _mergiesCountDocs);
                    result = true;
                }
            }
            if (errMessage.Length == 0)
            {
                errMessage = null;
            }
            //
            return result;
        }

        private bool CheckDrives(out string errMessage)
        {
            bool res = true;
            errMessage = "";

            if (cdRButton.Checked)
            {
                if (driveBox.SelectedItem == null || String.IsNullOrEmpty(driveBox.Text))
                {
                    errMessage +=
                        "\nНе найден диск с ключевой информацией.\nВозможно Вы:\n\t- не указали диск с ключевой информацией (шаг 1).";
                    res = false;
                }
            }
            else
            {
                if (!File.Exists(keyfileTextBox.Text))
                {
                    errMessage +=
                        "\nНе найден файл с ключевой информацией.\nВозможно Вы:\n\t - неверно указали файл с ключевой информацией";
                    res = false;
                }
            }

            if (flashRButton.Checked && flashBox.SelectedItem == null)
            {
                errMessage += "\nНе найден флеш накопитель.\nВозможно Вы:\n\t- не указали накопитель (шаг 2).";
                res = false;
            }

            if (errMessage.Length == 0)
            {
                errMessage = null;
            }
            //
            return res;
        }

        private bool ReadDisk(out string errMessage)
        {
            bool res = true;
            errMessage = "";
            DateTime beginDate, endDate;

            if (cdRButton.Checked)
            {
                _diskKey = ReadKey.Read(driveBox.Text.Substring(0, 2), ReadKey.DeviceType.CD, ReadKey.DataType.Key);
                _diskTable = ReadKey.Read(driveBox.Text.Substring(0, 2), ReadKey.DeviceType.CD, ReadKey.DataType.Table);
                ReadKey.ReadDates(driveBox.Text.Substring(0, 2), ReadKey.DeviceType.CD, out beginDate, out endDate);
            }
            else
            {
                string isoPath = keyfileTextBox.Text;

                _diskKey = ReadKey.Read(isoPath, ReadKey.DeviceType.CDImage, ReadKey.DataType.Key);
                _diskTable = ReadKey.Read(isoPath, ReadKey.DeviceType.CDImage, ReadKey.DataType.Table);
                ReadKey.ReadDates(isoPath, ReadKey.DeviceType.CDImage, out beginDate, out endDate);
            }

            keyDateLabel.Text = string.Format(keyDateLabel.Tag.ToString(), beginDate.ToShortDateString(),
                endDate.ToShortDateString());

            if (DateTime.Compare(DateTime.Now.Date, beginDate) == -1)
            {
                errMessage += "Период действия ключа ещё не наступил.";
                res = false;
            }
            if (DateTime.Compare(DateTime.Now.Date, endDate) == 1)
            {
                errMessage += "Истёк срок действия ключа.";
                res = false;
            }

            if (errMessage.Length == 0)
            {
                errMessage = null;
            }
            //
            return res;
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
                        tabPageDB.Controls.Remove(szv3WarningLabel);
                    }
                    else
                    {
                        _mergiesCountLists = -1;
                        _mergiesCountDocs = -1;

                        szvGroupBox.Visible = false;
                        // 
                        // szv3WarningLabel
                        // 
                        szv3WarningLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular,
                            GraphicsUnit.Point, ((byte)(204)));
                        szv3WarningLabel.ForeColor = Color.Red;
                        szv3WarningLabel.Location = new Point(6, 342);
                        szv3WarningLabel.Name = "szv3WarningLabel";
                        szv3WarningLabel.Size = new Size(286, 53);
                        szv3WarningLabel.TabIndex = 12;
                        szv3WarningLabel.Text =@"Сводная ведомость СЗВ-3 не обнаружена!
Формирование электронных данных невозможно!";
                        szv3WarningLabel.TextAlign = ContentAlignment.MiddleCenter;
                        tabPageDB.Controls.Add(szv3WarningLabel);
                    }
                }
            }

            packetCountBox.Text = _mergiesCountLists.ToString();
            docCountBox.Text = _mergiesCountDocs.ToString();
        }

        private OrgPropXml GetOrgProperties()
        {
            OrgPropXml orgProp = new OrgPropXml();
            orgProp.orgName = _organization.nameVal;
            orgProp.orgRegnum = _organization.regnumVal;
            orgProp.directorType = _organization.chiefpostVal;
            orgProp.directorFIO = _organization.chieffioVal;
            orgProp.bookkeeperFIO = _organization.bookerfioVal;
            orgProp.operatorName = _operator.nameVal;
            orgProp.repYear = ((int)yearBox.Value).ToString();
            orgProp.performer = _operator.nameVal;
            orgProp.programName = "Персонифицированный учет (для организаций)";
            orgProp.version = "3";
            orgProp.programVersion = "4.0";
            orgProp.date = DateTime.Now;
            //
            return orgProp;
        }

        private bool ValidateData()
        {
            string errMessage;
            bool isError;

            if (ExchangeTabControl.SelectedTab == tabPageDB)
            {
                isError = !CheckTabPageDB(out errMessage);
                if (isError)
                {
                    errMessage += "\n\nДальнейшее выполнение операции формирования невозможно!";
                    MainForm.ShowWarningMessage(errMessage, "Внимание");
                    return false;
                }
                if (!string.IsNullOrEmpty(errMessage))
                {
                    errMessage += "\n\nПродолжить операцию формирования?";
                    DialogResult dRes = MainForm.ShowQuestionMessage(errMessage, "Внимание");
                    if (dRes != DialogResult.Yes)
                    {
                        return false;
                    }
                }
            }

            if (ExchangeTabControl.SelectedTab == tabPageXML)
            {
                isError = !CheckTabPageXML(out errMessage);
                if (isError)
                {
                    errMessage += "\n\nДальнейшее выполнение операции формирования невозможно!";
                    MainForm.ShowWarningMessage(errMessage, "Внимание");
                    return false;
                }
                if (!string.IsNullOrEmpty(errMessage))
                {
                    errMessage += "\n\nПродолжить операцию формирования?";
                    DialogResult dRes = MainForm.ShowQuestionMessage(errMessage, "Внимание");
                    if (dRes != DialogResult.Yes)
                    {
                        return false;
                    }
                }
            }

            isError = !CheckDrives(out errMessage);
            if (isError)
            {
                errMessage += "\n\nДальнейшее выполнение операции формирования невозможно!";
                MainForm.ShowWarningMessage(errMessage, "Внимание");
                return false;
            }
            if (!string.IsNullOrEmpty(errMessage))
            {
                errMessage += "\n\nПродолжить операцию формирования?";
                DialogResult dRes = MainForm.ShowQuestionMessage(errMessage, "Внимание");
                if (dRes != DialogResult.Yes)
                {
                    return false;
                }
            }

            isError = !ReadDisk(out errMessage);
            if (isError)
            {
                errMessage += "\n\nДальнейшее выполнение операции формирования невозможно!";
                MainForm.ShowWarningMessage(errMessage, "Внимание");
            }
            else if (!string.IsNullOrEmpty(errMessage))
            {
                errMessage += "\n\nПродолжить операцию формирования?";
                DialogResult dRes = MainForm.ShowQuestionMessage(errMessage, "Внимание");
                if (dRes != DialogResult.Yes)
                {
                    return false;
                }
            }
            //
            return true;
        }

        private int MakeContainer(XmlDocument mapXml, XmlDocument szv3Xml,
            IEnumerable<XmlDocument> szv2Array,
            IEnumerable<IEnumerable<XmlDocument>> szv1Array)
        {
            if (_diskKey == null || _diskTable == null)
            {
                return -1;
            }
            if (_container != null)
            {
                _container.Close();
            }

            DirectoryInfo dir = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Settings.Default.TempFolder));
            if (flashRButton.Checked)
            {
                string flashRoot = GetFlashRoot();
                dir = Directory.CreateDirectory(string.Format(
                    @"{0}:\\Государственный пенсионный фонд ПМР\{1}.{2}",
                    flashRoot, _organization.regnumVal, _repYear));
            }
            else if (internetRButton.Checked)
            {
                dir = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Settings.Default.TempFolder));
            }

            string containerFilename = Path.Combine(dir.FullName, "edatacon.pfs");
            string mdcFilename = Path.Combine(dir.FullName, "mdc");

            OrgPropXml orgProp = GetOrgProperties();
            _container = Storage.MakeContainer(mapXml, szv3Xml, szv2Array, szv1Array,
                _diskKey, _diskTable);
            _container.Save(containerFilename);
            _container.Close();

            CfProperties.AddProperty(containerFilename, orgProp);

            byte[] hash = Mathdll.GostHash(containerFilename, _diskTable);
            File.WriteAllBytes(mdcFilename, hash);
            return 0;
        }

        public string GetFlashRoot()
        {
            if (flashBox.InvokeRequired)
            {
                return (string)flashBox.Invoke(new Func<String>(GetFlashRoot));
            }
            return flashBox.Text.Substring(0, 1);
        }

        #endregion

        #region Методы - обработчики событий

        private void driveBox_Click(object sender, EventArgs e)
        {
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            driveBox.Items.Clear();
            foreach (DriveInfo di in driveInfo)
            {
                if ((di.DriveType == DriveType.CDRom || (di.DriveType == DriveType.Removable && di.Name == "A:\\")) &&
                    di.IsReady)
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


        public XmlDocument mapXml, szv3Xml;
        public IEnumerable<XmlDocument> szv2Array;
        public IEnumerable<IEnumerable<XmlDocument>> szv1Array;

        private void createDataFileButton_Click(object sender, EventArgs e)
        {
            DisableControlsBeforeCreateFile();
            try
            {
                #region Панель отображения прогресса

                Panel panelProgress = new Panel();
                Label labelProgress = new Label();
                ProgressBar progressBar1 = new ProgressBar();
                panelProgress.SuspendLayout();

                // 
                // panelProgress
                // 
                panelProgress.BorderStyle = BorderStyle.FixedSingle;
                panelProgress.Controls.Add(labelProgress);
                panelProgress.Controls.Add(progressBar1);
                panelProgress.Location = new Point(170, 170);
                panelProgress.Name = "panelProgress";
                panelProgress.Size = new Size(409, 157);
                // 
                // progressBar1
                // 
                progressBar1.Location = new Point(31, 74);
                progressBar1.Name = "progressBar1";
                progressBar1.Size = new Size(339, 23);
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.TabIndex = 0;
                // 
                // labelProgress
                // 
                labelProgress.AutoSize = true;
                labelProgress.Location = new Point(111, 38);
                labelProgress.Name = "labelProgress";
                labelProgress.Size = new Size(174, 13);
                labelProgress.TabIndex = 1;
                labelProgress.Text = "Ожидайте завершения операции";

                Controls.Add(panelProgress);
                panelProgress.ResumeLayout(false);
                panelProgress.PerformLayout();
                panelProgress.BringToFront();

                #endregion

                bool isCorrect = ValidateData();
                if (!isCorrect)
                {
                    return;
                }

                mapXml = null;
                szv3Xml = null;
                szv2Array = null;
                szv1Array = null;


                if (ExchangeTabControl.SelectedTab == tabPageDB)
                {
                    _worker.RunWorkerAsync(1);
                }
                else if (ExchangeTabControl.SelectedTab == tabPageXML)
                {
                    _worker.RunWorkerAsync(2);
                }

                while (_worker.IsBusy)
                {
                    Application.DoEvents();
                }
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
                if (_container != null)
                {
                    try
                    {
                        _container.Close();
                    }
                    finally
                    {
                    }
                }
                Controls.RemoveByKey("panelProgress");
                EnableControlsAfterCreateFile();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            mapXml = null;
            szv3Xml = null;
            szv2Array = null;
            szv1Array = null;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            if (Convert.ToInt32(e.Argument) == 1)
            {
                long[] markedLists = MarkedLists();

                Storage.MakeXml(_repYear, _organization, markedLists, _connection,
                    out mapXml, out szv3Xml, out szv2Array, out szv1Array);
            }
            else if (Convert.ToInt32(e.Argument) == 2)
            {
                bool isCorrect;
                DirectoryInfo dir =
                    Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Settings.Default.TempFolder));
                using (StreamWriter writer = new StreamWriter(Path.Combine(dir.FullName, "Ошибки XML.txt"), false))
                {
                    writer.AutoFlush = true;
                    OrgPropXml orgProperties = GetOrgProperties();
                    isCorrect = Storage.ImportXml(xmlPathTextBox.Text, orgProperties,
                        out szv3Xml, out szv2Array, out szv1Array, writer);
                }
                if (!isCorrect)
                {
                    MainForm.ShowErrorMessage(
                        "Импорт xml файлов прошел некорректно.\nФормирование электронного контейнера невозможно!");
                    Process.Start(Path.Combine(dir.FullName, "Ошибки XML.txt"));
                    return;
                }
                mapXml = MapXml.GetXml(szv2Array, szv1Array);
            }

            if (mapXml != null && szv3Xml != null && szv2Array != null && szv1Array != null)
            {
                MakeContainer(mapXml, szv3Xml, szv2Array, szv1Array);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                MainForm.ShowInfoMessage(
                    string.Format(
                        "Файл с электронными данными успешно сформирован и готов к предоставлению в Фонд.\nДлительность операции: {0} ",
                        elapsedTime), "Формирование завершено");
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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
            string errMessage;
            bool checkRes = CheckDrives(out errMessage);

            if (!checkRes)
            {
                MainForm.ShowErrorFlexMessage(errMessage, "Внимание");
                return;
            }

            string filename = "";

            if (flashRButton.Checked)
            {
                string flashRoot = flashBox.Text.Substring(0, 1);
                filename = string.Format(@"{0}:\\Государственный пенсионный фонд ПМР\{1}.{2}\edatacon.pfs",
                    flashRoot, _organization.regnumVal, _repYear);
            }
            else if (internetRButton.Checked)
            {
                filename = Path.Combine(Path.GetTempPath(), Settings.Default.TempFolder);
                filename = Path.Combine(filename, "edatacon.pfs");
            }

            try
            {
                if (!ReadDisk(out errMessage))
                {
                    MainForm.ShowWarningMessage(errMessage, "Внимание");
                }
            }
            catch
            {
                if (cdRButton.Checked)
                {
                    errMessage = "Не удалось считать ключевые данные с диска!\nВозможно Вы вставили неверный диск.";
                }
                else
                {
                    errMessage = "Не удалось считать ключевые данные с файла!\nВозможно Вы вставили неверный файл.";
                }
                MainForm.ShowWarningMessage(errMessage, "Внимание");
                return;
            }


            if (!File.Exists(filename))
            {
                MainForm.ShowInfoMessage("Сначала необходимо сформировать электронный файл для обмена с ЕГФСС",
                    "Внимание");
                return;
            }

            if (_container != null)
            {
                _container.Close();
            }
            _container = new CompoundFile(filename);
            CFStream mapStream = _container.RootStorage.GetStream("map");
            byte[] mapBytes = Storage.DecryptStream(mapStream, _diskKey, _diskTable);
            //string mapStr = Encoding.GetEncoding(1251).GetString(mapBytes);
            CFStorage stylesDir = _container.RootStorage.GetStorage("styles");
            CFStream mapStyleStream = stylesDir.GetStream("map_style");
            byte[] mapStyleBytes = Storage.DecryptStream(mapStyleStream, _diskKey, _diskTable);
            //string styleStr = Encoding.GetEncoding(1251).GetString(mapStyleBytes);
            _container.Close();

            OrgPropXml props = CfProperties.ReadProperty(filename);
            string propHtml = props.GetHTML();

            string htmlStr;
            try
            {
                htmlStr = MapXml.GetHTML(mapBytes, mapStyleBytes);
            }
            catch (XsltException ex)
            {
                #region Текст сообщения всплывающего сообщения

                MainForm.ShowWarningFlexMessage(
                    "Электронный контейнер был сформирован программой версси 3.2.06 или ниже.\nБудет использован файл стиля из текущей версии программы.\n" +
                    ex.Message, "Внимание");

                #endregion

                htmlStr = MapXml.GetHTML(mapBytes);
            }
            catch (Exception ex)
            {
                MainForm.ShowWarningFlexMessage(ex.Message, "Необработанная ошибка");
                htmlStr = MapXml.GetHTML(mapBytes);
            }
            WebBrowser reportWb = new WebBrowser();
            htmlStr = htmlStr.Replace("<DIV class=\"insert_here\" />", propHtml);
            reportWb.DocumentText = htmlStr;
            MyPrinter.ShowWebPage(reportWb);
            reportWb.Navigating += new WebBrowserNavigatingEventHandler(reportWB_Navigating);
        }

        private void reportWB_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string uri = e.Url.ToString();
            if (uri != "about:blank")
            {
                e.Cancel = true;
                if (_diskKey != null && _diskTable != null)
                {
                    if (_container == null || _container.RootStorage == null)
                    {
                        string filename = "";
                        if (flashRButton.Checked)
                        {
                            string flashRoot = flashBox.Text.Substring(0, 1);
                            filename = string.Format(@"{0}:\\Государственный пенсионный фонд ПМР\{1}.{2}\edatacon.pfs",
                                flashRoot, _organization.regnumVal, _repYear);
                        }
                        else if (internetRButton.Checked)
                        {
                            filename = Path.Combine(Path.GetTempPath(), Settings.Default.TempFolder);
                            filename = Path.Combine(filename, "edatacon.pfs");
                        }

                        _container = new CompoundFile(filename);
                    }
                    string html = Storage.GetHTML(_container, uri, _diskKey, _diskTable);
                    _container.Close();
                    _container = null;
                    if (html == null)
                        return;
                    MyPrinter.ShowWebPage(html, true);
                }
            }
        }

        private void sendDataButton_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Path.GetTempPath(), Settings.Default.TempFolder);
            InvokerForm invokerForm = new InvokerForm(_organization, _repYear, path);
            invokerForm.ShowDialog();
        }

        private void checkAllButton_Click(object sender, EventArgs e)
        {
            packetsView.EndEdit();
            bool allchecked = true;

            foreach (
                DataRowView row in
                    _listsBS.Cast<DataRowView>().Where(row => Convert.ToInt32(row[ListsView2.countPens]) <= 0))
            {
                allchecked &= Convert.ToBoolean(row[Check]);
            }

            foreach (
                DataRowView row in
                    _listsBS.Cast<DataRowView>().Where(row => Convert.ToInt32(row[ListsView2.countPens]) <= 0))
            {
                row[Check] = !allchecked;
            }

            packetsView.Refresh();
            SumCheckedLists();
        }

        private void cdOrKeyfileRButton_CheckedChanged(object sender, EventArgs e)
        {
            bool cdChecked = cdRButton.Checked;

            label1.Enabled = cdChecked;
            driveBox.Enabled = cdChecked;
            keyfileTextBox.Enabled = !cdChecked;
            keyfileButton.Enabled = !cdChecked;
        }

        private void keyfileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Образ ключа (*.iso)|*.iso|Все файлы (*.*)|*.*";
            openDialog.FilterIndex = 0;
            DialogResult dRes = openDialog.ShowDialog(this);
            if (dRes == DialogResult.OK)
            {
                keyfileTextBox.Text = openDialog.FileName;
            }
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
                        (_listsBS.Current as DataRowView)[Check] =
                            !Convert.ToBoolean((_listsBS.Current as DataRowView)[Check]);
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

        private void keyDateLabel_DoubleClick(object sender, EventArgs e)
        {
            string result;
            ReadDisk(out result);
        }

        #endregion
    }
}
