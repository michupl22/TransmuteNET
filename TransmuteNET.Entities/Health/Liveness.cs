using System.Text.Json.Serialization;

namespace TransmuteNET.Entities.Health
{
    public class Liveness
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}