using System.Threading.Tasks;

namespace ElCazador.Worker.Modules
{
    public interface IPersistentModule : IModule
    {
        Task Run();
        Task Stop();
        bool IsEnabled { get; }
    }
}