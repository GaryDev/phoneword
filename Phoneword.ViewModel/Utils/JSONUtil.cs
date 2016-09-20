
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Phoneword.Util
{
    public static class JSONUtil
    {
        public static T ToObject<T>(string jsonStr)
        {
            if (string.IsNullOrWhiteSpace(jsonStr))
                jsonStr = "{}";

            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public static string ToJSONStr(object obj)
        {
            if (obj == null)
                return "{}";

            return JsonConvert.SerializeObject(obj);
        }
        
        public static string NormalizeJsonString(string json)
        {
            // Parse json string into JObject.
            var parsedObject = JObject.Parse(json);

            // Sort properties of JObject.
            var normalizedObject = SortPropertiesAlphabetically(parsedObject);

            // Serialize JObject .
            return JsonConvert.SerializeObject(normalizedObject);
        }

        private static JObject SortPropertiesAlphabetically(JObject original)
        {
            var result = new JObject();

            foreach (var property in original.Properties().ToList().OrderBy(p => p.Name))
            {
                var value = property.Value as JObject;

                if (value != null)
                {
                    value = SortPropertiesAlphabetically(value);
                    result.Add(property.Name, value);
                }
                else
                {
                    result.Add(property.Name, property.Value);
                }
            }

            return result;
        }
    }
}
