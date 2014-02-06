using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;
using SpriteUtility.Helpers;

namespace SpriteUtility
{
    [Serializable]
    public class ImageData
    {
        private static readonly List<string> FileNameList = new List<string>();
        private readonly ObservableCollection<ImageFrame> _frames;

        [JsonProperty("filename")]
        public string Filename { get; private set; }

        [JsonProperty("name")]
        public string Name
        {
            get { return Filename; }
            set
            {
                if (Filename != value)
                {
                    Filename = TestName(value);
                    FileNameList.Remove(value);
                    FileNameList.Add(Filename);
                    FileNameChanged(this, EventArgs.Empty);
                }
            }
        }

        [JsonProperty("frames")]
        public ObservableCollection<ImageFrame> Frames
        {
            get { return _frames; }
        }

        private ImageData()
        {
            Filename = "";
            _frames = new ObservableCollection<ImageFrame>();
            FileNameChanged += OnFileNameChanged;
        }

        public ImageData(string fileName) : this()
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                using (var image = Image.FromStream(stream))
                {
                    var extension = Path.GetExtension(fileName);
                    var thumbnail = image.GetThumbnailImage(38, 38, null, new IntPtr());
                    
                    if (extension != null && extension.Equals(".png"))
                    {
                        using (var ms = new MemoryStream())
                        {
                            image.Save(ms, ImageFormat.Png);

                            var frame = new ImageFrame(ms.ToArray(), image.Width, image.Height)
                            {
                                CenterPointX = image.Width/2,
                                CenterPointY = image.Height/2,
                                Thumbnail = ImageHelper.ImageToByteArray(thumbnail)
                            };

                            _frames.Add(frame);
                        }
                    }
                    else
                    {
                        extension = Path.GetExtension(fileName);
                        if (extension != null && extension.Equals(".gif"))
                        {
                            var dimension = new FrameDimension(image.FrameDimensionsList[0]);

                            // Number of frames
                            int frameCount = image.GetFrameCount(dimension);
                            // Return an Image at a certain index

                            for (int i = 0; i < frameCount; i++)
                            {
                                image.SelectActiveFrame(dimension, i);

                                using (var ms = new MemoryStream())
                                {
                                    image.Save(ms, ImageFormat.Png);

                                    _frames.Add(new ImageFrame(ms.ToArray(), image.Width, image.Height));
                                    _frames[i].CenterPointX = image.Width/2;
                                    _frames[i].CenterPointY = image.Height/2;
                                    _frames[i].Thumbnail = ImageHelper.ImageToByteArray(thumbnail);
                                    PropertyItem item = image.GetPropertyItem(0x5100); // FrameDelay in libgdiplus

                                    // Time is in 1/100th of a second, in miliseconds
                                    int time = (item.Value[0] + item.Value[1]*256)*10;
                                    _frames[i].Duration = time;
                                }
                            }
                        }
                    }
                }
            }

            Filename = TestName(Path.GetFileName(fileName));
            FileNameList.Add(Filename);
        }
        
        public event EventHandler<EventArgs> FileNameChanged;

        protected virtual void OnFileNameChanged(object sender, EventArgs e)
        {
        }

        private static string TestName(string fileName)
        {
            foreach (string name in FileNameList)
            {
                if (fileName == name)
                {
                    fileName = Path.GetFileNameWithoutExtension(fileName) + "(1)" + Path.GetExtension(fileName);
                    return TestName(fileName);
                }
            }
            return fileName;
        }

        public static void ResetNames()
        {
            FileNameList.Clear();
        }
    }
}