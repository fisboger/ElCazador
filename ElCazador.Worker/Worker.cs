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
using ElCazador.Worker.Interfaces;

namespace ElCazador.Worker
{
    public class Worker
    { //Networking.GetLocalIPAddress();
                                                                                     // internal static IPAddress IP { get; set; } = IPAddress.Parse("192.168.0.2"); //Networking.GetLocalIPAddress();
                                                                                     // private static Thread SpooferWork { get; set; }
                                                                                     // private static Thread SMBServerWork { get; set; }
                                                                                     // private static Thread HTTPServerWork { get; set; }
        private static IDictionary<string, User> Hashes { get; set; } = new ConcurrentDictionary<string, User>();

        private IWorkerController Controller { get; set; }

        public Worker(IWorkerController controller)
        {
            Controller = controller;

            var spoofer = new SpooferCore(controller, new SpooferSettings { });
            var smbServer = new SMBServer(controller);
            var httpServer = new HTTPServer(controller, 80);

            Task.Run(spoofer.Run);
            Task.Run(httpServer.Run);
            Task.Run(smbServer.Run);
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
