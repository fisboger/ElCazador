using System;
using System.Threading.Tasks;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Hubs.Actions
{
    public class LogHubActions : IHubAction<LogEntry>
    {
        private IHubContext<LogHub> LogHubContext { get; set; }

        public LogHubActions(IHubContext<LogHub> logHubContext)
        {
            LogHubContext = logHubContext;
        }
        public async Task Add(LogEntry logEntry)
        {
            await LogHubContext.Clients.All.SendAsync("AddLogEntry", logEntry.Name, string.Format(logEntry.Message, logEntry.Parameters));
        }

        public async Task Edit(LogEntry logEntry)
        {
            await Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}