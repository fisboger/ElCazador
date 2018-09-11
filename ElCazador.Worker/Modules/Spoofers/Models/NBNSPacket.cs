using System.Linq;
using ElCazador.Worker.Models;

namespace ElCazador.Worker.Modules.Spoofers.Models
{
    public class NBNSPacket : IPacket
    {
        public NBNSPacket(byte[] data, byte[] ip)
        {
            Tid = data.Take(2).ToArray();
            NbtName = data.Skip(12).Take(34).ToArray();
            Ip = ip;
        }

        public byte[] Build()
        {
            return Tid
                .Concat(Flags)
                .Concat(Question)
                .Concat(AnswerRRS)
                .Concat(AuthorityRRS)
                .Concat(AdditionalRRS)
                .Concat(NbtName)
                .Concat(Type)
                .Concat(Classy)
                .Concat(TTL)
                .Concat(Len)
                .Concat(Flags1)
                .Concat(Ip)
                .ToArray();
        }

        private byte[] Tid { get; set; }
        private byte[] Flags { get; set; } = { 0x85, 0x00 };
        private byte[] Question { get; set; } = { 0x00, 0x00 };
        private byte[] AnswerRRS { get; set; } = { 0x00, 0x01 };
        private byte[] AuthorityRRS { get; set; } = { 0x00, 0x00 };
        private byte[] AdditionalRRS { get; set; } = { 0x00, 0x00 };
        private byte[] NbtName { get; set; }
        private byte[] Type { get; set; } = { 0x00, 0x20 };
        private byte[] Classy { get; set; } = { 0x00, 0x01 };
        private byte[] TTL { get; set; } = { 0x00, 0x00, 0x00, 0xa5 };
        private byte[] Len { get; set; } = { 0x00, 0x06 };
        private byte[] Flags1 { get; set; } = { 0x00, 0x00 };
        private byte[] Ip { get; set; } = { 0x00, 0x00, 0x00, 0x00 };
    }
}