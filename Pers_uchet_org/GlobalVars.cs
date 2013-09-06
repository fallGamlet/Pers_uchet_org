using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace Pers_uchet_org
{
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
                oper.candeleteVal = (byte)reader[candelete];
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
                oper.candeleteVal = (int)reader[Operator.candelete];
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
            comm.CommandText = string.Format(@"INSERT INTO [{0}] ({1}, {2}) VALUES ({3}, {4});
                                            SELECT last_indert_rowid();",
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
            return string.Format(@" INSERT OR IGNORE INTO {0} ({1}, {2}, {3}) VALUES ( ({4}), {5}, {6}) ",
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
            return string.Format(" UPDATE [{0}] SET {1} = {2}, {3} = '{4}' WHERE {5} = {6} AND {7} = {8} ",
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
            return string.Format("UPDATE {0} SET {1} = {2}, {3} = '{4}' WHERE {5} in {6} AND {7} = {8} ",
                                    tablename, state, stateVal, dismissDate, date, 
                                    personID, personsIdStr, orgID,org_id);
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
            commantText+= "commit transaction;";
            SQLiteCommand command = new SQLiteCommand(commantText);
            command.Connection = new SQLiteConnection(connectionStr);
            command.Connection.Open();
            int count = command.ExecuteNonQuery();
            command.Connection.Close();
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
            SQLiteCommand command= new SQLiteCommand();
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
            ChangeState(person_id, org_id, 0, date.ToShortDateString(), connectionStr);
        }

        static public void SetStateToUvolit(IEnumerable<long> personIDArr, long org_id, DateTime date, string connectionStr)
        {
            ChangeState(personIDArr, org_id, 0, date.ToShortDateString(), connectionStr);
        }

        static public void SetStateToVosstanovit(long person_id, long org_id, string connectionStr)
        {
            ChangeState(person_id, org_id, 1, "", connectionStr);
        }

        static public void SetStateToRaboraet(IEnumerable<long> personIDArr, long org_id, string connectionStr)
        {
            ChangeState(personIDArr, org_id, 1, "", connectionStr);
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
            foreach(long val in personidArr)
                instr += val+",";
            instr = instr.Remove(instr.Length-1);
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

        public enum PersonState { Uvolen=0, Rabotaet=1};

        #region Названия полей в представления БД
        static public string id = "id";
        static public string socNumber = "soc_number";
        static public string fName = "f_name";
        static public string mName = "m_name";
        static public string lName ="l_name";
        static public string fio = "fio";
        static public string birthday = "birthday";
        static public string sex = "sex";
        static public string docType = "doc_type";
        static public string docSeries = "doc_series";
        static public string docNumber = "doc_number";
        static public string docDate = "doc_date";
        static public string docOrg = "doc_org";
        static public string regAdress = "regAdress";
        static public string factAdress = "factAdress";
        static public string bornAdress = "bornAdress";
        static public string citizen1 = "citizen1";
        static public string citizen2 = "citizen2";
        static public string state = "state";
        static public string orgID = "org_id";
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
            table.Columns.Add(factAdress, typeof(string));
            table.Columns.Add(bornAdress, typeof(string));
            table.Columns.Add(citizen1, typeof(string));
            table.Columns.Add(citizen2, typeof(string));
            table.Columns.Add(state, typeof(int));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format("SELECT {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19} FROM {20} ",
                                id, socNumber, fName, mName, lName, fio, birthday, sex, 
                                docType, docSeries, docNumber, docDate, docOrg, 
                                regAdress, factAdress, bornAdress, 
                                citizen1, citizen2, state, orgID, tablename);
        }

        static public string GetSelectText(long org_id)
        {
            return GetSelectText() + string.Format(" WHERE {0} IN ({1}) ",orgID, org_id);
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
        #endregion

        #region Методы - статические
        static public DataTable CreateTable()
        {
            DataTable table = new DataTable(tablename);
            table.Columns.Add(id, typeof(long));
            table.Columns.Add(classificatorID, typeof(long));
            table.Columns.Add(privilegeID, typeof(long));
            table.Columns.Add(value, typeof(string));
            table.Columns.Add(dateBegin, typeof(string));
            table.Columns.Add(dateEnd, typeof(string));
            return table;
        }

        static public string GetSelectText()
        {
            return string.Format(@" SELECT {7}.{0} as {0},{1},{2}, {8}.{10} as {3},{4},{5},{6} FROM {7} LEFT JOIN {8} ON {7}.{2} = {8}.{9} ",
                                id, classificatorID, privilegeID, privilegeName, value, dateBegin, dateEnd, tablename, Privilege.tablename, Privilege.id, Privilege.name);
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
            return string.Format(" SELECT {0},{1} FROM {2} ", id, name, tablename );
        }
        #endregion
    }
}
