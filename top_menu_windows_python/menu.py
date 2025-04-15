import tkinter as tk
from tkinter import ttk, messagebox
import configparser
import os
import re
import subprocess

def natural_sort_key(text):
    return [int(chunk) if chunk.isdigit() else chunk.lower() for chunk in re.split(r'(\d+)', text)]

class Form1(tk.Tk):
    def __init__(self, config_file=os.path.join(os.path.dirname(os.path.realpath(__file__)), "config.ini")):
        super().__init__()
        self.config_file = config_file
        self.load_config()

        self.title("Menu_2")
        self.overrideredirect(True)
        self.configure(background=self.backgroundColor)
        
        # Set window geometry
        self.geometry(f"{self.menuWidth}x{self.menuHeight}+{self.start_x}+{self.start_y}")

        # Create custom menu bar
        self.menu_bar = MenuBar(self)
        self.menu_bar.pack(fill='x')

        # Populate the menu after the menu bar is created
        self.populate_menu()

    def load_config(self):
        config = configparser.ConfigParser()
        config.read(self.config_file)

        settings = config['Settings']
        self.rootFolder = settings['rootFolder']
        self.menuWidth = int(settings['MenuWidth'])
        self.menuHeight = int(settings['MenuHeight'])
        self.textSize = int(settings['TextSize'])
        self.start_x = int(settings['Start_X-Position'])
        self.start_y = int(settings['Start_Y-Position'])
        
        def get_color(value, default="#FFFFFF"):
            """Ensure colors are in a valid Tkinter format."""
            value = value.strip().lower()
            if value.startswith("rgba"):
                match = re.search(r'rgba\((\d+),\s*(\d+),\s*(\d+)', value)
                if match:
                    r, g, b = map(int, match.groups())
                    return f"#{r:02x}{g:02x}{b:02x}"  
            elif value.startswith("#") and len(value) in (4, 7):
                return value  
            return default  

        self.backgroundColor = get_color(settings.get('backgroundColor', "#000000"))
        self.menuBarColor = get_color(settings.get('menuBarColor', "#000000"))
        self.textColor = get_color(settings.get('textColor', "#FFFF00"))
        self.borderColor = get_color(settings.get('borderColor', "#FFFF00"))
        self.textColorInactive = get_color(settings.get('textColor_Inactive', "#AAAAAA"))

    def open_item(self, item_path):
        try: 
            # Check the file extension
            _, ext = os.path.splitext(item_path)

            if ext == '.bat':
                # Run batch files using cmd
                subprocess.Popen(['cmd', '/c', item_path], shell=True)
            elif ext == '.exe':
                # Run executable files directly
                subprocess.Popen([item_path], shell=True)
            elif ext == '.ps1':
                # Run PowerShell scripts using PowerShell
                subprocess.Popen(['powershell', '-ExecutionPolicy', 'Bypass', '-File', item_path], shell=True)
            elif ext == '.py':
                # Run Python scripts using Python interpreter
                subprocess.Popen(['python', item_path], shell=True)
            else:
                # Open other file types with their default application
                os.startfile(item_path)
        except Exception as e:
            messagebox.showerror("Error", f"Failed to open file: {e}")


    def open_folder(self, folder_path):
        try:
            os.startfile(folder_path)
        except Exception as e:
            messagebox.showerror("Error", f"Failed to open folder: {e}")

    def populate_menu(self):
        items = sorted(os.listdir(self.rootFolder), key=natural_sort_key)
        print("Populating menu with items:")

        for item in items:
            item_path = os.path.join(self.rootFolder, item)

            # Check if the item starts with "9"
            if item.startswith("9"):
                print(f"Skipping {item} because it starts with '9'")
                continue

            # Process file name and remove the first 5 characters
            item_name, ext = os.path.splitext(item)
            item_name = item_name[5:]  # Remove the first five characters for display

            print(f"Adding {item_name} to menu")

            if os.path.isfile(item_path):
                self.menu_bar.add_menu_item(item_name, lambda p=item_path: self.open_item(p), self.textColor)
            elif os.path.isdir(item_path):
                self.menu_bar.add_folder_item(f"[{item_name}]", item_path)

class MenuBar(tk.Frame):
    def __init__(self, master=None):
        super().__init__(master, bd=0, relief='flat', background=master.menuBarColor)
        self.master = master
        self.configure(cursor='hand2')
        self.menus = {}

    def add_menu_item(self, label, command, text_color):
        button = tk.Button(self, text=label, command=command, 
                           background=self.master.menuBarColor, 
                           foreground=text_color, 
                           relief='flat',  # Remove the border by setting relief to 'flat'
                           activebackground=self.master.borderColor, 
                           activeforeground=self.master.textColor,
                           borderwidth=0,  # Remove the border width
                           highlightthickness=0)  # Remove the highlight border
        button.pack(side='left', padx=5, pady=2)

    def add_folder_item(self, label, folder_path):
        button = tk.Button(self, text=label, background=self.master.menuBarColor, 
                           foreground=self.master.textColor, activebackground=self.master.borderColor,
                           activeforeground=self.master.textColor, relief='flat',  # Set relief to 'flat'
                           borderwidth=0,  # Remove the border width
                           highlightthickness=0)  # Remove the highlight border

        menu = tk.Menu(self, tearoff=0, background=self.master.menuBarColor, foreground=self.master.textColor)
        self._populate_folder_menu(menu, folder_path)

        button.config(command=lambda: self.toggle_dropdown(menu, button))
        button.pack(side='left', padx=5, pady=2)
    
    def toggle_dropdown(self, menu, button):
        if menu.winfo_ismapped():
            menu.unpost()
        else:
            menu.post(button.winfo_rootx(), button.winfo_rooty() + button.winfo_height())

    def _populate_folder_menu(self, menu, folder_path):
        items = sorted(os.listdir(folder_path), key=natural_sort_key)
        
        for item in items:
            item_path = os.path.join(folder_path, item)
            item_name, ext = os.path.splitext(item)

            # Remove the first five characters for display
            item_name = item_name[5:]

            # Skip items starting with "9"
            if item.startswith("9"):
                continue

            if os.path.isdir(item_path):
                # Create a submenu for subfolders
                submenu = tk.Menu(menu, tearoff=0, background=self.master.menuBarColor, foreground=self.master.textColor)
                self._populate_folder_menu(submenu, item_path)  # Recursively populate
                menu.add_cascade(label=f"[{item_name}]", menu=submenu)
            elif os.path.isfile(item_path):
                menu.add_command(label=item_name, command=lambda p=item_path: self.master.open_item(p))

if __name__ == "__main__":
    app = Form1()
    app.mainloop()
