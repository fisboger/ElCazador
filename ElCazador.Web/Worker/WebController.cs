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

        private IHubAction<Target> TargetHubActions { get; set; }
        private IHubAction<User> UserHubActions { get; set; }


        public WebController(
            IHubAction<Target> targetHubActions,
            IHubAction<User> userHubActions)
        {
            TargetHubActions = targetHubActions;
            UserHubActions = userHubActions;
        }

        public IDataStorage DataStorage => throw new System.NotImplementedException();

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
            await TargetHubActions.Add(target);
        }
    }
}