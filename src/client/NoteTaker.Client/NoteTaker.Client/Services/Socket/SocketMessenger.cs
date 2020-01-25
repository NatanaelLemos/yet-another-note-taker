using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace NoteTaker.Client.Services.Socket
{
    public interface ISocketMessenger
    {
        Task SendMessage(ITcpSocketClient client, SocketMessage message);
        Task<SocketMessage> GetMessage(ITcpSocketClient client);
    }

    public class SocketMessenger : ISocketMessenger
    {
        public async Task SendMessage(ITcpSocketClient client, SocketMessage message)
        {
            var serialized = JsonSerializer.Serialize(message);

            var messageParts = new LinkedList<StringBuilder>();
            messageParts.AddLast(new StringBuilder());

            foreach (var c in serialized)
            {
                messageParts.Last.Value.Append(c);

                if (messageParts.Last.Value.Length > 500)
                {
                    messageParts.AddLast(new StringBuilder());
                }
            }

            messageParts.AddLast(new StringBuilder("\0"));

            foreach (var messagePart in messageParts)
            {
                if (messagePart.Length == 0)
                {
                    continue;
                }

                var bytes = Encoding.UTF8.GetBytes(messagePart.ToString());
                await client.WriteStream.WriteAsync(bytes, 0, bytes.Length);
            }

            await client.WriteStream.FlushAsync();
        }

        public async Task<SocketMessage> GetMessage(ITcpSocketClient client)
        {
            var bytesRead = -1;
            var read = new LinkedList<byte>();

            while (bytesRead != 0 && client != null)
            {
                var buffer = new byte[1];
                bytesRead = await client.ReadStream.ReadAsync(buffer, 0, 1);

                if (bytesRead <= 0)
                {
                    continue;
                }

                if (buffer[0] == '\0')
                {
                    var message = Encoding.UTF8.GetString(read.ToArray());
                    read.Clear();

                    return JsonSerializer.Deserialize<SocketMessage>(message);
                }
                else
                {
                    read.AddLast(buffer[0]);
                }
            }

            return default;
        }
    }
}
