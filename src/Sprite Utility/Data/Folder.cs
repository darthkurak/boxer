using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteUtility.Data
{
    public class Folder
    {
        public event EventHandler<EventArgs> NameChanged;

        public ObservableCollection<object> Childrens { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NameChanged(this, EventArgs.Empty);
                    Document.Instance.Invalidate(this, EventArgs.Empty);
                }
            }
        }

        public Folder()
        {
            Childrens = new ObservableCollection<object>();
            _name = "New Folder";
            NameChanged += OnNameChanged;
        }

        protected virtual void OnNameChanged(object sender, EventArgs e)
        {

        }

        public Folder(BinaryReader reader) : this()
        {
            int counter;

            _name = reader.ReadString();
            int childCount = reader.ReadInt32();

            for (counter = 0; counter < childCount; counter++)
            {
                var type = Type.GetType(reader.ReadString());

                if (type == typeof(Folder))
                {
                    Childrens.Add(new Folder(reader));
                }
                else if (type == typeof(ImageData))
                {
                    Childrens.Add(new ImageData(reader));
                }
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(Childrens.Count);
            foreach (object child in Childrens)
            {
                if (child is Folder)
                {
                    writer.Write(typeof(Folder).ToString());
                    (child as Folder).Write(writer);
                }
                if (child is ImageData)
                {
                    writer.Write(typeof(ImageData).ToString());
                    (child as ImageData).Write(writer);
                }
            }
        }
    }
}
