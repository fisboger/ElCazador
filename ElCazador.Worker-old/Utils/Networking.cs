using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ElCazador.Worker.Utils
{
    public class Networking
    {
        private static byte[][] IPv4EndingBytes = new byte[][]
        {
            new byte[] { 0x00, 0x01, 0x00, 0x01 },
            new byte[] { 0x00, 0xff, 0x00, 0xff }
        };

        public static IPAddress GetLocalIPAddress()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;

                return endPoint.Address;
            }
        }

        public static bool IsIPv4(byte[] buffer)
        {
            var ending = buffer.TakeLast(4);

            foreach (var bytes in IPv4EndingBytes)
            {
                if (ending.SequenceEqual(bytes))
                {
                    return true;
                }
            }

            return false;
        }
    }
}