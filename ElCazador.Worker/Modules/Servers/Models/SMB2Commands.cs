namespace ElCazador.Worker.Modules.Servers.Models
{
    internal class SMB2Commands
    {
        internal static readonly byte[] Request = { 0x00, 0x00 };
        internal static readonly byte[] NegotiateNTLM = { 0x01, 0x00 };
        internal static readonly byte SMB1NegotiateToSMB2 = 0x72;
    }
}