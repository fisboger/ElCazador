using System.Threading.Tasks;
using ElCazador.Worker.DataStore;
using ElCazador.Worker.Models;

namespace ElCazador.Worker.Interfaces
{
    public interface IWorkerController
    {
        WorkerSettings WorkerSettings { get; }
        IDataStore DataStore { get; }

        Task Add<T>(string name, T entity) where T : IDataObject;
        Task Log(string name, string value, params object[] args);
    }
}