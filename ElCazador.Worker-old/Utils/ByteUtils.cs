using System;
using System.Collections.Generic;
using System.Linq;

namespace ElCazador.Worker.Utils
{
    public static class ByteUtils
    {
        public static byte[] Combine(IEnumerable<byte[]> data)
        {
            var result = new List<byte>();

            foreach (var array in data)
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