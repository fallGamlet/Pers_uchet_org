using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;

namespace Pers_uchet_org
{
    public class XmlData
    {
        public enum ReportType { ADV1 = 0, ADV2, ADV3, ADV4, ADV5, ADV6, SZV1, SZV2, SZV3, RDV1, RDV21, RDV22, RDV3 }

        public static string GetReportUrl(ReportType type)
        {
            string url;
            switch (type)
            {
                case XmlData.ReportType.ADV1:
                    url = Properties.Settings.Default.report_adv1;
                    break;
                case XmlData.ReportType.ADV2:
                    url = Properties.Settings.Default.report_adv2;
                    break;
                case XmlData.ReportType.ADV3:
                    url = Properties.Settings.Default.report_adv3;
                    break;
                case XmlData.ReportType.ADV4:
                    url = Properties.Settings.Default.report_adv4;
                    break;
                case XmlData.ReportType.ADV5:
                    url = Properties.Settings.Default.report_adv5;
                    break;
                case XmlData.ReportType.ADV6:
                    url = Properties.Settings.Default.report_adv6;
                    break;
                case XmlData.ReportType.SZV1:
                    url = Properties.Settings.Default.report_szv1;
                    break;
                case XmlData.ReportType.SZV2:
                    url = Properties.Settings.Default.report_szv2;
                    break;
                case XmlData.ReportType.SZV3:
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
    }

    public class Szv1Xml
    {
        static public XmlDocument GetXml()
        {
            XmlDocument xmlRes = new XmlDocument();
            XmlElement docInfo = xmlRes.CreateElement("doc_info");

            XmlElement person = xmlRes.CreateElement("person");
            XmlNode lname = xmlRes.CreateElement("lname");
            XmlElement fname = xmlRes.CreateElement("fname");
            XmlElement mname = xmlRes.CreateElement("mname");
            XmlElement personRegnum = xmlRes.CreateElement("regnum");
            XmlElement citizen = xmlRes.CreateElement("citizen");
            XmlElement citizen1 = xmlRes.CreateElement("first");
            XmlElement citizen1ID = xmlRes.CreateElement("id");
            XmlElement citizen1Name = xmlRes.CreateElement("name");
            XmlElement citizen2 = xmlRes.CreateElement("second");
            XmlNode citizen2ID = citizen1ID.CloneNode(true);
            XmlNode citizen2Name = citizen1Name.CloneNode(true);
            XmlElement categoryID = xmlRes.CreateElement("category_id");
            XmlElement categoryName = xmlRes.CreateElement("category_name");
            XmlElement privelegeID = xmlRes.CreateElement("privelege_id");
            XmlElement privelegeName = xmlRes.CreateElement("privelege_name");

            XmlElement firm = xmlRes.CreateElement("firm");
            XmlElement firmRegnum = xmlRes.CreateElement("regnum");
            XmlElement firmName = xmlRes.CreateElement("name");

            XmlElement formType = xmlRes.CreateElement("form_type");
            XmlElement workPlace = xmlRes.CreateElement("work_place");
            XmlElement repYear = xmlRes.CreateElement("rep_year");
            XmlElement firmAdd = xmlRes.CreateElement("firm_add");
            XmlElement firmPay = xmlRes.CreateElement("firm_pay");

            XmlElement payment = xmlRes.CreateElement("payment");
            XmlElement month = xmlRes.CreateElement("month");
            XmlElement col1 = xmlRes.CreateElement("col_1");
            XmlElement col2 = xmlRes.CreateElement("col_2");
            XmlElement col3 = xmlRes.CreateElement("col_3");
            XmlElement col4 = xmlRes.CreateElement("col_4");
            XmlElement col5 = xmlRes.CreateElement("col_5");
            XmlElement col6 = xmlRes.CreateElement("col_6");

            XmlElement genPeriod = xmlRes.CreateElement("gen_period");
            XmlElement period = xmlRes.CreateElement("period");
            XmlElement genStart = xmlRes.CreateElement("gen_start");
            XmlElement genEnd = xmlRes.CreateElement("gen_end");

            XmlElement specStaj = xmlRes.CreateElement("spec_staj");
            XmlElement spec = xmlRes.CreateElement("spec");
            XmlElement specStart = xmlRes.CreateElement("start_date");
            XmlElement specEnd = xmlRes.CreateElement("end_date");
            XmlElement specPArtConditionID = xmlRes.CreateElement("part_condition_id");
            XmlElement specPArtConditionName = xmlRes.CreateElement("part_condition_name");
            XmlElement specBaseID = xmlRes.CreateElement("staj_base_id");
            XmlElement specBaseName = xmlRes.CreateElement("staj_base_name");
            XmlElement specServyearBaseID = xmlRes.CreateElement("servyear_base_id");
            XmlElement specServyearBaseName = xmlRes.CreateElement("servyear_base_name");
            XmlElement specMonths = xmlRes.CreateElement("smonths");
            XmlElement specDays = xmlRes.CreateElement("sdays");
            XmlElement specHours = xmlRes.CreateElement("shours");
            XmlElement specMinutes = xmlRes.CreateElement("sminutes");
            XmlElement specProfession = xmlRes.CreateElement("profession");

            XmlElement dopStaj = xmlRes.CreateElement("dop_staj");
            XmlElement dopRecord = xmlRes.CreateElement("record");
            XmlElement dopCodeID = xmlRes.CreateElement("dop_code_id");
            XmlElement dopCodeName = xmlRes.CreateElement("dop_code_name");
            XmlElement dopStart = xmlRes.CreateElement("dop_start");
            XmlElement dopEnd = xmlRes.CreateElement("dop_end");

            xmlRes.AppendChild(xmlRes.CreateXmlDeclaration("1.0", "windows-1251", null));
            xmlRes.AppendChild(docInfo);

            docInfo.AppendChild(person);
            person.AppendChild(lname);
            person.AppendChild(fname);
            person.AppendChild(mname);
            person.AppendChild(personRegnum);
            person.AppendChild(citizen);
            citizen.AppendChild(citizen1);
            citizen1.AppendChild(citizen1ID);
            citizen1.AppendChild(citizen1Name);
            citizen.AppendChild(citizen2);
            citizen2.AppendChild(citizen2ID);
            citizen2.AppendChild(citizen2Name);
            person.AppendChild(categoryID);
            person.AppendChild(categoryName);
            person.AppendChild(privelegeID);
            person.AppendChild(privelegeName);

            docInfo.AppendChild(firm);
            firm.AppendChild(firmRegnum);
            firm.AppendChild(firmName);

            docInfo.AppendChild(formType);
            docInfo.AppendChild(workPlace);
            docInfo.AppendChild(repYear);
            docInfo.AppendChild(firmAdd);
            docInfo.AppendChild(firmPay);

            docInfo.AppendChild(payment);
            month.AppendChild(col1);
            month.AppendChild(col2);
            month.AppendChild(col3);
            month.AppendChild(col4);
            month.AppendChild(col5);
            month.AppendChild(col6);
            for (int i = 0; i < 12; i++)
            {
                payment.AppendChild(month.CloneNode(true));
            }

            docInfo.AppendChild(genPeriod);
            genPeriod.AppendChild(period);
            period.AppendChild(genStart);
            period.AppendChild(genEnd);

            docInfo.AppendChild(specStaj);
            specStaj.AppendChild(spec);
            spec.AppendChild(specStart);
            spec.AppendChild(specEnd);
            spec.AppendChild(specPArtConditionID);
            spec.AppendChild(specPArtConditionName);
            spec.AppendChild(specBaseID);
            spec.AppendChild(specBaseName);
            spec.AppendChild(specServyearBaseID);
            spec.AppendChild(specServyearBaseName);
            spec.AppendChild(specMonths);
            spec.AppendChild(specHours);
            spec.AppendChild(specMinutes);
            spec.AppendChild(specProfession);

            docInfo.AppendChild(dopStaj);
            dopStaj.AppendChild(dopRecord);
            dopRecord.AppendChild(dopCodeID);
            dopRecord.AppendChild(dopCodeName);
            dopRecord.AppendChild(dopStart);
            dopRecord.AppendChild(dopEnd);
            //
            return xmlRes;
        }

        static void GetData(long doc_id)
        {

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
        /// Получить путь к пустому бланку отчета СЗВ-3
        /// </summary>
        /// <returns></returns>
        static public string GetReportUrl()
        {
            return Properties.Settings.Default.report_szv3;
        }
        #endregion
    }

    
}
