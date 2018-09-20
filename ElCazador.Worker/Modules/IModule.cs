using System.Threading.Tasks;

namespace ElCazador.Worker.Modules
{
    public interface IModule
    {
        string Name { get; }
        Task Run();
        bool IsEnabled { get; }
    }
}