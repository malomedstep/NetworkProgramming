using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpAsyncSocketListener {
    public enum MessageType {
        Exit,
        Start,
        Kill,
        Tasklist,
        TextMessage
    }
    public class Message {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public object Data { get; set; }
        public MessageType Type { get; set; }
    }

    class Program {
        private static void SocketArgs_Completed(object sender, SocketAsyncEventArgs e) {
            var c = e.AcceptSocket;
            Console.WriteLine($"[{c.RemoteEndPoint} connected]");
            while (true) {
                var a = new SocketAsyncEventArgs();
                a.Completed += A_Completed; ;
                c.ReceiveAsync(a);
            }
        }

        private static void A_Completed(object sender, SocketAsyncEventArgs e) {
           
        }

        static void Main(string[] args) {
            var server = new Socket(
                   AddressFamily.InterNetwork,
                   SocketType.Stream,
                   ProtocolType.Tcp
               );
            var ep = new IPEndPoint(
                IPAddress.Any,
                27001
            );
            server.Bind(ep);
            server.Listen(100);

            while (true) {
                var a = new SocketAsyncEventArgs();
                a.Completed += SocketArgs_Completed;
                server.AcceptAsync(a);
            }
        }
    }
}
