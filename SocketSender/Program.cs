using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace SocketSender {
    public static class SocketExtensions {
        public static void Send(
            this Socket socket,
            object @object) {
            using (var ms = new MemoryStream()) {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, @object);
                var bytes = ms.ToArray();
                socket.Send(bytes);
            }
        }


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

                var msg = new Message {
                    Id = Guid.NewGuid(),
                    Type = MessageType.Start,
                    Sender = Environment.UserName,
                    Data = str // EventArgs
                };

                client.Send(msg);

                var response = client.ReceiveString();
                Console.WriteLine($"Server: {response}");
            }
        }
    }
}
