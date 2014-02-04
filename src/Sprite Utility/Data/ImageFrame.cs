using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using RCR.Pipeline.Imaging;
using SpriteUtility.Helpers;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace SpriteUtility
{
    public class ImageFrame
    {
        private readonly byte[] _data;
        private readonly int _height;
        private readonly ObservableCollection<Poly> _polygons;
        private readonly int _width;
        private Rectangle _trimRectangle;
        private int _centerPointX, _centerPointY;
        private int _duration; //in 1/100 second
        private bool _isOpen;

        public ImageFrame(byte[] data, int width, int height) : this(width, height)
        {
            _data = data;

            using (var imageData = new MemoryStream(_data))
            {
                System.Drawing.Rectangle trimRectangle = BitmapTools.TrimRect(new Bitmap(imageData));
                _trimRectangle = new Rectangle(trimRectangle.X, trimRectangle.Y,
                    trimRectangle.Width, trimRectangle.Height);
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

        public ImageFrame(BinaryReader reader) : this(0, 0)
        {
            int dataSize = reader.ReadInt32();
            _data = reader.ReadBytes(dataSize);
            _width = reader.ReadInt32();
            _height = reader.ReadInt32();
            _centerPointX = reader.ReadInt32();
            _centerPointY = reader.ReadInt32();
            int polyCount = reader.ReadInt32();
            for (int counter = 0; counter < polyCount; counter++)
                _polygons.Add(new Poly(reader, this));
            _duration = reader.ReadInt32();
            _isOpen = reader.ReadBoolean();
            int thumbnailDataSize = reader.ReadInt32();
            byte[] thumbnailData = reader.ReadBytes(thumbnailDataSize);
            Thumbnail = ImageHelper.ByteArrayToImage(thumbnailData);
        }

        public Rectangle TrimRectangle
        {
            get { return _trimRectangle; }
        }

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

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

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

        public int CenterPointX
        {
            get { return _centerPointX; }
            set { _centerPointX = value; }
        }

        public int CenterPointY
        {
            get { return _centerPointY; }
            set { _centerPointY = value; }
        }

        public byte[] Data
        {
            get { return _data; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public ObservableCollection<Poly> Polygons
        {
            get { return _polygons; }
        }

        public Image Thumbnail { get; set; }
        public event EventHandler<EventArgs> CenterPointChanged;
        public event EventHandler<EventArgs> IsOpenChanged;

        protected virtual void OnCenterPointChanged(object sender, EventArgs e)
        {
        }


        protected virtual void OnOpenClosedStateChanged(object sender, EventArgs e)
        {
        }

        private void PolyCollectionChanged(object sender, EventArgs e)
        {
            Document.Instance.Invalidate(this, EventArgs.Empty);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(_data.Length);
            writer.Write(_data);
            writer.Write(_width);
            writer.Write(_height);
            writer.Write(_centerPointX);
            writer.Write(_centerPointY);
            writer.Write(_polygons.Count);
            foreach (Poly p in _polygons)
                p.Write(writer);
            writer.Write(_duration);
            writer.Write(_isOpen);
            byte[] thumbnailData = ImageHelper.ImageToByteArray(Thumbnail);
            writer.Write(thumbnailData.Length);
            writer.Write(thumbnailData);
        }

        public void UpdateCenterPoint()
        {
            CenterPointChanged(this, EventArgs.Empty);
            Document.Instance.Invalidate(this, EventArgs.Empty);
        }
    }
}