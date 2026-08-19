using System.Net;
using TransmuteNET.Entities;
using TransmuteNET.Entities.Data;
using TransmuteNET.Entities.Health;
using TransmuteNET.Entities.Tasks;

namespace TransmuteNET.Core
{
    public interface ITransmuteService
    {
        bool Success { get; }

        HttpStatusCode StatusCode { get; }

        UploadResult Upload(byte[] bytes, string fileName);

        TransmuteConverted Convert(Conversion conversion);

        byte[] Download(IFileMetadata file);

        byte[] Download(IFileMetadata[] files);

        TransmuteSource[] GetFiles();

        TransmuteConverted[] GetCompletedConversions();

        void Delete(TransmuteSource source);

        void Delete(TransmuteConverted converted);

        void DeleteAll();

        Liveness GetLiveness();

        Readiness GetReadiness();

        AppInfo GetInfo();

        TransmuteSettings GetSettings();

        void Update(TransmuteSettings settings);
    }
}