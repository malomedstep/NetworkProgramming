using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketSender {
    public static class SocketExtensions {
        public static void Send(
            this Socket socket,
            string text
        ) {
            var bytes = Encoding.ASCII.GetBytes(text);
            socket.Send(bytes);
        }

        public static string ReceiveString(
            this Socket socket) {
            var bytes = new byte[1024];
            int len = socket.Receive(bytes);
            var str = Encoding.ASCII.GetString(bytes, 0, len);
            return str;
        }
    }



    class Program {
        static void Main(string[] args) {
            var client = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );
            var ep = new IPEndPoint(
                IPAddress.Parse("10.2.25.75"), 27001);
            client.Connect(ep);

            while (true) {
                var str = Console.ReadLine();
                client.Send(str);
                var response = client.ReceiveString();
                Console.WriteLine($"Server: {response}");
            }
        }
    }
}
