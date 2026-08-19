using RestSharp;
using System.Net;
using System.Text.Json;
using TransmuteNET.Core.Exceptions;
using TransmuteNET.Entities;
using TransmuteNET.Entities.Data;
using TransmuteNET.Entities.Health;
using TransmuteNET.Entities.Tasks;

namespace TransmuteNET.Core
{
    public class TransmuteService : ITransmuteService
    {
        private readonly RestClient _client;

        private bool? _success;
        private HttpStatusCode? _statusCode;

        public bool Success
        {
            get
            {
                if (!_success.HasValue)
                {
                    throw new InvalidOperationException();
                }

                return _success.Value;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                if (!_statusCode.HasValue)
                {
                    throw new InvalidOperationException();
                }

                return _statusCode.Value;
            }
        }

        public TransmuteService(TransmuteConfig config)
        {
            RestClientOptions options = new()
            {
                BaseUrl = new Uri(config.Address)
            };

            Dictionary<string, string> headers = new()
            {
                { "Authorization", $"Bearer {config.ApiKey}" }
            };

            _client = new RestClient(options);
            _client.AddDefaultHeaders(headers);
        }

        public UploadResult Upload(byte[] bytes, string fileName)
        {
            RestRequest request = TransmuteRequests.UploadFile;

            request.AddFile("file", bytes, fileName);

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new EmptyResponseException();

            UploadResult? result = JsonSerializer.Deserialize<UploadResult>(response.Content);
            return result ?? throw new InvalidDataException("The send result cannot be retrieved");
        }

        public TransmuteConverted Convert(Conversion conversion)
        {
            RestRequest request = TransmuteRequests.Conversion;

            request.AddBody(conversion, ContentType.Json);

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new EmptyResponseException();

            TransmuteConverted? convertedFile = JsonSerializer.Deserialize<TransmuteConverted>(response.Content);
            return convertedFile ?? throw new InvalidDataException("The converted file cannot be retrieved");
        }

        public byte[] Download(IFileMetadata file)
        {
            RestRequest request = TransmuteRequests.Download;

            request.AddUrlSegment("id", file.ID);

            byte[]? binaryFile = _client.DownloadData(request);
            return binaryFile ?? throw new InvalidDataException("The downloaded file cannot be accessed");
        }

        public byte[] Download(IFileMetadata[] files)
        {
            RestRequest request = TransmuteRequests.BatchDownload;
            BatchDownload batchDownload = new(files);

            request.AddBody(batchDownload, ContentType.Json);

            byte[]? binaryFile = _client.DownloadData(request);
            return binaryFile ?? throw new InvalidDataException("The downloaded file cannot be accessed");
        }

        public TransmuteSource[] GetFiles()
        {
            RestRequest request = TransmuteRequests.Files;

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new EmptyResponseException();

            TransmuteSourceCollection? sourceFiles = JsonSerializer.Deserialize<TransmuteSourceCollection>(response.Content);
            return sourceFiles != null ? sourceFiles.Files : throw new InvalidDataException("Unable to retrieve the list of files");
        }

        public TransmuteConverted[] GetCompletedConversions()
        {
            RestRequest request = TransmuteRequests.CompletedConversions;

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new EmptyResponseException();

            TransmuteConvertedCollection? conversionsFiles = JsonSerializer.Deserialize<TransmuteConvertedCollection>(response.Content);
            return conversionsFiles != null ? conversionsFiles.Files : throw new InvalidDataException("Unable to retrieve a list of converted files");
        }

        public void Delete(TransmuteSource source)
        {
            RestRequest request = TransmuteRequests.DeleteFile;

            request.AddUrlSegment("id", source.ID);

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;
        }

        public void Delete(TransmuteConverted converted)
        {
            RestRequest request = TransmuteRequests.DeleteConversion;

            request.AddUrlSegment("id", converted.ID);

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;
        }

        public void DeleteAll()
        {
            RestRequest[] requests = [
                TransmuteRequests.DeleteFiles,
                TransmuteRequests.DeleteConverted
            ];

            List<RestResponse> responses = [];

            foreach (RestRequest request in requests)
            {
                RestResponse response = _client.Execute(request);
                responses.Add(response);
            }

            _success = responses.All(x => x.IsSuccessful);
            _statusCode = responses.Last().StatusCode;
        }

        public Liveness GetLiveness()
        {
            RestRequest request = TransmuteRequests.HealthLive;

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new EmptyResponseException();

            Liveness? result = JsonSerializer.Deserialize<Liveness>(response.Content);
            return result ?? throw new InvalidDataException("Unable to retrieve information about the service's remaining liveness");
        }

        public Readiness GetReadiness()
        {
            RestRequest request = TransmuteRequests.HealthReady;

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new EmptyResponseException();

            Readiness? result = JsonSerializer.Deserialize<Readiness>(response.Content);
            return result ?? throw new InvalidDataException("Unable to retrieve information about the service's readiness status");
        }

        public AppInfo GetInfo()
        {
            RestRequest request = TransmuteRequests.HealthInfo;

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new EmptyResponseException();

            AppInfo? result = JsonSerializer.Deserialize<AppInfo>(response.Content);
            return result ?? throw new InvalidDataException("Unable to retrieve information about the app");
        }

        public TransmuteSettings GetSettings()
        {
            RestRequest request = TransmuteRequests.Settings;

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new EmptyResponseException();

            TransmuteSettings? result = JsonSerializer.Deserialize<TransmuteSettings>(response.Content);
            return result ?? throw new InvalidDataException("Unable to retrieve information about the app's settings");
        }

        public void Update(TransmuteSettings settings)
        {
            RestRequest request = TransmuteRequests.UpdateSettings;

            request.AddBody(settings, ContentType.Json);

            RestResponse response = _client.Execute(request);
            _success = response.IsSuccessful;
            _statusCode = response.StatusCode;
        }
    }
}