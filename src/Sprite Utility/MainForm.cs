using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SpriteUtility
{
    public partial class MainForm : Form
    {
        public static Preferences Preferences;

        private event EventHandler<EventArgs> DocumentStarted;
        private event EventHandler<EventArgs> DocumentClosed;

        public MainForm()
        {
            InitializeComponent();

            Preferences = Preferences.LoadPreferences();

            MainTree.Initialize(ContentPane);

            DocumentStarted += OnDocumentStarted;
            DocumentClosed += OnDocumentClosed;
            OnDocumentClosed(this, EventArgs.Empty);

            var image = (Bitmap)Image.FromFile("icon@2x.png");
            Icon = Icon.FromHandle(image.GetHicon());
        }


        private void MenuFileNew_Click(object sender, EventArgs e)
        {
            if (Document.Instance != null && !Document.Instance.Saved)
                if (!PromptSave("Would you like to save the current document before starting a new one?"))
                    return;

            if (Document.Instance != null)
                DocumentClosed(this, EventArgs.Empty);

            Document.New();
            if (Document.Instance != null)
            {
                Document.Instance.DocumentChanged += new EventHandler<EventArgs>(OnDocumentChanged);
                Document.Instance.DocumentSaved += new EventHandler<EventArgs>(OnDocumentSaved);
                RefreshList();
                DocumentStarted(this, EventArgs.Empty);
            }
        }

        private void MenuFileOpen_Click(object sender, EventArgs e)
        {
            if (Document.Instance != null && !Document.Instance.Saved)
                if (!PromptSave("Would you like to save the current document before opening another one?"))
                    return;

            if (Document.Instance != null)
                DocumentClosed(this, EventArgs.Empty);

            Document.Open();
            if (Document.Instance == null)
                return;
            Document.Instance.DocumentChanged += new EventHandler<EventArgs>(OnDocumentChanged);
            Document.Instance.DocumentSaved += new EventHandler<EventArgs>(OnDocumentSaved);
            RefreshList();
            DocumentStarted(this, EventArgs.Empty);
        }

        private void MenuFileSave_Click(object sender, EventArgs e)
        {
            if (Document.Instance != null && !Document.Instance.Saved)
                Document.Instance.Save(false);
        }

        private void MenuFileSaveAs_Click(object sender, EventArgs e)
        {
            if (Document.Instance != null)
                Document.Instance.Save(true);
        }

        private void MenuFileClose_Click(object sender, EventArgs e)
        {
            if (Document.Instance != null && !Document.Instance.Saved)
                if (!PromptSave("Would you like to save the current document before closing it?"))
                    return;

            if (Document.Instance != null)
                DocumentClosed(this, EventArgs.Empty);
        }

        private bool PromptSave(String prompt)
        {
            DialogResult Result;

            ImageViewer.Paused = true;
            Result = MessageBox.Show(prompt, "Save File", MessageBoxButtons.YesNoCancel);
            ImageViewer.Paused = false;
            if (Result == DialogResult.Cancel)
                return false;
            else
            {
                if(Result == DialogResult.Yes)
                    Document.Instance.Save(false);
                return true;
            }
        }

        private void OnDocumentStarted(object sender, EventArgs e)
        {
            MenuFileSave.Enabled = true;
            MenuFileSaveAs.Enabled = true;
            MenuFileClose.Enabled = true;
            MenuFileExport.Enabled = true;
            if (Document.Instance.Saved)
                Text = "Sprite Utility [" + Path.GetFileName(Document.Instance.FileName) + "]";
            else
                Text = "Sprite Utility [" + Path.GetFileName(Document.Instance.FileName) + "*]";
        }

        private void OnDocumentClosed(object sender, EventArgs e)
        {
            MenuFileSave.Enabled = false;
            MenuFileSaveAs.Enabled = false;
            MenuFileClose.Enabled = false;
            MenuFileExport.Enabled = false;
            Text = "Sprite Utility";

            MainTree.Controls.Clear();
          //  ContentPane.Controls.Clear();
          //  MainTree.Initialize(ContentPane);
        }

        void OnDocumentChanged(object sender, EventArgs e)
        {
            Text = "Sprite Utility [" + Path.GetFileName(Document.Instance.FileName) + "*]";
        }

        void OnDocumentSaved(object sender, EventArgs e)
        {
            Text = "Sprite Utility [" + Path.GetFileName(Document.Instance.FileName) + "]";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Document.Instance != null && !Document.Instance.Saved)
                if (!PromptSave("Would you like to save this document before exiting?"))
                    e.Cancel = true;
        }

        private void RefreshList()
        {
            if (Document.Instance != null)
            {
                MainTree.AddSelectable(new DocumentStub(Document.Instance));
            }
        }

        private void MenuFileExport_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog {Filter = "JSON (*.json)|*.json"};
            ImageViewer.Paused = true;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Document.Instance.WriteJson(dialog.FileName);
            }
            ImageViewer.Paused = false;
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTree.ClearSelection();
            MainTree.HideAll();
            MainTree.PreferencesForm.Visible = true;
        }
    }
}