using System;
using System.Threading.Tasks;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Hubs
{
    public class TargetHub : Hub
    {
        private IWorkerController WorkerController { get; set; }
        public TargetHub(IWorkerController workerController)
        {
            WorkerController = workerController;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddTarget(Target target)
        {
            await WorkerController.Add("TargetHub", target);
        }
    }
}