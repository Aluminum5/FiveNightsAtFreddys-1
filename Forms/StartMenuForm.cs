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
        private Flashlight _flashlight = new Flashlight();
        private SoundEngine _soundEngine = new SoundEngine();
        private Sound _startMenuThemeMusic = null;


        public StartMenuForm()
        {
            InitializeComponent();

            //
            // Start the game threads that are global across the entire game. 
            //
            ThreadingEngine.StartThread(_soundEngine);
            ThreadingEngine.StartThread(_flashlight);

            //
            // Start the theme mustic for the start menu
            //
            _startMenuThemeMusic = _soundEngine.PlaySound(
                global::FNAF.Properties.Resources.StartMenuThemeMusic,  // theme mustic for the menu
                true                                                    // play it in a loop
            );
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ThreadingEngine.Dispose();
            base.OnClosing(e);
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

            //
            // Stop the theme music anytime a game starts.
            //
            _soundEngine.StopSound(_startMenuThemeMusic);

            //
            // The new game is initialized now start the office camera to begin.
            //
            WindowControls.ShowForm(officeCameraForm);
            Global.PlaySound(global::FNAF.Properties.Resources.Night1PhoneCall, false);
        }

        private void ContinueImageButton_Click(object sender, EventArgs e)
        {
            //
            // Stop the theme music anytime a game starts.
            //
            _soundEngine.StopSound(_startMenuThemeMusic);
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
