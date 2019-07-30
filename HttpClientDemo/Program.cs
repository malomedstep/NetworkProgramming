using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpClientDemo.Data;
using Newtonsoft.Json;

namespace HttpClientDemo {
    class Program {
        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args) {
            var manager = new BookManager();
            await manager.Add("CLR via PHP", "Jeffrey Richter", "Fantasy");
            var books = await manager.GetAll();
            foreach (var book in books) {
                foreach (var item in book) {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
            }

            //var book = 

            //foreach (var item in book) {
            //    Console.WriteLine(item);
            //}


            //var client = new HttpClient();
            //var url = "http://xam150.azurewebsites.net/api/books";
            //client.DefaultRequestHeaders.Add("Authorization", "f3ae2a49-a398-496f-b4d0-068e9531dcda");
            //// f3ae2a49-a398-496f-b4d0-068e9531dcda

            //    Book book = new Book() {
            //        Title = "CLR via C#",
            //        Authors = new List<string>(new[] { "Jeffrey Richter" }),
            //        ISBN = string.Empty,
            //        Genre = "Fantasy",
            //        PublishDate = DateTime.Now.Date,
            //    };

            //    var response = await client.PostAsync(url,
            //        new StringContent(
            //            JsonConvert.SerializeObject(book),
            //            Encoding.UTF8, "application/json"));

            //    Console.WriteLine(JsonConvert.DeserializeObject<Book>(
            //        await response.Content.ReadAsStringAsync()));
            //}


            //var response = await client.GetAsync(url);
            //if (response.StatusCode == HttpStatusCode.OK) {
            //    var content = response.Content;
            //    var data = await content.ReadAsStringAsync();
            //    var books = JsonConvert.DeserializeObject<Book[]>(data);
            //    foreach (var item in books) {
            //        Console.WriteLine($"{item.Title} {item.Authors.FirstOrDefault()}\n");
            //    }
            //} else if (response.StatusCode == HttpStatusCode.Unauthorized) {
            //    Console.WriteLine("Shalom, you need to get new token");
            //} else {
            //    Console.WriteLine();
            //}


        }
    }
}
