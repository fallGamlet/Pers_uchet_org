using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;
using Pers_uchet_org.Forms;

namespace Pers_uchet_org
{
    public class MyPrinter
    {
        public static void SetPrintSettings()
        {
            using (
                var saveKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\PageSetup", true))
            {
                saveKey.SetValue("margin_top", "0.39", RegistryValueKind.String);
                saveKey.SetValue("margin_bottom", "0.39", RegistryValueKind.String);
                saveKey.SetValue("margin_left", "0.39", RegistryValueKind.String);
                saveKey.SetValue("margin_right", "0.39", RegistryValueKind.String);
            }
        }

        public static void ShowPrintPreviewWebPage(WebBrowser wb)
        {
            SetPrintSettings();
            wb.ShowPrintPreviewDialog();
        }

        static public void ShowPrintPreviewWebPage(WebBrowser wb, string url)
        {
            if (wb == null)
                wb = new WebBrowser();
            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            wb.Navigate(url);
            //ShowPrintPreviewWebPage(wb);
        }

        static public void ShowPrintPreviewWebPage(WebBrowser wb, XmlData.ReportType type)
        {
            string url = XmlData.GetReportUrl(type);
            if (url != null)
            {
                ShowPrintPreviewWebPage(wb, url);
            }
            else
            {
                MainForm.ShowWarningMessage("Не удалось найти файл отчета!", "Внимание");
            }
        }

        static void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;
            if (wb == null)
            {
                MainForm.ShowInfoMessage("Нет ссылки на объект браузера (GlobalWars.cs -> wb_DocumentCompleted)", "Внимание");
                return;
            }
            MyPrinter.SetPrintSettings();
            wb.ShowPrintPreviewDialog();
        }

        {
            Form webForm = new Form { Width = 850, Height = 600 };
            webForm.Controls.Add(wb);
            wb.Dock = DockStyle.Fill;
            wb.Show();
            webForm.StartPosition = FormStartPosition.CenterScreen;
            if (maximased)
            {
                webForm.WindowState = FormWindowState.Maximized;
            }
            webForm.Show();
        }

        public static void ShowWebPage(WebBrowser wb)
        {
            ShowWebPage(wb, false);
        }

        public static void ShowWebPage(WebBrowser wb, string url, bool maximazed)
        {
            if (wb == null)
                wb = new WebBrowser();
            wb.Navigate(url);
            ShowWebPage(wb, maximazed);
        }

        public static void ShowWebPage(WebBrowser wb, string url)
        {
            ShowWebPage(wb, url, false);
        }

        public static void ShowWebPage(WebBrowser wb, XmlData.ReportType type, bool maximazed)
        {
            string url = XmlData.GetReportUrl(type);
            if (url != null)
            {
                ShowWebPage(wb, url, maximazed);
            }
            else
            {
                MainForm.ShowWarningMessage("Не удалось найти файл отчета!", "Внимание");
            }
        }

        public static void ShowWebPage(WebBrowser wb, XmlData.ReportType type)
        {
            ShowWebPage(wb, type, false);
        }

        public static void ShowWebPage(string htmlStr, bool maximazed)
        {
            WebBrowser wb = new WebBrowser();
            wb.DocumentText = htmlStr;
            ShowWebPage(wb, maximazed);
        }

        public static void ShowWebPage(string htmlStr)
        {
            ShowWebPage(htmlStr, false);
        }

        public static void PrintWebPage(WebBrowser wb)
        {
            SetPrintSettings();
            wb.Print();
        }

        public static void PrintWebPage(string htmlStr)
        {
            WebBrowser wb = new WebBrowser();
            wb.DocumentText = htmlStr;
            PrintWebPage(wb);
        }
    }

    public class Operator
    {
        // Название таблицы в БД
        public static string tablename = "Operator";

        #region Названия полей таблицы в БД

        public static string id = "id";
        public static string name = "name";
        public static string password = "password";
        public static string candelete = "candelete";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pName = "@name";
        public static string pPassword = "@password";
        public static string pCandelete = "@candelete";

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
            return (candeleteVal == 0);
        }

        public void SaveNewPassword(string connectionStr)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionStr))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = CreateUpdateCommand())
                    {
                        command.Connection = connection;
                        command.Transaction = transaction;
                        command.Parameters[pName].Value = nameVal;
                        command.Parameters[pPassword].Value = passwordVal;
                        command.Parameters[pId].Value = idVal;

                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
        }

        #endregion

        #region Методы статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(name, typeof(string));
            table.Columns.Add(password, typeof(string));
            table.Columns.Add(candelete, typeof(int));
            //
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3} FROM {4}",
                id, name, password, candelete, tablename);
        }

        public static SQLiteCommand CreateSelectCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pName, DbType.String, name));
            comm.Parameters.Add(new SQLiteParameter(pPassword, DbType.String, password));
            comm.Parameters.Add(new SQLiteParameter(pCandelete, DbType.String, candelete));
            comm.CommandText = GetSelectText();
            return comm;
        }

        public static SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pName, DbType.String, name));
            comm.Parameters.Add(new SQLiteParameter(pCandelete, DbType.String, candelete));
            comm.Parameters.Add(new SQLiteParameter(pPassword, DbType.String, password));
            comm.CommandText = string.Format(@"INSERT INTO [{0}] ({1}, {2}) VALUES ({3}, {4});
                                               SELECT last_insert_rowid(); ",
                tablename, name, password, pName, pPassword);
            return comm;
        }

        public static SQLiteCommand CreateUpdateCommand()
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

        public static SQLiteCommand CreateDeleteCommand()
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

        public static SQLiteDataAdapter CreateAdapter(string connectionStr)
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

        public static string GetSelectCommandText(long org_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3} FROM {4} WHERE {0} = {5};",
                id, name, password, candelete, tablename, org_id);
        }

        public static Operator GetOperator(long operator_id, string connectionStr)
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

        public static Operator GetOperator(string login, string password, string connectionStr)
        {
            Operator oper;
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;
            command.CommandText = GetSelectText() +
                                  string.Format(" WHERE {0} = '{1}' AND {2} = '{3}';", Operator.name, login,
                                      Operator.password, password);
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

        public static bool IsAdmin(DataRow operatorRow)
        {
            return ((int)operatorRow[candelete] == 0);
        }

        #endregion
    }

    public class Org
    {
        // Название таблицы в БД
        public static string tablename = "Org";

        #region Названия полей таблицы в БД

        public static string id = "id";
        public static string regnum = "regnum";
        public static string name = "name";
        public static string chief_post = "chief_post";
        public static string chief_fio = "chief_fio";
        public static string booker_fio = "booker_fio";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pRegnum = "@regnum";
        public static string pName = "@name";
        public static string pChief_post = "@chief_post";
        public static string pChief_fio = "@chief_fio";
        public static string pBooker_fio = "@booker_fio";

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

        public static string GetSelectCommandText()
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, {4}, {5} FROM {6} ",
                id, regnum, name, chief_post, chief_fio, booker_fio, tablename);
        }

        public static string GetSelectCommandText(long org_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, [{4}], {5} FROM {6} WHERE {0} = {7} ",
                id, regnum, name, chief_post, chief_fio, booker_fio, tablename, org_id);
        }

        public static string GetSelectByPersonText(long person_id)
        {
            return string.Format("{0} WHERE {1} in ({2})",
                GetSelectCommandText(),
                id,
                PersonOrg.GetSelectOrgIDText(person_id));
        }


        public static string GetSelectOrgsIdWithDocsForPersonText(long person_id)
        {
            return
                string.Format(
                    "SELECT o.{0} as id, COUNT(d.{1}) as count_docs FROM {2} o INNER JOIN {3} l ON l.{4} = o.{0} INNER JOIN {5} d ON l.{6} = d.{7} WHERE d.{8} = {9} GROUP BY o.{0} HAVING count_docs > 0 ",
                    Org.id, Docs.id, Org.tablename,
                    Lists.tablename, Lists.orgID,
                    Docs.tablename, Lists.id, Docs.listId,
                    Docs.personID, person_id);
        }

        public static List<long> GetOrgsIdWithDocsForPerson(long person_id, string connection_str)
        {
            List<long> orgIds = new List<long>();
            using (SQLiteConnection connection = new SQLiteConnection(connection_str))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = GetSelectOrgsIdWithDocsForPersonText(person_id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        orgIds.Add((long)reader[Org.id]);
                    }
                }
            }
            return orgIds;
        }

        public static string GetSelectByPerson(long person_id)
        {
            return string.Format("{0} WHERE {1} in ({2})",
                GetSelectCommandText(),
                id,
                PersonOrg.GetSelectOrgIDText(person_id));
        }

        public static string GetSelectTextByOperator(long oper_id)
        {
            return string.Format(@"{0} WHERE {1} IN (SELECT DISTINCT {2} FROM {3} WHERE {4} = {5}) ",
                GetSelectCommandText(),
                id,
                OperatorOrg.orgID, OperatorOrg.tablename, OperatorOrg.operatorID, oper_id);
        }

        public static string GetSelectTextByOperatorAccess(long oper_id)
        {
            return string.Format("{0} WHERE {1} IN ({2}) ",
                GetSelectCommandText(),
                id,
                OperatorOrg.GetSelectOrgIDForEditText(oper_id));
        }

        public static SQLiteCommand CreateSelectCommand()
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

        public static SQLiteCommand CreateInsertCommand()
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
                                                SELECT last_insert_rowid() ",
                tablename,
                regnum, name, chief_post, chief_fio, booker_fio,
                pRegnum, pName, pChief_post, pChief_fio, pBooker_fio);
            return comm;
        }

        public static SQLiteCommand CreateUpdateCommand()
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
                                WHERE {11} = {12};",
                tablename,
                regnum, pRegnum, name, pName, chief_post, pChief_post,
                chief_fio, pChief_fio, booker_fio, pBooker_fio,
                id, pId);
            return comm;
        }

        public static SQLiteCommand CreateDeleteCommand()
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

        public static SQLiteDataAdapter CreateAdapter(string connectionStr)
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

        public static DataTable CreateTable()
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

        public static bool IsCorrectRegNumber(string regNumber)
        {
            if (regNumber == String.Empty)
                return false;

            string regExprStr = @"[\p{Ll}ТБРГСДКУ]\d{6}";
            Regex regEx = new Regex(regExprStr);
            return regEx.IsMatch(regNumber);
        }

        public static string ChangeEnToRus(string regNumber)
        {
            string rus = "ТРСКУ";
            string eng = "TPCKY";
            char[] chars = regNumber.ToUpper().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int pos = eng.IndexOf(chars[i]);
                if (pos >= 0)
                    chars[i] = rus[pos];
            }
            return new string(chars);
        }

        public static bool IsDuplicate(string reg_num_org, long org_id, string connection_str)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connection_str))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = GetSelectCountOrgByRegNumAndId(reg_num_org, org_id);
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static string GetSelectCountOrgByRegNumAndId(string reg_num_org, long org_id)
        {
            return string.Format("SELECT COUNT({0}) FROM {1} WHERE {2} = '{3}' AND {0} != {4}", Org.id, Org.tablename,
                Org.regnum, reg_num_org, org_id);
        }

        #endregion
    }

    public class OperatorOrg
    {
        // Название таблицы в БД
        public static string tablename = "Operator_Org_relation";

        #region Названия полей таблицы в БД

        public static string id = "id";
        public static string orgID = "org_id";
        public static string operatorID = "operator_id";
        public static string code = "privileges_code";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pOrgID = "@orgID";
        public static string pOperatorID = "@operatorID";
        public static string pCode = "@privileges_code";

        #endregion

        #region Значения полей объекта

        public long idVal;
        public long orgIDVal;
        public long operatorIDVal;
        public string codeVal;

        #endregion

        #region Методы статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(operatorID, typeof(long));
            table.Columns.Add(code, typeof(string));
            return table;
        }

        public static SQLiteCommand CreateSelectCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.UInt64, orgID));
            comm.Parameters.Add(new SQLiteParameter(pOperatorID, DbType.UInt64, operatorID));
            comm.Parameters.Add(new SQLiteParameter(pCode, DbType.String, code));
            comm.CommandText = GetSelectCommandText();
            return comm;
        }

        public static SQLiteCommand CreateInsertCommand()
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

        public static SQLiteCommand CreateUpdateCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.String, orgID));
            comm.Parameters.Add(new SQLiteParameter(pOperatorID, DbType.String, operatorID));
            comm.Parameters.Add(new SQLiteParameter(pCode, DbType.String, code));
            comm.CommandText = string.Format(@" UPDATE {0} SET {1} = {2}, {3} = {4}, {5} = {6} WHERE {7} = {8};",
                tablename, orgID, pOrgID, operatorID, pOperatorID, code, pCode, id, pId);
            return comm;
        }

        public static SQLiteCommand CreateDeleteCommand()
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

        public static SQLiteDataAdapter CreateAdapter(string connectionStr)
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

        public static string GetSelectCommandText()
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3} FROM {4};",
                id, orgID, operatorID, code, tablename);
        }

        public static string GetSelectCommandText(long oper_org_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3} FROM {4} WHERE {0} = {5};",
                id, orgID, operatorID, code, tablename, oper_org_id);
        }

        public static string GetSelectCommandText(long operator_id, long org_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3} FROM {4} WHERE {1} = {5} AND {2} = {6};",
                id, operatorID, orgID, code, tablename, operator_id, org_id);
        }

        public static string GetSelectPrivilegeText(long operator_id, long org_id)
        {
            return string.Format(@"SELECT {0} FROM {1} WHERE {2} = {3} AND {4} = {5} LIMIT 1;",
                code, tablename, operatorID, operator_id, orgID, org_id);
        }

        public static string GetSelectOrgIDText(long operator_id)
        {
            return string.Format("SELECT DISTINCT {0} FROM {1} WHERE  {2} = {3} ",
                orgID, tablename, operatorID, operator_id);
        }

        public static string GetSelectOrgIDForEditText(long operator_id)
        {
            return string.Format("SELECT DISTINCT {0} FROM {1} WHERE  {2} = {3} AND {4} LIKE '2%'",
                orgID, tablename, operatorID, operator_id, code);
        }

        public static OperatorOrg GetOperatorOrg(long oper_org_id, string connectionStr)
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

        public static string GetPrivilege(long operator_id, long org_id, string connectionStr)
        {
            SQLiteCommand command = new SQLiteCommand(GetSelectPrivilegeText(operator_id, org_id));
            command.Connection = new SQLiteConnection(connectionStr);
            command.Connection.Open();
            object code = command.ExecuteScalar();
            command.Connection.Close();
            return code as string;
        }

        public static string GetPrivilegeForAdmin()
        {
            return "212111";
        }

        public static int[] GetAccessArray(string code)
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

        public static int GetAnketaDataAccesseCode(string code)
        {
            return int.Parse(code[0].ToString());
        }

        public static int GetAnketaPrintAccesseCode(string code)
        {
            return int.Parse(code[1].ToString());
        }

        public static int GetStajDohodDataAccesseCode(string code)
        {
            return int.Parse(code[2].ToString());
        }

        public static int GetStajDohodPrintAccesseCode(string code)
        {
            return int.Parse(code[3].ToString());
        }

        public static int GetExchangeAccesseCode(string code)
        {
            return int.Parse(code[4].ToString());
        }

        public static int GetImportDataAccesseCode(string code)
        {
            return int.Parse(code[5].ToString());
        }

        #endregion
    }

    public class PersonOrg
    {
        public static string tablename = "Person_Org_relation";

        #region Названия полей таблицы в БД

        public static string id = "id";
        public static string orgID = "org_id";
        public static string personID = "person_id";
        public static string state = "state";
        public static string dismissDate = "dismiss_date";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pOrgID = "@org_id";
        public static string pPersonID = "@person_id";
        public static string pState = "@state";
        public static string pDismissDate = "@dismiss_date";

        #endregion

        #region Методы - статические

        public static SQLiteCommand CreateSelectCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.UInt64, orgID));
            comm.Parameters.Add(new SQLiteParameter(pPersonID, DbType.UInt64, personID));
            comm.CommandText = GetSelectCommandText();
            return comm;
        }

        public static SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.UInt64, orgID));
            comm.Parameters.Add(new SQLiteParameter(pPersonID, DbType.UInt64, personID));
            comm.CommandText =
                string.Format(@"INSERT INTO [{0}] ({1}, {2}) VALUES ({3}, {4}); SELECT last_indert_rowid();",
                    tablename, orgID, personID, pOrgID, pPersonID);
            return comm;
        }

        public static SQLiteCommand CreateUpdateCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.UInt64, orgID));
            comm.Parameters.Add(new SQLiteParameter(pPersonID, DbType.UInt64, personID));
            comm.CommandText = string.Format(@"UPDATE {0} SET {1} = {2}, {3} = {4} WHERE {5} = {6};",
                tablename, orgID, pOrgID, personID, pPersonID, id, pId);
            return comm;
        }

        public static SQLiteDataAdapter CreateAdapter(string connectionStr)
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

        public static string GetSelectCommandText()
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, {4} FROM {5};",
                id, orgID, personID, state, dismissDate, tablename);
        }

        public static string GetSelectCommandText(long person_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, {4} FROM {5} WHERE {2} = {6};",
                id, orgID, personID, state, dismissDate, tablename, person_id);
        }

        public static string GetSelectPersonIDText(long org_id)
        {
            return string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2} = {3} ",
                personID, tablename, orgID, org_id);
        }

        public static string GetSelectCountPersonIdText(long org_id)
        {
            return string.Format("SELECT COUNT(DISTINCT {0}) FROM {1} WHERE {2} = {3} ",
                personID, tablename, orgID, org_id);
        }

        public static long GetCountPersonId(long org_id, string connection_str)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connection_str))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = GetSelectCountPersonIdText(org_id);
                    return (long)command.ExecuteScalar();
                }
            }
        }

        public static string GetSelectOrgIDText(long person_id)
        {
            return string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2} = {3} ",
                orgID, tablename, personID, person_id);
        }

        public static string GetSelectRowIDText(long person_id, long org_id)
        {
            return string.Format(@" SELECT {0} FROM {1} WHERE {2} = {3} AND {4} = {5} ",
                id, tablename, personID, person_id, orgID, org_id);
        }

        public static string GetInsertPersonOrgText(long person_id, long org_id)
        {
            return string.Format(@"INSERT OR IGNORE INTO {0} ({1}, {2}, {3}) VALUES ( ({4}), {5}, {6}) ",
                tablename,
                id, orgID, personID,
                GetSelectRowIDText(person_id, org_id), org_id, person_id);
        }

        public static string GetDeleteCommandText(long person_id)
        {
            return string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                tablename, personID, person_id);
        }

        public static string GetDeletePersonOrgText(long person_id, long org_id)
        {
            return string.Format(" DELETE FROM {0} WHERE {1} = {2} AND {3} = {4} ",
                tablename, personID, person_id, orgID, org_id);
        }

        public static string GetDeletePersonOrgText(long person_id, long[] org_idArray)
        {
            string instr = "( ";
            foreach (long val in org_idArray)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";

            return string.Format(" DELETE FROM {0} WHERE {1} = {2} AND {3} IN {4} ",
                tablename, personID, person_id, orgID, instr);
        }

        public static string GetChangeStateText(long person_id, long org_id, object stateVal, string date)
        {
            DateTime dateval;
            date = DateTime.TryParse(date, out dateval) ? string.Format("'{0}'", date) : "NULL";
            return string.Format(" UPDATE [{0}] SET {1} = {2}, {3} = {4} WHERE {5} = {6} AND {7} = {8} ",
                tablename, state, stateVal, dismissDate, date,
                personID, person_id, orgID, org_id);
        }

        public static string GetChangeStateText(IEnumerable<long> personIDArr, long org_id, object stateVal, string date)
        {
            string personsIdStr = "( ";
            foreach (long val in personIDArr)
                personsIdStr += val + ",";
            personsIdStr = personsIdStr.Remove(personsIdStr.Length - 1);
            personsIdStr += " )";
            DateTime dateval;
            date = DateTime.TryParse(date, out dateval) ? string.Format("'{0}'", date) : "NULL";
            return string.Format("UPDATE {0} SET {1} = {2}, {3} = {4} WHERE {5} in {6} AND {7} = {8} ",
                tablename, state, stateVal, dismissDate, date,
                personID, personsIdStr, orgID, org_id);
        }

        public static long[] GetOrgID(long person_id, string connectionStr)
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

        public static void InsertPersonOrg(long person_id, long[] org_idArray, string connectionStr)
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

        public static void InsertPersonOrg(List<long> person_idArr, long org_id, SQLiteConnection connection)
        {
            InsertPersonOrg(person_idArr, org_id, connection, null);
        }

        public static int InsertPersonOrg(List<long> person_idArr, long org_id, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            string commantText = String.Empty;
            foreach (long person_id in person_idArr)
                commantText += GetInsertPersonOrgText(person_id, org_id) + "; \n";

            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(commantText, connection, transaction);
            return command.ExecuteNonQuery();
        }

        public static void DeletePersonOrg(long person_id, long[] org_idArray, string connectionStr)
        {
            string commantText = GetDeletePersonOrgText(person_id, org_idArray);
            SQLiteCommand command = new SQLiteCommand(commantText);
            command.Connection = new SQLiteConnection(connectionStr);
            command.Connection.Open();
            int count = command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public static void ChangeState(long person_id, long org_id, object stateVal, string date, string connectionStr)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            command.CommandText = GetChangeStateText(person_id, org_id, stateVal, date);
            command.Connection = connection;
            connection.Open();
            int count = command.ExecuteNonQuery();
            connection.Close();
        }

        public static void ChangeState(IEnumerable<long> personIDArr, long org_id, object stateVal, string date,
            string connectionStr)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            command.CommandText = GetChangeStateText(personIDArr, org_id, stateVal, date);
            command.Connection = connection;
            connection.Open();
            int count = command.ExecuteNonQuery();
            connection.Close();
        }

        public static void SetStateToUvolit(long person_id, long org_id, DateTime date, string connectionStr)
        {
            ChangeState(person_id, org_id, 0, date.ToString("yyyy-MM-dd"), connectionStr);
        }

        public static void SetStateToUvolit(IEnumerable<long> personIDArr, long org_id, DateTime date,
            string connectionStr)
        {
            ChangeState(personIDArr, org_id, 0, date.ToString("yyyy-MM-dd"), connectionStr);
        }

        public static void SetStateToRabotaet(long person_id, long org_id, string connectionStr)
        {
            ChangeState(person_id, org_id, 1, null, connectionStr);
        }

        public static void SetStateToRabotaet(IEnumerable<long> personIDArr, long org_id, string connectionStr)
        {
            ChangeState(personIDArr, org_id, 1, null, connectionStr);
        }

        #endregion
    }

    public class PersonInfo
    {
        // название таблицы в БД
        public static string tablename = "Person_info";

        #region Поля таблицы в БД

        public static string id = "id";
        public static string socNumber = "soc_number";
        public static string fname = "f_name";
        public static string mname = "m_name";
        public static string lname = "l_name";
        public static string birthday = "birthday";
        public static string sex = "sex";
        public static string docID = "idoc_info_id";
        public static string regadrID = "regaddr_id";
        public static string factadrID = "factaddr_id";
        public static string birthplaceID = "birthplace_id";
        public static string citizen1 = "citizen1";
        public static string citizen2 = "citizen2";
        public static string state = "state";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pSocNumber = "@soc_number";
        public static string pFname = "@f_name";
        public static string pMname = "@m_name";
        public static string pLname = "@l_name";
        public static string pBirthday = "@birthday";
        public static string pSex = "@sex";
        public static string pDocID = "@idoc_info_id";
        public static string pRegadrID = "@regaddr_id";
        public static string pFactadrID = "@factaddr_id";
        public static string pBornplaceID = "@birthplace_id";
        public static string pCitizen1 = "@citizen1";
        public static string pCitizen2 = "@citizen2";
        public static string pState = "@state";

        #endregion

        #region Методы

        public static SQLiteCommand CreateSelectCommand()
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

        public static SQLiteCommand CreateInsertCommand()
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

        public static SQLiteCommand CreateUpdateCommand()
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

        public static string GetSelectText(long person_id)
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

        public static string GetSelectIDText(long person_id, string soc_number)
        {
            return string.Format(" SELECT {0} FROM {1} WHERE {2} = '{3}' AND {4} <> {5}",
                id, tablename, socNumber, soc_number, id, person_id);
        }

        public static string GetChangeStateText(long person_id, object stateVal)
        {
            return string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4};",
                tablename, state, stateVal, id, person_id);
        }

        public static string GetChangeStateText(IEnumerable<long> personidArr, object stateVal)
        {
            string instr = "( ";
            foreach (long val in personidArr)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";

            return string.Format("UPDATE {0} SET {1} = {2} WHERE {3} in {4};",
                tablename, state, stateVal, id, instr);
        }

        public static string GetDeleteText(long person_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2};",
                tablename, id, person_id);
        }

        public static string GetDeleteText(IEnumerable<long> personidArr)
        {
            string instr = "( ";
            foreach (long val in personidArr)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";
            return string.Format("DELETE FROM {0} WHERE {1} in {2};",
                tablename, id, instr);
        }

        public static bool IsExist(long person_id, string socnumber, string connectionStr)
        {
            if (String.IsNullOrEmpty(socnumber))
                return false;
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand(GetSelectIDText(person_id, socnumber));
            command.Connection = connection;
            connection.Open();
            object res = command.ExecuteScalar();
            connection.Close();

            return (res != null && res != DBNull.Value);
        }

        public static void Delete(IEnumerable<long> personidArr, string connectionStr)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = new SQLiteConnection(connectionStr);
            command.CommandText = GetDeleteText(personidArr);
            command.Connection.Open();
            int count = command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public static string CorrectSocnumberRusToEn(string socnumber)
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

        public static bool IsCorrectSocNumber(string socNumber)
        {
            if (socNumber == String.Empty)
                return true;

            socNumber = CorrectSocnumberRusToEn(socNumber);
            string regExprStr =
                @"0[2-9]-[\p{Ll}ABCEHKMPTXАВСЕНКМРТХ]\d{1}[\p{Ll}ABCEHKMPTXАВСЕНКМРТХ]\d{2}[\p{Ll}ABCEHKMPTXАВСЕНКМРТХ]\d{2}\s*";
            Regex regEx = new Regex(regExprStr);
            return regEx.IsMatch(socNumber);
        }

        #endregion
    }

    public class PersonView
    {
        public static string tablename = "Person_View";

        public enum PersonState
        {
            Uvolen = 0,
            Rabotaet = 1
        };

        #region Названия полей в представления БД

        public static string id = "id";
        public static string socNumber = "soc_number";
        public static string fName = "f_name";
        public static string mName = "m_name";
        public static string lName = "l_name";
        public static string fio = "fio";
        public static string birthday = "birthday";
        public static string sex = "sex";
        public static string docType = "doc_type";
        public static string docSeries = "doc_series";
        public static string docNumber = "doc_number";
        public static string docDate = "doc_date";
        public static string docOrg = "doc_org";
        public static string regAdress = "regAddress";
        public static string regAdressZipcode = "regAddress_zipcode";
        public static string factAdress = "factAddress";
        public static string factAdressZipcode = "factAddress_zipcode";
        public static string bornAdress = "bornAddress";
        public static string bornAdressZipcode = "bornAddress_zipcode";
        public static string bornAdressCountry = "bornAddress_country";
        public static string bornAdressArea = "bornAddress_area";
        public static string bornAdressRegion = "bornAddress_region";
        public static string bornAdressCity = "bornAddress_city";
        public static string citizen1 = "citizen1";
        public static string citizen2 = "citizen2";
        public static string citizen1ID = "citizen1_id";
        public static string citizen2ID = "citizen2_id";
        public static string state = "state";
        public static string dismissDate = "dismiss_date";
        public static string orgID = "org_id";
        public static string newDate = "new_date";
        public static string editDate = "edit_date";
        public static string operName = "operator";

        #endregion

        #region Времменные статические переменные

        //static IEnumerable<DataRow> PrintRows;

        #endregion

        #region Статические методы

        public static DataTable CreatetTable()
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

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long org_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", orgID, org_id);
        }

        public static string GetSelectText(long org_id, long person_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} AND {2} = {3}", orgID, org_id, id, person_id);
        }

        public static string GetSelectText(long org_id, int rep_year)
        {
            return GetSelectText() +
                   string.Format(
                       " WHERE {0} = {1} AND ({2} = 1 or ( {2} = 0 AND ({3} IS NUll OR strftime('%Y',{3}) >= {4}))) ",
                       orgID, org_id, state, dismissDate, rep_year);
        }

        public static void Print(IEnumerable<DataRow> printRows, Form parent)
        {
            string file = Path.GetFullPath(Properties.Settings.Default.report_adv1);
            WebBrowser wb = new WebBrowser();
            wb.Parent = parent;
            wb.Tag = printRows;
            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            wb.ScriptErrorsSuppressed = true;
            wb.Navigate(file);
        }

        {
            string file = Path.GetFullPath(Properties.Settings.Default.report_adv1);
            WebBrowser wb = new WebBrowser();
            wb.Tag = printRows;
            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            wb.ScriptErrorsSuppressed = true;
            wb.Navigate(file);
        }

        private static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
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
                string xmlStr = XmlData.Adv1Xml(personRow).InnerXml;
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

            //MyPrinter.SetPrintSettings();
            wb.ShowPrintPreviewDialog();
        }

        #endregion
    }

    public class PersonShortView
    {
        public static string tablename = "Person_info";

        #region Названия полей

        public static string id = PersonInfo.id;
        public static string socNumber = PersonInfo.socNumber;
        public static string fio = "fio";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(socNumber, typeof(string));
            table.Columns.Add(fio, typeof(string));
            return table;
        }

        public static string GetSelectText()
        {
            return
                string.Format(
                    @"SELECT {0},{1},(IFNULL({2},'') ||' '|| IFNULL({3},'') ||' '|| IFNULL({4},'')) as {5} FROM {6} ",
                    id, socNumber,
                    PersonInfo.lname, PersonInfo.fname, PersonInfo.mname, fio,
                    tablename);
        }

        public static string GetSelectText(string soc_number)
        {
            return string.Format("{0} WHERE {1} like '%{2}%' ", GetSelectText(), socNumber, soc_number);
        }

        public static string GetSelectText(string soc_number, string fname, string mname, string lname)
        {
            return
                string.Format(
                    "{0} WHERE {1} like '%{2}%' AND {3} like '%{4}%' AND {5} like '%{6}%' AND {7} like '%{8}%'",
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
        public static string tablename = "Person_View_2";

        public enum PersonState
        {
            Uvolen = 0,
            Rabotaet = 1
        };

        #region Названия полей в представления БД

        public static string id = "id";
        public static string socNumber = "soc_number";
        public static string fio = "fio";
        public static string state = "state";
        public static string dismisDate = "dismiss_date";
        public static string orgID = "org_id";

        #endregion

        #region Статические методы

        public static DataTable CreatetTable()
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

        public static string GetSelectText()
        {
            return string.Format("SELECT {0},{1},{2},{3},{4},{5} FROM {6}",
                id, socNumber, fio, state, dismisDate, orgID, tablename);
        }

        public static string GetSelectText(long org_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", orgID, org_id);
        }

        public static string GetSelectText(long org_id, int rep_year)
        {
            return GetSelectText() +
                   string.Format(
                       " WHERE {0} = {1} AND ({2} = 1 or ( {2} = 0 AND ({3} IS NUll OR {3} >= date('{4}-01-01')))) ",
                       orgID, org_id, state, dismisDate, rep_year);
        }

        public static string GetSelectRawPersonsText(long org_id, int rep_year)
        {
            return GetSelectText() +
                   string.Format(@" WHERE {0} = {1} AND ({2} = {3} or ( {2} = {4} AND ({5} IS NUll OR {5} >= date('{6}-01-01'))))
	AND {7} NOT IN (SELECT d.[{8}] FROM {9} d
	INNER JOIN {10} l ON d.[{11}] = l.[{12}] AND  l.[{13}] = {14} AND l.[{15}] = {6})",
                       orgID, org_id, state, (int)PersonState.Rabotaet, (int)PersonState.Uvolen, dismisDate, rep_year,
                       id, Docs.personID, Docs.tablename, Lists.tablename, Docs.listId, Lists.id, Lists.orgID, org_id,
                       Lists.repYear);
        }

        #endregion
    }

    public class Country
    {
        // название таблицы в БД
        public static string tablename = "Country";

        #region названия полей таблицы в БД

        public static string id = "id";
        public static string name = "name";
        public static string LAT = "LAT";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(name, typeof(string));
            table.Columns.Add(LAT, typeof(string));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format(" SELECT {0},{1},{2} FROM {3} ",
                id, name, LAT, tablename);
        }

        #endregion
    }

    public class Adress
    {
        // название таблицы в БД
        public static string tablename = "Adress";

        #region Названя полей таблицы в БД

        public static string id = "id";
        public static string zipCode = "zip_code";
        public static string country = "country";
        public static string area = "area";
        public static string region = "region";
        public static string city = "city";
        public static string street = "street";
        public static string building = "building";
        public static string appartment = "appartment";
        public static string phone = "phone";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pZipCode = "@zip_code";
        public static string pCountry = "@country";
        public static string pArea = "@area";
        public static string pRegion = "@region";
        public static string pCity = "@city";
        public static string pStreet = "@street";
        public static string pBuilding = "@building";
        public static string pAppartment = "@appartment";
        public static string pPhone = "@phone";

        #endregion

        #region Методы

        public static SQLiteCommand CreateSelectCommand()
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

        public static SQLiteCommand CreateInsertCommand()
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

        public static SQLiteCommand CreateUpdateCommand()
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

        public static string GetSelectCommandText(long adress_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} FROM {10} WHERE {0} = {11};",
                id, zipCode, country, area, region, city, street, building, appartment, phone, tablename, adress_id);
        }

        #endregion
    }

    public class IDocInfo
    {
        // название таблицы в БД
        public static string tablename = "IDoc_info";

        #region Название полей таблицы в БД

        public static string id = "id";
        public static string docTypeID = "doc_type_id";
        public static string series = "series";
        public static string number = "number";
        public static string date = "date";
        public static string org = "org";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pDocTypeID = "@doc_type_id";
        public static string pSeries = "@series";
        public static string pNumber = "@number";
        public static string pDate = "@date";
        public static string pOrg = "@org";

        #endregion

        #region Методы

        public static SQLiteCommand CreateSelectCommand()
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

        public static SQLiteCommand CreateInsertCommand()
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

        public static SQLiteCommand CreateUpdateCommand()
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

        public static DataTable CreateTable()
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

        public static string GetSelectCommandText(long idoc_id)
        {
            return string.Format(@"SELECT {0}, {1}, {2}, {3}, [{4}], {5} FROM {6} WHERE {0} = {7};",
                id, docTypeID, series, number, date, org, tablename, idoc_id);
        }

        #endregion
    }

    public class IDocType
    {
        // название таблицы в БД
        public static string tablename = "IDoc_type";

        #region названия полей таблицы в БД

        public static string id = "id";
        public static string name = "name";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(name, typeof(string));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT {0},{1} FROM {2} ",
                id, name, tablename);
        }

        #endregion
    }

    public class IndDocs
    {
        // название таблицы в БД
        public static string tablename = "IndDocs";

        #region названия полей таблицы в БД

        public static string id = "id";
        public static string docId = "doc_id";
        public static string classpercentId = "classpercent_id";
        public static string isGeneral = "is_general";
        public static string citizen1Id = "citizen1_id";
        public static string citizen2Id = "citizen2_id";

        #endregion

        public enum Job
        {
            General = 1,
            Second = 2
        };

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docId, typeof(long));
            table.Columns.Add(classpercentId, typeof(long));
            table.Columns.Add(isGeneral, typeof(long));
            table.Columns.Add(citizen1Id, typeof(long));
            table.Columns.Add(citizen2Id, typeof(long));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long doc_id)
        {
            return string.Format(GetSelectText() + "WHERE {0} = {1} ", docId, doc_id);
        }

        public static string GetReplaceText(long doc_id, long classpercent_id, int is_general, long citizen1_id,
            long citizen2_id)
        {
            return
                string.Format(
                    "REPLACE INTO {0} ({1},{2},{3},{4},{5},{6}) VALUES (({7}),{8},{9},{10},{11},{12}); SELECT LAST_INSERT_ROWID(); ",
                    tablename, IndDocs.id, docId, classpercentId, isGeneral, citizen1Id, citizen2Id,
                    GetSelectIDText(doc_id), doc_id, classpercent_id, is_general, citizen1_id, citizen2_id);
        }

        private static string GetSelectIDText(long doc_id)
        {
            return string.Format(@"SELECT DISTINCT {0} FROM {1} WHERE {2} = {3} ",
                id,
                tablename,
                docId,
                doc_id);
        }

        public static string GetCopyText(long oldDocId, long newDocId)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3}, {4}, {5})
	SELECT {6}, {2}, {3}, {4}, {5} FROM {0} WHERE {7} = {8}; SELECT LAST_INSERT_ROWID();",
                tablename, docId, classpercentId, isGeneral, citizen1Id, citizen2Id, newDocId, docId, oldDocId);
        }

        public static int CopyIndDocByDocId(long oldDocId, long newDocId, SQLiteConnection connection)
        {
            return CopyIndDocByDocId(oldDocId, newDocId, connection, null);
        }

        public static int CopyIndDocByDocId(long oldDocId, long newDocId, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(oldDocId, newDocId), connection, transaction);
            return command.ExecuteNonQuery();
        }

        #endregion
    }

    public class Classgroup
    {
        // название таблицы в БД
        public static string tablename = "Classgroup";

        #region Название полей таблицы в БД

        public static string id = "id";
        public static string name = "name";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(name, typeof(string));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format(" SELECT {0},{1} FROM {2} ", id, name, tablename);
        }

        #endregion
    }

    public class Classificator
    {
        // название таблицы в БД
        public static string tablename = "Classificator";

        #region Название полей таблицы в БД

        public static string id = "id";
        public static string classgroupID = "classgroup_id";
        public static string spisok = "spisok";
        public static string code = "code";
        public static string name = "name";
        public static string description = "description";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
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

        public static string GetSelectText()
        {
            return string.Format(@" SELECT {0},{1},{2},{3},{4},{5} FROM {6} ",
                id, classgroupID, spisok, code, name, description, tablename);
        }

        #endregion
    }

    public class Classpercent
    {
        // название таблицы в БД
        public static string tablename = "Classpercent";

        #region Название полей таблицы в БД

        public static string id = "id";
        public static string classificatorID = "classificator_id";
        public static string privilegeID = "privilege_id";
        public static string privilegeName = "privilege_name";
        public static string value = "value";
        public static string dateBegin = "date_begin";
        public static string dateEnd = "date_end";
        public static string obligatoryIsEnabled = "obligatory_is_enabled";
        public static string isAgriculture = "is_agriculture";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(classificatorID, typeof(long));
            table.Columns.Add(privilegeID, typeof(long));
            table.Columns.Add(value, typeof(double));
            table.Columns.Add(dateBegin, typeof(string));
            table.Columns.Add(dateEnd, typeof(string));
            table.Columns.Add(obligatoryIsEnabled, typeof(int));
            table.Columns.Add(isAgriculture, typeof(int));
            return table;
        }

        public static string GetSelectText()
        {
            return
                string.Format(
                    @" SELECT {7}.{0} as {0},{1},{2}, {8}.{10} as {3}, round(CAST(REPLACE({4},',','.') AS real) * 100 , 2) as {4},{5},{6} FROM {7} LEFT JOIN {8} ON {7}.{2} = {8}.{9} ",
                    id, classificatorID, privilegeID, privilegeName, value, dateBegin, dateEnd, tablename,
                    Privilege.tablename, Privilege.id, Privilege.name);
        }

        #endregion
    }

    public class ClasspercentView
    {
        // название таблицы в БД
        public static string tablename = "Classpercent_View";

        #region Название полей таблицы в БД

        public static string id = "id";
        public static string classificatorID = "classificator_id";
        public static string code = "code";
        public static string name = "name";
        public static string description = "description";
        public static string privilegeID = "privilege_id";
        public static string privilegeName = "privilege_name";
        public static string value = "value";
        public static string dateBegin = "date_begin";
        public static string dateEnd = "date_end";
        public static string obligatoryIsEnabled = "obligatory_is_enabled";
        public static string isAgriculture = "is_agriculture";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
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
            table.Columns.Add(obligatoryIsEnabled, typeof(int));
            table.Columns.Add(isAgriculture, typeof(int));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format(@" SELECT * FROM {0}", tablename);
        }

        public static string GetSelectClassgroupOneText(DateTime now)
        {
            string commandStr =
                string.Format(@"SELECT *  FROM {0} WHERE (strftime('%s',ifnull({1},'1990-01-01')) - strftime('%s','{2}') <= 0) 
                AND (strftime('%s',ifnull({3},'2999-01-01')) - strftime('%s','{2}') >= 0)
                AND {4} >=100 AND {4} < 200", tablename, dateBegin, now.ToString("yyyy-MM-dd"), dateEnd, classificatorID);

            return commandStr;
        }

        public static string GetSelectClassgroupTwoText(DateTime now)
        {
            string commandStr =
                string.Format(@"SELECT *  FROM {0} WHERE (strftime('%s',ifnull({1},'1990-01-01')) - strftime('%s','{2}') <= 0) 
                AND (strftime('%s',ifnull({3},'2999-01-01')) - strftime('%s','{2}') >= 0)
                AND {4} >=200 AND {4} < 300", tablename, dateBegin, now.ToString("yyyy-MM-dd"), dateEnd, classificatorID);

            return commandStr;
        }

        public static string GetSelectClassgroupThreeText(DateTime now)
        {
            string commandStr =
                string.Format(@"SELECT *  FROM {0} WHERE (strftime('%s',ifnull({1},'1990-01-01')) - strftime('%s','{2}') <= 0) 
                AND (strftime('%s',ifnull({3},'2999-01-01')) - strftime('%s','{2}') >= 0)
                AND {4} >=300 AND {4} < 400", tablename, dateBegin, now.ToString("yyyy-MM-dd"), dateEnd, classificatorID);

            return commandStr;
        }

        public static string GetSelectClassgroupFourText(DateTime now)
        {
            string commandStr =
                string.Format(@"SELECT *  FROM {0} WHERE (strftime('%s',ifnull({1},'1990-01-01')) - strftime('%s','{2}') <= 0) 
                AND (strftime('%s',ifnull({3},'2999-01-01')) - strftime('%s','{2}') >= 0)
                AND {4} >=400 AND {4} < 500", tablename, dateBegin, now.ToString("yyyy-MM-dd"), dateEnd, classificatorID);

            return commandStr;
        }

        public static string GetSelectClassgroupFiveText(DateTime now)
        {
            string commandStr =
                string.Format(@"SELECT *  FROM {0} WHERE (strftime('%s',ifnull({1},'1990-01-01')) - strftime('%s','{2}') <= 0) 
                AND (strftime('%s',ifnull({3},'2999-01-01')) - strftime('%s','{2}') >= 0)
                AND {4} >=500 AND {4} < 600", tablename, dateBegin, now.ToString("yyyy-MM-dd"), dateEnd, classificatorID);

            return commandStr;
        }

        public static string GetBindingSourceFilterFor100(DateTime now)
        {
            return string.Format("{0}<='{2}' AND ({1} >= '{2}' OR {1} IS NULL) AND {3} >=100 AND {3} < 200",
                ClasspercentView.dateBegin, ClasspercentView.dateEnd, now.ToString("yyyy-MM-dd"),
                ClasspercentView.classificatorID);
        }

        public static string GetBindingSourceFilterFor200(DateTime now)
        {
            return string.Format("{0}<='{2}' AND ({1} >= '{2}' OR {1} IS NULL) AND {3} >=200 AND {3} < 300",
                ClasspercentView.dateBegin, ClasspercentView.dateEnd, now.ToString("yyyy-MM-dd"),
                ClasspercentView.classificatorID);
        }

        public static string GetBindingSourceFilterFor300(DateTime now)
        {
            return string.Format("{0}<='{2}' AND ({1} >= '{2}' OR {1} IS NULL) AND {3} >=300 AND {3} < 400",
                ClasspercentView.dateBegin, ClasspercentView.dateEnd, now.ToString("yyyy-MM-dd"),
                ClasspercentView.classificatorID);
        }

        public static string GetBindingSourceFilterFor400(DateTime now)
        {
            return string.Format("{0}<='{2}' AND ({1} >= '{2}' OR {1} IS NULL) AND {3} >=400 AND {3} < 500",
                ClasspercentView.dateBegin, ClasspercentView.dateEnd, now.ToString("yyyy-MM-dd"),
                ClasspercentView.classificatorID);
        }

        public static string GetBindingSourceFilterFor500(DateTime now)
        {
            return string.Format("{0}<='{2}' AND ({1} >= '{2}' OR {1} IS NULL) AND {3} >=500 AND {3} < 600",
                ClasspercentView.dateBegin, ClasspercentView.dateEnd, now.ToString("yyyy-MM-dd"),
                ClasspercentView.classificatorID);
        }

        #endregion
    }

    public class Privilege
    {
        // название таблицы в БД
        public static string tablename = "Privilege";

        #region Названия полей таблицы в БД

        public static string id = "id";
        public static string name = "name";

        #endregion

        #region Методы - статические

        public static string GetSelectText()
        {
            return string.Format(" SELECT {0},{1} FROM {2} ", id, name, tablename);
        }

        #endregion
    }

    public class ObligatoryPercent
    {
        // название таблицы в БД
        public static string tablename = "Obligatory_Percent";

        #region Названия полей таблицы в БД

        public static string id = "id";
        public static string value = "value";
        public static string dateBegin = "date_begin";
        public static string dateEnd = "date_end";

        #endregion

        #region Методы - статические

        public static string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        public static double GetValue(int rep_year, string connectionStr)
        {
            double val = 0;
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;
            command.CommandText = GetSelectText() +
                                  string.Format(@"WHERE (strftime('%s',ifnull({0},'1990-01-01')) - strftime('%s','{2}-01-01') <= 0) 
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

    public class ListTypes
    {
        public static string tablename = "List_Types";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string name = "name";
        public static string dateBegin = "date_begin";
        public static string dateEnd = "date_end";

        #endregion

        #region Свойства

        /// <summary>
        /// Индивидуальные сведения
        /// </summary>
        public static long PersonalInfo
        {
            get { return 1; }
        }

        /// <summary>
        /// Регистрационные данные
        /// </summary>
        public static long RegistrationInfo
        {
            get { return 2; }
        }

        #endregion
    }

    public class ListsView
    {
        public static string tablename = "Lists_View";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string listTypeId = "list_type_id";
        public static string nameType = "name";
        public static string orgID = "org_id";
        public static string operatorNameReg = "name_reg";
        public static string regDate = "reg_date";
        public static string operatorNameChange = "name_change";
        public static string changeDate = "change_date";
        public static string repYear = "rep_year";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(listTypeId, typeof(int));
            table.Columns.Add(nameType, typeof(string));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(operatorNameReg, typeof(string));
            table.Columns.Add(regDate, typeof(DateTime));
            table.Columns.Add(operatorNameChange, typeof(string));
            table.Columns.Add(changeDate, typeof(DateTime));
            table.Columns.Add(repYear, typeof(int));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long org_id, int rep_year)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} AND {2} = {3}", orgID, org_id, repYear, rep_year);
        }

        #endregion
    }

    public class ListsView2
    {
        public static string tablename = "Lists_View_2";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string listTypeId = "list_type_id";
        public static string nameType = "name";
        public static string orgID = "org_id";
        public static string repYear = "rep_year";
        public static string countDocs = "count_docs";
        public static string countPens = "count_pens";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(listTypeId, typeof(int));
            table.Columns.Add(nameType, typeof(string));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(repYear, typeof(int));
            table.Columns.Add(countDocs, typeof(int));
            table.Columns.Add(countPens, typeof(int));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long org_id, int rep_year, long list_type_id)
        {
            return GetSelectText() +
                   string.Format(" WHERE {0} = {1} AND {2} = {3} AND {4} = {5} AND {6} > 0", listTypeId, list_type_id,
                       orgID, org_id, repYear, rep_year, countDocs);
        }

        #endregion
    }

    public class Lists
    {
        public static string tablename = "Lists";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string listTypeId = "list_type_id";
        public static string orgID = "org_id";
        public static string repYear = "rep_year";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pListTypeId = "@list_type_id";
        public static string pOrgID = "@org_id";
        public static string pRepYear = "@rep_year";

        #endregion

        #region Методы - статические

        public static SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            //comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pListTypeId, DbType.Int32, listTypeId));
            comm.Parameters.Add(new SQLiteParameter(pOrgID, DbType.UInt64, orgID));
            comm.Parameters.Add(new SQLiteParameter(pRepYear, DbType.Int32, repYear));
            comm.CommandText = string.Format(@" INSERT INTO {0}
                                                ({1}, {2}, {3})
                                                VALUES
                                                ({4}, {5}, {6});
                                                SELECT last_indert_rowid()",
                tablename,
                listTypeId, orgID, repYear,
                pListTypeId, pOrgID, pRepYear);
            return comm;
        }

        public static DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(listTypeId, typeof(int));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(repYear, typeof(int));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0}", tablename);
        }

        public static string GetSelectText(long org_id, int rep_year)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} AND {2} = {3}", orgID, org_id, repYear, rep_year);
        }

        /// <summary>
        /// Метод возвращет текст SQL запроса, который применяется для вставки в таблицу Lists
        /// </summary>
        /// <param name="list_type_id">Тип пакета: 1 - Индивидуальные сведения; 2 - Регистрационные данные. Правильные значения в таблице List_Types</param>
        /// <param name="org_id"></param>
        /// <param name="rep_year"></param>
        /// <returns></returns>
        public static string GetInsertText(long list_type_id, long org_id, int rep_year)
        {
            return string.Format("INSERT INTO {0} ({1},{2},{3}) VALUES ({4},{5},{6}); SELECT LAST_INSERT_ROWID(); ",
                tablename, listTypeId, orgID, repYear, list_type_id, org_id, rep_year);
        }

        public static string GetDeleteText(long list_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, list_id);
        }

        public static string GetUpdateYearText(long list_id, int new_rep_year)
        {
            return string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}", tablename, repYear, new_rep_year, id,
                list_id);
        }

        public static long UpdateYear(long list_id, int new_rep_year, SQLiteConnection connection)
        {
            return UpdateYear(list_id, new_rep_year, connection, null);
        }

        public static long UpdateYear(long list_id, int new_rep_year, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetUpdateYearText(list_id, new_rep_year), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }

        public static string GetUpdateOrgText(long list_id, long new_org_id)
        {
            return string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}", tablename, orgID, new_org_id, id, list_id);
        }

        public static string GetSelectPersonIdsText(long list_id)
        {
            return string.Format("SELECT person_id FROM Docs WHERE list_id = {0}", list_id);
        }

        public static string GetCopyText(long old_list_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3})
	SELECT {1}, {2}, {3} FROM {0} WHERE {4} = {5}; SELECT LAST_INSERT_ROWID();",
                tablename, listTypeId, orgID, repYear, id, old_list_id);
        }

        public static long CopyListById(long old_list_id, SQLiteConnection connection)
        {
            return CopyListById(old_list_id, connection, null);
        }

        public static long CopyListById(long old_list_id, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(old_list_id), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }

        public static string GetListsIdsText(long org_id, int rep_year, long doc_type, long person_id,
            long classpercent_id, IndDocs.Job job, long cur_doc_id)
        {
            return string.Format(@"SELECT DISTINCT l.{0} as id
                                    FROM {1} l
                                    INNER JOIN {2} d ON d.{3} = l.{0}
                                    INNER JOIN {4} id ON id.{5} = d.{6}
                                    WHERE {7} = {8} AND {9} = {10} AND {11} = {12} AND {13} = {14} AND {15} = {16} AND {17} = {18} AND d.{6} <> {19} ",
                id, tablename, Docs.tablename, Docs.listId, IndDocs.tablename, IndDocs.docId, Docs.id, orgID, org_id,
                repYear, rep_year, Docs.docTypeId, doc_type, Docs.personID, person_id, IndDocs.classpercentId,
                classpercent_id, IndDocs.isGeneral, (int)job, cur_doc_id);
        }

        #endregion
    }

    public class DocsView
    {
        public static string tablename = "Docs_View";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string docTypeId = "doc_type_id";
        public static string nameType = "name";
        public static string listId = "list_id";
        public static string personID = "person_id";
        public static string socNumber = "soc_number";
        public static string fio = "fio";
        public static string code = "code";
        public static string operNameReg = "name_reg";
        public static string regDate = "reg_date";
        public static string operNameChange = "name_change";
        public static string changeDate = "change_date";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docTypeId, typeof(int));
            table.Columns.Add(nameType, typeof(string));
            table.Columns.Add(listId, typeof(long));
            table.Columns.Add(personID, typeof(long));
            table.Columns.Add(socNumber, typeof(string));
            table.Columns.Add(fio, typeof(string));
            table.Columns.Add(code, typeof(string));
            table.Columns.Add(operNameReg, typeof(string));
            table.Columns.Add(regDate, typeof(DateTime));
            table.Columns.Add(operNameChange, typeof(string));
            table.Columns.Add(changeDate, typeof(DateTime));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectTextByListId(long list_id)
        {
            return string.Format("{0} WHERE {1} = {2} ORDER BY {3} ", GetSelectText(), listId, list_id, fio);
        }

        public static string GetSelectTextByListId(IEnumerable<long> list_id)
        {
            string instr = "( ";
            foreach (long val in list_id)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";
            return string.Format("{0} WHERE {1} = {2} ORDER BY {3}, {4} ", GetSelectText(), listId, instr, listId,
                docTypeId);
        }

        public static string GetSelectTextByDocId(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ", id, doc_id);
        }

        public static DataTable GetDocs(long list_id, string connectionStr)
        {
            DataTable table = CreateTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectTextByListId(list_id), connectionStr);
            adapter.Fill(table);
            return table;
        }

        public static DataTable GetDocs(IEnumerable<long> list_id, string connectionStr)
        {
            DataTable table = CreateTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectTextByListId(list_id), connectionStr);
            adapter.Fill(table);
            return table;
        }

        public static string GetCountDocsInListText(long list_id)
        {
            return string.Format("SELECT count(*) FROM {0} WHERE {1} = {2}", tablename, listId, list_id);
        }

        public static int GetCountDocsInList(long NewListId, string _connection)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connection))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = GetCountDocsInListText(NewListId);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        #endregion
    }

    public class DocsView2
    {
        public static string tablename = "Docs_View_2";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string listId = "list_id";
        public static string personID = "person_id";
        public static string repYear = "rep_year";
        public static string orgId = "org_id";
        public static string regNum = "regnum";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(listId, typeof(long));
            table.Columns.Add(personID, typeof(long));
            table.Columns.Add(repYear, typeof(long));
            table.Columns.Add(orgId, typeof(long));
            table.Columns.Add(regNum, typeof(string));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        //static public string GetSelectTextByListId(long list_id)
        //{
        //    return string.Format("{0} WHERE {1} = {2} ", GetSelectText(), listId, list_id);
        //}

        //static public string GetSelectTextByListId(IEnumerable<long> list_id)
        //{
        //    string instr = "( ";
        //    foreach (long val in list_id)
        //        instr += val + ",";
        //    instr = instr.Remove(instr.Length - 1);
        //    instr += " )";
        //    return string.Format("{0} WHERE {1} = {2} ORDER BY {3}, {4} ", GetSelectText(), listId, instr, listId, docTypeId);
        //}

        public static string GetSelectTextByDocId(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ", id, doc_id);
        }

        public static string GetSelectTextByPersonId(long person_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ", personID, person_id);
        }

        public static DataTable GetDocs(long person_id, string connectionStr)
        {
            DataTable table = CreateTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectTextByPersonId(person_id), connectionStr);
            adapter.Fill(table);
            return table;
        }

        //static public DataTable GetDocs(IEnumerable<long> list_id, string connectionStr)
        //{
        //    DataTable table = CreateTable();
        //    SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectTextByListId(list_id), connectionStr);
        //    adapter.Fill(table);
        //    return table;
        //}
        //static public string GetCountDocsInListText(long list_id)
        //{
        //    return string.Format("SELECT count(*) FROM {0} WHERE {1} = {2}", tablename, listId, list_id);
        //}

        //public static int GetCountDocsInList(long NewListId, string _connection)
        //{
        //    using (SQLiteConnection connection = new SQLiteConnection(_connection))
        //    {
        //        if (connection.State != ConnectionState.Open)
        //            connection.Open();
        //        using (SQLiteCommand command = connection.CreateCommand())
        //        {
        //            command.CommandText = GetCountDocsInListText(NewListId);
        //            return Convert.ToInt32(command.ExecuteScalar());
        //        }
        //    }
        //}

        #endregion
    }

    public class DocsShortView
    {
        // название таблицы
        public static string tablename = Docs.tablename;

        #region Названия полей в представления БД

        public static string id = Docs.id;
        public static string docTypeId = Docs.docTypeId;
        public static string listId = Docs.listId;
        public static string socNumber = PersonInfo.socNumber;
        public static string fname = PersonInfo.fname;
        public static string mname = PersonInfo.mname;
        public static string lname = PersonInfo.lname;

        #endregion

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docTypeId, typeof(int));
            table.Columns.Add(listId, typeof(long));
            table.Columns.Add(socNumber, typeof(string));
            table.Columns.Add(fname, typeof(string));
            table.Columns.Add(mname, typeof(string));
            table.Columns.Add(lname, typeof(string));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format(@"SELECT d.[{0}] as {0},d.[{1}] as {1},d.[{2}] as {2}
		                            ,pi.[{3}] as {3},pi.[{4}] as {4},pi.[{5}] as {5},pi.[{6}] as {6}
	                            FROM [{7}] d
	                            INNER JOIN {8} pi ON d.{9} = pi.{10}
	                            ORDER BY d.{1}",
                id, docTypeId, listId, socNumber, fname, mname, lname,
                tablename, PersonInfo.tablename, Docs.personID, PersonInfo.id);
        }

        public static string GetSelectText(IEnumerable<long> list_id)
        {
            string instr = "( ";
            foreach (long val in list_id)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";
            return string.Format(@"SELECT d.[{0}] as {0}, d.[{1}] as {1}, d.[{2}] as {2}
		                            ,pi.[{3}] as {3}, pi.[{4}] as {4}, pi.[{5}] as {5}, pi.[{6}] as {6}
                                FROM [{7}] d
                                INNER JOIN {8} pi ON d.{9} = pi.{10}
                                WHERE d.{2} in {11}
                                ORDER BY d.{1}",
                id, docTypeId, listId, socNumber, fname, mname, lname,
                tablename, PersonInfo.tablename, Docs.personID, PersonInfo.id,
                instr);
        }

        public static DataTable GetDocs(IEnumerable<long> list_id, string connectionStr)
        {
            DataTable table = CreateTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectText(list_id), connectionStr);
            adapter.Fill(table);
            //
            return table;
        }
    }

    public class DocsViewForXml
    {
        public static string tablename = "Docs_View_For_Xml";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string listId = "list_id";
        public static string repYear = "rep_year";
        public static string socNumber = "soc_number";
        public static string lName = "l_name";
        public static string fName = "f_name";
        public static string mName = "m_name";
        public static string citizen1Id = "citizen1_id";
        public static string citizen1Name = "citizen1_name";
        public static string citizen2Id = "citizen2_id";
        public static string citizen2Name = "citizen2_name";
        public static string classificatorId = "classificator_id";
        public static string code = "code";
        public static string privilegeId = "privilege_id";
        public static string privilegeName = "privilege_name";
        public static string docTypeId = "doc_type_id";
        public static string isGeneral = "is_general";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(listId, typeof(long));
            table.Columns.Add(repYear, typeof(int));
            table.Columns.Add(socNumber, typeof(string));
            table.Columns.Add(lName, typeof(string));
            table.Columns.Add(fName, typeof(string));
            table.Columns.Add(mName, typeof(string));
            table.Columns.Add(citizen1Id, typeof(string));
            table.Columns.Add(citizen1Name, typeof(string));
            table.Columns.Add(citizen2Id, typeof(string));
            table.Columns.Add(citizen2Name, typeof(string));
            table.Columns.Add(classificatorId, typeof(string));
            table.Columns.Add(code, typeof(string));
            table.Columns.Add(privilegeId, typeof(string));
            table.Columns.Add(privilegeName, typeof(string));
            table.Columns.Add(docTypeId, typeof(long));
            table.Columns.Add(isGeneral, typeof(long));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectTextByListId(long list_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ORDER BY {2} ", listId, list_id, lName);
        }

        public static string GetSelectTextByDocId(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ", id, doc_id);
        }

        public static DataRow GetRow(long doc_id, string connection_str)
        {
            DataTable table = DocsViewForXml.CreateTable();
            DataRow rowRes = null;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectTextByDocId(doc_id), connection_str);
            adapter.Fill(table);
            if (table.Rows.Count > 0)
                rowRes = table.Rows[0];
            return rowRes;
        }

        public static DataRow NewRow()
        {
            return CreateTable().NewRow();
        }

        #endregion
    }

    public class Docs
    {
        public static string tablename = "Docs";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string docTypeId = "doc_type_id";
        public static string listId = "list_id";
        public static string personID = "person_id";

        #endregion

        #region Методы - статические

        public static DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docTypeID, typeof(int));
            table.Columns.Add(listID, typeof(long));
            table.Columns.Add(personID, typeof(long));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long list_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", listID, list_id);
        }

        public static string GetUpdateDocTypeByDocIdText(long doc_id, long new_doc_type_id)
        {
            return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
                tablename, docTypeId, new_doc_type_id, id, doc_id);
        }

        public static string GetUpdateDocTypeByListText(long list_id, long new_doc_type_id)
        {
            return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
                tablename, docTypeId, new_doc_type_id, listId, list_id);
        }

        public static int UpdateDocTypeByDocId(List<long> doc_idArr, long new_doc_type_id, SQLiteConnection connection)
        {
            return UpdateDocTypeByDocId(doc_idArr, new_doc_type_id, connection, null);
        }

        public static int UpdateDocTypeByDocId(List<long> doc_idArr, long new_doc_type_id, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (doc_idArr.Count < 1)
                throw new ArgumentException("Количество документов на изменение должно быть >= 1");
            string commantText = String.Empty;
            foreach (long doc_id in doc_idArr)
                commantText += GetUpdateDocTypeByDocIdText(doc_id, new_doc_type_id) + "; \n";

            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(commantText, connection, transaction);
            return command.ExecuteNonQuery();
        }

        public static int UpdateDocTypeByListId(long list_id, long new_doc_type_id, SQLiteConnection connection)
        {
            return UpdateDocTypeByListId(list_id, new_doc_type_id, connection, null);
        }

        public static int UpdateDocTypeByListId(long list_id, long new_doc_type_id, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            string commantText = GetUpdateDocTypeByListText(list_id, new_doc_type_id);

            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(commantText, connection, transaction);
            return command.ExecuteNonQuery();
        }

        public static string GetDeleteText(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, doc_id);
        }

        public static string GetUpdateListIdText(long doc_id, long new_list_id)
        {
            return string.Format(@"UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
                tablename, listId, new_list_id, id, doc_id);
        }

        public static int UpdateListId(long doc_id, long new_list_id, SQLiteConnection connection)
        {
            return UpdateListId(doc_id, new_list_id, connection, null);
        }

        public static int UpdateListId(long doc_id, long new_list_id, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            string commantText = GetUpdateListIdText(doc_id, new_list_id);

            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(commantText, connection, transaction);
            return command.ExecuteNonQuery();
        }

        public static string GetInsertText(long doc_type_id, long list_id, long person_id)
        {
            return string.Format("INSERT INTO {0} ({1},{2},{3}) VALUES ({4},{5},{6}); SELECT LAST_INSERT_ROWID();",
                tablename, docTypeId, listId, personID, doc_type_id, list_id, person_id);
        }

        public static string GetCopyText(long doc_id, long list_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3})
	SELECT {1}, {6}, {3} FROM {0} WHERE {4} = {5}; SELECT LAST_INSERT_ROWID();",
                tablename, docTypeId, listId, personID, id, doc_id, list_id);
        }

        public static long CopyDocByDocId(long doc_id, long list_id, SQLiteConnection connection)
        {
            return CopyDocByDocId(doc_id, list_id, connection, null);
        }

        public static long CopyDocByDocId(long doc_id, long list_id, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(doc_id, list_id), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }

        public static string GetCountDocsText(long list_id, long doc_type)
        {
            return string.Format(@"SELECT COUNT({0}) FROM {1} WHERE {2} = {3} AND {4} = {5} ",
                id, tablename, listId, list_id, docTypeId, doc_type);
        }

        public static long CountDocsInList(long list_id, long doc_type, SQLiteConnection connection)
        {
            return CountDocsInList(list_id, doc_type, connection, null);
        }

        public static long CountDocsInList(long list_id, long doc_type, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCountDocsText(list_id, doc_type), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }

        public static string GetCountDocsByYearText(long year, long person_id)
        {
            return
                string.Format(
                    @"SELECT COUNT({0}) FROM {1} WHERE {2} = {3} AND {4} IN (SELECT {5} FROM {6} WHERE {7} = {8}) ",
                    id, tablename, personID, person_id, listId, Lists.id, Lists.tablename, Lists.repYear, year);
        }

        public static long CountDocsByYear(long year, long person_id, SQLiteConnection connection)
        {
            return CountDocsByYear(year, person_id, connection, null);
        }

        public static long CountDocsByYear(long year, long person_id, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCountDocsByYearText(year, person_id), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }

        /// <summary>
        /// Получить текст запроса на количество документов в указанном пакете с указанным типом документа
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <param name="docType_id">идентификатор типа документа</param>
        /// <returns></returns>
        public static string GetSelectCountText(long list_id, long docType_id)
        {
            return string.Format("SELECT count(distinct {0}) as [count] FROM {1} WHERE {2}={3} AND {4}={5} ",
                id, tablename, listId, list_id, docTypeId, docType_id);
        }

        /// <summary>
        /// Получить текст запроса на выборку сгруппированных количеств документов по типам документов
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <returns></returns>
        public static string GetSelectCountText(long list_id)
        {
            return string.Format(@"SELECT d.{0}, count(distinct d.{1}) as [count] 
                                    FROM {2} d
                                    INNER JOIN {3} dt ON d.{0} = dt.{4} and dt.{5} = 2
                                    INNER JOIN {6} pi ON pi.{7} = d.{8} AND length(pi.{9}) > 0
                                    WHERE {10} = {11}
                                    GROUP BY {0}",
                docTypeId, id,
                tablename,
                DocTypes.tablename, DocTypes.id, DocTypes.listTypeId,
                PersonInfo.tablename, PersonInfo.id, personID, PersonInfo.socNumber,
                listId, list_id);
        }

        /// <summary>
        /// Получить текст запроса для количества документов в указанных пакетах
        /// </summary>
        /// <param name="list_id">Список идентификаторов пакетов</param>
        /// <returns>Текст запроса</returns>
        public static string GetSelectCountText(IEnumerable<long> list_id)
        {
            StringBuilder list_id_str = new StringBuilder();
            foreach (long val in list_id)
            {
                list_id_str.Append(val + ",");
            }
            list_id_str[list_id_str.Length - 1] = ' ';

            return string.Format(@" SELECT count(distinct {0}) as [count] FROM {1} WHERE {2} in ({3}) ",
                id, tablename, listId, list_id_str);
        }

        /// <summary>
        /// Получить количество документов в указанных пакетах
        /// </summary>
        /// <param name="list_id">Список идентификаторов пакетов</param>
        /// <param name="connectionStr">Строка подключения к БД</param>
        /// <returns>Количество документов</returns>
        public static long Count(IEnumerable<long> list_id, string connectionStr)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand(GetSelectCountText(list_id), connection);
            connection.Open();
            object res = command.ExecuteScalar();
            connection.Close();
            //
            return (long)res;
        }

        /// <summary>
        /// Получить значение количества документов в указанном пакете с указанным типом документа
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <param name="docType_id">идентификатор типа документа</param>
        /// <param name="connectionStr">строка подключения к БД</param>
        /// <returns></returns>
        public static long CountDocsByListAndType(long list_id, long docType_id, string connectionStr)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand(GetSelectCountText(list_id, docType_id), connection);
            connection.Open();
            long res = (long)command.ExecuteScalar();
            connection.Close();
            return res;
        }

        /// <summary>
        /// Получить таблицу с типами документов и количествами этих типов документов
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <param name="connectionStr">строка подключения к БД</param>
        /// <returns>Таблицы со столбцами Тип документа, Количество документов ("count")</returns>
        public static DataTable CountDocsByListAndType(long list_id, string connectionStr)
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(docTypeID, typeof(long));
            table.Columns.Add("count", typeof(int));
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectCountText(list_id), connectionStr);
            adapter.Fill(table);
            return table;
        }

        /// <summary>
        /// Получить текст запроса для выборки сумм, сгруппированных по типам документов и группам сумм (1,2,3,4,5)
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <returns></returns>
        public static string GetSumsByDocTypeText(long list_id)
        {
            return string.Format(@"SELECT d.{0}, si.{1}, sum(si.{2}) as {2}
                                FROM {3} d INNER JOIN {4} si ON si.{5} = d.{6} and si.{1} in (1,2,3,4,5)
                                INNER JOIN {7} pi ON pi.{8} = d.{9} AND length(pi.{10}) > 0
                                WHERE d.{11} = {12}
                                GROUP BY d.{0}, si.{1}
                                ORDER BY d.{0}, si.{1}",
                docTypeId, SalaryInfo.salaryGroupsId, SalaryInfo.sum,
                tablename, SalaryInfo.tablename, SalaryInfo.docId, id,
                PersonInfo.tablename, PersonInfo.id, personID, PersonInfo.socNumber,
                listId, list_id);
        }

        /// <summary>
        /// Получить таблицу с выборкой сумм, сгруппированных по типам документов и группам сумм (1,2,3,4,5)
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <param name="connectionStr">строка подключения к БД</param>
        /// <returns>Таблица с столбцами: Тип документа, Группа суммы, Сумма</returns>
        public static DataTable SumsByDocType(long list_id, string connectionStr)
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(docTypeID, typeof(long));
            table.Columns.Add(SalaryInfo.salaryGroupsId, typeof(long));
            table.Columns.Add(SalaryInfo.sum, typeof(double));

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSumsByDocTypeText(list_id), connectionStr);
            adapter.Fill(table);
            return table;
        }

        /// <summary>
        /// Получить текст запроса на выборку идентификаторов документов
        /// </summary>
        /// <param name="list_id">Идентификатор пакета</param>
        /// <returns></returns>
        public static string GetSelectDocsIDText(long list_id)
        {
            return
                string.Format(
                    "SELECT d.{0} FROM {1} d INNER JOIN {2} pi ON d.{3} = pi.{4} AND pi.{5} IS NOT NULL AND TRIM(pi.{5}) <> '' WHERE d.{6} = {7}",
                    id, tablename, PersonInfo.tablename, Docs.personID, PersonInfo.id, PersonInfo.socNumber, listId,
                    list_id);
        }

        /// <summary>
        /// Получить массив идентификаторов документов в указанном пакете
        /// </summary>
        /// <param name="list_id">Идентификатор пакетов</param>
        /// <param name="connectionStr">Строка подключения к БД</param>
        /// <returns></returns>
        public static long[] GetDocsID(long list_id, string connectionStr)
        {
            LinkedList<long> idArray = new LinkedList<long>();
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand(GetSelectDocsIDText(list_id), connection);
            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                idArray.AddLast((long)reader[Docs.id]);
            }
            reader.Close();
            connection.Close();
            //
            return idArray.ToArray();
        }

        #endregion
    }

    public class DocTypes
    {
        public static string tablename = "Doc_Types";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string listTypeId = "list_type_id";
        public static string name = "name";

        #endregion

        #region Свойства

        /// <summary>
        /// Исходная форма
        /// </summary>
        public static long InitialFormId
        {
            get { return 21; }
        }

        /// <summary>
        /// Корректирующая форма
        /// </summary>
        public static long CorrectionFormId
        {
            get { return 22; }
        }

        /// <summary>
        /// Отменяющая форма
        /// </summary>
        public static long CancelingFormId
        {
            get { return 23; }
        }

        /// <summary>
        /// Назначение пенсии
        /// </summary>
        public static long GrantingPensionId
        {
            get { return 24; }
        }

        #endregion

        #region Методы - статические

        public static DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(listTypeId, typeof(long));
            table.Columns.Add(name, typeof(string));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long list_type_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", listTypeId, list_type_id);
        }

        #endregion
    }

    public class SalaryGroups
    {
        /// <summary>
        /// Сумма заработка (дохода), на который начислены страховые взносы
        /// </summary>
        public static int Column1
        {
            get { return 1; }
        }

        /// <summary>
        /// Сумма выплат, учитываемых для назначения пенсии
        /// </summary>
        public static int Column2
        {
            get { return 2; }
        }

        /// <summary>
        /// Сумма страховых взносов, начисленных работодателем
        /// </summary>
        public static int Column3
        {
            get { return 3; }
        }

        /// <summary>
        /// Сумма страховых взносов, уплаченных работодателем
        /// </summary>
        public static int Column4
        {
            get { return 4; }
        }

        /// <summary>
        /// Сумма обязательных страховых взносов, уплачиваемых из заработка
        /// </summary>
        public static int Column5
        {
            get { return 5; }
        }

        /// <summary>
        /// Всего полных дней для общего стажа
        /// </summary>
        public static int Column10
        {
            get { return 10; }
        }

        /// <summary>
        /// Признак тарифа
        /// </summary>
        public static int Column20
        {
            get { return 20; }
        }

        /// <summary>
        /// Средняя численность работников (застрахованных лиц)
        /// </summary>
        public static int Column21
        {
            get { return 21; }
        }
    }

    public class SalaryInfo
    {
        public static string tablename = "Salary_Info";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string docId = "doc_id";
        public static string salaryGroupsId = "salary_groups_id";
        public static string january = "january";
        public static string february = "february";
        public static string march = "march";
        public static string april = "april";
        public static string may = "may";
        public static string june = "june";
        public static string july = "july";
        public static string august = "august";
        public static string september = "september";
        public static string october = "october";
        public static string november = "november";
        public static string december = "december";
        public static string sum = "sum";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pDocId = "@doc_id";
        public static string pSalaryGroupsId = "@salary_groups_id";
        public static string pJanuary = "@january";
        public static string pFebruary = "@february";
        public static string pMarch = "@march";
        public static string pApril = "@april";
        public static string pMay = "@may";
        public static string pJune = "@june";
        public static string pJuly = "@july";
        public static string pAugust = "@august";
        public static string pSeptember = "@september";
        public static string pOctober = "@october";
        public static string pNovember = "@november";
        public static string pDecember = "@december";
        public static string pSum = "@sum";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long)).DefaultValue = 0;
            table.Columns.Add(docId, typeof(long)).DefaultValue = 0;
            table.Columns.Add(salaryGroupsId, typeof(long)).DefaultValue = 0;
            table.Columns.Add(january, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(february, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(march, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(april, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(may, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(june, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(july, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(august, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(september, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(october, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(november, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(december, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(sum, typeof(double)).DefaultValue = 0.0;
            return table;
        }

        public static DataTable CreateTableWithRows()
        {
            DataTable table = CreateTable();
            DataRow row = table.NewRow();
            row[SalaryInfo.salaryGroupsId] = SalaryGroups.Column1;
            row.EndEdit();
            table.Rows.Add(row);
            row = table.NewRow();
            row[SalaryInfo.salaryGroupsId] = SalaryGroups.Column2;
            row.EndEdit();
            table.Rows.Add(row);
            row = table.NewRow();
            row[SalaryInfo.salaryGroupsId] = SalaryGroups.Column3;
            row.EndEdit();
            table.Rows.Add(row);
            row = table.NewRow();
            row[SalaryInfo.salaryGroupsId] = SalaryGroups.Column4;
            row.EndEdit();
            table.Rows.Add(row);
            row = table.NewRow();
            row[SalaryInfo.salaryGroupsId] = SalaryGroups.Column5;
            row.EndEdit();
            table.Rows.Add(row);
            row = table.NewRow();
            row[SalaryInfo.salaryGroupsId] = SalaryGroups.Column10;
            row.EndEdit();
            table.Rows.Add(row);
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0}", tablename);
        }

        public static string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ORDER BY {2}", docId, doc_id, salaryGroupsId);
        }

        /// <summary>
        /// Получить текст запроса на выборку таблицы Salary_info с суммами выплат
        /// </summary>
        /// <param name="list_id">Перечисление идентификаторов Пакетов</param>
        /// <param name="doc_type_id">Перечисление идентификаторов Типов документов</param>
        /// <returns>В выбираемой таблице в столбце doc_id получается количество документов, а не их идентификаторы</returns>
        public static string GetSelectText(IEnumerable<long> list_id, IEnumerable<long> doc_type_id)
        {
            string list_id_str = "( ";
            foreach (long val in list_id)
                list_id_str += val + ",";
            list_id_str = list_id_str.Remove(list_id_str.Length - 1);
            list_id_str += " )";

            string doc_type_id_str = "( ";
            foreach (long val in doc_type_id)
                doc_type_id_str += val + ",";
            doc_type_id_str = doc_type_id_str.Remove(doc_type_id_str.Length - 1);
            doc_type_id_str += " )";

            return string.Format(@"SELECT 
                             [{0}]
	                        ,SUM([{1}]) as {1}
	                        ,SUM([{2}]) as {2}
	                        ,SUM([{3}]) as {3}
	                        ,SUM([{4}]) as {4}
	                        ,SUM([{5}]) as {5}
	                        ,SUM([{6}]) as {6}
	                        ,SUM([{7}]) as {7}
	                        ,SUM([{8}]) as {8}
	                        ,SUM([{9}]) as {9}
	                        ,SUM([{10}]) as {10}
	                        ,SUM([{11}]) as {11}
	                        ,SUM([{12}]) as {12}
                        FROM [{13}]
                        WHERE {14} in (SELECT {15} FROM {16} WHERE {17} in {18} AND {19} in {20})
                        GROUP BY [{0}]",
                salaryGroupsId,
                january, february, march, april, may, june, july, august, september, october, november, december,
                tablename, docId,
                Docs.id, Docs.tablename, Docs.listId, list_id_str, Docs.docTypeId, doc_type_id_str);
        }

        //        static public SQLiteCommand CreateReplaceCommand()
        //        {
        //            SQLiteCommand comm = new SQLiteCommand();
        //            comm.Parameters.Add(pId, DbType.Int64);
        //            comm.Parameters.Add(pDocId, DbType.Int64);
        //            comm.Parameters.Add(pSalaryGroupsId, DbType.Int64);
        //            comm.Parameters.Add(pJanuary, DbType.Double);
        //            comm.Parameters.Add(pFebruary, DbType.Double);
        //            comm.Parameters.Add(pMarch, DbType.Double);
        //            comm.Parameters.Add(pApril, DbType.Double);
        //            comm.Parameters.Add(pMay, DbType.Double);
        //            comm.Parameters.Add(pJune, DbType.Double);
        //            comm.Parameters.Add(pJuly, DbType.Double);
        //            comm.Parameters.Add(pAugust, DbType.Double);
        //            comm.Parameters.Add(pSeptember, DbType.Double);
        //            comm.Parameters.Add(pOctober, DbType.Double);
        //            comm.Parameters.Add(pNovember, DbType.Double);
        //            comm.Parameters.Add(pDecember, DbType.Double);
        //            comm.Parameters.Add(pSum, DbType.Double);

        //            comm.CommandText = string.Format(@"REPLACE INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16})
        //                                                        VALUES ({17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32});
        //                                               SELECT LAST_INSERT_ROWID();", tablename, id, docId, salaryGroupsId, january, february, march, april, may, june, july, august, september, october, november, december, sum, pId, pDocId, pSalaryGroupsId, pJanuary, pFebruary, pMarch, pApril, pMay, pJune, pJuly, pAugust, pSeptember, pOctober, pNovember, pDecember, pSum);
        //            return comm;
        //        }

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

        public static int FindRowIndex(DataTable table, string colname, object value)
        {
            foreach (DataRow row in table.Rows)
            {
                object v = row[colname];
                if (Object.Equals(v, value))
                    return table.Rows.IndexOf(row);
            }
            return -1;
        }

        public static int[] GetMonthIndexes(DataTable salaryInfo)
        {
            return new int[]
            {
                salaryInfo.Columns[SalaryInfo.january].Ordinal,
                salaryInfo.Columns[SalaryInfo.february].Ordinal,
                salaryInfo.Columns[SalaryInfo.march].Ordinal,
                salaryInfo.Columns[SalaryInfo.april].Ordinal,
                salaryInfo.Columns[SalaryInfo.may].Ordinal,
                salaryInfo.Columns[SalaryInfo.june].Ordinal,
                salaryInfo.Columns[SalaryInfo.july].Ordinal,
                salaryInfo.Columns[SalaryInfo.august].Ordinal,
                salaryInfo.Columns[SalaryInfo.september].Ordinal,
                salaryInfo.Columns[SalaryInfo.october].Ordinal,
                salaryInfo.Columns[SalaryInfo.november].Ordinal,
                salaryInfo.Columns[SalaryInfo.december].Ordinal
                //salaryInfo.Columns[SalaryInfo.sum].Ordinal,
            };
        }

        public static void SetDocId(DataTable table, long doc_id)
        {
            foreach (DataRow row in table.Rows)
            {
                row[docId] = doc_id;
                row.EndEdit();
            }
        }

        public static SQLiteCommand CreateSelectCommand()
        {
            return CreateSelectCommand(null, null);
        }

        public static SQLiteCommand CreateSelectCommand(SQLiteConnection connection)
        {
            return CreateSelectCommand(connection, null);
        }

        public static SQLiteCommand CreateSelectCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.CommandText = SpecialPeriodView.GetSelectText() +
                               string.Format("WHERE {0} = {1} ORDER BY {2} ", docId, pDocId, salaryGroupsId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteCommand CreateInsertCommand()
        {
            return CreateInsertCommand(null, null);
        }

        public static SQLiteCommand CreateInsertCommand(SQLiteConnection connection)
        {
            return CreateInsertCommand(connection, null);
        }

        public static SQLiteCommand CreateInsertCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            //comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.Parameters.Add(new SQLiteParameter(pSalaryGroupsId, DbType.UInt64, salaryGroupsId));
            comm.Parameters.Add(new SQLiteParameter(pJanuary, DbType.Double, january));
            comm.Parameters.Add(new SQLiteParameter(pFebruary, DbType.Double, february));
            comm.Parameters.Add(new SQLiteParameter(pMarch, DbType.Double, march));
            comm.Parameters.Add(new SQLiteParameter(pApril, DbType.Double, april));
            comm.Parameters.Add(new SQLiteParameter(pMay, DbType.Double, may));
            comm.Parameters.Add(new SQLiteParameter(pJune, DbType.Double, june));
            comm.Parameters.Add(new SQLiteParameter(pJuly, DbType.Double, july));
            comm.Parameters.Add(new SQLiteParameter(pAugust, DbType.Double, august));
            comm.Parameters.Add(new SQLiteParameter(pSeptember, DbType.Double, september));
            comm.Parameters.Add(new SQLiteParameter(pOctober, DbType.Double, october));
            comm.Parameters.Add(new SQLiteParameter(pNovember, DbType.Double, november));
            comm.Parameters.Add(new SQLiteParameter(pDecember, DbType.Double, december));
            comm.Parameters.Add(new SQLiteParameter(pSum, DbType.Double, sum));

            comm.CommandText =
                string.Format(
                    @"INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}) VALUES ({16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30}); ",
                    tablename,
                //id,
                    docId,
                    salaryGroupsId,
                    january,
                    february,
                    march,
                    april,
                    may,
                    june,
                    july,
                    august,
                    september,
                    october,
                    november,
                    december,
                    sum,
                //pId,
                    pDocId,
                    pSalaryGroupsId,
                    pJanuary,
                    pFebruary,
                    pMarch,
                    pApril,
                    pMay,
                    pJune,
                    pJuly,
                    pAugust,
                    pSeptember,
                    pOctober,
                    pNovember,
                    pDecember,
                    pSum);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteCommand CreateReplaceCommand()
        {
            return CreateReplaceCommand(null, null);
        }

        public static SQLiteCommand CreateReplaceCommand(SQLiteConnection connection)
        {
            return CreateReplaceCommand(connection, null);
        }

        public static SQLiteCommand CreateReplaceCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.Parameters.Add(new SQLiteParameter(pSalaryGroupsId, DbType.UInt64, salaryGroupsId));
            comm.Parameters.Add(new SQLiteParameter(pJanuary, DbType.Double, january));
            comm.Parameters.Add(new SQLiteParameter(pFebruary, DbType.Double, february));
            comm.Parameters.Add(new SQLiteParameter(pMarch, DbType.Double, march));
            comm.Parameters.Add(new SQLiteParameter(pApril, DbType.Double, april));
            comm.Parameters.Add(new SQLiteParameter(pMay, DbType.Double, may));
            comm.Parameters.Add(new SQLiteParameter(pJune, DbType.Double, june));
            comm.Parameters.Add(new SQLiteParameter(pJuly, DbType.Double, july));
            comm.Parameters.Add(new SQLiteParameter(pAugust, DbType.Double, august));
            comm.Parameters.Add(new SQLiteParameter(pSeptember, DbType.Double, september));
            comm.Parameters.Add(new SQLiteParameter(pOctober, DbType.Double, october));
            comm.Parameters.Add(new SQLiteParameter(pNovember, DbType.Double, november));
            comm.Parameters.Add(new SQLiteParameter(pDecember, DbType.Double, december));
            comm.Parameters.Add(new SQLiteParameter(pSum, DbType.Double, sum));

            comm.CommandText =
                string.Format(
                    @"REPLACE INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}) VALUES ({17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32}); ",
                    tablename, id,
                    docId,
                    salaryGroupsId,
                    january,
                    february,
                    march,
                    april,
                    may,
                    june,
                    july,
                    august,
                    september,
                    october,
                    november,
                    december,
                    sum,
                    pId,
                    pDocId,
                    pSalaryGroupsId,
                    pJanuary,
                    pFebruary,
                    pMarch,
                    pApril,
                    pMay,
                    pJune,
                    pJuly,
                    pAugust,
                    pSeptember,
                    pOctober,
                    pNovember,
                    pDecember,
                    pSum);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteCommand CreateDeleteCommand()
        {
            return CreateDeleteCommand(null, null);
        }

        public static SQLiteCommand CreateDeleteCommand(SQLiteConnection connection)
        {
            return CreateDeleteCommand(connection, null);
        }

        public static SQLiteCommand CreateDeleteCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.CommandText = string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                tablename, id, pId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteDataAdapter CreateAdapter(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand(connection, transaction);
            adapter.InsertCommand = CreateInsertCommand(connection, transaction);
            adapter.UpdateCommand = CreateReplaceCommand(connection, transaction);
            adapter.DeleteCommand = CreateDeleteCommand(connection, transaction);
            return adapter;
        }

        public static string GetCopyText(long old_doc_id, long new_doc_id)
        {
            return
                string.Format(
                    @"INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15})
	SELECT {16}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15} FROM {0} WHERE {17} = {18}; SELECT LAST_INSERT_ROWID();",
                    tablename, docId, salaryGroupsId, january, february, march, april, may, june, july, august,
                    september, october, november, december, sum, new_doc_id, docId, old_doc_id);
        }

        public static int CopySalaryInfoByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection)
        {
            return CopySalaryInfoByDocId(old_doc_id, new_doc_id, connection, null);
        }

        public static int CopySalaryInfoByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(old_doc_id, new_doc_id), connection, transaction);
            return command.ExecuteNonQuery();
        }
        #endregion
    }

    public class PersonSalarySums
    {
        static public string tablename = "DocsSalaryInfoSumsView";
        
        #region Названия полей в представления БД 
        static public string docID = "doc_id";
        static public string docTypeID = Docs.docTypeID;
        static public string listID = Docs.listID;
        static public string personID = Docs.personID;
        static public string socNumber = PersonInfo.socNumber;
        static public string fio = "fio";
        static public string col1 = "col1";
        static public string col2 = "col2";
        static public string col3 = "col3";
        static public string col4 = "col4";
        static public string col5 = "col5";
        #endregion

        #region Параметры для полей таблицы
        static public string pDocID = "@doc_id";
        static public string pDocTypeID = "@" + Docs.docTypeID;
        static public string pListID = "@" + Docs.listID;
        static public string pPersonID = "@" + Docs.personID;
        static public string pSocNumber = "@" + PersonInfo.socNumber;
        static public string pFio = "@fio";
        static public string pCol1 = "@col1";
        static public string pCol2 = "@col2";
        static public string pCol3 = "@col3";
        static public string pCol4 = "@col4";
        static public string pCol5 = "@col5";
        #endregion

        #region
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(docID, typeof(long)).DefaultValue = 0;
            table.Columns.Add(docTypeID, typeof(long)).DefaultValue = 0;
            table.Columns.Add(listID, typeof(long)).DefaultValue = 0;
            table.Columns.Add(personID, typeof(long)).DefaultValue = 0.0;
            table.Columns.Add(socNumber, typeof(string)).DefaultValue = 0.0;
            table.Columns.Add(fio, typeof(string)).DefaultValue = 0.0;
            table.Columns.Add(col1, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(col2, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(col3, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(col4, typeof(double)).DefaultValue = 0.0;
            table.Columns.Add(col5, typeof(double)).DefaultValue = 0.0;
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0}", tablename);
        }

        static public string GetSelectText(long list_id)
        {
            return string.Format("{0} WHERE {1} = {2} ", GetSelectText(), listID, list_id);
        }

        static public string GetSelectText(IEnumerable<long> doc_type_id)
        {
            string instr = "( ";
            foreach (long val in doc_type_id)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";

            return string.Format("{0} WHERE {1} in {2}", GetSelectText(),docTypeID, instr);
        }

        static public string GetSelectText(long list_id, IEnumerable<long> doc_type_id)
        {
            string instr = "( ";
            foreach (long val in doc_type_id)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";

            return string.Format("{0} WHERE {1}={2} AND {3} in {4}", GetSelectText(), listID, list_id, docTypeID, instr);
        }

        static public string GetSelectText(long list_id, IEnumerable<long> doc_type_id, bool excludeAvoidSocnum)
        {
            string query = GetSelectText(list_id, doc_type_id);
            if (excludeAvoidSocnum)
            {
                query += string.Format(" AND ({0} is not null and length({0}) > 0)", socNumber);
            }
            return query;
        }

        static public DataTable GetSums(long list_id,IEnumerable<long> doc_type_id, string connectionStr)
        {
            DataTable table = CreateTable();
            string query = GetSelectText(list_id, doc_type_id);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connectionStr);
            adapter.Fill(table);
            return table;
        }

        static public DataTable GetSums(long list_id, IEnumerable<long> doc_type_id, string connectionStr, bool excludeAvoidSocnum)
        {
            DataTable table = CreateTable();
            string query = GetSelectText(list_id, doc_type_id, excludeAvoidSocnum);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connectionStr);
            adapter.Fill(table);
            return table;
        }

        #endregion
    }

    // класс виртуальной таблицы (ей нет прямой аналогии в БД)
    public class SalaryInfoTranspose
    {
        public static string tablename = "Salary_Info_Transpose";

        #region Поля

        public static readonly string month = "month";
        public static readonly string col1 = SalaryGroups.Column1.ToString();
        public static readonly string col2 = SalaryGroups.Column2.ToString();
        public static readonly string col3 = SalaryGroups.Column3.ToString();
        public static readonly string col4 = SalaryGroups.Column4.ToString();
        public static readonly string col5 = SalaryGroups.Column5.ToString();
        public static readonly string col6 = SalaryGroups.Column10.ToString();

        #endregion

        #region Методы - статические

        public static DataTable CreateTableWithRows()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(month, typeof(int));
            table.Columns.Add(col1, typeof(double));
            table.Columns.Add(col2, typeof(double));
            table.Columns.Add(col3, typeof(double));
            table.Columns.Add(col4, typeof(double));
            table.Columns.Add(col5, typeof(double));
            table.Columns.Add(col6, typeof(int));
            for (int i = 1; i < 13; i++)
            {
                DataRow row = table.NewRow();
                row[month] = i;
                row[col1] = 0.0;
                row[col2] = 0.0;
                row[col3] = 0.0;
                row[col4] = 0.0;
                row[col5] = 0.0;
                row[col6] = 0.0;
                row.EndEdit();
                table.Rows.Add(row);
            }
            return table;
        }

        public static void ConvertToSalaryInfo(DataTable salaryInfoTranspose, DataTable salaryInfo)
        {
            int[] iMonth = SalaryInfo.GetMonthIndexes(salaryInfo);
            DataRow[] salaryInfoRows = new DataRow[]
            {
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column1),
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column2),
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column3),
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column4),
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column5),
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column10)
            };
            for (int i = 0; i < iMonth.Length; i++)
            {
                salaryInfoRows[0][iMonth[i]] = salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col1];
                salaryInfoRows[1][iMonth[i]] = salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col2];
                salaryInfoRows[2][iMonth[i]] = salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col3];
                salaryInfoRows[3][iMonth[i]] = salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col4];
                salaryInfoRows[4][iMonth[i]] = salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col5];
                salaryInfoRows[5][iMonth[i]] = salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col6];
            }
        }

        public static void ConvertFromSalaryInfo(DataTable salaryInfoTranspose, DataTable salaryInfo)
        {
            int[] iMonth = SalaryInfo.GetMonthIndexes(salaryInfo);
            DataRow[] salaryInfoRows = new DataRow[]
            {
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column1),
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column2),
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column3),
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column4),
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column5),
                SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long) SalaryGroups.Column10)
            };
            for (int i = 0; i < iMonth.Length; i++)
            {
                salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col1] = salaryInfoRows[0][iMonth[i]];
                salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col2] = salaryInfoRows[1][iMonth[i]];
                salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col3] = salaryInfoRows[2][iMonth[i]];
                salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col4] = salaryInfoRows[3][iMonth[i]];
                salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col5] = salaryInfoRows[4][iMonth[i]];
                salaryInfoTranspose.Rows[i][SalaryInfoTranspose.col6] = salaryInfoRows[5][iMonth[i]];
            }
        }

        #endregion
    }

    public class GeneralPeriod
    {
        public static string tablename = "Gen_period";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string docId = "doc_id";
        public static string beginDate = "begin_date";
        public static string endDate = "end_date";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pDocId = "@doc_id";
        public static string pBeginDate = "@begin_date";
        public static string pEndDate = "@end_date";

        #endregion

        #region Методы - статические

        public static DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docId, typeof(long));
            table.Columns.Add(beginDate, typeof(DateTime));
            table.Columns.Add(endDate, typeof(DateTime));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ORDER BY {2}", docId, doc_id, beginDate);
        }

        public static string GetInsertText(long doc_id, DateTime begin_date, DateTime end_date)
        {
            return string.Format("INSERT INTO {0} ({1},{2},{3}) VALUES ({4},'{5}','{6}'); SELECT LAST_INSERT_ROWID();",
                tablename, docId, beginDate, endDate, doc_id, begin_date.ToString("yyyy-MM-dd"),
                end_date.ToString("yyyy-MM-dd"));
        }

        public static string GetDeleteTextById(long _id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, _id);
        }

        public static string GetDeleteTextByDocId(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, docId, doc_id);
        }

        public static void SetDocId(DataTable table, long doc_id)
        {
            foreach (DataRow row in table.Rows.Cast<DataRow>().Where(row => row.RowState != DataRowState.Deleted))
            {
                row[docId] = doc_id;
                row.EndEdit();
            }
        }

        public static SQLiteCommand CreateSelectCommand()
        {
            return CreateSelectCommand(null, null);
        }

        public static SQLiteCommand CreateSelectCommand(SQLiteConnection connection)
        {
            return CreateSelectCommand(connection, null);
        }

        public static SQLiteCommand CreateSelectCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.CommandText = GetSelectText() + string.Format("WHERE {0} = {1}", docId, pDocId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteCommand CreateReplaceCommand()
        {
            return CreateReplaceCommand(null, null);
        }

        public static SQLiteCommand CreateReplaceCommand(SQLiteConnection connection)
        {
            return CreateReplaceCommand(connection, null);
        }

        public static SQLiteCommand CreateReplaceCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.Parameters.Add(new SQLiteParameter(pBeginDate, DbType.Date, beginDate));
            comm.Parameters.Add(new SQLiteParameter(pEndDate, DbType.Date, endDate));
            comm.CommandText = string.Format(@"REPLACE INTO {0} ({1},{2},{3},{4}) VALUES ({5}, {6}, {7}, {8}); ",
                tablename, id, docId, beginDate, endDate, pId, pDocId, pBeginDate, pEndDate);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteCommand CreateDeleteCommand()
        {
            return CreateDeleteCommand(null, null);
        }

        public static SQLiteCommand CreateDeleteCommand(SQLiteConnection connection)
        {
            return CreateDeleteCommand(connection, null);
        }

        public static SQLiteCommand CreateDeleteCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.CommandText = string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                tablename, id, pId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteDataAdapter CreateAdapter(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand(connection, transaction);
            adapter.InsertCommand = CreateReplaceCommand(connection, transaction);
            adapter.UpdateCommand = CreateReplaceCommand(connection, transaction);
            adapter.DeleteCommand = CreateDeleteCommand(connection, transaction);
            return adapter;
        }

        public static string GetCopyText(long old_doc_id, long new_doc_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3})
	SELECT {4}, {2}, {3} FROM {0} WHERE {5} = {6}; SELECT LAST_INSERT_ROWID();",
                tablename, docId, beginDate, endDate, new_doc_id, docId, old_doc_id);
        }

        public static int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection)
        {
            return CopyPeriodByDocId(old_doc_id, new_doc_id, connection, null);
        }

        public static int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(old_doc_id, new_doc_id), connection, transaction);
            return command.ExecuteNonQuery();
        }

        public static string GetReplaceYearText(long doc_id, int old_year, int new_year)
        {
            return
                string.Format(
                    @"UPDATE {0} SET {1} = replace({1}, {2}, {3}), {4} = replace({4}, {2}, {3}) WHERE {5} = {6}) ",
                    tablename, beginDate, old_year, new_year, endDate, id, doc_id);
        }

        public static int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection)
        {
            return ReplaceYear(doc_id, old_year, new_year, connection, null);
        }

        public static int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetReplaceYearText(doc_id, old_year, new_year), connection,
                transaction);
            return command.ExecuteNonQuery();
        }

        #endregion
    }

    public class DopPeriod
    {
        public static string tablename = "Dop_period";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string docId = "doc_id";
        public static string classificatorId = "classificator_id";
        public static string beginDate = "begin_date";
        public static string endDate = "end_date";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pDocId = "@doc_id";
        public static string pClassificatorId = "@classificator_id";
        public static string pBeginDate = "@begin_date";
        public static string pEndDate = "@end_date";

        #endregion

        #region Методы - статические

        public static DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docId, typeof(long));
            table.Columns.Add(classificatorId, typeof(long));
            table.Columns.Add(beginDate, typeof(DateTime));
            table.Columns.Add(endDate, typeof(DateTime));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", docId, doc_id);
        }

        public static string GetInsertText(long doc_id, long classificator_id, DateTime begin_date, DateTime end_date)
        {
            return
                string.Format(
                    "INSERT INTO {0} ({1},{2},{3},{4}) VALUES ({5},{6},'{7}','{8}'); SELECT LAST_INSERT_ROWID();",
                    tablename, docId, classificatorId, beginDate, endDate, doc_id, classificator_id,
                    begin_date.ToString("yyyy-MM-dd"), end_date.ToString("yyyy-MM-dd"));
        }

        public static string GetDeleteTextById(long _id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, _id);
        }

        public static string GetDeleteTextByDocId(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, docId, doc_id);
        }

        public static void SetDocId(DataTable table, long doc_id)
        {
            foreach (DataRow row in table.Rows.Cast<DataRow>().Where(row => row.RowState != DataRowState.Deleted))
            {
                row[docId] = doc_id;
                row.EndEdit();
            }
        }

        public static SQLiteCommand CreateSelectCommand()
        {
            return CreateSelectCommand(null, null);
        }

        public static SQLiteCommand CreateSelectCommand(SQLiteConnection connection)
        {
            return CreateSelectCommand(connection, null);
        }

        public static SQLiteCommand CreateSelectCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.CommandText = DopPeriodView.GetSelectText() + string.Format("WHERE {0} = {1}", docId, pDocId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteCommand CreateReplaceCommand()
        {
            return CreateReplaceCommand(null, null);
        }

        public static SQLiteCommand CreateReplaceCommand(SQLiteConnection connection)
        {
            return CreateReplaceCommand(connection, null);
        }

        public static SQLiteCommand CreateReplaceCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.Parameters.Add(new SQLiteParameter(pClassificatorId, DbType.UInt64, classificatorId));
            comm.Parameters.Add(new SQLiteParameter(pBeginDate, DbType.Date, beginDate));
            comm.Parameters.Add(new SQLiteParameter(pEndDate, DbType.Date, endDate));
            comm.CommandText =
                string.Format(@"REPLACE INTO {0} ({1},{2},{3},{4},{5}) VALUES ({6}, {7}, {8}, {9}, {10}); ",
                    tablename, id, docId, classificatorId, beginDate, endDate, pId, pDocId, pClassificatorId, pBeginDate,
                    pEndDate);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteCommand CreateDeleteCommand()
        {
            return CreateDeleteCommand(null, null);
        }

        public static SQLiteCommand CreateDeleteCommand(SQLiteConnection connection)
        {
            return CreateDeleteCommand(connection, null);
        }

        public static SQLiteCommand CreateDeleteCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.CommandText = string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                tablename, id, pId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteDataAdapter CreateAdapter(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand(connection, transaction);
            adapter.InsertCommand = CreateReplaceCommand(connection, transaction);
            adapter.UpdateCommand = CreateReplaceCommand(connection, transaction);
            adapter.DeleteCommand = CreateDeleteCommand(connection, transaction);
            return adapter;
        }

        public static string GetCopyText(long old_doc_id, long new_doc_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3}, {4})
	SELECT {5}, {2}, {3}, {4} FROM {0} WHERE {6} = {7}; SELECT LAST_INSERT_ROWID();",
                tablename, docId, classificatorId, beginDate, endDate, new_doc_id, docId, old_doc_id);
        }

        public static int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection)
        {
            return CopyPeriodByDocId(old_doc_id, new_doc_id, connection, null);
        }

        public static int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(old_doc_id, new_doc_id), connection, transaction);
            return command.ExecuteNonQuery();
        }

        public static string GetReplaceYearText(long doc_id, int old_year, int new_year)
        {
            return
                string.Format(
                    @"UPDATE {0} SET {1} = replace({1}, {2}, {3}), {4} = replace({4}, {2}, {3}) WHERE {5} = {6}) ",
                    tablename, beginDate, old_year, new_year, endDate, id, doc_id);
        }

        public static int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection)
        {
            return ReplaceYear(doc_id, old_year, new_year, connection, null);
        }

        public static int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetReplaceYearText(doc_id, old_year, new_year), connection,
                transaction);
            return command.ExecuteNonQuery();
        }

        #endregion
    }

    public class DopPeriodView
    {
        public static string tablename = "Dop_period_View";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string docId = "doc_id";
        public static string classificatorId = "classificator_id";
        public static string code = "code";
        public static string beginDate = "begin_date";
        public static string endDate = "end_date";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docId, typeof(long));
            table.Columns.Add(classificatorId, typeof(long));
            table.Columns.Add(code, typeof(string));
            table.Columns.Add(beginDate, typeof(DateTime));
            table.Columns.Add(endDate, typeof(DateTime));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", docId, doc_id);
        }

        public static string GetDeleteTextById(long _id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, _id);
        }

        public static string GetDeleteTextByDocId(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, docId, doc_id);
        }

        #endregion
    }

    public class SpecialPeriod
    {
        public static string tablename = "Spec_period";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string docId = "doc_id";
        public static string partCondition = "part_condition";
        public static string stajBase = "staj_base";
        public static string servYearBase = "serv_year_base";
        public static string beginDate = "begin_date";
        public static string endDate = "end_date";
        public static string month = "month";
        public static string day = "day";
        public static string hour = "hour";
        public static string minute = "minute";
        public static string profession = "profession";

        #endregion

        #region Методы - статические

        public static DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docId, typeof(long));
            table.Columns.Add(partCondition, typeof(long));
            table.Columns.Add(stajBase, typeof(long));
            table.Columns.Add(servYearBase, typeof(long));
            table.Columns.Add(beginDate, typeof(DateTime));
            table.Columns.Add(endDate, typeof(DateTime));
            table.Columns.Add(month, typeof(int));
            table.Columns.Add(day, typeof(int));
            table.Columns.Add(hour, typeof(int));
            table.Columns.Add(minute, typeof(int));
            table.Columns.Add(profession, typeof(string));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ORDER BY {2}", docId, doc_id, beginDate);
        }

        public static string GetInsertText(long doc_id, long part_condition, long staj_base, long serv_year_base,
            DateTime begin_date, DateTime end_date, int _month, int _day, int _hour, int _minute, string _profession)
        {
            return
                string.Format(
                    "INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}) VALUES ({12},{13},{14},{15},'{16}','{17}',{18},{19},{20},{21},'{22}'); SELECT LAST_INSERT_ROWID();",
                    tablename, docId, partCondition, stajBase, servYearBase, beginDate, endDate, month, day, hour,
                    minute, profession, doc_id, part_condition, staj_base, serv_year_base,
                    begin_date.ToString("yyyy-MM-dd"), end_date.ToString("yyyy-MM-dd"), _month, _day, _hour, _minute,
                    _profession);
        }

        public static string GetDeleteTextById(long _id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, _id);
        }

        public static string GetDeleteTextByDocId(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, docId, doc_id);
        }

        public static string GetCopyText(long old_doc_id, long new_doc_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11})
	SELECT {12}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11} FROM {0} WHERE {13} = {14}; SELECT LAST_INSERT_ROWID();",
                tablename, docId, partCondition, stajBase, servYearBase, beginDate, endDate, month, day, hour, minute,
                profession, new_doc_id, docId, old_doc_id);
        }

        public static int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection)
        {
            return CopyPeriodByDocId(old_doc_id, new_doc_id, connection, null);
        }

        public static int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(old_doc_id, new_doc_id), connection, transaction);
            return command.ExecuteNonQuery();
        }

        public static string GetReplaceYearText(long doc_id, int old_year, int new_year)
        {
            return
                string.Format(
                    @"UPDATE {0} SET {1} = replace({1}, {2}, {3}), {4} = replace({4}, {2}, {3}) WHERE {5} = {6}) ",
                    tablename, beginDate, old_year, new_year, endDate, id, doc_id);
        }

        public static int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection)
        {
            return ReplaceYear(doc_id, old_year, new_year, connection, null);
        }

        public static int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetReplaceYearText(doc_id, old_year, new_year), connection,
                transaction);
            return command.ExecuteNonQuery();
        }

        #endregion
    }

    public class SpecialPeriodView
    {
        public static string tablename = "Spec_period_View";

        #region Названия полей в представления БД

        public static string id = "id";
        public static string docId = "doc_id";
        public static string partCondition = "part_condition";
        public static string partConditionClassificatorId = "part_condition_classificator_id";
        public static string partCode = "part_code";
        public static string stajBase = "staj_base";
        public static string stajBaseClassificatorId = "staj_base_classificator_id";
        public static string stajCode = "staj_code";
        public static string servYearBase = "serv_year_base";
        public static string servYearBaseClassificatorId = "serv_year_base_classificator_id";
        public static string servCode = "serv_code";
        public static string beginDate = "begin_date";
        public static string endDate = "end_date";
        public static string month = "month";
        public static string day = "day";
        public static string hour = "hour";
        public static string minute = "minute";
        public static string profession = "profession";

        #endregion

        #region Параметры для полей таблицы

        public static string pId = "@id";
        public static string pDocId = "@doc_id";
        public static string pPartCondition = "@part_condition";
        public static string pStajBase = "@staj_base";
        public static string pServYearBase = "@serv_year_base";
        public static string pBeginDate = "@begin_date";
        public static string pEndDate = "@end_date";
        public static string pMonth = "@month";
        public static string pDay = "@day";
        public static string pHour = "@hour";
        public static string pMinute = "@minute";
        public static string pProfession = "@profession";

        #endregion

        #region Методы - статические

        public static DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docId, typeof(long));
            table.Columns.Add(partCondition, typeof(long));
            table.Columns.Add(partConditionClassificatorId, typeof(long));
            table.Columns.Add(partCode, typeof(string));
            table.Columns.Add(stajBase, typeof(long));
            table.Columns.Add(stajBaseClassificatorId, typeof(long));
            table.Columns.Add(stajCode, typeof(string));
            table.Columns.Add(servYearBase, typeof(long));
            table.Columns.Add(servYearBaseClassificatorId, typeof(long));
            table.Columns.Add(servCode, typeof(string));
            table.Columns.Add(beginDate, typeof(DateTime));
            table.Columns.Add(endDate, typeof(DateTime));
            table.Columns.Add(month, typeof(int));
            table.Columns.Add(day, typeof(int));
            table.Columns.Add(hour, typeof(int));
            table.Columns.Add(minute, typeof(int));
            table.Columns.Add(profession, typeof(string));
            return table;
        }

        public static string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", docId, doc_id);
        }

        public static string GetDeleteTextById(long _id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, _id);
        }

        public static string GetDeleteTextByDocId(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", SpecialPeriod.tablename, SpecialPeriod.docId, doc_id);
        }

        public static void SetDocId(DataTable table, long doc_id)
        {
            foreach (DataRow row in table.Rows.Cast<DataRow>().Where(row => row.RowState != DataRowState.Deleted))
            {
                row[docId] = doc_id;
                row.EndEdit();
            }
        }

        public static SQLiteCommand CreateSelectCommand()
        {
            return CreateSelectCommand(null, null);
        }

        public static SQLiteCommand CreateSelectCommand(SQLiteConnection connection)
        {
            return CreateSelectCommand(connection, null);
        }

        public static SQLiteCommand CreateSelectCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.CommandText = SpecialPeriodView.GetSelectText() + string.Format("WHERE {0} = {1}", docId, pDocId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteCommand CreateReplaceCommand()
        {
            return CreateReplaceCommand(null, null);
        }

        public static SQLiteCommand CreateReplaceCommand(SQLiteConnection connection)
        {
            return CreateReplaceCommand(connection, null);
        }

        public static SQLiteCommand CreateReplaceCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.Parameters.Add(new SQLiteParameter(pPartCondition, DbType.UInt64, partCondition));
            comm.Parameters.Add(new SQLiteParameter(pStajBase, DbType.UInt64, stajBase));
            comm.Parameters.Add(new SQLiteParameter(pServYearBase, DbType.UInt64, servYearBase));
            comm.Parameters.Add(new SQLiteParameter(pBeginDate, DbType.DateTime, beginDate));
            comm.Parameters.Add(new SQLiteParameter(pEndDate, DbType.DateTime, endDate));
            comm.Parameters.Add(new SQLiteParameter(pMonth, DbType.UInt32, month));
            comm.Parameters.Add(new SQLiteParameter(pDay, DbType.UInt32, day));
            comm.Parameters.Add(new SQLiteParameter(pHour, DbType.UInt32, hour));
            comm.Parameters.Add(new SQLiteParameter(pMinute, DbType.UInt32, minute));
            comm.Parameters.Add(new SQLiteParameter(pProfession, DbType.String, profession));

            comm.CommandText =
                string.Format(
                    @"REPLACE INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}) VALUES ({13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}); ",
                    SpecialPeriod.tablename,
                    SpecialPeriod.id,
                    SpecialPeriod.docId,
                    SpecialPeriod.partCondition,
                    SpecialPeriod.stajBase,
                    SpecialPeriod.servYearBase,
                    SpecialPeriod.beginDate,
                    SpecialPeriod.endDate,
                    SpecialPeriod.month,
                    SpecialPeriod.day,
                    SpecialPeriod.hour,
                    SpecialPeriod.minute,
                    SpecialPeriod.profession,
                    pId,
                    pDocId,
                    pPartCondition,
                    pStajBase,
                    pServYearBase,
                    pBeginDate,
                    pEndDate,
                    pMonth,
                    pDay,
                    pHour,
                    pMinute,
                    pProfession);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteCommand CreateDeleteCommand()
        {
            return CreateDeleteCommand(null, null);
        }

        public static SQLiteCommand CreateDeleteCommand(SQLiteConnection connection)
        {
            return CreateDeleteCommand(connection, null);
        }

        public static SQLiteCommand CreateDeleteCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.CommandText = string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                SpecialPeriod.tablename, id, pId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        public static SQLiteDataAdapter CreateAdapter(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand(connection, transaction);
            adapter.InsertCommand = CreateReplaceCommand(connection, transaction);
            adapter.UpdateCommand = CreateReplaceCommand(connection, transaction);
            adapter.DeleteCommand = CreateDeleteCommand(connection, transaction);
            return adapter;
        }

        #endregion
    }

    public class Mergies
    {
        public static string tablename = "Mergies";

        #region Название полей таблицы в БД

        public static string id = "id";
        public static string orgID = "org_id";
        public static string repYear = "rep_year";
        public static string listCount = "list_count";
        public static string docCount = "doc_count";
        public static string actual = "actual";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(repYear, typeof(int));
            table.Columns.Add(listCount, typeof(int));
            table.Columns.Add(docCount, typeof(int));
            table.Columns.Add(actual, typeof(bool));
            return table;
        }

        public static DataRow CreateRow()
        {
            return CreateTable().NewRow();
        }

        public static string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long org_id)
        {
            return string.Format("{0} WHERE {1} = {2} ", GetSelectText(), orgID, org_id);
        }

        public static string GetSelectRowText(long row_id)
        {
            return string.Format("{0} WHERE {1} = {2} ", GetSelectText(), id, row_id);
        }

        public static DataRow GetRow(long row_id, string connectionStr)
        {
            DataTable table = Mergies.CreateTable();
            DataRow rowRes = null;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectRowText(row_id), connectionStr);
            adapter.Fill(table);
            if (table.Rows.Count > 0)
                rowRes = table.Rows[0];
            return rowRes;
        }

        public static string GetSelectActualText(long org_id, int rep_year)
        {
            return string.Format("{0} WHERE ({1}={2} AND {3}=1 AND {4}={5})", GetSelectText(), orgID, org_id, actual,
                repYear, rep_year);
        }

        public static DataRow GetActualRow(long org_id, int rep_year, string connectionStr)
        {
            DataTable table = Mergies.CreateTable();
            DataRow rowRes = null;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectActualText(org_id, rep_year), connectionStr);
            adapter.Fill(table);
            if (table.Rows.Count > 0)
                rowRes = table.Rows[0];
            return rowRes;
        }

        public static string GetInsertText(long org_id, int rep_year, int list_count, int doc_count)
        {
            return
                string.Format(
                    " INSERT INTO {0} ({1},{2},{3},{4}) VALUES ({5},{6},{7},{8}); SELECT last_insert_rowid(); ",
                    tablename, orgID, repYear, listCount, docCount,
                    org_id, rep_year, list_count, doc_count);
        }

        public static string GetUpdateText(long row_id, long org_id, int rep_year, int list_count, int doc_count)
        {
            return string.Format(" UPDATE {0} SET {1}={2},{3}={4},{5}={6},{7}={8} WHERE {9}={10} ",
                tablename,
                orgID, org_id,
                repYear, rep_year,
                listCount, list_count,
                docCount, doc_count,
                id, row_id);
        }

        public static string GetChangeActualText(long row_id, bool actual_value)
        {
            return string.Format("UPDATE {0} SET {1}={2} WHERE {3}={4}",
                tablename, actual, actual_value, id, row_id);
        }

        public static string GetChangeActualText(IEnumerable<long> row_id, bool actual_value)
        {
            string instr = "( ";
            foreach (long val in row_id)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";

            return string.Format("UPDATE {0} SET {1}={2} WHERE {3} in {4}",
                tablename, actual, actual_value, id, instr);
        }

        public static string GetChangeActualByOrgText(long org_id, int rep_year, bool actual_value)
        {
            return string.Format("UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} ",
                tablename, actual, Convert.ToInt32(actual_value), orgID, org_id, repYear, rep_year);
        }

        public static string GetDeleteText(long row_id)
        {
            return string.Format(" DELETE FROM {0} WHERE {1}={2} ", tablename, id, row_id);
        }

        public static string GetDeleteByOrgText(long org_id)
        {
            return string.Format(" DELETE FROM {0} WHERE {1}={2} ", tablename, orgID, org_id);
        }

        public static string GetDeleteByOrgText(long org_id, int rep_year)
        {
            return string.Format(" DELETE FROM {0} WHERE {1}={2} AND {3}={4} ", tablename, orgID, org_id, repYear,
                rep_year);
        }

        public static SQLiteCommand InsertCommand(DataRow mergeRow)
        {
            if (mergeRow == null)
                return null;
            SQLiteCommand comm = new SQLiteCommand();
            comm.CommandText = GetInsertText((long)mergeRow[orgID], (int)mergeRow[repYear], (int)mergeRow[listCount],
                (int)mergeRow[docCount]);
            return comm;
        }

        public static SQLiteCommand UpdateCommand(DataRow mergeRow)
        {
            if (mergeRow == null)
                return null;
            SQLiteCommand comm = new SQLiteCommand();
            comm.CommandText = GetUpdateText((long)mergeRow[id], (long)mergeRow[orgID], (int)mergeRow[repYear],
                (int)mergeRow[listCount], (int)mergeRow[docCount]);
            return comm;
        }

        public static SQLiteCommand DeleteCommand(DataRow mergeRow)
        {
            if (mergeRow == null)
                return null;
            SQLiteCommand comm = new SQLiteCommand();
            comm.CommandText = GetDeleteText((long)mergeRow[id]);
            return comm;
        }

        public static int DeleteExecute(DataRow mergeRow, string connectionStr)
        {
            SQLiteConnection con = new SQLiteConnection(connectionStr);
            SQLiteCommand comm = DeleteCommand(mergeRow);
            comm.Connection = con;
            con.Open();
            int res = comm.ExecuteNonQuery();
            con.Close();
            return res;
        }

        #endregion
    }

    public class MergiesView : Mergies
    {
        // название представления в БД
        public new static string tablename = "Mergies_View";

        #region Дополненные поля для представления

        public static string operName = "operator";
        public static string newDate = "new_date";
        public static string editDate = "edit_date";

        #endregion

        #region Методы - статические

        public new static DataTable CreateTable()
        {
            DataTable table = Mergies.CreateTable();
            table.Columns.Add(newDate, typeof(DateTime));
            table.Columns.Add(editDate, typeof(DateTime));
            table.Columns.Add(operName, typeof(string));
            return table;
        }

        public new static string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        public new static string GetSelectRowText(long row_id)
        {
            return string.Format("{0} WHERE {1}={2} ", GetSelectText(), id, row_id);
        }

        public new static string GetSelectText(long org_id)
        {
            return string.Format("{0} WHERE {1}={2} ", GetSelectText(), orgID, org_id);
        }

        public static string GetSelectText(long org_id, int rep_year)
        {
            return string.Format("{0} WHERE {1}={2} AND {3}={4} ", GetSelectText(), orgID, org_id, repYear, rep_year);
        }

        public new static string GetSelectActualText(long org_id, int rep_year)
        {
            return string.Format("{0} WHERE ({1}={2} AND {3}=1 AND {4}={5})", GetSelectText(), orgID, org_id, actual,
                repYear, rep_year);
        }

        public static string GetReplaceFixDataText(DataRow mergeRow, FixData.FixType type)
        {
            DateTime fixdate = (DateTime)(type == FixData.FixType.New
                ? mergeRow[MergiesView.newDate]
                : mergeRow[MergiesView.editDate]);
            return FixData.GetReplaceText(Mergies.tablename,
                type,
                (long)mergeRow[MergiesView.id],
                (string)mergeRow[MergiesView.operName],
                fixdate);
        }

        public new static DataRow GetRow(long row_id, string connectionStr)
        {
            DataTable table = Mergies.CreateTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectRowText(row_id), connectionStr);
            adapter.Fill(table);
            if (table.Rows.Count > 0)
                return table.Rows[0];
            return null;
        }

        #endregion
    }

    public class MergeInfo
    {
        public static string tablename = "Merge_Info";

        #region Названия полей в БД

        public static string id = "id";
        public static string mergeID = "merge_id";
        public static string groupID = "groups_id";
        public static string january = "january";
        public static string february = "february";
        public static string march = "march";
        public static string april = "april";
        public static string may = "may";
        public static string june = "june";
        public static string july = "july";
        public static string august = "august";
        public static string september = "september";
        public static string october = "october";
        public static string november = "november";
        public static string december = "december";
        public static string sum = "sum";

        #endregion

        #region Названия параметров для команд

        public static string pId = "@id";
        public static string pMergeID = "@merge_id";
        public static string pGroupID = "@groups_id";
        public static string pJanuary = "@january";
        public static string pFebruary = "@february";
        public static string pMarch = "@march";
        public static string pApril = "@april";
        public static string pMay = "@may";
        public static string pJune = "@june";
        public static string pJuly = "@july";
        public static string pAugust = "@august";
        public static string pSeptember = "@september";
        public static string pOctober = "@october";
        public static string pNovember = "@november";
        public static string pDecember = "@december";
        public static string pSum = "@sum";

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
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

        public static DataTable CreateTableWithRows()
        {
            DataTable table = CreateTable();
            DataRow row = table.NewRow();
            row[MergeInfo.groupID] = SalaryGroups.Column1;
            row.EndEdit();
            table.Rows.Add(row);
            row = table.NewRow();
            row[MergeInfo.groupID] = SalaryGroups.Column2;
            row.EndEdit();
            table.Rows.Add(row);
            row = table.NewRow();
            row[MergeInfo.groupID] = SalaryGroups.Column3;
            row.EndEdit();
            table.Rows.Add(row);
            row = table.NewRow();
            row[MergeInfo.groupID] = SalaryGroups.Column4;
            row.EndEdit();
            table.Rows.Add(row);
            row = table.NewRow();
            row[MergeInfo.groupID] = SalaryGroups.Column5;
            row.EndEdit();
            table.Rows.Add(row);
            row = table.NewRow();
            row[MergeInfo.groupID] = SalaryGroups.Column21;
            row.EndEdit();
            table.Rows.Add(row);
            return table;
        }

        public static DataTable CreateTableWithRows(long merge_id)
        {
            DataTable table = CreateTableWithRows();
            MergeInfo.SetMergeID(table, merge_id);
            return table;
        }

        public static DataTable GetTable(long merge_id, string connectionStr)
        {
            DataTable table = CreateTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectText(merge_id), connectionStr);
            adapter.Fill(table);
            return table;
        }

        public static void SetMergeID(DataTable mergeInfoTable, long merge_id)
        {
            foreach (DataRow row in mergeInfoTable.Rows)
            {
                row[MergeInfo.mergeID] = merge_id;
                row.EndEdit();
            }
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

        public static int[] GetMonthIndexes(DataTable mergeInfo)
        {
            return new int[]
            {
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
                mergeInfo.Columns[MergeInfo.december].Ordinal
                //mergeInfo.Columns[MergeInfo.sum].Ordinal,
            };
        }

        public static double GetSum(DataTable mergeInfo, long group_id)
        {
            DataRow row = Find(mergeInfo, MergeInfo.groupID, group_id);
            return (double)row[sum];
        }

        public static double MathSum(DataTable mergeInfo, long group_id)
        {
            double sum = 0;
            int[] months = GetMonthIndexes(mergeInfo);
            DataRow row = Find(mergeInfo, MergeInfo.groupID, group_id);
            foreach (int col in months)
                sum += (double)row[col];
            return Math.Round(sum, 2);
        }

        public static void MathSums(DataTable mergeInfo)
        {
            double sum;
            int[] months = GetMonthIndexes(mergeInfo);
            foreach (DataRow row in mergeInfo.Rows)
            {
                sum = 0;
                foreach (int col in months)
                    sum += (double)row[col];
                row[MergeInfo.sum] = Math.Round(sum, 2);
            }
        }

        public static string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long merge_id)
        {
            return string.Format("{0} WHERE {1} = {2} ORDER BY {3}", GetSelectText(), mergeID, merge_id, groupID);
        }

        public static string GetDeleteText(long merge_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1}={2}",
                tablename, mergeID, merge_id);
        }

        public static string GetDeleteText(IEnumerable<long> row_id)
        {
            string instr = "( ";
            foreach (long val in row_id)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";

            return string.Format("DELETE FROM {0} WHERE {1} in {2}",
                tablename, mergeID, instr);
        }

        public static void SetParametersTo(SQLiteCommand command)
        {
            if (command == null)
                return;
            command.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            command.Parameters.Add(new SQLiteParameter(pMergeID, DbType.UInt64, mergeID));
            command.Parameters.Add(new SQLiteParameter(pGroupID, DbType.UInt64, groupID));
            command.Parameters.Add(new SQLiteParameter(pJanuary, DbType.Double, january));
            command.Parameters.Add(new SQLiteParameter(pFebruary, DbType.Double, february));
            command.Parameters.Add(new SQLiteParameter(pMarch, DbType.Double, march));
            command.Parameters.Add(new SQLiteParameter(pApril, DbType.Double, april));
            command.Parameters.Add(new SQLiteParameter(pMay, DbType.Double, may));
            command.Parameters.Add(new SQLiteParameter(pJune, DbType.Double, june));
            command.Parameters.Add(new SQLiteParameter(pJuly, DbType.Double, july));
            command.Parameters.Add(new SQLiteParameter(pAugust, DbType.Double, august));
            command.Parameters.Add(new SQLiteParameter(pSeptember, DbType.Double, september));
            command.Parameters.Add(new SQLiteParameter(pOctober, DbType.Double, october));
            command.Parameters.Add(new SQLiteParameter(pNovember, DbType.Double, november));
            command.Parameters.Add(new SQLiteParameter(pDecember, DbType.Double, december));
            command.Parameters.Add(new SQLiteParameter(pSum, DbType.Double, sum));
        }

        public static SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            SetParametersTo(comm);
            comm.CommandText = string.Format(
                @" INSERT INTO {0} 
                                ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})
                                VALUES ({16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30});
                                SELECT last_insert_rowid();",
                tablename,
                mergeID, groupID, january, february, march, april, may, june, july, august, september, october, november,
                december, sum,
                pMergeID, pGroupID, pJanuary, pFebruary, pMarch, pApril, pMay, pJune, pJuly, pAugust, pSeptember,
                pOctober, pNovember, pDecember, pSum);
            return comm;
        }

        public static SQLiteCommand CreateUpdateCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            SetParametersTo(comm);
            comm.CommandText = string.Format(
                @" UPDATE {0} SET {1}={2},{3}={4},{5}={6},{7}={8},{9}={10},{11}={12},{13}={14},
                             {15}={16},{17}={18},{19}={20},{21}={22},{23}={24},{25}={26},{27}={28},{29}={30}
                            WHERE {31} = {32} ",
                tablename,
                mergeID, pMergeID,
                groupID, pGroupID,
                january, pJanuary,
                february, pFebruary,
                march, pMarch,
                april, pApril,
                may, pMay,
                june, pJune,
                july, pJuly,
                august, pAugust,
                september, pSeptember,
                october, pOctober,
                november, pNovember,
                december, pDecember,
                sum, pSum,
                id, pId);
            return comm;
        }

        public static SQLiteCommand CreateDeleteCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            SetParametersTo(comm);
            comm.CommandText = string.Format(@" DELETE FROM {0} WHERE {1} = {2} ", tablename, id, pId);
            return comm;
        }

        public static SQLiteCommand CreateDeleteCommand(long merge_id)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.CommandText = GetDeleteText(merge_id);
            return comm;
        }

        public static SQLiteCommand CreateDeleteCommand(DataRow mergeRow)
        {
            return CreateDeleteCommand((long)mergeRow[mergeID]);
        }

        #endregion
    }

    // класс виртуальной таблицы (ей нет прямой аналогии в БД)
    public class MergeInfoTranspose
    {
        public static string tablename = "Merge_Info_transpose";

        #region Поля виртуальной таблицы

        public static readonly string month = "month";
        public static readonly string col1 = SalaryGroups.Column1.ToString();
        public static readonly string col2 = SalaryGroups.Column2.ToString();
        public static readonly string col3 = SalaryGroups.Column3.ToString();
        public static readonly string col4 = SalaryGroups.Column4.ToString();
        public static readonly string col5 = SalaryGroups.Column5.ToString();
        public static readonly string col6 = SalaryGroups.Column21.ToString();

        #endregion

        #region Методы - статические

        public static DataTable CreateTable()
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
                row[col1] = 0;
                row[col2] = 0;
                row[col3] = 0;
                row[col4] = 0;
                row[col5] = 0;
                row[col6] = 0;
                row.EndEdit();
                table.Rows.Add(row);
            }
            return table;
        }

        public static void ConvertToMergeInfo(DataTable mergeInfoTranspose, DataTable mergeInfo)
        {
            int[] iMonth = MergeInfo.GetMonthIndexes(mergeInfo);
            DataRow[] mergeInfoRows =
            {
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column1),
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column2),
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column3),
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column4),
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column5),
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column21)
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

        public static void ConvertFromMergeInfo(DataTable mergeInfoTranspose, DataTable mergeInfo)
        {
            int[] iMonth = MergeInfo.GetMonthIndexes(mergeInfo);
            DataRow[] mergeInfoRows =
            {
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column1),
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column2),
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column3),
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column4),
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column5),
                MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long) SalaryGroups.Column21)
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

        public static double GetSum(DataTable mergeInfoTranspose, string col_name)
        {
            double sum = 0;
            foreach (DataRow row in mergeInfoTranspose.Rows)
                sum += (double)row[col_name];
            return sum;
        }

        #endregion
    }

    public class Tables
    {
        // название таблицы в БД
        public static string tablename = "Tables";

        #region Название полей таблицы в БД

        public static string id = "id";
        public static string name = "name";

        #endregion

        #region Методы - статические

        public static string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(long table_id)
        {
            return string.Format("{0} WHERE {1} = {2} ", GetSelectText(), id, table_id);
        }

        public static string GetSelectText(string table_name)
        {
            return string.Format("{0} WHERE {1} = '{2}' ", GetSelectText(), name, table_name);
        }

        public static string GetSelectIDText(string table_name)
        {
            return string.Format(" SELECT {1} FROM {0} WHERE {2} = '{3}' ", tablename, id, name, table_name);
        }

        public static long GetID(string table_name, string connectionStr)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand(GetSelectIDText(table_name), conn);
            conn.Open();
            object res = command.ExecuteScalar();
            conn.Close();
            return (long)res;
        }

        #endregion
    }

    public class FixData
    {
        public enum FixType
        {
            New = 0,
            Edit = 1
        }

        // название таблицы в БД
        public static string tablename = "Fixdata";

        #region Название полей таблицы в БД

        public static string id = "id";
        public static string type = "type";
        public static string tableID = "table_id";
        public static string rowID = "row_id";
        public static string oper = "operator";
        public static string fixDate = "fix_date";

        #endregion

        #region

        #endregion

        #region Методы - статические

        public static string GetSelectText()
        {
            return string.Format(" SELECT {0},{1},{2},{3} FROM {4} ", type, rowID, oper, fixDate, tablename);
        }

        public static string GetSelectText(string table_name)
        {
            return string.Format("{0} WHERE {1}={2} ", GetSelectText(), tableID, table_name);
        }

        public static string GetSelectText(string table_name, long row_id)
        {
            return string.Format("{0} AND {1}={2} ", GetSelectText(table_name), rowID, row_id);
        }

        public static string GetSelectText(string table_name, long row_id, FixType fix_type)
        {
            return string.Format("{0} AND {1}={2} ", GetSelectText(table_name, row_id), type, (int)fix_type);
        }

        public static string GetSelectIDText(FixType type, string table_name, long row_id)
        {
            return
                string.Format(
                    @"SELECT f.{0} FROM {1} f LEFT JOIN {2} t ON t.{3}=f.{4} AND t.{5}='{6}' WHERE f.type={7} AND row_id={8}",
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

        public static string GetReplaceText(string table_name, FixType fix_type, long row_id, string oper_name,
            DateTime fix_date)
        {
            return
                string.Format(
                    @" REPLACE INTO {0} ({1},{2},{3},{4},{5},{6}) VALUES (({7}),{8},({9}),{10},'{11}','{12}'); SELECT LAST_INSERT_ROWID();",
                    tablename,
                    id,
                    type, tableID, rowID, oper, fixDate,
                    GetSelectIDText(fix_type, table_name, row_id),
                    (int)fix_type, Tables.GetSelectIDText(table_name), row_id, oper_name,
                    fix_date.ToString("yyyy-MM-dd H:mm:ss")
                    );
        }

        public static string GetDeleteText(string table_name, long row_id)
        {
            return string.Format(" DELETE FROM {0} WHERE {1}={2} AND {3}={4} ", tablename, tableID,
                Tables.GetSelectIDText(table_name), rowID, row_id);
        }

        internal static long ExecReplaceText(string table_name, FixType fix_type, long row_id, string oper_name,
            DateTime fix_date, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(
                GetReplaceText(table_name, fix_type, row_id, oper_name, fix_date), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }

        #endregion
    }

    public class Backup
    {
        public static BackupCreate isBackupCreate = BackupCreate.None;

        public enum BackupCreate
        {
            Create = 0,
            None = 1,
            DoNotCreate = 2
        }

        public static string columnDateTimeName = "dateTime";
        public static string columnPathName = "path";

        public enum TypeBackup
        {
            Auto = 0,
            ManualBackup = 1,
            RestoreBackup = 2
        }

        public enum SearchPatternType
        {
            AutoBackup = 0,
            ManualBackup = 1,
            RestoreBackup = 2,
            All = 3
        }

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(columnDateTimeName, typeof(string));
            table.Columns.Add(columnPathName, typeof(string));
            return table;
        }

        public static void FillTable(DataTable table, DirectoryInfo directoryInfo, SearchPatternType searchPatternType)
        {
            foreach (FileInfo backupFile in GetBackupFilesInDirectory(directoryInfo, searchPatternType))
            {
                DataRow row = table.NewRow();
                row[columnDateTimeName] = ExtractDateTimeFromBackupName(backupFile.Name);
                row[columnPathName] = backupFile.Name;
                table.Rows.Add(row);
            }
        }

        private static IEnumerable<FileInfo> GetBackupFilesInDirectory(DirectoryInfo directoryInfo,
            SearchPatternType searchPatternType)
        {
            string searchPattern;
            switch (searchPatternType)
            {
                case SearchPatternType.AutoBackup:
                    searchPattern = "pu_bkp_????-??-??_(??-??-??).zip";
                    break;
                case SearchPatternType.ManualBackup:
                    searchPattern = "pu_bkp_????-??-??_(??-??-??)_manual.zip";
                    break;
                case SearchPatternType.RestoreBackup:
                    searchPattern = "pu_bkp_????-??-??_(??-??-??)_restore.zip";
                    break;
                case SearchPatternType.All:
                    searchPattern = "pu_bkp_????-??-??_(??-??-??)*.zip";
                    break;
                default:
                    searchPattern = "*.zip";
                    break;
            }
            return
                directoryInfo.GetFiles(searchPattern, SearchOption.TopDirectoryOnly).OrderBy(f => f.Name).AsEnumerable();
        }

        public static string ExtractDateTimeFromBackupName(string fileName)
        {
            string result = fileName.Substring(7, 4);
            result += "." + fileName.Substring(12, 2);
            result += "." + fileName.Substring(15, 2);
            result += "   " + fileName.Substring(19, 2);
            result += ":" + fileName.Substring(22, 2);
            result += ":" + fileName.Substring(25, 2);

            return result;
        }

        //public delegate void ProgressDelegate(object sender, AddProgressEventArgs e);

        //public static ProgressDelegate AddProgress;

        public static void CreateBackup(string backupFolderPath, string databaseFilePath, TypeBackup type)
        {
            string backupFileName = GenerateNameString(type);

            using (ZipFile zip = new ZipFile())
            {
                //zip.AddProgress += new EventHandler<AddProgressEventArgs>(AddProgress);
                //zip.AddProgress += new EventHandler<AddProgressEventArgs>(zip_AddProgress);
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                zip.AddDirectoryWillTraverseReparsePoints = false;
                zip.Comment =
                    "Fai`l rezervnoi` kopii bazy` danny`kh dlia programmy` \"Personifitcirovanny`i` uchet dlia organizatcii`\".";
                zip.AddFile(databaseFilePath, "");
                zip.Save(Path.Combine(backupFolderPath, backupFileName));
            }

            //Очистка от лишних архивов
        }

        private static string GenerateNameString(TypeBackup type)
        {
            string backupFileName;
            switch (type)
            {
                case TypeBackup.Auto:
                    backupFileName = DateTime.Now.ToString("pu_bkp_yyyy-MM-dd_(HH-mm-ss)") + ".zip";
                    break;
                case TypeBackup.ManualBackup:
                    backupFileName = DateTime.Now.ToString("pu_bkp_yyyy-MM-dd_(HH-mm-ss)") + "_manual.zip";
                    break;
                case TypeBackup.RestoreBackup:
                    backupFileName = DateTime.Now.ToString("pu_bkp_yyyy-MM-dd_(HH-mm-ss)") + "_restore.zip";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return backupFileName;
        }

        //static void zip_AddProgress(object sender, AddProgressEventArgs e)
        //{
        //    Console.Write(e.BytesTransferred);
        //}

        public static void RestoreBackup(string backupFolderPath, string backupFileName, string databaseFilePath)
        {
            if (File.Exists(databaseFilePath))
                CreateBackup(backupFolderPath, databaseFilePath, TypeBackup.RestoreBackup);
            string databaseFolderPath = databaseFilePath.Substring(0, databaseFilePath.LastIndexOf("/"));
            using (ZipFile zip = ZipFile.Read(Path.Combine(backupFolderPath, backupFileName)))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(databaseFolderPath, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        public static void DeleteOldBackups(string backupFolderPath, int maxCount, TypeBackup type)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(backupFolderPath);
            IEnumerable<FileInfo> fileInfo = GetBackupFilesInDirectory(directoryInfo, SearchPatternType.AutoBackup);
            for (int i = 0; i < fileInfo.Count() - maxCount; i++)
            {
                fileInfo.ElementAt(i).Delete();
            }
        }
    }

    public class Options
    {
        // название таблицы в БД
        public static string tablename = "Options";

        #region Название полей таблицы в БД

        public static string key = "key";
        public static string value = "value";

        #endregion

        #region Имена ключей (возможно не все)

        public static string isFirstLoginKeyName = "isFirstLogin";
        public static string dbVersionKeyName = "dbVersion";

        #endregion

        #region Методы - статические

        public static string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        public static string GetSelectText(string key_name)
        {
            return string.Format("{0} WHERE {1}='{2}' ", GetSelectText(), key, key_name);
        }

        public static string GetSelectKeyValueText(string key_name)
        {
            return string.Format("SELECT {0} FROM {1} WHERE {2}='{3}' ", value, tablename, key, key_name);
        }

        public static string GetReplaceText(string key_name, string key_value)
        {
            return string.Format(@" REPLACE INTO {0} ({1},{2}) VALUES ('{3}','{4}') ",
                tablename, key, value, key_name, key_value);
        }

        public static string GetDeleteText(string key_name)
        {
            return string.Format(" DELETE FROM {0} WHERE {1}='{2}' ", tablename, key, key_name);
        }

        public static object GetKeyValue(string key_name, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetSelectKeyValueText(key_name), connection, transaction);
            return command.ExecuteScalar();
        }

        public static int ChangeKeyValue(string key_name, string key_value, SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetReplaceText(key_name, key_value), connection, transaction);
            return command.ExecuteNonQuery();
        }

        #endregion
    }
}