using System;

namespace ElCazador.Worker.Models
{
    public class Target
    {
        public Guid ID { get; set; }
        public string Hostname { get; set; }
        public string IP { get; set; }
        public bool Dumped { get; set; }
        public DateTime Timestamp { get; set; }
        
    }
}