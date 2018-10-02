using System;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ElCazador.Terminal
{
    public class TerminalController : IWorkerController
    {
        public IDataStorage DataStorage => throw new System.NotImplementedException();

        public WorkerSettings WorkerSettings { get; private set;}

        public TerminalController(WorkerSettings workerSettings)
        {
            WorkerSettings = workerSettings;
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
                    user.IPAddress.ToString(),
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