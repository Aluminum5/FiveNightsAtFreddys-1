using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FNAF.Common;
using FNAF.Forms;
using System.Media;
using System.IO;

namespace FNAF
{
    public class User
    {
        public ImageEx MaskImage;
        public CameraForm CurrentForm;
        public CameraForm LastForm;

        public User()
        {

        }
    }
    public static class Global
    {
        private static SoundPlayer _soundPlayer;

        public static User User;        

        public static void PlaySound(Stream stream, bool loop = false)
        {
            if (_soundPlayer == null) _soundPlayer = new SoundPlayer();
            else StopSound();

            _soundPlayer.Stream = stream;

            if (loop)
            {
                _soundPlayer.PlayLooping();
            }
            else
            {
                _soundPlayer.Play();
            }

            return;
        }
        public static void StopSound()
        {
            if (_soundPlayer != null) _soundPlayer.Stop();
        }
    }
}
