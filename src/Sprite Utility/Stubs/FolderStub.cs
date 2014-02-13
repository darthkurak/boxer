using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarseerPhysics;
using SpriteUtility.Data;
using SpriteUtility.Services;

namespace SpriteUtility.Stubs
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
                new MenuItem("Add Image", MenuAddImageClicked),
                new MenuItem("Add From Existing Folder", MenuAddExistingFolderClicked)
            });
        }

        private void MenuAddExistingFolderClicked(object sender, EventArgs e)
        {
            ImageDataFactory.ImportFromExistingDirectoryDialog(_folder);
        }

        private void MenuAddFolderClicked(object sender, EventArgs e)
        {
            _folder.Add(new Folder());
        }

        private void MenuRemoveFolderClicked(object sender, EventArgs e)
        {
            if (ParentSelection is ProjectStub)
            {
                (ParentSelection as ProjectStub).Document.Folders.Remove(_folder);
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
                    var imageData = ImageDataFactory.CreateFromFilename(filename);

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