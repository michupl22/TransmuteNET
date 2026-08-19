namespace TransmuteNET.Core.Exceptions
{
    public class EmptyResponseException : Exception
    {
        public static string Version => Transmute.TargetVersion;

        public EmptyResponseException() : base("The server returned an empty response") { }
    }
}