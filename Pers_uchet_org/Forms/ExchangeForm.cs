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

        private bool CheckTabPage3()
        {
            throw new NotImplementedException();
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

                if (tabControl1.SelectedTab == tabPage3)
                {
                    if (!CheckTabPage3())
                        return;
                }

                if (driveBox.SelectedItem == null)
                {
                    throw new DriveNotFoundException("Не найден диск с ключевой информацией.\nФормирование файла невозможно.\nВозможно Вы:\n\t- не указали диск с ключевой информацией (шаг 1).");
                }
                //Отключено на время дебага
                //if (flashBox.SelectedItem == null)
                //{
                //    throw new DriveNotFoundException("Не найден флеш накопитель.\nФормирование файла невозможно.\nВозможно Вы:\n\t- не указали накопитель (шаг 2).");
                //    return;
                //}

                //if (String.IsNullOrEmpty(driveBox.Text))
                //{
                //    throw new DriveNotFoundException("Не найден диск с ключевой информацией.\nФормирование файла невозможно.\nВозможно Вы:\n\t- не указали диск с ключевой информацией (шаг 1).");
                //    return;
                //}



                //byte[] arr = ReadKey.HexToBin("60FB8A6AA1EFF871E08187E197F0A209D1F833D47665A0F72B3C7C6F29F40AFB");
                //long l = ReadKey.Date2Julian(DateTime.Parse("16.11.2012"));

                //        if (DateTime.TryParse(endDateStr, out result))
                //        {
                //            if (DateTime.Compare(DateTime.Now.Date, result) == 1)
                //            {
                //                throw new Exception("Вы не можете формировать электронные данные,\nт.к. истёк срок действия ключа!");
                //            }
                //        }
                //        else
                //            throw new Exception("Ошибка чтения ключевого диска.");

                #region Переменные

                string key = "";
                string key1 = "";
                string table = "";
                string table1 = "";
                int shift = 0;
                byte[] keyArr;
                byte[] tableArr;
                byte[] syncArr;

                #endregion


                shift = 2048;
                //Читаем ключ
                key1 = ReadKey.ReadCD(driveBox.Text.Substring(0, 2), shift);
                //Читаем таблицу
                table = ReadKey.ReadCD(driveBox.Text.Substring(0, 2), shift * 2);
                table1 = ReadKey.ReadCD(driveBox.Text.Substring(0, 2), shift * 3);

                #region Тест
                ////Получаем ключ в HEX виде
                //for (int i = shift*2 - 2; i >= shift*2 - 1024 + 30; i -= 32)
                //{
                //    key += key1.Substring(i, 2);
                //}


                //string tempKey = key;

                ////key = key + key + key;
                ////for(int j = tempKey.Length - 2; j >= 0; j-=2)
                ////{
                ////    key += tempKey.Substring(j, 2);
                ////}

                //keyArr = new byte[key.Length/2];
                //keyArr = ReadKey.HexToBin(key);
                //table = table.Substring(0, 1024) + table1.Substring(0, 1024);
                //tableArr = new byte[table.Length/2];
                //tableArr = ReadKey.HexToBin(table);
                ////----------------------
                //string tempData = "0000000000000000";
                //key = "546D203368656C326973652073736E62206167796967747473656865202C3D73";
                //table =
                //    "040A09020D08000E060B010C070F05030E0B040C060D0F0A02030801000705090508010D0A0304020E0F0C070600090B070D0A010008090F0E04060C0B020503060C0701050F0D08040A090E00030B02040B0A000702010D03060805090C0F0E0D0B0401030F0509000A0E070608020C010F0D0005070A040902030E060B080C";
                //string sync = "1111111122222222";

                //keyArr = new byte[key.Length/2];
                //keyArr = ReadKey.HexToBin(key);
                //tableArr = new byte[table.Length/2];
                //tableArr = ReadKey.HexToBin(table);
                //byte[] dataArr = new byte[tempData.Length/2];
                //dataArr = ReadKey.HexToBin(tempData);
                //syncArr = ReadKey.HexToBin(sync);
                ////sync = ReadKey.ArrayToString(syncArr);
                ////syncArr = ReadKey.StringToArray(sync);

                //string tmp = "";
                //tmp = ReadKey.ArrayToString(syncArr);
                ////tmp = ReadKey.ArrayToString(keyArr);
                ////tmp = ReadKey.ArrayToString(tableArr);


                //if (syncArr.Length != 8)
                //    syncArr = new byte[8];
                //Gost28147_89.GostSimple(ref dataArr, keyArr, tableArr, 32, true);
                ////Gost28147_89.GostGama(ref dataArr, ref syncArr, keyArr, tableArr);

                ////Debug.Print("syncArr " + string.Join(" ", syncArr));

                //string resultStr;
                //resultStr = ReadKey.ArrayToString(dataArr);
                //tempData = ReadKey.BinToHex(resultStr);


                ////---Расшифровка
                //dataArr = new byte[tempData.Length/2];
                //dataArr = ReadKey.HexToBin(tempData);
                //sync = "";
                //syncArr = ReadKey.StringToArray(sync);
                //if (syncArr.Length != 8)
                //    syncArr = new byte[8];

                //Gost28147_89.GostGama(ref dataArr, ref syncArr, keyArr, tableArr);
                //resultStr = ReadKey.ArrayToString(dataArr);
                ////---
                #endregion

                //Debug.Print(keyStr);
                long beginDate = Convert.ToInt32("0x" + key1.Substring((2048 * 2) - 1024 + 100, 8), 16);
                long endDate = Convert.ToInt32("0x" + key1.Substring((2048 * 2) - 1024 + 200, 8), 16);
                string beginDateStr = ReadKey.Julian2Date(beginDate);
                string endDateStr = ReadKey.Julian2Date(endDate);
                keyDateLabel.Text = string.Format(keyDateLabel.Tag.ToString(), beginDateStr, endDateStr);

                

                DateTime result;
                if (DateTime.TryParse(beginDateStr, out result))
                {
                    if (DateTime.Compare(DateTime.Now.Date, result) == -1)
                    {
                        throw new Exception("Вы не можете формировать электронные данные,\nт.к. период действия ключа ещё не наступил!");
                    }
                }
                else
                    throw new Exception("Ошибка чтения ключевого диска.\nВозможно:\n\t- не указан диск с ключевой информацией (шаг 1);\n\t- указанный диск не является ключевым;\n\t- диск поврежден или не может быть прочитан.");

                if (DateTime.TryParse(endDateStr, out result))
                {
                    if (DateTime.Compare(DateTime.Now.Date, result) == 1)
                    {
                        throw new Exception("Вы не можете формировать электронные данные,\nт.к. истёк срок действия ключа!");
                    }
                }
                else
                    throw new Exception("Ошибка чтения ключевого диска.\nВозможно:\n\t- не указан диск с ключевой информацией (шаг 1);\n\t- указанный диск не является ключевым;\n\t- диск поврежден или не может быть прочитан.");



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
            //CompoundFile cf = new CompoundFile(@"C:\Temp\Государственный пенсионный фонд ПМР\Т001230.2005\edatacon.pfs");
            //IList<CFItem> list = cf.GetAllNamedEntries(cf.RootStorage.Name);

            //CFStorage rootStore = cf.RootStorage;
            //CFStream stream = rootStore.GetStream(rootStore.Name);
            //string data = Encoding.GetEncoding(1251).GetString(stream.GetData());

            //string data = "123321";
            //string synchro = "C0E1205FF4F0CE01";
            string table = "E4EAE9E2EDE8E0EEE6EBE1ECE7EFE5E3B4BAB9B2BDB8B0BEB6BBB1BCB7BFB5B3444A49424D48404E464B414C474F4543C4CAC9C2CDC8C0CEC6CBC1CCC7CFC5C3646A69626D68606E666B616C676F6563D4DAD9D2DDD8D0DED6DBD1DCD7DFD5D3F4FAF9F2FDF8F0FEF6FBF1FCF7FFF5F3A4AAA9A2ADA8A0AEA6ABA1ACA7AFA5A3242A29222D28202E262B212C272F2523343A39323D38303E363B313C373F3533848A89828D88808E868B818C878F8583141A19121D18101E161B111C171F1513040A09020D08000E060B010C070F0503747A79727D78707E767B717C777F7573545A59525D58505E565B515C575F5553949A99929D98909E969B919C979F95937578717D7A7374727E7F7C777670797BD5D8D1DDDAD3D4D2DEDFDCD7D6D0D9DBA5A8A1ADAAA3A4A2AEAFACA7A6A0A9AB1518111D1A1314121E1F1C171610191B0508010D0A0304020E0F0C070600090B8588818D8A8384828E8F8C878680898B9598919D9A9394929E9F9C979690999BF5F8F1FDFAF3F4F2FEFFFCF7F6F0F9FBE5E8E1EDEAE3E4E2EEEFECE7E6E0E9EB4548414D4A4344424E4F4C474640494B6568616D6A6364626E6F6C676660696BC5C8C1CDCAC3C4C2CECFCCC7C6C0C9CBB5B8B1BDBAB3B4B2BEBFBCB7B6B0B9BB2528212D2A2324222E2F2C272620292B5558515D5A5354525E5F5C575650595B3538313D3A3334323E3F3C373630393B464C4741454F4D48444A494E40434B42B6BCB7B1B5BFBDB8B4BAB9BEB0B3BBB2A6ACA7A1A5AFADA8A4AAA9AEA0A3ABA2060C0701050F0D08040A090E00030B02767C7771757F7D78747A797E70737B72262C2721252F2D28242A292E20232B22161C1711151F1D18141A191E10131B12D6DCD7D1D5DFDDD8D4DAD9DED0D3DBD2363C3731353F3D38343A393E30333B32666C6761656F6D68646A696E60636B62868C8781858F8D88848A898E80838B82565C5751555F5D58545A595E50535B52969C9791959F9D98949A999E90939B92C6CCC7C1C5CFCDC8C4CAC9CEC0C3CBC2F6FCF7F1F5FFFDF8F4FAF9FEF0F3FBF2E6ECE7E1E5EFEDE8E4EAE9EEE0E3EBE21D1B1411131F1519101A1E171618121CFDFBF4F1F3FFF5F9F0FAFEF7F6F8F2FCDDDBD4D1D3DFD5D9D0DADED7D6D8D2DC0D0B0401030F0509000A0E070608020C5D5B5451535F5559505A5E575658525C7D7B7471737F7579707A7E777678727CADABA4A1A3AFA5A9A0AAAEA7A6A8A2AC4D4B4441434F4549404A4E474648424C9D9B9491939F9599909A9E979698929C2D2B2421232F2529202A2E272628222C3D3B3431333F3539303A3E373638323CEDEBE4E1E3EFE5E9E0EAEEE7E6E8E2EC6D6B6461636F6569606A6E676668626CBDBBB4B1B3BFB5B9B0BABEB7B6B8B2BC8D8B8481838F8589808A8E878688828CCDCBC4C1C3CFC5C9C0CACEC7C6C8C2CC";
            string key = "D7015B32B81E11FD409A867CD7AB89899628A3EC4488FE0AFEEE4486746BD987";
            key = key + key + key + "87D96B748644EEFE0AFE8844ECA328968989ABD77C869A40FD111EB8325B01D7";
            string path = @"d:\Bins Б000044.2012\"; //map.bin mapstyle.bin svodstyle.bin   szvopisstyle.bin   szvstyle.bin

            int i;
            byte[] bData = File.ReadAllBytes(path + "map.bin");
            char[] buf = { '0', '0' };
            byte[] bKey, bTable, bSynchro;
            bSynchro = new byte[8];
            Array.Copy(bData, bData.Length - 8, bSynchro, 0, 8);
            bData = bData.Take(bData.Length - 8).ToArray();

            bKey = new byte[key.Length / 2];
            for (i = 0; i < key.Length; i += 2)
            {
                buf[0] = key[i];
                buf[1] = key[i + 1];
                int tmp = int.Parse(new string(buf), System.Globalization.NumberStyles.HexNumber);
                bKey[i / 2] = Convert.ToByte(tmp);
            }
            bTable = new byte[table.Length / 2];
            for (i = 0; i < table.Length; i += 2)
            {
                buf[0] = table[i];
                buf[1] = table[i + 1];
                int tmp = int.Parse(new string(buf), System.Globalization.NumberStyles.HexNumber);
                bTable[i / 2] = Convert.ToByte(tmp);
            }

            byte[] res = Mathdll.GostGamma(bData, bKey, bTable, bSynchro);
            File.WriteAllText(path + "map_new.xml", Encoding.GetEncoding(1251).GetString(res), Encoding.GetEncoding(1251));
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
            SumCheckedLists();
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
        #endregion


    }
}
