using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ElCazador.Worker.Modules.Tools.Models;

namespace ElCazador.Worker.Modules.Tools
{
    // We don't want this to be async as we really want the result and want to wait for it :)
    public static class ProcessStarter
    {
        public static async Task<ProcessResult> Start(string filename, string arguments)
        {
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
                    string errorOutput = await process.StandardError.ReadToEndAsync();
                    string resultOutput = await reader.ReadToEndAsync();


                    return new ProcessResult()
                    {
                        Path = filename,
                        Arguments = arguments,
                        ResultOutput = resultOutput
                    };
                }
            }
        }
    }
}