using System.Threading;
using ElCazador.Worker.Interfaces;

namespace ElCazador.Worker.Modules
{
    public class BaseModule
    {
        protected IWorkerController Controller { get; private set; }
        public string Name { get; private set; }

        protected BaseModule(IWorkerController controller, string name)
        {
            Controller = controller;
            Name = name;
        }
    }
}