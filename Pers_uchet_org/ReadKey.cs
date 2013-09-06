using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Pers_uchet_org
{


    class ReadKey
    {
        #region unmanaged CreateFile function
        public enum EFileAccess
        {
            GenericRead = unchecked((int)0x80000000),
            GenericWrite = 0x40000000,
            GenericExecute = 0x20000000,
            GenericAll = 0x10000000
        }

        public enum EFileShare
        {
            None = 0x0,
            Read = 0x1,
            Write = 0x2,
            Delete = 0x4
        }

        public enum ECreationDisposition
        {
            CreateNew = 1,
            CreateAlways = 2,
            OpenExisting = 3,
            OpenAlways = 4,
            TruncateExisting = 5
        }

        public enum EFileAttributes
        {
            eReadonly = 0x1,
            eHidden = 0x2,
            eSystem = 0x4,
            eDirectory = 0x10,
            eArchive = 0x20,
            eDevice = 0x40,
            eNormal = 0x80,
            eTemporary = 0x100,
            eSparseFile = 0x200,
            eReparsePoint = 0x400,
            eCompressed = 0x800,
            eOffline = 0x1000,
            eNotContentIndexed = 0x2000,
            eEncrypted = 0x4000,
            eWrite_Through = unchecked((int)0x80000000),
            eOverlapped = 0x40000000,
            eNoBuffering = 0x20000000,
            eRandomAccess = 0x10000000,
            eSequentialScan = 0x8000000,
            eDeleteOnClose = 0x4000000,
            eBackupSemantics = 0x2000000,
            ePosixSemantics = 0x1000000,
            eOpenReparsePoint = 0x200000,
            eOpenNoRecall = 0x100000,
            eFirstPipeInstance = 0x80000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
        #endregion

        #region Методы
        public static string ReadCD(string driveName, long shift)
        {
            if (shift % 2048 > 0)
            {
                throw new ArgumentException("Смещение не кратно 2048 байт. Чтение данных невозможно.");
            }
            byte[] buff = new byte[2048];
            string lpFileName = @"\\.\" + driveName;
            using (SafeFileHandle drive = CreateFile(
                  lpFileName,
                  unchecked((uint)EFileAccess.GenericRead),
                  (uint)EFileShare.Read,
                  IntPtr.Zero,
                  (uint)ECreationDisposition.OpenExisting,
                  (uint)EFileAttributes.eNormal,
                  IntPtr.Zero))
            {
                if (drive.IsInvalid)
                    throw new IOException("Unable to access drive. Win32 Error Code " + Marshal.GetLastWin32Error());

                using (FileStream stream = new FileStream(drive, FileAccess.Read))
                {
                    stream.Seek(shift, SeekOrigin.Begin);
                    stream.Read(buff, 0, 2048);
                }

                string key;
                key = ArrayToString(buff);
                return BinToHex(key);
            }
        }

        public static string ReadCDImage(string imgFilePath, long shift)
        {
            if (shift % 2048 > 0)
            {
                throw new ArgumentException("Смещение не кратно 2048 байт. Чтение данных невозможно.");
            }
            byte[] buff = new byte[2048];

            using (FileStream stream = (new FileInfo(imgFilePath)).OpenRead())
            {
                stream.Seek(shift, SeekOrigin.Begin);
                stream.Read(buff, 0, 2048);
            }

            string key;
            key = ArrayToString(buff);
            return BinToHex(key);
        }

        public static string ReadDiskette(string driveName, long shift)
        {
            if (shift % 512 > 0)
            {
                throw new ArgumentException("Смещение не кратно 512 байт. Чтение данных невозможно.");
            }
            byte[] buff = new byte[512];
            string lpFileName = @"\\.\" + driveName;
            using (SafeFileHandle drive = CreateFile(
                  lpFileName,
                  unchecked((uint)EFileAccess.GenericRead),
                  (uint)EFileShare.Read,
                  IntPtr.Zero,
                  (uint)ECreationDisposition.OpenExisting,
                  (uint)EFileAttributes.eNormal,
                  IntPtr.Zero))
            {
                if (drive.IsInvalid)
                    throw new IOException("Unable to access drive. Win32 Error Code " + Marshal.GetLastWin32Error());

                using (FileStream stream = new FileStream(drive, FileAccess.Read))
                {
                    stream.Seek(shift, SeekOrigin.Begin);
                    stream.Read(buff, 0, 512);
                }

                string key;
                key = ArrayToString(buff);
                return BinToHex(key);
            }
        }

        public static string ReadDisketteImage(string imgFilePath, long shift)
        {
            if (shift % 512 > 0)
            {
                throw new ArgumentException("Смещение не кратно 2048 байт. Чтение данных невозможно.");
            }
            byte[] buff = new byte[512];

            using (FileStream stream = (new FileInfo(imgFilePath)).OpenRead())
            {
                stream.Seek(shift, SeekOrigin.Begin);
                stream.Read(buff, 0, 512);
            }

            string key;
            key = ArrayToString(buff);
            return BinToHex(key);
        }

        public static string ArrayToString(byte[] arr)
        {
            string strOut = String.Empty;
            foreach (int x in arr)
            {
                strOut += (char)x;
            }
            return strOut;
        }

        public static byte[] StringToArray(string str)
        {
            byte[] byteArr = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                byteArr[i] = (byte)str[i];
            }
            return byteArr;
        }

        public static string BinToHex(string binStr)
        {
            string[] hexins = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            string tmp = "";
            int ibuf = 0;
            int m, n;
            for (int i = 0; i < binStr.Length; i++)
            {
                ibuf = Convert.ToInt32(binStr[i]);
                m = (ibuf & 0xFF) / 16;
                n = (ibuf & 0xFF) % 16;
                tmp += hexins[m] + hexins[n];

            }
            return tmp;
        }

        public static byte[] HexToBin(string hexStr)
        {
            byte[] byteArr = new byte[hexStr.Length / 2 + hexStr.Length % 2];
            for (int i = 0, j = 0; i < hexStr.Length / 2; i++, j += 2)
            {
                byteArr[i] = (byte)Convert.ToInt32(hexStr.Substring(j, 2), 16);
            }
            return byteArr;
        }

        public static string Julian2Date(long jDate)
        {
            long l;
            int n, i, j, dd, mm, yy;
            try
            {
                l = jDate + 68569;
                n = (int)(4 * l / 146097);
                l = l - (int)((146097 * n + 3) / 4);
                i = (int)(4000 * (l + 1) / 1461001);
                l = l - (int)(1461 * i / 4) + 31;
                j = (int)(80 * l / 2447);
                dd = (int)(l - (int)(2447 * j / 80));
                l = (int)(j / 11);
                mm = (int)(j + 2 - (12 * l));
                yy = (int)(100 * (n - 49) + i + l);
                return dd + "." + mm + "." + yy;
            }
            catch
            {
                return "";
            }
        }

        public static long Date2Julian(DateTime date)
        {
            long x1, x2, x3;
            try
            {
                x1 = 1461 * (date.Year + 4800 + (date.Month - 14) / 12) / 4;
                x2 = (367 * (date.Month - 2 - 12 * (((date.Month - 14) / 12)))) / 12;
                x3 = (3 * (date.Year + 4900 + (date.Month - 14) / 12) / 100) / 4;
                return x1 + x2 - x3 + date.Day - 32075;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        //тест
    }
}
