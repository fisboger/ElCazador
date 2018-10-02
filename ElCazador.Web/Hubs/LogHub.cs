using System;
using System.Threading.Tasks;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Hubs
{
    public class LogHub : Hub
    {
        private IWorkerController WorkerController { get; set; }
        public LogHub(IWorkerController workerController)
        {
            WorkerController = workerController;
        }

        // public async Task AddLine(string name, string message, string[])
        // {
        //     target.Timestamp = DateTime.UtcNow;
        //     target.ID = Guid.NewGuid();

        //     await Clients.All.SendAsync("AddTarget", target);
        // }
    }
}