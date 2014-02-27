using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Pers_uchet_org
{
    class Mathdll
    {
        [DllImport(@"mathdll.dll")]
        static private extern int gost_gamma(int hData, int hKey, int hTable, int hSynchro, int dlen);

        [DllImport(@"mathdll.dll")]
        static private extern int gost_fhash(string fileName, int table, int resHash);

        [DllImport(@"mathdll.dll")]
        static private extern int gost_imito(int hData, int hKey, int hTable, int hSynchro, int dlen);

        #region Обертки для gost_gamma
        /// <summary>
        /// Производить шифрование/дешифрование данных, возвращает зашифрованные/расшифрованные данные и выходной параметр imito (хеш результирующих данных)
        /// </summary>
        /// <param name="data">Данные для шифрования</param>
        /// <param name="key">Ключ</param>
        /// <param name="table">Таблица</param>
        /// <param name="synchro">Синхро посылка</param>
        /// <param name="imito">Выходной параметр - хэш зашифрованных/расшифрованных данных</param>
        /// <returns>Возврает зашифрованный/расшифрованный массив байт</returns>
        static public byte[] CryptData(byte[] data, byte[] key, byte[] table, byte[] synchro, out byte[] imito)
        {
            IntPtr ptrData = Marshal.AllocHGlobal(data.Length);
            IntPtr ptrKey = Marshal.AllocHGlobal(key.Length);
            IntPtr ptrTable = Marshal.AllocHGlobal(table.Length);
            IntPtr ptrSynchro = Marshal.AllocHGlobal(synchro.Length);

            Marshal.Copy(data, 0, ptrData, data.Length);
            Marshal.Copy(key, 0, ptrKey, key.Length);
            Marshal.Copy(table, 0, ptrTable, table.Length);
            Marshal.Copy(synchro, 0, ptrSynchro, synchro.Length);

            imito = new byte[synchro.Length];
            byte[] cryptData = new byte[data.Length];

            int res = gost_gamma(ptrData.ToInt32(), ptrKey.ToInt32(), ptrTable.ToInt32(), ptrSynchro.ToInt32(), data.Length);
            Marshal.Copy(ptrData, cryptData, 0, cryptData.Length);
            
            res = gost_imito(ptrData.ToInt32(), ptrKey.ToInt32(), ptrTable.ToInt32(), ptrSynchro.ToInt32(), data.Length);
            Marshal.Copy(ptrSynchro, imito, 0, imito.Length);

            Marshal.FreeHGlobal(ptrData);
            Marshal.FreeHGlobal(ptrKey);
            Marshal.FreeHGlobal(ptrTable);
            Marshal.FreeHGlobal(ptrSynchro);

            return cryptData;

        }

        static public int CryptData(byte[] key, byte[] table, byte[] synchro, ref byte[] data, out byte[] imito)
        {
            IntPtr ptrData = Marshal.AllocHGlobal(data.Length);
            IntPtr ptrKey = Marshal.AllocHGlobal(key.Length);
            IntPtr ptrTable = Marshal.AllocHGlobal(table.Length);
            IntPtr ptrSynchro = Marshal.AllocHGlobal(synchro.Length);

            Marshal.Copy(data, 0, ptrData, data.Length);
            Marshal.Copy(key, 0, ptrKey, key.Length);
            Marshal.Copy(table, 0, ptrTable, table.Length);
            Marshal.Copy(synchro, 0, ptrSynchro, synchro.Length);

            byte[] cdata = new byte[data.Length];
            Array.Copy(data, cdata, data.Length);

            int res = gost_gamma(ptrData.ToInt32(), ptrKey.ToInt32(), ptrTable.ToInt32(), ptrSynchro.ToInt32(), data.Length);
            Marshal.Copy(ptrData, data, 0, data.Length);
            
            imito = new byte[synchro.Length];
            res |= gost_imito(ptrData.ToInt32(), ptrKey.ToInt32(), ptrTable.ToInt32(), ptrSynchro.ToInt32(), data.Length);
            Marshal.Copy(ptrSynchro, imito, 0, imito.Length);

            Marshal.FreeHGlobal(ptrData);
            Marshal.FreeHGlobal(ptrKey);
            Marshal.FreeHGlobal(ptrTable);
            Marshal.FreeHGlobal(ptrSynchro);

            return res;
        }

        /// <summary>
        /// Зашифровать/Расшифровать данные
        /// </summary>
        /// <param name="data">Входные данные, которые необходимо зашифровать/расшифровать</param>
        /// <param name="key">Ключ</param>
        /// <param name="table">Таблица</param>
        /// <param name="synchro">Синхро посылка</param>
        /// <returns>Возврает зашифрованный/расшифрованный массив байт</returns>
        static public byte[] GostGamma(byte[] data, byte[] key, byte[] table, byte[] synchro)
        {
            IntPtr ptrData = Marshal.AllocHGlobal(data.Length);
            IntPtr ptrKey = Marshal.AllocHGlobal(key.Length);
            IntPtr ptrTable = Marshal.AllocHGlobal(table.Length);
            IntPtr ptrSynchro = Marshal.AllocHGlobal(synchro.Length);

            Marshal.Copy(data, 0, ptrData, data.Length);
            Marshal.Copy(key, 0, ptrKey, key.Length);
            Marshal.Copy(table, 0, ptrTable, table.Length);
            Marshal.Copy(synchro, 0, ptrSynchro, synchro.Length);

            byte[] cryptData = new byte[data.Length];
            int res = gost_gamma(ptrData.ToInt32(), ptrKey.ToInt32(), ptrTable.ToInt32(), ptrSynchro.ToInt32(), data.Length);
            Marshal.Copy(ptrData, cryptData, 0, cryptData.Length);

            Marshal.FreeHGlobal(ptrData);
            Marshal.FreeHGlobal(ptrKey);
            Marshal.FreeHGlobal(ptrTable);
            Marshal.FreeHGlobal(ptrSynchro);

            return cryptData;
            
        }

        /// <summary>
        /// Зашифровать/Расшифровать данные
        /// </summary>
        /// <param name="data">Входные данные, которые необходимо зашифровать/расшифровать</param>
        /// <param name="key">Ключ</param>
        /// <param name="table">Таблица</param>
        /// <param name="synchro">Синхро посылка</param>
        /// <param name="encoding"></param>
        /// <returns>Возврает зашифрованную/расшифрованную строку</returns>
        static public string GostGamma(string data, byte[] key, byte[] table, byte[] synchro, Encoding encoding)
        {
            byte[] bData = encoding.GetBytes(data);
            byte[] resbData = GostGamma(bData, key, table, synchro);
            return encoding.GetString(resbData);
        }

        /// <summary>
        /// Зашифровать/Расшифровать данные
        /// </summary>
        /// <param name="data">Входные данные, которые необходимо зашифровать/расшифровать</param>
        /// <param name="key">Ключ</param>
        /// <param name="table">Таблица</param>
        /// <param name="synchro">Синхро посылка</param>
        /// <returns>Возврает зашифрованную/расшифрованную строку</returns>
        static public string GostGamma(string data, byte[] key, byte[] table, byte[] synchro)
        {
            return GostGamma(data, key, table, synchro, Encoding.GetEncoding(1251));
        }

        /// <summary>
        /// Зашифровать/Расшифровать данные
        /// </summary>
        /// <param name="data">Входные данные, которые необходимо зашифровать/расшифровать</param>
        /// <param name="key">Ключ</param>
        /// <param name="table">Таблица</param>
        /// <param name="synchro">Синхро посылка</param>
        /// <param name="encoding"></param>
        /// <returns>Возврает зашифрованную/расшифрованную строку</returns>
        static public string GostGamma(string data, string key, string table, string synchro, Encoding encoding)
        {
            int i;
            char[] buf = { '0', '0' };
            byte[] bKey, bTable, bSynchro;

            bKey = new byte[key.Length/2];
            for (i = 0; i < key.Length; i += 2)
            {
                buf[0] = key[i];
                buf[1] = key[i + 1];
                int tmp = int.Parse(new string(buf), System.Globalization.NumberStyles.HexNumber);
                bKey[i / 2] = Convert.ToByte(tmp);
            }

            bTable = new byte[table.Length / 2];
            for (i = 0; i < table.Length; i += 2)
            {
                buf[0] = table[i];
                buf[1] = table[i + 1];
                int tmp = int.Parse(new string(buf), System.Globalization.NumberStyles.HexNumber);
                bTable[i / 2] = Convert.ToByte(tmp);
            }

            bSynchro = new byte[synchro.Length / 2];
            for (i = 0; i < synchro.Length; i += 2)
            {
                buf[0] = synchro[i];
                buf[1] = synchro[i + 1];
                int tmp = int.Parse(new string(buf), System.Globalization.NumberStyles.HexNumber);
                bSynchro[i / 2] = Convert.ToByte(tmp);
            }

            return GostGamma(data, bKey, bTable, bSynchro, encoding);
        }

        /// <summary>
        /// Зашифровать/Расшифровать данные
        /// </summary>
        /// <param name="data">Входные данные, которые необходимо зашифровать/расшифровать</param>
        /// <param name="key">Ключ</param>
        /// <param name="table">Таблица</param>
        /// <param name="synchro">Синхро посылка</param>
        /// <returns>Возврает зашифрованную/расшифрованную строку</returns>
        static public string GostGamma(string data, string key, string table, string synchro)
        {
            return GostGamma(data, key, table, synchro, Encoding.GetEncoding(1251));
        }
        #endregion

        #region Обертки для gost_hash
        static public byte[] GostHash(string filename, byte[] table)
        {
            byte[] hash = new byte[32];
            byte[] bFilename = Encoding.ASCII.GetBytes(filename);

            IntPtr ptrFilename = Marshal.AllocHGlobal(bFilename.Length);
            IntPtr ptrTable = Marshal.AllocHGlobal(table.Length);
            IntPtr ptrHash = Marshal.AllocHGlobal(hash.Length);

            Marshal.Copy(bFilename, 0, ptrFilename, bFilename.Length);
            Marshal.Copy(table, 0, ptrTable, table.Length);
            Marshal.Copy(hash, 0, ptrHash, hash.Length);

            int res = gost_fhash(filename, ptrTable.ToInt32(), ptrHash.ToInt32());
            if (res != -1)
            {
                Marshal.Copy(ptrHash, hash, 0, hash.Length);

                Marshal.FreeHGlobal(ptrHash);
                Marshal.FreeHGlobal(ptrTable);
                Marshal.FreeHGlobal(ptrFilename);
                return hash;
            }
            return null;
        }
        #endregion

        #region Обертка для gost_imito
        static public byte[] GostImito(byte[] data, byte[] key, byte[] table, byte[] synchro)
        {
            IntPtr ptrData = Marshal.AllocHGlobal(data.Length);
            IntPtr ptrKey = Marshal.AllocHGlobal(key.Length);
            IntPtr ptrTable = Marshal.AllocHGlobal(table.Length);
            IntPtr ptrSynchro = Marshal.AllocHGlobal(synchro.Length);

            Marshal.Copy(data, 0, ptrData, data.Length);
            Marshal.Copy(key, 0, ptrKey, key.Length);
            Marshal.Copy(table, 0, ptrTable, table.Length);
            Marshal.Copy(synchro, 0, ptrSynchro, synchro.Length);

            int res = gost_imito(ptrData.ToInt32(), ptrKey.ToInt32(), ptrTable.ToInt32(), ptrSynchro.ToInt32(), data.Length);
            
            byte[] resImito = new byte[synchro.Length];
            Marshal.Copy(ptrSynchro, resImito, 0, resImito.Length);

            Marshal.FreeHGlobal(ptrData);
            Marshal.FreeHGlobal(ptrKey);
            Marshal.FreeHGlobal(ptrTable);
            Marshal.FreeHGlobal(ptrSynchro);

            return resImito;

        }
        #endregion
    }
}
