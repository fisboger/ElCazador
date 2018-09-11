using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules.Spoofers.Models;
using ElCazador.Worker.Utils;

namespace ElCazador.Worker.Modules.Spoofers
{
    internal class LLMNRSpoofer : BaseSpoofer
    {
        private const int StaticPacketLength = 18;

        protected override string Protocol { get => "LLMNR"; }

        public LLMNRSpoofer(SpooferSettings settings, Models.SocketType socketType)
            : base(settings, socketType)
        {
        }

        protected override IPacket GetPacket(byte[] data, byte[] ip)
        {
            return new LLMNRPacket(data, ip);
        }

        protected override bool CheckRules(SpooferPacket state)
        {
            var nameLen = (int)state.Buffer[12];

            state.Buffer = state.Buffer.Take(StaticPacketLength + nameLen).ToArray();

            return Networking.IsIPv4(state.Buffer);
        }

        protected override string GetName(SpooferPacket state)
        {
            var nameLen = (int)state.Buffer[12];

            return Encoding.Default.GetString(state.Buffer.Skip(13).Take(nameLen).ToArray());
        }
    }
}