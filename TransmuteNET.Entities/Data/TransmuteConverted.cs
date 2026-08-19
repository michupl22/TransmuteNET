using System.Text.Json.Serialization;

namespace TransmuteNET.Entities.Data
{
    public class TransmuteConverted : IFileMetadata
    {
        [JsonPropertyName("id")]
        public string? ID { get; set; }

        [JsonPropertyName("original_filename")]
        public string? OrginalFileName { get; set; }

        [JsonPropertyName("media_type")]
        public string? MediaType { get; set; }

        [JsonPropertyName("extension")]
        public string? Extension { get; set; }

        [JsonPropertyName("size_bytes")]
        public long SizeBytes { get; set; }

        [JsonPropertyName("sha256_checksum")]
        public string? Checksum { get; set; }
    }
}