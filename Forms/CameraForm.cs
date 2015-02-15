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
using FNAF.Engines;
using System.Threading;

namespace FNAF.Forms
{
    public partial class CameraForm : Form
    {
        private FormData _formData;
        private Flashlight _flashlight;

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
                _flashlight = ThreadingEngine.GetThread<Flashlight>();
                _flashlight.ImageChanged += flashlight_ImageChanged;
                _flashlight.OutOfPower += flashlight_OutOfPower;

                this.FlashlightPowerPictureBox = new PictureBox();
                this.FlashlightPowerPictureBox.BackColor = System.Drawing.Color.Transparent;
                this.FlashlightPowerPictureBox.Image = _flashlight.Image;
                this.FlashlightPowerPictureBox.Location = new System.Drawing.Point(20, 20);
                this.FlashlightPowerPictureBox.Name = "FlashlightPictureBox";
                this.FlashlightPowerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.FlashlightPowerPictureBox.Size = new Size(_flashlight.Image.Width/3, _flashlight.Image.Height/3);
                this.FlashlightPowerPictureBox.Tag = _flashlight.Image.Tag;
                this.BackgroundPictureBox.Controls.Add(this.FlashlightPowerPictureBox);

                this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CameraForm_KeyDown);
            }
        }

        protected void flashlight_OutOfPower(object sender, EventArgs e)
        {
            _flashlight.TurnOff();
            this.BackgroundPictureBox.Image = _formData.DefaultImage.Image;
        }

        protected void flashlight_ImageChanged(object sender, EventArgs e)
        {
            this.FlashlightPowerPictureBox.Image = _flashlight.Image;
            this.FlashlightPowerPictureBox.Tag = _flashlight.Image.Tag;

            return;
        }

        protected void airVentPictureBox_Click(object sender, EventArgs e)
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

        protected void CameraForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.SuspendLayout();
            
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    if (_flashlight.On == false)
                    {
                        if (_formData.SupportsMask && !this.MaskPictureBox.Visible)
                        {
                            if (_flashlight.TurnOn() == FlashlightResult.Success)
                            {
                                this.BackgroundPictureBox.Image = _formData.DefaultFlashlightOnImage.Image;
                            }
                        }
                        else
                        {
                            if (_flashlight.TurnOn() == FlashlightResult.Success)
                            {
                                this.BackgroundPictureBox.Image = _formData.DefaultFlashlightOnImage.Image;
                            }
                        }
                    }
                    else
                    {
                        if (_flashlight.TurnOff() == FlashlightResult.Success)
                        {
                            this.BackgroundPictureBox.Image = _formData.DefaultImage.Image;
                        }
                    }

                    break;
                case Keys.M:
                    if (_formData.SupportsMask == true)
                    {
                        _flashlight.TurnOff();
                        this.BackgroundPictureBox.Image = _formData.DefaultImage.Image;
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
