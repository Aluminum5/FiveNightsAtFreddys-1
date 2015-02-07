using FNAF.Common;
using FNAF.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FNAF
{
    public partial class StartMenuForm : Form
    {
        public StartMenuForm()
        {
            InitializeComponent();

            Global.PlaySound(global::FNAF.Properties.Resources.StartMenuThemeMusic, true);
        }

        private void NewGameImageButton_Click(object sender, EventArgs e)
        {
            Global.User = new User()
            {
                MaskImage = new ImageEx(
                    "FreddyMask",
                    global::FNAF.Properties.Resources.FreddyMask
                )
            };
            WindowControls.ShowForm(new CameraForm(new FormData() 
            {
                AirVents = new AirVent[] 
                {
                    new AirVent("LeftAirVent") 
                    {
                        ButtonLightOff = new ImageEx(
                        
                            "LightButton_Off",
                            global::FNAF.Properties.Resources.LightButton_Off
                        ),
                        ButtonLightOn = new ImageEx(
                            "LightButton_On",
                            global::FNAF.Properties.Resources.LightButton_On
                        ),
                        ButtonPoint = new Point(100, 430),
                        ButtonSize = new Size(60, 55),
                        ImageLightOff = new ImageEx(
                            "OfficeNoCharactersNoFlashlight",
                            global::FNAF.Properties.Resources.OfficeNoFlashlightNoCharacters
                        ),
                        ImageLightOn = new ImageEx(
                            "OfficeLeftAirVentToyChica",
                            global::FNAF.Properties.Resources.OfficeLeftAirVentToyChica
                        )
                    },
                    new AirVent("RightAirVent") 
                    {
                        ButtonLightOff = new ImageEx(
                        
                            "LightButton_Off",
                            global::FNAF.Properties.Resources.LightButton_Off
                        ),
                        ButtonLightOn = new ImageEx(
                            "LightButton_On",
                            global::FNAF.Properties.Resources.LightButton_On
                        ),
                        ButtonPoint = new Point(1360, 430),
                        ButtonSize = new Size(60, 55),
                        ImageLightOff = new ImageEx(
                            "OfficeNoCharactersNoFlashlight",
                            global::FNAF.Properties.Resources.OfficeNoFlashlightNoCharacters
                        ),
                        ImageLightOn = new ImageEx(
                            "OfficeRightAirVentToyBonnie",
                            global::FNAF.Properties.Resources.OfficeRightAirVentToyBonnie
                        )
                    }
                }.ToList(),
                Name = "Office",
                DefaultImage = new ImageEx(
                    "OfficeNoCharactersNoFlashlight",
                    global::FNAF.Properties.Resources.OfficeNoFlashlightNoCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "OfficeNoCharactersFlashlight",
                    global::FNAF.Properties.Resources.OfficeNoFlashlightNoCharacters
                ),
                ShowCameraMap = true,
                SupportsFlashlight = true,
                SupportsMask = true
            }));

            Global.StopSound();
        }

        private void ContinueImageButton_Click(object sender, EventArgs e)
        {
            Global.StopSound();
        }

        private void ContinueImageButton_MouseEnter(object sender, EventArgs e)
        {
            this.ContinueImageButton.Image = global::FNAF.Properties.Resources.ContinueMouseOver;
        }

        private void ContinueImageButton_MouseLeave(object sender, EventArgs e)
        {
            this.ContinueImageButton.Image = global::FNAF.Properties.Resources.Continue;
        }

        private void NewGameImageButton_MouseEnter(object sender, EventArgs e)
        {
            this.NewGameImageButton.Image = global::FNAF.Properties.Resources.NewGameMouseOver;
        }

        private void NewGameImageButton_MouseLeave(object sender, EventArgs e)
        {
            this.NewGameImageButton.Image = global::FNAF.Properties.Resources.NewGame;
        }
    }
}
