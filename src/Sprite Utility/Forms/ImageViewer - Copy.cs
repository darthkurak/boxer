using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WinFormsGraphicsDevice;
using Keys = System.Windows.Forms.Keys;

namespace SpriteUtility
{
    //public enum Mode
    //{
    //    None, Pan, ZoomIn, ZoomOut, Center, Draw, PolyMove
    //}

    public enum Mode
    {
        Center, Polygon
    }

    public class ImageViewer : GraphicsDeviceControl
    {
        private float[] m_Zoom = null;

        private static bool m_Paused;
        private static ImageViewer m_Instance;

        public event EventHandler<EventArgs> ModeChanged;

        private Document m_Document;
        private ImageFrame m_Image;
        private Texture2D m_Texture, m_Center, m_Point;
        private SpriteBatch m_Sprite;
        private Rectangle m_Rect, m_CenterPoint;
        private Mode m_Mode;
        private int m_CurrentX, m_CurrentY;
        private bool m_SpaceKey, m_CtrlKey, m_AltKey;
        private Color m_BackgroundColor, m_CenterPointColor;
        private Cursor m_ArrowNormCursor, m_ArrowOverCursor, m_PenCursor, m_PenOverCursor;
        private Poly m_Poly;
        private Timer m_RefreshTimer;
        private PolyPoint m_Moving;
        private bool m_CenterMoving;
        private ContextMenu m_PolygonPointMenu;

        public ImageFrame Image { get { return m_Image; } }

        public ImageViewer(Document doc, ImageFrame Image)
        {
            m_Instance = this;

            m_Zoom = new float[] {0.001f, 0.002f, 0.003f, 0.004f, 0.005f, 0.007f, 0.01f, 0.015f, 0.02f, 0.03f, 0.04f, 0.05f, 0.0625f, 0.0833f, 0.125f, 0.1667f, 0.25f, 0.3333f, 0.5f, 0.6667f, 1, 2, 3, 4, 5, 6, 7, 8, 12, 16, 32};

            m_Document = doc;
            m_Image = Image;
            m_Mode = Mode.Center;
            m_SpaceKey = m_CtrlKey = false;
            m_BackgroundColor = MainForm.Preferences.ViewerBackground;
            m_CenterPointColor = MainForm.Preferences.CenterPointColor;
            m_Paused = false;
            m_Poly = null;
            m_Moving = null;

            m_ArrowNormCursor = new Cursor("ArrowNorm.cur");
            m_ArrowOverCursor = new Cursor("ArrowOver.cur");
            m_PenCursor = new Cursor("Pen.cur");
            m_PenOverCursor = new Cursor("PenOver.cur");
       
            m_RefreshTimer = new Timer();
            m_RefreshTimer.Interval = (int)(1000 * 1 / 30.0f);
            m_RefreshTimer.Tick += new EventHandler(Refresh);
            m_RefreshTimer.Start();

        }

        protected override void Initialize()
        {
            FileStream centerStream, pointStream;
            MemoryStream imageData;

            imageData = new MemoryStream(m_Image.Data);
            m_Texture = Texture2D.FromStream(GraphicsDevice, imageData);
            imageData.Close();

            centerStream = new FileStream("Center.png", FileMode.Open);
            m_Center = Texture2D.FromStream(GraphicsDevice, centerStream);
            centerStream.Close();

            pointStream = new FileStream("Point.png", FileMode.Open);
            m_Point = Texture2D.FromStream(GraphicsDevice, pointStream);
            pointStream.Close();

            m_Sprite = new SpriteBatch(GraphicsDevice);
            m_Rect = new Rectangle(m_Image.Pan.X, m_Image.Pan.Y, (int)(m_Texture.Width * m_Zoom[m_Image.Zoom]), (int)(m_Texture.Height * m_Zoom[m_Image.Zoom]));
            m_CenterPoint = new Rectangle((int)(m_Rect.Left - 19 / 2.0f + m_Image.CenterPointX * m_Zoom[m_Image.Zoom]), (int)(m_Rect.Top - 19 / 2.0f + m_Image.CenterPointY * m_Zoom[m_Image.Zoom]), 19, 19);
        }

        private void Refresh(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(!m_Paused)
                base.OnPaint(e);
        }

        public void RedrawAfterDeletePolygon()
        {
            m_Poly = null;
            Draw();
        }
        protected override void Draw()
        {
            Rectangle pointRect;
            int counter;

            GraphicsDevice.Clear(m_BackgroundColor);

            if (MainForm.Preferences.DrawBorder)
            {
                VertexPositionColor[] verts = new VertexPositionColor[5];
                verts[0].Color = verts[1].Color = verts[2].Color = verts[3].Color = verts[4].Color = MainForm.Preferences.BorderColor;
                verts[0].Position = verts[4].Position = new Vector3(m_Rect.Left - 1, m_Rect.Top - 1, 0);
                verts[1].Position = new Vector3(m_Rect.Right, m_Rect.Top - 1, 0);
                verts[2].Position = new Vector3(m_Rect.Right, m_Rect.Bottom, 0);
                verts[3].Position = new Vector3(m_Rect.Left - 1, m_Rect.Bottom, 0);

                BasicEffect effect = new BasicEffect(GraphicsDevice);
                effect.VertexColorEnabled = true;
                Matrix proj = Matrix.CreateOrthographicOffCenter(0, Width, Height, 0, -10, 10);
                effect.Projection = proj;
                Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, -10), new Vector3(0, 1, 0));
                effect.View = view;
                Matrix world = Matrix.Identity;
                effect.World = world;

                effect.Techniques[0].Passes[0].Apply();

                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, verts, 0, verts.Length - 1);
            }

            m_Sprite.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);


            //m_Sprite.Draw(m_Texture, new Vector2(m_Image.Pan.X, m_Image.Pan.Y), null, Color.White, 0, new Vector2(m_Texture.Width / 2, m_Texture.Height / 2),
            //    new Vector2(m_Zoom[m_Image.Zoom], m_Zoom[m_Image.Zoom]), SpriteEffects.None, 0);
            m_Sprite.Draw(m_Texture, m_Rect, Color.White);

            if (MainForm.Preferences.DrawLineArtForCenter)
            {
                m_CenterPoint.Location = new Point((int)(m_Rect.Left - 19 / 2.0f + (m_Image.CenterPointX + 0.5) * m_Zoom[m_Image.Zoom]), (int)(m_Rect.Top - 19 / 2.0f + (m_Image.CenterPointY + 0.5) * m_Zoom[m_Image.Zoom]));
                m_CenterPoint.Width = 19;
                m_CenterPoint.Height = 19;
                m_Sprite.Draw(m_Center, m_CenterPoint, MainForm.Preferences.CenterPointColor);
            }
            else
            {
                m_CenterPoint.Location = new Point((int)(m_Rect.Left - Math.Max(19, 19 * m_Zoom[m_Image.Zoom]) / 2.0f + (m_Image.CenterPointX + 0.5) * m_Zoom[m_Image.Zoom]), (int)(m_Rect.Top - Math.Max(19, 19 * m_Zoom[m_Image.Zoom]) / 2.0f + (m_Image.CenterPointY + 0.5) * m_Zoom[m_Image.Zoom]));
                m_CenterPoint.Width = (int)Math.Max(19, 19 * m_Zoom[m_Image.Zoom]);
                m_CenterPoint.Height = (int)Math.Max(19, 19 * m_Zoom[m_Image.Zoom]);
                m_Sprite.Draw(m_Center, m_CenterPoint, MainForm.Preferences.CenterPointColor);
            }

            if (m_Poly != null)
            {
                foreach (PolyPoint p in m_Poly.Points)
                {
                    pointRect = new Rectangle((int)(m_Rect.Left - 9 / 2.0f + p.X * m_Zoom[m_Image.Zoom]), (int)(m_Rect.Top - 9 / 2.0f + p.Y * m_Zoom[m_Image.Zoom]), 9, 9);
                    m_Sprite.Draw(m_Point, pointRect, MainForm.Preferences.PolygonColor);
                }
            }

            m_Sprite.End();

            if (m_Poly != null && m_Poly.Points.Count >= 2)
            {
                VertexPositionColor[] verts = new VertexPositionColor[m_Poly.Points.Count + 1];
                for (counter = 0; counter < verts.Length - 1; counter++)
                {
                    verts[counter].Position = new Vector3(m_Poly.Points[counter].X, m_Poly.Points[counter].Y, 0);
                    verts[counter].Color = MainForm.Preferences.PolygonColor;
                }
                verts[counter] = verts[0];

                BasicEffect effect = new BasicEffect(GraphicsDevice);
                effect.VertexColorEnabled = true;
                Matrix proj = Matrix.CreateOrthographicOffCenter(0, Width, Height, 0, -10, 10);
                effect.Projection = proj;
                Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, -10), new Vector3(0, 1, 0));
                effect.View = view;
                Matrix world = Matrix.CreateTranslation(new Vector3(m_Rect.X, m_Rect.Y, 0));
                world = Matrix.Multiply(Matrix.CreateScale(m_Zoom[m_Image.Zoom]), world);
                effect.World = world;

                effect.Techniques[0].Passes[0].Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, verts, 0, verts.Length - 1);
            }
        }

        public static bool Paused
        {
            get { return m_Paused; }
            set 
            { 
                m_Paused = value;
                if(!m_Paused && m_Instance != null)
                    m_Instance.Invalidate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            m_Texture.Dispose();
            m_Center.Dispose();
            base.Dispose(disposing);
        }

        public Mode Mode
        {
            get { return m_Mode; }
            set 
            {
                m_Mode = value;
            }
        }

        public Poly Polygon
        {
            get { return m_Poly; }
            set { m_Poly = value; }
        }


        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0)
                    DoZoom(1, new Point(e.X, e.Y));
                else if (e.Delta < 0)
                {
                    DoZoom(-1, new Point(e.X, e.Y));
                }
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                m_Image.Pan = new Point(m_Image.Pan.X, m_Image.Pan.Y - (int)Math.Round(m_Zoom[m_Image.Zoom]*1));
            if (e.KeyCode == Keys.Up)
                m_Image.Pan = new Point(m_Image.Pan.X, m_Image.Pan.Y + (int)Math.Round(m_Zoom[m_Image.Zoom] * 1));
            if (e.KeyCode == Keys.Right)
                m_Image.Pan = new Point(m_Image.Pan.X - (int)Math.Round(m_Zoom[m_Image.Zoom] * 1), m_Image.Pan.Y);
            if (e.KeyCode == Keys.Left)
                m_Image.Pan = new Point(m_Image.Pan.X + (int)Math.Round(m_Zoom[m_Image.Zoom] * 1), m_Image.Pan.Y);

            m_Rect.Location = m_Image.Pan;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                m_SpaceKey = true;
            else if (e.KeyCode == Keys.ControlKey)
                m_CtrlKey = true;
            else if (e.KeyCode == Keys.Menu)
                m_AltKey = true;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                m_Image.Polygons.Remove(m_Poly);
                m_Poly = null;
                Draw();
            }
            if (e.KeyCode == Keys.C)
            {
                m_Image.CenterPointX = (PointToClient(MousePosition).X - m_Rect.Left) / m_Zoom[m_Image.Zoom];
                m_Image.CenterPointX = (int)(2 * m_Image.CenterPointX + 0.5) / 2.0f;
                m_Image.CenterPointY = (PointToClient(MousePosition).Y - m_Rect.Top) / m_Zoom[m_Image.Zoom];
                m_Image.CenterPointY = (int)(2 * m_Image.CenterPointY + 0.5) / 2.0f;
                m_Image.UpdateCenterPoint();
            }

            if (e.KeyCode == Keys.Space)
                m_SpaceKey = false;
            else if (e.KeyCode == Keys.ControlKey)
                m_CtrlKey = false;
            else if (e.KeyCode == Keys.Menu)
                m_AltKey = false;

            if (m_CtrlKey && e.KeyCode == Keys.Add)
                DoZoom(1);

            if (m_CtrlKey && e.KeyCode == Keys.Subtract)
                DoZoom(-1);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Focus();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            float currentZoom = m_Zoom[m_Image.Zoom];

            if(e.Button == MouseButtons.Right)
            {
                m_CurrentX = e.X;
                m_CurrentY = e.Y;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (m_Mode == Mode.Center)
                {
                    m_CenterMoving = true;
                    m_Image.CenterPointX = (int) ((e.X - m_Rect.Left)/currentZoom);
                    m_Image.CenterPointY = (int) ((e.Y - m_Rect.Top)/currentZoom);
                }
                else if (m_Poly != null && m_Mode == Mode.Polygon && m_Moving == null)
                {
                    //check if selecting existing point
                    foreach (PolyPoint p in m_Poly.Points)
                    {
                        if (e.X >= m_Rect.Left + p.X*m_Zoom[m_Image.Zoom] - 9/2.0 &&
                            e.X <= m_Rect.Left + p.X*m_Zoom[m_Image.Zoom] + 9/2.0 &&
                            e.Y >= m_Rect.Top + p.Y*m_Zoom[m_Image.Zoom] - 9/2.0 &&
                            e.Y <= m_Rect.Top + p.Y*m_Zoom[m_Image.Zoom] + 9/2.0)
                        {
                            m_Moving = p;
                        }
                    }

                    //if not add point
                    if (m_Moving == null)
                    {
                        int x, y;

                        x = (int) (e.X/currentZoom - m_Image.Pan.X/currentZoom);
                        y = (int) (e.Y/currentZoom - m_Image.Pan.Y/currentZoom);

                        var p = new PolyPoint(x, y, m_Document.Invalidate);
                        m_Poly.Points.Add(p);

                        m_Moving = p;
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (m_Mode == Mode.Center)
            {
                m_Image.UpdateCenterPoint();
                m_CenterMoving = false;
            }
            else if (m_Mode == Mode.Polygon)
            {
                m_Moving = null;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool overCenter;
            bool overPoly;

            if(e.Button == MouseButtons.Right)
            {
                m_Image.Pan = new Point(m_Image.Pan.X + e.X - m_CurrentX, m_Image.Pan.Y + e.Y - m_CurrentY);
                m_Rect.Location = m_Image.Pan;
                m_CurrentX = e.X;
                m_CurrentY = e.Y;
            }

            if (m_Mode == Mode.Center && m_CenterMoving)
            {
                m_Image.CenterPointX = (int)((e.X - m_Rect.Left) / m_Zoom[m_Image.Zoom]);
                m_Image.CenterPointY = (int)((e.Y - m_Rect.Top) / m_Zoom[m_Image.Zoom]);
            }
            else if (m_Mode == Mode.Polygon && m_Moving != null)
            {
                m_Moving.X = (int)((e.X - m_Rect.Left) / m_Zoom[m_Image.Zoom]);
                m_Moving.Y = (int)((e.Y - m_Rect.Top) / m_Zoom[m_Image.Zoom]);
            }

            overCenter = false;
            overPoly = false;
            if ((m_Mode == Mode.Center || m_Mode == Mode.Polygon) && e.X >= m_CenterPoint.Left && e.X <= m_CenterPoint.Right && e.Y >= m_CenterPoint.Top && e.Y <= m_CenterPoint.Bottom)
            {
                overCenter = true;
            }
            else if (m_Poly != null && (m_Mode == Mode.Polygon))
            {
                foreach (PolyPoint p in m_Poly.Points)
                {
                    if (e.X >= m_Rect.Left + p.X * m_Zoom[m_Image.Zoom] - 9 / 2.0 && e.X <= m_Rect.Left + p.X * m_Zoom[m_Image.Zoom] + 9 / 2.0 && e.Y >= m_Rect.Top + p.Y * m_Zoom[m_Image.Zoom] - 9 / 2.0 && e.Y <= m_Rect.Top + p.Y * m_Zoom[m_Image.Zoom] + 9 / 2.0)
                    {
                        overPoly = true;
                    }
                }
            }

            if (m_Mode == Mode.Center)
            {
                if (overCenter)
                    Cursor = m_ArrowOverCursor;
                else
                {
                    Cursor = m_ArrowNormCursor;
                }
            }
            else
            {
                if (overPoly)
                    Cursor = m_PenOverCursor;
                else
                {
                    Cursor = m_PenCursor;
                }
            }
        }

        public void ResetZoom()
        {
            SetZoom(20);
        }

        private void SetZoom(int zoom, Point? mouseLocation = null)
        {
            float currentZoom = m_Zoom[m_Image.Zoom];

            m_Image.Zoom = zoom;

            if (mouseLocation == null)
            {
                m_Image.Pan = 
                new Point(
                    (int)
                        (m_CenterPoint.X - (m_CenterPoint.X - m_Image.Pan.X) / currentZoom * m_Zoom[m_Image.Zoom]),
                    (int)
                        (m_CenterPoint.Y - (m_CenterPoint.Y - m_Image.Pan.Y) / currentZoom * m_Zoom[m_Image.Zoom]));
            }
            else
            {
                m_Image.Pan = new Point(
                    (int)
                        (mouseLocation.Value.X -
                         (mouseLocation.Value.X - m_Image.Pan.X)/currentZoom*m_Zoom[m_Image.Zoom]),
                    (int)
                        (mouseLocation.Value.Y -
                         (mouseLocation.Value.Y - m_Image.Pan.Y)/currentZoom*m_Zoom[m_Image.Zoom]));
            }
            m_Rect.Location = m_Image.Pan;
            m_Rect.Width = (int)(m_Texture.Width * m_Zoom[m_Image.Zoom]);
            m_Rect.Height = (int)(m_Texture.Height * m_Zoom[m_Image.Zoom]);
        }

        public void DoZoom(int howMany, Point? mouseLocation = null)
        {
            var zoom = m_Image.Zoom + howMany;
            if (zoom >= m_Zoom.Length)
                zoom = m_Zoom.Length - 1;

            if (zoom < 0)
                zoom = 0;

            SetZoom(zoom, mouseLocation);
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (m_Poly != null && (m_Mode == Mode.Polygon))
                {
                    foreach (PolyPoint p in m_Poly.Points.ToList())
                    {
                        if (e.X >= m_Rect.Left + p.X*m_Zoom[m_Image.Zoom] - 9/2.0 &&
                            e.X <= m_Rect.Left + p.X*m_Zoom[m_Image.Zoom] + 9/2.0 &&
                            e.Y >= m_Rect.Top + p.Y*m_Zoom[m_Image.Zoom] - 9/2.0 &&
                            e.Y <= m_Rect.Top + p.Y*m_Zoom[m_Image.Zoom] + 9/2.0)
                        {
                                m_Poly.Points.Remove(p);
                        }
                    }
                }
            }

           
        }

        protected void OnModeChanged(object sender, EventArgs e)
        {
            if (m_Mode == Mode.Center)
                Cursor = m_ArrowNormCursor;
            if (m_Mode == Mode.Polygon)
                Cursor = m_PenCursor;
        }

    }
}

