using System;
using System.Threading.Tasks;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Hubs
{
    public class UserHub : Hub
    {
        private IWorkerController WorkerController { get; set; }
        public UserHub(IWorkerController workerController)
        {
            WorkerController = workerController;
        }

        public async Task AddUser(Target target)
        {
            target.Timestamp = DateTime.UtcNow;
            target.ID = Guid.NewGuid();

            await Clients.All.SendAsync("AddTarget", target);
        }
    }
}