using System.Threading.Tasks;

namespace ElCazador.Worker.Modules
{
    public interface IPersistantModule : IModule
    {
        Task Run();
        Task Stop();
        bool IsEnabled { get; }
    }
}