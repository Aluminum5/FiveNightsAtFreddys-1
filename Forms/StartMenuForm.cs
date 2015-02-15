using FNAF.Common;
using FNAF.Engines;
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
        public Flashlight _flashlight;

        public StartMenuForm()
        {
            InitializeComponent();

            _flashlight = new Flashlight();
            Global.PlaySound(global::FNAF.Properties.Resources.StartMenuThemeMusic, true);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ThreadingEngine.Dispose();
            base.OnClosing(e);
        }

        private void NewGameImageButton_Click(object sender, EventArgs e)
        {
            ThreadingEngine.AddThread(_flashlight);
            ThreadingEngine.StartThreads();

            Global.User = new User()
            {
                MaskImage = new ImageEx(
                    "FreddyMask",
                    global::FNAF.Properties.Resources.FreddyMask
                )
            };

            CameraForm officeCameraForm = new CameraForm(new FormData() 
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
                    global::FNAF.Properties.Resources.OfficeFlashlightNoCharacters
                ),
                ShowCameraMap = true,
                SupportsFlashlight = true,
                SupportsMask = true
            });

            Global.StopSound();

            WindowControls.ShowForm(officeCameraForm);
        }

        private void ContinueImageButton_Click(object sender, EventArgs e)
        {
            Global.StopSound();
        }

        private void ContinueImageButton_MouseEnter(object sender, EventArgs e)
        {
            this.ContinueImageButton.Image = global::FNAF.Properties.Resources.ContinueMouseOver;
            this.ContinueImageButton.Location = new Point(this.ContinueImageButton.Location.X - 70, this.ContinueImageButton.Location.Y);

            this.NewGameImageButton.Image = global::FNAF.Properties.Resources.NewGame;
            this.NewGameImageButton.Location = new Point(this.NewGameImageButton.Location.X + 55, this.NewGameImageButton.Location.Y);
        }

        private void ContinueImageButton_MouseLeave(object sender, EventArgs e)
        {
            this.ContinueImageButton.Image = global::FNAF.Properties.Resources.Continue;
            this.ContinueImageButton.Location = new Point(this.ContinueImageButton.Location.X + 70, this.ContinueImageButton.Location.Y);

            this.NewGameImageButton.Image = global::FNAF.Properties.Resources.NewGameMouseOver;
            this.NewGameImageButton.Location = new Point(this.NewGameImageButton.Location.X - 55, this.NewGameImageButton.Location.Y);
        }
    }
}
