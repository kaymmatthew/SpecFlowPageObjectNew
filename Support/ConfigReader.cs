using System.Text.Json;

namespace SpecFlowPageObjectNew.Support
{
    public static class ConfigReader
    {
        public static string DemoQaUrl { get; private set; } = "";

        public static void Load(string filePath = "settings.json")
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Configuration file not found: {filePath}");

                var json = File.ReadAllText(filePath);
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("DemoQaUrl", out var url))
                {
                    DemoQaUrl = url.GetString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to read configuration", ex);
            }
        }
    }
}