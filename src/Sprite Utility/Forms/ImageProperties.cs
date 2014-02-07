using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpriteUtility.Data;

namespace SpriteUtility
{
    public partial class ImageProperties : UserControl
    {
        ImageData m_ImageData;

        public ImageData ImageData
        {
            get { return m_ImageData; }
            set
            {
                m_ImageData = value;
                UpdateControls();
            }
        }
        public ImageProperties()
        {
            InitializeComponent();
        }

        private void UpdateControls()
        {
            FileName.Text = Path.GetFileNameWithoutExtension(m_ImageData.Name);
            FileType.Text = Path.GetExtension(m_ImageData.Name);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            m_ImageData.Name = FileName.Text + FileType.Text;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            FileName.Text = Path.GetFileNameWithoutExtension(m_ImageData.Name);
        }
    }
}
