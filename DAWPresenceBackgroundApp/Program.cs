using Microsoft.Win32;
using System.Diagnostics;
using System.Net;

namespace DAWPresenceBackgroundApp;

public class DAWApplicationContext : ApplicationContext
{
    private readonly TrayIcon _trayIcon;

    public DAWApplicationContext()
    {
        _trayIcon = new TrayIcon();
    }
}

static class Program
{
    private static ProcessCode? processCode;
    private const string ProcessName = "DAWPresenceBackgroundApp";

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        // Check for an uninstall argument
        if (args.Length > 0 && args[0] == "/uninstall")
        {
            SetStartup(false);
            MessageBox.Show("Startup registry key was removed successfully.", "DAWPresence", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return; // Exit after cleaning up
        }

        ApplicationConfiguration.Initialize();
        ConfigurationManager.LoadConfiguration();

        SetStartup(ConfigurationManager.Configuration.OpenOnStartup);

        // If the program is started again, shut down all instances and exit
        if (Process.GetProcessesByName(ProcessName).Length > 1)
        {
            if (!ConfigurationManager.Configuration.DisablePopup)
            {
                MessageBox.Show("DAWPresence will now shut down.", "DAWPresence", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            foreach (var process in Process.GetProcessesByName(ProcessName))
            {
                process.Kill();
            }

            return;
        }

        if (!ConfigurationManager.Configuration.DisablePopup)
        {
            MessageBox.Show(
                "DAWPresence is now running in the background. You can exit DAWPresence by running the executable again.",
                "DAWPresence", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        _ = Task.Run(() => ProcessCode.ProcessCodeAsync());

        if (ConfigurationManager.Configuration.TrayIcon)
        {
            Application.Run(new DAWApplicationContext());
        }
        else
        {
            Application.Run();
        }
    }

    public static void SetStartup(bool enable)
    {
        const string keyName = "DAWPresence";
        string executablePath = Application.ExecutablePath;

        using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
        {
            if (key == null) return;

            if (enable)
            {
                key.SetValue(keyName, $"\"{executablePath}\"");
            }
            else
            {
                if (key.GetValue(keyName) != null)
                {
                    key.DeleteValue(keyName, false);
                }
            }
        }
    }
}