using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ElCazador.Web.Hubs;
using ElCazador.Web.Hubs.Actions;
using ElCazador.Worker.DataStore;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules;
using ElCazador.Worker.Modules.Tools;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace ElCazador.Web.Worker
{
    public class WebController : IWorkerController
    {
        public IWorkerSettings WorkerSettings { get; set; }

        public IDataStore DataStore { get; set; }
        private IHubActions<Target> TargetHubActions { get; set; }
        private IHubActions<User> UserHubActions { get; set; }
        private IHubActions<LogEntry> LogHubActions { get; set; }
        private IDictionary<Type, IToolModule> Tools { get; set; }


        public WebController(
            IDataStore dataStore,
            IWorkerSettings workerSettings,
            IHubActions<Target> targetHubActions,
            IHubActions<User> userHubActions,
            IHubActions<LogEntry> logHubActions)
        {
            DataStore = dataStore;
            TargetHubActions = targetHubActions;
            UserHubActions = userHubActions;
            LogHubActions = logHubActions;

            WorkerSettings = workerSettings;

            Tools = new Dictionary<Type, IToolModule>();
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

        public async Task Log(string name, string value, params object[] args)
        {
            var logEntry = new LogEntry
            {
                Name = name,
                Message = value,
                Parameters = args
            };

            await Add(name, logEntry);
        }

        public async Task Dump(Target target, User user)
        {
            var tool = Tools[typeof(LsassDumpTool)];

            await tool.Run(target, user);
        }

        public void SynchronizeTool(Type type, IToolModule tool)
        {
            Tools.Add(type, tool);
        }

        private async Task Add(string name, LogEntry logEntry)
        {
            var value = string.Format(logEntry.Message, logEntry.Parameters);
            Console.WriteLine("{0}: {1}", name, value);

            await Add(logEntry, LogHubActions);
        }

        private async Task Add(string name, User user)
        {
            var store = DataStore.Get<User>();

            if (store.Get(user.Key) != null)
            {
                await Log("WebController", "User {0} already exists", user.Username);
            }
            else
            {
                await Log("WebController", "User {0} added", user.Username);

                await Add(user, UserHubActions);
            }
        }

        private async Task Add(string name, Target target)
        {
            await Log("WebController", "Target {0} added", target.Hostname);

            await Add(target, TargetHubActions);
        }

        private async Task Add<T>(T entity, IHubActions<T> hub)
        where T : IDataObject
        {
            var store = DataStore.Get<T>();

            await store.Add(entity);

            await hub.Add(entity);
        }
    }
}