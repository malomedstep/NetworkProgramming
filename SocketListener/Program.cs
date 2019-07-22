using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketListener {

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
                var client = server.Accept();
                Task.Run(() => {
                    var c = client;
                    Console.WriteLine($"[{c.RemoteEndPoint} connected]");
                    while (true) {
                        try {
                            var str = c.ReceiveString();
                            if (str == "exit") {
                                client.Send("NOPE");
                            } else if (str == "time") {
                                client.Send(DateTime.Now.ToString());
                            } else if (str == "tasklist") {
                                client.Send(
                                    string.Join("\n",
                                      Process.GetProcesses()
                                        .Select(p => p.ProcessName)));
                            } else if (str.Contains("taskkill")) {
                                var tok = str.Split(' ');
                                Process.GetProcessesByName(tok[1])
                                       .ToList()
                                       .ForEach(t => t.Kill());
                            } else if (str.Contains("start")) {
                                var tok = str.Split(' ');
                                Process.Start(tok[1]);
                            } else {
                                str = str.Substring(0, str.Length < 20 ? str.Length : 20);
                                Console.WriteLine($"{c.RemoteEndPoint}: {str}");
                            }
                            
                        } catch(Exception ex) {
                            Console.WriteLine($"{c.RemoteEndPoint} disconnected");
                            break;
                        }
                    }
                });
            }
        }
    }
}
