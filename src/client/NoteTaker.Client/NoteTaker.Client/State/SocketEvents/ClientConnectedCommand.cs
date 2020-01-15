namespace NoteTaker.Client.State.SocketEvents
{
    public class ClientConnectedCommand
    {
        public ClientConnectedCommand(string ip, int port)
        {
            IP = ip;
            Port = port;
        }

        public string IP { get; }
        public int Port { get; }
    }
}
