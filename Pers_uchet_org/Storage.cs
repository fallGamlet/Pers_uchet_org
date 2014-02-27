using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using OpenMcdf;
using System.IO;

using System.Runtime.InteropServices;
using Microsoft.VisualStudio.OLE.Interop;
using STATSTG = Microsoft.VisualStudio.OLE.Interop.STATSTG;

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

        private static void SetDataToStream(CFStream stream, string data, byte[] diskKey, byte[] diskTable)
        {
            byte[] cryptData = Encoding.GetEncoding(1251).GetBytes(data);
            SetDataToStream(stream, cryptData, diskKey, diskTable);
        }

        private static void SetDataToStream(CFStream stream, byte[] data, byte[] diskKey, byte[] diskTable)
        {
            byte[] synchroArr = GenerateSynchro();
            byte[] cryptData = Mathdll.GostGamma(data, diskKey, diskTable, synchroArr);
            byte[] buffer = new byte[data.Length + synchroArr.Length];
            Array.Copy(cryptData, 0, buffer, 0, cryptData.Length);
            Array.Copy(synchroArr, 0, buffer, cryptData.Length, synchroArr.Length);
            stream.SetData(buffer);
        }

        private static CFStream AddStream(CFStorage dir, XmlDocument dataXml, byte[] diskKey, byte[] diskTable)
        {
            return AddStream(dir, dataXml.InnerXml, diskKey, diskTable);
        }

        private static CFStream AddStream(CFStorage dir, string data, byte[] diskKey, byte[] diskTable)
        {
            byte[] cryptData = Encoding.GetEncoding(1251).GetBytes(data);
            return AddStream(dir, cryptData, diskKey, diskTable);
        }

        private static CFStream AddStream(CFStorage dir, byte[] data, byte[] diskKey, byte[] diskTable)
        {
            byte[] synchroArr = GenerateSynchro();
            byte[] imito = null;
            Mathdll.CryptData(diskKey, diskTable, synchroArr, ref data, out imito);
            CFStream stream = dir.AddStream(ReadKey.BinToHex(imito));
            byte[] buffer = new byte[data.Length + synchroArr.Length];
            Array.Copy(data, 0, buffer, 0, data.Length);
            Array.Copy(synchroArr, 0, buffer, data.Length, synchroArr.Length);
            stream.SetData(buffer);
            return stream;
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

        public static CompoundFile MakeContainer(XmlDocument mapXml, XmlDocument szv3Xml, 
                                        IEnumerable<XmlDocument> szv2XmlArray, 
                                        IEnumerable<IEnumerable<XmlDocument>> szv1XmlArray,
                                        byte[] diskKey, byte[] diskTable)
        {
            XmlElement rootMap = mapXml[MapXml.tagTopics];
            XmlElement svodRootMap = rootMap[MapXml.tagSvod];
            XmlNodeList lists = rootMap.GetElementsByTagName(MapXml.tagTopics);

            if (lists.Count != szv2XmlArray.Count() || lists.Count != szv1XmlArray.Count())
            {
                return null;
            }
            
            CompoundFile container = new CompoundFile(CFSVersion.Ver_3, false, false);
            CFStorage dir4 = container.RootStorage.AddStorage(rootMap.GetAttribute(MapXml.paramID));
            for (int i=0; i< szv2XmlArray.Count(); i++)
            {
                XmlElement curList = lists[i] as XmlElement;
                XmlElement curOpis = curList[MapXml.tagOpis];
                CFStorage curDir = dir4.AddStorage(curList.GetAttribute(MapXml.paramID));
                XmlDocument szv2Xml = szv2XmlArray.ElementAt(i);

                CFStream opisStream = AddStream(curDir, szv2Xml, diskKey, diskTable);
                curOpis[MapXml.tagFilename].InnerText = opisStream.Name;

                XmlNodeList docs = curList.GetElementsByTagName(MapXml.tagTopic);
                if (docs.Count != szv1XmlArray.ElementAt(i).Count())
                    continue;

                for (int j = 0; j < szv1XmlArray.ElementAt(i).Count(); j++)
                {
                    /*if (i >= 10 && j >= 51)
                    {
                        Console.WriteLine("Packet {0}, Doc {1}", i, j);
                    }*/
                    XmlDocument szv1Xml = szv1XmlArray.ElementAt(i).ElementAt(j);
                    XmlElement curDoc = docs[j] as XmlElement;
                    CFStream docStream = AddStream(curDir, szv1Xml, diskKey, diskTable);
                    curDoc[MapXml.tagFilename].InnerText = docStream.Name;
                }
                
            }


            CFStream svod = AddStream(dir4, szv3Xml, diskKey, diskTable);
            svodRootMap[MapXml.tagFilename].InnerText = svod.Name;

            CFStream map = container.RootStorage.AddStream("map");
            SetDataToStream(map, mapXml.InnerXml, diskKey, diskTable);

            CFStorage styleDir = container.RootStorage.AddStorage("styles");
            CFStream mapStyleStream = styleDir.AddStream("map_style");
            CFStream szv1StyleStream = styleDir.AddStream("szv_style");
            CFStream szv3StyleStream = styleDir.AddStream("svod_style");
            CFStream szv2StyleStream = styleDir.AddStream("szv_opis_style");

            SetDataToStream(mapStyleStream, System.IO.File.ReadAllBytes(MapXml.GetXslUrl()), diskKey, diskTable);
            SetDataToStream(szv1StyleStream, System.IO.File.ReadAllBytes(Szv1Xml.GetXslUrl()), diskKey, diskTable);
            SetDataToStream(szv3StyleStream, System.IO.File.ReadAllBytes(Szv3Xml.GetXslUrl()), diskKey, diskTable);
            SetDataToStream(szv2StyleStream, System.IO.File.ReadAllBytes(Szv2Xml.GetXslUrl()), diskKey, diskTable);
            return container;
        }

        public static bool ExportXml(string path, OrgPropXml orgProperty, XmlDocument szv3Xml, 
                                        IEnumerable<XmlDocument> szv2XmlArray, 
                                        IEnumerable<IEnumerable<XmlDocument>> szv1XmlArray)
        {
            if (szv2XmlArray.Count() != szv1XmlArray.Count())
                return false;
            string rootDirStr = string.Format(@"{0}\{1}\{2}",path,orgProperty.orgRegnum,orgProperty.repeyar);
            DirectoryInfo rootDir = Directory.CreateDirectory(rootDirStr);
            szv3Xml.PreserveWhitespace = true;
            szv3Xml.Save(rootDir.FullName + @"\сводная.xml");
            
            for (int i = 0; i < szv2XmlArray.Count(); i++)
            {
                IEnumerable<XmlDocument> szv1XmlNodeArr = szv1XmlArray.ElementAt(i);
                XmlDocument szv2Xml = szv2XmlArray.ElementAt(i);
                szv2Xml.PreserveWhitespace = true;
                szv2Xml.Save(string.Format(@"{0}\z_опись_{1:000}.xml", rootDir.FullName, i+1));
                DirectoryInfo packetDir = Directory.CreateDirectory(string.Format(@"{0}\Пакет_Z{1:000}", rootDir.FullName, i+1));
                
                for (int j = 0; j < szv1XmlNodeArr.Count(); j++)
                {
                    XmlDocument szv1Xml = szv1XmlNodeArr.ElementAt(j);
                    szv1Xml.PreserveWhitespace = true;
                    szv1Xml.Save(string.Format(@"{0}\z_документ_L{1:000}_D{2:000}.xml", packetDir.FullName, i+1, j+1));
                }
            }
            return true;
        }

        public static bool ImportXml(string path, OrgPropXml orgProperty, 
                                        out XmlDocument szv3Xml,
                                        out IEnumerable<XmlDocument> szv2XmlArray,
                                        out IEnumerable<IEnumerable<XmlDocument>> szv1XmlArray)
        {
            szv3Xml = null;
            szv2XmlArray = null;
            szv1XmlArray = null;
            string rootDirStr = string.Format(@"{0}\{1}\{2}", path, orgProperty.orgRegnum, orgProperty.repeyar);
            // если в корневой директории (папке) нет подпапки с Рег номером организации, 
            // в которой в свою очередь нет папки с отчетным годом (RepYear),
            // то процесс импорта невозможен
            if(!Directory.Exists(rootDirStr))
            {
                return false;
            }
            string szv3Filename = rootDirStr+@"\сводная.xml";
            // если нет файла Описи (СЗВ-3) импорт не может быть закончен
            if(!File.Exists(szv3Filename))
            {
                return false;
            }
            DirectoryInfo rootDir = new DirectoryInfo(rootDirStr);
            DirectoryInfo[] packetDirs = rootDir.GetDirectories("Пакет_Z???");
            FileInfo[] opisFiles = rootDir.GetFiles("z_опись_???.xml");
            FileInfo[][] szv1FilesArr = new FileInfo[packetDirs.Count()][];
            // если количество директорий (папок) с документами СЗВ-1 
            // отличается от количества файлов с описями (документы СЗВ-2)
            // процесс импорта следует прекратить
            if (packetDirs.Count() != opisFiles.Count())
            {
                return false;
            }
            string[] dirNumArr = new string[packetDirs.Count()];
            string[] fileNumArr = new string[opisFiles.Count()];
            int i;
            for (i = 0; i < dirNumArr.Length; i++)
            {
                string tmpStr = packetDirs[i].Name;
                dirNumArr[i] = tmpStr.Substring(tmpStr.Length - 3, 3);
                tmpStr = opisFiles[i].Name;
                fileNumArr[i] = tmpStr.Substring(tmpStr.Length - 7, 3);
            }
            int count = 0;
            for (i = 0; i < dirNumArr.Length; i++)
            {
                if (dirNumArr.Contains(fileNumArr[i]))
                {
                    count++;
                }
            }
            // не всем файлам описей (СЗВ-2) найдены 
            // соответствующие директории (папки) пакетов
            if (count < dirNumArr.Length)
            {
                return false;
            }
            count = 0;
            for (i = 0; i < dirNumArr.Length; i++)
            {
                szv1FilesArr[i] = packetDirs[i].GetFiles(string.Format("z_документ_L{0}_D???.xml", dirNumArr[i]));
                // если в директории (папке) есть файлы удовлетворяющие фильиру, 
                // то увеличиваем счетчик
                if (szv1FilesArr[i].Length > 0)
                {
                    count++;
                }
            }
            // если есть директории пакетов без файлов документов СЗВ-1,
            // то импорт невозможен
            if (count < dirNumArr.Length)
            {
                return false;
            }
            
            // Все проверки окончены, теперь можно проводить непосредственно импорт
            List<IEnumerable<XmlDocument>> szv1Lists = new List<IEnumerable<XmlDocument>>(opisFiles.Length);
            List<XmlDocument> szv2List = new List<XmlDocument>(opisFiles.Length);
            for (i = 0; i < opisFiles.Count(); i++)
            {
                XmlDocument szv2Xml = XmlData.ReadXml(opisFiles[i].FullName);
                szv2List.Add(szv2Xml);
                List<XmlDocument> szv1Docs = new List<XmlDocument>(szv1FilesArr[i].Length);
                for (int j = 0; j < szv1FilesArr[i].Length; j++)
                {
                    XmlDocument szv1Xml = XmlData.ReadXml(szv1FilesArr[i][j].FullName);
                    szv1Docs.Add(szv1Xml);
                }
                szv1Lists.Add(szv1Docs.ToArray());
            }
            // заполнение выходных параметров
            szv3Xml = XmlData.ReadXml(szv3Filename);
            szv2XmlArray = szv2List;
            szv1XmlArray = szv1Lists.ToArray();
            //
            return true;
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
            string[] path = streamPath.Split(new char[]{'\\'}, StringSplitOptions.RemoveEmptyEntries);
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
        #endregion
    }

    class CFProperties
    {
        [Flags]
        public enum STGM : int
        {
            DIRECT = 0x00000000,
            TRANSACTED = 0x00010000,
            SIMPLE = 0x08000000,
            READ = 0x00000000,
            WRITE = 0x00000001,
            READWRITE = 0x00000002,
            SHARE_DENY_NONE = 0x00000040,
            SHARE_DENY_READ = 0x00000030,
            SHARE_DENY_WRITE = 0x00000020,
            SHARE_EXCLUSIVE = 0x00000010,
            PRIORITY = 0x00040000,
            DELETEONRELEASE = 0x04000000,
            NOSCRATCH = 0x00100000,
            CREATE = 0x00001000,
            CONVERT = 0x00020000,
            FAILIFTHERE = 0x00000000,
            NOSNAPSHOT = 0x00200000,
            DIRECT_SWMR = 0x00400000,
        }

        enum ulKind : uint
        {
            PRSPEC_LPWSTR = 0,
            PRSPEC_PROPID = 1
        }

        enum CustomInfoProperty : uint
        {
            PIDCI_Organization_Regid = 0x00000002,
            PIDCI_Organization_Name = 0x00000003,
            PIDCI_Report_Year = 0x00000004,
            PIDCI_Director_Type = 0x00000005,
            PIDCI_Director_FIO = 0x00000006,
            PIDCI_Bookkeeper_FIO = 0x00000007,
            PIDCI_Performer = 0x00000008,
            PIDCI_Operator_Name = 0x00000009,
            PIDCI_Date_of_Construction = 0x0000000A,
            PIDCI_Version = 0x0000000B,
            PIDCI_ProgramName = 0x0000000C,
            PIDCI_ProgramVersion = 0x0000000D
        }

        public enum VARTYPE : short
        {
            VT_BSTR = 8,
            VT_FILETIME = 0x40,
            VT_LPSTR = 30,
            VT_LPWSTR = 31,
            VT_CF = 71
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct PROPVARIANTunion
        {
            [FieldOffset(0)]
            public sbyte cVal;
            [FieldOffset(0)]
            public byte bVal;
            [FieldOffset(0)]
            public short iVal;
            [FieldOffset(0)]
            public ushort uiVal;
            [FieldOffset(0)]
            public int lVal;
            [FieldOffset(0)]
            public uint ulVal;
            [FieldOffset(0)]
            public int intVal;
            [FieldOffset(0)]
            public uint uintVal;
            [FieldOffset(0)]
            public long hVal;
            [FieldOffset(0)]
            public ulong uhVal;
            [FieldOffset(0)]
            public float fltVal;
            [FieldOffset(0)]
            public double dblVal;
            [FieldOffset(0)]
            public short boolVal;
            [FieldOffset(0)]
            public int scode;
            [FieldOffset(0)]
            public long cyVal;
            [FieldOffset(0)]
            public double date;
            [FieldOffset(0)]
            public long filetime;
            [FieldOffset(0)]
            public IntPtr bstrVal;
            [FieldOffset(0)]
            public IntPtr pszVal;
            [FieldOffset(0)]
            public IntPtr pwszVal;
            [FieldOffset(0)]
            public IntPtr punkVal;
            [FieldOffset(0)]
            public IntPtr pdispVal;
        }

        public enum grfFlags : uint
        {
            PROPSETFLAG_DEFAULT = 0,
            PROPSETFLAG_NONSIMPLE = 1,
            PROPSETFLAG_ANSI = 2,
            PROPSETFLAG_UNBUFFERED = 4,
            PROPSETFLAG_CASE_SENSITIVE = 8
        }

        [DllImport("ole32.dll")]
        static extern int StgOpenStorage(
        [MarshalAs(UnmanagedType.LPWStr)]string pwcsName, IStorage pstgPriority,
        int grfMode, IntPtr snbExclude, uint reserved, out IStorage ppstgOpen);

        [DllImport("ole32.dll")]
        static extern int StgCreatePropSetStg(IStorage pStorage, uint reserved,
        out IPropertySetStorage ppPropSetStg);

        [DllImport("ole32.dll")]
        private extern static int PropVariantClear(ref PROPVARIANT pvar);
        
        [ComImport]
        [Guid("00000138-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPropertyStorage
        {
            [PreserveSig]
            int ReadMultiple(uint cpspec,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] PropertySpec[] rgpspec,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] PropertyVariant[] rgpropvar);

            [PreserveSig]
            void WriteMultiple(uint cpspec,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] PropertySpec[] rgpspec,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] PropertyVariant[] rgpropvar,
            uint propidNameFirst);

            [PreserveSig]
            uint DeleteMultiple(uint cpspec,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] PropertySpec[] rgpspec);
            [PreserveSig]
            uint ReadPropertyNames(uint cpropid,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] uint[] rgpropid,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] [Out] string[] rglpwstrName);
            [PreserveSig]
            uint NotDeclared1();
            [PreserveSig]
            uint NotDeclared2();
            [PreserveSig]
            uint Commit(uint grfCommitFlags);
            [PreserveSig]
            uint NotDeclared3();
            [PreserveSig]
            uint Enum(out IEnumSTATPROPSTG ppenum);
        }

        [ComImport]
        [Guid("0000013A-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPropertySetStorage
        {
            [PreserveSig]
            uint Create(ref Guid rfmtid, ref Guid pclsid, uint grfFlags, uint grfMode, out IPropertyStorage ppprstg);
            [PreserveSig]
            uint Open(ref Guid rfmtid, STGM grfMode, out IPropertyStorage ppprstg);
            [PreserveSig]
            uint NotDeclared3();
            [PreserveSig]
            uint Enum(out IEnumSTATPROPSETSTG ppenum);
        }

        public enum PropertySpecKind
        {
            Lpwstr,
            PropId
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PropertySpec
        {
            public PropertySpecKind kind;
            public PropertySpecData data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct PropertySpecData
        {
            [FieldOffset(0)]
            public uint propertyId;
            [FieldOffset(0)]
            public IntPtr name;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct PropertyVariant
        {
            [FieldOffset(0)]
            public VARTYPE vt;
            [FieldOffset(2)]
            public ushort wReserved1;
            [FieldOffset(4)]
            public ushort wReserved2;
            [FieldOffset(6)]
            public ushort wReserved3;
            [FieldOffset(8)]
            public PROPVARIANTunion unionmember;
        }

        private static PropertySpec CreateProperty(string name)
        {
            PropertySpec prop = new PropertySpec();
            byte[] data = Encoding.Unicode.GetBytes(name+"\0");
            IntPtr ptrData = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, ptrData, data.Length);
            prop.kind = PropertySpecKind.Lpwstr;
            prop.data.name = ptrData;
            return prop;
        }

        private static PropertyVariant CreatePropertyValue(string value)
        {
            PropertyVariant prop = new PropertyVariant();
            byte[] data = Encoding.GetEncoding(1251).GetBytes(value+"\0");
            IntPtr ptrData = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, ptrData, data.Length);
            prop.vt = VARTYPE.VT_LPSTR;
            prop.unionmember.pszVal = ptrData;
            return prop;
        }

        public static string GetString(IntPtr ptrData)
        {
            string tmp = Marshal.PtrToStringAnsi(ptrData);
            byte[] bData = new byte[tmp.Length];
            Marshal.Copy(ptrData, bData, 0, bData.Length);
            //
            return GetString(bData);
        }

        private static string GetString(byte[] data)
        {
            return Encoding.GetEncoding(1251).GetString(data);
        }

        private static void ReleaseProperties(PropertySpec[] propNames, PropertyVariant[] propValues)
        {
            foreach (PropertySpec item in propNames)
            {
                Marshal.FreeHGlobal(item.data.name);
            }
            foreach (PropertyVariant item in propValues)
            {
                Marshal.FreeHGlobal(item.unionmember.pszVal);
            }
        }

        static public void AddProperty(string path, OrgPropXml properties)
        {
            IStorage Is;
            if (StgOpenStorage(path, null, (int)(STGM.SHARE_EXCLUSIVE | STGM.READWRITE), IntPtr.Zero, 0, out Is) == 0 && Is != null)
            {
                IPropertySetStorage pss;
                if (StgCreatePropSetStg(Is, 0, out pss) == 0)
                {
                    var FMTID_CustomInformation = new Guid("{D170DF2E-1117-11D2-AA01-00805FFE11B8}");
                    var pCLSID = new Guid("{00000000-0000-0000-0000-000000000000}");
                    IPropertyStorage ps;
                    pss.Create(ref FMTID_CustomInformation, ref pCLSID, (uint)grfFlags.PROPSETFLAG_DEFAULT, (uint)(STGM.SHARE_EXCLUSIVE | STGM.READWRITE), out ps);

                    if (ps == null)
                    {
                        throw new Exception("Don't create container custom property");
                    }

                    PropertySpec[] propSpec = new PropertySpec[12];
                    PropertyVariant[] propVariant = new PropertyVariant[12];

                    propSpec[0] = CreateProperty(OrgPropXml.tagOrgRegnum);
                    propSpec[1] = CreateProperty(OrgPropXml.tagOrgName);
                    propSpec[2] = CreateProperty(OrgPropXml.tagRepyear);
                    propSpec[3] = CreateProperty(OrgPropXml.tagDirectorType);
                    propSpec[4] = CreateProperty(OrgPropXml.tagDirectorFIO);
                    propSpec[5] = CreateProperty(OrgPropXml.tagBookkeeperFIO);
                    propSpec[6] = CreateProperty(OrgPropXml.tagPerformer);
                    propSpec[7] = CreateProperty(OrgPropXml.tagOperatorName);
                    propSpec[8] = CreateProperty(OrgPropXml.tagDate);
                    propSpec[9] = CreateProperty(OrgPropXml.tagVersion);
                    propSpec[10] = CreateProperty(OrgPropXml.tagProgramName);
                    propSpec[11] = CreateProperty(OrgPropXml.tagProgramVersion);

                    propVariant[0] = CreatePropertyValue(properties.orgRegnum);
                    propVariant[1] = CreatePropertyValue(properties.orgName);
                    propVariant[2] = CreatePropertyValue(properties.repeyar);
                    propVariant[3] = CreatePropertyValue(properties.directorType);
                    propVariant[4] = CreatePropertyValue(properties.directorFIO);
                    propVariant[5] = CreatePropertyValue(properties.bookkeeperFIO);
                    propVariant[6] = CreatePropertyValue(properties.performer);
                    propVariant[7] = CreatePropertyValue(properties.operatorName);
                    propVariant[8] = CreatePropertyValue(properties.date.ToString("dd.MM.yyyy H:mm:ss"));
                    propVariant[9] = CreatePropertyValue(properties.version);
                    propVariant[10] = CreatePropertyValue(properties.programName);
                    propVariant[11] = CreatePropertyValue(properties.programVersion);

                    ps.WriteMultiple(12, propSpec, propVariant, 2);
                    ps.Commit(0);

                    ReleaseProperties(propSpec, propVariant);
                    Marshal.FinalReleaseComObject(pss);
                    pss = null;
                }
                else
                {
                    Console.WriteLine("Could not create property set storage");
                }

                Marshal.FinalReleaseComObject(Is);
                Is = null;
            }
            else
            {
                Console.WriteLine("File does not contain a structured storage");
            }

            GC.Collect();
        }

        static public OrgPropXml ReadProperty(string path)
        {
            IStorage Is;
            OrgPropXml properties = null;
            if (StgOpenStorage(path, null, (int)(STGM.SHARE_EXCLUSIVE | STGM.READWRITE), IntPtr.Zero, 0, out Is) == 0 && Is != null)
            {
                IPropertySetStorage pss;
                if (StgCreatePropSetStg(Is, 0, out pss) == 0)
                {
                    var FMTID_CustomInformation = new Guid("{D170DF2E-1117-11D2-AA01-00805FFE11B8}");
                    var pCLSID = new Guid("{00000000-0000-0000-0000-000000000000}");
                    IPropertyStorage ps;
                    UInt32 propCount = 12;
                    pss.Open(ref FMTID_CustomInformation, (STGM.SHARE_EXCLUSIVE | STGM.READ), out ps);
                    if (ps != null)
                    {
                        PropertySpec[] propSpec = new PropertySpec[propCount];
                        PropertyVariant[] propVariant = new PropertyVariant[propCount];

                        propSpec[0] = CreateProperty(OrgPropXml.tagOrgRegnum);
                        propSpec[1] = CreateProperty(OrgPropXml.tagOrgName);
                        propSpec[2] = CreateProperty(OrgPropXml.tagRepyear);
                        propSpec[3] = CreateProperty(OrgPropXml.tagDirectorType);
                        propSpec[4] = CreateProperty(OrgPropXml.tagDirectorFIO);
                        propSpec[5] = CreateProperty(OrgPropXml.tagBookkeeperFIO);
                        propSpec[6] = CreateProperty(OrgPropXml.tagPerformer);
                        propSpec[7] = CreateProperty(OrgPropXml.tagOperatorName);
                        propSpec[8] = CreateProperty(OrgPropXml.tagDate);
                        propSpec[9] = CreateProperty(OrgPropXml.tagVersion);
                        propSpec[10] = CreateProperty(OrgPropXml.tagProgramName);
                        propSpec[11] = CreateProperty(OrgPropXml.tagProgramVersion);

                        int res = ps.ReadMultiple(propCount, propSpec, propVariant);

                        properties = new OrgPropXml();

                        properties.orgRegnum = GetString(propVariant[0].unionmember.pszVal);
                        properties.orgName = GetString(propVariant[1].unionmember.pszVal);
                        properties.repeyar = GetString(propVariant[2].unionmember.pszVal);
                        properties.directorType = GetString(propVariant[3].unionmember.pszVal);
                        properties.directorFIO = GetString(propVariant[4].unionmember.pszVal);
                        properties.bookkeeperFIO = GetString(propVariant[5].unionmember.pszVal);
                        properties.performer = GetString(propVariant[6].unionmember.pszVal);
                        properties.operatorName = GetString(propVariant[7].unionmember.pszVal);
                        properties.date = DateTime.ParseExact(GetString(propVariant[8].unionmember.pszVal), 
                                                                "dd.MM.yyyy H:mm:ss", 
                                                                System.Globalization.CultureInfo.InvariantCulture);
                        properties.version = GetString(propVariant[9].unionmember.pszVal);
                        properties.programName = GetString(propVariant[10].unionmember.pszVal);
                        properties.programVersion = GetString(propVariant[11].unionmember.pszVal);
                        


                        Marshal.FinalReleaseComObject(ps);
                        ps = null;
                    }
                    else
                    {
                        Console.WriteLine("Could not open property storage");
                    }

                    Marshal.FinalReleaseComObject(pss);
                    pss = null;
                }
                else
                {
                    Console.WriteLine("Could not create property set storage");
                }

                Marshal.FinalReleaseComObject(Is);
                Is = null;
            }
            else
            {
                Console.WriteLine("File does not contain a structured storage");
            }

            GC.Collect();
            return properties;
        }
    }
}
