using System.Text.Json.Serialization;

namespace TransmuteNET.Entities.Data
{
    public class TransmuteConvertedCollection
    {
        [JsonPropertyName("conversions")]
        public TransmuteConverted[] Files { get; set; } = [];
    }
}