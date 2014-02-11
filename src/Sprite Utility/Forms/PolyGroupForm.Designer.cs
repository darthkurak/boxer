namespace SpriteUtility.Forms
{
    partial class PolyGroupForm
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
            this.PolyGroupName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Polygon Group Name:";
            // 
            // PolyGroupName
            // 
            this.PolyGroupName.Location = new System.Drawing.Point(122, 7);
            this.PolyGroupName.Name = "PolyGroupName";
            this.PolyGroupName.Size = new System.Drawing.Size(200, 20);
            this.PolyGroupName.TabIndex = 1;
            this.PolyGroupName.TextChanged += new System.EventHandler(this.PolyName_TextChanged);
            // 
            // PolyGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PolyGroupName);
            this.Controls.Add(this.label1);
            this.Name = "PolyGroupForm";
            this.Size = new System.Drawing.Size(429, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PolyGroupName;
    }
}
