using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SpriteUtility
{
    public class ImageFrameExport
    {
        public Vector2 CenterPoint { get; set; }
        public List<PolygonExport> Polygons { get; set; }
        public int Duration { get; set; }
        public bool IsOpen { get; set; }

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