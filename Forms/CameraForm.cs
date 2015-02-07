using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FNAF.Common;
using FNAF.Controls;
using System.Media;

namespace FNAF.Forms
{
    public partial class CameraForm : Form
    {
        private int _flashLightUsedCount = 0;
        private FormData _formData;
        private bool _flashLightOn = false;

        public CameraForm(FormData formData)
        {
            Global.User.CurrentForm = this;

            _formData = formData;

            //
            // Defaults for the form itself.
            //
            this.Name = formData.Name;
            this.Text = formData.Name;

            InitializeComponent();

            //
            // The PictureBox with the Image of the room.
            //
            this.BackgroundPictureBox = new PictureBox();
            this.BackgroundPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundPictureBox.Image = formData.DefaultImage.Image;
            this.BackgroundPictureBox.Location = new System.Drawing.Point(0, 0);
            this.BackgroundPictureBox.Name = formData.DefaultImage.Name;
            this.BackgroundPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BackgroundPictureBox.Size = new Size(this.Size.Width - 5, this.Size.Height - 15);
            this.BackgroundPictureBox.Tag = formData.DefaultImage.Name;
            this.Controls.Add(this.BackgroundPictureBox);

            if (formData.SupportsMask)
            {
                //
                // The PictureBox to overlay on the BackgroundPictureBox 
                //
                this.MaskPictureBox = new PictureBox();
                this.MaskPictureBox.BackColor = System.Drawing.Color.Transparent;
                this.MaskPictureBox.Image = Global.User.MaskImage.Image;
                this.MaskPictureBox.Location = new System.Drawing.Point(0, 0);
                this.MaskPictureBox.Name = formData.DefaultImage.Name;
                this.MaskPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.MaskPictureBox.Size = new Size(this.Size.Width - 5, this.Size.Height - 15);
                this.MaskPictureBox.Tag = Global.User.MaskImage.Name;
                this.MaskPictureBox.Visible = false;

                this.BackgroundPictureBox.Controls.Add(this.MaskPictureBox);
            }

            if (formData.AirVents.Count > 0)
            {
                foreach (AirVent airVent in formData.AirVents)
                {
                    PictureBox airVentPictureBox = new PictureBox();
                    airVentPictureBox.BackColor = System.Drawing.Color.Transparent;
                    airVentPictureBox.Image = airVent.ButtonLightOff.Image;
                    airVentPictureBox.Image.Tag = airVent.ButtonLightOff.Name;
                    airVentPictureBox.Location = airVent.ButtonPoint;
                    airVentPictureBox.Name = airVent.ButtonLightOff.Name;
                    airVentPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    airVentPictureBox.Size = airVent.ButtonSize;
                    airVentPictureBox.Tag = airVent;
                    airVentPictureBox.Click += new EventHandler(airVentPictureBox_Click);

                    this.BackgroundPictureBox.Controls.Add(airVentPictureBox);
                }
            }

            if (formData.ShowCameraMap)
            {
                this.CameraMap = new CameraMap();

                // 
                // CameraMap
                // 
                this.CameraMap.AutoSize = true;
                this.CameraMap.BackColor = System.Drawing.Color.Transparent;
                this.CameraMap.Location = new System.Drawing.Point(this.Size.Width - (356+15), this.Size.Height - (231+30));
                this.CameraMap.Name = "CameraMap";
                this.CameraMap.TabIndex = 5;
                this.CameraMap.Size = new System.Drawing.Size(356, 231);

                this.BackgroundPictureBox.Controls.Add(this.CameraMap);
            }

            if (formData.SupportsFlashlight)
            {
                this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CameraForm_KeyDown);
            }
        }

        void airVentPictureBox_Click(object sender, EventArgs e)
        {
            PictureBox airVentPictureBox = (PictureBox)sender;

            if (airVentPictureBox.Tag == null)
            {
                return;
            }

            AirVent airVent = (AirVent)airVentPictureBox.Tag; 

            if (airVentPictureBox.Image.Tag.ToString() == airVent.ButtonLightOn.Name)
            {
                this.BackgroundPictureBox.Image = airVent.ImageLightOff.Image;
                airVentPictureBox.Image = airVent.ButtonLightOff.Image;
                airVentPictureBox.Image.Tag = airVent.ButtonLightOff.Name;
                return;
            }
            else
            {
                this.BackgroundPictureBox.Image = airVent.ImageLightOn.Image;
                airVentPictureBox.Image = airVent.ButtonLightOn.Image;
                airVentPictureBox.Image.Tag = airVent.ButtonLightOn.Name;
                return;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Global.StopSound();

            base.OnClosing(e);
        }

        private void CameraForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.SuspendLayout();
            
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    if (_flashLightOn == false)
                    {
                        if (_flashLightUsedCount >= 3 && _formData.Characters.Count == 3)
                        {
                            this.BackgroundPictureBox.Image = _formData.Characters["Chica"].ScareImage.Image;
                            Global.PlaySound(_formData.Characters["Chica"].ScareScream, true);
                        }
                        else if (_flashLightUsedCount == 2 && _formData.Characters.Count == 3)
                        {
                            this.BackgroundPictureBox.Image = _formData.Characters["Chica"].VisibleImage.Image;
                        }
                        else
                        {
                            this.BackgroundPictureBox.Image = _formData.DefaultFlashlightOnImage.Image;
                        }

                        _flashLightUsedCount++;
                        _flashLightOn = true;
                    }
                    else
                    {
                        this.BackgroundPictureBox.Image = _formData.DefaultImage.Image;
                        _flashLightOn = false;
                    }

                    break;
                case Keys.M:
                    if (_formData.SupportsMask == true)
                    {
                        this.MaskPictureBox.Visible = !this.MaskPictureBox.Visible;
                    }

                    break;

                default:
                    break;
            }

            this.Update();
            this.Refresh();

            this.ResumeLayout(true);

            return;
        }
    }
}
