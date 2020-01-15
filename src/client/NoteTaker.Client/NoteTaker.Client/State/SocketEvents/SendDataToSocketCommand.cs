namespace NoteTaker.Client.State.SocketEvents
{
    public class SendDataToSocketCommand
    {
        public SendDataToSocketCommand(string ip, int port)
        {
            IP = ip;
            Port = port;
        }

        public string IP { get; }
        public int Port { get; }
    }
}
