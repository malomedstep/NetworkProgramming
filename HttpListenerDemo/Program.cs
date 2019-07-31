using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerDemo {

    class Seen {
        public int Id { get; set; }
        public virtual Poteryashka Who { get; set; }
        public string Where { get; set; }
        public DateTime When { get; set; }
    }
    class Poteryashka {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime? Lost { get; set; }
        public DateTime? Found { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public bool IsFound { get; set; }
        public virtual ICollection<Seen> Seen { get; set; }
    }



    class Program {
        static void Main(string[] args) {




            var props = new[] {
                "Hair", "Nose", "Eyes"
            };
            
            for (int i = 0; i < 10; i++) {
                Console.WriteLine($"{Faker.ArrayFaker.SelectFrom(props)} {Faker.EnumFaker.SelectFrom<ConsoleColor>()}");


                // Console.WriteLine($"{Faker.LocationFaker.Country()} {Faker.LocationFaker.City()} {Faker.LocationFaker.Street()}");
            }




            //var server = new HttpListener();
            //server.Prefixes.Add("http://localhost:27001/");
            //server.Start();

            //while(true) {
            //    var context = server.GetContext();
            //    var name = context.Request.QueryString["name"];
            //    var lang = context.Request.RawUrl.Split('/')[1];
            //    //var stream = context.Response.OutputStream;
            //    //var writer = new StreamWriter(stream);
            //    //writer.WriteLine($"<h1 style='color:red'>{(lang == "en" ? "Hello" : "Salam")}, <s>{name}</s></h1>");
            //    //writer.Close();
            //    context.Response.StatusCode = 515;
            //    context.Response.Close();
            //}
        }
    }
}
