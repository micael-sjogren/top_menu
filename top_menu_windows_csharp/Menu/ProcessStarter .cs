using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Menu
{
    public class ProcessStarter
    {
        // Starts a process based on the file type or configuration
        public void StartProcess(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            if (extension == ".ps1")
            {
                StartPowerShellScript(filePath);
            }
            else if (extension == ".py")
            {
                StartPython(filePath);
            }
            else
            {
                StartGeneralProcess(filePath);
            }
        }

        // Method to start a PowerShell script
        private void StartPowerShellScript(string filePath)
        {
            try
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        FileName = "C:\\windows\\system32\\windowspowershell\\v1.0\\powershell.exe",
                        Arguments = $"-ExecutionPolicy Bypass -File \"{filePath}\""
                    }
                };

                process.Start();

                // Capture the output and errors
                string output = process.StandardOutput.ReadToEnd();
                string errorOutput = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Display output or errors if needed
                if (!string.IsNullOrEmpty(output))
                {
                    // MessageBox.Show($"PowerShell Output:\n{output}");
                }

                if (!string.IsNullOrEmpty(errorOutput))
                {
                    ErrorHandler.ShowError($"PowerShell Errors:\n{errorOutput}");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Failed to execute PowerShell script: {ex.Message}");
            }
        }

        // Method to start a Python script
        private void StartPython(string filePath)
        {
            try
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        FileName = "python", // Ensure Python is in your system's PATH environment variable
                        Arguments = $"\"{filePath}\""
                    }
                };

                process.Start();

                // Capture the output and errors
                string output = process.StandardOutput.ReadToEnd();
                string errorOutput = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Display output or errors if needed
                if (!string.IsNullOrEmpty(output))
                {
                    // MessageBox.Show($"Python Output:\n{output}");
                }

                if (!string.IsNullOrEmpty(errorOutput))
                {
                    ErrorHandler.ShowError($"Python Errors:\n{errorOutput}");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Failed to execute Python script: {ex.Message}");
            }
        }

        // Method to start a general process (like opening files or executables)
        private void StartGeneralProcess(string filePath)
        {
            try
            {
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Failed to start the process: {ex.Message}");
            }
        }
    }
}
