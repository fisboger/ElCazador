using System.Net;

namespace ElCazador.Worker.Models
{
    public class User
    {
        public string Key { get { return string.Format("{0}\\{1}", Domain, Username); } }

        public IPAddress IPAddress { get; set; }
        public string Username { get; set; }
        public string Domain { get; set; }
        public string Challenge { get; set; }
        public string NetLMHash { get; set; }
        public string NetNTHash { get; set; }
    }
}