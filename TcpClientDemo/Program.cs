using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientDemo {
    class Program {
        static void MainAsync(string[] args) {
            var client = new TcpClient();
            var addr = IPAddress.Parse("10.2.25.75");
            var port = 27001;
          //  Console.ReadLine();
            client.Connect(addr, port);

            var stream = client.GetStream();
            var reader = new StreamReader(stream);
            var writer = new BinaryWriter(stream);

            while (true) {
                var str = Console.ReadLine();
                using (var ms = new MemoryStream()) {
                    Stream gzip = new GZipStream(ms, CompressionMode.Compress);
                    var wr = new StreamWriter(gzip);
                    wr.WriteLine(str);
                    wr.Flush();
                    gzip.Close(); // [ <====== HERE ]
                    var bytes = ms.ToArray();
                    writer.Write(bytes.Length);
                    writer.Flush();
                    writer.Write(bytes);
                    writer.Flush();
                }
                // await writer.FlushAsync();
            }
        }

        static void Main(string[] args) => MainAsync(args);
    }
}
