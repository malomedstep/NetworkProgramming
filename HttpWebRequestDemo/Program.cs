using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestDemo {
    class Program {
        static void Main(string[] args) {
            var url = "https://www.facebook.com";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            WebResponse response = request.GetResponse();
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            var body = reader.ReadToEnd();

            foreach (var header in response.Headers) {
                Console.WriteLine(response);
            }
            Console.WriteLine(body);

        }
    }
}
