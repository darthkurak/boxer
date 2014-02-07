using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpriteUtility.Data;

namespace SpriteUtility
{
    public sealed partial class PolyStub : CustomSelection
    {
        private PolygonGroup _polygonGroup;
        private Polygon _poly;

        public PolyStub(PolygonGroup polygonGroup, Polygon poly, CustomSelection parent) : base(parent)
        {
            InitializeComponent();

            _polygonGroup = polygonGroup;
            _poly = poly;
            PolyName.Text = _poly.Name + " (" + _poly.Points.Count.ToString() + " Points)";

            _poly.NameChanged += PolyOnNameChanged;
            _poly.Points.CollectionChanged += PointsOnCollectionChanged;

            SetColor();

            MainForm.Preferences.PreferencesSaved += PreferencesOnPreferencesSaved;

            ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Delete Polygon", new EventHandler(MenuPolyDeleteClick))
            });
        }

        private void PreferencesOnPreferencesSaved(object sender, EventArgs eventArgs)
        {
            SetColor();
        }

        private void PointsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            UpdateLabel();
        }

        private void PolyOnNameChanged(object sender, EventArgs eventArgs)
        {
            UpdateLabel();
        }

        void UpdateLabel()
        {
            PolyName.Text = _poly.Name + " (" + _poly.Points.Count.ToString() + " Points)";
        }

        public Polygon Poly
        {
            get { return _poly; }
        }

        public PolygonGroup PolygonGroup
        {
            get { return _polygonGroup; }
        }

        private void MenuPolyDeleteClick(object sender, EventArgs e)
        {
            PolygonGroup.Polygons.Remove(_poly);
            _poly = null;

            Panel.ImageViewer.RedrawAfterDeletePolygon();
            Panel.PolyForm.Visible = false;
        }

        public void SetColor()
        {
            if (IsSelected)
            {
                BackColor = MainForm.Preferences.PolygonStubColor;
            }
            else
            {
                BackColor = Color.FromArgb(100, MainForm.Preferences.PolygonStubColor);
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
