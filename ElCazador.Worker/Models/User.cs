using System;
using System.Net;
using ElCazador.Worker.DataStore;

namespace ElCazador.Worker.Models
{
    public class User : IDataObject
    {
        public User()
        {
            Timestamp = DateTime.UtcNow;
        }

        public object Key => Username;
        public DateTime Timestamp { get; set; }
        public string IPAddress { get; set; }
        public string Username { get; set; }
        [JsonEncrypt]
        public string Hash { get; set; }
        public bool IsClearText { get; set; }
        [JsonEncrypt]
        public string HashcatFormat { get; set; }
    }
}