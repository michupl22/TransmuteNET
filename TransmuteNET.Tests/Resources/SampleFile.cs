namespace TransmuteNET.Tests.Resources
{
    internal class SampleFile
    {
        private const string SAMPLE_FILE_PATH = "Samples\\fitness_banner.jpg";

        public static string FileName => Path.GetFileName(SAMPLE_FILE_PATH);

        public static byte[] Binary
        {
            get
            {
                FileStream sourceStream = File.OpenRead(SAMPLE_FILE_PATH);
                byte[] bytes = new byte[sourceStream.Length];
                sourceStream.ReadExactly(bytes, 0, (int)sourceStream.Length);
                sourceStream.Close();

                return bytes;
            }
        }
    }
}