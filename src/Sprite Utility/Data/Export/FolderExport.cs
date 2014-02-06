using System.Collections.Generic;
using System.Linq;
using SpriteUtility.Data;

namespace SpriteUtility
{
    public class FolderExport
    {
        public string FolderName { get; set; }

        public List<ImageDataExport> Images { get; set; }

        public List<FolderExport> Folders { get; set; }

        public FolderExport(Folder folder)
        {

            FolderName = folder.Name;

            var images = folder.Children.Where(p => p is ImageData).Cast<ImageData>();
            var folders = folder.Children.Where(p => p is Folder).Cast<Folder>();

            Images = new List<ImageDataExport>();

            foreach (var image in images)
            {
                Images.Add(new ImageDataExport(image));
            }

            Folders = new List<FolderExport>();

            foreach (var fold in folders)
            {
                Folders.Add(new FolderExport(fold));
            }
        }
    }
}