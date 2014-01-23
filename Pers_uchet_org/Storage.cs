using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using OpenMcdf;

namespace Pers_uchet_org
{
    class Storage
    {
        static public int MakeXml(int rep_year, Org org, IEnumerable<long> list_id, string connectionStr,
                                    out XmlDocument mapXml, out XmlDocument szv3Xml, 
                                    out IEnumerable<XmlDocument> szv2XmlArray,
                                    out IEnumerable<IEnumerable<XmlDocument>> szv1XmlArray)
        {
            int res = 0;
            XmlDocument szv3 =  Szv3Xml.GetXml(org.idVal, rep_year, connectionStr);
            LinkedList<XmlDocument> szv2Array = new LinkedList<XmlDocument>();
            LinkedList<IEnumerable<XmlDocument>> szv1Array = new LinkedList<IEnumerable<XmlDocument>>();
            foreach (long listID in list_id)
            {
                XmlDocument szv2 = Szv2Xml.GetXml(listID, connectionStr);
                long[] docsID = Docs.GetDocsID(listID, connectionStr);
                IEnumerable<XmlDocument> szv1 = Szv1Xml.GetXml(docsID, org, connectionStr);
                if (szv1 != null && szv2 != null)
                {
                    szv2Array.AddLast(szv2);
                    szv1Array.AddLast(szv1);
                }
                else
                {
                    res = -1;
                }
            }
            mapXml = MapXml.GetXml(szv2Array, szv1Array);
            szv3Xml = szv3;
            szv2XmlArray = szv2Array;
            szv1XmlArray = szv1Array;
            //
            return res;
        }

        static public int MakeContainer(XmlDocument mapXml, XmlDocument szv3Xml, 
                                        IEnumerable<XmlDocument> szv2XmlArray, 
                                        IEnumerable<IEnumerable<XmlDocument>> szv1XmlArray)
        {
            CompoundFile cf = new CompoundFile();
            CFStorage rootDir = cf.RootStorage.AddStorage("4");
            CFStream svod = rootDir.AddStream("Сводная ведомость");
            //myStream.SetData(Encoding.GetEncoding(1251).GetBytes(res));
            

            cf.Save("D:\\PersUchetContainer\\MyCompoundFile.cfs");
            cf.Close();
            return 0;
        }
    }
}
