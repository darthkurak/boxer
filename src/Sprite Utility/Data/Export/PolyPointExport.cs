using System;
using Newtonsoft.Json;

namespace SpriteUtility
{
    [Serializable]
    public class PolyPointExport
    {
        public PolyPointExport(PolyPoint point)
        {
            X = point.MappedX;
            Y = point.MappedY;
        }

        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }
    }
}