using System.Threading.Tasks;

namespace ElCazador.Worker.Modules
{
    public interface IModule
    {
         void Run();
         bool IsEnabled { get; }
    }
}