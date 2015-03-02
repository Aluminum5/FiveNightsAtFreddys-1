using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;


namespace FNAF.Engines
{
    public enum SoundPriority
    {
        Immediate,
        Normal,
        Latent
    }
    public class Sound
    {
        public Stream Stream;
        public bool Loop;
        public bool IsLooping;
        public bool PlayToEnd;
        public Guid Guid;

        public Sound(Stream stream, bool loop = false) 
            : this(stream, loop, false)
        {

        }
        public Sound(Stream stream, bool loop, bool playToEnd)
        {
            this.Stream = stream;
            this.Loop = loop;
            this.PlayToEnd = playToEnd;
            this.Guid = Guid.NewGuid();
        }
    }
    public class SoundEngine : ThreadBase
    {
        SoundPlayer _soundPlayer = new SoundPlayer();
        private object _lock = new object();
        private List<Sound> _sounds;
        private Sound _soundPlaying;
        private Sound _soundToResume;
        private List<Sound> _soundsToStop;
        protected CancellationTokenSource _cancellationTokenSource;

        public SoundEngine() : base("SoundEngine")
        {
        }

        protected override void ThreadStart()
        {
            lock (_lock)
            {
                _sounds = new List<Sound>();
                _soundPlaying = null;
                _soundToResume = null;
                _soundsToStop = new List<Sound>();
                _cancellationTokenSource = new CancellationTokenSource();
            }

             

            //
            // Threads infinite loop responding to sounds added and removed/started and stopped.
            //
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                //
                // Check if there are any sounds in the queue to stop
                //
                if (_soundsToStop.Count > 0)
                {
                    lock (_lock)
                    {
                        //
                        // Stop any sounds in the queue to stop. Always stop the sounds before the new 
                        // sounds are played. This routine checks if there are sounds in the queue to 
                        // stop and if not returns.
                        //
                        QueueSoundStop();
                    }
                }

                //
                // Check if there are any sounds in the queue to stop
                //
                if (_sounds.Count > 0)
                {
                    lock (_lock)
                    {
                        //
                        // play any sounds in the queue to play. This routine checks if there are 
                        // sounds in the queue to play and if not returns.
                        //
                        QueueSoundPlay();
                    }
                }

                Thread.Sleep(300);
            }

            lock (_lock)
            {
                //
                // Stop all the sounds 
                //
                _soundPlayer.Stop();

                //
                // Sound has been stopped reset the tracking variable
                //
                _soundPlaying = null;

                //
                // Clear the Sounds that were in the queue to play.
                //
                _sounds.Clear();

                //
                // Clear the list of sounds that were in the queue to stop.
                //
                _soundsToStop.Clear();

                //
                // Reset the sound that was set to resume after the current playing sound 
                // was complete
                //
                _soundToResume = null;
            }
        }
        protected override void ThreadStop()
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }

            return;
        }

        public Sound PlaySound(Stream stream, bool loop = false)
        {
            return PlaySound(new Sound(stream, loop));
        }
        public Sound PlaySound(Sound sound)
        {
            return PlaySound(sound, SoundPriority.Normal);
        }
        public Sound PlaySound(Sound sound, SoundPriority priority)
        {
            switch (priority)
            {
                case SoundPriority.Immediate:
                    Sound[] unprioritizedSounds = new Sound[_sounds.Count + 1];

                    lock (_lock)
                    {
                        _sounds.CopyTo(unprioritizedSounds);
                        _sounds.Clear();
                        _sounds.Add(sound);
                        _sounds.AddRange(unprioritizedSounds.ToList());
                    }
                    break;

                case SoundPriority.Latent:
                case SoundPriority.Normal:
                    lock (_lock)
                    {
                        _sounds.Add(sound);
                    }
                    break;

                default:
                    throw new ArgumentException(string.Format(
                        "This priority: {0} is not known to the PlaySound routine. Please implement.", 
                        priority.ToString()
                    ));

            }

            return sound;
        }
        public void StopSound(Sound soundToStop)
        {
            lock (_lock)
            {
                _soundsToStop.Add(soundToStop);
            }
        }

        private void QueueSoundStop()
        {
            Sound soundToStop = null;

            //
            // Check if there are any sounds in the queue to stop
            //
            if (_soundsToStop.Count <= 0)
            {
                return;
            }

            //
            // Grab the first indexed song always as when the song is stopped the queue is updated 
            // and the next song becomes the first.
            //
            soundToStop = _soundsToStop[0];

            //
            // It's possible to add a null sound to the list so if it is null then remove it and 
            // continue;
            //
            // It's also possible that the caller thinks they've added a sound to the queue but 
            // there is no sound. if that is the case just remove the sound letting them think
            // they started and stopped it.
            //
            if (soundToStop == null || _soundPlaying == null)
            {
                _soundsToStop.RemoveAt(0);
                return;
            }

            //
            // If there is a sound to stop, stop the player immediately, set the currently 
            // playing sound to null and remove the sound from the list of sounds to stop. 
            // Then continue the loop to check again. 
            //
            if (_soundPlaying.Guid == soundToStop.Guid)
            {
                _soundPlayer.Stop();
                _soundPlaying = null;
                _soundsToStop.Remove(soundToStop);
            }
            else
            {
                bool removeSound = false;

                //
                // Check to see if the soundToStop is in the queue if it is remove it.
                //
                foreach (Sound soundInQueue in _sounds)
                {
                    //
                    // If the sound is in the queue flag it for removal and break the loop.
                    //
                    if (soundInQueue == soundToStop)
                    {
                        removeSound = true;
                        break;
                    }
                }

                //
                // If the sound was found in the queue remove it now.
                //
                if (removeSound)
                {
                    _sounds.Remove(soundToStop);
                }

                //
                // The sound is not playing or queued to play any longer so remove it from 
                // the list of songs to stop.
                //
                _soundsToStop.Remove(soundToStop);
            }

            return;
        }
        private void QueueSoundPlay()
        {
            Sound sound = null;

            //
            // Check if there are any sounds in the queue to play
            //
            if (_sounds.Count <= 0)
            {
                return;
            }

            //
            // Get the first indexed sound as after each play the sound is removed form the queue.
            //
            sound = _sounds[0];

            //
            // It's possible to add a null sound to the list so if it is null then remove it and 
            // continue;
            //
            if (sound == null)
            {
                _soundsToStop.RemoveAt(0);
                return;
            }

            //
            // A sound was added to the queue process currently playing sound and then play 
            // the newly added sound.
            //

            //
            // If there is already a sound playing check to see if it is looping
            //
            if (_soundPlaying != null)
            {
                //
                // If it is looping the sound needs to be stopped as it's been interrupted and 
                // a new sound is needed to be played.
                //
                if (_soundPlaying.Loop && _soundPlaying.IsLooping)
                {
                    //
                    // Set the sound to resume after this new sound has completed.
                    //
                    _soundToResume = _soundPlaying;
                    _soundToResume.IsLooping = false;

                    //
                    // Reset the sound that is playing as it won't be playing anymore and 
                    // stop it from playing.
                    //
                    _soundPlaying = null;
                    _soundPlayer.Stop();
                } // _soundPlaying.Loop && _soundPlaying.IsLooping
            } // _soundPlaying != null)

            //
            // Load the stream into the player regardless of whether it is a looping sound or 
            // single play.
            //
            _soundPlayer.Stream = sound.Stream;
            _soundPlayer.LoadAsync();

            //
            // If the sound to play is not a looping sound 
            //
            if (sound.Loop == false)
            {
                if (sound.PlayToEnd)
                {
                    _soundPlayer.Play();
                }
                //
                // The sound is just to play to completion so play it and wait
                //
                _soundPlayer.PlaySync();

                //
                // Check to see if there is a sound to resume playing since this sound played 
                // to the end.
                //
                if (_soundToResume != null)
                {
                    //
                    // There was a sound in the queue to resume so load the sound set the 
                    // _soundPlaying to this sound, flag it as looping, and remove the sound 
                    // from needing to be resumed.
                    //
                    _soundPlayer.Stream = _soundToResume.Stream;
                    _soundPlayer.LoadAsync();
                    _soundPlaying = _soundToResume;
                    _soundPlaying.IsLooping = true;
                    _soundToResume = null;
                }
            }
            else
            {
                //
                // Note if the sound is a loop and the user puts another sound in the queue that 
                // is a loop the highest order sound will win and the old sound will be lost.
                //
                QueueSoundPlayLoop(sound);
            }

            //
            // Finally remove the sound from the queue so that the next sound can be played or 
            // the looping sound can continue;
            //
            _sounds.Remove(sound);

            return;
        }
        private void QueueSoundPlayLoop(Sound sound)
        {
            //
            // The sound is supposed to loop so play it looping set the current sound 
            // playing to this song and flag it as looping.
            //
            _soundPlayer.PlayLooping();
            _soundPlaying = sound;
            _soundPlaying.IsLooping = true;

            return;
        }
        private Uri UriFromStream(Stream stream)
        {
            MediaPlayer mp = new MediaPlayer();
        }
    }
}
