using RestSharp;

namespace TransmuteNET.Core
{
    public class TransmuteRequests
    {
        public static RestRequest UploadFile => new("api/files", Method.Post);

        public static RestRequest Conversion => new("api/conversions", Method.Post);

        public static RestRequest Download => new("api/files/{id}", Method.Get);

        public static RestRequest BatchDownload => new("api/files/batch", Method.Post);

        public static RestRequest Files => new("api/files", Method.Get);

        public static RestRequest CompletedConversions => new("api/conversions/complete", Method.Get);

        public static RestRequest DeleteFile => new("api/files/{id}", Method.Delete);

        public static RestRequest DeleteConversion => new("api/conversions/{id}", Method.Delete);

        public static RestRequest DeleteFiles => new("api/files/all", Method.Delete);

        public static RestRequest DeleteConverted => new("api/conversions/all", Method.Delete);

        public static RestRequest HealthLive => new("api/health/live", Method.Get);

        public static RestRequest HealthReady => new("api/health/ready", Method.Get);

        public static RestRequest HealthInfo => new("api/health/info", Method.Get);

        public static RestRequest Settings => new("api/settings", Method.Get);

        public static RestRequest UpdateSettings => new("api/settings", Method.Patch);
    }
}