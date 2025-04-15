using System.Windows.Forms;

public static class ErrorHandler
{
    public static void ShowError(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
