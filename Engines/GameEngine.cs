using FNAF.Common;
using FNAF.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FNAF.Engines
{
    public enum RoomType
    {
        PartyRoom1,
        PartyRoom2,
        PartyRoom3,
        PartyRoom4,
        Office,
        MainHall,
        ShowStage,
        PartsService,
        GameArea,
        PrizeCorner,
        KidsCove,
        LeftAirVent,
        RightAirVent
    }
    public enum ImageType
    {
        NoCharacter,
        NoCharacterNoFlashlight,
        CharacterVisible,
        FlashlightCharacterVisible,
        CharacterScare,
        AirVentLightCharacterVisible
    }
    public class RoomImage
    {
        public Image Image;
        public RoomType RoomType;
        public ImageType ImageType;
    }
    public class RoomImageCollection : CollectionBase
    {
        public RoomImage this[int index]
        {
            get
            {
                return ((RoomImage)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public RoomImageCollection()
            : this(new List<RoomImage>())
        {
        }
        public RoomImageCollection(IList list)
            : base()
        {
            foreach (object o in list)
            {
                if (o is RoomImage)
                {
                    this.Add((RoomImage)o);
                }
            }
        }

        public int Add(RoomImage value)
        {
            return (List.Add(value));
        }

        public int IndexOf(RoomImage value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, RoomImage value)
        {
            List.Insert(index, value);
        }

        public void Remove(RoomImage value)
        {
            List.Remove(value);
        }

        public bool Contains(RoomImage value)
        {
            // If value is not of type RoomImage, this will return false. 
            return (List.Contains(value));
        }

        public RoomImageCollection GetRoomImagesByType(RoomType roomType)
        {
            RoomImageCollection roomImages = new RoomImageCollection();

            foreach (RoomImage image in this.InnerList)
            {
                if (image.RoomType == roomType)
                {
                    roomImages.Add(image);
                }
            }

            return roomImages;
        }
    }
    public class CharacterCollection : CollectionBase
    {
        public Character this[int index]
        {
            get
            {
                return ((Character)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public Character this[string name]
        {
            get
            {
                foreach (Character character in base.InnerList)
                {
                    if (character.Name == name)
                    {
                        return character;
                    }
                }

                throw new MissingMemberException();
            }
        }

        public CharacterCollection()
            : this(new List<Character>())
        {
        }
        public CharacterCollection(IList list)
            : base()
        {
            foreach (object o in list)
            {
                if (o is Character)
                {
                    this.Add((Character)o);
                }
            }
        }

        public int Add(Character value)
        {
            return (List.Add(value));
        }

        public int IndexOf(Character value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, Character value)
        {
            List.Insert(index, value);
        }

        public void Remove(Character value)
        {
            List.Remove(value);
        }

        public bool Contains(Character value)
        {
            // If value is not of type Character, this will return false. 
            return (List.Contains(value));
        }
    }
    public class Character
    {
        public RoomImageCollection RoomImages;
        public Stream ScareScream;

        public string Name;

        public Character()
            : this(string.Empty, null, null)
        {
        }
        public Character(string name, RoomImageCollection roomImages, Stream scareScream)
        {
            this.Name = name;
            this.RoomImages = roomImages;
            this.ScareScream = scareScream;
        }
    }

    public abstract class GameEvent
    {
        public bool Complete;
        public TimeSpan SignalTime;

        public GameEvent()
        {

        }
    }

    public class SoundEvent : GameEvent
    {
        public Sound Sound;

        public SoundEvent()
        {

        }
        public SoundEvent(Sound sound)
        {
            this.Sound = sound;
        }
    }
    public class User
    {
        public Image MaskImage;
        public FormBase CurrentForm;
        public FormBase LastForm;

        public User()
        {

        }
    }

    public class GameEngine : ThreadBase
    {
        private static GameEvent[] _gameEventArray = new GameEvent[] {
            new SoundEvent() {
                Sound = new Sound(global::FNAF.Properties.Resources.Night1PhoneCall, false, true),
                SignalTime = new TimeSpan(0)
            }
        };
        private static CharacterCollection _characters = new CharacterCollection((new Character[] {
            #region OldBonnie
            new Character()
            {
                Name = "OldBonnie",
                ScareScream = global::FNAF.Properties.Resources.BonnieScream,
                RoomImages = new RoomImageCollection((new RoomImage[] {
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.OfficeFlashlightOldBonnieAndFoxyHall, 
                        ImageType = ImageType.FlashlightCharacterVisible, 
                        RoomType = RoomType.Office 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.LeftAirVentFlashlightOldBonnie, 
                        ImageType = ImageType.FlashlightCharacterVisible, 
                        RoomType = RoomType.Office 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.MainHallFlashlightOldBonnie, 
                        ImageType = ImageType.FlashlightCharacterVisible, 
                        RoomType = RoomType.MainHall 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.OfficeFlashlightOldBonnieHall, 
                        ImageType = ImageType.FlashlightCharacterVisible, 
                        RoomType = RoomType.Office 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.OldBonnieScare, 
                        ImageType = ImageType.CharacterScare, 
                        RoomType = RoomType.Office 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.PartyRoom1FlashlightOldBonnie, 
                        ImageType = ImageType.FlashlightCharacterVisible, 
                        RoomType = RoomType.PartyRoom1 
                    }
                }).ToList())
            },
            #endregion
            #region ToyBonnie
            new Character()
            {
                Name = "ToyBonnie",
                ScareScream = global::FNAF.Properties.Resources.BonnieScream,
                RoomImages = new RoomImageCollection((new RoomImage[] 
                {
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.OfficeRightAirVentToyBonnie, 
                        ImageType = ImageType.FlashlightCharacterVisible, 
                        RoomType = RoomType.Office 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.PartyRoom2FlashlightToyBonnie, 
                        ImageType = ImageType.FlashlightCharacterVisible, 
                        RoomType = RoomType.PartyRoom2 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.PartyRoom3FlashlightToyBonnie, 
                        ImageType = ImageType.FlashlightCharacterVisible, 
                        RoomType = RoomType.PartyRoom3 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.PartyRoom4FlashlightToyBonnie, 
                        ImageType = ImageType.FlashlightCharacterVisible, 
                        RoomType = RoomType.PartyRoom4 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.PartyRoom4NoFlashlightToyBonnie, 
                        ImageType = ImageType.CharacterVisible, 
                        RoomType = RoomType.Office 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.RightAirVentFlashlightToyBonnie, 
                        ImageType = ImageType.FlashlightCharacterVisible, 
                        RoomType = RoomType.Office 
                    },
                    new RoomImage() 
                    { 
                        Image = global::FNAF.Properties.Resources.ToyBonnieScare, 
                        ImageType = ImageType.CharacterScare, 
                        RoomType = RoomType.Office 
                    }
                }).ToList())
            },
            #endregion
            #region Mangle
            new Character()
            {
                Name = "Mangle",
                ScareScream = global::FNAF.Properties.Resources.ChicaScream,
                RoomImages = new RoomImageCollection((new RoomImage[] 
                {
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.GameAreaFlashlightBBAndMangle,
                        ImageType = ImageType.FlashlightCharacterVisible,
                        RoomType = RoomType.GameArea
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.KidsCoveFlashlightMangle,
                        ImageType = ImageType.FlashlightCharacterVisible,
                        RoomType = RoomType.KidsCove
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.KidsCoveNoFlashlightMangle,
                        ImageType = ImageType.CharacterVisible,
                        RoomType = RoomType.KidsCove
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.MainHallFlashlightMangle,
                        ImageType = ImageType.FlashlightCharacterVisible,
                        RoomType = RoomType.MainHall
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.MangleScare,
                        ImageType = ImageType.CharacterScare,
                        RoomType = RoomType.KidsCove
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.OfficeFlashlightMangleAndFoxyHall,
                        ImageType = ImageType.FlashlightCharacterVisible,
                        RoomType = RoomType.Office
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.OfficeFlashlightMangleHall,
                        ImageType = ImageType.FlashlightCharacterVisible,
                        RoomType = RoomType.Office
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.OfficeMangle,
                        ImageType = ImageType.CharacterVisible,
                        RoomType = RoomType.Office
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.OfficeRightAirVentMangle,
                        ImageType = ImageType.AirVentLightCharacterVisible,
                        RoomType = RoomType.Office
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.PartyRoom1FlashlightMangle,
                        ImageType = ImageType.FlashlightCharacterVisible,
                        RoomType = RoomType.PartyRoom1
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.PartyRoom2FlashlightMangle,
                        ImageType = ImageType.FlashlightCharacterVisible,
                        RoomType = RoomType.PartyRoom2
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.PrizeCornerFlashlightMangle,
                        ImageType = ImageType.FlashlightCharacterVisible,
                        RoomType = RoomType.PrizeCorner
                    },
                    new RoomImage()
                    {
                        Image = global::FNAF.Properties.Resources.RightAirVentFlashlightMangle,
                        ImageType = ImageType.FlashlightCharacterVisible,
                        RoomType = RoomType.RightAirVent
                    },
                }).ToList())
            }
            #endregion
        }).ToList());
        private static CameraForm[] _cameraForms = new CameraForm[] {
            #region Party Rooms
            new CameraForm()
            {
                RoomType = RoomType.PartyRoom1,
                DefaultImage = global::FNAF.Properties.Resources.PartyRoom1NoFlashlightNoCharacters,
                DefaultFlashlightOnImage = global::FNAF.Properties.Resources.PartyRoom1FlashlightEmpty,
                SupportsMask = false,
                SupportsFlashlight = true
            },
            new CameraForm()
            {
                RoomType = RoomType.PartyRoom2,
                DefaultImage = global::FNAF.Properties.Resources.PartyRoom2NoFlashlightNoCharacters,
                DefaultFlashlightOnImage = global::FNAF.Properties.Resources.PartyRoom2FlashlightNoCharacters,
                SupportsMask = false,
                SupportsFlashlight = true
            },
            new CameraForm()
            {
                RoomType = RoomType.PartyRoom3,
                DefaultImage = global::FNAF.Properties.Resources.PartyRoom3NoFlashlightNoCharacters,
                DefaultFlashlightOnImage = global::FNAF.Properties.Resources.PartyRoom3FlashlightNoCharacters,
                SupportsMask = false,
                SupportsFlashlight = true
            },
            new CameraForm()
            {
                RoomType = RoomType.PartyRoom4,
                DefaultImage = global::FNAF.Properties.Resources.PartyRoom4NoFlashlightNoCharacters,
                DefaultFlashlightOnImage = global::FNAF.Properties.Resources.PartyRoom4FlashlightNoCharacters,
                SupportsMask = false,
                SupportsFlashlight = true
            },
            #endregion
            #region Halls
            new CameraForm()
            {
                RoomType = RoomType.MainHall,
                DefaultImage = global::FNAF.Properties.Resources.MainHallNoFlashlightNoCharacters,
                DefaultFlashlightOnImage = global::FNAF.Properties.Resources.MainHallFlashlightNoCharacters,
                SupportsMask = false,
                SupportsFlashlight = true
            }
            #endregion
        };

        private DateTime _startTime;
        private System.Timers.Timer _gameTimer;
        private CancellationTokenSource _cancellationTokenSource;

        public User User;
        public ElapsedEventHandler GameTimeElapsed;

        public GameEngine(User user) : base("GameEngine")
        {
            this.User = user;
        }

        protected override void ThreadStart()
        {
            _gameTimer = new System.Timers.Timer();
            _cancellationTokenSource = new CancellationTokenSource();

            _gameTimer.Elapsed += GameTimer_Elapsed;

            _gameTimer.Interval = 5000;
            _gameTimer.Start();
            _startTime = DateTime.Now;

            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                continue;
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

        protected void GameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime signalTime = e.SignalTime;

            //
            // GameTimer elapses every set amount of time. each time this event is signaled iterate 
            // over each of the game events registered and Handle it according to type.
            //
            foreach (GameEvent gameEvent in _gameEventArray.ToList())
            {
                //
                // If the gameEvent is a sound event check to see if it has completed and whether 
                // the time the caller wants to play the sound has elapsed. If the time has 
                // elapsed and the sound has not completed play the sound according to the sound 
                // object embedded in the event.
                //
                if (gameEvent.GetType() == typeof(SoundEvent))
                {
                    SoundEvent soundEvent = (SoundEvent)gameEvent;

                    //
                    // Again skip any songs that are not yet ready to be played or have already 
                    // been played. Likely the complete constraint can be removed as eventually 
                    // logic will remove the game event once it's completed but for now gate on 
                    // the Complete property.
                    //
                    if (soundEvent.Complete || signalTime < _startTime.Add(soundEvent.SignalTime))
                    {
                        continue;
                    }

                    //
                    // Sound is ready to play queue it up for immediate playback
                    //
                    ThreadingEngine.GetThread<SoundEngine>().PlaySound(
                        soundEvent.Sound, 
                        SoundPriority.Immediate
                    );
                    soundEvent.Complete = true;
                }
                //
                // handle developer error not implementing game event types. If the developer is 
                // trying to handle inherited functionality this should be done using 
                // GameTimeElapsed Property from within the inheriting class.
                //
                else
                {
                    throw new NotImplementedException(string.Format(
                        "Event of type: {0} are not yet handled within GameTimer_Elapsed",
                        gameEvent.GetType().ToString()
                    ));
                }
            }

            //
            // Signal the inheriting objects time elapsed if one exists.
            //
            if (this.GameTimeElapsed != null)
            {
                this.GameTimeElapsed(sender, e);
            }

            return;
        }

        public CharacterCollection GetCharacters(RoomType roomType)
        {
            CharacterCollection characters = new CharacterCollection();

            foreach (Character character in _characters)
            {
                if (character.RoomImages.GetRoomImagesByType(roomType).Count > 0)
                {
                    characters.Add(character);
                }
            }

            return characters;
        }

        public CameraForm GetCameraForm(RoomType roomType)
        {
            foreach (CameraForm cameraForm in _cameraForms)
            {
                if (cameraForm.RoomType == roomType)
                {
                    return cameraForm;
                }
            }

            return new CameraForm();
        }

        public void UpdateForm(FormBase targetForm, FormBase sourceForm)
        {
            foreach (PropertyInfo property in typeof(FormBase).GetProperties())
            {
                foreach (Attribute attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is FormBaseAttribute)
                    {
                        object sourceFormValue = property.GetValue(sourceForm);

                        property.SetValue(targetForm, sourceFormValue);
                    }
                }
            }

            targetForm.Refresh();
            targetForm.Update();
        }

        public static void ShowForm(FormBase form, FormBase parent)
        {
            form.Show();

            if (parent != null) parent.Close();
        }
    }
}
