using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Client.Services.Socket;
using NoteTaker.Client.State;
using NoteTaker.Client.State.SocketEvents;
using NoteTaker.Domain.Services;
using Sockets.Plugin;

namespace NoteTaker.Client.Services
{
    //https://github.com/rdavisau/sockets-for-pcl
    public class SocketSenderAppService : ISocketSenderAppService
    {
        private readonly IEventBroker _eventBroker;
        private readonly ISyncService _syncService;
        private readonly ISocketMessenger _socketMessenger;

        public SocketSenderAppService(IEventBroker eventBroker, ISyncService syncService, ISocketMessenger socketMessenger)
        {
            _eventBroker = eventBroker;
            _syncService = syncService;
            _socketMessenger = socketMessenger;
        }

        public void StartListeners()
        {
            _eventBroker.Listen<SendDataToSocketCommand>(SendDataToSocketCommandHandler);
        }

        private async Task SendDataToSocketCommandHandler(SendDataToSocketCommand command)
        {
            using (var client = new TcpSocketClient())
            {
                await client.ConnectAsync(command.IP, command.Port);

                await _socketMessenger.SendMessage(client, new SocketMessage(SocketHeaders.RequestLastMessage));
                var response = await _socketMessenger.GetMessage(client);
                await client.DisconnectAsync();
            }
        }
    }
}
