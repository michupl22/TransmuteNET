namespace TransmuteNET.Entities.Data
{
    public interface IFileMetadata
    {
        string? ID { get; set; }

        string? OrginalFileName { get; set; }

        string? MediaType { get; set; }

        string? Extension { get; set; }

        long SizeBytes { get; set; }

        string? Checksum { get; set; }
    }
}