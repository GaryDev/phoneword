using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoneword.DataModel
{
    public class AppConfig
    {
        [JsonProperty(PropertyName = "name")]
        public string AppName { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string AppDescription { get; set; }

        [JsonProperty(PropertyName = "webApiUrl")]
        public string WebApiUrl { get; set; }

    }
}
