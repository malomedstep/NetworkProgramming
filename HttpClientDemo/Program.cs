using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpClientDemo {
    class Program {
        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args) {
            var client = new HttpClient();
            var url = "https://amp.insider.com/images/5bf838e048eb12130920d3a4-750-563.jpg";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsByteArrayAsync();
            File.WriteAllBytes("file.jpg", data);
        }
    }
}
