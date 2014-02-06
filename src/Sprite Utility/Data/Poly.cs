using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace SpriteUtility
{
    [Serializable]
    public class Poly
    {
        private readonly bool _invalidateTrigger;
        private readonly ObservableCollection<PolyPoint> _points;
        private string _name;
        private ImageFrame _frameParent;

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
                    Document.Instance.Invalidate(this, EventArgs.Empty);
                }
            }
        }
        
        public Poly()
        {
            _points = new ObservableCollection<PolyPoint>();
            _name = "New Poly";
            _invalidateTrigger = true;
            _points.CollectionChanged += PointCollectionChanged;
        }

        public void SetFrameParent(ImageFrame parent)
        {
            _frameParent = parent;
        }

        public event EventHandler<EventArgs> NameChanged;

        protected virtual void OnPointsChanged(object sender, EventArgs e) { }

        private void PointCollectionChanged(object sender, EventArgs e)
        {
            if (!_invalidateTrigger) return;
            Document.Instance.Invalidate(this, EventArgs.Empty);
        }

        public ImageFrame GetFrameParent()
        {
            return _frameParent;
        }
    }
}