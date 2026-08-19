using System.Text.Json.Serialization;

namespace TransmuteNET.Entities.Data
{
    public class TransmuteSourceCollection
    {
        [JsonPropertyName("files")]
        public TransmuteSource[] Files { get; set; } = [];
    }
}