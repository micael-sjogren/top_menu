using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu
{
    public class FormInitializer
    {
        public static void Initialize(Form form)
        {
            form.ShowInTaskbar = false;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + MenuHandler.x, Screen.PrimaryScreen.WorkingArea.Y + MenuHandler.y);
        }
    }

}
