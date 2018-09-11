using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules.Spoofers.Models;

namespace ElCazador.Worker.Modules.Spoofers
{
    internal class NBNSSpoofer : BaseSpoofer
    {

        protected override string Protocol => "NBNS";

        public NBNSSpoofer(SpooferSettings settings, Models.SocketType socketType)
            : base(settings, socketType)
        {
        }
        protected override IPacket GetPacket(byte[] data, byte[] ip)
        {
            return new NBNSPacket(data, ip);
        }

        protected override bool CheckRules(SpooferPacket state)
        {
            var result = state.Buffer[2] == 0x01 && state.Buffer[3] == 0x10;
            return result;
        }

        protected override string GetName(SpooferPacket state)
        {
            return DecodeName(state.Buffer.Skip(13).Take(32).ToArray());
        }

        private static string DecodeName(byte[] bytes)
        {
            var chars = new List<char>(bytes.Length / 2);

            for (int i = 0; i < bytes.Length; i += 2)
            {
                chars.Add((char)(((bytes[i] - 0x41) << 4) | ((bytes[i + 1] - 0x41) & 0xf)));
            }

            return new string(chars.Where(c => !Char.IsControl(c)).ToArray()).TrimEnd();
        }
    }
}