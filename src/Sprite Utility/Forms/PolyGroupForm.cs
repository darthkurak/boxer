using System;
using System.Windows.Forms;
using SpriteUtility.Data;

namespace SpriteUtility.Forms
{
    public partial class PolyGroupForm : UserControl
    {
        private PolygonGroup _polygonGroup;

        public PolyGroupForm()
        {
            InitializeComponent();
        }

        private void PolyName_TextChanged(object sender, EventArgs e)
        {
            _polygonGroup.Name = PolyGroupName.Text;
        }

        public PolygonGroup Poly
        {
            get { return _polygonGroup; }
            set 
            { 
                _polygonGroup = value;
                PolyGroupName.Text = _polygonGroup.Name;
            }
        }
    }
}
