using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElCazador.Worker.Models;

namespace ElCazador.Worker.Modules.Servers.Models
{
    internal class SMB2NegotiateResponse : IPacket
    {
        private IEnumerable<byte> Header { get; set; }

        internal SMB2NegotiateResponse(IEnumerable<byte> header)
        {
            Header = header;
        }

        internal SMB2NegotiateResponse(IEnumerable<byte> header, IEnumerable<byte> dialect)
        {
            Header = header;
            Dialect = dialect;
        }

        public byte[] Build()
        {
            return Header.Concat(Len)
                .Concat(Signing)
                .Concat(Dialect)
                .Concat(Reserved)
                .Concat(Guid)
                .Concat(Capabilities)
                .Concat(MaxTransSize)
                .Concat(MaxReadSize)
                .Concat(MaxWriteSize)
                .Concat(SystemTime)
                .Concat(BootTime)
                .Concat(SecBlobOffSet)
                .Concat(SecBlobLen)
                .Concat(Reserved2)
                .Concat(InitContextTokenASNId)
                .Concat(InitContextTokenASNLen)
                .Concat(ThisMechASNId)
                .Concat(ThisMechASNLen)
                .Concat(ThisMechASNStr)
                .Concat(SpNegoTokenASNId)
                .Concat(SpNegoTokenASNLen)
                .Concat(NegTokenASNId)
                .Concat(NegTokenASNLen)
                .Concat(NegTokenTag0ASNId)
                .Concat(NegTokenTag0ASNLen)
                .Concat(NegThisMechASNId)
                .Concat(NegThisMechASNLen)
                .Concat(NegThisMech1ASNId)
                .Concat(NegThisMech1ASNLen)
                .Concat(NegThisMech1ASNStr)
                .Concat(NegThisMech2ASNId)
                .Concat(NegThisMech2ASNLen)
                .Concat(NegThisMech2ASNStr)
                .Concat(NegThisMech3ASNId)
                .Concat(NegThisMech3ASNLen)
                .Concat(NegThisMech3ASNStr)
                .Concat(NegThisMech4ASNId)
                .Concat(NegThisMech4ASNLen)
                .Concat(NegThisMech4ASNStr)
                .Concat(NegThisMech5ASNId)
                .Concat(NegThisMech5ASNLen)
                .Concat(NegThisMech5ASNStr)
                .Concat(NegTokenTag3ASNId)
                .Concat(NegTokenTag3ASNLen)
                .Concat(NegHintASNId)
                .Concat(NegHintASNLen)
                .Concat(NegHintTag0ASNId)
                .Concat(NegHintTag0ASNLen)
                .Concat(NegHintFinalASNId)
                .Concat(NegHintFinalASNLen)
                .Concat(NegHintFinalASNStr).ToArray();
        }

        public IEnumerable<byte> Len { get; set; } = new byte[] {0x41, 0x00 };
        public IEnumerable<byte> Signing { get; set; } = new byte[] {0x01, 0x00 };
        public IEnumerable<byte> Dialect { get; set; } = new byte[] {0xff, 0x02 };
        public IEnumerable<byte> Reserved { get; set; } = new byte[] {0x00, 0x00 };
        public IEnumerable<byte> Guid { get; set; } = new byte[] {0xee, 0x85, 0xab, 0xf7, 0xea, 0xf6, 0x0c, 0x4f, 0x92, 0x81, 0x92, 0x47, 0x6d, 0xeb, 0x76, 0xa9 };
        public IEnumerable<byte> Capabilities { get; set; } = new byte[] {0x07, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> MaxTransSize { get; set; } = new byte[] {0x00, 0x00, 0x10, 0x00 };
        public IEnumerable<byte> MaxReadSize { get; set; } = new byte[] {0x00, 0x00, 0x10, 0x00 };
        public IEnumerable<byte> MaxWriteSize { get; set; } = new byte[] {0x00, 0x00, 0x10, 0x00 };
        public IEnumerable<byte> SystemTime { get; set; } = new byte[] {0x27, 0xfb, 0xea, 0xd7, 0x50, 0x09, 0xd2, 0x01 };
        public IEnumerable<byte> BootTime { get; set; } = new byte[] {0x22, 0xfb, 0x80, 0x01, 0x40, 0x09, 0xd2, 0x01 };
        public IEnumerable<byte> SecBlobOffSet { get; set; } = new byte[] {0x80, 0x00 };
        public IEnumerable<byte> SecBlobLen { get; set; } = new byte[] {0x69, 0x00 };
        public IEnumerable<byte> Reserved2 { get; set; } = new byte[] {0x00, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> InitContextTokenASNId { get; set; } = new byte[] {0x60 };
        public IEnumerable<byte> InitContextTokenASNLen { get; set; } = new byte[] {0x67 };
        public IEnumerable<byte> ThisMechASNId { get; set; } = new byte[] {0x06 };
        public IEnumerable<byte> ThisMechASNLen { get; set; } = new byte[] {0x06 };
        public IEnumerable<byte> ThisMechASNStr { get; set; } = new byte[] {0x2b, 0x06, 0x01, 0x05, 0x05, 0x02 };
        public IEnumerable<byte> SpNegoTokenASNId { get; set; } = new byte[] {0xa0 };
        public IEnumerable<byte> SpNegoTokenASNLen { get; set; } = new byte[] {0x5d };
        public IEnumerable<byte> NegTokenASNId { get; set; } = new byte[] {0x30 };
        public IEnumerable<byte> NegTokenASNLen { get; set; } = new byte[] {0x5b };
        public IEnumerable<byte> NegTokenTag0ASNId { get; set; } = new byte[] {0xA0 };
        public IEnumerable<byte> NegTokenTag0ASNLen { get; set; } = new byte[] {0x3c };
        public IEnumerable<byte> NegThisMechASNId { get; set; } = new byte[] {0x30 };
        public IEnumerable<byte> NegThisMechASNLen { get; set; } = new byte[] {0x3a };
        public IEnumerable<byte> NegThisMech1ASNId { get; set; } = new byte[] {0x06 };
        public IEnumerable<byte> NegThisMech1ASNLen { get; set; } = new byte[] {0x0a };
        public IEnumerable<byte> NegThisMech1ASNStr { get; set; } = new byte[] {0x2b, 0x06, 0x01, 0x04, 0x01, 0x82, 0x37, 0x02, 0x02, 0x1e };
        public IEnumerable<byte> NegThisMech2ASNId { get; set; } = new byte[] {0x06 };
        public IEnumerable<byte> NegThisMech2ASNLen { get; set; } = new byte[] {0x09 };
        public IEnumerable<byte> NegThisMech2ASNStr { get; set; } = new byte[] {0x2a, 0x86, 0x48, 0x82, 0xf7, 0x12, 0x01, 0x02, 0x02 };
        public IEnumerable<byte> NegThisMech3ASNId { get; set; } = new byte[] {0x06 };
        public IEnumerable<byte> NegThisMech3ASNLen { get; set; } = new byte[] {0x09 };
        public IEnumerable<byte> NegThisMech3ASNStr { get; set; } = new byte[] {0x2a, 0x86, 0x48, 0x86, 0xf7, 0x12, 0x01, 0x02, 0x02 };
        public IEnumerable<byte> NegThisMech4ASNId { get; set; } = new byte[] {0x06 };
        public IEnumerable<byte> NegThisMech4ASNLen { get; set; } = new byte[] {0x0a };
        public IEnumerable<byte> NegThisMech4ASNStr { get; set; } = new byte[] {0x2a, 0x86, 0x48, 0x86, 0xf7, 0x12, 0x01, 0x02, 0x02, 0x03 };
        public IEnumerable<byte> NegThisMech5ASNId { get; set; } = new byte[] {0x06 };
        public IEnumerable<byte> NegThisMech5ASNLen { get; set; } = new byte[] {0x0a };
        public IEnumerable<byte> NegThisMech5ASNStr { get; set; } = new byte[] {0x2b, 0x06, 0x01, 0x04, 0x01, 0x82, 0x37, 0x02, 0x02, 0x0a };
        public IEnumerable<byte> NegTokenTag3ASNId { get; set; } = new byte[] {0xa3 };
        public IEnumerable<byte> NegTokenTag3ASNLen { get; set; } = new byte[] {0x1b };
        public IEnumerable<byte> NegHintASNId { get; set; } = new byte[] {0x30 };
        public IEnumerable<byte> NegHintASNLen { get; set; } = new byte[] {0x19 };
        public IEnumerable<byte> NegHintTag0ASNId { get; set; } = new byte[] {0xa0 };
        public IEnumerable<byte> NegHintTag0ASNLen { get; set; } = new byte[] {0x17 };
        public IEnumerable<byte> NegHintFinalASNId { get; set; } = new byte[] {0x1b };
        public IEnumerable<byte> NegHintFinalASNLen { get; set; } = new byte[] {0x15 };
        public IEnumerable<byte> NegHintFinalASNStr { get; set; } = Encoding.UTF8.GetBytes("Server2008@SMB3.local");
    }
}