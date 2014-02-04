using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteUtility
{
    public partial class ViewerToolStrip : UserControl
    {
        private ImageViewer m_Viewer;

        public ViewerToolStrip(ImageViewer viewer)
        {
            InitializeComponent();

            m_Viewer = viewer;
            m_Viewer.ModeChanged += m_Viewer_ModeChanged;
        }

        void m_Viewer_ModeChanged(object sender, EventArgs e)
        {
            Select(m_Viewer.Mode);
        }

        private void ArrowButton_Click(object sender, EventArgs e)
        {
            Select(Mode.Center);
        }

        private void PenButton_Click(object sender, EventArgs e)
        {
            Select(Mode.Polygon);
        }

        public void Select(Mode mode)
        {
            ArrowButton.Checked = false;
            PenButton.Checked = false;
            m_Viewer.Mode = mode;
            if (mode == Mode.Center)
            {
                ArrowButton.Checked = true;
            }
            else
            {
                PenButton.Checked = true;
            }
        }

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
                m_Viewer.DoZoom(1);
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
                m_Viewer.DoZoom(-1);
        }

        private void ResetZoomButton_Click(object sender, EventArgs e)
        {
            m_Viewer.ResetZoom();
        }

    }
}
