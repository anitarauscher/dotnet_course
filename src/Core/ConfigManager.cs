using System.IO;
using System.Text.Json.Nodes;

namespace Epam.Automation.src.Core
{
    public class ConfigManager
    {
        private static JsonNode _config;

        static ConfigManager()
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "config.json");
            _config = JsonNode.Parse(File.ReadAllText(configPath))!;
        }

        private static ConfigManager _instance = new ConfigManager();
        public static ConfigManager Instance => _instance;

        public static string BaseUrl => _config["BaseUrl"]!.ToString();
        public static string ApiUrl => _config["ApiUrl"]!.ToString();
        public static int ApiTimeoutInSeconds => _config["ApiTimeoutInSeconds"]?.GetValue<int>() ?? 10;

        public string Get(string key) => _config[key]?.ToString() ?? string.Empty;

    }
}