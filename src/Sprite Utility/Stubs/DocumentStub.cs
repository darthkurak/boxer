using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using SpriteUtility.Data;
using SpriteUtility.Stubs;

namespace SpriteUtility
{
    public partial class DocumentStub : CustomSelection
    {
        private readonly Document _document;

        public DocumentStub(Document d)
        {
            InitializeComponent();
            _document = d;
            ProjectLabel.Text = "Project " + _document.Name + "(" + _document.Folders.Count + " folders)";
            _document.NameChanged += DocumentNameChanged;
            _document.Folders.CollectionChanged += Folders_CollectionChanged;

            SetContextMenu();

            SetColor();

            MainForm.Preferences.PreferencesSaved += PreferencesOnPreferencesSaved;
            foreach (Folder folder in d.Folders)
            {
                AddExpandable(new FolderStub(_document, folder, this));
            }
        }

        private void PreferencesOnPreferencesSaved(object sender, EventArgs eventArgs)
        {
            SetColor();
        }

        public Document Document
        {
            get { return _document; }
        }

        private void Folders_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProjectLabel.Text = "Project " + _document.Name + "(" + _document.Folders.Count + " folders)";

            bool found = false;
            int counter;

            foreach (Folder folder in _document.Folders)
            {
                found = false;
                foreach (FolderStub stub in Expandable)
                {
                    if (stub.Folder == folder)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    AddExpandable(new FolderStub(_document, folder, this));
            }

            for (counter = Expandable.Count - 1; counter >= 0; counter--)
            {
                found = false;
                foreach (Folder folder in _document.Folders)
                {
                    if (((FolderStub) Expandable[counter]).Folder == folder)
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

        private void SetContextMenu()
        {
            ContextMenu = new ContextMenu(new[] {new MenuItem("Add Folder", MenuAddFolderClicked)});
        }

        private void MenuAddFolderClicked(object sender, EventArgs e)
        {
            _document.Folders.Add(new Folder());
        }

        private void DocumentNameChanged(object sender, EventArgs e)
        {
            ProjectLabel.Text = "Project " + _document.Name;
        }

        public void SetColor()
        {
            if (IsSelected)
            {
                BackColor = MainForm.Preferences.DocumentStubColor;
            }
            else
            {
                BackColor = Color.FromArgb(100, MainForm.Preferences.DocumentStubColor);
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