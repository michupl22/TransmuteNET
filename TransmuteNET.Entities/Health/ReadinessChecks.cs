using System.Text.Json.Serialization;

namespace TransmuteNET.Entities.Health
{
    public class ReadinessChecks
    {
        [JsonPropertyName("database")]
        public string? Database { get; set; }

        [JsonPropertyName("storage")]
        public string? Storage { get; set; }
    }
}