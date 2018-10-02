namespace ElCazador.Worker.Models
{
    public class LogEntry
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string[] Parameters { get; set; }
    }
}