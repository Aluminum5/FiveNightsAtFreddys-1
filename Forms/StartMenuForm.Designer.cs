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
            this.BackgroundPictureBox = new System.Windows.Forms.PictureBox();
            this.ContinueImageButton = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.NewGameImageButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContinueImageButton)).BeginInit();
            this.SuspendLayout();
            // 
            // NewGameImageButton
            // 
            this.NewGameImageButton.BackColor = System.Drawing.Color.Transparent;
            this.NewGameImageButton.Image = global::FNAF.Properties.Resources.NewGameMouseOver;
            this.NewGameImageButton.Name = "NewGameImageButton";
            this.NewGameImageButton.Size = new System.Drawing.Size(292, 42);
            this.NewGameImageButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.NewGameImageButton.TabIndex = 1;
            this.NewGameImageButton.TabStop = false;
            this.NewGameImageButton.Click += new System.EventHandler(this.NewGameImageButton_Click);
            //
            // The PictureBox with the Image of the room.
            //
            this.BackgroundPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundPictureBox.Image = global::FNAF.Properties.Resources.StartScreen;
            this.BackgroundPictureBox.Location = new System.Drawing.Point(-2, -11);
            this.BackgroundPictureBox.Name = "BackgroundPictureBox";
            this.BackgroundPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BackgroundPictureBox.Tag = "BackgroundPictureBox";
            // 
            // ContinueImageButton
            // 
            this.ContinueImageButton.BackColor = System.Drawing.Color.Transparent;
            this.ContinueImageButton.Image = global::FNAF.Properties.Resources.Continue;
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
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.NewGameImageButton);
            this.Controls.Add(this.ContinueImageButton);
            this.Controls.Add(this.BackgroundPictureBox);
            this.Name = "StartMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Five Nights at Freddy\'s";
            ((System.ComponentModel.ISupportInitialize)(this.NewGameImageButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContinueImageButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            this.Size = new System.Drawing.Size(
                System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - 5,
                System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 5
            );
            this.BackgroundPictureBox.Size = new System.Drawing.Size(this.Size.Width - 5, this.Size.Height - 15);
            this.ContinueImageButton.Location = new System.Drawing.Point(300, (this.Size.Height / 2) + 150);
            this.NewGameImageButton.Location = new System.Drawing.Point(225, (this.Size.Height / 2) + 50);
        }

        #endregion

        private System.Windows.Forms.PictureBox BackgroundPictureBox;
        private System.Windows.Forms.PictureBox NewGameImageButton;
        private System.Windows.Forms.PictureBox ContinueImageButton;

    }
}

