using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules.Spoofers.Models;

namespace ElCazador.Worker.Modules.Spoofers
{
    internal abstract class BaseSpoofer : ISpoofer
    {

        protected SpooferSettings Settings { get; set; }
        protected Models.SocketType SocketType { get; set; }
        protected IWorkerController Controller { get; private set; }

        protected abstract string Protocol { get; }

        internal BaseSpoofer(IWorkerController controller, SpooferSettings settings, Models.SocketType socketType)
        {
            Settings = settings;
            SocketType = socketType;
            Controller = controller;
        }

        protected abstract bool CheckRules(SpooferPacket state);
        protected abstract string GetName(SpooferPacket state);
        protected abstract IPacket GetPacket(byte[] data, byte[] ip);

        public async Task HandlePacket(SpooferPacket state)
        {
            var ip = ((IPEndPoint)SocketType.IPEndPoint).Address;

            if (Controller.WorkerSettings.IP.Equals(ip) || !CheckRules(state))
            {
                return;
            }

            await HandleRequestPacket(state);

            if (!Settings.Inspect)
            {
                await SpoofPacket(state);
            }
        }

        public async Task HandleRequestPacket(SpooferPacket state)
        {   
            var name = GetName(state);
            
            // We have to write everything as once as this is threaded
            await Controller.Log(Protocol, "Received {0} request for {1} from {2}{3}",
                 Protocol,
                 name,
                 ((IPEndPoint)SocketType.IPEndPoint).Address.ToString(),
                 (Settings.Inspect) ? "" : " > Sending response");
        }

        public async Task SpoofPacket(SpooferPacket state)
        {
            var data = state.Buffer;
            var ip = Controller.WorkerSettings.IP.GetAddressBytes();

            var packet = GetPacket(data, ip).Build();

            try
            {
                SocketAsyncEventArgs e = new SocketAsyncEventArgs();
                e.RemoteEndPoint = SocketType.IPEndPoint;
                e.SetBuffer(packet, 0, packet.Length);
                e.SocketFlags = SocketFlags.None;


                SocketType.Socket.SendToAsync(e);
            }
            catch (Exception)
            {
                await Controller.Log(Protocol, "{0} Could not send response", Environment.NewLine);
            }
        }
    }
}