using System.Text.Json.Serialization;
using TransmuteNET.Entities.Data;

namespace TransmuteNET.Entities.Tasks
{
    public class BatchDownload
    {
        [JsonPropertyName("file_ids")]
        public string[] FileIds { get; set; }

        public BatchDownload(IEnumerable<IFileMetadata> files)
        {
            FileIds = [.. files.Where(x => x.ID != null).Select(x => x.ID!)];
        }
    }
}