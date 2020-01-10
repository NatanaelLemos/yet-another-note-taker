using System;
using System.Collections.Generic;
using System.Text;

namespace NoteTaker.Client.State.SocketEvents
{
    public class SocketStartedListeningCommand
    {
        public SocketStartedListeningCommand(string ip, int port)
        {
            IP = ip;
            Port = port;
        }

        public string IP { get; }

        public int Port { get; }
    }
}
