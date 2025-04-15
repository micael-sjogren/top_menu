# Minimalistic Menu

A simple and minimalist menu application that allows you to execute scripts, launch programs, and explore endless possibilities! Use this menu to organize and manage your scripts and applications effortlessly.

## Features 

- Execute batch files, PowerShell scripts, Python scripts, shortcuts, executable files, and more.
- Highly customizable menu settings for positioning, colors, and display.
- No installation needed, its a portable application so you can run it from your pc or usb and so on.

## Quickstart
You can try out the example to get started, extract it to C:\menu\ or adjust the config file.
[example.zip](https://github.com/user-attachments/files/17560440/example.zip)

## Prerequisites

- **Python**: If you plan to execute Python scripts, ensure Python is installed on your computer.

## Setup Instructions

1. **Copy Files**:
   - Copy `Main.exe` and `Main.exe.config` to a desired folder on your computer (e.g., `C:\menu\Main\8500_Source`).

2. **Configure the Menu**:
   - Open `Main.exe.config` and set the path to your menu items (e.g., `C:\menu\Main\`).

3. **Add Menu Scripts**:
   - Place your script files in the configured directory (e.g., `C:\menu\Main\`).

4. **Using Python Scripts**:
   - Ensure Python is installed on your system to execute Python scripts from the menu.


## Menu Folder Setup
The menu is using the folder structure to arrange the menu items, see example below.

Menu

![image](https://github.com/user-attachments/assets/0bd38429-109b-4e70-b66f-6ff1851966c4)


Folder

![image](https://github.com/user-attachments/assets/637c10f8-653f-4eb1-8b8e-ef59a8d24f6d)


Submenu

![image](https://github.com/user-attachments/assets/3bd89899-dce9-42d1-bd94-8897ac14bd87)


Subfolder

![image](https://github.com/user-attachments/assets/cb0bde3f-799e-422f-a22d-8b3475cdb450)






## File Naming Convention

To ensure proper sorting in the menu:

- **Prefix**: Add a four-digit number followed by an underscore at the beginning of all files and folders (e.g., `1000_example_script.bat`).
- **Hiding**: If the four-digit prefix is higher than `8000`, that item will not appear in the menu.
- **Dividers**: To create a divider in the submenu, create an empty text file named `1000_x---------------x.txt`.

## Customization

Feel free to explore and modify the `Main.exe.config` settings to adjust:

- **Positioning**: Change the layout of the menu items.
- **Colors**: Customize the visual appearance of the menu.


![image](https://github.com/user-attachments/assets/462e1333-e548-44dc-8a58-92e7eefcc6a4)
