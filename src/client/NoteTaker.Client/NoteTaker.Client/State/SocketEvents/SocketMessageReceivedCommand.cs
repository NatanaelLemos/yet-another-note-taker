using System;
namespace NoteTaker.Client.State.SocketEvents
{
    public class SocketMessageReceivedCommand
    {
        public SocketMessageReceivedCommand(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
