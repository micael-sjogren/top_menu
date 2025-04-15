using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Menu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormInitializer.Initialize(this);

            // Dynamically load the configuration based on the current exe name
            ConfigurationLoader.LoadConfiguration();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Set some properties for Mainmenu
                MainMenu.ForeColor = ConfigurationLoader.menuTextColor_Inactive;
                this.Width = MenuHandler.MenuWidth;
                this.Height = MenuHandler.MenuHeight;
                MainMenu.Font = new Font("Helvetica", MenuHandler.TextSize);

                MenuHandler.populateListWithAllItemsInRootFolder(); // Populate list containing all items in rootFolder

                // Add all files and folders to root menu
                foreach (string rootItem in MenuHandler.allItemsInRootFolder)
                {
                    ToolStripMenuItem dirName = new ToolStripMenuItem(rootItem);
                    ToolStripMenuItem fileName = new ToolStripMenuItem(rootItem);

                    // For each folder in root-menu
                    // Create a dropdown menu in the root-menu bar(top level)
                    if (Directory.Exists(rootItem))
                    {
                        ToolStripMenuItem FolderMenu = new ToolStripMenuItem(dirName.Text); // Create Main Menu Item 
                        FolderMenu.Text = Path.GetFileName(FolderMenu.Text); // Get the filename 
                        FolderMenu.Text = FolderMenu.Text.Remove(0, 5);
                        MainMenu.Items.Add(FolderMenu); // Adds the "folders" to the root-Menu

                        // For each folder In the root-menu folder
                        // add the folders to the drop-down menu
                        foreach (string subFolder in System.IO.Directory.GetDirectories(rootItem))
                        {
                            string folderName = Path.GetFileName(subFolder);
                            string firstFourChar = new string(folderName.Take(4).ToArray());
                            int number = Int32.Parse(firstFourChar);

                                                      
                            ToolStripMenuItem subFolderMenu = new ToolStripMenuItem(subFolder); // Create sub-Menu Item 
                            subFolderMenu.BackColor = ConfigurationLoader.backgroundColor;
                            subFolderMenu.ForeColor = ConfigurationLoader.dropDownMenuTextColor;
                            subFolderMenu.Tag = subFolder; // Path to current item
                            subFolderMenu.Text = Path.GetFileName(subFolderMenu.Text);
                            subFolderMenu.Text = subFolderMenu.Text.Remove(0, 5);
                            if (number < 8000)
                            {
                                FolderMenu.DropDownItems.Add(subFolderMenu);
                            }
                            // Foreach item in the subfolder
                            foreach (string subitem in System.IO.Directory.GetFiles(subFolder))
                            {
                                ToolStripMenuItem subitemName = new ToolStripMenuItem(subitem);
                                subitemName.BackColor = ConfigurationLoader.menuBackgroundColor;
                                subitemName.ForeColor = ConfigurationLoader.menuTextColor;
                                subitemName.Tag = subitem; // Path to current item
                                subitemName.Text = Path.GetFileName(subitemName.Text);
                                subitemName.Text = subitemName.Text.Remove(0, 5);
                                subitemName.Text = Path.GetFileNameWithoutExtension(subitemName.Text);

                                // Adds separators
                                if (subitemName.Text == "x---------------x")
                                {
                                    ToolStripSeparator toolStripSeparator = new ExtendedToolStripSeparator();
                                    subFolderMenu.DropDownItems.Add(toolStripSeparator);
                                }
                                if (subitemName.Text != "x---------------x")
                                {
                                    subFolderMenu.DropDownItems.Add(subitemName);
                                    subitemName.MouseDown += new MouseEventHandler(subMenuItemClickHandler);
                                }
                            }
                        }

                        // For each file In the root-menu folder
                        // add the items to the dropdown menu 
                        foreach (string item in System.IO.Directory.GetFiles(rootItem))
                        {
                            ToolStripMenuItem itemName = new ToolStripMenuItem(item);
                            itemName.BackColor = ConfigurationLoader.menuBackgroundColor;
                            itemName.ForeColor = ConfigurationLoader.menuTextColor;
                            itemName.Tag = item; // Path to current item
                            itemName.Text = Path.GetFileName(itemName.Text);
                            itemName.Text = itemName.Text.Remove(0, 5);
                            itemName.Text = Path.GetFileNameWithoutExtension(itemName.Text);

                            // Adds separators
                            if (itemName.Text == "x---------------x")
                            {
                                ToolStripSeparator toolStripSeparator = new ExtendedToolStripSeparator();
                                FolderMenu.DropDownItems.Add(toolStripSeparator);
                            }
                            if (itemName.Text != "x---------------x")
                            {
                                FolderMenu.DropDownItems.Add(itemName);
                                itemName.MouseDown += new MouseEventHandler(subMenuItemClickHandler);
                            }
                        }
                    }

                    // Add all files to the menu-root
                    if (File.Exists(rootItem))
                    {
                        fileName.Tag = rootItem; // Path to current item
                        fileName.Text = Path.GetFileName(fileName.Text);
                        fileName.Text = fileName.Text.Remove(0, 5);
                        fileName.Text = Path.GetFileNameWithoutExtension(fileName.Text);
                        fileName.MouseDown += new MouseEventHandler(fileMenuItemClickHandler);
                        MainMenu.Items.Add(fileName); // Adds the files to the Main menu
                    }
                }
            }
            catch (System.Exception excep)
            {
                ErrorHandler.ShowError(excep.Message);
            }
        }

        // When clicking an item in the menu 
        private ProcessStarter processStarter = new ProcessStarter();
        public void fileMenuItemClickHandler(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem fileName = (ToolStripMenuItem)sender;
                processStarter.StartProcess(fileName.Tag.ToString());
            }
            catch (Exception excep)
            {
                ErrorHandler.ShowError(excep.Message);
            }
        }

        // When clicking on a sub menu item
        public void subMenuItemClickHandler(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem itemName = (ToolStripMenuItem)sender;
                string filePath = itemName.Tag.ToString();
                processStarter.StartProcess(filePath);
            }
            catch (Exception excep)
            {
                ErrorHandler.ShowError(excep.Message);
            }
        }


        // Activate the menu when mouse enter
        private void MainMenu_MouseEnter_1(object sender, EventArgs e)
        {
            this.Activate();
            MainMenu.ForeColor = ConfigurationLoader.menuTextColor;
        }

        // Change look of Toolstrip Separator 
        public class ExtendedToolStripSeparator : ToolStripSeparator
        {
            public ExtendedToolStripSeparator()
            {
                this.Paint += ExtendedToolStripSeparator_Paint;
            }

            private void ExtendedToolStripSeparator_Paint(object sender, PaintEventArgs e)
            {
                // Get the separator's width and height.
                ToolStripSeparator toolStripSeparator = (ToolStripSeparator)sender;
                int width = toolStripSeparator.Width;
                int height = toolStripSeparator.Height;

                Color foreColor = ConfigurationLoader.borderLines;
                Color backColor = ConfigurationLoader.menuBackgroundColor;

                // Fill the background.
                e.Graphics.FillRectangle(new SolidBrush(backColor), 0, 0, width, height);

                // Draw the line.
                e.Graphics.DrawLine(new Pen(foreColor), 4, height / 2, width - 4, height / 2);
            }
        }

        private void MainMenu_MouseLeave(object sender, EventArgs e)
        {
            MainMenu.ForeColor = ConfigurationLoader.menuTextColor_Inactive;
        }
    }
}

