namespace TransmuteNET.CLI.Utilities
{
    internal class SourceFileReader
    {
        public static byte[] ReadWithTry(string path)
        {
            int retries = 3;
            byte[]? bytes = null;

            while (retries > 0)
            {
                try
                {
                    FileStream sourceStream = new(path, FileMode.Open);
                    bytes = new byte[sourceStream.Length];
                    sourceStream.ReadExactly(bytes, 0, (int)sourceStream.Length);
                    sourceStream.Close();
                }
                catch (IOException)
                {
                    Informant.Warning("Cannot read file " + path);
                    Thread.Sleep(5000);
                }
                catch (Exception)
                {
                    Informant.Error("Unknown error while reading file " + path);
                }

                retries--;
            }

            if (bytes is null)
            {
                throw new OperationCanceledException("Cannot read file.");
            }

            return bytes;
        }
    }
}