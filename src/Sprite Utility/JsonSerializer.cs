using System.IO;
using ServiceStack.Text;

namespace SpriteUtility
{
    public static class JsonSerializer
    {
        static JsonSerializer()
        {
            JsConfig.PropertyConvention = PropertyConvention.Lenient;
            JsConfig.EmitLowercaseUnderscoreNames = true;
            JsConfig.ExcludeTypeInfo = true;
            JsConfig.DateHandler = DateHandler.ISO8601;
        }

        public static string Serialize<T>(T value)
        {
            var json = ServiceStack.Text.JsonSerializer.SerializeToString(value);
            return json;
        }

        public static T Deserialize<T>(string json)
        {
            var instance = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(json);
            return instance;
        }

        public static T Deserialize<T>(Stream stream)
        {
            var instance = ServiceStack.Text.JsonSerializer.DeserializeFromStream<T>(stream);
            return instance;
        }
    }
}