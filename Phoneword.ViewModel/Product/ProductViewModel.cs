using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoneword.ViewModel
{
    public class ProductViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string ProductName { get; set; }
    }
}
