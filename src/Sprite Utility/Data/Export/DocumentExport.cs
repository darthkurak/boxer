using System.Collections.Generic;

namespace SpriteUtility
{
    public class DocumentExport
    {
        public string ProjectName { get; set; }
        public List<FolderExport> Folders { get; set; }

        public DocumentExport(Document document)
        {
            ProjectName = document.Name;
            Folders = new List<FolderExport>();
            foreach (var folder in document.Folders)
            {
                Folders.Add(new FolderExport(folder));
            }
        }
    }
}