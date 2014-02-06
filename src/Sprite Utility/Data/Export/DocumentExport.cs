using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpriteUtility.Data.Export
{
    [Serializable]
    public class DocumentExport
    {
        [JsonProperty("project_name")]
        public string ProjectName { get; set; }

        [JsonProperty("folders")]
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