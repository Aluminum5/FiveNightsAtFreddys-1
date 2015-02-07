namespace FNAF
{
    partial class LowerCameraButton
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
            this.S = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // S
            // 
            this.S.Location = new System.Drawing.Point(3, 0);
            this.S.Name = "S";
            this.S.Size = new System.Drawing.Size(192, 23);
            this.S.TabIndex = 0;
            this.S.Text = "Lower Cameras";
            this.S.UseVisualStyleBackColor = true;
            // 
            // LowerCameraButton
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.S);
            this.Name = "LowerCameraButton";
            this.Size = new System.Drawing.Size(197, 25);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button S;
    }
}
