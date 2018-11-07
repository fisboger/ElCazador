using System;
using System.Threading.Tasks;
using ElCazador.Web.Hubs.Actions;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Hubs
{
    public class LogHub : Hub
    {
        private IWorkerController WorkerController { get; set; }
        private IDataStore DataStore { get; set; }
        private IHubActions<LogEntry> LogHubActions { get; set; }
        public LogHub(
            IWorkerController workerController,
            IDataStore dataStore,
            IHubActions<LogEntry> userHubActions)
        {
            WorkerController = workerController;
            DataStore = dataStore;
            LogHubActions = userHubActions;
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var store = DataStore.Get<LogEntry>();

            foreach (var entry in store.All)
            {
                await LogHubActions.Add(entry);
            }
        }
        public async Task AddLogEntry(LogEntry entry)
        {
            await WorkerController.Add("LogHub", entry);
        }
    }
}