using System;
using System.Net;
using System.Net.Sockets;

namespace ElCazador.Worker.Modules.Spoofers.Models
{
    internal class SocketType : IEquatable<SocketType>
    {
        internal string Name { get; set; }
        internal Sockets Type { get; set; }
        internal ProtocolType ProtocolType { get; set; }
        internal int Port { get; set; }
        internal EndPoint IPEndPoint;
        internal Socket Socket { get; set; }
        internal IPAddress MulticastAddress { get; set; }

        public bool Equals(SocketType other)
        {
            return this == other;
        }
    }
}