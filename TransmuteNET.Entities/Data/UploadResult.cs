using System.Text.Json.Serialization;

namespace TransmuteNET.Entities.Data
{
    public class UploadResult
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("metadata")]
        public TransmuteSource? Source { get; set; }
    }
}