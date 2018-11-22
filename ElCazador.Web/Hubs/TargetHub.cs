using System;
using System.Threading.Tasks;
using ElCazador.Web.Hubs.Actions;
using ElCazador.Web.Hubs.Models;
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

        public async Task AddTarget(Target target)
        {
            await WorkerController.Add("TargetHub", target);
        }

        public async Task DumpTarget(DumpTarget dumpTarget)
        {
            var targetStore = DataStore.Get<Target>();
            var userStore = DataStore.Get<User>();

            var target = targetStore.Get(dumpTarget.TargetKey);
            var user = userStore.Get(dumpTarget.UserKey);

            await WorkerController.Dump(target, user);
        }
    }
}