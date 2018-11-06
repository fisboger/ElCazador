using System;
using System.Net;
using ElCazador.Worker.DataStore;

namespace ElCazador.Worker.Models
{
    public class User : IDataObject
    {
        public User()
        {
            ID = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }

        public Guid ID { get; set; }
        public DateTime Timestamp { get; set; }
        public string IPAddress { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public bool IsClearText { get; set; }
        public string HashcatFormat { get; set; }
    }
}