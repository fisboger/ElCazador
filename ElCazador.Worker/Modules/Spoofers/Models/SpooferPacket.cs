using System.Net.Sockets;

namespace ElCazador.Worker.Modules.Spoofers.Models
{
    internal class SpooferPacket
    {
        public const int BUFFER_SIZE = 508;
        public byte[] Buffer { get; set; } = new byte[BUFFER_SIZE];
    }
}