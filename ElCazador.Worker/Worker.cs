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
using Microsoft.Extensions.Hosting;
using ElCazador.Worker.Modules;
using System.Diagnostics;
using ElCazador.Worker.Modules.Tools;

namespace ElCazador.Worker
{
    public class Worker : IHostedService
    { //Networking.GetLocalIPAddress();
      // internal static IPAddress IP { get; set; } = IPAddress.Parse("192.168.0.2"); //Networking.GetLocalIPAddress();
      // private static Thread SpooferWork { get; set; }
      // private static Thread SMBServerWork { get; set; }
      // private static Thread HTTPServerWork { get; set; }
        private static IDictionary<string, User> Hashes { get; set; } = new ConcurrentDictionary<string, User>();

        private IDictionary<Type, IModule> Modules { get; set; }
        private IList<Thread> ModuleThreads { get; set; }

        private IWorkerController Controller { get; set; }

        public Worker(IWorkerController controller)
        {
            Controller = controller;

            RegisterModules();
        }

        private void RegisterModules()
        {
            Modules = new Dictionary<Type, IModule>()
            {
                { typeof(SpooferCore), new SpooferCore(Controller, new SpooferSettings()) },
                { typeof(SMBServer) , new SMBServer(Controller) },
                { typeof(HTTPServer) , new HTTPServer(Controller, 80) },
                { typeof(LsassDumpTool) , new LsassDumpTool(Controller) }
            };

            ModuleThreads = new List<Thread>(Modules.Count);
        }

        public async Task RunTool(Type type, Target target, User user)
        {
            var module = Modules[type];

            var toolModule = (IToolModule)module;

            await Task.Run(() => toolModule.Run(target, user));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Controller.Log("Worker", "Starting modules");

            foreach (var module in Modules)
            {
                if (module.Value is IPersistantModule)
                {
                    var persistentModule = ((IPersistantModule)module.Value);

                    var thread = new Thread(async () => await persistentModule.Run().ConfigureAwait(false));

                    ModuleThreads.Add(thread);

                    thread.Start();

                    await Controller.Log("Worker", "Started module {0}", module.Value.Name);
                }
                else if (module.Value is IToolModule)
                {
                    Controller.SynchronizeTool(module.Key, (IToolModule) module.Value);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Controller.Log("Worker", "Stopping modules");
            foreach (var module in Modules.Values)
            {
                if (module is IPersistantModule)
                {
                    await ((IPersistantModule)module).Stop();
                    await Controller.Log("Worker", "Stopped module {0}", module.Name);
                }
            }
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
