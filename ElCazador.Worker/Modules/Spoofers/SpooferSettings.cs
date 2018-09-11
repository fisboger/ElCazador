using System.Net;
using ElCazador.Worker.Utils;

namespace ElCazador.Worker.Modules.Spoofers
{
    internal class SpooferSettings
    {
        /// <summary>
        /// Whether or not you wish to run NBNS spoofing. Default true
        /// </summary>
        /// <returns>True to spoof NBNS. False to not</returns>
        internal bool NBNS { get; set; } = true;

        /// <summary>
        /// Whether or not you wish to run LLMNR spoofing. Default true
        /// </summary>
        /// <returns>True to spoof LLMNR. False to not/returns>
        internal bool LLMNR { get; set; } = true;
        
        /// <summary>
        /// Set to true to only inspect and don't send any packets
        /// </summary>
        /// <returns></returns>
        internal bool Inspect { get; set; } = false;
    }
}