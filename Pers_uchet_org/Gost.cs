using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pers_uchet_org
{
    class Gost28147_89
    {
        private const Int32 C1 = 0x1010104;
        private const Int32 C2 = 0x1010101;

        static void Elem_gost(ref UInt32 N1, ref UInt32 N2, UInt32 key, byte[] gostSboxExt)
        {
            UInt32 R1, R2, R3, R4, eax;
            Int64 temp = N1 + key;
            eax = (uint)(temp % 0x100000000);
            R4 = gostSboxExt[(eax & 0xFF)];
            R3 = gostSboxExt[((eax & 0xFF00) >> 8) + 256];
            R2 = gostSboxExt[((eax & 0xFF0000) >> 16) + 512];
            R1 = gostSboxExt[((eax & 0xFF000000) >> 24) + 768];
            eax = R4 | (R3 << 8) | (R2 << 16) | (R1 << 24);
            eax = (eax << 11) | (eax >> 21);
            eax ^= N2;
            N2 = N1;
            N1 = eax;
        }

        public static void GostSimple(ref byte[] uidArr, byte[] gostKeyArr, byte[] gostSBoxExtArr, int circles, bool isEncrypt)
        {
            int uidArrLen = uidArr.Length;
            UInt32 N1 = 0, N2 = 0;
            UInt32 k1, k2, k3, k4;
            UInt32[] keyArr = new UInt32[32];//возможно 31
            //Подготовка ключа
            for (int i = 0; i <= 7; i++)
            {
                k1 = (uint)gostKeyArr[i * 4];
                k2 = (uint)gostKeyArr[i * 4 + 1];
                k3 = (uint)gostKeyArr[i * 4 + 2];
                k4 = (uint)gostKeyArr[i * 4 + 3];
                keyArr[i] = k4 | (k3 << 8) | (k2 << 16) | (k1 << 24);
                keyArr[i + 8] = keyArr[i];
                keyArr[i + 16] = keyArr[i];
                keyArr[31 - i] = keyArr[i];
            }
            //Обработка массива байт порциями по 8 байт
            for (int i = 0; i < uidArrLen; i += 8)
            {
                //Левая часть беззнакового целого
                for (int j = 0; j <= 3; j++)
                    N1 = (N1 << 8) + uidArr[i + j];
                //Правая часть беззнакового целого
                for (int j = 4; j <= 7; j++)
                    N2 = (N2 << 8) + uidArr[i + j];

                if (isEncrypt)
                {
                    for (int j = 0; j < circles; j++)
                        Elem_gost(ref N1, ref  N2, keyArr[j], gostSBoxExtArr);
                }
                else
                {
                    for (int j = circles - 1; j >= 0; j--)
                        Elem_gost(ref N1, ref  N2, keyArr[j], gostSBoxExtArr);
                }

                //Правую часть к строке
                uidArr[i] = (byte)((N2 & 0xFF000000) >> 24);
                uidArr[i + 1] = (byte)((N2 & 0xFF0000) >> 16);
                uidArr[i + 2] = (byte)((N2 & 0xFF00) >> 8);
                uidArr[i + 3] = (byte)((N2 & 0xFF));
                //Левую часть к строке
                uidArr[i + 4] = (byte)((N1 & 0xFF000000) >> 24);
                uidArr[i + 5] = (byte)((N1 & 0xFF0000) >> 16);
                uidArr[i + 6] = (byte)((N1 & 0xFF00) >> 8);
                uidArr[i + 7] = (byte)((N1 & 0xFF));
            }
        }

        public static void GostGama(ref byte[] uidArr, ref byte[] synhroArr, byte[] gostKeyArr, byte[] gostSBoxExtArr)
        {
            int uidArrLen, newUidArrLen;
            uidArrLen = uidArr.Length;
            if (uidArrLen % 8 > 0)
            {
                newUidArrLen = uidArrLen + (8 - uidArrLen % 8);
                Array.Resize<byte>(ref uidArr, newUidArrLen);
            }
            UInt32 N1 = 0, N2 = 0, N3 = 0, N4 = 0, T1 = 0, T2 = 0;
            //Шифруем синхро
            GostSimple(ref synhroArr, gostKeyArr, gostSBoxExtArr, 32, true);

            //Подготовка синхро
            //Левая часть синхро как беззнаковое целое 
            for (int i = 0; i <= 3; i++)
            {
                N1 = (N1 << 8) + synhroArr[i];
            }
            //Правая часть синхро как беззнаковое целое 
            for (int i = 4; i <= 7; i++)
            {
                N2 = (N2 << 8) + synhroArr[i];
            }

            N3 = N1;
            N4 = N2;
            UInt64 temp = 0;
            //Обработка массива байт порциями по 8 байт
            for (int i = 0; i < uidArrLen; i += 8)
            {
                temp = N3;
                temp += C2;
                N3 = (uint)(temp % 0x100000000);
                temp = N4;
                temp += C1;
                N4 = (uint)(temp % (0x100000000 - 1));

                N1 = N3;
                N2 = N4;

                //Правую часть к строке
                synhroArr[0] = (byte)((N1 & 0xFF000000) >> 24);
                synhroArr[1] = (byte)((N1 & 0xFF0000) >> 16);
                synhroArr[2] = (byte)((N1 & 0xFF00) >> 8);
                synhroArr[3] = (byte)((N1 & 0xFF));
                //Левую часть к строке
                synhroArr[4] = (byte)((N2 & 0xFF000000) >> 24);
                synhroArr[5] = (byte)((N2 & 0xFF0000) >> 16);
                synhroArr[6] = (byte)((N2 & 0xFF00) >> 8);
                synhroArr[7] = (byte)((N2 & 0xFF));

                //Шифруем синхро
                GostSimple(ref synhroArr, gostKeyArr, gostSBoxExtArr, 32, true);

                //Подготовка синхро
                //Левая часть синхро как беззнаковое целое 
                for (int j = 0; j <= 3; j++)
                {
                    N1 = (N1 << 8) + synhroArr[j];
                }
                //Правая часть синхро как беззнаковое целое 
                for (int j = 4; j <= 7; j++)
                {
                    N2 = (N2 << 8) + synhroArr[j];
                }
                //Левая часть данных как беззнаковое целое
                for (int j = 3; j >= 0; j--)
                {
                    T1 = (T1 << 8) + uidArr[i + j];
                }
                //установка гаммы
                T1 ^= N1;
                //Правая часть данных как беззнаковое целое
                for (int j = 7; j >= 4; j--)
                {
                    T2 = (T2 << 8) + uidArr[i + j];
                }
                //установка гаммы
                T2 ^= N2;

                //Правую часть к строке
                uidArr[i] = (byte)((T1 & 0xFF));
                uidArr[i + 1] = (byte)((T1 & 0xFF00) >> 8);
                uidArr[i + 2] = (byte)((T1 & 0xFF0000) >> 16);
                uidArr[i + 3] = (byte)((T1 & 0xFF000000) >> 24);
                //Левую часть к строке
                uidArr[i + 4] = (byte)((T2 & 0xFF));
                uidArr[i + 5] = (byte)((T2 & 0xFF00) >> 8);
                uidArr[i + 6] = (byte)((T2 & 0xFF0000) >> 16);
                uidArr[i + 7] = (byte)((T2 & 0xFF000000) >> 24);

            }
            Array.Resize<byte>(ref uidArr, uidArrLen);
        }

        public static void GostImito(ref byte[] uidArr, ref byte[] synhroArr, byte[] gostKeyArr, byte[] gostSBoxExtArr)
        {
            int uidArrLen, newUidArrLen;
            uidArrLen = uidArr.Length;
            if (uidArrLen % 8 > 0)
            {
                newUidArrLen = uidArrLen + (8 - uidArrLen % 8);
                Array.Resize<byte>(ref uidArr, newUidArrLen);
            }
            UInt32 N1 = 0, N2 = 0, T1 = 0, T2 = 0;
            //Подготовка N1 и N2
            //Левая часть как беззнаковое целое 
            for (int i = 0; i <= 3; i++)
            {
                N1 = (N1 << 8) + uidArr[i];
            }
            //Правая часть как беззнаковое целое 
            for (int i = 4; i <= 7; i++)
            {
                N2 = (N2 << 8) + uidArr[i];
            }

            if (uidArrLen > 8)
            {
                //Обработка массива байт порциями по 8 байт
                for (int i = 8; i < uidArrLen; i += 8)
                {
                    GostSimple(ref uidArr, gostKeyArr, gostSBoxExtArr, 16, true);
                    //Левая часть следующего блока данных как беззнаковое целое
                    for (int j = 3; j >= 0; j--)
                    {
                        T1 = (T1 << 8) + uidArr[i + j];
                    }
                    N1 ^= T1;
                    //Правая часть следующего блока данных как беззнаковое целое
                    for (int j = 7; j >= 4; j--)
                    {
                        T2 = (T2 << 8) + uidArr[i + j];
                    }
                    N2 ^= T2;
                }
            }
            else
            {
                GostSimple(ref uidArr, gostKeyArr, gostSBoxExtArr, 16, true);
            }

            //Правую часть к строке
            synhroArr[0] = (byte)((N1 & 0xFF));
            synhroArr[1] = (byte)((N1 & 0xFF00) >> 8);
            synhroArr[2] = (byte)((N1 & 0xFF0000) >> 16);
            synhroArr[3] = (byte)((N1 & 0xFF000000) >> 24);
            //Левую часть к строке
            synhroArr[4] = (byte)((N2 & 0xFF));
            synhroArr[5] = (byte)((N2 & 0xFF00) >> 8);
            synhroArr[6] = (byte)((N2 & 0xFF0000) >> 16);
            synhroArr[7] = (byte)((N2 & 0xFF000000) >> 24);
        }

        public static void GostGamaAndBack(ref byte[] uidArr, ref byte[] synhroArr, byte[] gostKeyArr, byte[] gostSBoxExtArr, bool isEncrypt)
        {
            int uidArrLen, newUidArrLen;
            uidArrLen = uidArr.Length;
            if (uidArrLen % 8 > 0)
            {
                newUidArrLen = uidArrLen + (8 - uidArrLen % 8);
                Array.Resize<byte>(ref uidArr, newUidArrLen);
            }
            UInt32 N1 = 0, N2 = 0, T1 = 0, T2 = 0;
            //Обработка массива байт порциями по 8 байт
            for (int i = 0; i < uidArrLen; i += 8)
            {
                //Шифруем синхро
                GostSimple(ref synhroArr, gostKeyArr, gostSBoxExtArr, 32, true);

                //Подготовка синхро
                //Левая часть синхро как беззнаковое целое 
                for (int j = 0; j <= 3; j++)
                {
                    N1 = (N1 << 8) + synhroArr[j];
                }
                //Правая часть синхро как беззнаковое целое 
                for (int j = 4; j <= 7; j++)
                {
                    N2 = (N2 << 8) + synhroArr[j];
                }
                //Левая часть блока данных как беззнаковое целое
                for (int j = 3; j >= 0; j--)
                {
                    T1 = (T1 << 8) + uidArr[i + j];
                }
                //установка гаммы
                T1 ^= N1;
                //Правая часть блока данных как беззнаковое целое
                for (int j = 7; j >= 4; j--)
                {
                    T2 = (T2 << 8) + uidArr[i + j];
                }
                //установка гаммы
                T2 ^= N2;

                if (isEncrypt)
                {
                    //Правую часть к строке
                    synhroArr[0] = (byte)((T1 & 0xFF000000) >> 24);
                    synhroArr[1] = (byte)((T1 & 0xFF0000) >> 16);
                    synhroArr[2] = (byte)((T1 & 0xFF00) >> 8);
                    synhroArr[3] = (byte)((T1 & 0xFF));
                    //Левую часть к строке
                    synhroArr[4] = (byte)((T2 & 0xFF000000) >> 24);
                    synhroArr[5] = (byte)((T2 & 0xFF0000) >> 16);
                    synhroArr[6] = (byte)((T2 & 0xFF00) >> 8);
                    synhroArr[7] = (byte)((T2 & 0xFF));
                }
                else
                {
                    //Правую часть к строке
                    synhroArr[0] = uidArr[i];
                    synhroArr[1] = uidArr[i + 1];
                    synhroArr[2] = uidArr[i + 2];
                    synhroArr[3] = uidArr[i + 3];
                    //Левую часть к строке
                    synhroArr[4] = uidArr[i + 4];
                    synhroArr[5] = uidArr[i + 5];
                    synhroArr[6] = uidArr[i + 6];
                    synhroArr[7] = uidArr[i + 7];
                }

                //Правую часть к строке
                uidArr[i] = (byte)((T1 & 0xFF));
                uidArr[i + 1] = (byte)((T1 & 0xFF00) >> 8);
                uidArr[i + 2] = (byte)((T1 & 0xFF0000) >> 16);
                uidArr[i + 3] = (byte)((T1 & 0xFF000000) >> 24);
                //Левую часть к строке
                uidArr[i + 4] = (byte)((T2 & 0xFF));
                uidArr[i + 5] = (byte)((T2 & 0xFF00) >> 8);
                uidArr[i + 6] = (byte)((T2 & 0xFF0000) >> 16);
                uidArr[i + 7] = (byte)((T2 & 0xFF000000) >> 24);

            }
            Array.Resize<byte>(ref uidArr, uidArrLen);
        }

        public static string GenerateSynhro()
        {
            return "";
        }
    }
}
