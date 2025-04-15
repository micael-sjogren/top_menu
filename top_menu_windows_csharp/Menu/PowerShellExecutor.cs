using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Menu
{
    public static class PowerShellExecutor
    {
        public static void ExecuteScript(string filePath)
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
                string output = process.StandardOutput.ReadToEnd();
                string errorOutput = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(errorOutput))
                {
                    MessageBox.Show($"PowerShell Errors:\n{errorOutput}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to execute PowerShell script: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
