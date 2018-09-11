using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ElCazador.Worker.Modules.Servers.Models
{
    internal class SMBPacket
    {
        public const int BUFFER_SIZE = 1024;
        internal byte[] Buffer { get; set; } = new byte[BUFFER_SIZE];
        internal Socket Socket { get; set; }

        #region SMB fields
        internal byte ProtocolVersion { get => Buffer[8]; }
        internal IEnumerable<byte> Command { get => Buffer.Skip(16).Take(2); }
        internal IEnumerable<byte> MessageID { get => Buffer.Skip(28).Take(8); }
        internal IEnumerable<byte> CreditCharge { get => Buffer.Skip(10).Take(2); }
        internal IEnumerable<byte> ProcessID { get => Buffer.Skip(36).Take(4); }
        internal IEnumerable<byte> Credits
        {
            get
            {
                var result = Buffer.Skip(18).Take(2);

                if (result.SequenceEqual(new byte[] { 0x00, 0x00 }))
                {
                    return new byte[] { 0x01, 0x00 };
                }
                
                return result;
            }
        }
        internal IEnumerable<byte> SessionID { get => Buffer.Skip(44).Take(8); }

        #endregion
    }
}