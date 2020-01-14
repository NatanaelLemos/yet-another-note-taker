using System;
using System.Text;
using System.Threading.Tasks;
using Sockets.Plugin;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = "127.0.0.1";
            var port = 6660;
            var r = new Random();

            var client = new TcpSocketClient();
            client.ConnectAsync(address, port).Wait();

            // we're connected!
            for (int i = 0; i < 5; i++)
            {
                var message = "hello world\0";
                var bytes = Encoding.UTF8.GetBytes(message);
                client.WriteStream.WriteAsync(bytes, 0, bytes.Length).Wait();
                client.WriteStream.FlushAsync().Wait();

                // wait a little before sending the next bit of data
                Task.Delay(TimeSpan.FromMilliseconds(500)).Wait();
            }

            client.DisconnectAsync().Wait();
        }
    }
}