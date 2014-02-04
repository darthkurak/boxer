namespace SpriteUtility
{
    partial class ViewerToolStrip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewerToolStrip));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ArrowButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.PenButton = new System.Windows.Forms.ToolStripButton();
            this.ResetZoomButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ArrowButton,
            this.PenButton,
            this.ZoomInButton,
            this.ZoomOutButton,
            this.ResetZoomButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(32, 247);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ArrowButton
            // 
            this.ArrowButton.Checked = true;
            this.ArrowButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ArrowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ArrowButton.Image = ((System.Drawing.Image)(resources.GetObject("ArrowButton.Image")));
            this.ArrowButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ArrowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ArrowButton.Name = "ArrowButton";
            this.ArrowButton.Size = new System.Drawing.Size(29, 20);
            this.ArrowButton.Click += new System.EventHandler(this.ArrowButton_Click);
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomInButton.Image")));
            this.ZoomInButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.Size = new System.Drawing.Size(29, 18);
            this.ZoomInButton.Text = "Zoom  In";
            this.ZoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOutButton.Image")));
            this.ZoomOutButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(29, 18);
            this.ZoomOutButton.Text = "Zoom Out";
            this.ZoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // PenButton
            // 
            this.PenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PenButton.Image = ((System.Drawing.Image)(resources.GetObject("PenButton.Image")));
            this.PenButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PenButton.Name = "PenButton";
            this.PenButton.Size = new System.Drawing.Size(29, 19);
            this.PenButton.Text = "Draw Polygon";
            this.PenButton.Click += new System.EventHandler(this.PenButton_Click);
            // 
            // ResetZoomButton
            // 
            this.ResetZoomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ResetZoomButton.Image = ((System.Drawing.Image)(resources.GetObject("ResetZoomButton.Image")));
            this.ResetZoomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ResetZoomButton.Name = "ResetZoomButton";
            this.ResetZoomButton.Size = new System.Drawing.Size(29, 20);
            this.ResetZoomButton.Text = "toolStripButton1";
            this.ResetZoomButton.Click += new System.EventHandler(this.ResetZoomButton_Click);
            // 
            // ViewerToolStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.toolStrip1);
            this.Name = "ViewerToolStrip";
            this.Size = new System.Drawing.Size(32, 247);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ZoomInButton;
        private System.Windows.Forms.ToolStripButton ZoomOutButton;
        private System.Windows.Forms.ToolStripButton ArrowButton;
        private System.Windows.Forms.ToolStripButton PenButton;
        private System.Windows.Forms.ToolStripButton ResetZoomButton;
    }
}
