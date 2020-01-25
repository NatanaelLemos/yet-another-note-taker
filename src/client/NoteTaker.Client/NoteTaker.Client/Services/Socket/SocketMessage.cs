using System;
using System.Collections.Generic;
using System.Text;

namespace NoteTaker.Client.Services.Socket
{
    public class SocketMessage
    {
        public SocketMessage()
        {
        }

        public SocketMessage(SocketHeaders header, string body = "")
        {
            Header = header;
            Body = body;
        }

        public SocketHeaders Header { get; set; }
        public string Body { get; set; }
    }
}
