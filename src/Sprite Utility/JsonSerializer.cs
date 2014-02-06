using Newtonsoft.Json;

namespace SpriteUtility
{
    public static class JsonSerializer
    {
        private static readonly JsonSerializerSettings Settings;
        
        static JsonSerializer()
        {
            Settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Formatting = Formatting.Indented,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }

        public static string Serialize<T>(T value)
        {
            var json = JsonConvert.SerializeObject(value, Settings);
            return json;
        }

        public static T Deserialize<T>(string json)
        {
            var instance = JsonConvert.DeserializeObject<T>(json, Settings);
            return instance;
        }
    }
}