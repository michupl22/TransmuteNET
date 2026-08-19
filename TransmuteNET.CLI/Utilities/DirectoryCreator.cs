namespace TransmuteNET.CLI.Utilities
{
    internal class DirectoryCreator
    {
        public static DirectoryInfo GetDirectory(string path)
        {
            DirectoryInfo directory;

            if (!Directory.Exists(path))
            {
                directory = new DirectoryInfo(path);
            }
            else
            {
                directory = new DirectoryInfo(path);
            }

            return directory;
        }
    }
}