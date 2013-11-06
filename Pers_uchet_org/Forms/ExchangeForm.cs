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

        #region Методы
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

        private void xmlPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog xmlFolderBrowser = new FolderBrowserDialog();
            xmlFolderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            xmlFolderBrowser.ShowNewFolderButton = false;
            if (xmlFolderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                xmlPathTextBox.Text = xmlFolderBrowser.SelectedPath;
        }
    }
}
