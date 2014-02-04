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
        #region Поля
        private static Random rand = new Random((int)DateTime.Now.Ticks);
        #endregion

        #region Методы - статические
        private static byte[] GenerateSynchro()
        {
            long tick = DateTime.Now.Ticks;
            tick *= rand.Next();
            return BitConverter.GetBytes(tick);
        }

        private static void SetDataToStream(CFStream stream, string data, byte[] diskTable, byte[] diskKey)
        {
            byte[] cryptData = Encoding.GetEncoding(1251).GetBytes(data);
            SetDataToStream(stream, cryptData, diskTable, diskKey);
        }

        private static void SetDataToStream(CFStream stream, byte[] data, byte[] diskTable, byte[] diskKey)
        {
            byte[] synchroArr = GenerateSynchro();
            byte[] cryptData = Mathdll.GostGamma(data, diskKey, diskTable, synchroArr);
            stream.SetData(cryptData);
            stream.AppendData(synchroArr);
        }

        public static int MakeXml(int rep_year, Org org, IEnumerable<long> list_id, string connectionStr,
                                    out XmlDocument mapXml, 
                                    out XmlDocument szv3Xml, 
                                    out IEnumerable<XmlDocument> szv2XmlArray,
                                    out IEnumerable<IEnumerable<XmlDocument>> szv1XmlArray)
        {
            int res = 0;
            XmlDocument szv3 = Szv3Xml.GetXml(org.idVal, rep_year, connectionStr);
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

        public static CompoundFile MakeContainer(XmlDocument propertyXml, XmlDocument mapXml, XmlDocument szv3Xml, 
                                        IEnumerable<XmlDocument> szv2XmlArray, 
                                        IEnumerable<IEnumerable<XmlDocument>> szv1XmlArray,
                                        byte[] diskTable, byte[] diskKey)
        {
            XmlElement rootMap = mapXml[MapXml.tagTopics];
            XmlElement svodRootMap = rootMap[MapXml.tagSvod];
            XmlNodeList lists = rootMap.GetElementsByTagName(MapXml.tagTopics);

            if (lists.Count != szv2XmlArray.Count() || lists.Count != szv1XmlArray.Count())
            {
                return null;
            }
            
            CompoundFile container = new CompoundFile();
            CFStorage dir4 = container.RootStorage.AddStorage(rootMap.GetAttribute(MapXml.paramID));
            
            for (int i=0; i< szv2XmlArray.Count(); i++)
            {
                XmlElement curList = lists[i] as XmlElement;
                XmlElement curOpis = curList[MapXml.tagOpis];
                
                CFStorage curDir = dir4.AddStorage(curList.GetAttribute(MapXml.paramID));
                XmlDocument szv2Xml = szv2XmlArray.ElementAt(i);

                CFStream opisStream = curDir.AddStream(curOpis[MapXml.tagFilename].InnerText);
                SetDataToStream(opisStream, szv2Xml.InnerXml, diskTable, diskKey);

                XmlNodeList docs = curList.GetElementsByTagName(MapXml.tagTopic);
                if (docs.Count != szv1XmlArray.ElementAt(i).Count())
                    continue;

                for (int j = 0; j < szv1XmlArray.ElementAt(i).Count(); j++)
                {
                    XmlDocument szv1Xml = szv1XmlArray.ElementAt(i).ElementAt(j);
                    XmlElement curDoc = docs[j] as XmlElement;
                    CFStream docStream = curDir.AddStream(curDoc[MapXml.tagFilename].InnerText);
                    SetDataToStream(docStream, szv1Xml.InnerXml, diskTable, diskKey);
                }
            }

            CFStream svod = dir4.AddStream(svodRootMap[MapXml.tagFilename].InnerText);
            SetDataToStream(svod, szv3Xml.InnerXml, diskTable, diskKey);

            CFStream map = container.RootStorage.AddStream("map");
            SetDataToStream(map, mapXml.InnerXml, diskTable, diskKey);

            CFStorage styleDir = container.RootStorage.AddStorage("styles");
            CFStream mapStyleStream = styleDir.AddStream("map_style");
            CFStream szv1StyleStream = styleDir.AddStream("szv_style");
            CFStream szv3StyleStream = styleDir.AddStream("svod_style");
            CFStream szv2StyleStream = styleDir.AddStream("szv_opis_style");

            SetDataToStream(mapStyleStream, System.IO.File.ReadAllBytes(MapXml.GetXslUrl()), diskTable, diskKey);
            SetDataToStream(szv1StyleStream, System.IO.File.ReadAllBytes(Szv1Xml.GetXslUrl()), diskTable, diskKey);
            SetDataToStream(szv3StyleStream, System.IO.File.ReadAllBytes(Szv3Xml.GetXslUrl()), diskTable, diskKey);
            SetDataToStream(szv2StyleStream, System.IO.File.ReadAllBytes(Szv2Xml.GetXslUrl()), diskTable, diskKey);

            CFStream property = container.RootStorage.AddStream("property");
            property.SetData(Encoding.GetEncoding(1251).GetBytes(propertyXml.InnerXml));
            //
            return container;
        }

        public static byte[] DecryptStream(CFStream stream, byte[] diskKey, byte[] diskTable)
        {
            int count = (int)stream.Size - 8;
            byte[] resData = stream.GetData(0, ref count);
            count = 8;
            byte[] synchro = stream.GetData(stream.Size - 8, ref count);

            resData =  Mathdll.GostGamma(resData, diskKey, diskTable, synchro);
            return resData;
        }

        public static CFStream GetFileStream(CompoundFile file, string streamPath)
        {
            string[] path = streamPath.Split('\\');
            CFStorage curDir = file.RootStorage;
            for (int i = 0; i < path.Length - 1; i++)
            {
                curDir = curDir.GetStorage(path[i]);
            }
            return curDir.GetStream(path[path.Length-1]);
        }

        public static string GetHTML(CompoundFile file, string fileUri, byte[] diskKey, byte[] diskTable)
        {
            string[] uri = fileUri.Split(':');
            if(uri.Length != 2)
                return null;
            string xslPath;
            switch (uri[0])
            {
                case "svd":
                    xslPath = @"styles\svod_style";
                    break;
                case "ops":
                    xslPath = @"styles\szv_opis_style";
                    break;
                case "ind":
                    xslPath = @"styles\szv_style";
                    break;
                default:
                    xslPath = null;
                    break;
            }
            if (xslPath == null)
                return null;
            CFStream xmlStream = GetFileStream(file, uri[1]);
            CFStream xslStream = GetFileStream(file, xslPath);
            byte[] xmlData = DecryptStream(xmlStream, diskKey, diskTable);
            byte[] xslData = DecryptStream(xslStream, diskKey, diskTable);
            string resHTML = XmlData.GetHTML(xmlData, xslData);
            //
            return resHTML;
        }

        public static void Save(CompoundFile file, string path)
        {

        }
        #endregion
    }
}
