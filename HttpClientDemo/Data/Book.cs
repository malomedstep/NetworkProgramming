using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo.Data {
    public class Book {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public DateTime PublishDate { get; set; }
        public string Genre { get; set; }

        public IEnumerator GetEnumerator() {
            yield return ISBN;
            yield return Title;
            yield return Authors.First();
            yield return PublishDate;
            yield return Genre;
        }
    }
}
