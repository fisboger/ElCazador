using System;
using System.Net;
using ElCazador.Worker.DataStore;

namespace ElCazador.Worker.Models
{
    public class User : IDataObject
    {
        public Guid ID { get; set; }
        public string Key { get { return string.Format("{0}\\{1}", Domain, Username); } }

        public string IPAddress { get; set; }
        public string Username { get; set; }
        public string Domain { get; set; }
        public string Challenge { get; set; }
        public string NetLMHash { get; set; }
        public string NetNTHash { get; set; }
    }
}