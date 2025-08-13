using DAWPresence;
using YamlDotNet.Serialization;

namespace DAWPresenceBackgroundApp;

public class ConfigurationManager
{
    // The name of the directory in AppData where the configuration file is stored
    private const string AppDataDirectoryName = "DAWPresence";
    // The path to the configuration file within AppData
    public static readonly string ConfigFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        AppDataDirectoryName,
        "config.yml"
    );

    public static AppConfiguration Configuration { get; private set; } = new();

    public static void SaveConfiguration()
    {
        var serializer = new SerializerBuilder().Build();
        var yaml = serializer.Serialize(Configuration);
        File.WriteAllText(ConfigFilePath, yaml);
        Console.WriteLine("Configuration Saved");
    }

    public static void LoadConfiguration()
    {
        string? configDirectory = Path.GetDirectoryName(ConfigFilePath);
        if (configDirectory != null && !Directory.Exists(configDirectory!))
        {
            Directory.CreateDirectory(configDirectory!);
        }

        if (File.Exists(ConfigFilePath))
        {
            var deserializer = new Deserializer();
            Configuration = deserializer.Deserialize<AppConfiguration>(File.ReadAllText(ConfigFilePath));
            Console.WriteLine("Configuration Loaded from /AppData");
        }
        else
        {
            Configuration = new AppConfiguration();
            SaveConfiguration(); // Save the initial config to the new location
            Console.WriteLine("Configuration Created at /AppData");
        }
    }
}