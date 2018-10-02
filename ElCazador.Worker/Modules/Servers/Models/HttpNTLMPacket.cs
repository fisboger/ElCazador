using System.Linq;
using System.Text;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules.Spoofers.Models;
using ElCazador.Worker.Utils;

namespace ElCazador.Worker.Modules.Servers.Models
{
    public class HttpNTLMPacket : IPacket
    {
        public HttpNTLMPacket() { }

        public byte[] Build()
        {
            var packet = Signature
                .Concat(SignatureNull)
                .Concat(MessageType)
                .Concat(TargetNameLen)
                .Concat(TargetNameMaxLan)
                .Concat(TargetNameOffset)
                .Concat(NegoFlags)
                .Concat(ServerChallenge)
                .Concat(Reserved)
                .Concat(TargetInfoLen)
                .Concat(TargetInfoMaxLen)
                .Concat(TargetInfoOffset)
                .Concat(NTLMOsVersion)
                .Concat(TargetNameStr)
                .Concat(Av1)
                .Concat(Av1Len)
                .Concat(Av1Str)
                .Concat(Av2)
                .Concat(Av2Len)
                .Concat(Av2Str)
                .Concat(Av3)
                .Concat(Av3Len)
                .Concat(Av3Str)
                .Concat(Av4)
                .Concat(Av4Len)
                .Concat(Av4Str)
                .Concat(Av5)
                .Concat(Av5Len)
                .Concat(Av5Str)
                .Concat(Av6)
                .Concat(Av6Len)
                .ToArray();

            return packet;
        }
        
        private byte[] Signature { get; set; } = Encoding.UTF8.GetBytes("NTLMSSP");
        private byte[] SignatureNull { get; set; } = new byte[] { 0x00 };
        private byte[] MessageType { get; set; } = new byte[] { 0x02, 0x00, 0x00, 0x00 };
        private byte[] TargetNameLen { get; set; } = new byte[] { 0x06, 0x00 };
        private byte[] TargetNameMaxLan { get; set; } = new byte[] { 0x06, 00 };
        private byte[] TargetNameOffset { get; set; } = new byte[] { 0x38, 0x00, 0x00, 0x00 };
        private byte[] NegoFlags { get; set; } = new byte[] { 0x05, 0x02, 0x89, 0xa2 };
        private byte[] ServerChallenge { get; set; } = new byte[] { 0x67, 0x69, 0x76, 0x65, 0x63, 0x70, 0x72, 0x23 };
        private byte[] Reserved { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] TargetInfoLen { get; set; } = new byte[] { 0x80, 0x00 };
        private byte[] TargetInfoMaxLen { get; set; } = new byte[] { 0x80, 0x00 };
        private byte[] TargetInfoOffset { get; set; } = new byte[] { 0x3e, 0x00, 0x00, 0x00 };
        private byte[] NTLMOsVersion { get; set; } = new byte[] { 0x05, 0x02, 0xce, 0x0e, 0x00, 0x00, 0x00, 0x0f };
        private byte[] TargetNameStr { get; set; } = Encoding.Unicode.GetBytes("SMB");
        private byte[] Av1 { get; set; } = new byte[] { 0x02, 0x00 };
        private byte[] Av1Len { get; set; } = new byte[] { 0x06, 0x00 };
        private byte[] Av1Str { get; set; } = Encoding.Unicode.GetBytes("SMB");
        private byte[] Av2 { get; set; } = new byte[] { 0x01, 0x00 };
        private byte[] Av2Len { get; set; } = new byte[] { 0x16, 0x00 };
        private byte[] Av2Str { get; set; } = Encoding.Unicode.GetBytes("SMB-TOOLKIT");
        private byte[] Av3 { get; set; } = new byte[] { 0x04, 0x00 };
        private byte[] Av3Len { get; set; } = new byte[] { 0x12, 0x00 };
        private byte[] Av3Str { get; set; } = Encoding.Unicode.GetBytes("smb.local");
        private byte[] Av4 { get; set; } = new byte[] { 0x03, 0x00 };
        private byte[] Av4Len { get; set; } = new byte[] { 0x28, 0x00 };
        private byte[] Av4Str { get; set; } = Encoding.Unicode.GetBytes("server2003.smb.local");
        private byte[] Av5 { get; set; } = new byte[] { 0x05, 0x00 };
        private byte[] Av5Len { get; set; } = new byte[] { 0x12, 0x00 };
        private byte[] Av5Str { get; set; } = Encoding.Unicode.GetBytes("smb.local");
        private byte[] Av6 { get; set; } = new byte[] { 0x00, 0x00 };
        private byte[] Av6Len { get; set; } = new byte[] { 0x00, 0x00 };

    }
}