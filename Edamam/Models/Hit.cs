using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edamam.Models
{
    public class Hit
    {
        public Recipe Recipe { get; set; }
        public Links _links { get; set; }
    }
}
