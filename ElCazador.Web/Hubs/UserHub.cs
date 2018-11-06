using System;
using System.Threading.Tasks;
using ElCazador.Web.Hubs.Actions;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Hubs
{
    public class UserHub : Hub
    {
        private IWorkerController WorkerController { get; set; }
        private IDataStore DataStore { get; set; }
        private IHubActions<User> UserHubActions { get; set; }
        public UserHub(
            IWorkerController workerController,
            IDataStore dataStore,
            IHubActions<User> userHubActions)
        {
            WorkerController = workerController;
            DataStore = dataStore;
            UserHubActions = userHubActions;
        }
        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var store = DataStore.Get<User>();

            foreach (var user in store.All)
            {
                await UserHubActions.Add(user);
            }
        }
        public async Task AddUser(User user)
        {
            await WorkerController.Add("UserHub", user);
        }
    }
}