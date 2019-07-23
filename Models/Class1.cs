using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    [Serializable]
    public enum MessageType {
        Exit,
        Start,
        Kill,
        Tasklist
    }
    [Serializable]
    public class Message {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public object Data { get; set; }
        public MessageType Type { get; set; }
    }
}
