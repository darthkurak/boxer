using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpriteUtility.Data;

namespace SpriteUtility
{
    public partial class FolderProperties : UserControl
    {
        Folder _folder;

        public Folder FolderDocument
        {
            get { return _folder; }
            set
            {
                _folder = value;
                _firstTime = true;
                ProjectProperties_Load(this, EventArgs.Empty);
            }
        }

        public FolderProperties()
        {
            InitializeComponent();
        }

        private bool _firstTime = true;

        private void ProjectName_TextChanged(object sender, EventArgs e)
        {
            if (!_firstTime)
                _folder.Name = FolderName.Text;
            else
            {
                _firstTime = false;
            }
        }

        private void ProjectProperties_Load(object sender, EventArgs e)
        {
            if (_folder != null)
                FolderName.Text = _folder.Name;
        }
    }
}
