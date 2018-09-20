using System;
using System.Threading.Tasks;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Hubs
{
    public class TargetHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddTarget(Target target)
        {
            target.Timestamp = DateTime.UtcNow;
            target.ID = Guid.NewGuid();

            await Clients.All.SendAsync("AddTarget", target);
        }
    }
}