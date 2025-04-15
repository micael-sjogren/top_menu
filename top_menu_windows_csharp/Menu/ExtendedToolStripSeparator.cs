using System.Drawing;
using System.Windows.Forms;

namespace Menu
{
    public class ExtendedToolStripSeparator : ToolStripSeparator
    {
        public ExtendedToolStripSeparator()
        {
            this.Paint += ExtendedToolStripSeparator_Paint;
        }

        private void ExtendedToolStripSeparator_Paint(object sender, PaintEventArgs e)
        {
            var separator = (ToolStripSeparator)sender;
            int width = separator.Width;
            int height = separator.Height;

            Color foreColor = MenuSettings.BorderLines;
            Color backColor = MenuSettings.MenuBackgroundColor;

            e.Graphics.FillRectangle(new SolidBrush(backColor), 0, 0, width, height);
            e.Graphics.DrawLine(new Pen(foreColor), 4, height / 2, width - 4, height / 2);
        }
    }
}
