using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoneword.ViewModel
{
    public class ApiError
    {
        [JsonProperty(PropertyName = "errCode")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "errMessage")]
        public string ErrorDescription { get; set; }

        [JsonProperty(PropertyName = "errStatus")]
        public int HttpStatus { get; set; }

        [JsonProperty(PropertyName = "errReason")]
        public string ReasonPhrase { get; set; }
    }
}