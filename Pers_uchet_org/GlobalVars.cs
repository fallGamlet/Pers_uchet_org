using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;

namespace Pers_uchet_org
{
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
            Form webForm = new Form { Width = 850, Height = 600 };
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

        static public void ShowWebPage(WebBrowser wb, XmlData.ReportType type)
        {
            string url = XmlData.GetReportUrl(type);
            if (url != null)
            {
                ShowWebPage(wb, url);
            }
            else
            {
                MainForm.ShowWarningMessage("Не удалось найти файл отчета!", "Внимание");
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
            comm.CommandText = string.Format(@"INSERT INTO [{0}] ({1}, {2}) VALUES ({3}, {4});
                                               SELECT last_insert_rowid(); ",
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

        static public void InsertPersonOrg(List<long> person_idArr, long org_id, SQLiteConnection connection)
        {
            InsertPersonOrg(person_idArr, org_id, connection, null);
        }

        static public int InsertPersonOrg(List<long> person_idArr, long org_id, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string commantText = String.Empty;
            foreach (long person_id in person_idArr)
                commantText += GetInsertPersonOrgText(person_id, org_id) + "; \n";

            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(commantText, connection, transaction);
            return command.ExecuteNonQuery();
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

    public class IndDocs
    {
        // название таблицы в БД
        static public string tablename = "IndDocs";

        #region названия полей таблицы в БД
        static public string id = "id";
        static public string docId = "doc_id";
        static public string classpercentId = "classpercent_id";
        static public string isGeneral = "is_general";
        static public string citizen1Id = "citizen1_id";
        static public string citizen2Id = "citizen2_id";
        #endregion

        public enum Job { General = 1, Second = 2 };

        #region Методы - статические
        static public DataTable CreateTable()
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

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long doc_id)
        {
            return string.Format(GetSelectText() + "WHERE {0} = {1} ", docId, doc_id);
        }

        static public string GetReplaceText(long doc_id, long classpercent_id, int is_general, long citizen1_id, long citizen2_id)
        {
            return string.Format("REPLACE INTO {0} ({1},{2},{3},{4},{5},{6}) VALUES (({7}),{8},{9},{10},{11},{12}); SELECT LAST_INSERT_ROWID(); ", tablename, IndDocs.id, docId, classpercentId, isGeneral, citizen1Id, citizen2Id, GetSelectIDText(tablename, doc_id), doc_id, classpercent_id, is_general, citizen1_id, citizen2_id);
        }

        static private string GetSelectIDText(string table_name, long doc_id)
        {
            return string.Format(@"SELECT DISTINCT {0} FROM {1} WHERE {2} = {3} ",
                                    id,
                                    tablename,
                                    docId,
                                    doc_id);
        }

        static public string GetCopyText(long oldDocId, long newDocId)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3}, {4}, {5})
	SELECT {6}, {2}, {3}, {4}, {5} FROM {0} WHERE {7} = {8}; SELECT LAST_INSERT_ROWID();",
                                tablename, docId, classpercentId, isGeneral, citizen1Id, citizen2Id, newDocId, docId, oldDocId);
        }

        static public int CopyIndDocByDocId(long oldDocId, long newDocId, SQLiteConnection connection)
        {
            return CopyIndDocByDocId(oldDocId, newDocId, connection, null);
        }

        static public int CopyIndDocByDocId(long oldDocId, long newDocId, SQLiteConnection connection, SQLiteTransaction transaction)
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
        static public string isAgriculture = "is_agriculture";
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
            table.Columns.Add(obligatoryIsEnabled, typeof(int));
            table.Columns.Add(isAgriculture, typeof(int));
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
        static public string isAgriculture = "is_agriculture";
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
            table.Columns.Add(obligatoryIsEnabled, typeof(int));
            table.Columns.Add(isAgriculture, typeof(int));
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

    public class ListTypes
    {
        static public string tablename = "List_Types";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string name = "name";
        static public string dateBegin = "date_begin";
        static public string dateEnd = "date_end";
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
        static public string tablename = "Lists_View";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string listTypeId = "list_type_id";
        static public string nameType = "name";
        static public string orgID = "org_id";
        static public string operatorNameReg = "name_reg";
        static public string regDate = "reg_date";
        static public string operatorNameChange = "name_change";
        static public string changeDate = "change_date";
        static public string repYear = "rep_year";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
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

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
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
        static public string repYear = "rep_year";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pListTypeId = "@list_type_id";
        static public string pOrgID = "@org_id";
        static public string pRepYear = "@rep_year";
        #endregion

        #region Методы - статические
        static public SQLiteCommand CreateInsertCommand()
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

        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(listTypeId, typeof(int));
            table.Columns.Add(orgID, typeof(long));
            table.Columns.Add(repYear, typeof(int));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0}", tablename);
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
        /// <param name="rep_year"></param>
        /// <returns></returns>
        static public string GetInsertText(long list_type_id, long org_id, int rep_year)
        {
            return string.Format("INSERT INTO {0} ({1},{2},{3}) VALUES ({4},{5},{6}); SELECT LAST_INSERT_ROWID(); ", tablename, listTypeId, orgID, repYear, list_type_id, org_id, rep_year);
        }

        static public string GetDeleteText(long list_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, list_id);
        }

        static public string GetUpdateYearText(long list_id, int new_rep_year)
        {
            return string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}", tablename, repYear, new_rep_year, id, list_id);
        }

        static public long UpdateYear(long list_id, int new_rep_year, SQLiteConnection connection)
        {
            return UpdateYear(list_id, new_rep_year, connection, null);
        }

        static public long UpdateYear(long list_id, int new_rep_year, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetUpdateYearText(list_id, new_rep_year), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }

        static public string GetUpdateOrgText(long list_id, long new_org_id)
        {
            return string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}", tablename, orgID, new_org_id, id, list_id);
        }

        static public string GetSelectPersonIdsText(long list_id)
        {
            return string.Format("SELECT person_id FROM Docs WHERE list_id = {0}", list_id);
        }

        static public string GetCopyText(long old_list_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3})
	SELECT {1}, {2}, {3} FROM {0} WHERE {4} = {5}; SELECT LAST_INSERT_ROWID();",
                                tablename, listTypeId, orgID, repYear, id, old_list_id);
        }

        static public long CopyListById(long old_list_id, SQLiteConnection connection)
        {
            return CopyListById(old_list_id, connection, null);
        }

        static public long CopyListById(long old_list_id, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(old_list_id), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }

        static public string GetListsIdsText(long org_id, int rep_year, long doc_type, long person_id, long classpercent_id, IndDocs.Job job, long cur_doc_id)
        {
            return string.Format(@"SELECT DISTINCT l.{0} as id
                                    FROM {1} l
                                    INNER JOIN {2} d ON d.{3} = l.{0}
                                    INNER JOIN {4} id ON id.{5} = d.{6}
                                    WHERE {7} = {8} AND {9} = {10} AND {11} = {12} AND {13} = {14} AND {15} = {16} AND {17} = {18} AND d.{6} <> {19} ",
                               id, tablename, Docs.tablename, Docs.listId, IndDocs.tablename, IndDocs.docId, Docs.id, orgID, org_id, repYear, rep_year, Docs.docTypeId, doc_type, Docs.personID, person_id, IndDocs.classpercentId, classpercent_id, IndDocs.isGeneral, (int)job, cur_doc_id);
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
        static public string code = "code";
        static public string operNameReg = "name_reg";
        static public string regDate = "reg_date";
        static public string operNameChange = "name_change";
        static public string changeDate = "change_date";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
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

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectTextByListId(long list_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ORDER BY {2} ", listId, list_id, fio);
        }

        static public string GetSelectTextByDocId(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ", id, doc_id);
        }
        #endregion
    }

    public class DocsViewForXml
    {
        static public string tablename = "Docs_View_For_Xml";

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
        static public DataTable CreateTable()
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

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectTextByListId(long list_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ORDER BY {2} ", listId, list_id, lName);
        }

        static public string GetSelectTextByDocId(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ", id, doc_id);
        }

        static public DataRow GetRow(long doc_id, string connection_str)
        {
            DataTable table = DocsViewForXml.CreateTable();
            DataRow rowRes = null;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectTextByDocId(doc_id), connection_str);
            adapter.Fill(table);
            if (table.Rows.Count > 0)
                rowRes = table.Rows[0];
            return rowRes;
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
        #endregion

        #region Методы - статические
        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docTypeId, typeof(int));
            table.Columns.Add(listId, typeof(long));
            table.Columns.Add(personID, typeof(long));
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

        static public int UpdateDocTypeByDocId(List<long> doc_idArr, long new_doc_type_id, SQLiteConnection connection)
        {
            return UpdateDocTypeByDocId(doc_idArr, new_doc_type_id, connection, null);
        }

        static public int UpdateDocTypeByDocId(List<long> doc_idArr, long new_doc_type_id, SQLiteConnection connection, SQLiteTransaction transaction)
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

        static public int UpdateDocTypeByListId(long list_id, long new_doc_type_id, SQLiteConnection connection)
        {
            return UpdateDocTypeByListId(list_id, new_doc_type_id, connection, null);
        }

        static public int UpdateDocTypeByListId(long list_id, long new_doc_type_id, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string commantText = GetUpdateDocTypeByListText(list_id, new_doc_type_id);

            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(commantText, connection, transaction);
            return command.ExecuteNonQuery();
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

        static public int UpdateListId(long doc_id, long new_list_id, SQLiteConnection connection)
        {
            return UpdateListId(doc_id, new_list_id, connection, null);
        }

        static public int UpdateListId(long doc_id, long new_list_id, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string commantText = GetUpdateListIdText(doc_id, new_list_id);

            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(commantText, connection, transaction);
            return command.ExecuteNonQuery();
        }

        static public string GetInsertText(long doc_type_id, long list_id, long person_id)
        {
            return string.Format("INSERT INTO {0} ({1},{2},{3}) VALUES ({4},{5},{6}); SELECT LAST_INSERT_ROWID();", tablename, docTypeId, listId, personID, doc_type_id, list_id, person_id);
        }

        static public string GetCopyText(long doc_id, long list_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3})
	SELECT {1}, {6}, {3} FROM {0} WHERE {4} = {5}; SELECT LAST_INSERT_ROWID();",
                                tablename, docTypeId, listId, personID, id, doc_id, list_id);
        }

        static public long CopyDocByDocId(long doc_id, long list_id, SQLiteConnection connection)
        {
            return CopyDocByDocId(doc_id, list_id, connection, null);
        }

        static public long CopyDocByDocId(long doc_id, long list_id, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string commantText = String.Empty;

            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(doc_id, list_id), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }

        static public string GetCountDocsText(long list_id, long doc_type)
        {
            return string.Format(@"SELECT COUNT({0}) FROM {1} WHERE {2} = {3} AND {4} = {5} ",
                                id, tablename, listId, list_id, docTypeId, doc_type);
        }

        static public long CountDocsInList(long list_id, long doc_type, SQLiteConnection connection)
        {
            return CountDocsInList(list_id, doc_type, connection, null);
        }

        static public long CountDocsInList(long list_id, long doc_type, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCountDocsText(list_id, doc_type), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }

        static public string GetCountDocsByYearText(long year, long person_id)
        {
            return string.Format(@"SELECT COUNT({0}) FROM {1} WHERE {2} = {3} AND {4} IN (SELECT {5} FROM {6} WHERE {7} = {8}) ",
                                id, tablename, personID, person_id, listId, Lists.id, Lists.tablename, Lists.repYear, year);
        }

        static public long CountDocsByYear(long year, long person_id, SQLiteConnection connection)
        {
            return CountDocsByYear(year, person_id, connection, null);
        }

        static public long CountDocsByYear(long year, long person_id, SQLiteConnection connection, SQLiteTransaction transaction)
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
        static public string GetSelectCountText(long list_id, long docType_id)
        {
            return string.Format("SELECT count(distinct {0}) as [count] FROM {1} WHERE {2}={3} AND {4}={5} ",
                                id, tablename, listId, list_id, docTypeId, docType_id);
        }

        /// <summary>
        /// Получить текст запроса на выборку сгруппированных количеств документов по типам документов
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <returns></returns>
        static public string GetSelectCountText(long list_id)
        {
            return string.Format(@"SELECT d.{0}, count(distinct d.{1}) as [count] 
                                    FROM {2} d
                                    INNER JOIN {3} dt ON d.{0} = dt.{4} and dt.{5} = 2
                                    WHERE {6} = {7}
                                    GROUP BY {0}",
                                docTypeId, id, 
                                tablename, 
                                DocTypes.tablename, DocTypes.id, DocTypes.listTypeId, 
                                listId, list_id);
        }

        /// <summary>
        /// Получить значение количества документов в указанном пакете с указанным типом документа
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <param name="docType_id">идентификатор типа документа</param>
        /// <param name="connectionStr">строка подключения к БД</param>
        /// <returns></returns>
        static public long CountDocsByListAndType(long list_id, long docType_id, string connectionStr)
        {
            long res;
            SQLiteConnection connection = new SQLiteConnection(connectionStr);
            SQLiteCommand command = new SQLiteCommand(GetSelectCountText(list_id, docType_id), connection);
            connection.Open();
            res = (long)command.ExecuteScalar();
            connection.Close();
            return res;
        }

        /// <summary>
        /// Получить таблицу с типами документов и количествами этих типов документов
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <param name="connectionStr">строка подключения к БД</param>
        /// <returns>Таблицы со столбцами Тип документа, Количество документов ("count")</returns>
        static public DataTable CountDocsByListAndType(long list_id, string connectionStr)
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(docTypeId, typeof(long));
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
        static public string GetSumsByDocTypeText(long list_id)
        {
            return string.Format(@"SELECT d.{0}, si.{1}, sum(si.{2}) as {2}
                                FROM {3} d INNER JOIN {4} si ON si.{5} = d.{6} and si.{1} in (1,2,3,4,5)
                                WHERE d.{7} = {8}
                                GROUP BY d.{0}, si.{1}
                                ORDER BY d.{0}, si.{1}",
                                docTypeId, SalaryInfo.salaryGroupsId, SalaryInfo.sum,
                                tablename, SalaryInfo.tablename, SalaryInfo.docId, id,
                                listId, list_id);
        }

        /// <summary>
        /// Получить таблицу с выборкой сумм, сгруппированных по типам документов и группам сумм (1,2,3,4,5)
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <param name="connectionStr">строка подключения к БД</param>
        /// <returns>Таблица с столбцами: Тип документа, Группа суммы, Сумма</returns>
        static public DataTable SumsByDocType(long list_id, string connectionStr)
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(docTypeId, typeof(long));
            table.Columns.Add(SalaryInfo.salaryGroupsId, typeof(long));
            table.Columns.Add(SalaryInfo.sum, typeof(double));

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSumsByDocTypeText(list_id), connectionStr);
            adapter.Fill(table);
            return table;
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
        static public string sum = "sum";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pDocId = "@doc_id";
        static public string pSalaryGroupsId = "@salary_groups_id";
        static public string pJanuary = "@january";
        static public string pFebruary = "@february";
        static public string pMarch = "@march";
        static public string pApril = "@april";
        static public string pMay = "@may";
        static public string pJune = "@june";
        static public string pJuly = "@july";
        static public string pAugust = "@august";
        static public string pSeptember = "@september";
        static public string pOctober = "@october";
        static public string pNovember = "@november";
        static public string pDecember = "@december";
        static public string pSum = "@sum";
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
            table.Columns.Add(sum, typeof(double));
            return table;
        }

        static public DataTable CreateTableWithRows()
        {
            DataTable table = CreateTable();
            DataRow row;
            row = table.NewRow();
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

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0}", tablename);
        }

        static public string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ORDER BY {2}", docId, doc_id, salaryGroupsId);
        }

        /// <summary>
        /// Получить текст запроса на выборку таблицы Salary_info с суммами выплат
        /// </summary>
        /// <param name="list_id">Перечисление идентификаторов Пакетов</param>
        /// <param name="doc_type_id">Перечисление идентификаторов Типов документов</param>
        /// <returns>В выбираемой таблице в столбце doc_id получается количество документов, а не их идентификаторы</returns>
        static public string GetSelectText(IEnumerable<long> list_id, IEnumerable<long> doc_type_id)
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
                            ,COUNT({14}) as {14}
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
            return new int[] {
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
                            salaryInfo.Columns[SalaryInfo.december].Ordinal,
                            //salaryInfo.Columns[SalaryInfo.sum].Ordinal,
                            };
        }

        static public void SetDocId(DataTable table, long doc_id)
        {
            foreach (DataRow row in table.Rows)
            {
                row[docId] = doc_id;
                row.EndEdit();
            }
        }

        static public SQLiteCommand CreateSelectCommand()
        {
            return CreateSelectCommand(null, null);
        }

        static public SQLiteCommand CreateSelectCommand(SQLiteConnection connection)
        {
            return CreateSelectCommand(connection, null);
        }

        static public SQLiteCommand CreateSelectCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.CommandText = SpecialPeriodView.GetSelectText() + string.Format("WHERE {0} = {1} ORDER BY {2} ", docId, pDocId, salaryGroupsId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        static public SQLiteCommand CreateReplaceCommand()
        {
            return CreateReplaceCommand(null, null);
        }

        static public SQLiteCommand CreateReplaceCommand(SQLiteConnection connection)
        {
            return CreateReplaceCommand(connection, null);
        }

        static public SQLiteCommand CreateReplaceCommand(SQLiteConnection connection, SQLiteTransaction transaction)
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

            comm.CommandText = string.Format(@"REPLACE INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}) VALUES ({17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32}); ",
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

        static public SQLiteCommand CreateDeleteCommand()
        {
            return CreateDeleteCommand(null, null);
        }

        static public SQLiteCommand CreateDeleteCommand(SQLiteConnection connection)
        {
            return CreateDeleteCommand(connection, null);
        }

        static public SQLiteCommand CreateDeleteCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.CommandText = string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                                                tablename, id, pId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        static public SQLiteDataAdapter CreateAdapter(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand(connection, transaction);
            adapter.InsertCommand = CreateReplaceCommand(connection, transaction);
            adapter.UpdateCommand = CreateReplaceCommand(connection, transaction);
            adapter.DeleteCommand = CreateDeleteCommand(connection, transaction);
            return adapter;
        }

        static public string GetCopyText(long old_doc_id, long new_doc_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15})
	SELECT {16}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15} FROM {0} WHERE {17} = {18}; SELECT LAST_INSERT_ROWID();",
                                tablename, docId, salaryGroupsId, january, february, march, april, may, june, july, august, september, october, november, december, sum, new_doc_id, docId, old_doc_id);
        }

        static public int CopySalaryInfoByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection)
        {
            return CopySalaryInfoByDocId(old_doc_id, new_doc_id, connection, null);
        }

        static public int CopySalaryInfoByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(old_doc_id, new_doc_id), connection, transaction);
            return command.ExecuteNonQuery();
        }
        #endregion
    }

    // класс виртуальной таблицы (ей нет прямой аналогии в БД)
    public class SalaryInfoTranspose
    {
        static public string tablename = "Salary_Info_Transpose";

        #region Поля
        readonly static public string month = "month";
        readonly static public string col1 = SalaryGroups.Column1.ToString();
        readonly static public string col2 = SalaryGroups.Column2.ToString();
        readonly static public string col3 = SalaryGroups.Column3.ToString();
        readonly static public string col4 = SalaryGroups.Column4.ToString();
        readonly static public string col5 = SalaryGroups.Column5.ToString();
        readonly static public string col6 = SalaryGroups.Column10.ToString();
        #endregion

        #region Методы - статические
        static public DataTable CreateTableWithRows()
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

        static public void ConvertToSalaryInfo(DataTable salaryInfoTranspose, DataTable salaryInfo)
        {
            int[] iMonth = SalaryInfo.GetMonthIndexes(salaryInfo);
            DataRow[] salaryInfoRows = new DataRow[] {
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column1),
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column2),
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column3),
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column4),
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column5),
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column10)
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

        static public void ConvertFromSalaryInfo(DataTable salaryInfoTranspose, DataTable salaryInfo)
        {
            int[] iMonth = SalaryInfo.GetMonthIndexes(salaryInfo);
            DataRow[] salaryInfoRows = new DataRow[] {
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column1),
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column2),
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column3),
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column4),
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column5),
                            SalaryInfo.Find(salaryInfo, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column10)
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
        static public string tablename = "Gen_period";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string docId = "doc_id";
        static public string beginDate = "begin_date";
        static public string endDate = "end_date";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pDocId = "@doc_id";
        static public string pBeginDate = "@begin_date";
        static public string pEndDate = "@end_date";
        #endregion

        #region Методы - статические
        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docId, typeof(long));
            table.Columns.Add(beginDate, typeof(DateTime));
            table.Columns.Add(endDate, typeof(DateTime));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ORDER BY {2}", docId, doc_id, beginDate);
        }

        static public string GetInsertText(long doc_id, DateTime begin_date, DateTime end_date)
        {
            return string.Format("INSERT INTO {0} ({1},{2},{3}) VALUES ({4},'{5}','{6}'); SELECT LAST_INSERT_ROWID();", tablename, docId, beginDate, endDate, doc_id, begin_date.ToString("yyyy-MM-dd"), end_date.ToString("yyyy-MM-dd"));
        }

        static public string GetDeleteTextById(long _id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, _id);
        }

        static public string GetDeleteTextByDocId(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, docId, doc_id);
        }

        static public void SetDocId(DataTable table, long doc_id)
        {
            foreach (DataRow row in table.Rows)
            {
                row[docId] = doc_id;
                row.EndEdit();
            }
        }

        static public SQLiteCommand CreateSelectCommand()
        {
            return CreateSelectCommand(null, null);
        }

        static public SQLiteCommand CreateSelectCommand(SQLiteConnection connection)
        {
            return CreateSelectCommand(connection, null);
        }

        static public SQLiteCommand CreateSelectCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.CommandText = GetSelectText() + string.Format("WHERE {0} = {1}", docId, pDocId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        static public SQLiteCommand CreateReplaceCommand()
        {
            return CreateReplaceCommand(null, null);
        }

        static public SQLiteCommand CreateReplaceCommand(SQLiteConnection connection)
        {
            return CreateReplaceCommand(connection, null);
        }

        static public SQLiteCommand CreateReplaceCommand(SQLiteConnection connection, SQLiteTransaction transaction)
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

        static public SQLiteCommand CreateDeleteCommand()
        {
            return CreateDeleteCommand(null, null);
        }

        static public SQLiteCommand CreateDeleteCommand(SQLiteConnection connection)
        {
            return CreateDeleteCommand(connection, null);
        }

        static public SQLiteCommand CreateDeleteCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.CommandText = string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                                                tablename, id, pId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        static public SQLiteDataAdapter CreateAdapter(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand(connection, transaction);
            adapter.InsertCommand = CreateReplaceCommand(connection, transaction);
            adapter.UpdateCommand = CreateReplaceCommand(connection, transaction);
            adapter.DeleteCommand = CreateDeleteCommand(connection, transaction);
            return adapter;
        }

        static public string GetCopyText(long old_doc_id, long new_doc_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3})
	SELECT {4}, {2}, {3} FROM {0} WHERE {5} = {6}; SELECT LAST_INSERT_ROWID();",
                                tablename, docId, beginDate, endDate, new_doc_id, docId, old_doc_id);
        }

        static public int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection)
        {
            return CopyPeriodByDocId(old_doc_id, new_doc_id, connection, null);
        }

        static public int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(old_doc_id, new_doc_id), connection, transaction);
            return command.ExecuteNonQuery();
        }

        static public string GetReplaceYearText(long doc_id, int old_year, int new_year)
        {
            return string.Format(@"UPDATE {0} SET {1} = replace({1}, {2}, {3}), {4} = replace({4}, {2}, {3}) WHERE {5} = {6}) ",
                                tablename, beginDate, old_year, new_year, endDate, id, doc_id);
        }

        static public int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection)
        {
            return ReplaceYear(doc_id, old_year, new_year, connection, null);
        }

        static public int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetReplaceYearText(doc_id, old_year, new_year), connection, transaction);
            return command.ExecuteNonQuery();
        }

        #endregion
    }

    public class DopPeriod
    {
        static public string tablename = "Dop_period";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string docId = "doc_id";
        static public string classificatorId = "classificator_id";
        static public string beginDate = "begin_date";
        static public string endDate = "end_date";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pDocId = "@doc_id";
        static public string pClassificatorId = "@classificator_id";
        static public string pBeginDate = "@begin_date";
        static public string pEndDate = "@end_date";
        #endregion

        #region Методы - статические
        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docId, typeof(long));
            table.Columns.Add(classificatorId, typeof(long));
            table.Columns.Add(beginDate, typeof(DateTime));
            table.Columns.Add(endDate, typeof(DateTime));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", docId, doc_id);
        }

        static public string GetInsertText(long doc_id, long classificator_id, DateTime begin_date, DateTime end_date)
        {
            return string.Format("INSERT INTO {0} ({1},{2},{3},{4}) VALUES ({5},{6},'{7}','{8}'); SELECT LAST_INSERT_ROWID();", tablename, docId, classificatorId, beginDate, endDate, doc_id, classificator_id, begin_date.ToString("yyyy-MM-dd"), end_date.ToString("yyyy-MM-dd"));
        }

        static public string GetDeleteTextById(long _id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, _id);
        }

        static public string GetDeleteTextByDocId(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, docId, doc_id);
        }

        static public void SetDocId(DataTable table, long doc_id)
        {
            foreach (DataRow row in table.Rows)
            {
                row[docId] = doc_id;
                row.EndEdit();
            }
        }

        static public SQLiteCommand CreateSelectCommand()
        {
            return CreateSelectCommand(null, null);
        }

        static public SQLiteCommand CreateSelectCommand(SQLiteConnection connection)
        {
            return CreateSelectCommand(connection, null);
        }

        static public SQLiteCommand CreateSelectCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.CommandText = DopPeriodView.GetSelectText() + string.Format("WHERE {0} = {1}", docId, pDocId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        static public SQLiteCommand CreateReplaceCommand()
        {
            return CreateReplaceCommand(null, null);
        }

        static public SQLiteCommand CreateReplaceCommand(SQLiteConnection connection)
        {
            return CreateReplaceCommand(connection, null);
        }

        static public SQLiteCommand CreateReplaceCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.Parameters.Add(new SQLiteParameter(pClassificatorId, DbType.UInt64, classificatorId));
            comm.Parameters.Add(new SQLiteParameter(pBeginDate, DbType.Date, beginDate));
            comm.Parameters.Add(new SQLiteParameter(pEndDate, DbType.Date, endDate));
            comm.CommandText = string.Format(@"REPLACE INTO {0} ({1},{2},{3},{4},{5}) VALUES ({6}, {7}, {8}, {9}, {10}); ",
                                            tablename, id, docId, classificatorId, beginDate, endDate, pId, pDocId, pClassificatorId, pBeginDate, pEndDate);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        static public SQLiteCommand CreateDeleteCommand()
        {
            return CreateDeleteCommand(null, null);
        }

        static public SQLiteCommand CreateDeleteCommand(SQLiteConnection connection)
        {
            return CreateDeleteCommand(connection, null);
        }

        static public SQLiteCommand CreateDeleteCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.CommandText = string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                                                tablename, id, pId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        static public SQLiteDataAdapter CreateAdapter(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = CreateSelectCommand(connection, transaction);
            adapter.InsertCommand = CreateReplaceCommand(connection, transaction);
            adapter.UpdateCommand = CreateReplaceCommand(connection, transaction);
            adapter.DeleteCommand = CreateDeleteCommand(connection, transaction);
            return adapter;
        }

        static public string GetCopyText(long old_doc_id, long new_doc_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3}, {4})
	SELECT {5}, {2}, {3}, {4} FROM {0} WHERE {6} = {7}; SELECT LAST_INSERT_ROWID();",
                                tablename, docId, classificatorId, beginDate, endDate, new_doc_id, docId, old_doc_id);
        }

        static public int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection)
        {
            return CopyPeriodByDocId(old_doc_id, new_doc_id, connection, null);
        }

        static public int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(old_doc_id, new_doc_id), connection, transaction);
            return command.ExecuteNonQuery();
        }

        static public string GetReplaceYearText(long doc_id, int old_year, int new_year)
        {
            return string.Format(@"UPDATE {0} SET {1} = replace({1}, {2}, {3}), {4} = replace({4}, {2}, {3}) WHERE {5} = {6}) ",
                                tablename, beginDate, old_year, new_year, endDate, id, doc_id);
        }

        static public int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection)
        {
            return ReplaceYear(doc_id, old_year, new_year, connection, null);
        }

        static public int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetReplaceYearText(doc_id, old_year, new_year), connection, transaction);
            return command.ExecuteNonQuery();
        }
        #endregion
    }

    public class DopPeriodView
    {
        static public string tablename = "Dop_period_View";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string docId = "doc_id";
        static public string classificatorId = "classificator_id";
        static public string code = "code";
        static public string beginDate = "begin_date";
        static public string endDate = "end_date";
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
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

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", docId, doc_id);
        }

        static public string GetDeleteTextById(long _id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, _id);
        }

        static public string GetDeleteTextByDocId(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, docId, doc_id);
        }
        #endregion
    }

    public class SpecialPeriod
    {
        static public string tablename = "Spec_period";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string docId = "doc_id";
        static public string partCondition = "part_condition";
        static public string stajBase = "staj_base";
        static public string servYearBase = "serv_year_base";
        static public string beginDate = "begin_date";
        static public string endDate = "end_date";
        static public string month = "month";
        static public string day = "day";
        static public string hour = "hour";
        static public string minute = "minute";
        static public string profession = "profession";
        #endregion

        #region Методы - статические
        static public DataTable CreatetTable()
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

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1} ORDER BY {2}", docId, doc_id, beginDate);
        }

        static public string GetInsertText(long doc_id, long part_condition, long staj_base, long serv_year_base, DateTime begin_date, DateTime end_date, int _month, int _day, int _hour, int _minute, string _profession)
        {
            return string.Format("INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}) VALUES ({12},{13},{14},{15},'{16}','{17}',{18},{19},{20},{21},'{22}'); SELECT LAST_INSERT_ROWID();", tablename, docId, partCondition, stajBase, servYearBase, beginDate, endDate, month, day, hour, minute, profession, doc_id, part_condition, staj_base, serv_year_base, begin_date.ToString("yyyy-MM-dd"), end_date.ToString("yyyy-MM-dd"), _month, _day, _hour, _minute, _profession);
        }

        static public string GetDeleteTextById(long _id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, _id);
        }

        static public string GetDeleteTextByDocId(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, docId, doc_id);
        }

        static public string GetCopyText(long old_doc_id, long new_doc_id)
        {
            return string.Format(@"INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11})
	SELECT {12}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11} FROM {0} WHERE {13} = {14}; SELECT LAST_INSERT_ROWID();",
                                tablename, docId, partCondition, stajBase, servYearBase, beginDate, endDate, month, day, hour, minute, profession, new_doc_id, docId, old_doc_id);
        }

        static public int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection)
        {
            return CopyPeriodByDocId(old_doc_id, new_doc_id, connection, null);
        }

        static public int CopyPeriodByDocId(long old_doc_id, long new_doc_id, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetCopyText(old_doc_id, new_doc_id), connection, transaction);
            return command.ExecuteNonQuery();
        }

        static public string GetReplaceYearText(long doc_id, int old_year, int new_year)
        {
            return string.Format(@"UPDATE {0} SET {1} = replace({1}, {2}, {3}), {4} = replace({4}, {2}, {3}) WHERE {5} = {6}) ",
                                tablename, beginDate, old_year, new_year, endDate, id, doc_id);
        }

        static public int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection)
        {
            return ReplaceYear(doc_id, old_year, new_year, connection, null);
        }

        static public int ReplaceYear(long doc_id, int old_year, int new_year, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetReplaceYearText(doc_id, old_year, new_year), connection, transaction);
            return command.ExecuteNonQuery();
        }
        #endregion
    }

    public class SpecialPeriodView
    {
        static public string tablename = "Spec_period_View";

        #region Названия полей в представления БД
        static public string id = "id";
        static public string docId = "doc_id";
        static public string partCondition = "part_condition";
        static public string partCode = "part_code";
        static public string stajBase = "staj_base";
        static public string stajCode = "staj_code";
        static public string servYearBase = "serv_year_base";
        static public string servCode = "serv_code";
        static public string beginDate = "begin_date";
        static public string endDate = "end_date";
        static public string month = "month";
        static public string day = "day";
        static public string hour = "hour";
        static public string minute = "minute";
        static public string profession = "profession";
        #endregion

        #region Параметры для полей таблицы
        static public string pId = "@id";
        static public string pDocId = "@doc_id";
        static public string pPartCondition = "@part_condition";
        static public string pStajBase = "@staj_base";
        static public string pServYearBase = "@serv_year_base";
        static public string pBeginDate = "@begin_date";
        static public string pEndDate = "@end_date";
        static public string pMonth = "@month";
        static public string pDay = "@day";
        static public string pHour = "@hour";
        static public string pMinute = "@minute";
        static public string pProfession = "@profession";
        #endregion

        #region Методы - статические
        static public DataTable CreatetTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(docId, typeof(long));
            table.Columns.Add(partCondition, typeof(long));
            table.Columns.Add(partCode, typeof(string));
            table.Columns.Add(stajBase, typeof(long));
            table.Columns.Add(stajCode, typeof(string));
            table.Columns.Add(servYearBase, typeof(long));
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

        static public string GetSelectText()
        {
            return string.Format("SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long doc_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} = {1}", docId, doc_id);
        }

        static public string GetDeleteTextById(long _id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", tablename, id, _id);
        }

        static public string GetDeleteTextByDocId(long doc_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1} = {2}", SpecialPeriod.tablename, SpecialPeriod.docId, doc_id);
        }

        static public void SetDocId(DataTable table, long doc_id)
        {
            foreach (DataRow row in table.Rows)
            {
                row[docId] = doc_id;
                row.EndEdit();
            }
        }

        static public SQLiteCommand CreateSelectCommand()
        {
            return CreateSelectCommand(null, null);
        }

        static public SQLiteCommand CreateSelectCommand(SQLiteConnection connection)
        {
            return CreateSelectCommand(connection, null);
        }

        static public SQLiteCommand CreateSelectCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pDocId, DbType.UInt64, docId));
            comm.CommandText = SpecialPeriodView.GetSelectText() + string.Format("WHERE {0} = {1}", docId, pDocId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        static public SQLiteCommand CreateReplaceCommand()
        {
            return CreateReplaceCommand(null, null);
        }

        static public SQLiteCommand CreateReplaceCommand(SQLiteConnection connection)
        {
            return CreateReplaceCommand(connection, null);
        }

        static public SQLiteCommand CreateReplaceCommand(SQLiteConnection connection, SQLiteTransaction transaction)
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

            comm.CommandText = string.Format(@"REPLACE INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}) VALUES ({13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}); ",
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

        static public SQLiteCommand CreateDeleteCommand()
        {
            return CreateDeleteCommand(null, null);
        }

        static public SQLiteCommand CreateDeleteCommand(SQLiteConnection connection)
        {
            return CreateDeleteCommand(connection, null);
        }

        static public SQLiteCommand CreateDeleteCommand(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.Parameters.Add(new SQLiteParameter(pId, DbType.UInt64, id));
            comm.CommandText = string.Format(@"DELETE FROM {0} WHERE {1} = {2};",
                                                SpecialPeriod.tablename, id, pId);
            comm.Connection = connection;
            comm.Transaction = transaction;
            return comm;
        }

        static public SQLiteDataAdapter CreateAdapter(SQLiteConnection connection, SQLiteTransaction transaction)
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
            table.Columns.Add(repYear, typeof(int));
            table.Columns.Add(listCount, typeof(int));
            table.Columns.Add(docCount, typeof(int));
            table.Columns.Add(actual, typeof(bool));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long org_id)
        {
            return string.Format("{0} WHERE {1} = {2} ", GetSelectText(), orgID, org_id);
        }

        static public string GetSelectRowText(long row_id)
        {
            return string.Format("{0} WHERE {1} = {2} ", GetSelectText(), id, row_id);
        }

        static public DataRow GetRow(long row_id, string connectionStr)
        {
            DataTable table = Mergies.CreateTable();
            DataRow rowRes = null;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectRowText(row_id), connectionStr);
            adapter.Fill(table);
            if (table.Rows.Count > 0)
                rowRes = table.Rows[0];
            return rowRes;
        }

        static public string GetSelectActualText(long org_id, int rep_year)
        {
            return string.Format("{0} WHERE ({1}={2} AND {3}=1 AND {4}={5})", GetSelectText(), orgID, org_id, actual, repYear, rep_year);
        }

        static public string GetInsertText(long org_id, int rep_year, int list_count, int doc_count)
        {
            return string.Format(" INSERT INTO {0} ({1},{2},{3},{4}) VALUES ({5},{6},{7},{8}); SELECT last_insert_rowid(); ",
                                    tablename, orgID, repYear, listCount, docCount,
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
                                    tablename, actual, Convert.ToInt32(actual_value), orgID, org_id, repYear, rep_year);
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

        static public SQLiteCommand InsertCommand(DataRow mergeRow)
        {
            if (mergeRow == null)
                return null;
            SQLiteCommand comm = new SQLiteCommand();
            comm.CommandText = GetInsertText((long)mergeRow[orgID], (int)mergeRow[repYear], (int)mergeRow[listCount], (int)mergeRow[docCount]);
            return comm;
        }

        static public SQLiteCommand UpdateCommand(DataRow mergeRow)
        {
            if (mergeRow == null)
                return null;
            SQLiteCommand comm = new SQLiteCommand();
            comm.CommandText = GetUpdateText((long)mergeRow[id], (long)mergeRow[orgID], (int)mergeRow[repYear], (int)mergeRow[listCount], (int)mergeRow[docCount]);
            return comm;
        }

        static public SQLiteCommand DeleteCommand(DataRow mergeRow)
        {
            if (mergeRow == null)
                return null;
            SQLiteCommand comm = new SQLiteCommand();
            comm.CommandText = GetDeleteText((long)mergeRow[id]);
            return comm;
        }

        static public int DeleteExecute(DataRow mergeRow, string connectionStr)
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
        new static public string tablename = "Mergies_View";

        #region Дополненные поля для представления
        static public string operName = "operator";
        static public string newDate = "new_date";
        static public string editDate = "edit_date";
        #endregion

        #region Методы - статические
        new static public DataTable CreateTable()
        {
            DataTable table = Mergies.CreateTable();
            table.Columns.Add(newDate, typeof(DateTime));
            table.Columns.Add(editDate, typeof(DateTime));
            table.Columns.Add(operName, typeof(string));
            return table;
        }

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

        new static public string GetSelectActualText(long org_id, int rep_year)
        {
            return string.Format("{0} WHERE ({1}={2} AND {3}=1 AND {4}={5})", GetSelectText(), orgID, org_id, actual, repYear, rep_year);
        }

        static public string GetReplaceFixDataText(DataRow mergeRow, FixData.FixType type)
        {
            DateTime fixdate = (DateTime)(type == FixData.FixType.New ?
                                    mergeRow[MergiesView.newDate] :
                                    mergeRow[MergiesView.editDate]);
            return FixData.GetReplaceText(Mergies.tablename,
                                    type,
                                    (long)mergeRow[MergiesView.id],
                                    (string)mergeRow[MergiesView.operName],
                                    fixdate);
        }

        new static public DataRow GetRow(long row_id, string connectionStr)
        {
            DataTable table = Mergies.CreateTable();
            DataRow rowRes = null;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectRowText(row_id), connectionStr);
            adapter.Fill(table);
            if (table.Rows.Count > 0)
                rowRes = table.Rows[0];
            return null;
        }
        #endregion
    }

    public class MergeInfo
    {
        static public string tablename = "Merge_Info";

        #region Названия полей в БД
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

        #region Названия параметров для команд
        static public string pId = "@id";
        static public string pMergeID = "@merge_id";
        static public string pGroupID = "@groups_id";
        static public string pJanuary = "@january";
        static public string pFebruary = "@february";
        static public string pMarch = "@march";
        static public string pApril = "@april";
        static public string pMay = "@may";
        static public string pJune = "@june";
        static public string pJuly = "@july";
        static public string pAugust = "@august";
        static public string pSeptember = "@september";
        static public string pOctober = "@october";
        static public string pNovember = "@november";
        static public string pDecember = "@december";
        static public string pSum = "@sum";
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

        static public DataTable CreateTableWithRows()
        {
            DataTable table = CreateTable();
            DataRow row;
            row = table.NewRow();
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

        static public DataTable CreateTableWithRows(long merge_id)
        {
            DataTable table = CreateTableWithRows();
            MergeInfo.SetMergeID(table, merge_id);
            return table;
        }

        static public DataTable GetTable(long merge_id, string connectionStr)
        {
            DataTable table = CreateTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(GetSelectText(merge_id), connectionStr);
            adapter.Fill(table);
            return table;
        }

        static public void SetMergeID(DataTable mergeInfoTable, long merge_id)
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
            return new int[] {
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
            double sum; ;
            int[] months = GetMonthIndexes(mergeInfo);
            foreach (DataRow row in mergeInfo.Rows)
            {
                sum = 0;
                foreach (int col in months)
                    sum += (double)row[col];
                row[MergeInfo.sum] = Math.Round(sum, 2);
            }
        }

        static public string GetSelectText()
        {
            return string.Format(" SELECT * FROM {0} ", tablename);
        }

        static public string GetSelectText(long merge_id)
        {
            return string.Format("{0} WHERE {1} = {2} ORDER BY {3}", GetSelectText(), mergeID, merge_id, groupID);
        }

        static public string GetDeleteText(long merge_id)
        {
            return string.Format("DELETE FROM {0} WHERE {1}={2}",
                                    tablename, mergeID, merge_id);
        }

        static public string GetDeleteText(IEnumerable<long> row_id)
        {
            string instr = "( ";
            foreach (long val in row_id)
                instr += val + ",";
            instr = instr.Remove(instr.Length - 1);
            instr += " )";

            return string.Format("DELETE FROM {0} WHERE {1} in {2}",
                                    tablename, mergeID, instr);
        }

        static public void SetParametersTo(SQLiteCommand command)
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

        static public SQLiteCommand CreateInsertCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            SetParametersTo(comm);
            comm.CommandText = string.Format(
                                @" INSERT INTO {0} 
                                ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})
                                VALUES ({16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30});
                                SELECT last_insert_rowid();",
                            tablename,
                            mergeID, groupID, january, february, march, april, may, june, july, august, september, october, november, december, sum,
                            pMergeID, pGroupID, pJanuary, pFebruary, pMarch, pApril, pMay, pJune, pJuly, pAugust, pSeptember, pOctober, pNovember, pDecember, pSum);
            return comm;
        }

        static public SQLiteCommand CreateUpdateCommand()
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

        static public SQLiteCommand CreateDeleteCommand()
        {
            SQLiteCommand comm = new SQLiteCommand();
            SetParametersTo(comm);
            comm.CommandText = string.Format(@" DELETE FROM {0} WHERE {1} = {2} ", tablename, id, pId);
            return comm;
        }

        static public SQLiteCommand CreateDeleteCommand(long merge_id)
        {
            SQLiteCommand comm = new SQLiteCommand();
            comm.CommandText = GetDeleteText(merge_id);
            return comm;
        }

        static public SQLiteCommand CreateDeleteCommand(DataRow mergeRow)
        {
            return CreateDeleteCommand((long)mergeRow[mergeID]);
        }
        #endregion
    }

    // класс виртуальной таблицы (ей нет прямой аналогии в БД)
    public class MergeInfoTranspose
    {
        static public string tablename = "Merge_Info_transpose";

        #region Поля виртуальной таблицы
        readonly static public string month = "month";
        readonly static public string col1 = SalaryGroups.Column1.ToString();
        readonly static public string col2 = SalaryGroups.Column2.ToString();
        readonly static public string col3 = SalaryGroups.Column3.ToString();
        readonly static public string col4 = SalaryGroups.Column4.ToString();
        readonly static public string col5 = SalaryGroups.Column5.ToString();
        readonly static public string col6 = SalaryGroups.Column21.ToString();
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

        static public void ConvertToMergeInfo(DataTable mergeInfoTranspose, DataTable mergeInfo)
        {
            int[] iMonth = MergeInfo.GetMonthIndexes(mergeInfo);
            DataRow[] mergeInfoRows = new DataRow[] {
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column1),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column2),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column3),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column4),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column5),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column21)
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
            int[] iMonth = MergeInfo.GetMonthIndexes(mergeInfo);
            DataRow[] mergeInfoRows = new DataRow[] {
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column1),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column2),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column3),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column4),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column5),
                            MergeInfo.Find(mergeInfo, MergeInfo.groupID, (long)SalaryGroups.Column21)
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

        static public double GetSum(DataTable mergeInfoTranspose, string col_name)
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
        public enum FixType { New = 0, Edit = 1 }
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
            return string.Format("{0} AND {1}={2} ", GetSelectText(table_name), rowID, row_id);
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
            return string.Format(@" REPLACE INTO {0} ({1},{2},{3},{4},{5},{6}) VALUES (({7}),{8},({9}),{10},'{11}','{12}'); SELECT LAST_INSERT_ROWID();",
                                    tablename,
                                    id,
                                    type, tableID, rowID, oper, fixDate,
                                    GetSelectIDText(fix_type, table_name, row_id),
                                    (int)fix_type, Tables.GetSelectIDText(table_name), row_id, oper_name, fix_date.ToString("yyyy-MM-dd H:mm:ss")
                                );
        }

        static public string GetDeleteText(string table_name, long row_id)
        {
            return string.Format(" DELETE FROM {0} WHERE {1}={2} AND {3}={4} ", tablename, tableID, Tables.GetSelectIDText(table_name), rowID, row_id);
        }
        #endregion

        internal static long ExecReplaceText(string table_name, FixType fix_type, long row_id, string oper_name, DateTime fix_date, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string commantText = String.Empty;

            if (connection.State != ConnectionState.Open)
                connection.Open();
            SQLiteCommand command = new SQLiteCommand(GetReplaceText(table_name, fix_type, row_id, oper_name, fix_date), connection, transaction);
            return Convert.ToInt64(command.ExecuteScalar());
        }
    }
}
