namespace TransmuteNET.Tests.Resources
{
    internal class TransmuteParameters
    {
        public static string Host
        {
            get
            {
                string? address = Environment.GetEnvironmentVariable("TRANSMUTE_HOST");

                if (string.IsNullOrWhiteSpace(address))
                    throw new OperationCanceledException("You need to define the host in the TRANSMUTE_HOST environment variable. Did you load the correct test.runsettings file?");

                return address;
            }
        }

        public static string ApiKey
        {
            get
            {
                string? apiKey = Environment.GetEnvironmentVariable("TRANSMUTE_APIKEY");

                if (string.IsNullOrWhiteSpace(apiKey))
                    throw new OperationCanceledException("You need to define the API key in the TRANSMUTE_APIKEY environment variable. Did you load the correct test.runsettings file?");

                return apiKey;
            }
        }
    }
}