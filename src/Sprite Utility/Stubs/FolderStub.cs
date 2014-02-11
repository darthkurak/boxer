using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using fwd;
using Newtonsoft.Json.Schema;
using SpriteUtility.Data;
using SpriteUtility.Services;

namespace SpriteUtility
{
    public partial class FolderStub : CustomSelection
    {
        private readonly Document _document;
        private readonly Folder _folder;

        public FolderStub(Document document, Folder folder, CustomSelection parent)
            : base(parent)
        {
            InitializeComponent();
            _folder = folder;
            _document = document;
            FolderLabel.Text = _folder.Name + " (" + _folder.Children.Count + ")";
            _folder.NameChanged += FolderNameChanged;
            _folder.Children.CollectionChanged += ChildrenCountChanged;

            SetColor();

            MainForm.Preferences.PreferencesSaved += PreferencesOnPreferencesSaved;
            foreach (var child in _folder.Children)
            {
                if (child is Folder)
                {
                    var folderStub = new FolderStub(_document, child as Folder, this);
                    AddExpandable(folderStub);
                }
                else if (child is ImageData)
                {
                    AddExpandable(new ImageStub(child as ImageData, this));
                }
            }

            SetContextMenu();
        }

        private void PreferencesOnPreferencesSaved(object sender, EventArgs eventArgs)
        {
            SetColor();
        }

        public Folder Folder
        {
            get { return _folder; }
        }

        private void FolderNameChanged(object sender, EventArgs e)
        {
            FolderLabel.Text = _folder.Name + " (" + _folder.Children.Count + ")";

            Panel.Reorder();
        }

        private void SetContextMenu()
        {
            ContextMenu = new ContextMenu(new[]
            {
                new MenuItem("Add Folder", MenuAddFolderClicked),
                new MenuItem("Remove Folder", MenuRemoveFolderClicked),
                new MenuItem("Add Image", MenuAddImageClicked)
            });
        }

        private void MenuAddFolderClicked(object sender, EventArgs e)
        {
            _folder.Add(new Folder());
        }

        private void MenuRemoveFolderClicked(object sender, EventArgs e)
        {
            if (ParentSelection is DocumentStub)
            {
                (ParentSelection as DocumentStub).Document.Folders.Remove(_folder);
            }
            else if (ParentSelection is FolderStub)
            {
                (ParentSelection as FolderStub).Folder.Remove(_folder);
            }
        }

        private void MenuAddImageClicked(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog {Filter = "Image Files (*.png, *.gif)|*.png;*.gif", Multiselect = true};
            ImageViewer.Paused = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var filename in dialog.FileNames)
                {
                    var imageData = new ImageData(filename);

                    // Since we are adding new images we can stub in some
                    //    conventional defaults (currently for trimmed frames only)

                    if (MainForm.Preferences.TrimToMinimalNonTransparentArea)
                    {
                        foreach (var frame in imageData.Frames)
                        {
                            var polyGroup = new PolygonGroup(frame);

                            frame.PolygonGroups.Add(polyGroup);

                            // Add an attack box stub
                            var attack = new Polygon(polyGroup);
                            attack.Name = "Attack";
                            polyGroup.Polygons.Add(attack);
                            
                            // Add a default foot box
                            var foot = new Polygon(polyGroup);
                            foot.Name = "Foot";

                            var bottom = frame.TrimRectangle.Bottom;
                            var left = frame.TrimRectangle.Left;
                            var top = frame.TrimRectangle.Top;
                            var right = frame.TrimRectangle.Right;
                            var width = frame.TrimRectangle.Width;
                            var height = frame.TrimRectangle.Height;

                            var tl = new PolyPoint(left + (int)(width * 0.25f), bottom - 2, foot);
                            var tr = new PolyPoint(right - (int)(width * 0.25f), bottom - 2, foot);
                            var br = new PolyPoint(right - (int)(width * 0.25f), bottom, foot);
                            var bl = new PolyPoint(left + (int)(width * 0.25f), bottom, foot);
                            foot.Points.Add(tl);
                            foot.Points.Add(tr);
                            foot.Points.Add(br);
                            foot.Points.Add(bl);

                            polyGroup.Polygons.Add(foot);

                            // Set the center to best effort with no half-pixels
                            frame.CenterPointX = left + (int)(width * 0.5f);
                            frame.CenterPointY = top + (int)(height * 0.5f);
                        }   
                    }

                    _folder.Add(imageData);
                }
            }
            ImageViewer.Paused = false;
        }

        private void ChildrenCountChanged(object sender, EventArgs e)
        {
            FolderLabel.Text = _folder.Name + " (" + _folder.Children.Count + ")";

            bool found = false;
            int counter;

            foreach (object child in _folder.Children)
            {
                if (child is Folder)
                {
                    found = false;
                    foreach (CustomSelection stub in Expandable)
                    {
                        if (stub is FolderStub)
                        {
                            if ((stub as FolderStub).Folder == child)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (!found)
                        AddExpandable(new FolderStub(_document, child as Folder, this));
                }
                else if (child is ImageData)
                {
                    found = false;
                    foreach (CustomSelection stub in Expandable)
                    {
                        if (stub is ImageStub)
                        {
                            if ((stub as ImageStub).Image == child)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (!found)
                        AddExpandable(new ImageStub(child as ImageData, this));
                }
            }

            for (counter = Expandable.Count - 1; counter >= 0; counter--)
            {
                found = false;

                foreach (object child in _folder.Children)
                {
                    if (child is Folder && Expandable[counter] is FolderStub &&
                        ((FolderStub) Expandable[counter]).Folder == child)
                    {
                        found = true;
                        break;
                    }
                    if (child is ImageData && Expandable[counter] is ImageStub &&
                        ((ImageStub) Expandable[counter]).Image == child)
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

        public void SetColor()
        {
            if (IsSelected)
            {
                BackColor = MainForm.Preferences.FolderStubColor;
            }
            else
            {
                BackColor = Color.FromArgb(100, MainForm.Preferences.FolderStubColor);
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