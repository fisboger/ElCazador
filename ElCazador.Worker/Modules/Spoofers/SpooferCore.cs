using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Modules;
using ElCazador.Worker.Modules.Spoofers.Models;

namespace ElCazador.Worker.Modules.Spoofers
{
    internal class SpooferCore : AbstractModule, IModule
    {
        private SpooferSettings Settings { get; set; }


        #region Sockets
        private Models.SocketType UDP137Socket = new Models.SocketType
        {
            Name = Sockets.UDP137,
            ProtocolType = ProtocolType.Udp,
            Port = 137
        };
        private Models.SocketType UDP5355Socket = new Models.SocketType
        {
            Name = Sockets.UDP5355,
            ProtocolType = ProtocolType.Udp,
            Port = 5355,
            MulticastAddress = IPAddress.Parse("224.0.0.252")
        };
        #endregion
        // TODO: This is dirty, maybe find a better way
        private IDictionary<Models.SocketType, List<ISpoofer>> Spoofers { get; set; } = new Dictionary<Models.SocketType, List<ISpoofer>>();
        private IList<Models.SocketType> SocketTypes { get; set; } = new List<Models.SocketType>();

        public bool IsEnabled => Settings.NBNS || Settings.LLMNR;

        internal SpooferCore(IWorkerController controller, SpooferSettings settings) : base(controller, "SpooferCore")
        {
            Settings = settings;

            AddSpoofer(UDP137Socket, new NBNSSpoofer(Controller, Settings, UDP137Socket), Settings.NBNS);
            AddSpoofer(UDP5355Socket, new LLMNRSpoofer(Controller, Settings, UDP5355Socket), Settings.LLMNR);
        }

        public async Task Run()
        {
            await StartSockets();
        }

        private void AddSpoofer(Models.SocketType socketType, ISpoofer spoofer, bool run)
        {
            if (!run)
            {
                return;
            }

            if (!Spoofers.ContainsKey(socketType))
            {
                SocketTypes.Add(socketType);
                Spoofers.Add(socketType, new List<ISpoofer>());
            }

            var socketRef = Spoofers[socketType];

            socketRef.Add(spoofer);
        }

        private async Task StartSocket(Models.SocketType socketType)
        {
            if (!Spoofers.ContainsKey(socketType))
            {
                return;
            }

            // Get all local ip addresses
            socketType.IPEndPoint = new IPEndPoint(IPAddress.Any, socketType.Port);

            var result = new Socket(
                AddressFamily.InterNetwork,
                System.Net.Sockets.SocketType.Dgram,
                ProtocolType.Udp);

            // Bind the socket to start receiving
            result.Bind(socketType.IPEndPoint);

            if (socketType.MulticastAddress != null)
            {
                var multicastOption = new MulticastOption(socketType.MulticastAddress, IPAddress.Any);

                result.SetSocketOption(
                    SocketOptionLevel.IP,
                    SocketOptionName.AddMembership,
                    multicastOption);
            }

            socketType.Socket = result;

            await Task.CompletedTask;
        }

        private async Task StartSockets()
        {
            foreach (var socketType in SocketTypes)
            {
                await StartSocket(socketType);
            }

            await Controller.Log(Name, "Spoofer module started");

            while (true)
            {
                await BeginReceiveSockets();

                Thread.Sleep(100);
            }
        }

        private async Task BeginReceiveSockets()
        {
            foreach (var socketType in SocketTypes.Where(s => s.ProtocolType == ProtocolType.Udp))
            {
                var state = new SpooferPacket();

                socketType.Socket.BeginReceiveFrom(
                    state.Buffer,
                    0,
                    SpooferPacket.BUFFER_SIZE,
                    SocketFlags.None,
                    ref socketType.IPEndPoint,
                    new AsyncCallback(async (ar) => await ReadCallback(ar, socketType)),
                    state);
            }

            await Task.CompletedTask;
        }

        public async Task ReadCallback(IAsyncResult result, Models.SocketType socketType)
        {
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            SpooferPacket state = (SpooferPacket)result.AsyncState;
            Socket socket = socketType.Socket;

            // Read data from the client socket.   
            int length = socket.EndReceiveFrom(result, ref socketType.IPEndPoint);

            if (length > 0)
            {

                foreach (var handler in Spoofers[socketType])
                {
                    await Task.Run(() => handler.HandlePacket(state));
                }
            }
            else
            {
                await Controller.Log(Name, "Couldn't read packet({0}). Dumping Hex {1}{2}",
                    length,
                    Environment.NewLine,
                    ElCazador.Worker.Utils.Hex.Dump(state.Buffer));
            }

            // Fetch new packets
            state = new SpooferPacket();

            socket.BeginReceiveFrom(
                state.Buffer,
                0,
                SpooferPacket.BUFFER_SIZE,
                SocketFlags.None,
                ref socketType.IPEndPoint,
                new AsyncCallback(async (ar) => await ReadCallback(ar, socketType)),
                state);
        }
    }
}