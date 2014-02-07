using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace SpriteUtility.Data
{
    [Serializable]
    public class Polygon
    {
        private readonly bool _invalidateTrigger;
        private readonly ObservableCollection<PolyPoint> _points;
        private string _name;
        private PolygonGroup _polygonGroupParent;

        public PolygonGroup PolygonGroupParent
        {
            get { return _polygonGroupParent; }
        }

        [JsonProperty("points")]
        public ObservableCollection<PolyPoint> Points
        {
            get { return _points; }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (!_name.Equals(value))
                {
                    _name = value;
                    if (NameChanged != null)
                    {
                        NameChanged(this, EventArgs.Empty);
                    }

                    Document.TryInvalidate(this, EventArgs.Empty);
                }
            }
        }

        public Polygon()
        {
            _points = new ObservableCollection<PolyPoint>();
            _name = "New Poly";
            _invalidateTrigger = true;
            _points.CollectionChanged += PointCollectionChanged;
        }

        public Polygon(PolygonGroup polygonGroupParent) : this()
        {
            _polygonGroupParent = polygonGroupParent;
        }

        public void SetPolygonGroupParent(PolygonGroup polygonGroupParent)
        {
            _polygonGroupParent = polygonGroupParent;
        }

        public event EventHandler<EventArgs> NameChanged;

        private void PointCollectionChanged(object sender, EventArgs e)
        {
            if (!_invalidateTrigger) return;
            Document.TryInvalidate(this, EventArgs.Empty);
        }

    }
}