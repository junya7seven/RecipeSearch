using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChat.Models
{
    public class RootObjectGigaChat
    {
        public List<Choice> Choices { get; set; }
        public long Created { get; set; }
        public string Model { get; set; }
        public string Object { get; set; }
        public Usage Usage { get; set; }
    }
}
