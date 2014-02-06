using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Color = System.Drawing.Color;


namespace SpriteUtility
{
    public class XmlColor
    {
        private Color color_ = Color.Black;

        public XmlColor() { }
        public XmlColor(Color c) { color_ = c; }


        public Color ToColor()
        {
            return color_;
        }

        public void FromColor(Color c)
        {
            color_ = c;
        }

        public static implicit operator Color(XmlColor x)
        {
            return x.ToColor();
        }

        public static implicit operator XmlColor(Color c)
        {
            return new XmlColor(c);
        }

        [XmlAttribute]
        public string Web
        {
            get { return ColorTranslator.ToHtml(color_); }
            set
            {
                try
                {
                    if (Alpha == 0xFF) // preserve named color value if possible
                        color_ = ColorTranslator.FromHtml(value);
                    else
                        color_ = Color.FromArgb(Alpha, ColorTranslator.FromHtml(value));
                }
                catch (Exception)
                {
                    color_ = Color.Black;
                }
            }
        }

        [XmlAttribute]
        public byte Alpha
        {
            get { return color_.A; }
            set
            {
                if (value != color_.A) // avoid hammering named color if no alpha change
                    color_ = Color.FromArgb(value, color_);
            }
        }

        public bool ShouldSerializeAlpha() { return Alpha < 0xFF; }
    }

    public class Preferences
    {
        public event EventHandler<EventArgs> PreferencesSaved;

        public Preferences()
        {
            ViewerBackground = Color.Blue;
            CenterPointColor = Color.White;
            PolygonColor = Color.White;
            CenterLineColor = Color.Yellow;
            DrawLineArtForCenter = true;
            DrawBorder = false;
            BorderColor = Color.Black;
            ZoomIndex = 20;
            TrimToMinimalNonTransparentArea = false;
            CameraPos = new Vector2(0,0);
            TrimBorderColor = Color.Red;

            DocumentStubColor = Color.Aquamarine;
            FolderStubColor = Color.Bisque;
            ImageStubColor = Color.LightSkyBlue;
            FrameStubColor = Color.LightCyan;
            PolygonStubColor = Color.AliceBlue;    

            PreferencesSaved += OnPreferencesSaved;
        }

        private void OnPreferencesSaved(object sender, EventArgs eventArgs)
        {
            
        }

        private const string _path = "preferences.xml";


        public static Preferences LoadPreferences()
        {
            if (!File.Exists(_path))
                return new Preferences();

            FileStream stream = new FileStream(_path, FileMode.Open);
      
            XmlSerializer serializer = new XmlSerializer(typeof(Preferences));
            var preferences = (Preferences)serializer.Deserialize(stream);

            stream.Close();
            stream.Dispose();
            
            return preferences;
        }

        public bool TrimToMinimalNonTransparentArea { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color ViewerBackground { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color CenterPointColor { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color PolygonColor { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color CenterLineColor { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color DocumentStubColor { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color FolderStubColor { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color ImageStubColor { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color FrameStubColor { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color PolygonStubColor { get; set; }


        public bool DrawLineArtForCenter { get; set; }

        public bool MarkAllAsOpen { get; set; }

        public bool DrawBorder { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color BorderColor { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color TrimBorderColor { get; set; }

        public Vector2 CameraPos { get; set; }

        public int ZoomIndex { get; set; }


        public void CommitChanges()
        {
            FileStream stream = new FileStream(_path, FileMode.Create);
        
            XmlSerializer serializer = new XmlSerializer(GetType());
            serializer.Serialize(stream, this);
            stream.Close();
            stream.Dispose();

            PreferencesSaved(this, EventArgs.Empty);
        }
    }
}
