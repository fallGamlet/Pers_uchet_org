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

namespace Pers_uchet_org
{
    public class XmlData
    {
        public enum ReportType { Adv1 = 0, Adv2, Adv3, Adv4, Adv5, Adv6, Szv1, Szv2, Szv3, Rdv1, Rdv21, Rdv22, Rdv3 }

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
    }

    public class Szv1Xml
    {
        // название формы
        static public string name = "СЗВ-1";
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
        public static string tagFormType = "form_type";
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

        static public XmlDocument GetXml(DataRow docRow, Org org, DataTable salaryInfoTable, DataTable salaryInfoTableTranspose, DataTable generalTable, DataTable specTable, DataTable dopTable)
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

            firmAdd.InnerText = ((double)salaryInfoTable.Rows[SalaryInfo.FindRowIndex(salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column3)][SalaryInfo.sum]).ToString("F2");
            firmPay.InnerText = ((double)salaryInfoTable.Rows[SalaryInfo.FindRowIndex(salaryInfoTable, SalaryInfo.salaryGroupsId, (long)SalaryGroups.Column5)][SalaryInfo.sum]).ToString("F2");

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
                specPartConditionId.InnerText = row[SpecialPeriodView.partCondition].ToString();
                specPartConditionName.InnerText = row[SpecialPeriodView.partCode].ToString();
                specBaseId.InnerText = row[SpecialPeriodView.stajBase].ToString();
                specBaseName.InnerText = row[SpecialPeriodView.stajCode].ToString();
                specServyearBaseId.InnerText = row[SpecialPeriodView.servYearBase].ToString();
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

        static public string GetReportUrl()
        {
            return Properties.Settings.Default.report_szv1;
        }
    }

    public class Szv2Xml
    {
        static public string name = "СЗВ-2";
        #region названия тегов, присутствующих в xml
        static public string tagInddocs = "inddocs";
        static public string tagInddoc = "inddoc";
        static public string tagTypeID = "type_id";
        static public string tagCount = "count";
        static public string tagSummaryInfo = "summary_info";
        static public string tagCol1 = "col_1";
        static public string tagCol2 = "col_2";
        static public string tagCol3 = "col_3";
        static public string tagCol4 = "col_4";
        static public string tagCol5 = "col_5";
        #endregion

        #region Методы - статические
        static void NormalizeDocsCountTable(DataTable docsCount)
        {
            bool init, correct, cancel, granting;
            init = correct = cancel = granting = false;
            long docTypeID;
            foreach(DataRow row in docsCount.Rows)
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
        static public XmlDocument GetXml(DataTable docsCount, DataTable docsSums)
        {
            NormalizeDocsCountTable(docsCount);

            XmlDocument xmlRes = new XmlDocument();
            XmlElement inddocs = xmlRes.CreateElement(tagInddocs);
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

                foreach(DataRow sumRow in docsSums.Rows)
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
        static public XmlDocument GetXml(long list_id, string connectionStr)
        {
            DataTable docsCount = Docs.CountDocsByListAndType(list_id, connectionStr);
            DataTable docsSums = Docs.SumsByDocType(list_id, connectionStr);
            return GetXml(docsCount, docsSums);
        }

        /// <summary>
        /// Получить путь к пустому бланку отчета СЗВ-2
        /// </summary>
        /// <returns></returns>
        static public string GetReportUrl()
        {
            return Properties.Settings.Default.report_szv2;
        }
        #endregion
    }

    public class Szv3Xml
    {
        // название формы
        static public string name = "СЗВ-3";
        #region названия тегов, присутствующих в xml
        static public string tagSvod = "svod";
        static public string tagPacks = "packs_count";
        static public string tagDocs = "docs_count";
        static public string tagPayment = "payment";
        static public string tagMonth = "month";
        static public string tagMonthCol1 = "col_1";
        static public string tagMonthCol2 = "col_2";
        static public string tagMonthCol3 = "col_3";
        static public string tagMonthCol4 = "col_4";
        static public string tagMonthCol5 = "col_5";
        static public string tagMonthCol6 = "col_6";
        #endregion

        #region Методы - статические
        /// <summary>
        /// Получить XML объект формы СЗВ-3
        /// </summary>
        /// <param name="merge_id">Идентификатор сводной ведомости</param>
        /// <param name="coinnectionStr">Строка подключения к БД</param>
        /// <returns>объект XML документа</returns>
        static public XmlDocument GetXml(long merge_id, string coinnectionStr)
        {
            DataRow mergeRow = Mergies.GetRow(merge_id, coinnectionStr);
            DataTable mergeInfo = MergeInfo.GetTable(merge_id, coinnectionStr);
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
        static public XmlDocument GetXml(DataRow mergeRow, DataTable mergeInfoT)
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
        static public string GetReportUrl()
        {
            return Properties.Settings.Default.report_szv3;
        }

        /// <summary>
        /// Получить путь к файлу стиля XSL для формы СЗВ-3 относительно старта директории программы
        /// </summary>
        /// <returns></returns>
        static public string GetXslUrl()
        {
            return Properties.Settings.Default.xsl_szv3;
        }

        static public string GetHTML(XmlDocument xmlDoc, string xslFilename)
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

        static public string GetHTML(XmlDocument xmlDoc)
        {
            return GetHTML(xmlDoc, GetXslUrl());
        }

        static public string GetHTML(DataRow mergeRow, DataTable mergeInfoT)
        {
            return GetHTML(GetXml(mergeRow, mergeInfoT));
        }

        static public string GetHTML(long merge_id, string coinnectionStr)
        {
            return GetHTML(GetXml(merge_id, coinnectionStr));
        }
        #endregion
    }
}
