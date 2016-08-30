using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoneword.DataModel
{
    public class UserLoginData
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string SessionToken { get; set; }
    }
}