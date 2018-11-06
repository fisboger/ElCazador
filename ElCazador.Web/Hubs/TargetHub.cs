using System;
using System.Threading.Tasks;
using ElCazador.Web.Hubs.Actions;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Hubs
{
    public class TargetHub : Hub
    {
        private IWorkerController WorkerController { get; set; }
        private IDataStore DataStore { get; set; }
        private IHubActions<Target> TargetHubActions { get; set; }

        public TargetHub(
            IWorkerController workerController,
            IDataStore dataStore,
            IHubActions<Target> targetHubActions)
        {
            WorkerController = workerController;
            DataStore = dataStore;
            TargetHubActions = targetHubActions;
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var store = DataStore.Get<Target>();

            foreach (var target in store.All)
            {
                await TargetHubActions.Add(target);
            }
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