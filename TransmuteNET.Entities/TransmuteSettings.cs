using System.Text.Json.Serialization;

namespace TransmuteNET.Entities
{
    public class TransmuteSettings
    {
        [JsonPropertyName("theme")]
        public string? Theme { get; set; }

        [JsonPropertyName("auto_download")]
        public bool AutoDownload { get; set; }

        [JsonPropertyName("keep_originals")]
        public bool KeepOriginals { get; set; }

        [JsonPropertyName("cleanup_ttl_minutes")]
        public int CleanupTTLMinutes { get; set; }
    }
}