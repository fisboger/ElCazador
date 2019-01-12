using System.Threading.Tasks;
using ElCazador.Worker.Models;

namespace ElCazador.Worker.Modules
{
    public interface IActionModule : IModule
    {
        Task Run(Target target, User user);
    }
}