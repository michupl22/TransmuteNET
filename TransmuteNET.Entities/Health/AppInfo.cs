using System.Text.Json.Serialization;

namespace TransmuteNET.Entities.Health
{
    public class AppInfo
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("version")]
        public string? Version { get; set; }
    }
}