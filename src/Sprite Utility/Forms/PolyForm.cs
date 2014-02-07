using System;
using System.Windows.Forms;
using SpriteUtility.Data;

namespace SpriteUtility.Forms
{
    public partial class PolyForm : UserControl
    {
        private Polygon _polygon;

        public PolyForm()
        {
            InitializeComponent();
        }

        private void PolyName_TextChanged(object sender, EventArgs e)
        {
            _polygon.Name = PolyName.Text;
        }

        public Polygon Poly
        {
            get { return _polygon; }
            set 
            { 
                _polygon = value;
                PolyName.Text = _polygon.Name;
            }
        }
    }
}
