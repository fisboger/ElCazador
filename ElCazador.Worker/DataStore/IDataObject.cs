using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ElCazador.Worker.DataStore
{
    public interface IDataObject
    {
        object Key { get; }
        DateTime Timestamp { get; }
    }
}