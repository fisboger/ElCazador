using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElCazador.Worker.Models;

namespace ElCazador.Worker.Modules.Servers.Models
{
    internal class SMB2NTLMNegotiate : IPacket
    {
        private IEnumerable<byte> Header { get; set; }

        internal SMB2NTLMNegotiate(IEnumerable<byte> header, IEnumerable<byte> challenge = null)
        {
            Header = header;

            if (challenge != null && challenge.Count() == 8)
            {
                NTLMSSPNtServerChallenge = challenge;
            }
        }
        public byte[] Build()
        {
            return Header
                .Concat(Len)
                .Concat(SessionFlag)
                .Concat(SecBlobOffSet)
                .Concat(SecBlobLen)
                .Concat(ChoiceTagASNId)
                .Concat(ChoiceTagASNLenOfLen)
                .Concat(ChoiceTagASNIdLen)
                .Concat(NegTokenTagASNId)
                .Concat(NegTokenTagASNLenOfLen)
                .Concat(NegTokenTagASNIdLen)
                .Concat(Tag0ASNId)
                .Concat(Tag0ASNIdLen)
                .Concat(NegoStateASNId)
                .Concat(NegoStateASNLen)
                .Concat(NegoStateASNValue)
                .Concat(Tag1ASNId)
                .Concat(Tag1ASNIdLen)
                .Concat(Tag1ASNId2)
                .Concat(Tag1ASNId2Len)
                .Concat(Tag1ASNId2Str)
                .Concat(Tag2ASNId)
                .Concat(Tag2ASNIdLenOfLen)
                .Concat(Tag2ASNIdLen)
                .Concat(Tag3ASNId)
                .Concat(Tag3ASNIdLenOfLen)
                .Concat(Tag3ASNIdLen)
                .Concat(NTLMSSPSignature)
                .Concat(NTLMSSPSignatureNull)
                .Concat(NTLMSSPMessageType)
                .Concat(NTLMSSPNtWorkstationLen)
                .Concat(NTLMSSPNtWorkstationMaxLen)
                .Concat(NTLMSSPNtWorkstationBuffOffset)
                .Concat(NTLMSSPNtNegotiateFlags)
                .Concat(NTLMSSPNtServerChallenge)
                .Concat(NTLMSSPNtReserved)
                .Concat(NTLMSSPNtTargetInfoLen)
                .Concat(NTLMSSPNtTargetInfoMaxLen)
                .Concat(NTLMSSPNtTargetInfoBuffOffset)
                .Concat(NegTokenInitSeqMechMessageVersionHigh)
                .Concat(NegTokenInitSeqMechMessageVersionLow)
                .Concat(NegTokenInitSeqMechMessageVersionBuilt)
                .Concat(NegTokenInitSeqMechMessageVersionReserved)
                .Concat(NegTokenInitSeqMechMessageVersionNTLMType)
                .Concat(NTLMSSPNtWorkstationName)
                .Concat(NTLMSSPNTLMChallengeAVPairsId)
                .Concat(NTLMSSPNTLMChallengeAVPairsLen)
                .Concat(NTLMSSPNTLMChallengeAVPairsUnicodeStr)
                .Concat(NTLMSSPNTLMChallengeAVPairs1Id)
                .Concat(NTLMSSPNTLMChallengeAVPairs1Len)
                .Concat(NTLMSSPNTLMChallengeAVPairs1UnicodeStr)
                .Concat(NTLMSSPNTLMChallengeAVPairs2Id)
                .Concat(NTLMSSPNTLMChallengeAVPairs2Len)
                .Concat(NTLMSSPNTLMChallengeAVPairs2UnicodeStr)
                .Concat(NTLMSSPNTLMChallengeAVPairs3Id)
                .Concat(NTLMSSPNTLMChallengeAVPairs3Len)
                .Concat(NTLMSSPNTLMChallengeAVPairs3UnicodeStr)
                .Concat(NTLMSSPNTLMChallengeAVPairs5Id)
                .Concat(NTLMSSPNTLMChallengeAVPairs5Len)
                .Concat(NTLMSSPNTLMChallengeAVPairs5UnicodeStr)
                .Concat(NTLMSSPNTLMChallengeAVPairs7Id)
                .Concat(NTLMSSPNTLMChallengeAVPairs7Len)
                .Concat(NTLMSSPNTLMChallengeAVPairs7UnicodeStr)
                .Concat(NTLMSSPNTLMChallengeAVPairs6Id)
                .Concat(NTLMSSPNTLMChallengeAVPairs6Len)
                .ToArray();


        }

        public IEnumerable<byte> Len { get; set; } = new byte[] { 0x09, 0x00 };
        public IEnumerable<byte> SessionFlag { get; set; } = new byte[] { 0x00, 0x00 };
        public IEnumerable<byte> SecBlobOffSet { get; set; } = new byte[] { 0x48, 0x00 };
        public IEnumerable<byte> SecBlobLen { get; set; } = new byte[] { 0x06, 0x01 };
        public IEnumerable<byte> ChoiceTagASNId { get; set; } = new byte[] { 0xa1 };
        public IEnumerable<byte> ChoiceTagASNLenOfLen { get; set; } = new byte[] { 0x82 };
        public IEnumerable<byte> ChoiceTagASNIdLen { get; set; } = new byte[] { 0x01, 0x02 };
        public IEnumerable<byte> NegTokenTagASNId { get; set; } = new byte[] { 0x30 };
        public IEnumerable<byte> NegTokenTagASNLenOfLen { get; set; } = new byte[] { 0x81 };
        public IEnumerable<byte> NegTokenTagASNIdLen { get; set; } = new byte[] { 0xff };
        public IEnumerable<byte> Tag0ASNId { get; set; } = new byte[] { 0xA0 };
        public IEnumerable<byte> Tag0ASNIdLen { get; set; } = new byte[] { 0x03 };
        public IEnumerable<byte> NegoStateASNId { get; set; } = new byte[] { 0x0A };
        public IEnumerable<byte> NegoStateASNLen { get; set; } = new byte[] { 0x01 };
        public IEnumerable<byte> NegoStateASNValue { get; set; } = new byte[] { 0x01 };
        public IEnumerable<byte> Tag1ASNId { get; set; } = new byte[] { 0xA1 };
        public IEnumerable<byte> Tag1ASNIdLen { get; set; } = new byte[] { 0x0c };
        public IEnumerable<byte> Tag1ASNId2 { get; set; } = new byte[] { 0x06 };
        public IEnumerable<byte> Tag1ASNId2Len { get; set; } = new byte[] { 0x0A };
        public IEnumerable<byte> Tag1ASNId2Str { get; set; } = new byte[] { 0x2b, 0x06, 0x01, 0x04, 0x01, 0x82, 0x37, 0x02, 0x02, 0x0a };
        public IEnumerable<byte> Tag2ASNId { get; set; } = new byte[] { 0xA2 };
        public IEnumerable<byte> Tag2ASNIdLenOfLen { get; set; } = new byte[] { 0x81 };
        public IEnumerable<byte> Tag2ASNIdLen { get; set; } = new byte[] { 0xE9 };
        public IEnumerable<byte> Tag3ASNId { get; set; } = new byte[] { 0x04 };
        public IEnumerable<byte> Tag3ASNIdLenOfLen { get; set; } = new byte[] { 0x81 };
        public IEnumerable<byte> Tag3ASNIdLen { get; set; } = new byte[] { 0xE6 };
        public IEnumerable<byte> NTLMSSPSignature { get; set; } = Encoding.UTF8.GetBytes("NTLMSSP");
        public IEnumerable<byte> NTLMSSPSignatureNull { get; set; } = new byte[] { 0x00 };
        public IEnumerable<byte> NTLMSSPMessageType { get; set; } = new byte[] { 0x02, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> NTLMSSPNtWorkstationLen { get; set; } = new byte[] { 0x08, 0x00 };
        public IEnumerable<byte> NTLMSSPNtWorkstationMaxLen { get; set; } = new byte[] { 0x08, 0x00 };
        public IEnumerable<byte> NTLMSSPNtWorkstationBuffOffset { get; set; } = new byte[] { 0x38, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> NTLMSSPNtNegotiateFlags { get; set; } = new byte[] { 0x15, 0x82, 0x89, 0xe2 };
        public IEnumerable<byte> NTLMSSPNtServerChallenge { get; set; } = new byte[] { 0x67, 0x69, 0x76, 0x65, 0x63, 0x70, 0x72, 0x23};
        public IEnumerable<byte> NTLMSSPNtReserved { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> NTLMSSPNtTargetInfoLen { get; set; } = new byte[] { 0xa6, 0x00 };
        public IEnumerable<byte> NTLMSSPNtTargetInfoMaxLen { get; set; } = new byte[] { 0xa6, 0x00 };
        public IEnumerable<byte> NTLMSSPNtTargetInfoBuffOffset { get; set; } = new byte[] { 0x40, 0x00, 0x00, 0x00 };
        public IEnumerable<byte> NegTokenInitSeqMechMessageVersionHigh { get; set; } = new byte[] { 0x06 };
        public IEnumerable<byte> NegTokenInitSeqMechMessageVersionLow { get; set; } = new byte[] { 0x03 };
        public IEnumerable<byte> NegTokenInitSeqMechMessageVersionBuilt { get; set; } = new byte[] { 0x80, 0x25 };
        public IEnumerable<byte> NegTokenInitSeqMechMessageVersionReserved { get; set; } = new byte[] { 0x00, 0x00, 0x00 };
        public IEnumerable<byte> NegTokenInitSeqMechMessageVersionNTLMType { get; set; } = new byte[] { 0x0f };
        public IEnumerable<byte> NTLMSSPNtWorkstationName { get; set; } = Encoding.Unicode.GetBytes("SMB3");
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairsId { get; set; } = new byte[] { 0x02, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairsLen { get; set; } = new byte[] { 0x08, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairsUnicodeStr { get; set; } = Encoding.Unicode.GetBytes("SMB3");
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs1Id { get; set; } = new byte[] { 0x01, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs1Len { get; set; } = new byte[] { 0x1e, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs1UnicodeStr { get; set; } = Encoding.Unicode.GetBytes("WIN-PRH492RQAFV");
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs2Id { get; set; } = new byte[] { 0x04, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs2Len { get; set; } = new byte[] { 0x14, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs2UnicodeStr { get; set; } = Encoding.Unicode.GetBytes("SMB3.local");
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs3Id { get; set; } = new byte[] { 0x03, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs3Len { get; set; } = new byte[] { 0x34, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs3UnicodeStr { get; set; } = Encoding.Unicode.GetBytes("WIN-PRH492RQAFV.SMB3.local");
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs5Id { get; set; } = new byte[] { 0x05, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs5Len { get; set; } = new byte[] { 0x14, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs5UnicodeStr { get; set; } = Encoding.Unicode.GetBytes("SMB3.local");
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs7Id { get; set; } = new byte[] { 0x07, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs7Len { get; set; } = new byte[] { 0x08, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs7UnicodeStr { get; set; } = new byte[] { 0xc0, 0x65, 0x31, 0x50, 0xde, 0x09, 0xd2, 0x01 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs6Id { get; set; } = new byte[] { 0x00, 0x00 };
        public IEnumerable<byte> NTLMSSPNTLMChallengeAVPairs6Len { get; set; } = new byte[] { 0x00, 0x00 };
    }
}