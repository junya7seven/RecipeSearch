using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edamam.Models
{
    public class ParametersModelEdamam
    {
        public string Url { get; set; } = "https://api.edamam.com/api/recipes/v2";
        public string type { get; set; } = "public";
        public string appId { get; set; } = "901c81ef";
        public string appKey { get; set; } = "fa9555cf8a18d1e6acf9c7a0bbb4f4e2";
        public string field { get; set; } = "ingredientLines";
    }
}
