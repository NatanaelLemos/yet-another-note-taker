using NoteTaker.Client.Services.Socket;

namespace NoteTaker.Client.State.SocketEvents
{
    public class SocketMessageReceivedCommand
    {
        public SocketMessageReceivedCommand(SocketMessage message)
        {
            Message = message;
        }

        public SocketMessage Message { get; }
    }
}
