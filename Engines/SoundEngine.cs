using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace FNAF.Engines
{
    public delegate void SoundPlayed(object sender, EventArgs e);

    public class Sound
    {
        public Stream Stream;
        public bool Loop;
        public SoundPlayed SoundPlayed;

        public Sound(Stream stream, bool loop = false)
        {
            this.Stream = stream;
            this.Loop = loop;
        }
    }
    class SoundEngine : ThreadBase
    {
        private SoundPlayer _soundPlayer = new SoundPlayer();
        private object _lock = new object();
        private List<Sound> _sounds = new List<Sound>();

        public Stream CurrentStream;

        public SoundEngine() : base("SoundEngine")
        {
        }

        protected override void Start(object param)
        {
            while (true)
            {
                foreach (Sound sound in _sounds)
                {

                    _soundPlayer.PlaySync();
                    sound.SoundPlayed(this, new EventArgs());
                }
            }
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
