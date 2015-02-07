namespace FNAF
{
    partial class StartMenuForm
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
            this.NewGameImageButton = new System.Windows.Forms.PictureBox();
            this.MenuImage = new System.Windows.Forms.PictureBox();
            this.ContinueImageButton = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.NewGameImageButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContinueImageButton)).BeginInit();
            this.SuspendLayout();
            // 
            // NewGameImageButton
            // 
            this.NewGameImageButton.BackColor = System.Drawing.Color.Transparent;
            this.NewGameImageButton.Image = global::FNAF.Properties.Resources.NewGameMouseOver;
            this.NewGameImageButton.Location = new System.Drawing.Point(187, 525);
            this.NewGameImageButton.Name = "NewGameImageButton";
            this.NewGameImageButton.Size = new System.Drawing.Size(292, 42);
            this.NewGameImageButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.NewGameImageButton.TabIndex = 1;
            this.NewGameImageButton.TabStop = false;
            this.NewGameImageButton.Click += new System.EventHandler(this.NewGameImageButton_Click);
            this.NewGameImageButton.MouseEnter += new System.EventHandler(this.NewGameImageButton_MouseEnter);
            this.NewGameImageButton.MouseLeave += new System.EventHandler(this.NewGameImageButton_MouseLeave);
            // 
            // MenuImage
            // 
            this.MenuImage.BackColor = System.Drawing.Color.Transparent;
            this.MenuImage.Image = global::FNAF.Properties.Resources.StartScreen;
            this.MenuImage.ImageLocation = "";
            this.MenuImage.Location = new System.Drawing.Point(-2, -11);
            this.MenuImage.Margin = new System.Windows.Forms.Padding(0);
            this.MenuImage.MinimumSize = new System.Drawing.Size(1280, 720);
            this.MenuImage.Name = "MainMenu";
            this.MenuImage.Size = new System.Drawing.Size(1280, 720);
            this.MenuImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.MenuImage.TabIndex = 0;
            this.MenuImage.TabStop = false;
            // 
            // ContinueImageButton
            // 
            this.ContinueImageButton.BackColor = System.Drawing.Color.Transparent;
            this.ContinueImageButton.Image = global::FNAF.Properties.Resources.ContinueMouseOver;
            this.ContinueImageButton.Location = new System.Drawing.Point(187, 525);
            this.ContinueImageButton.Name = "ContinueImageButton";
            this.ContinueImageButton.Size = new System.Drawing.Size(292, 42);
            this.ContinueImageButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ContinueImageButton.TabIndex = 1;
            this.ContinueImageButton.TabStop = false;
            this.ContinueImageButton.Click += new System.EventHandler(this.ContinueImageButton_Click);
            this.ContinueImageButton.MouseEnter += new System.EventHandler(this.ContinueImageButton_MouseEnter);
            this.ContinueImageButton.MouseLeave += new System.EventHandler(this.ContinueImageButton_MouseLeave);
            // 
            // StartMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Size = this.ClientSize = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - 5, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 5);
            this.Controls.Add(this.NewGameImageButton);
            this.Controls.Add(this.MenuImage);
            this.Name = "StartMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Five Nights at Freddy\'s";
            ((System.ComponentModel.ISupportInitialize)(this.NewGameImageButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContinueImageButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MenuImage;
        private System.Windows.Forms.PictureBox NewGameImageButton;
        private System.Windows.Forms.PictureBox ContinueImageButton;

    }
}

