using DAWPresence;
using DiscordRPC;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using YamlDotNet.Serialization;

namespace DAWPresenceBackgroundApp;

public class ProcessCode
{
    private const string AppVersion = "0.1.18";
    private const int SwHide = 0;
    private const string CreditText = "DAWPresence by @myuuiii, improved by @Simoxus";
    private static DiscordRpcClient? _client;
    private static DateTime? _startTime;

    public static async Task ProcessCodeAsync()
    {
        ApplicationConfiguration.Initialize();
        await CheckLatestVersion();
        await ExecuteTaskAsync();
    }

    private static async Task CheckLatestVersion()
    {
        if (ConfigurationManager.Configuration.CheckForUpdates)
        {
            try
            {
                using var client = new HttpClient();

                // Fetch the latest version string from the GitHub repo
                var latestVersionRaw = await client.GetStringAsync("https://raw.githubusercontent.com/Simoxus/DAWPresence/main/VERSION.txt");
                var latestVersionString = latestVersionRaw.Trim();

                Console.WriteLine($"Latest version: {latestVersionString}. Sending request to user for update.");

                // Parse both version strings into System.Version objects
                if (Version.TryParse(AppVersion, out Version? currentVersion) &&
                    Version.TryParse(latestVersionString, out Version? latestVersion))
                {
                    // Check if the latest version is greater than the current version
                    if (latestVersion > currentVersion)
                    {
                        DialogResult dialogResult = MessageBox.Show(
                            $"A new version of DAWPresence is available: {latestVersionString}. Do you want to download it from the official GitHub page?",
                            "DAWPresence", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                // Open the GitHub page using the default browser
                                Process.Start(new ProcessStartInfo("https://github.com/Simoxus/DAWPresence") { UseShellExecute = true });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Could not open the GitHub link: {ex.Message}", "DAWPresence",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Could not parse version numbers for CheckLatestVersion function.");
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"An error occurred while checking for updates: {e.Message}", "DAWPresence",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    protected static async Task ExecuteTaskAsync()
    {
        IEnumerable<Daw?> dawInstances = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Daw)))
            .Select(t => (Daw?)Activator.CreateInstance(t));

        var registeredDawArray = dawInstances as Daw[] ?? dawInstances.ToArray();
        foreach (var daw in registeredDawArray)
        {
            Console.WriteLine(
                $"{daw?.DisplayName ?? "A null DAW instance was found in registeredDawArray"} has been registered");
        }

        while (true)
        {
            var runningDaw = registeredDawArray.FirstOrDefault(d => d.IsRunning);
            if (runningDaw is null)
            {
                _client?.ClearPresence();
                _client?.Dispose();
                _client = null;
                _startTime = null;
                Console.WriteLine("No DAW is running");
                await Task.Delay(ConfigurationManager.Configuration.UpdateInterval);
                continue;
            }

            _startTime ??= DateTime.UtcNow;
            Console.WriteLine("Detected: " + runningDaw.DisplayName);
            Console.WriteLine("Project: " + runningDaw.GetProjectNameFromProcessWindow());

            if (_client is null || _client.ApplicationID != runningDaw.ApplicationId)
            {
                _client?.ClearPresence();
                _client?.Dispose();
                _client = new DiscordRpcClient(runningDaw.ApplicationId);
                _client.Initialize();
            }

            _client.SetPresence(new RichPresence
            {
                Details = !runningDaw.HideDetails && !string.IsNullOrEmpty(runningDaw.GetProjectNameFromProcessWindow())
                    ? ConfigurationManager.Configuration.WorkingPrefixText + runningDaw.GetProjectNameFromProcessWindow()
                    : runningDaw.HideDetails ? null : ConfigurationManager.Configuration.IdleText,
                State = "",
                Assets = new Assets
                {
                    LargeImageKey = ConfigurationManager.Configuration.UseCustomImage
                        ? ConfigurationManager.Configuration.CustomImageKey
                        : runningDaw.ImageKey,
                    LargeImageText = CreditText
                },
                Timestamps = new Timestamps
                {
                    Start = _startTime?.Add(-ConfigurationManager.Configuration.Offset)
                }
            });

            await Task.Delay(ConfigurationManager.Configuration.UpdateInterval);
        }
    }
}