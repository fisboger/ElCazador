using System;
using System.Net;
using System.Threading.Tasks;
using ElCazador.Web.Hubs;
using ElCazador.Web.Hubs.Actions;
using ElCazador.Worker.DataStore;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ElCazador.Web.Worker
{
    public class WebController : IWorkerController
    {
        public WorkerSettings WorkerSettings => new WorkerSettings
        {
            IP = IPAddress.Parse("10.64.13.98")
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

        public async Task Add<T>(string name, T entity) where T : IDataObject
        {
            // TODO: Redo this please
            if (entity is User)
            {
                await Add(name, entity as User);
            }
            else if (entity is Target)
            {
                await Add(name, entity as Target);
            }
            else if (entity is LogEntry)
            {
                await Add(name, entity as LogEntry);
            }
        }

        public async Task Log(string name, string value, params object[] args) {
            var logEntry = new LogEntry {
                Name = name,
                Message = value,
                Parameters = args
            };

            await Add(name, logEntry);
        }

        public async Task Add(string name, LogEntry logEntry)
        {
            logEntry.ID = Guid.NewGuid();
            logEntry.Timestamp = DateTime.UtcNow;

            var value = string.Format(logEntry.Message, logEntry.Parameters);
            Console.WriteLine("{0}: {1}", name, value);

            var logStore = DataStore.Get<LogEntry>();

            await logStore.Add(logEntry);

            await LogHubActions.Add(logEntry);
        }

        public async Task Add(string name, User user)
        {
            user.ID = Guid.NewGuid();

            Console.WriteLine("{0}: Got user {1}", name, user.Username);
            var userStore = DataStore.Get<User>();

            await userStore.Add(user);

            await UserHubActions.Add(user);
        }

        public async Task Output(string name, Target target)
        {
            target.Timestamp = DateTime.UtcNow;
            target.ID = Guid.NewGuid();

            var targetStore = DataStore.Get<Target>();

            await targetStore.Add(target);

            await TargetHubActions.Add(target);
        }
    }
}