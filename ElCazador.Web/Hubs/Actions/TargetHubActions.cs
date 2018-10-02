using System;
using System.Threading.Tasks;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Hubs.Actions
{
    public class TargetHubActions : IHubAction<Target>
    {
        private IHubContext<TargetHub> TargetHubContext { get; set; }

        public TargetHubActions(IHubContext<TargetHub> targetHubContext)
        {
            TargetHubContext = targetHubContext;
        }
        public async Task Add(Target target)
        {
            await TargetHubContext.Clients.All.SendAsync("AddTarget", target);
        }

        public async Task Edit(Target target)
        {
            await Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}