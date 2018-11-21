using System;
using System.ComponentModel;
using ElCazador.Worker.DataStore;

namespace ElCazador.Worker.Models
{
    public class Target : IDataObject
    {
        public Target()
        {
            Timestamp = DateTime.UtcNow;
        }
        public object Key => Hostname;
        public string Hostname { get; set; }
        public string IP { get; set; }
        public bool Dumped { get; set; }
        public DateTime DumpedTimestamp { get; set; }
        public DateTime Timestamp { get; set; }
    }
}