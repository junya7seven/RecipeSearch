using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edamam.Models
{
    public class RootObjectCook
    {
        public int from { get; set; }
        public int to { get; set; }
        public int count { get; set; }
        public object _links { get; set; }
        public List<Hit> hits { get; set; }
    }
}
