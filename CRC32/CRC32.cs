// Authored by Daniil Mirensky
// <DanyaSWorlD>
// 13.12.2018

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Daquga.Security.Cryptography
{
    /// <summary>
    /// CRC-32-IEEE 802.3 reversed polinom
    /// </summary>
    public static class CRC32
    {
        private static uint[] crc_table;

        /// <summary>
        /// Считает хеш файла
        /// </summary>
        /// <returns>Хеш файла</returns>
        public static string FromFile(string file)
        {
            BuildTable();

            using (var stream = File.OpenRead(file))
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return Crc32(buffer).ToString("X4");
            }
        }

        /// <summary>
        /// Считает хеш строки
        /// </summary>
        /// <returns>Хеш строки</returns>
        public static string FromString(string s)
        {
            BuildTable();
            return Crc32(Encoding.Default.GetBytes(s)).ToString("X4");
        }

        /// <summary>
        /// Ститает хеш потоко байт
        /// </summary>
        /// <returns>Хеш потока</returns>
        public static string FromStream(Stream stream)
        {
            BuildTable();

            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return Crc32(buffer).ToString("X4");
        }

        private static void BuildTable()
        {
            if (crc_table != null) return;

            const uint poly = 0xEDB88320;
            crc_table = new uint[256];
            for (uint i = 0; i < crc_table.Length; ++i)
            {
                var temp = i;

                for (var j = 8; j > 0; --j)
                    temp = (temp & 1) == 1 ? (temp >> 1) ^ poly : temp >> 1;

                crc_table[i] = temp;
            }
        }

        private static uint Crc32(IEnumerable<byte> array)
        {
            var result = 0xFFFFFFFF;

            foreach (var b in array)
            {
                var last_byte = (byte)(result & 0xFF);
                result >>= 8;
                result = result ^ crc_table[last_byte ^ b];
            }

            return result ^ 0xFFFFFFFF;
        }
    }
}