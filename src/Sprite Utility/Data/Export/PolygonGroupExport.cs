using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SpriteUtility.Data.Export
{
    [Serializable]
    public class PolygonGroupExport
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("polygons")]
        public List<PolygonExport> Polygons { get; set; }

        public PolygonGroupExport(PolygonGroup poly)
        {
            Name = poly.Name;
            Polygons = new List<PolygonExport>(poly.Polygons.Count);
            foreach (var polygon in poly.Polygons)
            {
                Polygons.Add(new PolygonExport(polygon));
            }
        }
    }
}
