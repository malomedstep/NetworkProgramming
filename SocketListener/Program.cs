using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace SocketListener {

    public static class SocketExtensions {
        public static T Receive<T>(
           this Socket socket) {
            var bytes = new byte[1000000];
            int len = socket.Receive(bytes);
            using (var ms = new MemoryStream(bytes, 0, len)) {
                var bf = new BinaryFormatter();
                var obj = (T)bf.Deserialize(ms);
                return obj;
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
                            var msg = c.Receive<Message>();
                            switch (msg.Type) {
                                case MessageType.Exit:

                                    break;
                                case MessageType.Start:
                                    Process.Start(msg.Data as string);
                                    break;
                                case MessageType.Kill:

                                    break;
                                case MessageType.Tasklist:

                                    break;
                                default:
                                    break;
                            }

                            //var str = c.ReceiveString();
                            //if (str == "exit") {
                            //    client.Send("NOPE");
                            //} else if (str == "time") {
                            //    client.Send(DateTime.Now.ToString());
                            //} else if (str == "tasklist") {
                            //    client.Send(
                            //        string.Join("\n",
                            //          Process.GetProcesses()
                            //            .Select(p => p.ProcessName)));
                            //} else if (str.Contains("taskkill")) {
                            //    var tok = str.Split(' ');
                            //    if (tok.Length < 2) {
                            //        client.Send("INVALID SYNTAX");
                            //    } else {
                            //        if (tok[1].ToLower().Contains("chrome")) {
                            //            client.Send("NOPE. NOPE AGAIN");
                            //        } else {
                            //            Process.GetProcessesByName(tok[1])
                            //                   .ToList()
                            //                   .ForEach(t => t.Kill());
                            //            client.Send("OK");
                            //        }
                            //    }
                            //} else if (str.Contains("start")) {
                            //    var tok = str.Split(' ');
                            //    if (tok.Length < 2) {
                            //        client.Send("INVALID SYNTAX");
                            //    } else {
                            //        Process.Start(tok[1]);
                            //        client.Send("OK");
                            //    }
                            //} else {
                            //    str = str.Substring(0, str.Length < 20 ? str.Length : 20);
                            //    Console.WriteLine($"{c.RemoteEndPoint}: {str}");
                            //}

                        } catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine($"{c.RemoteEndPoint} disconnected");
                            break;
                        }
                    }
                });
            }
        }
    }
}
