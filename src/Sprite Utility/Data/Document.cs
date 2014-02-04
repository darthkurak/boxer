using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using SpriteUtility.Data;

namespace SpriteUtility
{
    public class Document
    {
        private const byte VERSION_CODE_1 = 0x01;
        private static bool m_TriggerInvalidate;
        private readonly ObservableCollection<Folder> _folders;
        private string m_Name;

        private static Document _instance;

        public static Document Instance
        {
            get { return _instance; }
        }

        public Document()
        {
            Saved = false;
            FileName = "Not Saved";
            m_Name = "New Project";
            _folders = new ObservableCollection<Folder>();
            ImageData.ResetNames();
            m_TriggerInvalidate = true;
            DocumentChanged += OnDocumentChanged;
            DocumentSaved += OnDocumentSaved;
            NameChanged += OnNameChanged;
            _folders.CollectionChanged += FolderCollectionChanged;
        }

        [IgnoreDataMember]
        public bool Saved { get; private set; }

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                    NameChanged(this, EventArgs.Empty);
                    Invalidate(this, EventArgs.Empty);
                }
            }
        }

        public ObservableCollection<Folder> Folders
        {
            get { return _folders; }
        }

        [IgnoreDataMember]
        public string FileName { get; private set; }

        public event EventHandler<EventArgs> DocumentChanged;
        public event EventHandler<EventArgs> DocumentSaved;
        public event EventHandler<EventArgs> NameChanged;

        public void Save(bool forceNewName)
        {
            SaveFileDialog Dialog;
            DialogResult Result;
            FileStream Stream;
            BinaryWriter Writer;

            if (forceNewName || FileName == "Not Saved")
            {
                Dialog = new SaveFileDialog();
                Dialog.Filter = "Sprite Utility Files (*.suf)|*.suf";
                ImageViewer.Paused = true;
                Result = Dialog.ShowDialog();
                ImageViewer.Paused = false;
                if (Result == DialogResult.OK)
                    FileName = Dialog.FileName;
                else
                    return;
            }

            Stream = new FileStream(FileName, FileMode.Create);
            Writer = new BinaryWriter(Stream);
            Writer.Write(VERSION_CODE_1);
            Writer.Write(m_Name);

            Writer.Write(Folders.Count);
            foreach (Folder folder in Folders)
                folder.Write(Writer);

            Stream.Close();
            Saved = true;

            DocumentSaved(this, EventArgs.Empty);
        }

        public static void New()
        {
            var document = new Document();
            _instance = document;
        }

        public static void Open()
        {
            string fileName;

            var dialog = new OpenFileDialog();
            dialog.Filter = "Sprite Utility Files (*.suf)|*.suf";
            ImageViewer.Paused = true;
            DialogResult result = dialog.ShowDialog();
            ImageViewer.Paused = false;
            if (result == DialogResult.OK)
                fileName = dialog.FileName;
            else
                return;

            var stream = new FileStream(fileName, FileMode.Open);
            var reader = new BinaryReader(stream);
            byte version = reader.ReadByte();

            if (version == VERSION_CODE_1)
            {
                var newDocument = new Document {FileName = fileName};
                newDocument.Saved = true;
                newDocument.m_Name = reader.ReadString();

                int folderCount = reader.ReadInt32();
                m_TriggerInvalidate = false;

                for (int counter = 0; counter < folderCount; counter++)
                    newDocument.Folders.Add(new Folder(reader));
                m_TriggerInvalidate = true;

                stream.Close();
                _instance = newDocument;
            }
        }

        public void WriteJson(string fileName)
        {
            string json = JsonSerializer.Serialize(new DocumentExport(this));
            File.WriteAllText(fileName, json);
        }


        protected virtual void OnDocumentChanged(object sender, EventArgs e)
        {
        }

        protected virtual void OnDocumentSaved(object sender, EventArgs e)
        {
        }

        protected virtual void OnNameChanged(object sender, EventArgs e)
        {
        }

        private void FolderCollectionChanged(object sender, EventArgs e)
        {
            Invalidate(this, EventArgs.Empty);
        }

        public void Invalidate(object sender, EventArgs e)
        {
            if (m_TriggerInvalidate)
            {
                DocumentChanged(sender, e);
                Saved = false;
            }
        }
    }
}