﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Pers_uchet_org
{
    public class MyXml
    {
        public enum ReportType { ADV1=0, ADV2, ADV3, ADV4, ADV5, ADV6, SZV1, SZV2, SZV3, RDV1, RDV21, RDV22, RDV3}

        public static string GetReportUrl(ReportType type)
        {
            string url;
            switch (type)
            {
                case MyXml.ReportType.ADV1:
                    url = Properties.Settings.Default.report_adv1;
                    break;
                case MyXml.ReportType.ADV2:
                    url = Properties.Settings.Default.report_adv2;
                    break;
                case MyXml.ReportType.ADV3:
                    url = Properties.Settings.Default.report_adv3;
                    break;
                case MyXml.ReportType.ADV4:
                    url = Properties.Settings.Default.report_adv4;
                    break;
                case MyXml.ReportType.ADV5:
                    url = Properties.Settings.Default.report_adv5;
                    break;
                case MyXml.ReportType.ADV6:
                    url = Properties.Settings.Default.report_adv6;
                    break;
                case MyXml.ReportType.SZV1:
                    url = Properties.Settings.Default.report_szv1;
                    break;
                case MyXml.ReportType.SZV2:
                    url = Properties.Settings.Default.report_szv2;
                    break;
                case MyXml.ReportType.SZV3:
                    url = Properties.Settings.Default.report_szv3;
                    break;
                default:
                    url = "/";
                    break;
            }
            url = Path.GetFullPath(url);
            if (File.Exists(url))
                return url;
            else
                return null;
        }

        static public XmlDocument PersonXml(DataRow row)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement doc_info = xml.CreateElement("doc_info");
            XmlElement person = xml.CreateElement("person");
            XmlElement lname = xml.CreateElement("lname");
            XmlElement fname = xml.CreateElement("fname");
            XmlElement mname = xml.CreateElement("mname");
            XmlElement citizen = xml.CreateElement("citizen");
            XmlElement citizen1 = xml.CreateElement("first");
            XmlElement citizen2 = xml.CreateElement("second");
            XmlElement citizen1ID = xml.CreateElement("id");
            XmlElement citizen2ID = (XmlElement)citizen1ID.Clone();
            XmlElement citizen1Name = xml.CreateElement("name");
            XmlElement citizen2Name = (XmlElement)citizen1Name.Clone();
            XmlElement categoryID = xml.CreateElement("category_id");
            XmlElement categoryName = xml.CreateElement("category_name");
            XmlElement privelegeID = xml.CreateElement("privelege_id");
            XmlElement privelegeName = xml.CreateElement("privelege_name");

            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "windows-1251", null));
            xml.AppendChild(doc_info);
            doc_info.AppendChild(person);
            person.AppendChild(lname);
            person.AppendChild(fname);
            person.AppendChild(mname);
            person.AppendChild(citizen);
            citizen.AppendChild(citizen1);
            citizen.AppendChild(citizen2);
            citizen1.AppendChild(citizen1ID);
            citizen1.AppendChild(citizen1Name);
            citizen2.AppendChild(citizen2ID);
            citizen2.AppendChild(citizen2Name);
            person.AppendChild(categoryID);
            person.AppendChild(categoryName);
            person.AppendChild(privelegeID);
            person.AppendChild(privelegeName);

            lname.InnerText = row[PersonView.lName] as string;
            fname.InnerText = row[PersonView.fName] as string;
            mname.InnerText = row[PersonView.mName] as string;
            citizen1ID.InnerText = row["citizen1_id"].ToString();
            citizen2ID.InnerText = row["citizen2_id"].ToString();
            citizen1Name.InnerText = row["citizen1_name"] as string;
            citizen2Name.InnerText = row["citizen2_name"] as string;
            categoryID.InnerText = row["category_id"].ToString();
            categoryName.InnerText = row["category_name"] as string;
            privelegeID.InnerText = row["privelege_id"].ToString();
            privelegeName.InnerText = row["privelege_name"] as string;
            //
            return xml;
        }

        static public XmlDocument Adv1Xml(DataRow personViewRow)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement person = xml.CreateElement("person");
            XmlElement lname = xml.CreateElement("lname");
            XmlElement fname = xml.CreateElement("fname");
            XmlElement mname = xml.CreateElement("mname");
            XmlElement regnum = xml.CreateElement("regnum");
            XmlElement birthday = xml.CreateElement("birthday");
            XmlElement sex = xml.CreateElement("sex");
            XmlElement doctype = xml.CreateElement("doctype");
            XmlElement docseries = xml.CreateElement("docseries");
            XmlElement docnumber = xml.CreateElement("docnumber");
            XmlElement docdate = xml.CreateElement("docdate");
            XmlElement docorg = xml.CreateElement("docorg");
            XmlElement regaddressZipcode = xml.CreateElement("regaddress_zipcode");
            XmlElement regaddress = xml.CreateElement("regaddress");
            XmlElement factaddressZipcode = xml.CreateElement("factaddress_zipcode");
            XmlElement factaddress = xml.CreateElement("factaddress");
            XmlElement bornaddress = xml.CreateElement("bornaddress");
            XmlElement bornCountry = xml.CreateElement("borncountry");
            XmlElement bornArea = xml.CreateElement("bornarea");
            XmlElement bornRegion = xml.CreateElement("bornregion");
            XmlElement bornCity = xml.CreateElement("borncity");
            XmlElement bornZipcode = xml.CreateElement("bornzipcode");
            XmlElement citizen = xml.CreateElement("citizen");
            XmlElement citizen1 = xml.CreateElement("first");
            XmlElement citizen2 = xml.CreateElement("second");
            XmlElement citizen1ID = xml.CreateElement("id");
            XmlElement citizen2ID = (XmlElement)citizen1ID.Clone();
            XmlElement citizen1Name = xml.CreateElement("name");
            XmlElement citizen2Name = (XmlElement)citizen1Name.Clone();

            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "windows-1251", null));
            xml.AppendChild(person);
            person.AppendChild(lname);
            person.AppendChild(fname);
            person.AppendChild(mname);
            person.AppendChild(regnum);
            person.AppendChild(birthday);
            person.AppendChild(sex);
            person.AppendChild(doctype);
            person.AppendChild(docseries);
            person.AppendChild(docnumber);
            person.AppendChild(docdate);
            person.AppendChild(docorg);
            person.AppendChild(regaddressZipcode);
            person.AppendChild(regaddress);
            person.AppendChild(factaddressZipcode);
            person.AppendChild(factaddress);
            bornaddress.AppendChild(bornCountry);
            bornaddress.AppendChild(bornArea);
            bornaddress.AppendChild(bornRegion);
            bornaddress.AppendChild(bornCity);
            bornaddress.AppendChild(bornZipcode);
            person.AppendChild(bornaddress);
            person.AppendChild(citizen);
            citizen.AppendChild(citizen1);
            citizen.AppendChild(citizen2);
            citizen1.AppendChild(citizen1ID);
            citizen1.AppendChild(citizen1Name);
            citizen2.AppendChild(citizen2ID);
            citizen2.AppendChild(citizen2Name);

            lname.InnerText = personViewRow[PersonView.lName] as string;
            fname.InnerText = personViewRow[PersonView.fName] as string;
            mname.InnerText = personViewRow[PersonView.mName] as string;
            regnum.InnerText = personViewRow[PersonView.socNumber] as string;
            birthday.InnerText = ((DateTime)personViewRow[PersonView.birthday]).ToShortDateString();
            sex.InnerText = (int)personViewRow[PersonView.sex]==1? "м": "ж";
            doctype.InnerText = personViewRow[PersonView.docType] as string;
            docseries.InnerText = personViewRow[PersonView.docSeries] as string;
            docnumber.InnerText = personViewRow[PersonView.docNumber] as string;
            docdate.InnerText = ((DateTime)personViewRow[PersonView.docDate]).ToShortDateString();
            docorg.InnerText = personViewRow[PersonView.docOrg] as string;
            regaddressZipcode.InnerText = personViewRow[PersonView.regAdressZipcode] as string;
            regaddress.InnerText = personViewRow[PersonView.regAdress] as string;
            factaddressZipcode.InnerText = personViewRow[PersonView.factAdressZipcode] as string;
            factaddress.InnerText = personViewRow[PersonView.factAdress] as string;
            bornCountry.InnerText = personViewRow[PersonView.bornAdressCountry] as string;
            bornArea.InnerText = personViewRow[PersonView.bornAdressArea] as string;
            bornRegion.InnerText = personViewRow[PersonView.bornAdressRegion] as string;
            bornCity.InnerText = personViewRow[PersonView.bornAdressCity] as string;
            bornZipcode.InnerText = personViewRow[PersonView.bornAdressZipcode] as string;
            citizen1ID.InnerText = personViewRow["citizen1_id"].ToString();
            citizen2ID.InnerText = personViewRow["citizen2_id"].ToString();
            citizen1Name.InnerText = personViewRow["citizen1"] as string;
            citizen2Name.InnerText = personViewRow["citizen2"] as string;
            //
            return xml;
        }


    }

    public class MyPrinter
    {
        static public void SetPrintSettings()
        {
            using (var saveKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\PageSetup", true))
            {
                saveKey.SetValue("margin_top", "0.39", RegistryValueKind.String);
                saveKey.SetValue("margin_bottom", "0.39", RegistryValueKind.String);
                saveKey.SetValue("margin_left", "0.39", RegistryValueKind.String);
                saveKey.SetValue("margin_right", "0.39", RegistryValueKind.String);
            }
        }

        static public void ShowPrintPreviewWebPage(WebBrowser wb)
        {
            MyPrinter.SetPrintSettings();
            wb.ShowPrintPreviewDialog();
        }

        static public void ShowWebPage(WebBrowser wb)
        {
            Form webForm = new Form();
            webForm.Width = 850;
            webForm.Height = 600;
            webForm.Controls.Add(wb);
            wb.Dock = DockStyle.Fill;
            wb.Show();
            webForm.Show();
        }

        static public void ShowWebPage(WebBrowser wb, string url)
        {
            if (wb == null)
                wb = new WebBrowser();
            wb.Navigate(url);
            ShowWebPage(wb);
        }

        static public void ShowWebPage(WebBrowser wb, MyXml.ReportType type)
        {
            string url = MyXml.GetReportUrl(type);
            if (url != null)
            {
                ShowWebPage(wb, url);
            }
            else
            {
                MainForm.ShowWarningMessage("Не удалось найти файл отчета!","Внимание");
            }
        }

        static public void PrintWebPage(WebBrowser wb)
        {
            MyPrinter.SetPrintSettings();
            wb.Print();
        }
    }

    public class Operator
    {
        // Название таблицы в БД
        static public string tablename = "Operator";

        #region Названия полей таблицы в БД
        static public string id = "id";
        static public string name = "name";
        static public string password = "password";
        static public string candelete = "candelete";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pName = "@name";
        static public string pPassword = "@password";
        static public string pCandelete = "@candelete";
        #endregion

        #region Значения полей объекта
        public long idVal;
        public string nameVal;
        public string passwordVal;
        public int candeleteVal;
        #endregion

        #region Методы
        public bool IsAdmin()
        {
            return (this.candeleteVal == 0);
        }
        #endregion

        #region Методы статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(name, typeof(string));
            table.Columns.Add(password, typeof(string));
            table.Columns.Add(candelete, typeof(int));
            //
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3} FROM {4}",
                                        id, name, password, candelete, tablename);
        }

        static public SQLiteCommand CreateSelectCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pName, DbType.String, name));
            comm.Parameters.Add(new SQLiteParameter(pPassword, DbType.String, password));
            comm.Parameters.Add(new SQLiteParameter(pCandelete, DbType.String, candelete));
            comm.CommandText = GetSelectText();
            return comm;
        }

        static public SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pName, DbType.String, name));
            comm.Parameters.Add(new SQLiteParameter(pCandelete, DbType.String, candelete));
            comm.Parameters.Add(new SQLiteParameter(pPassword, DbType.String, password));
            comm.CommandText = string.Format(@"BEGIN INSERT INTO [{0}] ({1}, {2}) VALUES ({3}, {4});
                                               SELECT last_insert_rowid(); END",
                                            tablename, name, password, pName, pPassword);
            return comm;
        }

        static public SQLiteCommand CreateUpdateCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pName, DbType.String, name));
            comm.Parameters.Add(new SQLiteParameter(pPassword, DbType.String, password));
            comm.Parameters.Add(new SQLiteParameter(pCandelete, DbType.String, candelete));
            comm.CommandText = string.Format(@"UPDATE {0} SET {1} = {2}, {3} = {4} WHERE {5} = {6};",
                                                tablename, name, pName, password, pPassword, id, pId);
            return comm;
        }

        static public SQLiteCommand CreateDeleteCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pName, DbType.String, name));
            comm.Parameters.Add(new SQLiteParameter(pPassword, DbType.String, password));
            comm.Parameters.Add(new SQLiteParameter(pCandelete, DbType.String, candelete));
            comm.CommandText = string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                                                tablename, id, pId);
            return comm;
        }

        static public SQLiteDataAdapter CreateAdapter(string connectionStr)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand();
            adapter.SelectCommand.Connection = connection;
            adapter.InsertCommand = CreateInsertCommand();
            adapter.InsertCommand.Connection = connection;
            adapter.UpdateCommand = CreateUpdateCommand();
            adapter.UpdateCommand.Connection = connection;
            adapter.DeleteCommand = CreateDeleteCommand();
            adapter.DeleteCommand.Connection = connection;

            return adapter;
        }

        static public string GetSelectCommandText(long org_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3} FROM {4} WHERE {0} = {5};",
                                        id, name, password, candelete, tablename, org_id);
        }

        static public Operator GetOperator(long operator_id, string connectionStr)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.CommandText = GetSelectCommandText(operator_id);
            command.Connection = new SQLiteConnection(connectionStr);
            Operator oper = new Operator();

            command.Connection.Open();
            oper.idVal = operator_id;
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                oper.idVal = (long)reader[id];
                oper.nameVal = reader[name] as string;
                oper.passwordVal = reader[password] as string;
                oper.candeleteVal = (int)reader[candelete];
            }
            command.Connection.Close();
            //
            return oper;
        }

        static public Operator GetOperator(string login, string password, string connectionStr)
        {
            Operator oper;
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;
            command.CommandText = GetSelectText() + string.Format(" WHERE {0} = '{1}' AND {2} = '{3}';", Operator.name, login, Operator.password, password);
            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                oper = new Operator();
                oper.idVal = (long)reader[Operator.id];
                oper.nameVal = reader[Operator.name] as string;
                oper.passwordVal = reader[Operator.password] as string;
                oper.candeleteVal = (int)reader[candelete];
            }
            else
            {
                oper = null;
            }
            reader.Close();
            connection.Close();
            return oper;
        }

        static public bool IsAdmin(DataRow operatorRow)
        {
            return ((int)operatorRow[candelete] == 0);
        }
        #endregion
    }

    public class Org
    {
        // Название таблицы в БД
        static public string tablename = "Org";

        #region Названия полей таблицы в БД
        static public string id = "id";
        static public string regnum = "regnum";
        static public string name = "name";
        static public string chief_post = "chief_post";
        static public string chief_fio = "chief_fio";
        static public string booker_fio = "booker_fio";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pRegnum = "@regnum";
        static public string pName = "@name";
        static public string pChief_post = "@chief_post";
        static public string pChief_fio = "@chief_fio";
        static public string pBooker_fio = "@booker_fio";
        #endregion

        #region Значения полей объекта
        public long idVal;
        public string regnumVal;
        public string nameVal;
        public string chiefpostVal;
        public string chieffioVal;
        public string bookerfioVal;
        #endregion

        #region Методы статические
        static public string GetSelectCommandText()
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, {4}, {5} FROM {6} ",
                            id, regnum, name, chief_post, chief_fio, booker_fio, tablename);
        }

        static public string GetSelectCommandText(long org_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, [{4}], {5} FROM {6} WHERE {0} = {7} ",
                            id, regnum, name, chief_post, chief_fio, booker_fio, tablename, org_id);
        }

        static public string GetSelectByPerson(long person_id)
        {
            return string.Format("{0} WHERE {1} in ({2})",
                                GetSelectCommandText(),
                                id,
                                PersonOrg.GetSelectOrgIDText(person_id));
        }

        static public string GetSelectTextByOperator(long oper_id)
        {
            return string.Format(@"{0} WHERE {1} IN (SELECT DISTINCT {2} FROM {3} WHERE {4} = {5}) ",
                                    GetSelectCommandText(),
                                    id,
                                    OperatorOrg.orgID, OperatorOrg.tablename, OperatorOrg.operatorID, oper_id);
        }

        static public string GetSelectTextByOperatorAccess(long oper_id)
        {
            return string.Format("{0} WHERE {1} IN ({2}) ",
                GetSelectCommandText(),
                id,
                OperatorOrg.GetSelectOrgIDForEditText(oper_id));
        }

        static public SQLiteCommand CreateSelectCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pRegnum, DbType.String, regnum));
            comm.Parameters.Add(new SQLiteParameter(pName, DbType.String, name));
            comm.Parameters.Add(new SQLiteParameter(pChief_post, DbType.String, chief_post));
            comm.Parameters.Add(new SQLiteParameter(pChief_fio, DbType.String, chief_fio));
            comm.Parameters.Add(new SQLiteParameter(pBooker_fio, DbType.String, booker_fio));
            comm.CommandText = GetSelectCommandText();
            return comm;
        }

        static public SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pRegnum, DbType.String, regnum));
            comm.Parameters.Add(new SQLiteParameter(pName, DbType.String, name));
            comm.Parameters.Add(new SQLiteParameter(pChief_post, DbType.String, chief_post));
            comm.Parameters.Add(new SQLiteParameter(pChief_fio, DbType.String, chief_fio));
            comm.Parameters.Add(new SQLiteParameter(pBooker_fio, DbType.String, booker_fio));
            comm.CommandText = string.Format(@" INSERT INTO [{0}] 
                                                ({1}, {2}, {3}, [{4}], {5})
                                                VALUES
                                                ({6}, {7}, {8}, {9}, {10});
                                                SELECT last_indert_rowid() ",
                                        tablename,
                                        regnum, name, chief_post, chief_fio, booker_fio,
                                        pRegnum, pName, pChief_post, pChief_fio, pBooker_fio);
            return comm;
        }

        static public SQLiteCommand CreateUpdateCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pRegnum, DbType.String, regnum));
            comm.Parameters.Add(new SQLiteParameter(pName, DbType.String, name));
            comm.Parameters.Add(new SQLiteParameter(pChief_post, DbType.String, chief_post));
            comm.Parameters.Add(new SQLiteParameter(pChief_fio, DbType.String, chief_fio));
            comm.Parameters.Add(new SQLiteParameter(pBooker_fio, DbType.String, booker_fio));
            comm.CommandText = string.Format(@"
                                UPDATE {0} SET
                                 {1} = {2}, {3} = {4}, {5} = {6}
                                ,{7} = {8}, {9} = {10} 
                                WHERE{11} = {12};",
                            tablename,
                            regnum, pRegnum, name, pName, chief_post, pChief_post,
                            chief_fio, pChief_fio, booker_fio, pBooker_fio,
                            id, pId);
            return comm;
        }

        static public SQLiteCommand CreateDeleteCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pRegnum, DbType.String, regnum));
            comm.Parameters.Add(new SQLiteParameter(pName, DbType.String, name));
            comm.Parameters.Add(new SQLiteParameter(pChief_post, DbType.String, chief_post));
            comm.Parameters.Add(new SQLiteParameter(pChief_fio, DbType.String, chief_fio));
            comm.Parameters.Add(new SQLiteParameter(pBooker_fio, DbType.String, booker_fio));
            comm.CommandText = string.Format(@" DELETE FROM {0} WHERE {1} = {2} ",
                                                            tablename, id, pId);
            return comm;
        }

        static public SQLiteDataAdapter CreateAdapter(string connectionStr)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand();
            adapter.SelectCommand.Connection = connection;
            adapter.InsertCommand = CreateInsertCommand();
            adapter.InsertCommand.Connection = connection;
            adapter.UpdateCommand = CreateUpdateCommand();
            adapter.UpdateCommand.Connection = connection;
            adapter.DeleteCommand = CreateDeleteCommand();
            adapter.DeleteCommand.Connection = connection;
            return adapter;
        }

        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(regnum, typeof(string));
            table.Columns.Add(name, typeof(string));
            table.Columns.Add(chief_post, typeof(string));
            table.Columns.Add(chief_fio, typeof(string));
            table.Columns.Add(booker_fio, typeof(string));
            //
            return table;
        }
        #endregion
    }

    public class OperatorOrg
    {
        // Название таблицы в БД
        static public string tablename = "Operator_Org_relation";

        #region Названия полей таблицы в БД
        static public string id = "id";
        static public string orgID = "org_id";
        static public string operatorID = "operator_id";
        static public string code = "privileges_code";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pOrgID = "@orgID";
        static public string pOperatorID = "@operatorID";
        static public string pCode = "@privileges_code";
        #endregion

        #region Значения полей объекта
        public long idVal;
        public long orgIDVal;
        public long operatorIDVal;
        public string codeVal;
        #endregion

        #region Методы статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(operatorID, typeof(long));
            table.Columns.Add(code, typeof(string));
            return table;
        }

        static public SQLiteCommand CreateSelectCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.UInt64, orgID));
            comm.Parameters.Add(new SQLiteParameter(pOperatorID, DbType.UInt64, operatorID));
            comm.Parameters.Add(new SQLiteParameter(pCode, DbType.String, code));
            comm.CommandText = GetSelectCommandText();
            return comm;
        }

        static public SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.String, orgID));
            comm.Parameters.Add(new SQLiteParameter(pOperatorID, DbType.String, operatorID));
            comm.Parameters.Add(new SQLiteParameter(pCode, DbType.String, code));
            comm.CommandText = string.Format(@" INSERT INTO [{0}] ({1}, {2}, {3}) VALUES ({4}, {5}, {6});
                                               SELECT last_insert_rowid() ",
                                            tablename, orgID, operatorID, code, pOrgID, pOperatorID, pCode);
            return comm;
        }

        static public SQLiteCommand CreateUpdateCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.String, orgID));
            comm.Parameters.Add(new SQLiteParameter(pOperatorID, DbType.String, operatorID));
            comm.CommandText = string.Format(@"UPDATE {0} SET {1} = {2}, {3} = {4}, {5} = {6} WHERE {7} = {8};",
                                                tablename, orgID, pOrgID, operatorID, pOperatorID, code, pCode, id, pId);
            return comm;
        }

        static public SQLiteCommand CreateDeleteCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOperatorID, DbType.String, operatorID));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.String, orgID));
            comm.Parameters.Add(new SQLiteParameter(pCode, DbType.String, code));
            comm.CommandText = string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                                                tablename, id, pId);
            return comm;
        }

        static public SQLiteDataAdapter CreateAdapter(string connectionStr)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand();
            adapter.SelectCommand.Connection = connection;
            adapter.InsertCommand = CreateInsertCommand();
            adapter.InsertCommand.Connection = connection;
            adapter.UpdateCommand = CreateUpdateCommand();
            adapter.UpdateCommand.Connection = connection;
            adapter.DeleteCommand = CreateDeleteCommand();
            adapter.DeleteCommand.Connection = connection;

            return adapter;
        }

        static public string GetSelectCommandText()
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3} FROM {4};",
                                                    id, orgID, operatorID, code, tablename);
        }

        static public string GetSelectCommandText(long oper_org_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3} FROM {4} WHERE {0} = {5};",
                                        id, orgID, operatorID, code, tablename, oper_org_id);
        }

        static public string GetSelectCommandText(long operator_id, long org_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3} FROM {4} WHERE {1} = {5} AND {2} = {6};",
                                        id, operatorID, orgID, code, tablename, operator_id, org_id);
        }

        static public string GetSelectPrivilegeText(long operator_id, long org_id)
        {
            return string.Format(@"SELECT {0} FROM {1} WHERE {2} = {3} AND {4} = {5} LIMIT 1;",
                                        code, tablename, operatorID, operator_id, orgID, org_id);
        }

        static public string GetSelectOrgIDText(long operator_id)
        {
            return string.Format("SELECT DISTINCT {0} FROM {1} WHERE  {2} = {3} ",
                                orgID, tablename, operatorID, operator_id, code);
        }

        static public string GetSelectOrgIDForEditText(long operator_id)
        {
            return string.Format("SELECT DISTINCT {0} FROM {1} WHERE  {2} = {3} AND {4} LIKE '2%'",
                                orgID, tablename, operatorID, operator_id, code);
        }

        static public OperatorOrg GetOperatorOrg(long oper_org_id, string connectionStr)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.CommandText = GetSelectCommandText(oper_org_id);
            command.Connection = new SQLiteConnection(connectionStr);
            OperatorOrg operOrg = new OperatorOrg();

            command.Connection.Open();
            operOrg.idVal = oper_org_id;
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                operOrg.orgIDVal = (long)reader[orgID];
                operOrg.operatorIDVal = (long)reader[operatorID];
                operOrg.codeVal = reader[code] as string;
            }
            command.Connection.Close();
            //
            return operOrg;
        }

        static public string GetPrivilege(long operator_id, long org_id, string connectionStr)
        {
            SQLiteCommand command = new SQLiteCommand(OperatorOrg.GetSelectPrivilegeText(operator_id, org_id));
            command.Connection = new SQLiteConnection(connectionStr);
            command.Connection.Open();
            object code = command.ExecuteScalar();
            command.Connection.Close();
            return code as string;
        }

        static public string GetPrivilegeForAdmin()
        {
            return "212111";
        }

        static public int[] GetAccessArray(string code)
        {
            /*
         * Уровень доступа оператора к данным определяется строкой из цифр, где
         * 0 цифра - уровень доступа к анкетным данным (0,1,2)
         * 1 цифра - доступ к печати анкетных данных (0,1)
         * 2 цифра - уровень доступа к стажу и доходу (0,1,2)
         * 3 цифра - доступ к печати стажа и дохода (0,1)
         * 4 цифра - доступ к электронному обмену с ЕГФСС (0,1)
         * 5 цифра - доступ к импорту анкет из других организаций, даже из тех, к которым оператор не имеет полного досупа (0,1)
         * 
         * Уровень досупа бывает:
         * без доступа = 0
         * только чтение = 1
         * полный доступ = 2
         */
            int[] codeArr = new int[6];
            codeArr[0] = int.Parse(code[0].ToString());
            codeArr[1] = int.Parse(code[1].ToString());
            codeArr[2] = int.Parse(code[2].ToString());
            codeArr[3] = int.Parse(code[3].ToString());
            codeArr[4] = int.Parse(code[4].ToString());
            codeArr[5] = int.Parse(code[5].ToString());
            return codeArr;
        }

        static public int GetAnketaDataAccesseCode(string code)
        {
            return int.Parse(code[0].ToString());
        }

        static public int GetAnketaPrintAccesseCode(string code)
        {
            return int.Parse(code[1].ToString());
        }

        static public int GetStajDohodDataAccesseCode(string code)
        {
            return int.Parse(code[2].ToString());
        }

        static public int GetStajDohodPrintAccesseCode(string code)
        {
            return int.Parse(code[3].ToString());
        }

        static public int GetExchangeAccesseCode(string code)
        {
            return int.Parse(code[4].ToString());
        }

        static public int GetImportDataAccesseCode(string code)
        {
            return int.Parse(code[5].ToString());
        }
        #endregion
    }

    public class PersonOrg
    {
        static public string tablename = "Person_Org_relation";

        #region Названия полей таблицы в БД
        static public string id = "id";
        static public string orgID = "org_id";
        static public string personID = "person_id";
        static public string state = "state";
        static public string dismissDate = "dismiss_date";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pOrgID = "@org_id";
        static public string pPersonID = "@person_id";
        static public string pState = "@state";
        static public string pDismissDate = "@dismiss_date";
        #endregion

        #region Методы - статические
        static public SQLiteCommand CreateSelectCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.UInt64, orgID));
            comm.Parameters.Add(new SQLiteParameter(pPersonID, DbType.UInt64, personID));
            comm.CommandText = GetSelectCommandText();
            return comm;
        }

        static public SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.UInt64, orgID));
            comm.Parameters.Add(new SQLiteParameter(pPersonID, DbType.UInt64, personID));
            comm.CommandText = string.Format(@"INSERT INTO [{0}] ({1}, {2}) VALUES ({3}, {4}); SELECT last_indert_rowid();",
                                            tablename, orgID, personID, pOrgID, pPersonID);
            return comm;
        }

        static public SQLiteCommand CreateUpdateCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.UInt64, orgID));
            comm.Parameters.Add(new SQLiteParameter(pPersonID, DbType.UInt64, personID));
            comm.CommandText = string.Format(@"UPDATE {0} SET {1} = {2}, {3} = {4} WHERE {5} = {6};",
                                            tablename, orgID, pOrgID, personID, pPersonID, id, pId);
            return comm;
        }

        static public SQLiteDataAdapter CreateAdapter(string connectionStr)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand();
            adapter.InsertCommand = CreateInsertCommand();
            adapter.UpdateCommand = CreateUpdateCommand();
            SQLiteConnection connction = new SQLiteConnection(connectionStr);
            adapter.SelectCommand.Connection = connction;
            adapter.InsertCommand.Connection = connction;
            adapter.UpdateCommand.Connection = connction;
            return adapter;
        }

        static public string GetSelectCommandText()
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {4}, {5} FROM {6};",
                                        id, orgID, personID, state, dismissDate, tablename);
        }

        static public string GetSelectCommandText(long person_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, {4} FROM {5} WHERE {2} = {6};",
                                        id, orgID, personID, state, dismissDate, tablename, person_id);
        }

        static public string GetSelectPersonIDText(long org_id)
        {
            return string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2} = {3} ",
                                    personID, tablename, orgID, org_id);
        }

        static public string GetSelectOrgIDText(long person_id)
        {
            return string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2} = {3} ",
                                    orgID, tablename, personID, person_id);
        }

        static public string GetSelectRowIDText(long person_id, long org_id)
        {
            return string.Format(@" SELECT {0} FROM {1} WHERE {2} = {3} AND {4} = {5} ",
                                id, tablename, personID, person_id, orgID, org_id);
        }

        static public string GetInsertPersonOrgText(long person_id, long org_id)
        {
            return string.Format(@"INSERT OR IGNORE INTO {0} ({1}, {2}, {3}) VALUES ( ({4}), {5}, {6}) ",
                                tablename,
                                id, orgID, personID,
                                GetSelectRowIDText(person_id, org_id), org_id, person_id);
        }

        static public string GetDeleteCommandText(long person_id)
        {
            return string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                                        tablename, personID, person_id);
        }

        static public string GetDeletePersonOrgText(long person_id, long org_id)
        {
            return string.Format(" DELETE FROM {0} WHERE {1} = {2} AND {3} = {4} ",
                                tablename, personID, person_id, orgID, org_id);
        }

        static public string GetDeletePersonOrgText(long person_id, long[] org_idArray)
        {
            string instr = "( ";
            foreach (long val in org_idArray)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";

            return string.Format(" DELETE FROM {0} WHERE {1} = {2} AND {3} IN {4} ",
                                tablename, personID, person_id, orgID, instr);
        }

        static public string GetChangeStateText(long person_id, long org_id, object stateVal, string date)
        {
            DateTime dateval;
            if (DateTime.TryParse(date, out dateval))
                date = string.Format("'{0}'", date);
            else
                date = "NULL";
            return string.Format(" UPDATE [{0}] SET {1} = {2}, {3} = {4} WHERE {5} = {6} AND {7} = {8} ",
                                    tablename, state, stateVal, dismissDate, date,
                                    personID, person_id, orgID, org_id);
        }

        static public string GetChangeStateText(IEnumerable<long> personIDArr, long org_id, object stateVal, string date)
        {
            string personsIdStr = "( ";
            foreach (long val in personIDArr)
                personsIdStr += val + ",";
            personsIdStr = personsIdStr.Remove(personsIdStr.Length - 1);
            personsIdStr += " )";
            DateTime dateval;
            if (DateTime.TryParse(date, out dateval))
                date = string.Format("'{0}'", date);
            else
                date = "NULL";
            return string.Format("UPDATE {0} SET {1} = {2}, {3} = {4} WHERE {5} in {6} AND {7} = {8} ",
                                    tablename, state, stateVal, dismissDate, date,
                                    personID, personsIdStr, orgID, org_id);
        }

        static public long[] GetOrgID(long person_id, string connectionStr)
        {
            LinkedList<long> idList = new LinkedList<long>();
            SQLiteCommand command = new SQLiteCommand(GetSelectOrgIDText(person_id));
            command.Connection = new SQLiteConnection(connectionStr);
            command.Connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                idList.AddLast((long)reader[PersonOrg.orgID]);
            }
            command.Connection.Close();
            return idList.ToArray();
        }

        static public void InsertPersonOrg(long person_id, long[] org_idArray, string connectionStr)
        {
            string commantText = "begin transaction; \n";
            foreach (long org_id in org_idArray)
                commantText += GetInsertPersonOrgText(person_id, org_id) + "; \n";
            commantText += "commit transaction;";
            SQLiteCommand command = new SQLiteCommand(commantText);
            command.Connection = new SQLiteConnection(connectionStr);
            command.Connection.Open();
            int count = command.ExecuteNonQuery();
            command.Connection.Close();
        }

        static public void InsertPersonOrg(List<long> person_idArr, long org_id, string connectionStr)
        {
            string commantText = String.Empty;
            foreach (long person_id in person_idArr)
                commantText += GetInsertPersonOrgText(person_id, org_id) + "; \n";

            using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                SQLiteTransaction trans = connection.BeginTransaction();
                SQLiteCommand command = new SQLiteCommand(commantText, connection, trans);
                int count = command.ExecuteNonQuery();
                trans.Commit();
            }
        }

        static public void DeletePersonOrg(long person_id, long[] org_idArray, string connectionStr)
        {
            string commantText = GetDeletePersonOrgText(person_id, org_idArray);
            SQLiteCommand command = new SQLiteCommand(commantText);
            command.Connection = new SQLiteConnection(connectionStr);
            command.Connection.Open();
            int count = command.ExecuteNonQuery();
            command.Connection.Close();
        }

        static public void ChangeState(long person_id, long org_id, object stateVal, string date, string connectionStr)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            command.CommandText = GetChangeStateText(person_id, org_id, stateVal, date);
            command.Connection = connection;
            connection.Open();
            int count = command.ExecuteNonQuery();
            connection.Close();
        }

        static public void ChangeState(IEnumerable<long> personIDArr, long org_id, object stateVal, string date, string connectionStr)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            command.CommandText = GetChangeStateText(personIDArr, org_id, stateVal, date);
            command.Connection = connection;
            connection.Open();
            int count = command.ExecuteNonQuery();
            connection.Close();
        }

        static public void SetStateToUvolit(long person_id, long org_id, DateTime date, string connectionStr)
        {
            ChangeState(person_id, org_id, 0, date.ToString("yyyy-MM-dd"), connectionStr);
        }

        static public void SetStateToUvolit(IEnumerable<long> personIDArr, long org_id, DateTime date, string connectionStr)
        {
            ChangeState(personIDArr, org_id, 0, date.ToString("yyyy-MM-dd"), connectionStr);
        }

        static public void SetStateToRabotaet(long person_id, long org_id, string connectionStr)
        {
            ChangeState(person_id, org_id, 1, null, connectionStr);
        }

        static public void SetStateToRabotaet(IEnumerable<long> personIDArr, long org_id, string connectionStr)
        {
            ChangeState(personIDArr, org_id, 1, null, connectionStr);
        }
        #endregion
    }

    public class PersonInfo
    {
        // название таблицы в БД
        static public string tablename = "Person_info";

        #region Поля таблицы в БД
        static public string id = "id";
        static public string socNumber = "soc_number";
        static public string fname = "f_name";
        static public string mname = "m_name";
        static public string lname = "l_name";
        static public string birthday = "birthday";
        static public string sex = "sex";
        static public string docID = "idoc_info_id";
        static public string regadrID = "regaddr_id";
        static public string factadrID = "factaddr_id";
        static public string birthplaceID = "birthplace_id";
        static public string citizen1 = "citizen1";
        static public string citizen2 = "citizen2";
        static public string state = "state";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pSocNumber = "@soc_number";
        static public string pFname = "@f_name";
        static public string pMname = "@m_name";
        static public string pLname = "@l_name";
        static public string pBirthday = "@birthday";
        static public string pSex = "@sex";
        static public string pDocID = "@idoc_info_id";
        static public string pRegadrID = "@regaddr_id";
        static public string pFactadrID = "@factaddr_id";
        static public string pBornplaceID = "@birthplace_id";
        static public string pCitizen1 = "@citizen1";
        static public string pCitizen2 = "@citizen2";
        static public string pState = "@state";
        #endregion

        #region Методы
        static public SQLiteCommand CreateSelectCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pSocNumber, DbType.String, socNumber));
            comm.Parameters.Add(new SQLiteParameter(pFname, DbType.String, fname));
            comm.Parameters.Add(new SQLiteParameter(pMname, DbType.String, mname));
            comm.Parameters.Add(new SQLiteParameter(pLname, DbType.String, lname));
            comm.Parameters.Add(new SQLiteParameter(pBirthday, DbType.Date, birthday));
            comm.Parameters.Add(new SQLiteParameter(pSex, DbType.UInt64, sex));
            comm.Parameters.Add(new SQLiteParameter(pDocID, DbType.UInt64, docID));
            comm.Parameters.Add(new SQLiteParameter(pRegadrID, DbType.UInt64, regadrID));
            comm.Parameters.Add(new SQLiteParameter(pFactadrID, DbType.UInt64, factadrID));
            comm.Parameters.Add(new SQLiteParameter(pBornplaceID, DbType.UInt64, birthplaceID));
            comm.Parameters.Add(new SQLiteParameter(pCitizen1, DbType.UInt64, citizen1));
            comm.Parameters.Add(new SQLiteParameter(pCitizen2, DbType.UInt64, citizen2));
            comm.Parameters.Add(new SQLiteParameter(pState, DbType.UInt32, state));
            comm.CommandText = string.Format(
                            @"SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, 
                            {8}, {9}, {10}, {11}, {12}, {13}
                            FROM {14};",
                            id, socNumber, fname, mname, lname, birthday, sex, docID,
                            regadrID, factadrID, birthplaceID, citizen1, citizen2, state,
                            tablename);
            return comm;
        }

        static public SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pSocNumber, DbType.String, socNumber));
            comm.Parameters.Add(new SQLiteParameter(pFname, DbType.String, fname));
            comm.Parameters.Add(new SQLiteParameter(pMname, DbType.String, mname));
            comm.Parameters.Add(new SQLiteParameter(pLname, DbType.String, lname));
            comm.Parameters.Add(new SQLiteParameter(pBirthday, DbType.String, birthday));
            comm.Parameters.Add(new SQLiteParameter(pSex, DbType.UInt64, sex));
            comm.Parameters.Add(new SQLiteParameter(pDocID, DbType.UInt64, docID));
            comm.Parameters.Add(new SQLiteParameter(pRegadrID, DbType.UInt64, regadrID));
            comm.Parameters.Add(new SQLiteParameter(pFactadrID, DbType.UInt64, factadrID));
            comm.Parameters.Add(new SQLiteParameter(pBornplaceID, DbType.UInt64, birthplaceID));
            comm.Parameters.Add(new SQLiteParameter(pCitizen1, DbType.UInt64, citizen1));
            comm.Parameters.Add(new SQLiteParameter(pCitizen2, DbType.UInt64, citizen2));
            comm.Parameters.Add(new SQLiteParameter(pState, DbType.UInt32, state));
            comm.CommandText = string.Format(
                                @"INSERT INTO {0} 
                                ({1}, {2}, {3}, {4}, {5}, {6}, {7}, 
                                 {8}, {9}, {10}, {11}, {12})
                                VALUES
                                ({13}, {14}, {15}, {16}, {17}, {18}, {19}, 
                                 {20}, {21}, {22}, {23}, {24});
                                SELECT last_insert_rowid();",
                            tablename,
                            socNumber, fname, mname, lname, birthday, sex, docID,
                            regadrID, factadrID, birthplaceID, citizen1, citizen2,
                            pSocNumber, pFname, pMname, pLname, pBirthday, pSex, pDocID,
                            pRegadrID, pFactadrID, pBornplaceID, pCitizen1, pCitizen2);
            return comm;
        }

        static public SQLiteCommand CreateUpdateCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pSocNumber, DbType.String, socNumber));
            comm.Parameters.Add(new SQLiteParameter(pFname, DbType.String, fname));
            comm.Parameters.Add(new SQLiteParameter(pMname, DbType.String, mname));
            comm.Parameters.Add(new SQLiteParameter(pLname, DbType.String, lname));
            comm.Parameters.Add(new SQLiteParameter(pBirthday, DbType.String, birthday));
            comm.Parameters.Add(new SQLiteParameter(pSex, DbType.UInt32, sex));
            comm.Parameters.Add(new SQLiteParameter(pDocID, DbType.UInt64, docID));
            comm.Parameters.Add(new SQLiteParameter(pRegadrID, DbType.UInt64, regadrID));
            comm.Parameters.Add(new SQLiteParameter(pFactadrID, DbType.UInt64, factadrID));
            comm.Parameters.Add(new SQLiteParameter(pBornplaceID, DbType.UInt64, birthplaceID));
            comm.Parameters.Add(new SQLiteParameter(pCitizen1, DbType.UInt64, citizen1));
            comm.Parameters.Add(new SQLiteParameter(pCitizen2, DbType.UInt64, citizen2));
            comm.Parameters.Add(new SQLiteParameter(pState, DbType.UInt32, state));
            comm.CommandText = string.Format(
                            @"UPDATE {0} SET
                             {1} = {2}, {3} = {4}, {5} = {6}, {7} = {8}, {9} = {10}, {11} = {12}, 
                             {13} = {14}, {15} = {16}, {17} = {18}, {19} = {20}, 
                             {21} = {22}, {23} = {24}
                            WHERE {25} = {26};",
                            tablename,
                            socNumber, pSocNumber, fname, pFname, mname, pMname, lname, pLname, birthday, pBirthday, sex, pSex,
                            docID, pDocID, regadrID, pRegadrID, factadrID, pFactadrID, birthplaceID, pBornplaceID,
                            citizen1, pCitizen1, citizen2, pCitizen2,
                            id, pId);
            return comm;
        }

        static public string GetSelectText(long person_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, 
                                {8}, {9}, {10}, {11}, {12}, {13}
                                FROM {14}
                                WHERE {0} = {15};",
                                id, socNumber, fname, mname, lname, birthday, sex, docID,
                                regadrID, factadrID, birthplaceID, citizen1, citizen2, state,
                                tablename,
                                person_id);
        }

        static public string GetSelectIDText(string socnumber)
        {
            return string.Format(" SELECT {0} FROM {1} WHERE {2} = '{3}' ",
                                id, tablename, socNumber, socnumber);
        }

        static public string GetChangeStateText(long person_id, object stateVal)
        {
            return string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4};",
                                    tablename, state, stateVal, id, person_id);
        }

        static public string GetChangeStateText(IEnumerable<long> personidArr, object stateVal)
        {
            string instr = "( ";
            foreach (long val in personidArr)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";

            return string.Format("UPDATE {0} SET {1} = {2} WHERE {3} in {4};",
                                    tablename, state, stateVal, id, instr);
        }

        static public string GetDeleteText(long person_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2};",
                                    tablename, id, person_id);
        }

        static public string GetDeleteText(IEnumerable<long> personidArr)
        {
            string instr = "( ";
            foreach (long val in personidArr)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";
            return string.Format("DELETE FROM {0} WHERE {1} in {2};",
                                    tablename, id, instr);
        }

        static public bool IsExist(string socnumber, string connectionStr)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand(GetSelectIDText(socnumber));
            command.Connection = connection;
            connection.Open();
            object res = command.ExecuteScalar();
            connection.Close();

            return (res != null && res != DBNull.Value);
        }

        static public void Delete(IEnumerable<long> personidArr, string connectionStr)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = new SQLiteConnection(connectionStr);
            command.CommandText = GetDeleteText(personidArr);
            command.Connection.Open();
            int count = command.ExecuteNonQuery();
            command.Connection.Close();
        }

        static public string CorrectSocnumber(string socnumber)
        {
            string rus = "АВСЕНКМРТХ";
            string eng = "ABCEHKMPTX";
            char[] chars = socnumber.ToUpper().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int pos = rus.IndexOf(chars[i]);
                if (pos >= 0)
                    chars[i] = eng[pos];
            }
            return new string(chars);
        }
        #endregion
    }

    public class PersonView
    {
        static public string tablename = "Person_View";

        public enum PersonState { Uvolen = 0, Rabotaet = 1 };

        #region Названия полей в представления БД
        static public string id = "id";
        static public string socNumber = "soc_number";
        static public string fName = "f_name";
        static public string mName = "m_name";
        static public string lName = "l_name";
        static public string fio = "fio";
        static public string birthday = "birthday";
        static public string sex = "sex";
        static public string docType = "doc_type";
        static public string docSeries = "doc_series";
        static public string docNumber = "doc_number";
        static public string docDate = "doc_date";
        static public string docOrg = "doc_org";
        static public string regAdress = "regAddress";
        static public string regAdressZipcode = "regAddress_zipcode";
        static public string factAdress = "factAddress";
        static public string factAdressZipcode = "factAddress_zipcode";
        static public string bornAdress = "bornAddress";
        static public string bornAdressZipcode = "bornAddress_zipcode";
        static public string bornAdressCountry = "bornAddress_country";
        static public string bornAdressArea = "bornAddress_area";
        static public string bornAdressRegion = "bornAddress_region";
        static public string bornAdressCity = "bornAddress_city";
        static public string citizen1 = "citizen1";
        static public string citizen2 = "citizen2";
        static public string citizen1ID = "citizen1_id";
        static public string citizen2ID = "citizen2_id";
        static public string state = "state";
        static public string dismissDate = "dismiss_date";
        static public string orgID = "org_id";
        static public string newDate = "new_date";
        static public string editDate = "edit_date";
        static public string operName = "operator";
        #endregion


        #region Времменные статические переменные
        //static IEnumerable<DataRow> PrintRows;
        #endregion

        #region Статические методы
        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(socNumber, typeof(string));
            table.Columns.Add(fName, typeof(string));
            table.Columns.Add(mName, typeof(string));
            table.Columns.Add(lName, typeof(string));
            table.Columns.Add(fio, typeof(string));
            table.Columns.Add(birthday, typeof(DateTime));
            table.Columns.Add(sex, typeof(int));
            table.Columns.Add(docType, typeof(string));
            table.Columns.Add(docSeries, typeof(string));
            table.Columns.Add(docNumber, typeof(string));
            table.Columns.Add(docDate, typeof(DateTime));
            table.Columns.Add(docOrg, typeof(string));
            table.Columns.Add(regAdress, typeof(string));
            table.Columns.Add(regAdressZipcode, typeof(string));
            table.Columns.Add(factAdress, typeof(string));
            table.Columns.Add(factAdressZipcode, typeof(string));
            table.Columns.Add(bornAdress, typeof(string));
            table.Columns.Add(bornAdressZipcode, typeof(string));
            table.Columns.Add(bornAdressCountry, typeof(string));
            table.Columns.Add(bornAdressRegion, typeof(string));
            table.Columns.Add(bornAdressArea, typeof(string));
            table.Columns.Add(bornAdressCity, typeof(string));
            table.Columns.Add(citizen1, typeof(string));
            table.Columns.Add(citizen2, typeof(string));
            table.Columns.Add(citizen1ID, typeof(long));
            table.Columns.Add(citizen2ID, typeof(long));
            table.Columns.Add(state, typeof(int));
            table.Columns.Add(dismissDate, typeof(DateTime));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(newDate, typeof(DateTime));
            table.Columns.Add(editDate, typeof(DateTime));
            table.Columns.Add(operName, typeof(string));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long org_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", orgID, org_id);
        }

        static public string GetSelectText(long org_id, long person_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} AND {2} = {3}", orgID, org_id, id, person_id);
        }

        static public string GetSelectText(long org_id, int rep_year)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} AND ({2} = 1 or ( {2} = 0 AND ({3} IS NUll OR strftime('%Y',{3}) >= {4}))) ", orgID, org_id, state, dismissDate, rep_year);
        }

        static public void Print(IEnumerable<DataRow> printRows)
        {
            string file = Path.GetFullPath(Properties.Settings.Default.report_adv1);
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Tag = printRows;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Navigate(file);
        }

        static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (sender as WebBrowser);
            if (wb == null)
            {
                return;
            }
            List<string> htmlDivList = new List<string>();
            HtmlDocument htmlDoc = wb.Document;
            IEnumerable<DataRow> PrintRows = wb.Tag as IEnumerable<DataRow>;
            foreach (DataRow personRow in PrintRows)
            {
                string xmlStr = MyXml.Adv1Xml(personRow).InnerXml;
                htmlDoc.InvokeScript("setAllData", new object[] { xmlStr });
                htmlDivList.Add(htmlDoc.Body.InnerHtml);
            }
            if (htmlDivList.Count > 0)
            {
                StringBuilder sb = new StringBuilder(htmlDivList.Count * htmlDivList[0].Length);
                foreach (string div in htmlDivList)
                    sb.Append(div);
                htmlDoc.Body.InnerHtml = sb.ToString();
            }

            MyPrinter.SetPrintSettings();
            wb.ShowPrintPreviewDialog();
        }
        #endregion
    }

    public class PersonShortView
    {
        static public string tablename = "Person_info";

        #region Названия полей
        static public string id = PersonInfo.id;
        static public string socNumber = PersonInfo.socNumber;
        static public string fio = "fio";
        #endregion

        #region
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(socNumber, typeof(string));
            table.Columns.Add(fio, typeof(string));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(@"SELECT {0},{1},(IFNULL({2},'') ||' '|| IFNULL({3},'') ||' '|| IFNULL({4},'')) as {5} FROM {6} ",
                    id, socNumber,
                    PersonInfo.lname, PersonInfo.fname, PersonInfo.mname, fio,
                    tablename);
        }

        static public string GetSelectText(string soc_number)
        {
            return string.Format("{0} WHERE {1} like '%{2}%' ", GetSelectText(), socNumber, soc_number);
        }

        static public string GetSelectText(string soc_number, string fname, string mname, string lname)
        {
            return string.Format("{0} WHERE {1} like '%{2}%' AND {3} like '%{4}%' AND {5} like '%{6}%' AND {7} like '%{8}%'",
                                GetSelectText(),
                                socNumber, soc_number,
                                PersonInfo.fname, fname,
                                PersonInfo.mname, mname,
                                PersonInfo.lname, lname);
        }
        #endregion
    }

    public class PersonView2
    {
        static public string tablename = "Person_View_2";

        public enum PersonState { Uvolen = 0, Rabotaet = 1 };

        #region Названия полей в представления БД
        static public string id = "id";
        static public string socNumber = "soc_number";
        static public string fio = "fio";
        static public string state = "state";
        static public string dismisDate = "dismiss_date";
        static public string orgID = "org_id";
        #endregion

        #region Статические методы
        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(socNumber, typeof(string));
            table.Columns.Add(fio, typeof(string));
            table.Columns.Add(state, typeof(int));
            table.Columns.Add(dismisDate, typeof(DateTime));
            table.Columns.Add(orgID, typeof(long));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT {0},{1},{2},{3},{4},{5} FROM {6}",
                                id, socNumber, fio, state, dismisDate, orgID, tablename);
        }

        static public string GetSelectText(long org_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", orgID, org_id);
        }

        static public string GetSelectText(long org_id, int rep_year)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} AND ({2} = 1 or ( {2} = 0 AND ({3} IS NUll OR {3} >= date('{4}-01-01')))) ", orgID, org_id, state, dismisDate, rep_year);
        }

        static public string GetSelectRawPersonsText(long org_id, int rep_year)
        {
            return GetSelectText() + string.Format(@" WHERE {0} = {1} AND ({2} = {3} or ( {2} = {4} AND ({5} IS NUll OR {5} >= date('{6}-01-01'))))
	AND {7} NOT IN (SELECT d.[{8}] FROM {9} d
	INNER JOIN {10} l ON d.[{11}] = l.[{12}] AND  l.[{13}] = {14} AND l.[{15}] = {6})",
         orgID, org_id, state, (int)PersonState.Rabotaet, (int)PersonState.Uvolen, dismisDate, rep_year,
         id, Docs.personID, Docs.tablename, Lists.tablename, Docs.listId, Lists.id, Lists.orgID, org_id, Lists.repYear);
        }
        #endregion
    }

    public class Country
    {
        // название таблицы в БД
        static public string tablename = "Country";

        #region названия полей таблицы в БД
        static public string id = "id";
        static public string name = "name";
        static public string LAT = "LAT";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(name, typeof(string));
            table.Columns.Add(LAT, typeof(string));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(" SELECT {0},{1},{2} FROM {3} ",
                                            id, name, LAT, tablename);
        }
        #endregion
    }

    public class Adress
    {
        // название таблицы в БД
        static public string tablename = "Adress";

        #region Названя полей таблицы в БД
        static public string id = "id";
        static public string zipCode = "zip_code";
        static public string country = "country";
        static public string area = "area";
        static public string region = "region";
        static public string city = "city";
        static public string street = "street";
        static public string building = "building";
        static public string appartment = "appartment";
        static public string phone = "phone";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pZipCode = "@zip_code";
        static public string pCountry = "@country";
        static public string pArea = "@area";
        static public string pRegion = "@region";
        static public string pCity = "@city";
        static public string pStreet = "@street";
        static public string pBuilding = "@building";
        static public string pAppartment = "@appartment";
        static public string pPhone = "@phone";
        #endregion

        #region Методы
        static public SQLiteCommand CreateSelectCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pZipCode, DbType.String, zipCode));
            comm.Parameters.Add(new SQLiteParameter(pCountry, DbType.String, country));
            comm.Parameters.Add(new SQLiteParameter(pArea, DbType.String, area));
            comm.Parameters.Add(new SQLiteParameter(pRegion, DbType.Date, region));
            comm.Parameters.Add(new SQLiteParameter(pCity, DbType.String, city));
            comm.Parameters.Add(new SQLiteParameter(pStreet, DbType.String, street));
            comm.Parameters.Add(new SQLiteParameter(pBuilding, DbType.String, building));
            comm.Parameters.Add(new SQLiteParameter(pAppartment, DbType.String, appartment));
            comm.Parameters.Add(new SQLiteParameter(pPhone, DbType.String, phone));
            comm.CommandText = string.Format(
                        @"SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} FROM {10};",
                        id, zipCode, country, area, region, city, street, building, appartment, phone, tablename);
            return comm;
        }

        static public SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pZipCode, DbType.String, zipCode));
            comm.Parameters.Add(new SQLiteParameter(pCountry, DbType.String, country));
            comm.Parameters.Add(new SQLiteParameter(pArea, DbType.String, area));
            comm.Parameters.Add(new SQLiteParameter(pRegion, DbType.String, region));
            comm.Parameters.Add(new SQLiteParameter(pCity, DbType.String, city));
            comm.Parameters.Add(new SQLiteParameter(pStreet, DbType.String, street));
            comm.Parameters.Add(new SQLiteParameter(pBuilding, DbType.String, building));
            comm.Parameters.Add(new SQLiteParameter(pAppartment, DbType.String, appartment));
            comm.Parameters.Add(new SQLiteParameter(pPhone, DbType.String, phone));
            comm.CommandText = string.Format(
                        @"INSERT INTO {0} 
                        ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})
                        VALUES
                        ({10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18});
                        SELECT last_insert_rowid();",
                        tablename,
                        zipCode, country, area, region, city, street, building, appartment, phone,
                        pZipCode, pCountry, pArea, pRegion, pCity, pStreet, pBuilding, pAppartment, pPhone);
            return comm;
        }

        static public SQLiteCommand CreateUpdateCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pZipCode, DbType.String, zipCode));
            comm.Parameters.Add(new SQLiteParameter(pCountry, DbType.String, country));
            comm.Parameters.Add(new SQLiteParameter(pArea, DbType.String, area));
            comm.Parameters.Add(new SQLiteParameter(pRegion, DbType.String, region));
            comm.Parameters.Add(new SQLiteParameter(pCity, DbType.String, city));
            comm.Parameters.Add(new SQLiteParameter(pStreet, DbType.String, street));
            comm.Parameters.Add(new SQLiteParameter(pBuilding, DbType.String, building));
            comm.Parameters.Add(new SQLiteParameter(pAppartment, DbType.String, appartment));
            comm.Parameters.Add(new SQLiteParameter(pPhone, DbType.String, phone));
            comm.CommandText = string.Format(
                        @"UPDATE {0} SET
                        {1} = {2}, {3} = {4}, {5} = {6}, {7} = {8}, {9} = {10}, 
                        {11} = {12}, {13} = {14}, {15} = {16}, {17} = {18}
                        WHERE {19} = {20};",
                        tablename,
                        zipCode, pZipCode, country, pCountry, area, pArea, region, pRegion, city, pCity,
                        street, pStreet, building, pBuilding, appartment, pAppartment, phone, pPhone,
                        id, pId);
            return comm;
        }

        static public string GetSelectCommandText(long adress_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} FROM {10} WHERE {0} = {11};",
                                id, zipCode, country, area, region, city, street, building, appartment, phone, tablename, adress_id);
        }
        #endregion
    }

    public class IDocInfo
    {
        // название таблицы в БД
        static public string tablename = "IDoc_info";

        #region Название полей таблицы в БД
        static public string id = "id";
        static public string docTypeID = "doc_type_id";
        static public string series = "series";
        static public string number = "number";
        static public string date = "date";
        static public string org = "org";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pDocTypeID = "@doc_type_id";
        static public string pSeries = "@series";
        static public string pNumber = "@number";
        static public string pDate = "@date";
        static public string pOrg = "@org";
        #endregion

        #region Методы
        static public SQLiteCommand CreateSelectCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pDocTypeID, DbType.UInt64, docTypeID));
            comm.Parameters.Add(new SQLiteParameter(pSeries, DbType.String, series));
            comm.Parameters.Add(new SQLiteParameter(pNumber, DbType.String, number));
            comm.Parameters.Add(new SQLiteParameter(pDate, DbType.Date, date));
            comm.Parameters.Add(new SQLiteParameter(pOrg, DbType.String, org));
            comm.CommandText = string.Format(
                            @"SELECT {0}, {1}, {2}, {3}, [{4}], {5} FROM {6};",
                            id, docTypeID, series, number, date, org, tablename);
            return comm;
        }

        static public SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pDocTypeID, DbType.UInt64, docTypeID));
            comm.Parameters.Add(new SQLiteParameter(pSeries, DbType.String, series));
            comm.Parameters.Add(new SQLiteParameter(pNumber, DbType.String, number));
            comm.Parameters.Add(new SQLiteParameter(pDate, DbType.String, date));
            comm.Parameters.Add(new SQLiteParameter(pOrg, DbType.String, org));
            comm.CommandText = string.Format(@"
                                INSERT INTO [{0}] 
                                ({1}, {2}, {3}, [{4}], {5})
                                VALUES
                                ({6}, {7}, {8}, {9}, {10});
                                SELECT last_insert_rowid();",
                            tablename,
                            docTypeID, series, number, date, org,
                            pDocTypeID, pSeries, pNumber, pDate, pOrg);
            return comm;
        }

        static public SQLiteCommand CreateUpdateCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pDocTypeID, DbType.UInt64, docTypeID));
            comm.Parameters.Add(new SQLiteParameter(pSeries, DbType.String, series));
            comm.Parameters.Add(new SQLiteParameter(pNumber, DbType.String, number));
            comm.Parameters.Add(new SQLiteParameter(pDate, DbType.String, date));
            comm.Parameters.Add(new SQLiteParameter(pOrg, DbType.String, org));
            comm.CommandText = string.Format(@"UPDATE {0} SET 
                                            {1} = {2}, {3} = {4}, {5} = {6}, [{7}] = {8}, {9} = {10}
                                            WHERE ({11} = {12});",
                                        tablename,
                                        docTypeID, pDocTypeID, series, pSeries, number, pNumber, date, pDate, org, pOrg,
                                        id, pId);
            return comm;
        }

        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docTypeID, typeof(long));
            table.Columns.Add(series, typeof(string));
            table.Columns.Add(number, typeof(string));
            table.Columns.Add(date, typeof(DateTime));
            table.Columns.Add(org, typeof(string));
            return table;
        }

        static public string GetSelectCommandText(long idoc_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, [{4}], {5} FROM {6} WHERE {0} = {7};",
                            id, docTypeID, series, number, date, org, tablename, idoc_id);
        }
        #endregion
    }

    public class IDocType
    {
        // название таблицы в БД
        static public string tablename = "IDoc_type";

        #region названия полей таблицы в БД
        static public string id = "id";
        static public string name = "name";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(name, typeof(string));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT {0},{1} FROM {2} ",
                                        id, name, tablename);
        }
        #endregion
    }

    public class Classgroup
    {
        // название таблицы в БД
        static public string tablename = "Classgroup";

        #region Название полей таблицы в БД
        static public string id = "id";
        static public string name = "name";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(name, typeof(string));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(" SELECT {0},{1} FROM {2} ", id, name, tablename);
        }
        #endregion
    }

    public class Classificator
    {
        // название таблицы в БД
        static public string tablename = "Classificator";

        #region Название полей таблицы в БД
        static public string id = "id";
        static public string classgroupID = "classgroup_id";
        static public string spisok = "spisok";
        static public string code = "code";
        static public string name = "name";
        static public string description = "description";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(classgroupID, typeof(long));
            table.Columns.Add(spisok, typeof(long));
            table.Columns.Add(code, typeof(string));
            table.Columns.Add(name, typeof(string));
            table.Columns.Add(description, typeof(string));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(@" SELECT {0},{1},{2},{3},{4},{5} FROM {6} ",
                                id, classgroupID, spisok, code, name, description, tablename);
        }
        #endregion
    }

    public class Classpercent
    {
        // название таблицы в БД
        static public string tablename = "Classpercent";

        #region Название полей таблицы в БД
        static public string id = "id";
        static public string classificatorID = "classificator_id";
        static public string privilegeID = "privilege_id";
        static public string privilegeName = "privilege_name";
        static public string value = "value";
        static public string dateBegin = "date_begin";
        static public string dateEnd = "date_end";
        static public string obligatoryIsEnabled = "obligatory_is_enabled";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(classificatorID, typeof(long));
            table.Columns.Add(privilegeID, typeof(long));
            table.Columns.Add(value, typeof(double));
            table.Columns.Add(dateBegin, typeof(string));
            table.Columns.Add(dateEnd, typeof(string));
            table.Columns.Add(obligatoryIsEnabled, typeof(string));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(@" SELECT {7}.{0} as {0},{1},{2}, {8}.{10} as {3}, round(CAST(REPLACE({4},',','.') AS real) * 100 , 2) as {4},{5},{6} FROM {7} LEFT JOIN {8} ON {7}.{2} = {8}.{9} ",
                                id, classificatorID, privilegeID, privilegeName, value, dateBegin, dateEnd, tablename, Privilege.tablename, Privilege.id, Privilege.name);
        }
        #endregion
    }

    public class ClasspercentView
    {
        // название таблицы в БД
        static public string tablename = "Classpercent_View";

        #region Название полей таблицы в БД
        static public string id = "id";
        static public string classificatorID = "classificator_id";
        static public string code = "code";
        static public string name = "name";
        static public string description = "description";
        static public string privilegeID = "privilege_id";
        static public string privilegeName = "privilege_name";
        static public string value = "value";
        static public string dateBegin = "date_begin";
        static public string dateEnd = "date_end";
        static public string obligatoryIsEnabled = "obligatory_is_enabled";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(classificatorID, typeof(long));
            table.Columns.Add(code, typeof(string));
            table.Columns.Add(name, typeof(string));
            table.Columns.Add(description, typeof(string));
            table.Columns.Add(privilegeID, typeof(long));
            table.Columns.Add(privilegeName, typeof(string));
            table.Columns.Add(value, typeof(double));
            table.Columns.Add(dateBegin, typeof(DateTime));
            table.Columns.Add(dateEnd, typeof(DateTime));
            table.Columns.Add(obligatoryIsEnabled, typeof(string));

            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(@" SELECT * FROM {0}", tablename);
        }

        static public string GetSelectClassgroupOneText(DateTime now)
        {
            string commandStr = string.Format(@"SELECT *  FROM {0} WHERE (strftime('%s',ifnull({1},'1990-01-01')) - strftime('%s','{2}') <= 0) 
                AND (strftime('%s',ifnull({3},'2999-01-01')) - strftime('%s','{2}') >= 0)
                AND {4} >=100 AND {4} < 200", tablename, dateBegin, now.ToString("yyyy-MM-dd"), dateEnd, classificatorID);

            return commandStr;
        }

        static public string GetSelectClassgroupTwoText(DateTime now)
        {
            string commandStr = string.Format(@"SELECT *  FROM {0} WHERE (strftime('%s',ifnull({1},'1990-01-01')) - strftime('%s','{2}') <= 0) 
                AND (strftime('%s',ifnull({3},'2999-01-01')) - strftime('%s','{2}') >= 0)
                AND {4} >=200 AND {4} < 300", tablename, dateBegin, now.ToString("yyyy-MM-dd"), dateEnd, classificatorID);

            return commandStr;
        }

        static public string GetSelectClassgroupThreeText(DateTime now)
        {
            string commandStr = string.Format(@"SELECT *  FROM {0} WHERE (strftime('%s',ifnull({1},'1990-01-01')) - strftime('%s','{2}') <= 0) 
                AND (strftime('%s',ifnull({3},'2999-01-01')) - strftime('%s','{2}') >= 0)
                AND {4} >=300 AND {4} < 400", tablename, dateBegin, now.ToString("yyyy-MM-dd"), dateEnd, classificatorID);

            return commandStr;
        }

        static public string GetSelectClassgroupFourText(DateTime now)
        {
            string commandStr = string.Format(@"SELECT *  FROM {0} WHERE (strftime('%s',ifnull({1},'1990-01-01')) - strftime('%s','{2}') <= 0) 
                AND (strftime('%s',ifnull({3},'2999-01-01')) - strftime('%s','{2}') >= 0)
                AND {4} >=400 AND {4} < 500", tablename, dateBegin, now.ToString("yyyy-MM-dd"), dateEnd, classificatorID);

            return commandStr;
        }

        static public string GetSelectClassgroupFiveText(DateTime now)
        {
            string commandStr = string.Format(@"SELECT *  FROM {0} WHERE (strftime('%s',ifnull({1},'1990-01-01')) - strftime('%s','{2}') <= 0) 
                AND (strftime('%s',ifnull({3},'2999-01-01')) - strftime('%s','{2}') >= 0)
                AND {4} >=500 AND {4} < 600", tablename, dateBegin, now.ToString("yyyy-MM-dd"), dateEnd, classificatorID);

            return commandStr;
        }

        static public string GetBindingSourceFilterFor100(DateTime now)
        {
            return string.Format("{0}<='{2}' AND ({1} >= '{2}' OR {1} IS NULL) AND {3} >=100 AND {3} < 200", ClasspercentView.dateBegin, ClasspercentView.dateEnd, now.ToString("yyyy-MM-dd"), ClasspercentView.classificatorID);
        }

        static public string GetBindingSourceFilterFor200(DateTime now)
        {
            return string.Format("{0}<='{2}' AND ({1} >= '{2}' OR {1} IS NULL) AND {3} >=200 AND {3} < 300", ClasspercentView.dateBegin, ClasspercentView.dateEnd, now.ToString("yyyy-MM-dd"), ClasspercentView.classificatorID);
        }

        static public string GetBindingSourceFilterFor300(DateTime now)
        {
            return string.Format("{0}<='{2}' AND ({1} >= '{2}' OR {1} IS NULL) AND {3} >=300 AND {3} < 400", ClasspercentView.dateBegin, ClasspercentView.dateEnd, now.ToString("yyyy-MM-dd"), ClasspercentView.classificatorID);
        }

        static public string GetBindingSourceFilterFor400(DateTime now)
        {
            return string.Format("{0}<='{2}' AND ({1} >= '{2}' OR {1} IS NULL) AND {3} >=400 AND {3} < 500", ClasspercentView.dateBegin, ClasspercentView.dateEnd, now.ToString("yyyy-MM-dd"), ClasspercentView.classificatorID);
        }

        static public string GetBindingSourceFilterFor500(DateTime now)
        {
            return string.Format("{0}<='{2}' AND ({1} >= '{2}' OR {1} IS NULL) AND {3} >=500 AND {3} < 600", ClasspercentView.dateBegin, ClasspercentView.dateEnd, now.ToString("yyyy-MM-dd"), ClasspercentView.classificatorID);
        }

        #endregion
    }

    public class Privilege
    {
        // название таблицы в БД
        static public string tablename = "Privilege";

        #region Названия полей таблицы в БД
        static public string id = "id";
        static public string name = "name";
        #endregion

        #region Методы - статические
        static public string GetSelectText()
        {
            return string.Format(" SELECT {0},{1} FROM {2} ", id, name, tablename);
        }
        #endregion
    }

    public class ObligatoryPercent
    {
        // название таблицы в БД
        static public string tablename = "Obligatory_Percent";

        #region Названия полей таблицы в БД
        static public string id = "id";
        static public string value = "value";
        static public string dateBegin = "date_begin";
        static public string dateEnd = "date_end";
        #endregion

        #region Методы - статические
        static public string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        static public double GetValue(int rep_year, string connectionStr)
        {
            double val = 0;
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;
            command.CommandText = GetSelectText() + string.Format(@"WHERE (strftime('%s',ifnull({0},'1990-01-01')) - strftime('%s','{2}-01-01') <= 0) 
		AND (strftime('%s',ifnull({1},'now')) - strftime('%s','{2}-01-01') >= 0);", dateBegin, dateEnd, rep_year);
            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                val = Double.Parse(reader[value].ToString());
            }
            reader.Close();
            connection.Close();
            return val;
        }
        #endregion
    }

    public class ListsView
    {
        static public string tablename = "Lists_View";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string listTypeId = "list_type_id";
        static public string nameType = "name";
        static public string orgID = "org_id";
        static public string operatorIdReg = "oper_id_reg";
        static public string operatorNameReg = "name_reg";
        static public string regDate = "reg_date";
        static public string operatorIdChange = "oper_id_change";
        static public string operatorNameChange = "name_change";
        static public string changeDate = "change_date";
        static public string repYear = "rep_year";
        #endregion

        #region Методы - статические
        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(listTypeId, typeof(int));
            table.Columns.Add(nameType, typeof(string));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(operatorIdReg, typeof(long));
            table.Columns.Add(operatorNameReg, typeof(string));
            table.Columns.Add(regDate, typeof(DateTime));
            table.Columns.Add(operatorIdChange, typeof(long));
            table.Columns.Add(operatorNameChange, typeof(string));
            table.Columns.Add(changeDate, typeof(DateTime));
            table.Columns.Add(repYear, typeof(int));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10} FROM {11} ",
                                id, listTypeId, nameType, orgID, operatorIdReg, operatorNameReg, regDate, operatorIdChange, operatorNameChange, changeDate, repYear, tablename);
        }

        static public string GetSelectText(long org_id, int rep_year)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} AND {2} = {3}", orgID, org_id, repYear, rep_year);
        }
        #endregion
    }

    public class Lists
    {
        static public string tablename = "Lists";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string listTypeId = "list_type_id";
        static public string orgID = "org_id";
        static public string operatorIdReg = "oper_id_reg";
        static public string regDate = "reg_date";
        static public string operatorIdChange = "oper_id_change";
        static public string changeDate = "change_date";
        static public string repYear = "rep_year";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pListTypeId = "@list_type_id";
        static public string pOrgID = "@org_id";
        static public string pOperatorIdReg = "@oper_id_reg";
        static public string pRegDate = "@reg_date";
        static public string pOperatorIdChange = "@oper_id_change";
        static public string pChangeDate = "@change_date";
        static public string pRepYear = "@rep_year";
        #endregion

        #region Методы - статические
        static public SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            //comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pListTypeId, DbType.Int32, listTypeId));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.UInt64, orgID));
            comm.Parameters.Add(new SQLiteParameter(pOperatorIdReg, DbType.UInt64, operatorIdReg));
            comm.Parameters.Add(new SQLiteParameter(pRegDate, DbType.String, regDate));
            comm.Parameters.Add(new SQLiteParameter(pOperatorIdChange, DbType.UInt64, operatorIdChange));
            comm.Parameters.Add(new SQLiteParameter(pChangeDate, DbType.String, changeDate));
            comm.Parameters.Add(new SQLiteParameter(pRepYear, DbType.Int32, repYear));
            comm.CommandText = string.Format(@" INSERT INTO {0}
                                                ({1}, {2}, {3}, {4}, {5}, {6}, {7})
                                                VALUES
                                                ({6}, {7}, {8}, {9}, {10}, {11}, {12});
                                                SELECT last_indert_rowid()",
                                        tablename,
                                        listTypeId, orgID, operatorIdReg, regDate, operatorIdChange, changeDate, repYear,
                                        pListTypeId, pOrgID, pOperatorIdReg, pRegDate, pOperatorIdChange, pChangeDate, pRepYear);
            return comm;
        }

        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(listTypeId, typeof(int));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(operatorIdReg, typeof(long));
            table.Columns.Add(regDate, typeof(DateTime));
            table.Columns.Add(operatorIdChange, typeof(long));
            table.Columns.Add(changeDate, typeof(DateTime));
            table.Columns.Add(repYear, typeof(int));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT {0},{1},{2},{3},{4},{5},{6},{7} FROM {8}",
                                id, listTypeId, orgID, operatorIdReg, regDate, operatorIdChange, changeDate, repYear, tablename);
        }

        static public string GetSelectText(long org_id, int rep_year)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} AND {2} = {3}", orgID, org_id, repYear, rep_year);
        }
        /// <summary>
        /// Метод возвращет текст SQL запроса, который применяется для вставки в таблицу Lists
        /// </summary>
        /// <param name="list_type_id">Тип пакета: 1 - Индивидуальные сведения; 2 - Регистрационные данные. Правильные значения в таблице List_Types</param>
        /// <param name="org_id"></param>
        /// <param name="operator_id_reg"></param>
        /// <param name="reg_date"></param>
        /// <param name="operator_id_change"></param>
        /// <param name="change_date"></param>
        /// <param name="rep_year"></param>
        /// <returns></returns>
        static public string GetInsertText(long list_type_id, long org_id, long operator_id_reg, string reg_date, long operator_id_change, string change_date, int rep_year)
        {
            DateTime result;
            if (!DateTime.TryParse(reg_date, out result))
            {
                throw new ArgumentException("Not valid date string.", "reg_date");
            }

            if (!DateTime.TryParse(change_date, out result))
            {
                throw new ArgumentException("Not valid date string.", "change_date");
            }

            return string.Format("INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7}) VALUES ({8},{9},{10},'{11}',{12},'{13}',{14})", tablename, listTypeId, orgID, operatorIdReg, regDate, operatorIdChange, changeDate, repYear,
                list_type_id, org_id, operator_id_reg, reg_date, operator_id_change, change_date, rep_year);
        }

        static public string GetDeleteText(long list_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, list_id);
        }

        static public string GetUpdateYearText(long list_id, int new_rep_year)
        {
            return string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}", tablename, repYear, new_rep_year, id, list_id);
        }

        static public string GetUpdateOrgText(long list_id, long new_org_id)
        {
            return string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}", tablename, orgID, new_org_id, id, list_id);
        }

        static public string GetSelectPersonIdsText(long list_id)
        {
            return string.Format("SELECT person_id FROM Docs WHERE list_id = {0}", list_id);
        }
        #endregion
    }

    public class DocsView
    {
        static public string tablename = "Docs_View";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string docTypeId = "doc_type_id";
        static public string nameType = "name";
        static public string listId = "list_id";
        static public string personID = "person_id";
        static public string socNumber = "soc_number";
        static public string fio = "fio";
        static public string operIdReg = "oper_id_reg";
        static public string operNameReg = "name_reg";
        static public string regDate = "reg_date";
        static public string operIdChange = "oper_id_change";
        static public string operNameChange = "name_change";
        static public string changeDate = "change_date";
        #endregion

        #region Методы - статические
        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docTypeId, typeof(int));
            table.Columns.Add(nameType, typeof(string));
            table.Columns.Add(listId, typeof(long));
            table.Columns.Add(personID, typeof(long));
            table.Columns.Add(socNumber, typeof(string));
            table.Columns.Add(fio, typeof(string));
            table.Columns.Add(operIdReg, typeof(long));
            table.Columns.Add(operNameReg, typeof(string));
            table.Columns.Add(regDate, typeof(DateTime));
            table.Columns.Add(operIdChange, typeof(long));
            table.Columns.Add(operNameChange, typeof(string));
            table.Columns.Add(changeDate, typeof(DateTime));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectTextByListId(long list_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", listId, list_id);
        }

        static public string GetSelectTextByDocId(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", id, doc_id);
        }
        #endregion
    }

    public class Docs
    {
        static public string tablename = "Docs";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string docTypeId = "doc_type_id";
        static public string listId = "list_id";
        static public string personID = "person_id";
        static public string operIdReg = "oper_id_reg";
        static public string regDate = "reg_date";
        static public string operIdChange = "oper_id_change";
        static public string changeDate = "change_date";
        #endregion

        #region Методы - статические
        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docTypeId, typeof(int));
            table.Columns.Add(listId, typeof(long));
            table.Columns.Add(personID, typeof(long));
            table.Columns.Add(operIdReg, typeof(long));
            table.Columns.Add(regDate, typeof(DateTime));
            table.Columns.Add(operIdChange, typeof(long));
            table.Columns.Add(changeDate, typeof(DateTime));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long list_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", listId, list_id);
        }

        static public string GetUpdateDocTypeByDocIdText(long doc_id, long new_doc_type_id)
        {
            return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
                                tablename, docTypeId, new_doc_type_id, id, doc_id);
        }

        static public string GetUpdateDocTypeByListText(long list_id, long new_doc_type_id)
        {
            return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
                                tablename, docTypeId, new_doc_type_id, listId, list_id);
        }

        static public void UpdateDocTypeByDocId(List<long> doc_idArr, long new_doc_type_id, string connectionStr)
        {
            if (doc_idArr.Count < 1)
                throw new ArgumentException("Количество документов на изменение должно быть >= 1");
            string commantText = String.Empty;
            foreach (long doc_id in doc_idArr)
                commantText += GetUpdateDocTypeByDocIdText(doc_id, new_doc_type_id) + "; \n";

            using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                SQLiteTransaction trans = connection.BeginTransaction();
                SQLiteCommand command = new SQLiteCommand(commantText, connection, trans);
                int count = command.ExecuteNonQuery();
                trans.Commit();
            }
        }

        static public void UpdateDocTypeByListId(long list_id, long new_doc_type_id, string connectionStr)
        {
            string commantText = GetUpdateDocTypeByListText(list_id, new_doc_type_id);

            using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                SQLiteTransaction trans = connection.BeginTransaction();
                SQLiteCommand command = new SQLiteCommand(commantText, connection, trans);
                int count = command.ExecuteNonQuery();
                trans.Commit();
            }
        }

        static public string GetDeleteText(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, doc_id);
        }

        static public string GetUpdateListIdText(long doc_id, long new_list_id)
        {
            return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
                                tablename, listId, new_list_id, id, doc_id);
        }

        static public int UpdateListId(long doc_id, long new_list_id, string connectionStr)
        {
            string commantText = GetUpdateListIdText(doc_id, new_list_id);
            int count = 0;
            using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                SQLiteTransaction trans = connection.BeginTransaction();
                SQLiteCommand command = new SQLiteCommand(commantText, connection, trans);
                count = command.ExecuteNonQuery();
                trans.Commit();
                connection.Close();
            }
            return count;
        }
        #endregion
    }

    public class DocTypes
    {
        static public string tablename = "Doc_Types";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string listTypeId = "list_type_id";
        static public string name = "name";
        #endregion

        #region Методы - статические
        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(listTypeId, typeof(long));
            table.Columns.Add(name, typeof(string));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long list_type_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", listTypeId, list_type_id);
        }


        #endregion
    }

    public class SalaryInfo
    {
        static public string tablename = "Salary_Info";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string docId = "doc_id";
        static public string salaryGroupsId = "salary_groups_id";
        static public string january = "january";
        static public string february = "february";
        static public string march = "march";
        static public string april = "april";
        static public string may = "may";
        static public string june = "june";
        static public string july = "july";
        static public string august = "august";
        static public string september = "september";
        static public string october = "october";
        static public string november = "november";
        static public string december = "december";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docId, typeof(long));
            table.Columns.Add(salaryGroupsId, typeof(long));
            table.Columns.Add(january, typeof(double));
            table.Columns.Add(february, typeof(double));
            table.Columns.Add(march, typeof(double));
            table.Columns.Add(april, typeof(double));
            table.Columns.Add(may, typeof(double));
            table.Columns.Add(june, typeof(double));
            table.Columns.Add(july, typeof(double));
            table.Columns.Add(august, typeof(double));
            table.Columns.Add(september, typeof(double));
            table.Columns.Add(october, typeof(double));
            table.Columns.Add(november, typeof(double));
            table.Columns.Add(december, typeof(double));
            return table;
        }

        static public DataTable CreateTransposeTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add("months", typeof(int));
            table.Columns.Add("1", typeof(double));
            table.Columns.Add("2", typeof(double));
            table.Columns.Add("3", typeof(double));
            table.Columns.Add("4", typeof(double));
            table.Columns.Add("5", typeof(double));
            table.Columns.Add("10", typeof(int));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ORDER BY {2}", docId, doc_id, salaryGroupsId);
        }

        public static DataTable TransposeDataTable(DataTable dtTableToTranspose, Int32 columnIndex)
        {
            DataTable dtTransposedTable = new DataTable(tablename);

            //String colName = dtTableToTranspose.Columns[columnIndex].ColumnName.ToString();
            dtTransposedTable.Columns.Add("months");

            foreach (DataRow row in dtTableToTranspose.Rows)
            {
                dtTransposedTable.Columns.Add(row[columnIndex].ToString());
            }

            Int32 colIndex = 0;
            Int32 month = 1;
            foreach (DataColumn dc in dtTableToTranspose.Columns)
            {
                if (colIndex != columnIndex)
                {
                    DataRow newRow = dtTransposedTable.NewRow();
                    newRow[0] = month;
                    month++;
                    for (Int32 destColIndex = 1; destColIndex < dtTransposedTable.Columns.Count; destColIndex++)
                    {
                        newRow[destColIndex] = dtTableToTranspose.Rows[destColIndex - 1][colIndex];
                    }

                    dtTransposedTable.Rows.Add(newRow);
                }
                colIndex++;
            }
            return dtTransposedTable;
        }

        //static public string GetUpdateDocTypeByDocIdText(long doc_id, long new_doc_type_id)
        //{
        //    return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
        //                        tablename, docTypeId, new_doc_type_id, id, doc_id);
        //}

        //static public string GetUpdateDocTypeByListText(long list_id, long new_doc_type_id)
        //{
        //    return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
        //                        tablename, docTypeId, new_doc_type_id, listId, list_id);
        //}

        //static public void UpdateDocTypeByDocId(List<long> doc_idArr, long new_doc_type_id, string connectionStr)
        //{
        //    if (doc_idArr.Count < 1)
        //        throw new ArgumentException("Количество документов на изменение должно быть >= 1");
        //    string commantText = String.Empty;
        //    foreach (long doc_id in doc_idArr)
        //        commantText += GetUpdateDocTypeByDocIdText(doc_id, new_doc_type_id) + "; \n";

        //    using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
        //    {
        //        if (connection.State != ConnectionState.Open)
        //            connection.Open();
        //        SQLiteTransaction trans = connection.BeginTransaction();
        //        SQLiteCommand command = new SQLiteCommand(commantText, connection, trans);
        //        int count = command.ExecuteNonQuery();
        //        trans.Commit();
        //    }
        //}

        //static public void UpdateDocTypeByListId(long list_id, long new_doc_type_id, string connectionStr)
        //{
        //    string commantText = GetUpdateDocTypeByListText(list_id, new_doc_type_id);

        //    using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
        //    {
        //        if (connection.State != ConnectionState.Open)
        //            connection.Open();
        //        SQLiteTransaction trans = connection.BeginTransaction();
        //        SQLiteCommand command = new SQLiteCommand(commantText, connection, trans);
        //        int count = command.ExecuteNonQuery();
        //        trans.Commit();
        //    }
        //}

        //static public string GetDeleteText(long doc_id)
        //{
        //    return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, doc_id);
        //}

        //static public string GetUpdateListIdText(long doc_id, long new_list_id)
        //{
        //    return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
        //                        tablename, listId, new_list_id, id, doc_id);
        //}

        //static public int UpdateListId(long doc_id, long new_list_id, string connectionStr)
        //{
        //    string commantText = GetUpdateListIdText(doc_id, new_list_id);
        //    int count = 0;
        //    using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
        //    {
        //        if (connection.State != ConnectionState.Open)
        //            connection.Open();
        //        SQLiteTransaction trans = connection.BeginTransaction();
        //        SQLiteCommand command = new SQLiteCommand(commantText, connection, trans);
        //        count = command.ExecuteNonQuery();
        //        trans.Commit();
        //        connection.Close();
        //    }
        //    return count;
        //}
        #endregion
    }

    public class Mergies
    {
        static public string tablename = "Mergies";

        #region Название полей таблицы в БД
        static public string id = "id";
        static public string orgID = "org_id";
        static public string repYear = "rep_year";
        static public string listCount = "list_count";
        static public string docCount = "doc_count";
        static public string actual = "actual";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(repYear, typeof(long));
            table.Columns.Add(listCount, typeof(long));
            table.Columns.Add(docCount, typeof(long));
            table.Columns.Add(actual, typeof(bool));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectRowText(long row_id)
        {
            return string.Format("{0} WHERE {1} = {2} ", GetSelectText(), id, row_id);
        }

        static public string GetSelectText(long org_id)
        {
            return string.Format("{0} WHERE {1} = {2} ", GetSelectText(), orgID, org_id);
        }

        static public string GetSelectActualText(long org_id)
        {
            return string.Format("{0} WHERE {1} = {2} AND {3} = 1 ", GetSelectText(), orgID, org_id, actual);
        }

        static public string GetInsertText(long org_id, int rep_year, int list_count, int doc_count)
        {
            return string.Format(" INSERT INTO {0} ({1},{2},{3},{4})VALUES({5},{6},{7},{8}); SELECT last_insert_rowid(); ", 
                                    orgID, repYear, listCount, docCount,
                                    org_id, rep_year, list_count, doc_count);
        }
        
        static public string GetUpdateText(long row_id, long org_id, int rep_year, int list_count, int doc_count)
        {
            return string.Format(" UPDATE {0} SET {1}={2},{3}={4},{5}={6},{7}={8} WHERE {9}={10} ",
                                    tablename,
                                    orgID, org_id, 
                                    repYear, rep_year, 
                                    listCount, list_count, 
                                    docCount, doc_count, 
                                    id, row_id);
        }

        static public string GetChangeActualText(long row_id, bool actual_value)
        {
            return string.Format("UPDATE {0} SET {1}={2} WHERE {3}={4}", 
                                    tablename, actual, actual_value, id, row_id);
        }

        static public string GetChangeActualText(IEnumerable<long> row_id, bool actual_value)
        {
            string instr = "( ";
            foreach (long val in row_id)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";

            return string.Format("UPDATE {0} SET {1}={2} WHERE {3} in {4}",
                                    tablename, actual, actual_value, id, instr);
        }

        static public string GetChangeActualByOrgText(long org_id, int rep_year, bool actual_value)
        {
            return string.Format("UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} ",
                                    tablename, actual, actual_value, orgID, org_id, repYear, rep_year);
        }

        static public string GetDeleteText(long row_id)
        {
            return string.Format(" DELETE FROM {0} WHERE {1}={2} ", tablename, id, row_id);
        }

        static public string GetDeleteByOrgText(long org_id)
        {
            return string.Format(" DELETE FROM {0} WHERE {1}={2} ", tablename, orgID, org_id);
        }

        static public string GetDeleteByOrgText(long org_id, int rep_year)
        {
            return string.Format(" DELETE FROM {0} WHERE {1}={2} AND {3}={4} ", tablename, orgID, org_id, repYear, rep_year);
        }
        #endregion
    }

    public class MergiesView: Mergies
    {
        // название представления в БД
        new static public string tablename = "Mergies_View";

        #region Дополненные поля для представления
        static public string operName = "operator";
        static public string newDate = "new_date";
        static public string editDate = "edit_date";
        #endregion
        
        new static public string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }
        
        new static public string GetSelectRowText(long row_id)
        {
            return string.Format("{0} WHERE {1}={2} ", GetSelectText(), id, row_id);
        }

        new static public string GetSelectText(long org_id)
        {
            return string.Format("{0} WHERE {1}={2} ", GetSelectText(), orgID, org_id);
        }

        static public string GetSelectText(long org_id, int rep_year)
        {
            return string.Format("{0} WHERE {1}={2} AND {3}={4} ", GetSelectText(), orgID, org_id, repYear, rep_year);
        }

        new static public string GetSelectActualText(long org_id)
        {
            return string.Format("{0} WHERE {1} = {2} AND {3} = 1 ", GetSelectText(), orgID, org_id, actual);
        }
        
        new static public DataTable CreateTable()
        {
            DataTable table = Mergies.CreateTable();
            table.Columns.Add(newDate, typeof(DateTime));
            table.Columns.Add(editDate, typeof(DateTime));
            table.Columns.Add(operName, typeof(string));
            return table;
        }
    }

    public class MergeInfo
    {
        static public string tablename = "Merge_Info";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string mergeID = "merge_id";
        static public string groupID = "groups_id";
        static public string january = "january";
        static public string february = "february";
        static public string march = "march";
        static public string april = "april";
        static public string may = "may";
        static public string june = "june";
        static public string july = "july";
        static public string august = "august";
        static public string september = "september";
        static public string october = "october";
        static public string november = "november";
        static public string december = "december";
        static public string sum = "sum";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(mergeID, typeof(long));
            table.Columns.Add(groupID, typeof(long));
            table.Columns.Add(january, typeof(double));
            table.Columns.Add(february, typeof(double));
            table.Columns.Add(march, typeof(double));
            table.Columns.Add(april, typeof(double));
            table.Columns.Add(may, typeof(double));
            table.Columns.Add(june, typeof(double));
            table.Columns.Add(july, typeof(double));
            table.Columns.Add(august, typeof(double));
            table.Columns.Add(september, typeof(double));
            table.Columns.Add(october, typeof(double));
            table.Columns.Add(november, typeof(double));
            table.Columns.Add(december, typeof(double));
            table.Columns.Add(sum, typeof(double));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long merge_id)
        {
            return string.Format("{0} WHERE {1} = {2} ORDER BY {3}", GetSelectText(), mergeID, merge_id, groupID);
        }

        public static DataTable TransposeDataTable(DataTable dtTableToTranspose, Int32 columnIndex)
        {
            DataTable dtTransposedTable = new DataTable(tablename);

            //String colName = dtTableToTranspose.Columns[columnIndex].ColumnName.ToString();
            dtTransposedTable.Columns.Add("months");

            foreach (DataRow row in dtTableToTranspose.Rows)
            {
                dtTransposedTable.Columns.Add(row[columnIndex].ToString());
            }

            Int32 colIndex = 0;
            Int32 month = 1;
            foreach (DataColumn dc in dtTableToTranspose.Columns)
            {
                if (colIndex != columnIndex)
                {
                    DataRow newRow = dtTransposedTable.NewRow();
                    newRow[0] = month;
                    month++;
                    for (Int32 destColIndex = 1; destColIndex < dtTransposedTable.Columns.Count; destColIndex++)
                    {
                        newRow[destColIndex] = dtTableToTranspose.Rows[destColIndex - 1][colIndex];
                    }

                    dtTransposedTable.Rows.Add(newRow);
                }
                colIndex++;
            }
            return dtTransposedTable;
        }

        public static DataRow Find(DataTable table, string colname, object value)
        {
            foreach (DataRow row in table.Rows)
            {
                object v = row[colname];
                if (Object.Equals(v, value))
                    return row;
            }
            return null;
        }

        //static public string GetUpdateDocTypeByDocIdText(long doc_id, long new_doc_type_id)
        //{
        //    return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
        //                        tablename, docTypeId, new_doc_type_id, id, doc_id);
        //}

        //static public string GetUpdateDocTypeByListText(long list_id, long new_doc_type_id)
        //{
        //    return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
        //                        tablename, docTypeId, new_doc_type_id, listId, list_id);
        //}

        //static public void UpdateDocTypeByDocId(List<long> doc_idArr, long new_doc_type_id, string connectionStr)
        //{
        //    if (doc_idArr.Count < 1)
        //        throw new ArgumentException("Количество документов на изменение должно быть >= 1");
        //    string commantText = String.Empty;
        //    foreach (long doc_id in doc_idArr)
        //        commantText += GetUpdateDocTypeByDocIdText(doc_id, new_doc_type_id) + "; \n";

        //    using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
        //    {
        //        if (connection.State != ConnectionState.Open)
        //            connection.Open();
        //        SQLiteTransaction trans = connection.BeginTransaction();
        //        SQLiteCommand command = new SQLiteCommand(commantText, connection, trans);
        //        int count = command.ExecuteNonQuery();
        //        trans.Commit();
        //    }
        //}

        //static public void UpdateDocTypeByListId(long list_id, long new_doc_type_id, string connectionStr)
        //{
        //    string commantText = GetUpdateDocTypeByListText(list_id, new_doc_type_id);

        //    using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
        //    {
        //        if (connection.State != ConnectionState.Open)
        //            connection.Open();
        //        SQLiteTransaction trans = connection.BeginTransaction();
        //        SQLiteCommand command = new SQLiteCommand(commantText, connection, trans);
        //        int count = command.ExecuteNonQuery();
        //        trans.Commit();
        //    }
        //}

        //static public string GetDeleteText(long doc_id)
        //{
        //    return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, doc_id);
        //}

        //static public string GetUpdateListIdText(long doc_id, long new_list_id)
        //{
        //    return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
        //                        tablename, listId, new_list_id, id, doc_id);
        //}

        //static public int UpdateListId(long doc_id, long new_list_id, string connectionStr)
        //{
        //    string commantText = GetUpdateListIdText(doc_id, new_list_id);
        //    int count = 0;
        //    using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
        //    {
        //        if (connection.State != ConnectionState.Open)
        //            connection.Open();
        //        SQLiteTransaction trans = connection.BeginTransaction();
        //        SQLiteCommand command = new SQLiteCommand(commantText, connection, trans);
        //        count = command.ExecuteNonQuery();
        //        trans.Commit();
        //        connection.Close();
        //    }
        //    return count;
        //}
        #endregion
    }

    // класс виртуальной таблицы (ей нет прямой аналогии в БД)
    public class MergeInfoTranspose
    {
        static public string tablename = "Merge_Info_transpose";

        #region Поля виртуальной таблицы
        static public string month = "month";
        static public string col1 = "1";
        static public string col2 = "2";
        static public string col3 = "3";
        static public string col4 = "4";
        static public string col5 = "5";
        static public string col6 = "21";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(month, typeof(int));
            table.Columns.Add(col1, typeof(double));
            table.Columns.Add(col2, typeof(double));
            table.Columns.Add(col3, typeof(double));
            table.Columns.Add(col4, typeof(double));
            table.Columns.Add(col5, typeof(double));
            table.Columns.Add(col6, typeof(int));

            for (int i = 0; i < 12; i++)
            {
                DataRow row = table.NewRow();
                row[month] = i + 1;
                row.EndEdit();
                table.Rows.Add(row);
            }
            return table;
        }

        static public void ConvertToMergeInfo(DataTable mergeInfoTranspose, DataTable mergeInfo)
        {
            int[] iMonth = new int[] {
                            mergeInfo.Columns[MergeInfo.january].Ordinal,
                            mergeInfo.Columns[MergeInfo.february].Ordinal,
                            mergeInfo.Columns[MergeInfo.march].Ordinal,
                            mergeInfo.Columns[MergeInfo.april].Ordinal,
                            mergeInfo.Columns[MergeInfo.may].Ordinal,
                            mergeInfo.Columns[MergeInfo.june].Ordinal,
                            mergeInfo.Columns[MergeInfo.july].Ordinal,
                            mergeInfo.Columns[MergeInfo.august].Ordinal,
                            mergeInfo.Columns[MergeInfo.september].Ordinal,
                            mergeInfo.Columns[MergeInfo.october].Ordinal,
                            mergeInfo.Columns[MergeInfo.november].Ordinal,
                            mergeInfo.Columns[MergeInfo.december].Ordinal,
                            //mergeInfo.Columns[MergeInfo.sum].Ordinal,
                            };
            DataRow[] mergeInfoRows = new DataRow[] {
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 1L),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 2L),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 3L),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 4L),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 5L),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 21L)
                            };
            for (int i = 0; i < iMonth.Length; i++)
            {
                mergeInfoRows[0][iMonth[i]] = mergeInfoTranspose.Rows[i][MergeInfoTranspose.col1];
                mergeInfoRows[1][iMonth[i]] = mergeInfoTranspose.Rows[i][MergeInfoTranspose.col2];
                mergeInfoRows[2][iMonth[i]] = mergeInfoTranspose.Rows[i][MergeInfoTranspose.col3];
                mergeInfoRows[3][iMonth[i]] = mergeInfoTranspose.Rows[i][MergeInfoTranspose.col4];
                mergeInfoRows[4][iMonth[i]] = mergeInfoTranspose.Rows[i][MergeInfoTranspose.col5];
                mergeInfoRows[5][iMonth[i]] = mergeInfoTranspose.Rows[i][MergeInfoTranspose.col6];
            }
        }

        static public void ConvertFromMergeInfo(DataTable mergeInfoTranspose, DataTable mergeInfo)
        {
            int[] iMonth = new int[] {
                            mergeInfo.Columns[MergeInfo.january].Ordinal,
                            mergeInfo.Columns[MergeInfo.february].Ordinal,
                            mergeInfo.Columns[MergeInfo.march].Ordinal,
                            mergeInfo.Columns[MergeInfo.april].Ordinal,
                            mergeInfo.Columns[MergeInfo.may].Ordinal,
                            mergeInfo.Columns[MergeInfo.june].Ordinal,
                            mergeInfo.Columns[MergeInfo.july].Ordinal,
                            mergeInfo.Columns[MergeInfo.august].Ordinal,
                            mergeInfo.Columns[MergeInfo.september].Ordinal,
                            mergeInfo.Columns[MergeInfo.october].Ordinal,
                            mergeInfo.Columns[MergeInfo.november].Ordinal,
                            mergeInfo.Columns[MergeInfo.december].Ordinal,
                            //mergeInfo.Columns[MergeInfo.sum].Ordinal,
                            };
            DataRow[] mergeInfoRows = new DataRow[] {
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 1L),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 2L),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 3L),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 4L),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 5L),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, 21L)
                            };
            for (int i = 0; i < iMonth.Length; i++)
            {
                mergeInfoTranspose.Rows[i][MergeInfoTranspose.col1] = mergeInfoRows[0][iMonth[i]];
                mergeInfoTranspose.Rows[i][MergeInfoTranspose.col2] = mergeInfoRows[1][iMonth[i]];
                mergeInfoTranspose.Rows[i][MergeInfoTranspose.col3] = mergeInfoRows[2][iMonth[i]];
                mergeInfoTranspose.Rows[i][MergeInfoTranspose.col4] = mergeInfoRows[3][iMonth[i]];
                mergeInfoTranspose.Rows[i][MergeInfoTranspose.col5] = mergeInfoRows[4][iMonth[i]];
                mergeInfoTranspose.Rows[i][MergeInfoTranspose.col6] = mergeInfoRows[5][iMonth[i]];
            }
        }
        #endregion
    }

    public class Tables
    {
        // название таблицы в БД
        static public string tablename = "Tables";

        #region Название полей таблицы в БД
        static public string id = "id";
        static public string name = "name";
        #endregion

        #region Методы - статические
        static public string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long table_id)
        {
            return string.Format("{0} WHERE {1} = {2} ", GetSelectText(), id, table_id);
        }

        static public string GetSelectText(string table_name)
        {
            return string.Format("{0} WHERE {1} = '{2}' ", GetSelectText(), name, table_name);
        }

        static public string GetSelectIDText(string table_name)
        {
            return string.Format(" SELECT {1} FROM {0} WHERE {2} = '{3}' ", tablename, id, name, table_name);
        }

        static public long GetID(string table_name, string connectionStr)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand(GetSelectIDText(table_name), conn);
            object res; ;
            conn.Open();
            res = command.ExecuteScalar();
            conn.Close();
            return (long)res;
        }
        #endregion
    }

    public class FixData
    {
        public enum FixType { New=0, Edit=1 }
        // название таблицы в БД
        static public string tablename = "Fixdata";

        #region Название полей таблицы в БД
        static public string id = "id";
        static public string type = "type";
        static public string tableID = "table_id";
        static public string rowID = "row_id";
        static public string oper = "operator";
        static public string fixDate = "fix_date";
        #endregion

        #region
        #endregion

        #region Методы - статические
        static public string GetSelectText()
        {
            return string.Format(" SELECT {0},{1},{2},{3} FROM {4} ", type, rowID, oper, fixDate, tablename);
        }

        static public string GetSelectText(string table_name)
        {
            return string.Format("{0} WHERE {1}={2} ", GetSelectText(), tableID, table_name);
        }

        static public string GetSelectText(string table_name, long row_id)
        {
            return string.Format("{0} AND {1}={2} ",GetSelectText(table_name), rowID, row_id);
        }

        static public string GetSelectText(string table_name, long row_id, FixType fix_type)
        {
            return string.Format("{0} AND {1}={2} ", GetSelectText(table_name, row_id), type, (int)fix_type);
        }

        static public string GetSelectIDText(FixData.FixType type, string table_name, long row_id)
        {
            return string.Format(@"SELECT f.{0} FROM {1} f LEFT JOIN {2} t ON t.{3}=f.{4} AND t.{5}='{6}' WHERE f.type={7} AND row_id={8}",
                                    id, 
                                    tablename, 
                                    Tables.tablename, 
                                    Tables.id,
                                    tableID,
                                    Tables.name,
                                    table_name,
                                    (int)type,  
                                    row_id);
        }

        static public string GetReplaceText(string table_name, FixType fix_type, long row_id, string oper_name, DateTime fix_date)
        {
            return string.Format(@" REPLACE INTO {0} ({1},{2},{3},{4},{5},{6}) VALUES (({7}),{8},({9}),{10},'{11}','{12}') ", 
                                    tablename,
                                    id, 
                                    type, tableID, rowID, oper, fixDate,
                                    GetSelectIDText(fix_type, table_name, row_id),
                                    (int)fix_type, Tables.GetSelectIDText(table_name), row_id, oper_name, fix_date.ToString("yyyy-MM-dd")
                                );
        }

        static public string GetDeleteText()
        {
            return string.Format("");
        }
        #endregion
    }
}
