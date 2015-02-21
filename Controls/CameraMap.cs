using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FNAF.Forms;
using FNAF.Common;
using FNAF.Engines;

namespace FNAF.Controls
{
    public partial class CameraMap : UserControl
    {
        private SoundEngine _soundEngine;

        public CameraMap()
        {
            _soundEngine = ThreadingEngine.GetThread<SoundEngine>();

            InitializeComponent();
        }

        private void CameraButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            CharacterCollection characters = GetCharacters(button);

            CameraForm cameraForm = GetCameraForm(button);

            GameEngine.ShowForm(cameraForm, this, true);

        }

        private CharacterCollection GetCharacters(Button button)
        {
            switch (button.Name)
            {
                case "Camera1Button":
                    return ThreadingEngine.GetThread<GameEngine>().GetCharacters(RoomType.PartyRoom1);

                case "Camera2Button":
                    return ThreadingEngine.GetThread<GameEngine>().GetCharacters(RoomType.PartyRoom2);

                case "Camera3Button":
                    return ThreadingEngine.GetThread<GameEngine>().GetCharacters(RoomType.PartyRoom3);

                case "Camera4Button":
                    return ThreadingEngine.GetThread<GameEngine>().GetCharacters(RoomType.PartyRoom4);

                case "Camera5Button":
                    return ThreadingEngine.GetThread<GameEngine>().GetCharacters(RoomType.MainHall);

                case "Camera6Button":

                    break;

                case "Camera7Button":

                    break;

                case "Camera8Button":

                    break;

                case "Camera9Button":

                    break;

                case "Camera10Button":

                    break;

                case "Camera11utton":

                    break;

                case "Camera12Button":

                    break;

                case "YouButton":

                    break;
            }

            return new CharacterCollection();
        }

        private CameraForm GetCameraForm(Button button)
        {
            switch (button.Name)
            {
                case "Camera1Button":
                    return ThreadingEngine.GetThread<GameEngine>().GetCameraForm(RoomType.PartyRoom1);

                case "Camera2Button":
                    return ThreadingEngine.GetThread<GameEngine>().GetCameraForm(RoomType.PartyRoom2);

                case "Camera3Button":
                    return ThreadingEngine.GetThread<GameEngine>().GetCameraForm(RoomType.PartyRoom3);

                case "Camera4Button":
                    return ThreadingEngine.GetThread<GameEngine>().GetCameraForm(RoomType.PartyRoom4);

                case "Camera5Button":
                    return ThreadingEngine.GetThread<GameEngine>().GetCameraForm(RoomType.MainHall);

                case "Camera6Button":

                    break;

                case "Camera7Button":

                    break;

                case "Camera8Button":

                    break;

                case "Camera9Button":

                    break;

                case "Camera10Button":

                    break;

                case "Camera11utton":

                    break;

                case "Camera12Button":

                    break;

                case "YouButton":

                    break;
            }

            return new CameraForm();
        }
    }
}
