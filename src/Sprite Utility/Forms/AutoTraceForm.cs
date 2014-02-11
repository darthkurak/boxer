using System;
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
                AlphaToleranceTrackBar.Value = AutoTraceFormResult.AlphaTolerance;
                HullToleranceTrackBar.Value = (int)AutoTraceFormResult.HullTolerance;
                MultiPartDetectionCheckBox.Checked = AutoTraceFormResult.MultiPartDetection;
                HoleDetectionCheckBox.Checked = AutoTraceFormResult.HoleDetection;
            }
            else
            {
                AlphaToleranceTrackBar.Value = 50;
                HullToleranceTrackBar.Value = 5;
                MultiPartDetectionCheckBox.Checked = false;
                HoleDetectionCheckBox.Checked = false;
            }       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoTraceFormResult = new AutoTraceFormResult
            {
                AlphaTolerance = (byte) AlphaToleranceTrackBar.Value,
                HoleDetection = HoleDetectionCheckBox.Checked,
                MultiPartDetection = MultiPartDetectionCheckBox.Checked,
                HullTolerance = HullToleranceTrackBar.Value
            };

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
