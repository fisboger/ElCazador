using System.Net;
using Microsoft.Extensions.Configuration;

namespace ElCazador.Worker.Models
{
    public class WorkerSettings : IWorkerSettings
    {
        private IConfiguration Configuration { get; set; }

        public WorkerSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IPAddress IP => IPAddress.Parse(Configuration.GetValue<string>("ElCazador:IPAddress"));
        public string ImpacketExamplesPath => Configuration.GetValue<string>("ElCazador:ImpacketExamplesPath");
        public string DumpPath => Configuration.GetValue<string>("ElCazador:DumpPath");
    }
}