using System;
using System.IO;

namespace SpriteUtility
{
    public class PolyPoint
    {
        private readonly Poly _polyParent;
        private int _x, _y;

        public PolyPoint(Poly polyParent)
            : this(0, 0, polyParent)
        {
        }

        public PolyPoint(int x, int y, Poly polyParent)
        {
            _x = x;
            _y = y;
            _polyParent = polyParent;
            PointChanged += OnPointChanged;
        }

        public PolyPoint(BinaryReader reader, Poly polyParent)
            : this(polyParent)
        {
            _x = reader.ReadInt32();
            _y = reader.ReadInt32();
        }

        public int MappedX
        {
            get
            {
                if (MainForm.Preferences.TrimToMinimalNonTransparentArea)
                {
                    return _x - _polyParent.FrameParent.TrimRectangle.X;
                }
                return _x;
            }
        }

        public int MappedY
        {
            get
            {
                if (MainForm.Preferences.TrimToMinimalNonTransparentArea)
                {
                    return _y - _polyParent.FrameParent.TrimRectangle.Y;
                }
                return _y;
            }
        }

        public int X
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    PointChanged(this, EventArgs.Empty);
                    Document.Instance.Invalidate(this, EventArgs.Empty);
                }
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    PointChanged(this, EventArgs.Empty);
                    Document.Instance.Invalidate(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> PointChanged;

        protected virtual void OnPointChanged(object sender, EventArgs e)
        {
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(_x);
            writer.Write(_y);
        }
    }
}