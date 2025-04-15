using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Menu
{
    public class ConfigurationService
    {
        public void LoadConfiguration(string exePath)
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exePath + ".config");

            if (File.Exists(configFilePath))
            {
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFilePath };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                MenuSettings.LoadSettings(config);
            }
            else
            {
                MessageBox.Show($"Configuration file '{configFilePath}' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
