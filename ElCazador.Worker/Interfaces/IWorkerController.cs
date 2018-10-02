using System.Threading.Tasks;
using ElCazador.Worker.Models;

namespace ElCazador.Worker.Interfaces
{
    public interface IWorkerController
    {
        WorkerSettings WorkerSettings { get; }
        IDataStore DataStore { get; }

        Task Output(string name, User user);
        Task Log(string name, string value, params object[] args);
    }
}