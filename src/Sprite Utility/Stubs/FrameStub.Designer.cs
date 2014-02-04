namespace SpriteUtility
{
    partial class FrameStub
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FrameLabel = new System.Windows.Forms.Label();
            this.PolyCount = new System.Windows.Forms.Label();
            this.Thumbnail = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Thumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // FrameLabel
            // 
            this.FrameLabel.AutoSize = true;
            this.FrameLabel.BackColor = System.Drawing.Color.Transparent;
            this.FrameLabel.Location = new System.Drawing.Point(23, 3);
            this.FrameLabel.Name = "FrameLabel";
            this.FrameLabel.Size = new System.Drawing.Size(171, 13);
            this.FrameLabel.TabIndex = 0;
            this.FrameLabel.Text = "Frame 1 (100, 100) - 100ms (Open)";
            // 
            // PolyCount
            // 
            this.PolyCount.AutoSize = true;
            this.PolyCount.BackColor = System.Drawing.Color.Transparent;
            this.PolyCount.Location = new System.Drawing.Point(23, 19);
            this.PolyCount.Name = "PolyCount";
            this.PolyCount.Size = new System.Drawing.Size(62, 13);
            this.PolyCount.TabIndex = 1;
            this.PolyCount.Text = "Polygons: 0";
            // 
            // Thumbnail
            // 
            this.Thumbnail.BackColor = System.Drawing.Color.DarkGray;
            this.Thumbnail.Location = new System.Drawing.Point(200, 0);
            this.Thumbnail.Name = "Thumbnail";
            this.Thumbnail.Size = new System.Drawing.Size(38, 38);
            this.Thumbnail.TabIndex = 2;
            this.Thumbnail.TabStop = false;
            // 
            // FrameStub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Thumbnail);
            this.Controls.Add(this.PolyCount);
            this.Controls.Add(this.FrameLabel);
            this.Name = "FrameStub";
            this.Size = new System.Drawing.Size(250, 38);
            ((System.ComponentModel.ISupportInitialize)(this.Thumbnail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FrameLabel;
        private System.Windows.Forms.Label PolyCount;
        private System.Windows.Forms.PictureBox Thumbnail;
    }
}
