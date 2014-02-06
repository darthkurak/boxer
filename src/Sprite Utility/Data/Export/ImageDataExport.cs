using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpriteUtility.Data.Export
{
    [Serializable]
    public class ImageDataExport
    {
        [JsonProperty("image_file")]
        public string ImageFile { get; set; }

        [JsonProperty("frames")]
        public List<ImageFrameExport> Frames { get; set; }

        public ImageDataExport(ImageData data)
        {
            ImageFile = data.Filename;
            Frames = new List<ImageFrameExport>(data.Frames.Count);
            foreach (var frame in data.Frames)
            {
                Frames.Add(new ImageFrameExport(frame));
            }
        }
    }
}