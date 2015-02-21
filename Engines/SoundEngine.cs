using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        private List<Sound> _sounds = new List<Sound>();
        private Sound _soundPlaying;
        private Sound _soundToResume;
        private List<Sound> _soundsToStop = new List<Sound>();

        public SoundEngine() : base("SoundEngine")
        {
        }

        protected override void Start(object param)
        {
            while (true)
            {
                Sound sound = null;
                Sound soundToStop = null;

                //
                // Check if there are any sounds in the queue to play
                //
                if (_sounds.Count > 0)
                {
                    lock (_lock)
                    {
                        sound = _sounds[0];
                    }
                }

                //
                // Check if there are any sounds in the queue to stop
                //
                if (_soundsToStop.Count > 0)
                {
                    lock (_lock)
                    {
                        soundToStop = _soundsToStop[0];
                    }

                    //
                    // It's possible to add a null sound to the list so if it is null then remove it and continue;
                    //
                    if (soundToStop == null)
                    {
                        lock (_lock)
                        {
                            _soundsToStop.RemoveAt(0);
                        }
                    }
                }

                //
                // If there are no sounds to play or to stop there is no work to do so continue.
                //
                if (sound == null && soundToStop == null)
                {
                    continue;
                }

                //
                // If there is a sound to stop, stop the player immediately, set the currently 
                // playing sound to null and remove the sound from the list of sounds to stop. 
                // Then continue the loop to check again. 
                //
                if (soundToStop != null && _soundPlaying.Guid == soundToStop.Guid)
                {
                    lock (_lock)
                    {
                        _soundPlayer.Stop();
                        _soundPlaying = null;
                        _soundsToStop.Remove(soundToStop);
                    }

                    continue;
                }
                else if (soundToStop != null)
                {
                    bool removeSound = false;

                    lock (_lock)
                    {
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
                    }

                    //
                    // If the sound was found in the queue remove it now.
                    //
                    if (removeSound)
                    {
                        lock (_lock)
                        {
                            _sounds.Remove(soundToStop);
                        }
                    }

                    lock (_lock)
                    {
                        //
                        // The sound is not playing or queued to play any longer so remove it from 
                        // the list of songs to stop.
                        //
                        _soundsToStop.Remove(soundToStop);
                    }
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
                        lock (_lock)
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
                        }
                    } // _soundPlaying.Loop && _soundPlaying.IsLooping
                } // _soundPlaying != null)

                lock (_lock)
                {
                    //
                    // Load the stream into the player of the new sound.
                    //
                    _soundPlayer.Stream = sound.Stream;
                    _soundPlayer.LoadAsync();
                }

                //
                // Check to see if the sound is a loop.
                // Note if the sound is a loop and the user puts another sound in the queue that 
                // is a loop the highest order sound will win and the old sound will be lost.
                //
                if (sound.Loop)
                {
                    lock (_lock)
                    {
                        //
                        // The sound is supposed to loop so play it looping set the current sound 
                        // playing to this song and flag it as looping.
                        //
                        _soundPlayer.PlayLooping();
                        _soundPlaying = sound;
                        _soundPlaying.IsLooping = true;
                    }
                }
                else
                {
                    lock (_lock)
                    {
                        //
                        // The sound is just to play to completion so play it and wait
                        //
                        _soundPlayer.PlaySync();
                    }

                    //
                    // Check to see if there is a sound to resume playing since this sound played 
                    // to the end.
                    //
                    if (_soundToResume != null)
                    {
                        lock (_lock)
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
                }

                lock (_lock)
                {
                    //
                    // Finally remove the sound from the queue so that the next sound can be played or 
                    // the looping sound can continue;
                    //
                    _sounds.Remove(sound);
                }
            }
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

                    _sounds.CopyTo(unprioritizedSounds);
                    _sounds.Clear();
                    _sounds.Add(sound);
                    _sounds.AddRange(unprioritizedSounds.ToList());
                    break;

                case SoundPriority.Latent:
                case SoundPriority.Normal:
                    _sounds.Add(sound);
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
            _soundsToStop.Add(soundToStop);
        }
    }
}
