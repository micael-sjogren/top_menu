using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;



namespace Menu
{
    public static class MenuHandler
    {
        public static List<string> allItemsInRootFolder = new List<string>();
        public static List<string> folderAndFiles = new List<string>();
        public static List<string> subFolderContent = new List<string>();

        public static string rootFolder = (ConfigurationManager.AppSettings["rootFolder"]);
        public static int MenuWidth = Int32.Parse(ConfigurationManager.AppSettings["MenuWidth"]);
        public static int MenuHeight = Int32.Parse(ConfigurationManager.AppSettings["MenuHeight"]);
        public static int TextSize = Int32.Parse(ConfigurationManager.AppSettings["TextSize"]);
        public static int x = Int32.Parse(ConfigurationManager.AppSettings["Start_X-Position"]);
        public static int y = Int32.Parse(ConfigurationManager.AppSettings["Start_Y-Position"]);

        // Populate the allItemsInRootFolder list
        public static void populateListWithAllItemsInRootFolder()
        {
            List<string> folders = new List<string>();
            List<string> files = new List<string>();

            // Populate folders with the condition that the number in the name is less than 8000
            foreach (string folder in Directory.GetDirectories(rootFolder))
            {
                string folderName = Path.GetFileName(folder);
                string firstFourChar = new string(folderName.Take(4).ToArray());
                int number = Int32.Parse(firstFourChar);

                if (number < 8000)
                {
                    folders.Add(folder);
                }
            }

            // Add files to the files list
            foreach (string file in Directory.GetFiles(rootFolder))
            {
                files.Add(file);
            }

            // Combine folders and files, then sort alphabetically
            allItemsInRootFolder = folders.Concat(files).OrderBy(item => Path.GetFileName(item)).ToList();
        }

        public static void displayMenu()
        {
            Console.SetWindowSize(MenuWidth, MenuHeight);
            Console.SetCursorPosition(x, y);

            // Display the menu
            for (int i = 0; i < allItemsInRootFolder.Count; i++)
            {
                string item = Path.GetFileName(allItemsInRootFolder[i]);
                Console.WriteLine(item);
            }
        }
    }
}
