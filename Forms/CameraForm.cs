using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FNAF.Common;
using FNAF.Controls;
using System.Media;
using FNAF.Engines;
using System.Threading;

namespace FNAF.Forms
{
    public partial class CameraForm : FormBase
    {
        public CharacterCollection Characters;        

        public CameraForm() : this("RoomForm")
        {
        }
        public CameraForm(string name) : this(null, name)
        {
        }
        public CameraForm(Sound sound, string name)
            : base(name)
        {
            InitializeComponent();

            if (Sound != null)
            {
                base.Sound = ThreadingEngine.GetThread<SoundEngine>().PlaySound(Sound);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (base.Sound != null)
            {
                ThreadingEngine.GetThread<SoundEngine>().StopSound(base.Sound);
            }

            base.OnClosing(e);
        }
    }
}
