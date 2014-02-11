namespace SpriteUtility.Forms
{
    partial class AutoTraceForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MultiPartDetectionCheckBox = new System.Windows.Forms.CheckBox();
            this.HoleDetectionCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.HullToleranceTrackBar = new System.Windows.Forms.TrackBar();
            this.AlphaToleranceTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.HullToleranceTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaToleranceTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hull tolerence:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Alpha tolerence:";
            // 
            // MultiPartDetectionCheckBox
            // 
            this.MultiPartDetectionCheckBox.AutoSize = true;
            this.MultiPartDetectionCheckBox.Location = new System.Drawing.Point(120, 201);
            this.MultiPartDetectionCheckBox.Name = "MultiPartDetectionCheckBox";
            this.MultiPartDetectionCheckBox.Size = new System.Drawing.Size(116, 17);
            this.MultiPartDetectionCheckBox.TabIndex = 2;
            this.MultiPartDetectionCheckBox.Text = "Multi-part detection";
            this.MultiPartDetectionCheckBox.UseVisualStyleBackColor = true;
            // 
            // HoleDetectionCheckBox
            // 
            this.HoleDetectionCheckBox.AutoSize = true;
            this.HoleDetectionCheckBox.Location = new System.Drawing.Point(120, 178);
            this.HoleDetectionCheckBox.Name = "HoleDetectionCheckBox";
            this.HoleDetectionCheckBox.Size = new System.Drawing.Size(95, 17);
            this.HoleDetectionCheckBox.TabIndex = 3;
            this.HoleDetectionCheckBox.Text = "Hole detection";
            this.HoleDetectionCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(469, 294);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(569, 294);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // HullToleranceTrackBar
            // 
            this.HullToleranceTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.HullToleranceTrackBar.Location = new System.Drawing.Point(136, 36);
            this.HullToleranceTrackBar.Name = "HullToleranceTrackBar";
            this.HullToleranceTrackBar.Size = new System.Drawing.Size(508, 45);
            this.HullToleranceTrackBar.TabIndex = 6;
            // 
            // AlphaToleranceTrackBar
            // 
            this.AlphaToleranceTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.AlphaToleranceTrackBar.Location = new System.Drawing.Point(136, 127);
            this.AlphaToleranceTrackBar.Maximum = 100;
            this.AlphaToleranceTrackBar.Name = "AlphaToleranceTrackBar";
            this.AlphaToleranceTrackBar.Size = new System.Drawing.Size(508, 45);
            this.AlphaToleranceTrackBar.TabIndex = 7;
            // 
            // AutoTraceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 329);
            this.Controls.Add(this.AlphaToleranceTrackBar);
            this.Controls.Add(this.HullToleranceTrackBar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.HoleDetectionCheckBox);
            this.Controls.Add(this.MultiPartDetectionCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "AutoTraceForm";
            this.Text = "AutoTraceForm";
            ((System.ComponentModel.ISupportInitialize)(this.HullToleranceTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaToleranceTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox MultiPartDetectionCheckBox;
        private System.Windows.Forms.CheckBox HoleDetectionCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TrackBar HullToleranceTrackBar;
        private System.Windows.Forms.TrackBar AlphaToleranceTrackBar;
    }
}