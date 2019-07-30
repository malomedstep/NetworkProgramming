using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerDemo {
    class Program {
        static void Main(string[] args) {
            var server = new HttpListener();
            server.Prefixes.Add("http://localhost:27001/");
            server.Start();

            while(true) {
                var context = server.GetContext();
                var name = context.Request.QueryString["name"];
                var lang = context.Request.RawUrl.Split('/')[1];
                //var stream = context.Response.OutputStream;
                //var writer = new StreamWriter(stream);
                //writer.WriteLine($"<h1 style='color:red'>{(lang == "en" ? "Hello" : "Salam")}, <s>{name}</s></h1>");
                //writer.Close();
                context.Response.StatusCode = 515;
                context.Response.Close();
            }
        }
    }
}
