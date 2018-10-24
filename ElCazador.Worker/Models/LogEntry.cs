using System;
using ElCazador.Worker.DataStore;

namespace ElCazador.Worker.Models
{
    public class LogEntry : IDataObject
    {
        public Guid ID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public object[] Parameters { get; set; }

    }
}