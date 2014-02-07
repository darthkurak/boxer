using System;
using Newtonsoft.Json;

namespace SpriteUtility.Data
{
    public class PolyPoint
    {
        private Polygon _polyParent;
        private int _x, _y;

        [JsonProperty("mapped_x")]
        public int MappedX
        {
            get
            {
                if (MainForm.Preferences.TrimToMinimalNonTransparentArea)
                {
                    return _x - _polyParent.GetFrameParent().TrimRectangle.X;
                }
                return _x;
            }
        }

        [JsonProperty("mapped_y")]
        public int MappedY
        {
            get
            {
                if (MainForm.Preferences.TrimToMinimalNonTransparentArea)
                {
                    return _y - _polyParent.GetFrameParent().TrimRectangle.Y;
                }
                return _y;
            }
        }

        [JsonProperty("x")]
        public int X
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    if (PointChanged != null)
                    {
                        PointChanged(this, EventArgs.Empty);
                    }
                    Document.Instance.Invalidate(this, EventArgs.Empty);
                }
            }
        }

        [JsonProperty("y")]
        public int Y
        {
            get { return _y; }
            set
            {
                if (_y == value) return;
                _y = value;
                if (PointChanged != null)
                {
                    PointChanged(this, EventArgs.Empty);
                }
                Document.Instance.Invalidate(this, EventArgs.Empty);
            }
        }

        [JsonConstructor]
        public PolyPoint(int x, int y)
        {
            _x = x;
            _y = y;
            PointChanged += OnPointChanged;
        }
        
        public event EventHandler<EventArgs> PointChanged;

        protected virtual void OnPointChanged(object sender, EventArgs e) { }

        public void SetPolygonParent(Polygon polygon)
        {
            _polyParent = polygon;
        }
    }
}