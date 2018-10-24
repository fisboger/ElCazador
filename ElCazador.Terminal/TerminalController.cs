using System;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using System.Threading;
using System.Threading.Tasks;
using ElCazador.Worker.DataStore;

namespace ElCazador.Terminal
{
    public class TerminalController : IWorkerController
    {
        public IDataStore DataStore => throw new System.NotImplementedException();

        public WorkerSettings WorkerSettings { get; private set; }

        public TerminalController(WorkerSettings workerSettings)
        {
            WorkerSettings = workerSettings;
        }

        public async Task Add<T>(string name, T entity) where T : IDataObject
        {
            // TODO: Redo this please
            if (entity is User)
            {
                await Output(name, entity as User);
            }
            else if (entity is Target)
            {

            }
            else if (entity is LogEntry)
            {
                
            }
        }

        public async Task Log(string name, string value, params object[] args)
        {
            Console.WriteLine(name + ": " + value, args);

            await Task.CompletedTask;
        }

        public async Task Output(string name, User user)
        {
            Console.WriteLine(
                    @"{0}Received NetNTLMv2 hash from {1}{0}{2}::{3}:{4}:{5}:{6}",
                    Environment.NewLine,
                    user.IPAddress,
                    user.Username,
                    user.Domain,
                    user.Challenge,
                    String.Concat(user.NetLMHash),
                    string.Concat(user.NetNTHash)
                );

            await Task.CompletedTask;
        }
    }
}