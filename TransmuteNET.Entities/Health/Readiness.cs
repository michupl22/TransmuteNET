using System.Text.Json.Serialization;

namespace TransmuteNET.Entities.Health
{
    public class Readiness
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("checks")]
        public ReadinessChecks? Checks { get; set; }
    }
}