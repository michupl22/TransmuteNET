namespace TransmuteNET.CLI.Extensions
{
    internal static class FileSystemWatcherExt
    {
        public static void AddFilters(this FileSystemWatcher watcher, Options options)
        {
            IEnumerable<string> filters = options.GetWatchFilters();

            foreach (string filter in filters)
            {
                watcher.Filters.Add(filter);
            }
        }
    }
}