using System.Threading.Tasks;
using ElCazador.Worker.Models;

namespace ElCazador.Worker.Modules
{
    public interface IToolModule
    {
        Task Run(Target target, User user);
    }
}