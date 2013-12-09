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

namespace Pers_uchet_org
{
    public partial class ExchangeForm : Form
    {
        #region Поля

        #endregion

        #region Конструктор и инициализатор
        public ExchangeForm()
        {
            InitializeComponent();
        }

        private void ExchangeForm_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            driveBox_Click(sender, e);
            flashBox_Click(sender, e);
        }
        #endregion

        #region Методы - свои
        private void disableControlsBeforeCreateFile()
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
        }

        private void enableControlsAfterCreateFile()
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
            disableControlsBeforeCreateFile();

            try
            {
                if (tabControl1.SelectedTab == tabPage2)
                {
                    int packetCount, docCount, packetSzvCount, docSzvCount;
                    packetCount = int.Parse(checkedPacketCountBox.Text);
                    docCount = int.Parse(chekedDocCountBox.Text);
                    packetSzvCount = int.Parse(packetCountBox.Text);
                    docSzvCount = int.Parse(docCountBox.Text);

                    if (packetCount < packetSzvCount)
                    {
                        if (MessageBox.Show("Количество выбранных пакетов документов: " + packetCount + "\nменьше, чем указано в сводной ведомости: " + packetSzvCount + ".\nПродолжить формирование электронных данных?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No)
                            return;
                    }

                    if (docCount < docSzvCount)
                    {
                        if (MessageBox.Show("Количество выбранных документов \"СЗВ-1\": " + packetCount + "\nменьше, чем указано в сводной ведомости: " + packetSzvCount + ".\nПродолжить формирование электронных данных?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No)
                            return;
                    }
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

                //Получаем ключ в HEX виде
                for (int i = shift * 2 - 2; i >= shift * 2 - 1024 + 30; i -= 32)
                {
                    key += key1.Substring(i, 2);
                }


                string tempKey = key;

                //key = key + key + key;
                //for(int j = tempKey.Length - 2; j >= 0; j-=2)
                //{
                //    key += tempKey.Substring(j, 2);
                //}

                keyArr = new byte[key.Length / 2];
                keyArr = ReadKey.HexToBin(key);
                table = table.Substring(0, 1024) + table1.Substring(0, 1024);
                tableArr = new byte[table.Length / 2];
                tableArr = ReadKey.HexToBin(table);
                //----------------------
                string tempData = "0000000000000000";
                key = "546D203368656C326973652073736E62206167796967747473656865202C3D73";
                table = "040A09020D08000E060B010C070F05030E0B040C060D0F0A02030801000705090508010D0A0304020E0F0C070600090B070D0A010008090F0E04060C0B020503060C0701050F0D08040A090E00030B02040B0A000702010D03060805090C0F0E0D0B0401030F0509000A0E070608020C010F0D0005070A040902030E060B080C";
                string sync = "1111111122222222";

                keyArr = new byte[key.Length / 2];
                keyArr = ReadKey.HexToBin(key);
                tableArr = new byte[table.Length / 2];
                tableArr = ReadKey.HexToBin(table);
                byte[] dataArr = new byte[tempData.Length / 2];
                dataArr = ReadKey.HexToBin(tempData);
                syncArr = ReadKey.HexToBin(sync);
                //sync = ReadKey.ArrayToString(syncArr);
                //syncArr = ReadKey.StringToArray(sync);

                string tmp = "";
                tmp = ReadKey.ArrayToString(syncArr);
                //tmp = ReadKey.ArrayToString(keyArr);
                //tmp = ReadKey.ArrayToString(tableArr);


                if (syncArr.Length != 8)
                    syncArr = new byte[8];
                Gost28147_89.GostSimple(ref dataArr, keyArr, tableArr, 32, true);
                //Gost28147_89.GostGama(ref dataArr, ref syncArr, keyArr, tableArr);

                //Debug.Print("syncArr " + string.Join(" ", syncArr));

                string resultStr;
                resultStr = ReadKey.ArrayToString(dataArr);
                tempData = ReadKey.BinToHex(resultStr);


                //---Расшифровка
                dataArr = new byte[tempData.Length / 2];
                dataArr = ReadKey.HexToBin(tempData);
                sync = "";
                syncArr = ReadKey.StringToArray(sync);
                if (syncArr.Length != 8)
                    syncArr = new byte[8];

                Gost28147_89.GostGama(ref dataArr, ref syncArr, keyArr, tableArr);
                resultStr = ReadKey.ArrayToString(dataArr);
                //---


                //----------------


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
                enableControlsAfterCreateFile();
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
            if (xmlFolderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                xmlPathTextBox.Text = xmlFolderBrowser.SelectedPath;
        }

        private void viewdataButton_Click(object sender, EventArgs e)
        {
            string data = "123321";
            string synchro = "C0E1205FF4F0CE01";
            string table = "E4EAE9E2EDE8E0EEE6EBE1ECE7EFE5E3B4BAB9B2BDB8B0BEB6BBB1BCB7BFB5B3444A49424D48404E464B414C474F4543C4CAC9C2CDC8C0CEC6CBC1CCC7CFC5C3646A69626D68606E666B616C676F6563D4DAD9D2DDD8D0DED6DBD1DCD7DFD5D3F4FAF9F2FDF8F0FEF6FBF1FCF7FFF5F3A4AAA9A2ADA8A0AEA6ABA1ACA7AFA5A3242A29222D28202E262B212C272F2523343A39323D38303E363B313C373F3533848A89828D88808E868B818C878F8583141A19121D18101E161B111C171F1513040A09020D08000E060B010C070F0503747A79727D78707E767B717C777F7573545A59525D58505E565B515C575F5553949A99929D98909E969B919C979F95937578717D7A7374727E7F7C777670797BD5D8D1DDDAD3D4D2DEDFDCD7D6D0D9DBA5A8A1ADAAA3A4A2AEAFACA7A6A0A9AB1518111D1A1314121E1F1C171610191B0508010D0A0304020E0F0C070600090B8588818D8A8384828E8F8C878680898B9598919D9A9394929E9F9C979690999BF5F8F1FDFAF3F4F2FEFFFCF7F6F0F9FBE5E8E1EDEAE3E4E2EEEFECE7E6E0E9EB4548414D4A4344424E4F4C474640494B6568616D6A6364626E6F6C676660696BC5C8C1CDCAC3C4C2CECFCCC7C6C0C9CBB5B8B1BDBAB3B4B2BEBFBCB7B6B0B9BB2528212D2A2324222E2F2C272620292B5558515D5A5354525E5F5C575650595B3538313D3A3334323E3F3C373630393B464C4741454F4D48444A494E40434B42B6BCB7B1B5BFBDB8B4BAB9BEB0B3BBB2A6ACA7A1A5AFADA8A4AAA9AEA0A3ABA2060C0701050F0D08040A090E00030B02767C7771757F7D78747A797E70737B72262C2721252F2D28242A292E20232B22161C1711151F1D18141A191E10131B12D6DCD7D1D5DFDDD8D4DAD9DED0D3DBD2363C3731353F3D38343A393E30333B32666C6761656F6D68646A696E60636B62868C8781858F8D88848A898E80838B82565C5751555F5D58545A595E50535B52969C9791959F9D98949A999E90939B92C6CCC7C1C5CFCDC8C4CAC9CEC0C3CBC2F6FCF7F1F5FFFDF8F4FAF9FEF0F3FBF2E6ECE7E1E5EFEDE8E4EAE9EEE0E3EBE21D1B1411131F1519101A1E171618121CFDFBF4F1F3FFF5F9F0FAFEF7F6F8F2FCDDDBD4D1D3DFD5D9D0DADED7D6D8D2DC0D0B0401030F0509000A0E070608020C5D5B5451535F5559505A5E575658525C7D7B7471737F7579707A7E777678727CADABA4A1A3AFA5A9A0AAAEA7A6A8A2AC4D4B4441434F4549404A4E474648424C9D9B9491939F9599909A9E979698929C2D2B2421232F2529202A2E272628222C3D3B3431333F3539303A3E373638323CEDEBE4E1E3EFE5E9E0EAEEE7E6E8E2EC6D6B6461636F6569606A6E676668626CBDBBB4B1B3BFB5B9B0BABEB7B6B8B2BC8D8B8481838F8589808A8E878688828CCDCBC4C1C3CFC5C9C0CACEC7C6C8C2CC";
            string key = "43C5066185400FDFF687B31526FAAB5E61F380EFC05150F5A2AA9C377A8C86B043C5066185400FDFF687B31526FAAB5E61F380EFC05150F5A2AA9C377A8C86B043C5066185400FDFF687B31526FAAB5E61F380EFC05150F5A2AA9C377A8C86B0B0868C7A379CAAA2F55051C0EF80F3615EABFA2615B387F6DF0F40856106C543";
            string res = Mathdll.GostGamma(data, key, table, synchro);
        }

        private void senddataButton_Click(object sender, EventArgs e)
        {
            
        }
        #endregion
    }
}
