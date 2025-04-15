using System.Diagnostics;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Menu
{
    public class MenuBuilder
    {
        private MenuStrip _mainMenu;

        public MenuBuilder(MenuStrip mainMenu)
        {
            _mainMenu = mainMenu;
        }

        public void BuildMenu()
        {
            MenuHandler.populateListWithAllItemsInRootFolder();

            foreach (string rootItem in MenuHandler.allItemsInRootFolder)
            {
                if (Directory.Exists(rootItem))
                {
                    AddFolderToMenu(rootItem);
                }
                else if (File.Exists(rootItem))
                {
                    AddFileToMenu(rootItem);
                }
            }
        }

        private void AddFolderToMenu(string folder)
        {
            ToolStripMenuItem FolderMenu = new ToolStripMenuItem
            {
                Text = Path.GetFileName(folder).Remove(0, 5)
            };

            foreach (string subFolder in Directory.GetDirectories(folder))
            {
                if (int.Parse(new string(Path.GetFileName(subFolder).Take(4).ToArray())) < 8000)
                {
                    ToolStripMenuItem subFolderMenu = CreateSubFolderMenu(subFolder);
                    FolderMenu.DropDownItems.Add(subFolderMenu);
                }
            }

            foreach (string item in Directory.GetFiles(folder))
            {
                ToolStripMenuItem itemMenu = CreateFileMenu(item);
                FolderMenu.DropDownItems.Add(itemMenu);
            }

            _mainMenu.Items.Add(FolderMenu);
        }

        private ToolStripMenuItem CreateSubFolderMenu(string subFolder)
        {
            var subFolderMenu = new ToolStripMenuItem(Path.GetFileName(subFolder).Remove(0, 5))
            {
                Tag = subFolder,
                BackColor = MenuSettings.BackgroundColor,
                ForeColor = MenuSettings.DropDownMenuTextColor
            };

            foreach (string subitem in Directory.GetFiles(subFolder))
            {
                ToolStripMenuItem subitemMenu = CreateFileMenu(subitem);
                subFolderMenu.DropDownItems.Add(subitemMenu);
            }

            return subFolderMenu;
        }

        private ToolStripMenuItem CreateFileMenu(string file)
        {
            var fileMenu = new ToolStripMenuItem(Path.GetFileNameWithoutExtension(file))
            {
                Tag = file,
                BackColor = MenuSettings.MenuBackgroundColor,
                ForeColor = MenuSettings.MenuTextColor
            };

            if (fileMenu.Text == "x---------------x")
            {
                fileMenu = new ToolStripSeparator();
            }

            fileMenu.MouseDown += FileMenuItemClickHandler;
            return fileMenu;
        }

        private void FileMenuItemClickHandler(object sender, EventArgs e)
        {
            var fileName = (ToolStripMenuItem)sender;
            Process.Start(fileName.Tag.ToString());
        }
    }
}
