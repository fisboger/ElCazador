using System.Net;
using System.Threading.Tasks;
using ElCazador.Web.Hubs;
using ElCazador.Web.Hubs.Actions;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Worker
{
    public class WebController : IWorkerController
    {
        public WorkerSettings WorkerSettings => new WorkerSettings
        {
            IP = IPAddress.Parse("10.64.13.89")
        };

        public IDataStore DataStore { get; set; }
        private IHubActions<Target> TargetHubActions { get; set; }
        private IHubActions<User> UserHubActions { get; set; }
        private IHubActions<LogEntry> LogHubActions { get; set; }


        public WebController(
            IDataStore dataStore,
            IHubActions<Target> targetHubActions,
            IHubActions<User> userHubActions,
            IHubActions<LogEntry> logHubActions)
        {
            DataStore = dataStore;
            TargetHubActions = targetHubActions;
            UserHubActions = userHubActions;
            LogHubActions = logHubActions;
        }

        public async Task Log(string name, string value, params object[] args)
        {
            await Task.CompletedTask;
        }

        public async Task Output(string name, User user) 
        {
            await UserHubActions.Add(user);
        }

        public async Task Output(string name, Target target) 
        {
            var targetStore = DataStore.Get<Target>();

            await TargetHubActions.Add(target);
        }
    }
}