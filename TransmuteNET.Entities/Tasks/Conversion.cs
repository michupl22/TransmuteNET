using System.Text.Json.Serialization;
using TransmuteNET.Entities.Data;

namespace TransmuteNET.Entities.Tasks
{
    public class Conversion
    {
        [JsonPropertyName("id")]
        public string? ID { get; set; }

        [JsonPropertyName("output_format")]
        public string? OutputFormat { get; set; }

        [JsonPropertyName("quality")]
        public string? Quality { get; set; }

        public Conversion() { }

        public Conversion(TransmuteSource source, string outputFormat, string quality)
        {
            if (source == null || string.IsNullOrWhiteSpace(source.ID))
            {
                throw new InvalidOperationException();
            }

            ID = source.ID;
            OutputFormat = outputFormat;
            Quality = quality;
        }
    }
}