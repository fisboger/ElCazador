using System;
using ElCazador.Worker.DataStore;

namespace ElCazador.Worker.Models
{
    public class LogEntry : IDataObject
    {
        public LogEntry()
        {
            Timestamp = DateTime.UtcNow;
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public object[] Parameters { get; set; }

        #region Computed properties
        public string FormattedMessage => string.Format(Message, Parameters);

        public string TimestampString => Timestamp.ToString("MM/dd/yyyy HH:mm:ss");
        #endregion

    }
}