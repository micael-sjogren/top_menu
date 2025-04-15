using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu
{
    public static class ConfigurationLoader
    {
        public static Color backgroundColor;
        public static Color dropDownMenuTextColor;
        public static Color menuTextColor;
        public static Color menuTextColor_Inactive;
        public static Color menuBackgroundColor;
        public static Color borderLines;

        public static void LoadConfiguration()
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exePath + ".config");

            if (File.Exists(configFilePath))
            {
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFilePath };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                backgroundColor = GetColorSetting(config, "menuBar_DropDownButton_BackgroundColor");
                dropDownMenuTextColor = GetColorSetting(config, "menuBar_DropDownButton_TextColor");
                menuTextColor = GetColorSetting(config, "textColor");
                menuTextColor_Inactive = GetColorSetting(config, "textColor_Inactive");
                menuBackgroundColor = GetColorSetting(config, "backgroundColor");
                borderLines = GetColorSetting(config, "borderColor");
            }
            else
            {
                ErrorHandler.ShowError($"Configuration file '{configFilePath}' not found.");
            }
        }

        private static Color GetColorSetting(Configuration config, string key)
        {
            string colorName = config.AppSettings.Settings[key]?.Value;
            return Color.FromName(colorName ?? "DefaultColor"); // Handle default value
        }
    }

}
