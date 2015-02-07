using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FNAF
{
    class WindowControls
    {
        public static void ShowForm(Form form, Control control, bool closeParentForm)
        {
            Control parent = control.Parent;
            
            while (!(parent is Form))
            {
                parent = parent.Parent;
            }

            Form parentForm = (Form)parent;

            form.Show();
            parentForm.Close();
        }

        public static void ShowForm(Form form)
        {
            form.Show();
        }

        public static void LowerCameras(Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is Button)
                {
                    control.Visible = false;
                }
            }

        }
    }
}
