using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public SocketSenderAppService(IEventBroker eventBroker, ISyncService syncService)
        {
            _eventBroker = eventBroker;
            _syncService = syncService;
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

                var fullMessage = await _syncService.GetMessages();
                var messageParts = new LinkedList<StringBuilder>();
                messageParts.AddLast(new StringBuilder());

                foreach (var c in fullMessage)
                {
                    messageParts.Last.Value.Append(c);

                    if (messageParts.Last.Value.Length > 500)
                    {
                        messageParts.AddLast(new StringBuilder());
                    }
                }

                foreach (var messagePart in messageParts)
                {
                    var bytes = Encoding.UTF8.GetBytes(messagePart.ToString());
                    await client.WriteStream.WriteAsync(bytes, 0, bytes.Length);
                }

                var ender = Encoding.UTF8.GetBytes("\0");
                await client.WriteStream.WriteAsync(ender, 0, ender.Length);

                await client.WriteStream.FlushAsync();
                await client.DisconnectAsync();
            }
        }
    }
}
