using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using SpriteUtility.Data;

namespace SpriteUtility
{
    public partial class PolygonGroupStub : CustomSelection
    {
        private readonly PolygonGroup _polygonGroup;

        public PolygonGroupStub(PolygonGroup polyGroup, FrameStub parent)
            : base(parent)
        {
            InitializeComponent();
            _polygonGroup = polyGroup;

            FileName.Text = polyGroup.Name;
            FrameCount.Text = _polygonGroup.Polygons.Count + (_polygonGroup.Polygons.Count == 1 ? " Polygon" : " Polygons");

            polyGroup.NameChanged += NameChanged;
            polyGroup.Polygons.CollectionChanged += PolygonsOnCollectionChanged;

            foreach (Polygon poly in _polygonGroup.Polygons)
            {
                AddExpandable(new PolyStub(_polygonGroup, poly, this));
            }

            SetColor();

            MainForm.Preferences.PreferencesSaved += PreferencesOnPreferencesSaved;
            SetContextMenu();
        }

        private void PreferencesOnPreferencesSaved(object sender, EventArgs eventArgs)
        {
            SetColor();
        }

        public PolygonGroup PolygonGroup
        {
            get { return _polygonGroup; }
        }

        private void PolygonsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            bool found;
            int counter;
            FrameCount.Text = _polygonGroup.Polygons.Count + (_polygonGroup.Polygons.Count == 1 ? " Polygon" : " Polygons");

            foreach (Polygon polygon in _polygonGroup.Polygons)
            {
                found = false;
                foreach (PolyStub stub in Expandable)
                {
                    if (stub.Poly == polygon)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    AddExpandable(new PolyStub(_polygonGroup, polygon, this));
            }

            for (counter = Expandable.Count - 1; counter >= 0; counter--)
            {
                found = false;
                foreach (Polygon polygon in _polygonGroup.Polygons)
                {
                    if (((PolyStub) Expandable[counter]).Poly == polygon)
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

        private void NameChanged(object sender, EventArgs e)
        {
            FileName.Text = _polygonGroup.Name;
           // Panel.Reorder();
        }

        private void SetContextMenu()
        {
            ContextMenu = new ContextMenu(new[]
            {
                new MenuItem("Remove Polygon Group", MenuRemovePolygonGroup),
                new MenuItem("Add Polygon", MenuAddPoly), 
            });
        }


        public void SetColor()
        {
            if (IsSelected)
            {
                BackColor = MainForm.Preferences.ImageStubColor;
            }
            else
            {
                BackColor = Color.FromArgb(100, MainForm.Preferences.ImageStubColor);
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

        private void MenuRemovePolygonGroup(object sender, EventArgs e)
        {
            Panel.ImageProperties.Visible = false;
            _polygonGroup.Polygons.Clear();
            var folderStub = ParentSelection as FrameStub;
            if (folderStub != null) folderStub.Frame.PolygonGroups.Remove(_polygonGroup);
        }

        private void MenuAddPoly(object sender, EventArgs e)
        {
            _polygonGroup.Polygons.Add(new Polygon(_polygonGroup));
        }
    }
}