using System;
using ElCazador.Worker.DataStore;

namespace ElCazador.Worker.Modules.Tools.Models
{
    public class ProcessResult : IDataObject
    {
        public ProcessResult()
        {
            Key = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }


        public object Key { get; private set; }
        public DateTime Timestamp { get; set; }

        public string Path { get; set; }

        [JsonEncrypt]
        public string Arguments { get; set; }
        public string ResultOutput { get; set; }

        public bool HasErrors
        {
            get
            {
                if (ResultOutput.Contains("SMB SessionError:"))
                {
                    return true;
                }

                return false;
            }
        }
    }
}