using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteUtility.Forms
{
    public partial class AutoTraceForm : Form
    {
        public static AutoTraceFormResult AutoTraceFormResult { get; private set; }


        public AutoTraceForm()
        {
            InitializeComponent();

            if (AutoTraceFormResult != null)
            {
                AlphaTolerenceTrackBar.Value = AutoTraceFormResult.AlphaTolerence;
                HullTolerenceTrackBar.Value = (int)AutoTraceFormResult.HullTolerence;
                MultiPartDetectionCheckBox.Checked = AutoTraceFormResult.MultiPartDetection;
                HoleDetectionCheckBox.Checked = AutoTraceFormResult.HoleDetection;
            }
            else
            {
                AlphaTolerenceTrackBar.Value = 50;
                HullTolerenceTrackBar.Value = 5;
                MultiPartDetectionCheckBox.Checked = false;
                HoleDetectionCheckBox.Checked = false;
            }       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoTraceFormResult = new AutoTraceFormResult()
            {
                AlphaTolerence = (byte) AlphaTolerenceTrackBar.Value,
                HoleDetection = HoleDetectionCheckBox.Checked,
                MultiPartDetection = MultiPartDetectionCheckBox.Checked,
                HullTolerence = (float) HullTolerenceTrackBar.Value
            };

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
