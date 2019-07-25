using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpListenerDemo {
    class Program {
        static async Task MainAsync(string[] args) {
            var listener = new TcpListener(IPAddress.Any, 27001);
            listener.Start();

            while (true) {
                var client = await listener.AcceptTcpClientAsync();
                Console.WriteLine($"{client.Client.RemoteEndPoint} connected");
                var stream = client.GetStream();
                var reader = new BinaryReader(stream);
                var writer = new StreamWriter(stream);

                while (true) {
                    var msg = "";

                    var byteslen = reader.ReadInt32();
                    var bytes = reader.ReadBytes(byteslen);

                    using (var ms = new MemoryStream(bytes)) {
                        var gzip = new GZipStream(ms, CompressionMode.Decompress);
                        var r = new StreamReader(gzip);
                        msg = r.ReadLine();
                    }

                    if (msg.ToLower() == "exit") {
                        await writer.WriteLineAsync("Goodbye");
                        client.Close();
                        break;
                    }

                    Console.WriteLine($"{client.Client.RemoteEndPoint}: {msg}");
                }
            }
        }

        static void Main(string[] args) =>
            MainAsync(args).GetAwaiter().GetResult();
    }
}
