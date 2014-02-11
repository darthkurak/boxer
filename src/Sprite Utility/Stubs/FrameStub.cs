using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using FarseerPhysics;
using SpriteUtility.Data;
using SpriteUtility.Forms;
using SpriteUtility.Helpers;
using SpriteUtility.Services;

namespace SpriteUtility.Stubs
{
    public partial class FrameStub : CustomSelection
    {
        private readonly ImageFrame _frame;

        public FrameStub(ImageFrame frame, ImageStub parent) : base(parent)
        {
            InitializeComponent();
            _frame = frame;
            SetFrameLabel();
            PolyCount.Text = "Polygon Groups: " + _frame.PolygonGroups.Count;
            SetContextMenu();
            Thumbnail.BackgroundImage = ImageHelper.ByteArrayToImage(_frame.Thumbnail);
            parent.Image.Frames.CollectionChanged += Image_FramesChanged;
            _frame.PolygonGroups.CollectionChanged += PolyGroupsChanged;
            _frame.IsOpenChanged += OnIsOpenChanged;

            SetColor();

            MainForm.Preferences.PreferencesSaved += PreferencesOnPreferencesSaved;

            foreach (PolygonGroup p in _frame.PolygonGroups)
                AddExpandable(new PolygonGroupStub(p, this));
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

        private void PolyGroupsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            bool found;
            int counter;

            PolyCount.Text = "Polygons Groups: " + _frame.PolygonGroups.Count;

            foreach (PolygonGroup p in _frame.PolygonGroups)
            {
                found = false;
                foreach (PolygonGroupStub stub in Expandable)
                {
                    if (stub.PolygonGroup == p)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    AddExpandable(new PolygonGroupStub(p, this));
            }

            for (counter = Expandable.Count - 1; counter >= 0; counter--)
            {
                found = false;
                foreach (PolygonGroup p in _frame.PolygonGroups)
                {
                    if (((PolygonGroupStub)Expandable[counter]).PolygonGroup == p)
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

        private void MenuAddPolyGroupClicked(object sender, EventArgs e)
        {
            var poly = new PolygonGroup(_frame);
            _frame.PolygonGroups.Add(poly);
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
                    new MenuItem("Add Polygon Group", MenuAddPolyGroupClicked), new MenuItem(label, MenuMarkAsOpenClicked),
                    new MenuItem("Auto-Trace", MenuAutoTraceClicked),
                    new MenuItem("Remove Frame", MenuRemoveImage)
                });
        }

        private void MenuAutoTraceClicked(object sender, EventArgs e)
        {
            var autoTraceForm = new AutoTraceForm();
            autoTraceForm.ShowDialog();
            AddTracePolygonGroup();
        }

        private void AddTracePolygonGroup()
        {
            using (var ms = new MemoryStream(_frame.Data))
            {
                var imageBitmap = Image.FromStream(ms);
                var errorBuilder = new StringBuilder();
                var shape = TraceService.CreateComplexShape(imageBitmap, 20000, errorBuilder,
                    AutoTraceForm.AutoTraceFormResult.HullTolerance,
                    AutoTraceForm.AutoTraceFormResult.AlphaTolerance,
                    AutoTraceForm.AutoTraceFormResult.MultiPartDetection,
                    AutoTraceForm.AutoTraceFormResult.HoleDetection);

                if (shape != null)
                {
                    var polygonGroup = new PolygonGroup(_frame);
                    var count = 1;
                    foreach (var polygon in shape.Vertices)
                    {
                        var poly = new Polygon(polygonGroup) {Name = "Polygon " + count};

                        foreach (var point in polygon)
                        {
                            var x = (int) ConvertUnits.ToDisplayUnits(point.X);
                            var y = (int) ConvertUnits.ToDisplayUnits(point.Y);

                            x += (int) (_frame.Width*0.5f);
                            y += (int) (_frame.Height*0.5f);

                            poly.Points.Add(new PolyPoint(x, y, poly));
                        }

                        polygonGroup.Polygons.Add(poly);
                        count++;
                    }

                    _frame.PolygonGroups.Add(polygonGroup);
                }

                ms.Close();
            }
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
            _frame.PolygonGroups.Clear();
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