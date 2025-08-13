namespace DAWPresence;

public class AppConfiguration
{
    /// Presence Update Settings \\

    /// <summary>
    /// How often the app checks for a running DAW and updates the Discord presence.
    /// </summary>
    public TimeSpan UpdateInterval { get; set; } = new(0, 0, 3);

    /// <summary>
    /// Time offset to add to the elapsed time display (e.g., resuming after a pause).
    /// </summary>
    public TimeSpan Offset { get; set; } = TimeSpan.Zero;

    // Display Text Settings \\

    /// <summary>
    /// Text shown in the presence when no project is open.
    /// </summary>
    public string IdleText { get; set; } = "Not working on a project";

    /// <summary>
    /// Text shown before the project name when a project is open.
    /// </summary>
    public string WorkingPrefixText { get; set; } = "Working on ";

    // Image Settings \\

    /// <summary>
    /// Whether to use a custom image key instead of the default.
    /// </summary>
    public bool UseCustomImage { get; set; } = false;

    /// <summary>
    /// Custom Discord asset image key to use if <see cref="UseCustomImage"/> is true.
    /// </summary>
    public string CustomImageKey { get; set; } = "myuuiii";

    // App Behavior Settings \\

    /// <summary>
    /// Whether the application should open on startup.
    /// </summary>
    public bool OpenOnStartup { get; set; } = false;

    /// <summary>
    /// Show a system tray icon while the app is running.
    /// </summary>
    public bool TrayIcon { get; set; } = true;

    /// <summary>
    /// Suppress the startup popup window.
    /// </summary>
    public bool DisablePopup { get; set; } = false;

    /// <summary>
    /// Check for version updates when the app starts.
    /// </summary>
    public bool CheckForUpdates { get; set; } = false;

    /// <summary>
    /// Enable hot reloading of the configuration file (for debugging and development).
    /// </summary>
    public bool Debug { get; set; } = false;
}