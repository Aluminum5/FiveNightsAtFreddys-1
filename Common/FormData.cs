using FNAF.Controls;
using FNAF.Engines;
using FNAF.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FNAF.Common
{
    public class AirVent
    {
        public string Name;
        public Image ImageLightOff;
        public Image ImageLightOn;
        public Image ButtonLightOn;
        public Image ButtonLightOff;
        public Point ButtonPoint;
        public Size ButtonSize;

        public AirVent(string name)
        {
            this.Name = name;
        }
    }
    public abstract class FormBase : Form
    {
        private Flashlight _flashlight;

        protected Flashlight Flashlight
        {
            get
            {
                return _flashlight;
            }
        }

        public bool SupportsMask;
        public bool SupportsFlashlight;
        public bool SupportsMap;
        public PictureBox BackgroundPictureBox;
        public PictureBox FlashlightPowerPictureBox;
        public PictureBox MaskPictureBox;
        public CameraMap CameraMap;
        public Image DefaultImage;
        public Image DefaultFlashlightOnImage;
        public Sound Sound;
        public RoomType RoomType;

        public FormBase(string name, bool supportsMask = false, bool supportsFlashlight = true)
        {
            this.Name = name;
        }

        protected override void OnLoad(EventArgs e)
        {
            //
            // The PictureBox with the Image of the room.
            //
            this.BackgroundPictureBox = new PictureBox();
            this.BackgroundPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundPictureBox.Image = this.DefaultImage;
            this.BackgroundPictureBox.Location = new System.Drawing.Point(0, 0);
            this.BackgroundPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BackgroundPictureBox.Size = new Size(this.Size.Width - 5, this.Size.Height - 15);
            this.Controls.Add(this.BackgroundPictureBox);

            if (this.SupportsFlashlight)
            {
                _flashlight = ThreadingEngine.GetThread<Flashlight>();
                _flashlight.OutOfPower += Flashlight_OutOfPower;
                _flashlight.ImageChanged += Flashlight_ImageChanged;

                this.FlashlightPowerPictureBox = new PictureBox();
                this.FlashlightPowerPictureBox.BackColor = System.Drawing.Color.Transparent;
                this.FlashlightPowerPictureBox.Image = _flashlight.Image;
                this.FlashlightPowerPictureBox.Location = new System.Drawing.Point(20, 20);
                this.FlashlightPowerPictureBox.Name = "FlashlightPictureBox";
                this.FlashlightPowerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.FlashlightPowerPictureBox.Size = new Size(_flashlight.Image.Width / 3, _flashlight.Image.Height / 3);
                this.FlashlightPowerPictureBox.Tag = _flashlight.Image.Tag;
                this.BackgroundPictureBox.Controls.Add(this.FlashlightPowerPictureBox);
            }

            if (this.SupportsMask)
            {
                //
                // The PictureBox to overlay on the BackgroundPictureBox 
                //
                this.MaskPictureBox = new PictureBox();
                this.MaskPictureBox.BackColor = System.Drawing.Color.Transparent;
                this.MaskPictureBox.Image = ThreadingEngine.GetThread<GameEngine>().User.MaskImage;
                this.MaskPictureBox.Location = new System.Drawing.Point(0, 0);
                this.MaskPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.MaskPictureBox.Size = new Size(this.Size.Width - 5, this.Size.Height - 15);
                this.MaskPictureBox.Visible = false;

                this.BackgroundPictureBox.Controls.Add(this.MaskPictureBox);
            }

            if (this.SupportsMap)
            {
                //
                // The PictureBox to overlay on the BackgroundPictureBox 
                //
                this.CameraMap = new CameraMap();
                this.CameraMap.BackColor = System.Drawing.Color.Transparent;
                this.CameraMap.Location = new System.Drawing.Point(this.Size.Height - 200, this.Size.Width - 200);
                this.CameraMap.Visible = true;

                this.BackgroundPictureBox.Controls.Add(this.CameraMap);
            }

            this.KeyDown += new KeyEventHandler(Form_KeyDown);

            base.OnLoad(e);
        }

        protected void Flashlight_OutOfPower(object sender, EventArgs e)
        {
            _flashlight.TurnOff();
            this.BackgroundPictureBox.Image = this.DefaultImage;
        }

        protected void Flashlight_ImageChanged(object sender, EventArgs e)
        {
            this.FlashlightPowerPictureBox.Image = _flashlight.Image;
            this.FlashlightPowerPictureBox.Tag = _flashlight.Image.Tag;

            return;
        }
        protected void Form_KeyDown(object sender, KeyEventArgs e)
        {
            this.SuspendLayout();

            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    if (_flashlight.On == false)
                    {
                        if (this.SupportsMask && !this.MaskPictureBox.Visible)
                        {
                            if (_flashlight.TurnOn() == FlashlightResult.Success)
                            {
                                this.BackgroundPictureBox.Image = this.DefaultFlashlightOnImage;
                            }
                        }
                        else
                        {
                            if (_flashlight.TurnOn() == FlashlightResult.Success)
                            {
                                this.BackgroundPictureBox.Image = this.DefaultFlashlightOnImage;
                            }
                        }
                    }
                    else
                    {
                        if (_flashlight.TurnOff() == FlashlightResult.Success)
                        {
                            this.BackgroundPictureBox.Image = this.DefaultImage;
                        }
                    }

                    break;
                case Keys.M:
                    if (this.SupportsMask == true)
                    {
                        _flashlight.TurnOff();
                        this.BackgroundPictureBox.Image = this.DefaultImage;
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
