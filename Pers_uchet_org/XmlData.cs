using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Pers_uchet_org
{
    public class XmlData
    {
        public enum ReportType { Adv1 = 0, Adv2, Adv3, Adv4, Adv5, Adv6, Szv1, Szv2, Szv3/*, Rdv1, Rdv21, Rdv22, Rdv3*/ }

        public static string GetReportUrl(ReportType type)
        {
            string url;
            switch (type)
            {
                case XmlData.ReportType.Adv1:
                    url = Properties.Settings.Default.report_adv1;
                    break;
                case XmlData.ReportType.Adv2:
                    url = Properties.Settings.Default.report_adv2;
                    break;
                case XmlData.ReportType.Adv3:
                    url = Properties.Settings.Default.report_adv3;
                    break;
                case XmlData.ReportType.Adv4:
                    url = Properties.Settings.Default.report_adv4;
                    break;
                case XmlData.ReportType.Adv5:
                    url = Properties.Settings.Default.report_adv5;
                    break;
                case XmlData.ReportType.Adv6:
                    url = Properties.Settings.Default.report_adv6;
                    break;
                case XmlData.ReportType.Szv1:
                    url = Properties.Settings.Default.report_szv1;
                    break;
                case XmlData.ReportType.Szv2:
                    url = Properties.Settings.Default.report_szv2;
                    break;
                case XmlData.ReportType.Szv3:
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

        public static XmlDocument PersonXml(DataRow row)
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

        public static XmlDocument Adv1Xml(DataRow personViewRow)
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
            sex.InnerText = (int)personViewRow[PersonView.sex] == 1 ? "м" : "ж";
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

        public static string FormatXml(String Xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(Xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return Xml;
            }
        }

        public static string GetHTML(XmlDocument xmlDoc, string xslFilename)
        {
            XPathNavigator xpn = xmlDoc.CreateNavigator();
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(xslFilename);
            MemoryStream outStream = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Encoding = Encoding.GetEncoding(1251);
            setting.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(outStream, setting);
            myXslTrans.Transform(xpn, writer);
            String htmlStr = Encoding.GetEncoding(1251).GetString(outStream.ToArray());
            return htmlStr;
        }

        public static string GetHTML(byte[] xmlBytes, byte[] xslBytes)
        {
            XmlDocument mapXml = new XmlDocument();
            string xslStr = Encoding.GetEncoding(1251).GetString(xslBytes);
            string xmlStr = Encoding.GetEncoding(1251).GetString(xmlBytes);
            mapXml.InnerXml = xmlStr;

            XPathNavigator xpn = mapXml.CreateNavigator();
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            XmlReader xslReader = XmlReader.Create(new MemoryStream(xslBytes));
            myXslTrans.Load(xslReader);
            MemoryStream outStream = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Encoding = Encoding.GetEncoding(1251);
            setting.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(outStream, setting);
            myXslTrans.Transform(xpn, writer);
            String htmlStr = System.Text.Encoding.GetEncoding(1251).GetString(outStream.ToArray());
            return htmlStr;
        }

        public static string GetHTML(byte[] xmlBytes, string xslFilename)
        {
            XmlDocument mapXml = new XmlDocument();
            string xmlStr = Encoding.GetEncoding(1251).GetString(xmlBytes);
            mapXml.InnerXml = xmlStr;

            XPathNavigator xpn = mapXml.CreateNavigator();
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(xslFilename);
            MemoryStream outStream = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Encoding = Encoding.GetEncoding(1251);
            setting.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(outStream, setting);
            myXslTrans.Transform(xpn, writer);
            String htmlStr = System.Text.Encoding.GetEncoding(1251).GetString(outStream.ToArray());
            return htmlStr;
        }

        public static XmlDocument ReadXml(string filename)
        {
            XmlDocument resDoc = new XmlDocument();
            resDoc.InnerXml = File.ReadAllText(filename, Encoding.GetEncoding(1251));
            //
            return resDoc;
        }

        public static Boolean ValidateXml(string xmlSchemaPath, string xmlDocumentPath, StreamWriter outputWriter)
        {
            bool isValid = true;
            XmlDocument xmlDocument = XmlData.ReadXml(xmlDocumentPath);
            xmlDocument.Schemas.Add(null, xmlSchemaPath);

            outputWriter.WriteLine("Файл: " + Path.GetFileName(xmlDocumentPath));
            xmlDocument.Validate(delegate(object sender, ValidationEventArgs args)
            {
                isValid = false;
                outputWriter.WriteLine();
                //writer.WriteLine("Строка: " + args.Exception.LineNumber + ", колонка: " + args.Exception.LinePosition);
                outputWriter.WriteLine(args.Message);
            });
            outputWriter.WriteLine("__________________________________________________");
            outputWriter.WriteLine();
            outputWriter.Flush();

            return isValid;
        }
    }

    public class Szv1Xml
    {
        // название формы
        public static string name = "СЗВ-1";
        #region названия тегов, присутствующих в xml
        public static string tagDocInfo = "doc_info";
        public static string tagPerson = "person";
        public static string tagLname = "lname";
        public static string tagFname = "fname";
        public static string tagMname = "mname";
        public static string tagPersonRegnum = "regnum";
        public static string tagCitizen = "citizen";
        public static string tagCitizen1 = "first";
        public static string tagCitizen1ID = "id";
        public static string tagCitizen1Name = "name";
        public static string tagCitizen2 = "second";
        public static string tagCitizen2ID = "id";
        public static string tagCitizen2Name = "name";
        public static string tagCategoryID = "category_id";
        public static string tagCategoryName = "category_name";
        public static string tagPrivelegeID = "privelege_id";
        public static string tagPrivelegeName = "privelege_name";
        public static string tagFirm = "firm";
        public static string tagFirmRegnum = "regnum";
        public static string tagFirmName = "name";
        public static string tagFormType = "formtype";
        public static string tagWorkPlace = "work_place";
        public static string tagRepYear = "rep_year";
        public static string tagFirmAdd = "firm_add";
        public static string tagFirmPay = "firm_pay";
        public static string tagPayment = "payment";
        public static string tagMonth = "month";
        public static string tagCol1 = "col_1";
        public static string tagCol2 = "col_2";
        public static string tagCol3 = "col_3";
        public static string tagCol4 = "col_4";
        public static string tagCol5 = "col_5";
        public static string tagCol6 = "col_6";
        public static string tagGenPeriod = "gen_period";
        public static string tagPeriod = "period";
        public static string tagGenStart = "gen_start";
        public static string tagGenEnd = "gen_end";
        public static string tagSpecStaj = "spec_staj";
        public static string tagSpec = "spec";
        public static string tagSpecStart = "start_date";
        public static string tagSpecEnd = "end_date";
        public static string tagSpecPartConditionID = "part_condition_id";
        public static string tagSpecPartConditionName = "part_condition_name";
        public static string tagSpecBaseID = "staj_base_id";
        public static string tagSpecBaseName = "staj_base_name";
        public static string tagSpecServyearBaseID = "servyear_base_id";
        public static string tagSpecServyearBaseName = "servyear_base_name";
        public static string tagSpecMonths = "smonths";
        public static string tagSpecDays = "sdays";
        public static string tagSpecHours = "shours";
        public static string tagSpecMinutes = "sminutes";
        public static string tagSpecProfession = "profession";
        public static string tagDopStaj = "dop_staj";
        public static string tagDopRecord = "record";
        public static string tagDopCodeID = "dop_code_id";
        public static string tagDopCodeName = "dop_code_name";
        public static string tagDopStart = "dop_start";
        public static string tagDopEnd = "dop_end";
        #endregion

        public static IEnumerable<XmlDocument> GetXml(IEnumerable<long> doc_id, Org org, string connection_str)
        {
            List<XmlDocument> xmlArray = new List<XmlDocument>();
            foreach (long id in doc_id)
            {
                XmlDocument xmlDoc = GetXml(id, org, connection_str);
                xmlArray.Add(xmlDoc);
            }
            //
            return xmlArray;
        }

        /// <summary>
        /// Получить XML объект формы СЗВ-1
        /// </summary>
        /// <param name="doc_id">Идентификатор документа</param>
        /// <param name="org">Объект данных об организации</param>
        /// <param name="connection_str">Строка подключения к БД</param>
        /// <returns>Объект XML документа</returns>
        public static XmlDocument GetXml(long doc_id, Org org, string connection_str)
        {
            #region Считывание данных

            DataRow docRow = DocsViewForXml.GetRow(doc_id, connection_str);
            if (docRow == null)
                throw new NullReferenceException("Документ не найден");

            DataTable salaryInfoTable = SalaryInfo.CreateTable();
            DataTable salaryInfoTableTranspose = SalaryInfoTranspose.CreateTableWithRows();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(SalaryInfo.GetSelectText(doc_id), connection_str);
            adapter.Fill(salaryInfoTable);
            SalaryInfoTranspose.ConvertFromSalaryInfo(salaryInfoTableTranspose, salaryInfoTable);

            DataTable generalTable = GeneralPeriod.CreatetTable();
            adapter = GeneralPeriod.CreateAdapter(new SQLiteConnection(connection_str), null);
            adapter.SelectCommand.CommandText = GeneralPeriod.GetSelectText(doc_id);
            adapter.Fill(generalTable);

            DataTable specTable = SpecialPeriodView.CreatetTable();
            adapter = new SQLiteDataAdapter
            {
                SelectCommand =
                    new SQLiteCommand(SpecialPeriodView.GetSelectText(doc_id), new SQLiteConnection(connection_str))
            };
            adapter.Fill(specTable);

            DataTable dopTable = DopPeriodView.CreateTable();
            adapter = new SQLiteDataAdapter
            {
                SelectCommand =
                    new SQLiteCommand(DopPeriodView.GetSelectText(doc_id), new SQLiteConnection(connection_str))
            };
            adapter.Fill(dopTable);

            #endregion
            return GetXml(docRow, org, salaryInfoTable, salaryInfoTableTranspose, generalTable, specTable, dopTable);
        }

        /// <summary>
        /// Получить XML объект формы СЗВ-1
        /// </summary>
        /// <param name="docRow">Строка с данными о документе и персоне</param>
        /// <param name="org">Объект данных об организации</param>
        /// <param name="salaryInfoTable">Таблица с суммами</param>
        /// <param name="salaryInfoTableTranspose">Таблица с суммами (транспонированная)</param>
        /// <param name="generalTable">Таблица с основными стажами</param>
        /// <param name="specTable">Таблица со специальными стажами</param>
        /// <param name="dopTable">Таблица с дополнительными стажами</param>
        /// <returns>Объект XML документа</returns>
        public static XmlDocument GetXml(DataRow docRow, Org org, DataTable salaryInfoTable, DataTable salaryInfoTableTranspose, DataTable generalTable, DataTable specTable, DataTable dopTable)
        {
            #region Создание xml элементов

            XmlDocument xmlRes = new XmlDocument();
            XmlElement docInfo = xmlRes.CreateElement(tagDocInfo);

            XmlElement person = xmlRes.CreateElement(tagPerson);
            XmlNode lname = xmlRes.CreateElement(tagLname);
            XmlElement fname = xmlRes.CreateElement(tagFname);
            XmlElement mname = xmlRes.CreateElement(tagMname);
            XmlElement personRegnum = xmlRes.CreateElement(tagPersonRegnum);
            XmlElement citizen = xmlRes.CreateElement(tagCitizen);
            XmlElement citizen1 = xmlRes.CreateElement(tagCitizen1);
            XmlElement citizen1Id = xmlRes.CreateElement(tagCitizen1ID);
            XmlElement citizen1Name = xmlRes.CreateElement(tagCitizen1Name);
            XmlElement citizen2 = xmlRes.CreateElement(tagCitizen2);
            XmlNode citizen2Id = xmlRes.CreateElement(tagCitizen2ID);
            XmlNode citizen2Name = xmlRes.CreateElement(tagCitizen2Name);
            XmlElement categoryId = xmlRes.CreateElement(tagCategoryID);
            XmlElement categoryName = xmlRes.CreateElement(tagCategoryName);
            XmlElement privilegeId = xmlRes.CreateElement(tagPrivelegeID);
            XmlElement privilegeName = xmlRes.CreateElement(tagPrivelegeName);

            XmlElement firm = xmlRes.CreateElement(tagFirm);
            XmlElement firmRegnum = xmlRes.CreateElement(tagFirmRegnum);
            XmlElement firmName = xmlRes.CreateElement(tagFirmName);

            XmlElement formType = xmlRes.CreateElement(tagFormType);
            XmlElement workPlace = xmlRes.CreateElement(tagWorkPlace);
            XmlElement repYear = xmlRes.CreateElement(tagRepYear);
            XmlElement firmAdd = xmlRes.CreateElement(tagFirmAdd);
            XmlElement firmPay = xmlRes.CreateElement(tagFirmPay);

            XmlElement payment = xmlRes.CreateElement(tagPayment);
            XmlElement genPeriod = xmlRes.CreateElement(tagGenPeriod);
            XmlElement specStaj = xmlRes.CreateElement(tagSpecStaj);
            XmlElement dopStaj = xmlRes.CreateElement(tagDopStaj);
            #endregion

            #region Заполнение данными

            lname.InnerText = docRow[DocsViewForXml.lName].ToString();
            fname.InnerText = docRow[DocsViewForXml.fName].ToString();
            mname.InnerText = docRow[DocsViewForXml.mName].ToString();
            personRegnum.InnerText = docRow[DocsViewForXml.socNumber].ToString();

            citizen1Id.InnerText = docRow[DocsViewForXml.citizen1Id].ToString();
            citizen1Name.InnerText = docRow[DocsViewForXml.citizen1Name].ToString();
            citizen2Id.InnerText = docRow[DocsViewForXml.citizen2Id].ToString();
            citizen2Name.InnerText = docRow[DocsViewForXml.citizen2Name].ToString();

            categoryId.InnerText = docRow[DocsViewForXml.classificatorId].ToString();
            categoryName.InnerText = docRow[DocsViewForXml.code].ToString();
            privilegeId.InnerText = docRow[DocsViewForXml.privilegeId].ToString();
            privilegeName.InnerText = docRow[DocsViewForXml.privilegeName].ToString();

            firmRegnum.InnerText = org.regnumVal;
            firmName.InnerText = org.nameVal;

            formType.InnerText = docRow[DocsViewForXml.docTypeId].ToString();
            workPlace.InnerText = docRow[DocsViewForXml.isGeneral].ToString();
            repYear.InnerText = docRow[DocsViewForXml.repYear].ToString();

            firmAdd.InnerText = ((double)salaryInfoTable.Rows[SalaryInfo.FindRowIndex(salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column3)][SalaryInfo.sum]).ToString("F2").Replace(',', '.');
            firmPay.InnerText = ((double)salaryInfoTable.Rows[SalaryInfo.FindRowIndex(salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column5)][SalaryInfo.sum]).ToString("F2").Replace(',', '.');

            #endregion

            xmlRes.AppendChild(xmlRes.CreateXmlDeclaration("1.0", "windows-1251", null));
            xmlRes.AppendChild(docInfo);

            docInfo.AppendChild(person);

            person.AppendChild(lname);
            person.AppendChild(fname);
            person.AppendChild(mname);
            person.AppendChild(personRegnum);

            person.AppendChild(citizen);
            citizen.AppendChild(citizen1);
            citizen1.AppendChild(citizen1Id);
            citizen1.AppendChild(citizen1Name);
            citizen.AppendChild(citizen2);
            citizen2.AppendChild(citizen2Id);
            citizen2.AppendChild(citizen2Name);

            person.AppendChild(categoryId);
            person.AppendChild(categoryName);
            person.AppendChild(privilegeId);
            person.AppendChild(privilegeName);

            docInfo.AppendChild(firm);
            firm.AppendChild(firmRegnum);
            firm.AppendChild(firmName);

            docInfo.AppendChild(formType);
            docInfo.AppendChild(workPlace);
            docInfo.AppendChild(repYear);

            docInfo.AppendChild(firmAdd);
            docInfo.AppendChild(firmPay);


            #region Заполнение данными заработной платы

            for (int i = 0; i < 12; i++)
            {
                XmlElement month = xmlRes.CreateElement(tagMonth);
                XmlElement col1 = xmlRes.CreateElement(tagCol1);
                XmlElement col2 = xmlRes.CreateElement(tagCol2);
                XmlElement col3 = xmlRes.CreateElement(tagCol3);
                XmlElement col4 = xmlRes.CreateElement(tagCol4);
                XmlElement col5 = xmlRes.CreateElement(tagCol5);
                XmlElement col6 = xmlRes.CreateElement(tagCol6);
                col1.InnerText = ((double)salaryInfoTableTranspose.Rows[i][SalaryInfoTranspose.col1]).ToString("F2").Replace(',', '.');
                col2.InnerText = ((double)salaryInfoTableTranspose.Rows[i][SalaryInfoTranspose.col2]).ToString("F2").Replace(',', '.');
                col3.InnerText = ((double)salaryInfoTableTranspose.Rows[i][SalaryInfoTranspose.col3]).ToString("F2").Replace(',', '.');
                col4.InnerText = ((double)salaryInfoTableTranspose.Rows[i][SalaryInfoTranspose.col4]).ToString("F2").Replace(',', '.');
                col5.InnerText = ((double)salaryInfoTableTranspose.Rows[i][SalaryInfoTranspose.col5]).ToString("F2").Replace(',', '.');
                col6.InnerText = salaryInfoTableTranspose.Rows[i][SalaryInfoTranspose.col6].ToString();
                month.AppendChild(col1);
                month.AppendChild(col2);
                month.AppendChild(col3);
                month.AppendChild(col4);
                month.AppendChild(col5);
                month.AppendChild(col6);
                payment.AppendChild(month);
            }
            docInfo.AppendChild(payment);
            #endregion

            #region Заполнение данными основного стажа

            foreach (DataRow row in generalTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;
                XmlElement period = xmlRes.CreateElement(tagPeriod);
                XmlElement genStart = xmlRes.CreateElement(tagGenStart);
                XmlElement genEnd = xmlRes.CreateElement(tagGenEnd);
                genStart.InnerText = (DateTime.Parse(row[GeneralPeriod.beginDate].ToString())).ToString("dd.MM.yyyy");
                genEnd.InnerText = (DateTime.Parse(row[GeneralPeriod.endDate].ToString())).ToString("dd.MM.yyyy");
                genPeriod.AppendChild(period);
                period.AppendChild(genStart);
                period.AppendChild(genEnd);
            }
            docInfo.AppendChild(genPeriod);
            #endregion

            #region Заполнение данными специального стажа

            foreach (DataRow row in specTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;
                XmlElement spec = xmlRes.CreateElement(tagSpec);
                XmlElement specStart = xmlRes.CreateElement(tagSpecStart);
                XmlElement specEnd = xmlRes.CreateElement(tagSpecEnd);
                XmlElement specPartConditionId = xmlRes.CreateElement(tagSpecPartConditionID);
                XmlElement specPartConditionName = xmlRes.CreateElement(tagSpecPartConditionName);
                XmlElement specBaseId = xmlRes.CreateElement(tagSpecBaseID);
                XmlElement specBaseName = xmlRes.CreateElement(tagSpecBaseName);
                XmlElement specServyearBaseId = xmlRes.CreateElement(tagSpecServyearBaseID);
                XmlElement specServyearBaseName = xmlRes.CreateElement(tagSpecServyearBaseName);
                XmlElement specMonths = xmlRes.CreateElement(tagSpecMonths);
                XmlElement specDays = xmlRes.CreateElement(tagSpecDays);
                XmlElement specHours = xmlRes.CreateElement(tagSpecHours);
                XmlElement specMinutes = xmlRes.CreateElement(tagSpecMinutes);
                XmlElement specProfession = xmlRes.CreateElement(tagSpecProfession);
                specStart.InnerText = (DateTime.Parse(row[SpecialPeriodView.beginDate].ToString())).ToString("dd.MM.yyyy");
                specEnd.InnerText = (DateTime.Parse(row[SpecialPeriodView.endDate].ToString())).ToString("dd.MM.yyyy");
                specPartConditionId.InnerText = row[SpecialPeriodView.partConditionClassificatorId].ToString();
                specPartConditionName.InnerText = row[SpecialPeriodView.partCode].ToString();
                specBaseId.InnerText = row[SpecialPeriodView.stajBaseClassificatorId].ToString();
                specBaseName.InnerText = row[SpecialPeriodView.stajCode].ToString();
                specServyearBaseId.InnerText = row[SpecialPeriodView.servYearBaseClassificatorId].ToString();
                specServyearBaseName.InnerText = row[SpecialPeriodView.servCode].ToString();
                specMonths.InnerText = row[SpecialPeriodView.month].ToString();
                specDays.InnerText = row[SpecialPeriodView.day].ToString();
                specHours.InnerText = row[SpecialPeriodView.hour].ToString();
                specMinutes.InnerText = row[SpecialPeriodView.minute].ToString();
                specProfession.InnerText = row[SpecialPeriodView.profession].ToString();
                specStaj.AppendChild(spec);
                spec.AppendChild(specStart);
                spec.AppendChild(specEnd);
                spec.AppendChild(specPartConditionId);
                spec.AppendChild(specPartConditionName);
                spec.AppendChild(specBaseId);
                spec.AppendChild(specBaseName);
                spec.AppendChild(specServyearBaseId);
                spec.AppendChild(specServyearBaseName);
                spec.AppendChild(specMonths);
                spec.AppendChild(specDays);
                spec.AppendChild(specHours);
                spec.AppendChild(specMinutes);
                spec.AppendChild(specProfession);
            }
            docInfo.AppendChild(specStaj);
            #endregion

            #region Заполнение данными дополнительного стажа

            foreach (DataRow row in dopTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;
                XmlElement dopRecord = xmlRes.CreateElement(tagDopRecord);
                XmlElement dopCodeId = xmlRes.CreateElement(tagDopCodeID);
                XmlElement dopCodeName = xmlRes.CreateElement(tagDopCodeName);
                XmlElement dopStart = xmlRes.CreateElement(tagDopStart);
                XmlElement dopEnd = xmlRes.CreateElement(tagDopEnd);
                dopCodeId.InnerText = row[DopPeriodView.classificatorId].ToString();
                dopCodeName.InnerText = row[DopPeriodView.code].ToString();
                dopStart.InnerText = (DateTime.Parse(row[DopPeriodView.beginDate].ToString())).ToString("dd.MM.yyyy");
                dopEnd.InnerText = (DateTime.Parse(row[DopPeriodView.endDate].ToString())).ToString("dd.MM.yyyy");
                dopStaj.AppendChild(dopRecord);
                dopRecord.AppendChild(dopCodeId);
                dopRecord.AppendChild(dopCodeName);
                dopRecord.AppendChild(dopStart);
                dopRecord.AppendChild(dopEnd);
            }
            docInfo.AppendChild(dopStaj);
            #endregion

            //string formStr = XmlData.FormatXml(xmlRes.InnerXml);
            return xmlRes;
        }

        /// <summary>
        /// Получить путь к пустому бланку отчета формы СЗВ-1 относительно старта директории программы
        /// </summary>
        /// <returns></returns>
        public static string GetReportUrl()
        {
            return Properties.Settings.Default.report_szv1;
        }

        /// <summary>
        /// Получить путь к файлу стиля XSL для формы СЗВ-1 относительно старта директории программы
        /// </summary>
        /// <returns></returns>
        public static string GetXslUrl()
        {
            return Properties.Settings.Default.xsl_szv1;
        }

        /// <summary>
        /// Получить Html строку формы СЗВ-1
        /// </summary>
        /// <param name="xmlDoc">Объект XML документа</param>
        /// <returns>Html строка</returns>
        public static string GetHtml(XmlDocument xmlDoc)
        {
            return XmlData.GetHTML(xmlDoc, GetXslUrl());
        }

        /// <summary>
        /// Получить Html строку формы СЗВ-1
        /// </summary>
        /// <param name="docRow">Строка с данными о документе и персоне</param>
        /// <param name="org">Объект данных об организации</param>
        /// <param name="salaryInfoTable">Таблица с суммами</param>
        /// <param name="salaryInfoTableTranspose">Таблица с суммами (транспонированная)</param>
        /// <param name="generalTable">Таблица с основными стажами</param>
        /// <param name="specTable">Таблица со специальными стажами</param>
        /// <param name="dopTable">Таблица с дополнительными стажами</param>
        /// <returns>Html строка</returns>
        public static string GetHtml(DataRow docRow, Org org, DataTable salaryInfoTable, DataTable salaryInfoTableTranspose, DataTable generalTable, DataTable specTable, DataTable dopTable)
        {
            return GetHtml(GetXml(docRow, org, salaryInfoTable, salaryInfoTableTranspose, generalTable, specTable, dopTable));
        }

        /// <summary>
        /// Получить Html строку формы СЗВ-1
        /// </summary>
        /// <param name="doc_id">Идентификатор документа</param>
        /// <param name="org">Объект данных об организации</param>
        /// <param name="connection_str">Строка подключения к БД</param>
        /// <returns>Html строка</returns>
        public static string GetHtml(long doc_id, Org org, string connection_str)
        {
            return GetHtml(GetXml(doc_id, org, connection_str));
        }
    }

    public class Szv2Xml
    {
        public static string name = "СЗВ-2";
        #region названия тегов, присутствующих в xml
        public static string tagInddocs = "inddocs";
        public static string tagInddoc = "inddoc";
        public static string tagTypeID = "type_id";
        public static string tagCount = "count";
        public static string tagSummaryInfo = "summary_info";
        public static string tagCol1 = "col_1";
        public static string tagCol2 = "col_2";
        public static string tagCol3 = "col_3";
        public static string tagCol4 = "col_4";
        public static string tagCol5 = "col_5";
        #endregion

        #region Методы - статические
        static void NormalizeDocsCountTable(DataTable docsCount)
        {
            bool init, correct, cancel, granting;
            init = correct = cancel = granting = false;
            long docTypeID;
            foreach (DataRow row in docsCount.Rows)
            {
                docTypeID = (long)row[Docs.docTypeId];
                if (docTypeID == DocTypes.InitialFormId)
                    init = true;
                else if (docTypeID == DocTypes.CorrectionFormId)
                    correct = true;
                else if (docTypeID == DocTypes.CancelingFormId)
                    cancel = true;
                else if (docTypeID == DocTypes.GrantingPensionId)
                    granting = true;
            }
            if (!init)
                docsCount.Rows.Add(DocTypes.InitialFormId, 0);
            if (!correct)
                docsCount.Rows.Add(DocTypes.CorrectionFormId, 0);
            if (!cancel)
                docsCount.Rows.Add(DocTypes.CancelingFormId, 0);
            if (!granting)
                docsCount.Rows.Add(DocTypes.GrantingPensionId, 0);
        }

        /// <summary>
        /// Получить XML объект формы СЗВ-2
        /// </summary>
        /// <param name="docsCount">Таблица с количеством документов сгруппированных по типам документов</param>
        /// <param name="docsSums">Таблицы сумм документов сгруппированных по типам документов и типам групп документов</param>
        /// <returns>объект XML документа</returns>
        public static XmlDocument GetXml(DataTable docsCount, DataTable docsSums)
        {
            NormalizeDocsCountTable(docsCount);

            XmlDocument xmlRes = new XmlDocument();
            XmlElement inddocs = xmlRes.CreateElement(tagInddocs);

            xmlRes.AppendChild(xmlRes.CreateXmlDeclaration("1.0", "windows-1251", null));
            xmlRes.AppendChild(inddocs);

            docsCount.DefaultView.Sort = string.Format("{0} asc", Docs.docTypeId);
            //docsSums.DefaultView.Sort = string.Format("{0}, {1} asc", Docs.docTypeId, SalaryInfo.salaryGroupsId);
            foreach (DataRowView row in docsCount.DefaultView)
            {
                XmlElement inddoc = xmlRes.CreateElement(tagInddoc);
                XmlElement typeID = xmlRes.CreateElement(tagTypeID);
                XmlElement count = xmlRes.CreateElement(tagCount);
                XmlElement summaryInfo = xmlRes.CreateElement(tagSummaryInfo);
                XmlElement col1 = xmlRes.CreateElement(tagCol1);
                XmlElement col2 = xmlRes.CreateElement(tagCol2);
                XmlElement col3 = xmlRes.CreateElement(tagCol3);
                XmlElement col4 = xmlRes.CreateElement(tagCol4);
                XmlElement col5 = xmlRes.CreateElement(tagCol5);

                long curDoctypeID = (long)row[Docs.docTypeId];
                typeID.InnerText = curDoctypeID.ToString();
                count.InnerText = row["count"].ToString();
                inddocs.AppendChild(inddoc);
                inddoc.AppendChild(typeID);
                inddoc.AppendChild(count);
                inddoc.AppendChild(summaryInfo);
                summaryInfo.AppendChild(col1);
                summaryInfo.AppendChild(col2);
                summaryInfo.AppendChild(col3);
                summaryInfo.AppendChild(col4);
                summaryInfo.AppendChild(col5);

                col1.InnerText = col2.InnerText =
                                col3.InnerText =
                                col4.InnerText =
                                col5.InnerText = "0.00";

                foreach (DataRow sumRow in docsSums.Rows)
                {
                    long doctypeID = (long)sumRow[Docs.docTypeId];
                    double val = (double)sumRow[SalaryInfo.sum];
                    if (doctypeID == curDoctypeID)
                    {
                        long salarygroupID = (long)sumRow[SalaryInfo.salaryGroupsId];
                        string valStr = val.ToString("F2").Replace(',', '.');
                        switch (salarygroupID)
                        {
                            case 1:
                                col1.InnerText = valStr;
                                break;
                            case 2:
                                col2.InnerText = valStr;
                                break;
                            case 3:
                                col3.InnerText = valStr;
                                break;
                            case 4:
                                col4.InnerText = valStr;
                                break;
                            case 5:
                                col5.InnerText = valStr;
                                break;
                        }
                    }
                }
            }
            //
            return xmlRes;
        }

        /// <summary>
        /// Получить XML объект формы СЗВ-2
        /// </summary>
        /// <param name="list_id">идентификатор пакета</param>
        /// <param name="connectionStr">строка подключения к БД</param>
        /// <returns>объект XML документа</returns>
        public static XmlDocument GetXml(long list_id, string connectionStr)
        {
            DataTable docsCount = Docs.CountDocsByListAndType(list_id, connectionStr);
            DataTable docsSums = Docs.SumsByDocType(list_id, connectionStr);
            return GetXml(docsCount, docsSums);
        }

        /// <summary>
        /// Получить путь к пустому бланку отчета СЗВ-2
        /// </summary>
        /// <returns></returns>
        public static string GetReportUrl()
        {
            return Properties.Settings.Default.report_szv2;
        }

        /// <summary>
        /// Получить путь к файлу стиля XSL для формы СЗВ-2 относительно старта директории программы
        /// </summary>
        /// <returns></returns>
        public static string GetXslUrl()
        {
            return Properties.Settings.Default.xsl_szv2;
        }

        /// <summary>
        /// Получить текст с HTML, применив XSL стиль к XML документу
        /// </summary>
        /// <param name="xmlDoc">объект XML документа</param>
        /// <param name="xslFilename">название файла XSL стиля</param>
        /// <returns>Текст HTML</returns>
        public static string GetHtml(XmlDocument xmlDoc, string xslFilename)
        {
            XPathNavigator xpn = xmlDoc.CreateNavigator();
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(xslFilename);
            MemoryStream outStream = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Encoding = Encoding.GetEncoding(1251);
            setting.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(outStream, setting);
            myXslTrans.Transform(xpn, writer);
            String htmlStr = System.Text.Encoding.GetEncoding(1251).GetString(outStream.ToArray());
            return htmlStr;
        }

        /// <summary>
        /// Получить текст с HTML, применив XSL стиль к XML документу
        /// </summary>
        /// <param name="xmlDoc">объект XML документа</param>
        /// <returns></returns>
        public static string GetHtml(XmlDocument xmlDoc)
        {
            return GetHtml(xmlDoc, GetXslUrl());
        }

        /// <summary>
        /// Получить текст с HTML
        /// </summary>
        /// <param name="docsCountTable">Таблица с записями Тип документа/Количество </param>
        /// <param name="docsSumsTable">Таблица с записями Тип документа/Тип группы/Сумма</param>
        /// <returns></returns>
        public static string GetHtml(DataTable docsCountTable, DataTable docsSumsTable)
        {
            return GetHtml(GetXml(docsCountTable, docsSumsTable));
        }

        /// <summary>
        /// Получить текст с HTML
        /// </summary>
        /// <param name="list_id">Идентификатор пакета</param>
        /// <param name="coinnectionStr">Строка подключения к БД</param>
        /// <returns></returns>
        public static string GetHtml(long list_id, string coinnectionStr)
        {
            return GetHtml(GetXml(list_id, coinnectionStr));
        }
        #endregion
    }

    public class Szv3Xml
    {
        // название формы
        public static string name = "СЗВ-3";
        #region названия тегов, присутствующих в xml
        public static string tagSvod = "svod";
        public static string tagPacks = "packs_count";
        public static string tagDocs = "docs_count";
        public static string tagPayment = "payment";
        public static string tagMonth = "month";
        public static string tagMonthCol1 = "col_1";
        public static string tagMonthCol2 = "col_2";
        public static string tagMonthCol3 = "col_3";
        public static string tagMonthCol4 = "col_4";
        public static string tagMonthCol5 = "col_5";
        public static string tagMonthCol6 = "col_6";
        #endregion

        #region Методы - статические
        /// <summary>
        /// Получить XML объект формы СЗВ-3
        /// </summary>
        /// <param name="merge_id">Идентификатор сводной ведомости</param>
        /// <param name="coinnectionStr">Строка подключения к БД</param>
        /// <returns>Объект XML документа</returns>
        public static XmlDocument GetXml(long merge_id, string coinnectionStr)
        {
            DataRow mergeRow = Mergies.GetRow(merge_id, coinnectionStr);
            DataTable mergeInfo = MergeInfo.GetTable(merge_id, coinnectionStr);
            DataTable mergeInfoT = MergeInfoTranspose.CreateTable();
            MergeInfoTranspose.ConvertFromMergeInfo(mergeInfoT, mergeInfo);
            return GetXml(mergeRow, mergeInfoT);
        }

        /// <summary>
        /// Получить XML объект формы СЗВ-3 для актуальной сводной ведомости организации указанного года
        /// </summary>
        /// <param name="org_id">Идентификатор организации</param>
        /// <param name="rep_year">Год</param>
        /// <param name="coinnectionStr">Строка подключения к БД</param>
        /// <returns></returns>
        public static XmlDocument GetXml(long org_id, int rep_year, string coinnectionStr)
        {
            DataRow mergeRow = Mergies.GetActualRow(org_id, rep_year, coinnectionStr);
            DataTable mergeInfo = MergeInfo.GetTable((long)mergeRow[Mergies.id], coinnectionStr);
            DataTable mergeInfoT = MergeInfoTranspose.CreateTable();
            MergeInfoTranspose.ConvertFromMergeInfo(mergeInfoT, mergeInfo);
            return GetXml(mergeRow, mergeInfoT);
        }

        /// <summary>
        /// Получить XML объект формы СЗВ-3
        /// </summary>
        /// <param name="mergeRow">Строка сводной ведомости</param>
        /// <param name="mergeInfoT">Таблицы с суммами сводной ведомости (транспонированная)</param>
        /// <returns></returns>
        public static XmlDocument GetXml(DataRow mergeRow, DataTable mergeInfoT)
        {
            XmlDocument xmlRes = new XmlDocument();
            XmlElement svod = xmlRes.CreateElement(tagSvod);
            XmlElement packsCount = xmlRes.CreateElement(tagPacks);
            XmlElement docsCount = xmlRes.CreateElement(tagDocs);
            XmlElement payment = xmlRes.CreateElement(tagPayment);

            packsCount.InnerText = mergeRow[Mergies.listCount].ToString();
            docsCount.InnerText = mergeRow[Mergies.docCount].ToString();

            xmlRes.AppendChild(xmlRes.CreateXmlDeclaration("1.0", "windows-1251", null));
            xmlRes.AppendChild(svod);
            svod.AppendChild(packsCount);
            svod.AppendChild(docsCount);

            svod.AppendChild(payment);
            for (int i = 0; i < 12; i++)
            {
                XmlElement month = xmlRes.CreateElement(tagMonth);
                XmlElement col1 = xmlRes.CreateElement(tagMonthCol1);
                XmlElement col2 = xmlRes.CreateElement(tagMonthCol2);
                XmlElement col3 = xmlRes.CreateElement(tagMonthCol3);
                XmlElement col4 = xmlRes.CreateElement(tagMonthCol4);
                XmlElement col5 = xmlRes.CreateElement(tagMonthCol5);
                XmlElement col6 = xmlRes.CreateElement(tagMonthCol6);
                col1.InnerText = mergeInfoT.Rows[i][MergeInfoTranspose.col1].ToString().Replace(',', '.');
                col2.InnerText = mergeInfoT.Rows[i][MergeInfoTranspose.col2].ToString().Replace(',', '.');
                col3.InnerText = mergeInfoT.Rows[i][MergeInfoTranspose.col3].ToString().Replace(',', '.');
                col4.InnerText = mergeInfoT.Rows[i][MergeInfoTranspose.col4].ToString().Replace(',', '.');
                col5.InnerText = mergeInfoT.Rows[i][MergeInfoTranspose.col5].ToString().Replace(',', '.');
                col6.InnerText = mergeInfoT.Rows[i][MergeInfoTranspose.col6].ToString().Replace(',', '.');
                month.AppendChild(col1);
                month.AppendChild(col2);
                month.AppendChild(col3);
                month.AppendChild(col4);
                month.AppendChild(col5);
                month.AppendChild(col6);
                payment.AppendChild(month);
            }
            //
            return xmlRes;
        }

        /// <summary>
        /// Получить путь к пустому бланку отчета формы СЗВ-3 относительно старта директории программы
        /// </summary>
        /// <returns></returns>
        public static string GetReportUrl()
        {
            return Properties.Settings.Default.report_szv3;
        }

        /// <summary>
        /// Получить путь к файлу стиля XSL для формы СЗВ-3 относительно старта директории программы
        /// </summary>
        /// <returns></returns>
        public static string GetXslUrl()
        {
            return Properties.Settings.Default.xsl_szv3;
        }

        /// <summary>
        /// Получить текст с HTML, применив XSL стиль к XML документу
        /// </summary>
        /// <param name="xmlDoc">объект XML документа</param>
        /// <param name="xslFilename">название файла XSL стиля</param>
        /// <returns>Текст HTML</returns>
        public static string GetHtml(XmlDocument xmlDoc, string xslFilename)
        {
            XPathNavigator xpn = xmlDoc.CreateNavigator();
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(xslFilename);
            MemoryStream outStream = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Encoding = Encoding.GetEncoding(1251);
            setting.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(outStream, setting);
            myXslTrans.Transform(xpn, writer);
            String htmlStr = System.Text.Encoding.GetEncoding(1251).GetString(outStream.ToArray());
            return htmlStr;
        }

        /// <summary>
        /// Получить текст с HTML, применив XSL стиль к XML документу
        /// </summary>
        /// <param name="xmlDoc">объект XML документа</param>
        /// <returns></returns>
        public static string GetHtml(XmlDocument xmlDoc)
        {
            return XmlData.GetHTML(xmlDoc, GetXslUrl());
        }

        /// <summary>
        /// Получить текст с HTML
        /// </summary>
        /// <param name="mergeRow">Строка с данными данными сводной ведомости с форматом таблицы Mergies</param>
        /// <param name="mergeInfoT">Транспонированная тaблица MergeInfo с числовыпи данными</param>
        /// <returns></returns>
        public static string GetHtml(DataRow mergeRow, DataTable mergeInfoT)
        {
            return GetHtml(GetXml(mergeRow, mergeInfoT));
        }

        /// <summary>
        /// Получить текст с HTML
        /// </summary>
        /// <param name="merge_id">Идентификатор сводной ведомости</param>
        /// <param name="coinnectionStr">Строка подключения к БД</param>
        /// <returns></returns>
        public static string GetHtml(long merge_id, string coinnectionStr)
        {
            return GetHtml(GetXml(merge_id, coinnectionStr));
        }
        #endregion
    }

    public class MapXml
    {
        // название
        public static string name = "map";

        #region Названия тегов, присутствующих в XML
        public static string tagTopics = "TOPICS";
        public static string tagSvod = "SVOD";
        public static string tagTitle = "TITLE";
        public static string tagFilename = "FILENAME";
        public static string tagPath = "PATH";
        public static string tagOpis = "OPIS";
        public static string tagTopic = "TOPIC";
        public static string tagDoctype = "DOCTYPE";
        public static string tagRegnum = "REGNUM";

        public static string paramType = "TYPE";
        public static string paramID = "ID";
        #endregion

        #region Методы - статические
        public static XmlDocument GetXml(IEnumerable<XmlDocument> szv2Array, IEnumerable<IEnumerable<XmlDocument>> szv1Array)
        {
            int docCount, packetCount;
            string rootDirStr = "4";
            packetCount = szv2Array.Count();

            XmlDocument xmlRes = new XmlDocument();
            XmlElement root = xmlRes.CreateElement(tagTopics);
            root.SetAttribute(paramType, "Индивидуальные сведения");
            root.SetAttribute(paramID, rootDirStr);
            XmlElement svod = xmlRes.CreateElement(tagSvod);
            XmlElement svodTitle = xmlRes.CreateElement(tagTitle);
            XmlElement svodFilename = xmlRes.CreateElement(tagFilename);
            XmlElement svodPath = xmlRes.CreateElement(tagPath);
            xmlRes.AppendChild(xmlRes.CreateXmlDeclaration("1.0", "windows-1251", null));
            xmlRes.AppendChild(root);
            root.AppendChild(svod);
            svod.AppendChild(svodTitle);
            svod.AppendChild(svodFilename);
            svod.AppendChild(svodPath);

            svodTitle.InnerText = "Сводная ведомость";
            svodFilename.InnerText = "svod";
            svodPath.InnerText = rootDirStr;

            for (int i = 0; i < packetCount; i++)
            {
                string packetID = string.Format("{0:000}", i + 1);
                XmlElement topics = xmlRes.CreateElement(tagTopics);
                topics.SetAttribute(paramType, string.Format("Пакет {0}", packetID));
                topics.SetAttribute(paramID, packetID);
                XmlElement opis = xmlRes.CreateElement(tagOpis);
                XmlElement opisTitle = xmlRes.CreateElement(tagTitle);
                XmlElement opisFilename = xmlRes.CreateElement(tagFilename);
                XmlElement opisPath = xmlRes.CreateElement(tagPath);

                root.AppendChild(topics);
                topics.AppendChild(opis);
                opis.AppendChild(opisTitle);
                opis.AppendChild(opisFilename);
                opis.AppendChild(opisPath);

                opisTitle.InnerText = "Опись документов";
                opisFilename.InnerText = string.Format("opis{0:000}", i);//GetImito();
                opisPath.InnerText = string.Format(@"4\{0}\", packetID);

                IEnumerable<XmlDocument> szv1Docs = szv1Array.ElementAt(i);
                docCount = szv1Docs.Count();
                for (int j = 0; j < docCount; j++)
                {
                    XmlElement topicNode = xmlRes.CreateElement(tagTopic);
                    XmlElement topicNodeTitle = xmlRes.CreateElement(tagTitle);
                    XmlElement topicNodeFilename = xmlRes.CreateElement(tagFilename);
                    XmlElement topicNodeDoctype = xmlRes.CreateElement(tagDoctype);
                    XmlElement topicNodePath = xmlRes.CreateElement(tagPath);
                    XmlElement topicNodeRegnum = xmlRes.CreateElement(tagRegnum);

                    topics.AppendChild(topicNode);
                    topicNode.AppendChild(topicNodeTitle);
                    topicNode.AppendChild(topicNodeFilename);
                    topicNode.AppendChild(topicNodeDoctype);
                    topicNode.AppendChild(topicNodePath);
                    topicNode.AppendChild(topicNodeRegnum);

                    string fio = string.Format("{0} {1} {2}"
                                            , szv1Docs.ElementAt(j).GetElementsByTagName(Szv1Xml.tagLname)[0].InnerText
                                            , szv1Docs.ElementAt(j).GetElementsByTagName(Szv1Xml.tagFname)[0].InnerText
                                            , szv1Docs.ElementAt(j).GetElementsByTagName(Szv1Xml.tagMname)[0].InnerText
                                            );
                    topicNodeTitle.InnerText = fio;
                    topicNodeFilename.InnerText = string.Format("{0:000}{1:000}", i, j);
                    topicNodeRegnum.InnerText = szv1Docs.ElementAt(j).GetElementsByTagName(Szv1Xml.tagPersonRegnum)[0].InnerText;
                    topicNodeDoctype.InnerText = szv1Docs.ElementAt(j).GetElementsByTagName(Szv1Xml.tagFormType)[0].InnerText;
                    topicNodePath.InnerText = string.Format(@"4\{0}\", packetID);
                }
            }
            //
            return xmlRes;
        }

        public static string GetXslUrl()
        {
            return Properties.Settings.Default.xsl_map;
        }

        public static string GetHTML(byte[] mapXmlBytes, byte[] mapXslBytes)
        {
            return XmlData.GetHTML(mapXmlBytes, mapXslBytes);
        }

        public static string GetHTML(XmlDocument mapXml, string xslFilename)
        {
            return XmlData.GetHTML(mapXml, xslFilename);
        }

        public static string GetHTML(XmlDocument mapXml)
        {
            return GetHTML(mapXml, GetXslUrl());
        }

        public static string GetHTML(byte[] mapXmlBytes)
        {
            return XmlData.GetHTML(mapXmlBytes, GetXslUrl());
        }

        public static string GetHTML(IEnumerable<XmlDocument> szv2Array, IEnumerable<IEnumerable<XmlDocument>> szv1Array)
        {
            return GetHTML(GetXml(szv2Array, szv1Array));
        }
        #endregion
    }

    public class OrgPropXml
    {
        public static string name = "org_property";

        #region Названия тегов, присутствующих в XML
        public static string tagProperties = "Properties";
        public static string tagOrgRegnum = "Organization_Regid";
        public static string tagOrgName = "Organization_Name";
        public static string tagRepyear = "Report_Year";
        public static string tagDirectorType = "Director_Type";
        public static string tagDirectorFIO = "Director_FIO";
        public static string tagBookkeeperFIO = "Bookkeeper_FIO";
        public static string tagPerformer = "Performer";
        public static string tagOperatorName = "Operator_Name";
        public static string tagDate = "Date_of_Construction";
        public static string tagVersion = "Version";
        public static string tagProgramName = "Program_Name";
        public static string tagProgramVersion = "Program_Version";
        #endregion

        #region Поля
        public string orgRegnum;
        public string orgName;
        public string repeyar;
        public string directorType;
        public string directorFIO;
        public string bookkeeperFIO;
        public string performer;
        public string operatorName;
        public DateTime date;
        public string version;
        public string programName;
        public string programVersion;
        #endregion

        #region Конструкторы
        public OrgPropXml()
        {
            orgRegnum = null;
            orgName = null;
            repeyar = null;
            directorType = null;
            directorFIO = null;
            bookkeeperFIO = null;
            performer = null;
            operatorName = null;
            date = DateTime.MinValue;
            version = null;
            programName = null;
            programVersion = null;
        }

        public OrgPropXml(XmlDocument propertyXml)
        {
            TakeValues(propertyXml);
        }
        #endregion

        #region Методы
        public XmlDocument GetXml()
        {
            XmlDocument xmlRes = new XmlDocument();
            XmlElement properties = xmlRes.CreateElement(tagProperties);
            XmlElement elOrgRegnum = xmlRes.CreateElement(tagOrgRegnum);
            XmlElement elOrgName = xmlRes.CreateElement(tagOrgName);
            XmlElement elRepYear = xmlRes.CreateElement(tagRepyear);
            XmlElement elDirectorType = xmlRes.CreateElement(tagDirectorType);
            XmlElement elDirectorFIO = xmlRes.CreateElement(tagDirectorFIO);
            XmlElement elBookkeeperFIO = xmlRes.CreateElement(tagBookkeeperFIO);
            XmlElement elPerformer = xmlRes.CreateElement(tagPerformer);
            XmlElement elOperatorName = xmlRes.CreateElement(tagOperatorName);
            XmlElement elDate = xmlRes.CreateElement(tagDate);
            XmlElement elVersion = xmlRes.CreateElement(tagVersion);
            XmlElement elProgVersion = xmlRes.CreateElement(tagProgramVersion);
            XmlElement elProgName = xmlRes.CreateElement(tagProgramName);

            elOrgRegnum.InnerText = orgRegnum;
            elOrgName.InnerText = orgName;
            elRepYear.InnerText = repeyar;
            elDirectorType.InnerText = directorType;
            elDirectorFIO.InnerText = directorFIO;
            elBookkeeperFIO.InnerText = bookkeeperFIO;
            elPerformer.InnerText = performer;
            elOperatorName.InnerText = operatorName;
            elDate.InnerText = date.ToString("dd.MM.yyyy H:mm:ss");
            elVersion.InnerText = version;
            elProgName.InnerText = programName;
            elProgVersion.InnerText = programVersion;

            xmlRes.AppendChild(xmlRes.CreateXmlDeclaration("1.0", "windows-1251", null));
            xmlRes.AppendChild(properties);
            properties.AppendChild(elOrgRegnum);
            properties.AppendChild(elOrgName);
            properties.AppendChild(elRepYear);
            properties.AppendChild(elDirectorType);
            properties.AppendChild(elDirectorFIO);
            properties.AppendChild(elBookkeeperFIO);
            properties.AppendChild(elPerformer);
            properties.AppendChild(elOperatorName);
            properties.AppendChild(elDate);
            properties.AppendChild(elVersion);
            properties.AppendChild(elProgVersion);
            properties.AppendChild(elProgName);
            //
            return xmlRes;
        }

        public void TakeValues(XmlDocument propertyXml)
        {
            orgRegnum = GetValue(propertyXml, tagOrgRegnum);
            orgName = GetValue(propertyXml, tagOrgName);
            repeyar = GetValue(propertyXml, tagRepyear);
            directorType = GetValue(propertyXml, tagDirectorType);
            directorFIO = GetValue(propertyXml, tagDirectorFIO);
            bookkeeperFIO = GetValue(propertyXml, tagBookkeeperFIO);
            operatorName = GetValue(propertyXml, tagOperatorName);
            date = DateTime.Parse(GetValue(propertyXml, tagDate));
            version = GetValue(propertyXml, tagVersion);
            programName = GetValue(propertyXml, tagProgramName);
            programVersion = GetValue(propertyXml, tagProgramVersion);
        }

        public string GetHTML()
        {
            XmlDocument xml = this.GetXml();
            string html = XmlData.GetHTML(xml, GetXslUrl());
            //
            return html;
        }
        #endregion

        #region Методы - статические
        public static string GetValue(XmlDocument propertyXml, string tagname)
        {
            string res = null;
            XmlNodeList nodes = propertyXml.GetElementsByTagName(tagname);
            if (nodes.Count > 0)
                res = nodes[0].InnerText;
            //
            return res;
        }

        public static string GetXslUrl()
        {
            return Properties.Settings.Default.xsl_orgproperties;
        }
        #endregion
    }
}
