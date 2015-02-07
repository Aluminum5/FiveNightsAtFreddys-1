using FNAF.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNAF.Common
{
    public class ImageEx
    {
        public string Name;
        public Image Image;
        public ImageEx(string name, Image image)
        {
            this.Name = name;
            this.Image = image;
        }
    }
    public class AirVent
    {
        public string Name;
        public ImageEx ImageLightOff;
        public ImageEx ImageLightOn;
        public ImageEx ButtonLightOn;
        public ImageEx ButtonLightOff;
        public Point ButtonPoint;
        public Size ButtonSize;

        public AirVent(string name)
        {
            this.Name = name;
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
        public ImageEx ScareImage;
        public ImageEx VisibleImage;
        public ImageEx OtherImage;
        public Stream ScareScream;

        public string Name;

        public Character()
            : this(string.Empty, null, null, null, null)
        {
        }
        public Character(string name, ImageEx scareImage, ImageEx visibleImage, ImageEx otherImage, Stream scareScream)
        {
            this.Name = name;
            this.ScareImage = scareImage;
            this.VisibleImage = visibleImage;
            this.OtherImage = otherImage;
            this.ScareScream = scareScream;
        }
    }
    public class FormData
    {
        public string Name;
        public CharacterCollection Characters;
        public ImageEx DefaultImage;
        public ImageEx DefaultFlashlightOnImage;
        public bool SupportsFlashlight;
        public bool SupportsMask;
        public bool ShowCameraMap;
        public List<AirVent> AirVents;

        public FormData()
            :this("FormData", new CharacterCollection(), null, null)
        {

        }
        public FormData(
            string Name,
            CharacterCollection characters,
            ImageEx defaultImage, 
            ImageEx defaultFlashlightOnImage,
            bool supportsFlashlight = true, 
            bool supportsMask = false,
            bool showCameraMap = true
            )
        {
            this.Characters = characters;
            this.DefaultImage = defaultImage;
            this.DefaultFlashlightOnImage = defaultFlashlightOnImage;
            this.SupportsFlashlight = supportsFlashlight;
            this.SupportsMask = supportsMask;
            this.ShowCameraMap = showCameraMap;
            this.AirVents = new List<AirVent>();
        }
    }
}
