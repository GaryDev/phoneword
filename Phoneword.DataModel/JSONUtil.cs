
namespace Phoneword.Util
{
    public static class JSONUtil
    {
        public static T ToObject<T>(string jsonStr)
        {
            if (string.IsNullOrWhiteSpace(jsonStr))
                jsonStr = "{}";

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public static string ToJSONStr(object obj)
        {
            if (obj == null)
                return "{}";

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
