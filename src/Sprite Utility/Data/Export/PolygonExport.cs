using System.Collections.Generic;

namespace SpriteUtility
{
    public class PolygonExport
    {
        public string Name { get; set; }
        public List<PolyPointExport> Points { get; set; }

        public PolygonExport(Poly poly)
        {
            Name = poly.Name;
            Points = new List<PolyPointExport>(poly.Points.Count);
            foreach (var point in poly.Points)
            {
                Points.Add(new PolyPointExport(point));           
            }
        }
    }
}