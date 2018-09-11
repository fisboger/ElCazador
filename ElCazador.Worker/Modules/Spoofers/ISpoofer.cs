using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using ElCazador.Worker.Modules.Spoofers.Models;

namespace ElCazador.Worker.Modules.Spoofers
{
    internal interface ISpoofer
    {
         Task HandlePacket(SpooferPacket state);
         Task HandleRequestPacket(SpooferPacket state);
         Task SpoofPacket(SpooferPacket state);
    }
}