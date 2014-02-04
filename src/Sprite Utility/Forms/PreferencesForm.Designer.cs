namespace SpriteUtility
{
    partial class PreferencesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BackgroundButton = new System.Windows.Forms.Button();
            this.CenterButton = new System.Windows.Forms.Button();
            this.PolygonButton = new System.Windows.Forms.Button();
            this.BorderButton = new System.Windows.Forms.Button();
            this.BorderCheckbox = new System.Windows.Forms.CheckBox();
            this.CenterCheckbox = new System.Windows.Forms.CheckBox();
            this.MarkAllAsOpenCheckbox = new System.Windows.Forms.CheckBox();
            this.CenterLineButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TrimBorderButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.TrimToMinimalNonTransparentArea = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.documentStubColorButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.folderStubColorButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.imageStubColorButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.frameStubColorButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.polygonStubColor = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image View Background Color";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Image View Center Point Color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Image View Polygon Color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Image View Border Color";
            // 
            // BackgroundButton
            // 
            this.BackgroundButton.Location = new System.Drawing.Point(159, 3);
            this.BackgroundButton.Name = "BackgroundButton";
            this.BackgroundButton.Size = new System.Drawing.Size(24, 23);
            this.BackgroundButton.TabIndex = 4;
            this.BackgroundButton.UseVisualStyleBackColor = true;
            this.BackgroundButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // CenterButton
            // 
            this.CenterButton.Location = new System.Drawing.Point(159, 32);
            this.CenterButton.Name = "CenterButton";
            this.CenterButton.Size = new System.Drawing.Size(24, 23);
            this.CenterButton.TabIndex = 5;
            this.CenterButton.UseVisualStyleBackColor = true;
            this.CenterButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // PolygonButton
            // 
            this.PolygonButton.Location = new System.Drawing.Point(159, 61);
            this.PolygonButton.Name = "PolygonButton";
            this.PolygonButton.Size = new System.Drawing.Size(24, 23);
            this.PolygonButton.TabIndex = 6;
            this.PolygonButton.UseVisualStyleBackColor = true;
            this.PolygonButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // BorderButton
            // 
            this.BorderButton.Location = new System.Drawing.Point(159, 90);
            this.BorderButton.Name = "BorderButton";
            this.BorderButton.Size = new System.Drawing.Size(24, 23);
            this.BorderButton.TabIndex = 7;
            this.BorderButton.UseVisualStyleBackColor = true;
            this.BorderButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // BorderCheckbox
            // 
            this.BorderCheckbox.AutoSize = true;
            this.BorderCheckbox.Location = new System.Drawing.Point(6, 126);
            this.BorderCheckbox.Name = "BorderCheckbox";
            this.BorderCheckbox.Size = new System.Drawing.Size(85, 17);
            this.BorderCheckbox.TabIndex = 8;
            this.BorderCheckbox.Text = "Draw Border";
            this.BorderCheckbox.UseVisualStyleBackColor = true;
            this.BorderCheckbox.CheckedChanged += new System.EventHandler(this.CheckboxClick);
            // 
            // CenterCheckbox
            // 
            this.CenterCheckbox.AutoSize = true;
            this.CenterCheckbox.Location = new System.Drawing.Point(6, 149);
            this.CenterCheckbox.Name = "CenterCheckbox";
            this.CenterCheckbox.Size = new System.Drawing.Size(135, 17);
            this.CenterCheckbox.TabIndex = 9;
            this.CenterCheckbox.Text = "Fixed Size Center Point";
            this.CenterCheckbox.UseVisualStyleBackColor = true;
            this.CenterCheckbox.CheckedChanged += new System.EventHandler(this.CheckboxClick);
            // 
            // MarkAllAsOpenCheckbox
            // 
            this.MarkAllAsOpenCheckbox.AutoSize = true;
            this.MarkAllAsOpenCheckbox.Location = new System.Drawing.Point(6, 172);
            this.MarkAllAsOpenCheckbox.Name = "MarkAllAsOpenCheckbox";
            this.MarkAllAsOpenCheckbox.Size = new System.Drawing.Size(196, 17);
            this.MarkAllAsOpenCheckbox.TabIndex = 10;
            this.MarkAllAsOpenCheckbox.Text = "Mark all subsequent frames as open";
            this.MarkAllAsOpenCheckbox.UseVisualStyleBackColor = true;
            this.MarkAllAsOpenCheckbox.CheckedChanged += new System.EventHandler(this.CheckboxClick);
            // 
            // CenterLineButton
            // 
            this.CenterLineButton.Location = new System.Drawing.Point(345, 3);
            this.CenterLineButton.Name = "CenterLineButton";
            this.CenterLineButton.Size = new System.Drawing.Size(24, 23);
            this.CenterLineButton.TabIndex = 12;
            this.CenterLineButton.UseVisualStyleBackColor = true;
            this.CenterLineButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Image View Center Line Color";
            // 
            // TrimBorderButton
            // 
            this.TrimBorderButton.Location = new System.Drawing.Point(345, 32);
            this.TrimBorderButton.Name = "TrimBorderButton";
            this.TrimBorderButton.Size = new System.Drawing.Size(24, 23);
            this.TrimBorderButton.TabIndex = 14;
            this.TrimBorderButton.UseVisualStyleBackColor = true;
            this.TrimBorderButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(189, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Image View Trim Border Color";
            // 
            // TrimToMinimalNonTransparentArea
            // 
            this.TrimToMinimalNonTransparentArea.AutoSize = true;
            this.TrimToMinimalNonTransparentArea.Location = new System.Drawing.Point(6, 195);
            this.TrimToMinimalNonTransparentArea.Name = "TrimToMinimalNonTransparentArea";
            this.TrimToMinimalNonTransparentArea.Size = new System.Drawing.Size(196, 17);
            this.TrimToMinimalNonTransparentArea.TabIndex = 15;
            this.TrimToMinimalNonTransparentArea.Text = "Trim to minimal non-transparent area";
            this.TrimToMinimalNonTransparentArea.UseVisualStyleBackColor = true;
            this.TrimToMinimalNonTransparentArea.CheckedChanged += new System.EventHandler(this.CheckboxClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.polygonStubColor);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.frameStubColorButton);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.imageStubColorButton);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.folderStubColorButton);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.documentStubColorButton);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(6, 223);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 191);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stubs Colors";
            // 
            // documentStubColorButton
            // 
            this.documentStubColorButton.Location = new System.Drawing.Point(153, 23);
            this.documentStubColorButton.Name = "documentStubColorButton";
            this.documentStubColorButton.Size = new System.Drawing.Size(24, 23);
            this.documentStubColorButton.TabIndex = 18;
            this.documentStubColorButton.UseVisualStyleBackColor = true;
            this.documentStubColorButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Document Stub Color";
            // 
            // folderStubColorButton
            // 
            this.folderStubColorButton.Location = new System.Drawing.Point(153, 60);
            this.folderStubColorButton.Name = "folderStubColorButton";
            this.folderStubColorButton.Size = new System.Drawing.Size(24, 23);
            this.folderStubColorButton.TabIndex = 20;
            this.folderStubColorButton.UseVisualStyleBackColor = true;
            this.folderStubColorButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Folder Stub Color";
            // 
            // imageStubColorButton
            // 
            this.imageStubColorButton.Location = new System.Drawing.Point(153, 98);
            this.imageStubColorButton.Name = "imageStubColorButton";
            this.imageStubColorButton.Size = new System.Drawing.Size(24, 23);
            this.imageStubColorButton.TabIndex = 22;
            this.imageStubColorButton.UseVisualStyleBackColor = true;
            this.imageStubColorButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Image Stub Color";
            // 
            // frameStubColorButton
            // 
            this.frameStubColorButton.Location = new System.Drawing.Point(153, 136);
            this.frameStubColorButton.Name = "frameStubColorButton";
            this.frameStubColorButton.Size = new System.Drawing.Size(24, 23);
            this.frameStubColorButton.TabIndex = 24;
            this.frameStubColorButton.UseVisualStyleBackColor = true;
            this.frameStubColorButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Frame Stub Color";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(183, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Polygon Stub Color";
            // 
            // polygonStubColor
            // 
            this.polygonStubColor.Location = new System.Drawing.Point(339, 23);
            this.polygonStubColor.Name = "polygonStubColor";
            this.polygonStubColor.Size = new System.Drawing.Size(24, 23);
            this.polygonStubColor.TabIndex = 17;
            this.polygonStubColor.UseVisualStyleBackColor = true;
            this.polygonStubColor.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TrimToMinimalNonTransparentArea);
            this.Controls.Add(this.TrimBorderButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CenterLineButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MarkAllAsOpenCheckbox);
            this.Controls.Add(this.CenterCheckbox);
            this.Controls.Add(this.BorderCheckbox);
            this.Controls.Add(this.BorderButton);
            this.Controls.Add(this.PolygonButton);
            this.Controls.Add(this.CenterButton);
            this.Controls.Add(this.BackgroundButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PreferencesForm";
            this.Size = new System.Drawing.Size(389, 435);
            this.Load += new System.EventHandler(this.PreferencesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BackgroundButton;
        private System.Windows.Forms.Button CenterButton;
        private System.Windows.Forms.Button PolygonButton;
        private System.Windows.Forms.Button BorderButton;
        private System.Windows.Forms.CheckBox BorderCheckbox;
        private System.Windows.Forms.CheckBox CenterCheckbox;
        private System.Windows.Forms.CheckBox MarkAllAsOpenCheckbox;
        private System.Windows.Forms.Button CenterLineButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button TrimBorderButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox TrimToMinimalNonTransparentArea;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button polygonStubColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button frameStubColorButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button imageStubColorButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button folderStubColorButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button documentStubColorButton;
        private System.Windows.Forms.Label label7;
    }
}
