using System.Configuration;
using System.Drawing;

namespace Menu
{
    public static class MenuSettings
    {
        public static Color BackgroundColor { get; private set; }
        public static Color DropDownMenuTextColor { get; private set; }
        public static Color MenuTextColor { get; private set; }
        public static Color MenuTextColorInactive { get; private set; }
        public static Color MenuBackgroundColor { get; private set; }
        public static Color BorderLines { get; private set; }
        public static int MenuWidth { get; private set; }
        public static int MenuHeight { get; private set; }
        public static int TextSize { get; private set; }
        public static int StartX { get; private set; }
        public static int StartY { get; private set; }

        public static void LoadSettings(Configuration config)
        {
            BackgroundColor = Color.FromName(config.AppSettings.Settings["menuBar_DropDownButton_BackgroundColor"].Value);
            DropDownMenuTextColor = Color.FromName(config.AppSettings.Settings["menuBar_DropDownButton_TextColor"].Value);
            MenuTextColor = Color.FromName(config.AppSettings.Settings["textColor"].Value);
            MenuTextColorInactive = Color.FromName(config.AppSettings.Settings["textColor_Inactive"].Value);
            MenuBackgroundColor = Color.FromName(config.AppSettings.Settings["backgroundColor"].Value);
            BorderLines = Color.FromName(config.AppSettings.Settings["borderColor"].Value);
            MenuWidth = int.Parse(config.AppSettings.Settings["MenuWidth"].Value);
            MenuHeight = int.Parse(config.AppSettings.Settings["MenuHeight"].Value);
            TextSize = int.Parse(config.AppSettings.Settings["TextSize"].Value);
            StartX = int.Parse(config.AppSettings.Settings["Start_X-Position"].Value);
            StartY = int.Parse(config.AppSettings.Settings["Start_Y-Position"].Value);
        }
    }
}
