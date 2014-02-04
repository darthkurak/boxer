using System.Collections.Generic;

namespace SpriteUtility
{
    public class ImageDataExport
    {
        public string ImageFile { get; set; }
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