using System;
using System.Globalization;
using System.Windows.Forms;

namespace SpriteUtility.Forms
{
    public partial class PreferencesForm : UserControl
    {
        public PreferencesForm()
        {
            InitializeComponent();
            BackgroundButton.BackColor = MainForm.Preferences.ViewerBackground;
            CenterButton.BackColor = MainForm.Preferences.CenterPointColor;
            PolygonButton.BackColor = MainForm.Preferences.PolygonColor;
            BorderButton.BackColor = MainForm.Preferences.BorderColor;
            CenterLineButton.BackColor = MainForm.Preferences.CenterLineColor;
            TrimBorderButton.BackColor = MainForm.Preferences.TrimBorderColor;
            CenterCheckbox.Checked = MainForm.Preferences.DrawLineArtForCenter;
            BorderCheckbox.Checked = MainForm.Preferences.DrawBorder;
            MarkAllAsOpenCheckbox.Checked = MainForm.Preferences.MarkAllAsOpen;
            TrimToMinimalNonTransparentArea.Checked = MainForm.Preferences.TrimToMinimalNonTransparentArea;
            documentStubColorButton.BackColor = MainForm.Preferences.DocumentStubColor;
            imageStubColorButton.BackColor = MainForm.Preferences.ImageStubColor;
            frameStubColorButton.BackColor = MainForm.Preferences.FrameStubColor;
            folderStubColorButton.BackColor = MainForm.Preferences.FolderStubColor;
            polygonStubColor.BackColor = MainForm.Preferences.PolygonStubColor;
            simulationRatioTextBox.Text = MainForm.Preferences.SimulationRatio.ToString("0.00", CultureInfo.InvariantCulture);
            PolygonSelectedColorButton.BackColor = MainForm.Preferences.PolygonSelectedColor;
            PolygonGroupStubColorButton.BackColor = MainForm.Preferences.PolygonGroupStubColor;
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.AnyColor = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (sender == BackgroundButton)
                    MainForm.Preferences.ViewerBackground = dialog.Color;
                else if (sender == CenterButton)
                    MainForm.Preferences.CenterPointColor = dialog.Color;
                if (sender == PolygonButton)
                    MainForm.Preferences.PolygonColor = dialog.Color;
                if (sender == BorderButton)
                    MainForm.Preferences.BorderColor = dialog.Color;
                if (sender == CenterLineButton)
                    MainForm.Preferences.CenterLineColor = dialog.Color;
                if (sender == TrimBorderButton)
                    MainForm.Preferences.TrimBorderColor = dialog.Color;
                if (sender == documentStubColorButton)
                    MainForm.Preferences.DocumentStubColor = dialog.Color;
                if (sender == imageStubColorButton)
                    MainForm.Preferences.ImageStubColor = dialog.Color;
                if (sender == folderStubColorButton)
                    MainForm.Preferences.FolderStubColor = dialog.Color;
                if (sender == frameStubColorButton)
                    MainForm.Preferences.FrameStubColor = dialog.Color;
                if (sender == polygonStubColor)
                    MainForm.Preferences.PolygonStubColor = dialog.Color;
                if (sender == PolygonSelectedColorButton)
                    MainForm.Preferences.PolygonSelectedColor = dialog.Color;
                if (sender == PolygonGroupStubColorButton)
                    MainForm.Preferences.PolygonGroupStubColor = dialog.Color;

                ((Button)sender).BackColor = dialog.Color;
                MainForm.Preferences.CommitChanges();
            }
        }

        private void CheckboxClick(object sender, EventArgs e)
        {
            if (sender == BorderCheckbox)
                MainForm.Preferences.DrawBorder = BorderCheckbox.Checked;
            else if (sender == CenterCheckbox)
                MainForm.Preferences.DrawLineArtForCenter = CenterCheckbox.Checked;
            else if (sender == MarkAllAsOpenCheckbox)
                MainForm.Preferences.MarkAllAsOpen = MarkAllAsOpenCheckbox.Checked;
            else if (sender == TrimToMinimalNonTransparentArea)
                MainForm.Preferences.TrimToMinimalNonTransparentArea = TrimToMinimalNonTransparentArea.Checked;
            MainForm.Preferences.CommitChanges();
        }

        private void PreferencesForm_Load(object sender, EventArgs e)
        {

        }

        private void simulationRatioTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender == simulationRatioTextBox)
            {
                float simulationRatio;
                if (float.TryParse(simulationRatioTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out simulationRatio))
                {
                    MainForm.Preferences.SimulationRatio = simulationRatio;
                }
                MainForm.Preferences.CommitChanges();
            }
        }
    }
}
