using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Client.Services.Socket;
using NoteTaker.Client.State;
using NoteTaker.Client.State.SocketEvents;
using NoteTaker.Domain.Services;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace NoteTaker.Client.Services
{
    //https://github.com/rdavisau/sockets-for-pcl
    public class SocketListenerAppService : ISocketListenerAppService
    {
        private readonly IEventBroker _eventBroker;
        private readonly ISyncService _syncService;
        private readonly ISocketMessenger _socketMessenger;

        private const int _listenPort = 6660;
        private TcpSocketListener _listener;

        public SocketListenerAppService(IEventBroker eventBroker, ISyncService syncService, ISocketMessenger socketMessenger)
        {
            _eventBroker = eventBroker;
            _syncService = syncService;
            _socketMessenger = socketMessenger;
        }

        public void StartListeners()
        {
            _eventBroker.Listen<StartListeningCommand>(StartListeningCommandHandler);
            _eventBroker.Listen<StopListeningCommand>(StopListeningCommandHandler);
        }

        private async Task StartListeningCommandHandler(StartListeningCommand command)
        {
            if (_listener != null)
            {
                return;
            }

            _listener = new TcpSocketListener();
            _listener.ConnectionReceived += Listener_ConnectionReceived;

            var ipAddress = GetIpAddress();
            await _listener.StartListeningAsync(_listenPort);
            await _eventBroker.Command(new SocketStartedListeningCommand(ipAddress, _listenPort));
        }

        private async Task StopListeningCommandHandler(StopListeningCommand command)
        {
            if (_listener == null)
            {
                return;
            }

            await _listener.StopListeningAsync();
            _listener.Dispose();
            _listener = null;

            var ipAddress = GetIpAddress();
            await _eventBroker.Command(new SocketStoppedListeningCommand(ipAddress, _listenPort));
        }

        private async void Listener_ConnectionReceived(object sender, TcpSocketListenerConnectEventArgs e)
        {
            await _eventBroker.Command(new ClientConnectedCommand(e.SocketClient.RemoteAddress, e.SocketClient.RemotePort));

            while (true)
            {
                var message = await _socketMessenger.GetMessage(e.SocketClient);
                await _eventBroker.Command(new SocketMessageReceivedCommand(message));

                var response = ProcessResponse(message);
                await _socketMessenger.SendMessage(e.SocketClient, response);
            }
        }

        private SocketMessage ProcessResponse(SocketMessage message)
        {
            switch (message.Header)
            {
                case SocketHeaders.RequestLastMessage:
                    return new SocketMessage(SocketHeaders.RequestLastMessage, "result");
                default:
                    break;
            }

            return default;
        }

        private string GetIpAddress()
        {
            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddress = ipHostInfo.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
            return ipAddress.ToString();
        }
    }
}
