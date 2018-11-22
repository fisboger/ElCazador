using System.Net;

namespace ElCazador.Worker.Models
{
    public interface IWorkerSettings
    {
        IPAddress IP { get; }
        string ImpacketExamplesPath { get; }
        string DumpPath { get; }
    }
}