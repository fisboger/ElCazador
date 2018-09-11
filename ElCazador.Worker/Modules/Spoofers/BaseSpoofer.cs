using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules.Spoofers.Models;

namespace ElCazador.Worker.Modules.Spoofers
{
    internal abstract class BaseSpoofer : ISpoofer
    {

        protected SpooferSettings Settings { get; set; }
        protected Models.SocketType SocketType { get; set; }

        protected abstract string Protocol { get; }

        internal BaseSpoofer(SpooferSettings settings, Models.SocketType socketType)
        {
            Settings = settings;
            SocketType = socketType;
        }

        protected abstract bool CheckRules(SpooferPacket state);
        protected abstract string GetName(SpooferPacket state);
        protected abstract IPacket GetPacket(byte[] data, byte[] ip);

        public async Task HandlePacket(SpooferPacket state)
        {
            var ip = ((IPEndPoint)SocketType.IPEndPoint).Address;

            if (Program.IP.Equals(ip) || !CheckRules(state))
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
            Program.Write("{0}Received {1} request for {2} from {3}{4}",
                 Environment.NewLine,
                 Protocol,
                 name,
                 ((IPEndPoint)SocketType.IPEndPoint).Address.ToString(),
                 (Settings.Inspect) ? "" : " > Sending response");
        }

        public async Task SpoofPacket(SpooferPacket state)
        {
            var data = state.Buffer;
            var ip = Program.IP.GetAddressBytes();

            var packet = GetPacket(data, ip).Build();

            try
            {
                await SocketType.Socket.SendToAsync(packet, SocketFlags.None, SocketType.IPEndPoint);
            }
            catch (Exception e)
            {
                Program.Write("{0} Could not send response", Environment.NewLine);
            }
        }
    }
}