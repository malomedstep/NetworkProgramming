using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpSocketClient {
    class Program {
        static void Main(string[] args) {
            var client = new Socket(
               AddressFamily.InterNetwork,
               SocketType.Dgram,
               ProtocolType.Udp
           );
            var msg = "Hello, world";
            var bytes = Encoding.ASCII.GetBytes(msg);
            var ep = new IPEndPoint(
                IPAddress.Parse("127.0.0.1"),
                27001
            );

            client.SendTo(bytes, ep);
            Console.WriteLine("goodbye");
            Console.ReadLine();
        }
    }
}
