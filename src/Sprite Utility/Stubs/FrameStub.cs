using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using SpriteUtility.Helpers;

namespace SpriteUtility
{
    public partial class FrameStub : CustomSelection
    {
        private readonly ImageFrame _frame;

        public FrameStub(ImageFrame frame, ImageStub parent) : base(parent)
        {
            InitializeComponent();
            _frame = frame;
            SetFrameLabel();
            PolyCount.Text = "Polygons: " + _frame.Polygons.Count;
            SetContextMenu();
            Thumbnail.BackgroundImage = ImageHelper.ByteArrayToImage(_frame.Thumbnail);
            parent.Image.Frames.CollectionChanged += Image_FramesChanged;
            _frame.Polygons.CollectionChanged += PolysChanged;
            _frame.IsOpenChanged += OnIsOpenChanged;

            SetColor();

            MainForm.Preferences.PreferencesSaved += PreferencesOnPreferencesSaved;

            foreach (Poly p in _frame.Polygons)
                AddExpandable(new PolyStub(_frame, p, this));
        }

        private void PreferencesOnPreferencesSaved(object sender, EventArgs eventArgs)
        {
            SetColor();
        }

        public ImageFrame Frame
        {
            get { return _frame; }
        }

        public int FrameNumber
        {
            get
            {
                if (ParentSelection is ImageStub)
                {
                    ObservableCollection<ImageFrame> frames = (ParentSelection as ImageStub).Image.Frames;
                    int index = frames.IndexOf(_frame) + 1;
                    return index;
                }
                return -1;
            }
        }

        private void OnIsOpenChanged(object sender, EventArgs eventArgs)
        {
            SetContextMenu();

            SetFrameLabel();
        }

        private void Image_FramesChanged(object sender, EventArgs e)
        {
            SetFrameLabel();
        }

        private Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            if (bitmapsource != null)
            {
                Bitmap bitmap;
                using (var outStream = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                    enc.Save(outStream);
                    bitmap = new Bitmap(outStream);
                }
                return bitmap;
            }
            return null;
        }

        private void PolysChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            bool found;
            int counter;

            PolyCount.Text = "Polygons: " + _frame.Polygons.Count;

            foreach (Poly p in _frame.Polygons)
            {
                found = false;
                foreach (PolyStub stub in Expandable)
                {
                    if (stub.Poly == p)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    AddExpandable(new PolyStub(_frame, p, this));
            }

            for (counter = Expandable.Count - 1; counter >= 0; counter--)
            {
                found = false;
                foreach (Poly p in _frame.Polygons)
                {
                    if (((PolyStub) Expandable[counter]).Poly == p)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    RemoveExpandable(Expandable[counter]);
            }

            Panel.Reorder();
        }

        private void MenuAddPolyClicked(object sender, EventArgs e)
        {
            _frame.Polygons.Add(new Poly(_frame));
        }

        private void SetFrameLabel()
        {
            if (_frame.Duration > 0)
                FrameLabel.Text = "Frame " + FrameNumber + " (" + _frame.Width + ", " + _frame.Height + ", " +
                                  (_frame.IsOpen ? "Open" : "Closed") + ") - " + _frame.Duration + "ms ";
            else
            {
                FrameLabel.Text = "Frame " + FrameNumber + " (" + _frame.Width + ", " +
                                  _frame.Height + ", " + (_frame.IsOpen ? "Open" : "Closed") + ")";
            }
        }

        public void ChangeOpenClosedState()
        {
            _frame.IsOpen = !_frame.IsOpen;
        }

        private void SetContextMenu()
        {
            string label = "";

            if (_frame.IsOpen)
                label = "Mark as Closed";
            if (!_frame.IsOpen)
                label = "Mark as Open";
            ContextMenu =
                new ContextMenu(new[]
                {
                    new MenuItem("Add Poly", MenuAddPolyClicked), new MenuItem(label, MenuMarkAsOpenClicked),
                    new MenuItem("Remove Frame", MenuRemoveImage)
                });
        }

        private void MenuMarkAsOpenClicked(object sender, EventArgs e)
        {
            ChangeOpenClosedState();


            if (MainForm.Preferences.MarkAllAsOpen && _frame.IsOpen)
            {
                foreach (CustomSelection expandable in ParentSelection.Expandable)
                {
                    if (expandable is FrameStub)
                    {
                        var frameStub = expandable as FrameStub;

                        if (frameStub.FrameNumber > FrameNumber)
                            frameStub.ChangeOpenClosedState();
                    }
                }
            }

            SetFrameLabel();
        }


        private void MenuRemoveImage(object sender, EventArgs e)
        {
            Panel.ImageViewer.Visible = false;
            _frame.Polygons.Clear();
            var imageStub = ParentSelection as ImageStub;
            if (imageStub != null) imageStub.Image.Frames.Remove(_frame);
        }

        public void SetColor()
        {
            if (IsSelected)
            {
                BackColor = MainForm.Preferences.FrameStubColor;
            }
            else
            {
                BackColor = Color.FromArgb(100, MainForm.Preferences.FrameStubColor);
            }
        }

        protected override void OnSelected(object sender, EventArgs e)
        {
            base.OnSelected(sender, e);
            SetColor();
        }

        protected override void OnUnselected(object sender, EventArgs e)
        {
            base.OnUnselected(sender, e);
            SetColor();
        }

    }
}