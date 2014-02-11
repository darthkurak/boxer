using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using SpriteUtility.Data;
using SpriteUtility.Stubs;

namespace SpriteUtility
{
    public partial class ImageStub : CustomSelection
    {
        private readonly ImageData m_Image;

        public ImageStub(ImageData img, FolderStub parent) : base(parent)
        {
            InitializeComponent();
            m_Image = img;

            FileName.Text = img.Name;
            FrameCount.Text = m_Image.Frames.Count + (m_Image.Frames.Count == 1 ? " Frame" : " Frames");

            img.FileNameChanged += FileNameChanged;
            img.Frames.CollectionChanged += FramesOnCollectionChanged;

            foreach (ImageFrame frame in m_Image.Frames)
            {
                AddExpandable(new FrameStub(frame, this));
            }

            SetColor();

            MainForm.Preferences.PreferencesSaved += PreferencesOnPreferencesSaved;
            SetContextMenu();
        }

        private void PreferencesOnPreferencesSaved(object sender, EventArgs eventArgs)
        {
            SetColor();
        }

        public ImageData Image
        {
            get { return m_Image; }
        }

        private void FramesOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            bool found;
            int counter;
            FrameCount.Text = m_Image.Frames.Count + (m_Image.Frames.Count == 1 ? " Frame" : " Frames");

            foreach (ImageFrame frame in m_Image.Frames)
            {
                found = false;
                foreach (FrameStub stub in Expandable)
                {
                    if (stub.Frame == frame)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    AddExpandable(new FrameStub(frame, this));
            }

            for (counter = Expandable.Count - 1; counter >= 0; counter--)
            {
                found = false;
                foreach (ImageFrame frame in m_Image.Frames)
                {
                    if (((FrameStub) Expandable[counter]).Frame == frame)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    RemoveExpandable(Expandable[counter]);
            }
        }

        private void FileNameChanged(object sender, EventArgs e)
        {
            FileName.Text = m_Image.Name;
            Panel.Reorder();
        }

        private void SetContextMenu()
        {
            ContextMenu = new ContextMenu(new[] {new MenuItem("Remove Image", MenuRemoveImage)});
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

        private void MenuRemoveImage(object sender, EventArgs e)
        {
            Panel.ImageProperties.Visible = false;
            m_Image.Frames.Clear();
            var folderStub = ParentSelection as FolderStub;
            if (folderStub != null) folderStub.Folder.Remove(m_Image);
        }
    }
}