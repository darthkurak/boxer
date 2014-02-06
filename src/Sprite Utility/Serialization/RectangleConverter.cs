using System;
using Newtonsoft.Json;

namespace SpriteUtility
{
    public class RectangleConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var rectangle = (Microsoft.Xna.Framework.Rectangle) value;
            writer.WriteValue(rectangle.X);
            writer.WriteValue(rectangle.Y);
            writer.WriteValue(rectangle.Width);
            writer.WriteValue(rectangle.Height);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            //{X:0 Y:0 Width:199 Height:151}
            return new Microsoft.Xna.Framework.Rectangle();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Microsoft.Xna.Framework.Rectangle);
        }
    }
}