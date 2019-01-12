using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElCazador.Worker.DataStore;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules;

namespace ElCazador.Worker.Interfaces
{
    public interface IWorkerController
    {
        IWorkerSettings WorkerSettings { get; }
        IDataStore DataStore { get; }

        Task Add<T>(string name, T entity) where T : IDataObject;
        Task Log(string name, string value, params object[] args);
        Task Dump(Target target, User user);
        void SynchronizeTool(Type type, IActionModule tool);
    }
}