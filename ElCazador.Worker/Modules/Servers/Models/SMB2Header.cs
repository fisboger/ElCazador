using System;
using System.Collections.Generic;
using System.Linq;
using ElCazador.Worker.Models;

namespace ElCazador.Worker.Modules.Servers.Models
{
    public class SMB2Header : IPacket
    {
        public SMB2Header(IEnumerable<byte> command)
        {
            Command = command;
        }

        public SMB2Header(
            IEnumerable<byte> command,
            IEnumerable<byte> messageID,
            IEnumerable<byte> creditCharge,
            IEnumerable<byte> credits,
            IEnumerable<byte> processID
            )
        {
            Command = command;
            MessageID = messageID;
            CreditCharge = creditCharge;
            Credits = credits;
            ProcessID = processID;

        }

        public SMB2Header(
            IEnumerable<byte> command,
            IEnumerable<byte> messageID,
            IEnumerable<byte> creditCharge,
            IEnumerable<byte> credits,
            IEnumerable<byte> processID,
            IEnumerable<byte> sessionID,
            IEnumerable<byte> ntStatus
        )
        {
            Command = command;
            MessageID = messageID;
            CreditCharge = creditCharge;
            Credits = credits;
            ProcessID = processID;
            SessionID = SessionID;
            NTStatus = ntStatus;
        }

        public byte[] Build()
        {
            return ProtocolID
                .Concat(Length)
                .Concat(CreditCharge)
                .Concat(NTStatus)
                .Concat(Command)
                .Concat(Credits)
                .Concat(Flags)
                .Concat(NextCommand)
                .Concat(MessageID)
                .Concat(ProcessID)
                .Concat(TreeID)
                .Concat(SessionID)
                .Concat(Signature)
                .ToArray();
        }
        public IEnumerable<byte> ProtocolID { get; set; } = new byte[] { 0xfe, 0x53, 0x4d, 0x42 }; // 0xFE SMB
        public IEnumerable<byte> Length { get; set; } = new byte[] { 0x40, 0x00 }; // Always 64
        public IEnumerable<byte> CreditCharge { get; set; } = new byte[] { 0x00, 0x00 };
        public IEnumerable<byte> NTStatus { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> Command { get; set; }
        public IEnumerable<byte> Credits { get; set; } = new byte[] { 0x01, 0x00 };
        public IEnumerable<byte> Flags { get; set; } = new byte[] { 0x01, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> NextCommand { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> MessageID { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> ProcessID { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> TreeID { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> SessionID { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> Signature { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
    }
}