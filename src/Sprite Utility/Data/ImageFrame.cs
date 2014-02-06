using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace SpriteUtility
{
    [Serializable]
    public class ImageFrame
    {
        private readonly byte[] _data;
        private readonly int _height;
        private readonly ObservableCollection<Poly> _polygons;
        private readonly int _width;
        private Rectangle _trimRectangle;
        private int _centerPointX;
        private int _centerPointY;
        private int _duration; //in 1/100 second
        private bool _isOpen;

        [JsonProperty("trim_rectangle")]
        public Rectangle TrimRectangle
        {
            get { return _trimRectangle; }
        }

        [JsonProperty("is_open")]
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen != value)
                {
                    _isOpen = value;
                    IsOpenChanged(this, EventArgs.Empty);
                    Document.Instance.Invalidate(this, EventArgs.Empty);
                }
            }
        }

        [JsonProperty("duration")]
        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        [JsonProperty("mapped_center_point_x")]
        public int MappedCenterPointX
        {
            get
            {
                if (MainForm.Preferences.TrimToMinimalNonTransparentArea)
                {
                    return _centerPointX - _trimRectangle.X;
                }
                return _centerPointX;
            }
        }

        [JsonProperty("mapped_center_point_y")]
        public int MappedCenterPointY
        {
            get
            {
                if (MainForm.Preferences.TrimToMinimalNonTransparentArea)
                {
                    return _centerPointY - _trimRectangle.Y;
                }
                return _centerPointY;
            }
        }

        [JsonProperty("center_point_x")]
        public int CenterPointX
        {
            get { return _centerPointX; }
            set { _centerPointX = value; }
        }

        [JsonProperty("center_point_y")]
        public int CenterPointY
        {
            get { return _centerPointY; }
            set { _centerPointY = value; }
        }

        [JsonProperty("data")]
        public byte[] Data
        {
            get { return _data; }
        }

        [JsonProperty("width")]
        public int Width
        {
            get { return _width; }
        }

        [JsonProperty("height")]
        public int Height
        {
            get { return _height; }
        }

        [JsonProperty("polygons")]
        public ObservableCollection<Poly> Polygons
        {
            get { return _polygons; }
        }

        [JsonProperty("thumbnail")]
        public byte[] Thumbnail { get; set; }

        [JsonConstructor]
        public ImageFrame(byte[] data, int width, int height) : this(width, height)
        {
            _data = data;
            using (var imageData = new MemoryStream(_data))
            {
                var trimRectangle = BitmapTools.TrimRect(new Bitmap(imageData));
                _trimRectangle = new Rectangle(trimRectangle.X, trimRectangle.Y, trimRectangle.Width, trimRectangle.Height);
            }
        }

        public ImageFrame(int width, int height)
        {
            _data = null;
            _width = width;
            _height = height;
            _polygons = new ObservableCollection<Poly>();

            _centerPointX = _centerPointY = 0;
            _polygons.CollectionChanged += PolyCollectionChanged;

            CenterPointChanged += OnCenterPointChanged;
        }
        
        public event EventHandler<EventArgs> CenterPointChanged;
        public event EventHandler<EventArgs> IsOpenChanged;

        protected virtual void OnCenterPointChanged(object sender, EventArgs e) { }
        protected virtual void OnOpenClosedStateChanged(object sender, EventArgs e) { }

        private void PolyCollectionChanged(object sender, EventArgs e)
        {
            Document.Instance.Invalidate(this, EventArgs.Empty);
        }

        public void UpdateCenterPoint()
        {
            CenterPointChanged(this, EventArgs.Empty);
            Document.Instance.Invalidate(this, EventArgs.Empty);
        }
    }
}