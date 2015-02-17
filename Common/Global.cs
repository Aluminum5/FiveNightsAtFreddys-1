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
        public static User User;
    }
}
