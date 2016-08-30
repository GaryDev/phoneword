using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoneword.DataModel
{
    public class UserLogin
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}