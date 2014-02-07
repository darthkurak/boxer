using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SpriteUtility.CustomControls;
using SpriteUtility.Forms;

namespace SpriteUtility
{
    public class CustomSelectionPanel : StackPanel
    {
        private FolderProperties _folderProperties;
        private ImageProperties _imageProperties;
        private ImageViewer _imageViewer;
        private PolyForm _polyForm;
        private PreferencesForm _preferencesForm;
        private ProjectProperties _projectProperties;
        private ViewerToolStrip _viewerToolStrip;
        private Panel _contentPanel;
        private PolyGroupForm _polyGroupForm;
        private CustomSelection _selected;

        public CustomSelectionPanel()
        {
            _selected = null;
            _contentPanel = null;
            AutoScroll = true;
        }

        public PreferencesForm PreferencesForm
        {
            get { return _preferencesForm; }
        }

        public ImageProperties ImageProperties
        {
            get { return _imageProperties; }
        }

        public ImageViewer ImageViewer
        {
            get { return _imageViewer; }
        }

        public PolyForm PolyForm
        {
            get { return _polyForm; }
        }

        public Panel ContentPanel
        {
            get { return _contentPanel; }
            set { _contentPanel = value; }
        }

        public void Initialize(Panel contentPanel)
        {
            _contentPanel = contentPanel;
            _projectProperties = new ProjectProperties();
            _contentPanel.Controls.Add(_projectProperties);
            _folderProperties = new FolderProperties();
            _contentPanel.Controls.Add(_folderProperties);
            _preferencesForm = new PreferencesForm();
            _contentPanel.Controls.Add(_preferencesForm);
            _imageProperties = new ImageProperties();
            _contentPanel.Controls.Add(_imageProperties);
            _polyForm = new PolyForm();
            _polyForm.Dock = DockStyle.Top;
            _polyGroupForm = new PolyGroupForm();
            _polyGroupForm.Dock = DockStyle.Top;
            _contentPanel.Controls.Add(_polyForm);
            _contentPanel.Controls.Add(_polyGroupForm);
            HideAll();
        }

        public void Reorder()
        {
            var root = Controls[0] as CustomSelection;
            RemoveSelectable(Controls[0] as CustomSelection);
            InsertSelectable(root, 0, 0);
        }

        public void AddSelectable(CustomSelection sel)
        {
            sel.Panel = this;
            Controls.Add(sel);
            sel.Selected += ItemSelected;
        }

        public int InsertSelectable(CustomSelection sel, int index, int level)
        {
            sel.Panel = this;
            Controls.Add(sel);

            sel.Margin = new Padding(20*level, sel.Margin.Top, sel.Margin.Right, sel.Margin.Bottom);

            Controls.SetChildIndex(sel, index);
            List<CustomSelection> expandable = sel.Expandable;

            if (sel.IsExpanded && sel.Expandable.Count > 0)
            {
                var firstExpandable = sel.Expandable.First();

                if (firstExpandable is FolderStub || firstExpandable is ImageStub)
                {
                    var folders = sel.Expandable.Where(p => p is FolderStub).Cast<FolderStub>();
                    var images = sel.Expandable.Where(p => p is ImageStub).Cast<ImageStub>();

                    folders = folders.OrderBy(p => p.Folder.Name);
                    images = images.OrderBy(p => p.Image.Name);

                    expandable = new List<CustomSelection>(folders);
                    expandable.AddRange(images);
                }

                foreach (CustomSelection child in expandable)
                {
                    index = InsertSelectable(child, index + 1, level + 1);
                }

            }



            sel.Selected += ItemSelected;

            return index;
        }

        protected override void OnMouseEnter(EventArgs e) { Focus(); }

        public void RemoveSelectable(CustomSelection sel)
        {
            sel.Selected -= ItemSelected;
            foreach (CustomSelection child in sel.Expandable)
            {
                RemoveSelectable(child);
            }
            Controls.Remove(sel);
        }

        public void ClearSelection()
        {
            if (_selected != null)
                _selected.ChangeSelection(null);
        }

        public void HideAll()
        {
            foreach (Control control in _contentPanel.Controls)
            {
                control.Visible = false;
            }
        }

        private void ItemSelected(object sender, EventArgs e)
        {
            if (_selected != null)
                _selected.ChangeSelection((CustomSelection) sender);
            _selected = (CustomSelection) sender;

            if (typeof (DocumentStub) == _selected.GetType() && _contentPanel != null)
            {
                HideAll();
                _projectProperties.Document = ((DocumentStub) _selected).Document;
                _projectProperties.Visible = true;
            }
            else if (typeof (FolderStub) == _selected.GetType() && _contentPanel != null)
            {
                HideAll();
                _folderProperties.FolderDocument = ((FolderStub) _selected).Folder;
                _folderProperties.Visible = true;
            }
            else if (typeof (PreferencesStub) == _selected.GetType() && _contentPanel != null)
            {
                HideAll();
                _projectProperties.Visible = true;
            }
            else if (typeof (ImageStub) == _selected.GetType() && _contentPanel != null)
            {
                HideAll();
                _imageProperties.ImageData = ((ImageStub) _selected).Image;
                _imageProperties.Visible = true;
            }
            else if (typeof (FrameStub) == _selected.GetType() && _contentPanel != null)
            {
                HideAll();
                if (_imageViewer == null)
                {
                    _imageViewer = new ImageViewer(((FrameStub) _selected).Frame) {Dock = DockStyle.Fill};
                    _contentPanel.Controls.Add(_imageViewer);
                }
                else
                {
                    _imageViewer.Image = ((FrameStub) _selected).Frame;
                    _imageViewer.Visible = true;
                }

                if (_viewerToolStrip == null)
                {
                    _viewerToolStrip = new ViewerToolStrip(_imageViewer);
                    _viewerToolStrip.Dock = DockStyle.Left;
                    _contentPanel.Controls.Add(_viewerToolStrip);
                }
                else
                {
                    _viewerToolStrip.Visible = true;
                }
            }
            else if (_selected is PolyStub && _contentPanel != null)
            {
                HideAll();
                _imageViewer.Visible = true;
               // _imageViewer.PolygonGroup = null;
                _imageViewer.Polygon = ((PolyStub) _selected).Poly;
                _polyForm.Poly = ((PolyStub) _selected).Poly;
                _polyForm.Visible = true;
                _viewerToolStrip.Select(Mode.Polygon);
            }
            else if (_selected is PolygonGroupStub && _contentPanel != null)
            {
                HideAll();
                _imageViewer.Visible = true;
                _imageViewer.Polygon = null;
                _imageViewer.PolygonGroup = ((PolygonGroupStub)_selected).PolygonGroup;
                _polyGroupForm.Poly = ((PolygonGroupStub)_selected).PolygonGroup;
                _polyGroupForm.Visible = true;
                _viewerToolStrip.Select(Mode.Polygon);
            }
        }
    }
}