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
    public partial class StartMenuForm : FormBase
    {
        private Flashlight _flashlight = new Flashlight();
        private SoundEngine _soundEngine = new SoundEngine();
        private Sound _startMenuThemeMusic = null;


        public StartMenuForm() : base("StartMenuForm")
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
            OfficeForm officeCameraForm = new OfficeForm();

            //
            // Stop the theme music anytime a game starts.
            //
            _soundEngine.StopSound(_startMenuThemeMusic);

            User user = new User()
            {
                MaskImage = global::FNAF.Properties.Resources.FreddyMask,
                CurrentForm = officeCameraForm,
                LastForm = this
            };

            //
            // Start the game engine. This is the engine that handles basic game logic such as 
            // when to play certain sounds, randomly moves characters throughout the rooms and 
            // decides when to scare if to scare at all. This will also setup the user/security 
            // guard.
            //
            ThreadingEngine.StartThread(new GameEngine(user));

            //
            // The new game is initialized now start the office camera to begin.
            //
            GameEngine.ShowForm(officeCameraForm);
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
