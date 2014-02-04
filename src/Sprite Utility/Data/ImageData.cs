using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace SpriteUtility
{
    public class ImageData
    {
        private static readonly List<string> FileNameList = new List<string>();

        private readonly ObservableCollection<ImageFrame> _frames;

        private ImageData()
        {
            Filename = "";
            _frames = new ObservableCollection<ImageFrame>();
            FileNameChanged += OnFileNameChanged;
        }

        public ImageData(string fileName) : this()
        {
            using (FileStream stream = File.Open(fileName, FileMode.Open))
            {
                using (Image image = Image.FromStream(stream))
                {
                    if (Path.GetExtension(fileName).Equals(".png"))
                    {
                        using (var ms = new MemoryStream())
                        {
                            image.Save(ms, ImageFormat.Png);

                            _frames.Add(new ImageFrame(ms.ToArray(), image.Width, image.Height));
                            _frames[_frames.Count - 1].CenterPointX = image.Width/2;
                            _frames[_frames.Count - 1].CenterPointY = image.Height/2;
                            _frames[_frames.Count - 1].Thumbnail = image.GetThumbnailImage(38, 38, null, new IntPtr());
                        }
                    }
                    else if (Path.GetExtension(fileName).Equals(".gif"))
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
                                _frames[i].Thumbnail = image.GetThumbnailImage(38, 38, null, new IntPtr());
                                PropertyItem item = image.GetPropertyItem(0x5100); // FrameDelay in libgdiplus

                                // Time is in 1/100th of a second, in miliseconds
                                int time = (item.Value[0] + item.Value[1]*256)*10;
                                _frames[i].Duration = time;
                            }
                        }
                    }
                }
            }

            Filename = TestName(Path.GetFileName(fileName));
            FileNameList.Add(Filename);
        }

        public ImageData(BinaryReader reader) : this()
        {
            ImageFrame frame;
            int counter, frameCount;

            Filename = reader.ReadString();
            frameCount = reader.ReadInt32();

            for (counter = 0; counter < frameCount; counter++)
            {
                frame = new ImageFrame(reader);
                _frames.Add(frame);
            }
        }

        public string Filename { get; private set; }

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

        public ObservableCollection<ImageFrame> Frames
        {
            get { return _frames; }
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


        public void Write(BinaryWriter writer)
        {
            writer.Write(Filename);
            writer.Write(_frames.Count);
            foreach (ImageFrame frame in _frames)
                frame.Write(writer);
        }

        public void ExportImage(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.OpenOrCreate);
            if (_frames.Count == 1)
            {
                stream.Write(_frames[0].Data, 0, _frames[0].Data.Length);
            }
            else
            {
                var encoder = new GifBitmapEncoder();
                foreach (ImageFrame frame in _frames)
                {
                    var decoder = new GifBitmapDecoder(new MemoryStream(frame.Data),
                        BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    BitmapSource source = decoder.Frames[0].Clone();
                    encoder.Frames.Add(BitmapFrame.Create(source));
                }
                encoder.Save(stream);
            }
            stream.Close();
        }
    }
}