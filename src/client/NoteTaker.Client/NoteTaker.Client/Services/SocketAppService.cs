using System;
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

        private const int _port = 6660;
        private Socket _socket;

        public SocketAppService(IEventBroker eventBroker)
            : base(500)
        {
            _eventBroker = eventBroker;
        }

        public void StartListeners()
        {
            _eventBroker.Listen<StartListeningCommand>(StartListeningCommandHandler);
            _eventBroker.Listen<StopListeningCommand>(StopListeningCommandHandler);
        }

        private Task StopListeningCommandHandler(StopListeningCommand command)
        {
            return Task.Factory.StartNew(() =>
            {
                if (_socket == null)
                {
                    return;
                }

                _socket.Disconnect(false);
                _socket.Dispose();
                _socket = null;
            });
        }

        private Task StartListeningCommandHandler(StartListeningCommand command)
        {
            return Task.Factory.StartNew(async () =>
            {
                var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                var ipAddress = ipHostInfo.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
                var localEndPoint = new IPEndPoint(ipAddress, _port);

                // Create a TCP/IP socket.
                _socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.
                try
                {
                    _socket.Bind(localEndPoint);
                    _socket.Listen(100);

                    while (true)
                    {
                        await _eventBroker.Command(new SocketStartedListeningCommand(ipAddress.ToString(), _port));
                        var handler = _socket.Accept();
                        var data = "";
                        byte[] bytes = new byte[1024];

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
