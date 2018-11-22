using System;
using System.Net;
using ElCazador.Worker;
using ElCazador.Worker.Models;

namespace ElCazador.Terminal
{
    class Program
    {
        static ElCazador.Worker.Worker Worker;
        static void Main(string[] args)
        {
            // Worker = new ElCazador.Worker.Worker(new TerminalController(new WorkerSettings
            // {
            //     IP = IPAddress.Parse("10.64.13.98")
            // }));

            Console.Out.Flush();
            Console.ReadLine();
        }
    }
}
