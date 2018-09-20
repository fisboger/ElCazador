using System.Net;
using System.Threading.Tasks;
using ElCazador.Web.Hubs;
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

        private IHubContext<TargetHub> TargetHubContext { get; set; }


        public WebController(IHubContext<TargetHub> targetHubContext)
        {
            TargetHubContext = targetHubContext;
        }

        public IDataStorage DataStorage => throw new System.NotImplementedException();

        public async Task Log(string name, string value, params object[] args)
        {
            await Task.CompletedTask;
        }

        public async Task Output(string name, Hash hash)
        {
            await TargetHubContext.Clients.All.SendAsync("AddTarget", hash);
        }
    }
}