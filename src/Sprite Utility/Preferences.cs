using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using SpriteUtility.Services;
using Color = System.Drawing.Color;


namespace SpriteUtility
{
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
            PolygonSelectedColor = Color.Red;
            SimulationRatio = 128f;

            PreferencesSaved += OnPreferencesSaved;
        }

        private void OnPreferencesSaved(object sender, EventArgs eventArgs)
        {
            
        }

        private const string Path = "preferences.xml";


        public static Preferences LoadPreferences()
        {
            if (!File.Exists(Path))
                return new Preferences();

            var stream = new FileStream(Path, FileMode.Open);
      
            var serializer = new XmlSerializer(typeof(Preferences));
            var preferences = (Preferences)serializer.Deserialize(stream);

            stream.Close();
            stream.Dispose();
            
            return preferences;
        }

        private float _simulatioRatio;

        public float SimulationRatio
        {
            get { return _simulatioRatio; }
            set
            {
                _simulatioRatio = value;
                TraceService.SetDisplayUnitToSimUnitRatio(_simulatioRatio);
            }
        }

        [XmlElement(Type = typeof(XmlColor))]
        public Color PolygonSelectedColor { get; set; }

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
            var stream = new FileStream(Path, FileMode.Create);
        
            var serializer = new XmlSerializer(GetType());
            serializer.Serialize(stream, this);
            stream.Close();
            stream.Dispose();

            PreferencesSaved(this, EventArgs.Empty);
        }
    }
}
