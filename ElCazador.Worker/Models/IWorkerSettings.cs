using System.Net;

namespace ElCazador.Worker.Models
{
    public interface IWorkerSettings
    {
        IPAddress IP { get; }
        string ImpacketExamplesPath { get; }
        string DumpPath { get; }
        string PythonExecutable { get; }
        string MimikatzExecutable { get; }
        string ProcdumpExecutable { get; }
    }
}