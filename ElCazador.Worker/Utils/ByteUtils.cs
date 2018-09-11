using System;
using System.Collections.Generic;
using System.Linq;

namespace ElCazador.Worker.Utils
{
    public static class ByteUtils
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }

        public static byte[] Combine(IEnumerable<byte[]> source)
        {
            var result = new List<byte>();

            foreach (var array in source)
            {
                result = result.Concat(array).ToList();
            }

            return result.ToArray();
        }

        public static byte[] IntToLittleEndian(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        public static byte[] IntToBigEndian(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        public static byte[] IntToShortLittleEndian(int value)
        {
            return new byte[] { IntToLittleEndian(value)[0] };
        }
    }
}