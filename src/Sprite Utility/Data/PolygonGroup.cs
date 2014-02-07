using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SpriteUtility.Data
{
    public class PolygonGroup
    {
        private readonly bool _invalidateTrigger;
        private ImageFrame _frameParent;
        private string _name;
        private readonly ObservableCollection<Polygon> _polygons;

        public ImageFrame FrameParent
        {
            get { return _frameParent; }
        }

        [JsonProperty("polygons")]
        public ObservableCollection<Polygon> Polygons
        {
            get { return _polygons; }
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
                    Document.Instance.Invalidate(this, EventArgs.Empty);
                }
            }
        }

        public PolygonGroup()
        {
            _polygons = new ObservableCollection<Polygon>();
            _name = "Body";
            _invalidateTrigger = true;
            _polygons.CollectionChanged += PolygonsCollectionChanged;
        }

        public PolygonGroup(ImageFrame frameParent) : this()
        {
            _frameParent = frameParent;
        }

        public void SetFrameParent(ImageFrame frameParent)
        {
            _frameParent = frameParent;
        }
        public event EventHandler<EventArgs> NameChanged;

        protected virtual void OnPointsChanged(object sender, EventArgs e) { }

        private void PolygonsCollectionChanged(object sender, EventArgs e)
        {
            if (!_invalidateTrigger) return;
            Document.Instance.Invalidate(this, EventArgs.Empty);
        }

    }
}
