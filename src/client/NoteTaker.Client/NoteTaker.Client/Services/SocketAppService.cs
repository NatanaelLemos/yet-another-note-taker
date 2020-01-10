using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Client.State;
using NoteTaker.Client.State.SocketEvents;

namespace NoteTaker.Client.Services
{
    public class SocketAppService : TimedAppServiceBase, ISocketAppService
    {
        private readonly IEventBroker _eventBroker;

        public SocketAppService(IEventBroker eventBroker)
            : base(500)
        {
            _eventBroker = eventBroker;
        }

        public void StartListeners()
        {
            _eventBroker.Listen<StartListeningCommand>(StartListeningCommandHandler);
        }

        private Task StartListeningCommandHandler(StartListeningCommand command)
        {
            return Task.Factory.StartNew(async () =>
            {
                var port = 6660;
                var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                var ipAddress = ipHostInfo.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
                var localEndPoint = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                var listener = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(100);

                    while (true)
                    {
                        await _eventBroker.Command(new SocketStartedListeningCommand(ipAddress.ToString(), port));
                        var handler = listener.Accept();
                        var data = "";
                        byte[] bytes = new Byte[1024];

                        // An incoming connection needs to be processed.
                        while (true)
                        {
                            int bytesRec = handler.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            if (data.IndexOf("<EOF>") > -1)
                            {
                                break;
                            }
                        }

                        // Show the data on the console.
                        Console.WriteLine("Text received : {0}", data);

                        // Echo the data back to the client.
                        byte[] msg = Encoding.ASCII.GetBytes(data);

                        handler.Send(msg);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.Read();
            });
        }
    }
}
