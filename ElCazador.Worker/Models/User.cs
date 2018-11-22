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

        public object Key => string.Format("{0}\\{1}", Domain, Username);
        public DateTime Timestamp { get; set; }
        public string IPAddress { get; set; }
        public string Username { get; set; }
        public string Domain { get; set; }
        public bool IsClearText { get; set; }
        public string PasswordType { get; set; }


        [JsonEncrypt]
        public string HashcatFormat { get; set; }
        [JsonEncrypt]
        public string Hash { get; set; }
    }
}