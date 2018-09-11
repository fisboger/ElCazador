using System.Net;

namespace ElCazador.Worker.Models
{
    public class Hash
    {
        public string Key { get { return string.Format("{0}\\{1}", Domain, User); } }

        public IPAddress IPAddress { get; set; }
        public string User { get; set; }
        public string Domain { get; set; }
        public string Challenge { get; set; }
        public string NetLMHash { get; set; }
        public string NetNTHash { get; set; }
    }
}