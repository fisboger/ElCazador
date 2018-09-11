using System.Collections.Generic;
using System.Linq;
using ElCazador.Worker.Models;

namespace ElCazador.Worker.Modules.Servers.Models
{
    internal class SMB2AccessDenied : IPacket
    {
        private IEnumerable<byte> Header { get; set; }

        internal SMB2AccessDenied(IEnumerable<byte> header)
        {
            Header = header;
        }


        public byte[] Build()
        {
            return Header
                .Concat(Len)
                .Concat(SessionFlag)
                .Concat(SecBlobOffSet)
                .ToArray();
        }

        public IEnumerable<byte> Len { get; set; } = new byte[] { 0x09, 0x00 };
        public IEnumerable<byte> SessionFlag { get; set; } = new byte[] { 0x00, 0x00 };
        public IEnumerable<byte> SecBlobOffSet { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00 };
    }
}