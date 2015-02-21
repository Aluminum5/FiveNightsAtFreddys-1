using FNAF.Common;
using FNAF.Engines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FNAF.Forms
{
    public partial class OfficeForm : FormBase
    {
        private static readonly List<AirVent> _airVents = new AirVent[] 
        {
            new AirVent("LeftAirVent") 
            {
                ButtonLightOff = global::FNAF.Properties.Resources.LightButton_Off,
                ButtonLightOn = global::FNAF.Properties.Resources.LightButton_On,
                ButtonPoint = new Point(100, 430),
                ButtonSize = new Size(60, 55),
                ImageLightOff = global::FNAF.Properties.Resources.OfficeNoFlashlightNoCharacters,
                ImageLightOn = global::FNAF.Properties.Resources.OfficeLeftAirVentToyChica
            },
            new AirVent("RightAirVent") 
            {
                ButtonLightOff = global::FNAF.Properties.Resources.LightButton_Off,
                ButtonLightOn = global::FNAF.Properties.Resources.LightButton_On,
                ButtonPoint = new Point(1360, 430),
                ButtonSize = new Size(60, 55),
                ImageLightOff = global::FNAF.Properties.Resources.OfficeNoFlashlightNoCharacters,
                ImageLightOn = global::FNAF.Properties.Resources.OfficeRightAirVentToyBonnie
            }
        }.ToList();

        public OfficeForm() : base("Office")
        {
            base.DefaultImage = global::FNAF.Properties.Resources.OfficeNoFlashlightNoCharacters;
            base.DefaultFlashlightOnImage = global::FNAF.Properties.Resources.OfficeFlashlightNoCharacters;
            base.SupportsFlashlight = true;
            base.SupportsMask = true;

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (AirVent airVent in _airVents)
            {
                PictureBox airVentPictureBox = new PictureBox();
                airVentPictureBox.BackColor = System.Drawing.Color.Transparent;
                airVentPictureBox.Image = airVent.ButtonLightOff;
                airVentPictureBox.Image.Tag = airVent.ButtonLightOff;
                airVentPictureBox.Location = airVent.ButtonPoint;
                airVentPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                airVentPictureBox.Size = airVent.ButtonSize;
                airVentPictureBox.Tag = airVent;
                airVentPictureBox.Click += new EventHandler(airVentPictureBox_Click);

                this.BackgroundPictureBox.Controls.Add(airVentPictureBox);
            }
        }

        protected void airVentPictureBox_Click(object sender, EventArgs e)
        {
            PictureBox airVentPictureBox = (PictureBox)sender;

            if (airVentPictureBox.Tag == null)
            {
                return;
            }

            AirVent airVent = (AirVent)airVentPictureBox.Tag;

            if (airVentPictureBox.Image.Tag == airVent.ButtonLightOn)
            {
                this.BackgroundPictureBox.Image = airVent.ImageLightOff;
                airVentPictureBox.Image = airVent.ButtonLightOff;
                airVentPictureBox.Image.Tag = airVent.ButtonLightOff;
                return;
            }
            else
            {
                this.BackgroundPictureBox.Image = airVent.ImageLightOn;
                airVentPictureBox.Image = airVent.ButtonLightOn;
                airVentPictureBox.Image.Tag = airVent.ButtonLightOn;
                return;
            }
        }
    }
}
