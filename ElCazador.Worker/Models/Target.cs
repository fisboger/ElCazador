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
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }
        public string Hostname { get; set; }
        public string IP { get; set; }
        public bool Dumped { get; set; }
        public DateTime Timestamp { get; set; }
    }
}