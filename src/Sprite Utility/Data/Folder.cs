using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace SpriteUtility.Data
{
    [Serializable]
    public class Folder
    {
        private string _name;

        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                if (NameChanged != null)
                {
                    NameChanged(this, EventArgs.Empty);
                }
                Document.Instance.Invalidate(this, EventArgs.Empty);
            }
        }

        public event EventHandler<EventArgs> NameChanged;

        [JsonIgnore]
        internal readonly ObservableCollection<object> Children;

        [JsonProperty("folders")]
        public List<Folder> Folders { get; set; }

        [JsonProperty("images")]
        public List<ImageData> Images { get; set; }

        public Folder()
        {
            Children = new ObservableCollection<object>();
            Folders = new List<Folder>();
            Images = new List<ImageData>();

            _name = "New Folder";
        }

        public void Add(Folder folder)
        {
            Folders.Add(folder);
            Children.Add(folder);
        }

        public void Remove(Folder folder)
        {
            Folders.Remove(folder);
            Children.Remove(folder);
        }

        public void Add(ImageData image)
        {
            Images.Add(image);
            Children.Add(image);
        }

        public void Remove(ImageData image)
        {
            Images.Remove(image);
            Children.Remove(image);
        }
    }
}
