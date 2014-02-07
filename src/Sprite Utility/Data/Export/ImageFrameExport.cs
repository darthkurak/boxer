using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using SpriteUtility.Data;
using SpriteUtility.Data.Export;

namespace SpriteUtility
{
    [Serializable]
    public class ImageFrameExport
    {
        [JsonProperty("center_point")]
        public Vector2 CenterPoint { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("is_open")]
        public bool IsOpen { get; set; }

        [JsonProperty("polygons")]
        public List<PolygonExport> Polygons { get; set; }

        public ImageFrameExport(ImageFrame frame)
        {
            CenterPoint = new Vector2(frame.MappedCenterPointX, frame.MappedCenterPointY);
            Polygons = new List<PolygonExport>(frame.Polygons.Count);
            foreach (var poly in frame.Polygons)
            {
                Polygons.Add(new PolygonExport(poly));
            }
            Duration = frame.Duration;
            IsOpen = frame.IsOpen;
        }
    }
}