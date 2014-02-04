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
    public partial class ProjectProperties : UserControl
    {
        Document m_Document;

        public Document Document
        {
            get { return m_Document; }
            set
            {
                m_Document = value;
                ProjectProperties_Load(this, EventArgs.Empty);
            }
        }

        public ProjectProperties()
        {
            InitializeComponent();
        }

        private void ProjectName_TextChanged(object sender, EventArgs e)
        {
            m_Document.Name = ProjectName.Text;
        }

        private void ProjectProperties_Load(object sender, EventArgs e)
        {
            if (m_Document != null)
                ProjectName.Text = m_Document.Name;
        }
    }
}
