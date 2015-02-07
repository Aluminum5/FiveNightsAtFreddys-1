using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace FNAF.Engines
{
    class SoundEngine
    {
        private SoundPlayer _soundPlayer;
        private User _user;

        public SoundEngine(User user)
        {
            _user = user;
            _soundPlayer = new SoundPlayer();
        }

        public void PlaySound(Stream stream, bool loop = false)
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
        public void StopSound()
        {
            if (_soundPlayer != null) _soundPlayer.Stop();
        }
    }
}
