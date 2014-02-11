namespace SpriteUtility
{
    partial class PolygonGroupStub
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
            this.FileName = new System.Windows.Forms.Label();
            this.FrameCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FileName
            // 
            this.FileName.AutoSize = true;
            this.FileName.BackColor = System.Drawing.Color.Transparent;
            this.FileName.Location = new System.Drawing.Point(13, 3);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(23, 13);
            this.FileName.TabIndex = 0;
            this.FileName.Text = "File";
            // 
            // FrameCount
            // 
            this.FrameCount.AutoSize = true;
            this.FrameCount.BackColor = System.Drawing.Color.Transparent;
            this.FrameCount.Location = new System.Drawing.Point(13, 19);
            this.FrameCount.Name = "FrameCount";
            this.FrameCount.Size = new System.Drawing.Size(45, 13);
            this.FrameCount.TabIndex = 2;
            this.FrameCount.Text = "1 Frame";
            // 
            // PolygonGroupStub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FrameCount);
            this.Controls.Add(this.FileName);
            this.Name = "PolygonGroupStub";
            this.Size = new System.Drawing.Size(250, 35);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FileName;
        private System.Windows.Forms.Label FrameCount;
    }
}
