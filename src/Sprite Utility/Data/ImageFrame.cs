using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace SpriteUtility.Data
{
    [Serializable]
    public class ImageFrame
    {
        private Rectangle _trimRectangle;
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
                    if (IsOpenChanged != null)
                    {
                        IsOpenChanged(this, EventArgs.Empty);
                    }
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
                    return CenterPointX - _trimRectangle.X;
                }
                return CenterPointX;
            }
        }

        [JsonProperty("mapped_center_point_y")]
        public int MappedCenterPointY
        {
            get
            {
                if (MainForm.Preferences.TrimToMinimalNonTransparentArea)
                {
                    return CenterPointY - _trimRectangle.Y;
                }
                return CenterPointY;
            }
        }

        [JsonProperty("center_point_x")]
        public int CenterPointX { get; set; }

        [JsonProperty("center_point_y")]
        public int CenterPointY { get; set; }

        [JsonProperty("data")]
        public byte[] Data { get; private set; }

        [JsonProperty("width")]
        public int Width { get; private set; }

        [JsonProperty("height")]
        public int Height { get; private set; }

        [JsonProperty("polygons")]
        public ObservableCollection<PolygonGroup> PolygonGroups { get; private set; }

        [JsonProperty("thumbnail")]
        public byte[] Thumbnail { get; set; }

        [JsonProperty("image_path")]
        public string ImagePath { get; set; }

        [JsonConstructor]
        public ImageFrame(byte[] data, int width, int height) : this(width, height)
        {
            Data = data;
            using (var imageData = new MemoryStream(Data))
            {
                _trimRectangle = BitmapTools.TrimRect(new Bitmap(imageData)); ;
            }
        }

        public ImageFrame(int width, int height)
        {
            Data = null;
            Width = width;
            Height = height;
            PolygonGroups = new ObservableCollection<PolygonGroup>();

            CenterPointX = CenterPointY = 0;
            PolygonGroups.CollectionChanged += PolygonGroupsCollectionChanged;

            CenterPointChanged += OnCenterPointChanged;
        }
        
        public event EventHandler<EventArgs> CenterPointChanged;
        public event EventHandler<EventArgs> IsOpenChanged;

        protected virtual void OnCenterPointChanged(object sender, EventArgs e) { }
        protected virtual void OnOpenClosedStateChanged(object sender, EventArgs e) { }

        private void PolygonGroupsCollectionChanged(object sender, EventArgs e)
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