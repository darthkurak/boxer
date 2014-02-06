using System;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace SpriteUtility
{
    [Serializable]
    public class Poly
    {
        private readonly bool _invalidateTrigger;
        private readonly ObservableCollection<PolyPoint> _points;
        private string _name;

        public Poly(ImageFrame frameParent)
        {
            _points = new ObservableCollection<PolyPoint>();
            _name = "New Poly";
            _invalidateTrigger = true;
            FrameParent = frameParent;

            _points.CollectionChanged += PointCollectionChanged;
        }

        public Poly(BinaryReader reader, ImageFrame frameParent)
            : this(frameParent)
        {
            _name = reader.ReadString();
            int pointCount = reader.ReadInt32();
            _invalidateTrigger = false;
            for (int counter = 0; counter < pointCount; counter++)
                _points.Add(new PolyPoint(reader, this));
            _invalidateTrigger = true;
        }

        [JsonProperty("frame_parent")]
        public ImageFrame FrameParent { get; private set; }

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
                    NameChanged(this, EventArgs.Empty);
                    Document.Instance.Invalidate(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> NameChanged;

        protected virtual void OnPointsChanged(object sender, EventArgs e)
        {
        }

        private void PointCollectionChanged(object sender, EventArgs e)
        {
            if (!_invalidateTrigger) return;
            Document.Instance.Invalidate(this, EventArgs.Empty);
        }
    }
}