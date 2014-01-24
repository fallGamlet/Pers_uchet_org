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

        static public CompoundFile MakeContainer(XmlDocument mapXml, XmlDocument szv3Xml, 
                                        IEnumerable<XmlDocument> szv2XmlArray, 
                                        IEnumerable<IEnumerable<XmlDocument>> szv1XmlArray)
        {
            XmlElement rootMap = mapXml[MapXml.tagTopics];
            XmlElement svodRootMap = rootMap[MapXml.tagSvod];
            XmlNodeList lists = rootMap.GetElementsByTagName(MapXml.tagTopics);

            if (lists.Count != szv2XmlArray.Count() || lists.Count != szv1XmlArray.Count())
            {
                return null;
            }
            
            CompoundFile cf = new CompoundFile();
            CFStorage dir4 = cf.RootStorage.AddStorage(rootMap.GetAttribute(MapXml.paramID));

            for (int i=0; i< szv2XmlArray.Count(); i++)
            {
                XmlElement curList = lists[i] as XmlElement;
                XmlElement curOpis = curList[MapXml.tagOpis];
                
                CFStorage curDir = dir4.AddStorage(curList.GetAttribute(MapXml.paramID));
                XmlDocument szv2Xml = szv2XmlArray.ElementAt(i);

                CFStream opisStream = curDir.AddStream(curOpis[MapXml.tagFilename].InnerText);
                opisStream.SetData(Encoding.GetEncoding(1251).GetBytes(szv2Xml.InnerXml));

                XmlNodeList docs = curList.GetElementsByTagName(MapXml.tagTopic);
                if (docs.Count != szv1XmlArray.ElementAt(i).Count())
                    continue;

                for (int j = 0; j < szv1XmlArray.ElementAt(i).Count(); j++)
                {
                    XmlDocument szv1Xml = szv1XmlArray.ElementAt(i).ElementAt(j);
                    XmlElement curDoc = docs[j] as XmlElement;
                    CFStream docStream = curDir.AddStream(curDoc[MapXml.tagFilename].InnerText);
                    docStream.SetData(Encoding.GetEncoding(1251).GetBytes(szv1Xml.InnerXml));
                }
            }

            CFStream svod = dir4.AddStream(svodRootMap[MapXml.tagFilename].InnerText);
            svod.SetData(Encoding.GetEncoding(1251).GetBytes(szv3Xml.InnerXml));


            CFStream map = cf.RootStorage.AddStream("map");
            map.SetData(Encoding.GetEncoding(1251).GetBytes(mapXml.InnerXml));

            CFStorage styleDir = cf.RootStorage.AddStorage("styles");
            CFStream mapStyleStream = styleDir.AddStream("map_style");
            CFStream szv1StyleStream = styleDir.AddStream("szv_style");
            CFStream szv3StyleStream = styleDir.AddStream("svod_style");
            CFStream szv2StyleStream = styleDir.AddStream("szv_opis_style");

            mapStyleStream.SetData(System.IO.File.ReadAllBytes(MapXml.GetXslUrl()));
            szv1StyleStream.SetData(System.IO.File.ReadAllBytes(Szv1Xml.GetXslUrl()));
            szv3StyleStream.SetData(System.IO.File.ReadAllBytes(Szv3Xml.GetXslUrl()));
            szv2StyleStream.SetData(System.IO.File.ReadAllBytes(Szv2Xml.GetXslUrl()));

            return cf;
        }
    }
}
