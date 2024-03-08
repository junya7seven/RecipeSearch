using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GigaChat.Models
{
    public class ModelList
    {
        public Model[] _data { set { data = value; } }

        private Model[] data { get; set; }

        public string obj { get; set; }

        public Model this[int x]
        {
            get { return data[x]; }
        }

        public List<Model> ToList() => data.ToList();
    }
}
