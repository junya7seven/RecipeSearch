using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChat.Models
{
    public class Choice
    {
        public MessageQuery Message { get; set; }
        public int Index { get; set; }
        public string FinishReason { get; set; }
    }
}
