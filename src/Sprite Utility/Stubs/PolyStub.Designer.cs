namespace SpriteUtility
{
    sealed partial class PolyStub
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
            this.PolyName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PolyName
            // 
            this.PolyName.AutoSize = true;
            this.PolyName.BackColor = System.Drawing.Color.Transparent;
            this.PolyName.Location = new System.Drawing.Point(33, 3);
            this.PolyName.Name = "PolyName";
            this.PolyName.Size = new System.Drawing.Size(104, 13);
            this.PolyName.TabIndex = 0;
            this.PolyName.Text = "Poly Name (3 points)";
            // 
            // PolyStub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PolyName);
            this.Name = "PolyStub";
            this.Size = new System.Drawing.Size(250, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PolyName;
    }
}
