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
    public partial class PolyForm : UserControl
    {
        private Poly m_Poly;

        public PolyForm()
        {
            InitializeComponent();
        }

        private void PolyName_TextChanged(object sender, EventArgs e)
        {
            m_Poly.Name = PolyName.Text;
        }

        public Poly Poly
        {
            get { return m_Poly; }
            set 
            { 
                m_Poly = value;
                PolyName.Text = m_Poly.Name;
            }
        }
    }
}
