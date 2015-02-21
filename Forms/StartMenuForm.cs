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
    /// <summary>
    /// 
    /// </summary>
    public partial class StartMenuForm : FormBase
    {
        /// <summary>
        /// The sound engine for this form is used to playback the theme music but could be used 
        /// for clicks or something else if needed. It is initialized as the form loads.
        /// </summary>
        private SoundEngine _soundEngine = new SoundEngine();
        /// <summary>
        /// This is the theme music sound that loops while on the start menu.
        /// </summary>
        private Sound _startMenuThemeMusic = null;

        /// <summary>
        /// Initializes the form components such as the buttons, form itself, and pictureboxes.
        /// </summary>
        public StartMenuForm() : base("StartMenuForm")
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overridden OnLoad Event sets up the form and starts the engines needed for this form 
        /// as well as playing the theme music in a loop.
        /// </summary>
        /// <param name="e">Not used, passed down to the base class' OnLoad event handler</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //
            // Start the sound engine here to play the theme mustic.
            //
            ThreadingEngine.StartThread(_soundEngine);

            //
            // Start the theme mustic for the start menu
            //
            _startMenuThemeMusic = _soundEngine.PlaySound(
                global::FNAF.Properties.Resources.StartMenuThemeMusic,  // theme mustic for the menu
                true                                                    // play it in a loop
            );
        }

        /// <summary>
        /// Since this is the main form and doesn't close until the game is set to close the only 
        /// work needed here is to cleanup any outstanding thread work.
        /// </summary>
        /// <param name="e">Not used, passed down to the base class' OnClosing event handler.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            //
            // Must cleanup the threads here as the application is shutting down.
            //
            ThreadingEngine.Dispose();

            //
            // Threads are cleaned up close the application.
            //
            base.OnClosing(e);
        }

        /// <summary>
        /// Occurs when the user clicks the new game button. When a new game is created the office 
        /// camera is shown and a user instance is created. This is also a good time to setup the 
        /// Game logic engine which handles the characters and user. The flashlight is also 
        /// initialized.
        /// </summary>
        /// <param name="sender">ignored</param>
        /// <param name="e">ignored</param>
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
            // Each game requires the flashlight so start the flashlight thread here.
            //
            ThreadingEngine.StartThread(new Flashlight());

            //
            // The new game is initialized now start the office camera to begin.
            //
            GameEngine.ShowForm(officeCameraForm);
        }

        /// <summary>
        /// Not Implemented yet but will eventually continue a users game from where they left
        /// off the last time they were playing. For now it just throws up a message box notifying
        /// the user that it's not supported yet.
        /// </summary>
        /// <param name="sender">ignored</param>
        /// <param name="e">ignored</param>
        private void ContinueImageButton_Click(object sender, EventArgs e)
        {
            //
            // Stop the theme music anytime a game starts.
            //
            _soundEngine.StopSound(_startMenuThemeMusic);

            //
            // Haven't implemented tracking last game yet.
            //
            MessageBox.Show(
                "We apologized but currently continuing your game is not supported. Support for " +
                "this feature to come soon.", 
                "Continue Not Supported", 
                MessageBoxButtons.OK
            );
        }

        /// <summary>
        /// Occurs when the users mouse or finger enters the boundaries of the continue button.
        /// This handles changing where the arrow is indicating the user is about to click.
        /// Some location handling occurs as well since the buttons are different sizes and the 
        /// location needs to be offset based on that new size.
        /// </summary>
        /// <param name="sender">ignored</param>
        /// <param name="e">ignored</param>
        private void ContinueImageButton_MouseEnter(object sender, EventArgs e)
        {
            //
            // Change the continue button's image to be the button with the arrow in front of it.
            // When doing this the width of the image increases and the location needs to take into
            // account this change by offsetting the horizontal or x-axis location by -70 pixels
            //
            this.ContinueImageButton.Image = global::FNAF.Properties.Resources.ContinueMouseOver;
            this.ContinueImageButton.Location = new Point(
                this.ContinueImageButton.Location.X - 70, 
                this.ContinueImageButton.Location.Y
            );

            //
            // Change the new game button's image to be the button without the arrow in front of it.
            // When doing this the width of the image decreases and the location needs to take into
            // account this change by offsetting the horizontal or x-axis location by 55 pixels
            //
            this.NewGameImageButton.Image = global::FNAF.Properties.Resources.NewGame;
            this.NewGameImageButton.Location = new Point(
                this.NewGameImageButton.Location.X + 55, 
                this.NewGameImageButton.Location.Y
            );
        }

        /// <summary>
        /// Occurs when the users mouse or finger leaves the boundaries of the continue button.
        /// This handles changing where the arrow is indicating the user is about to click. By
        /// default when the user is not over the continue button the arrow is in front of the new
        /// game button. Some location handling occurs as well since the buttons are different 
        /// sizes and the location needs to be offset based on that new size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContinueImageButton_MouseLeave(object sender, EventArgs e)
        {
            //
            // Change the continue button's image to be the button without the arrow in front of it.
            // When doing this the width of the image decreases and the location needs to take into
            // account this change by offsetting the horizontal or x-axis location by 70 pixels
            //
            this.ContinueImageButton.Image = global::FNAF.Properties.Resources.Continue;
            this.ContinueImageButton.Location = new Point(
                this.ContinueImageButton.Location.X + 70, 
                this.ContinueImageButton.Location.Y
            );

            //
            // Change the new game button's image to be the button with the arrow in front of it.
            // When doing this the width of the image increases and the location needs to take into
            // account this change by offsetting the horizontal or x-axis location by -55 pixels
            //
            this.NewGameImageButton.Image = global::FNAF.Properties.Resources.NewGameMouseOver;
            this.NewGameImageButton.Location = new Point(
                this.NewGameImageButton.Location.X - 55, 
                this.NewGameImageButton.Location.Y
            );
        }
    }
}
