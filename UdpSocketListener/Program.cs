using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpSocketListener {
    class Program {
        static void Main(string[] args) {
            var server = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp
            );
            var ep = new IPEndPoint(
                IPAddress.Any,
                27001
            );
            server.Bind(ep);


            var buffer = new byte[server.ReceiveBufferSize];
            EndPoint clientEp = new IPEndPoint(IPAddress.Any, 0);
            int len = server.ReceiveFrom(buffer, ref clientEp);

            server.SendTo(buffer, clientEp);

            var str = Encoding.ASCII.GetString(buffer, 0, len);
            Console.WriteLine(str);
            Console.WriteLine("goodbye");
        }
    }
}
