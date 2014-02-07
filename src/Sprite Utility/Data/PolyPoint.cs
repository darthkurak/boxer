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
                    return _x - _polyParent.PolygonGroupParent.FrameParent.TrimRectangle.X;
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
                    return _y - _polyParent.PolygonGroupParent.FrameParent.TrimRectangle.Y;
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
                    Document.TryInvalidate(this, EventArgs.Empty);
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
                Document.TryInvalidate(this, EventArgs.Empty);
            }
        }


        [JsonConstructor]
        public PolyPoint(int x, int y)
        {
            _x = x;
            _y = y;
            PointChanged += OnPointChanged;
        }

        public PolyPoint(int x, int y, Polygon polyParent) : this(x,y)
        {
            _polyParent = polyParent;
        }

        public void SetPolygonParent(Polygon polyParent)
        {
            _polyParent = polyParent;
        }
        
        public event EventHandler<EventArgs> PointChanged;

        protected virtual void OnPointChanged(object sender, EventArgs e) { }

    }
}