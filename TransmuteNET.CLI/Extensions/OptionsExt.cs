using System.Text.Json;
using TransmuteNET.CLI.Utilities;
using TransmuteNET.Core.Dictionaries;
using TransmuteNET.Entities;

namespace TransmuteNET.CLI.Extensions
{
    internal static class OptionsExt
    {
        public static TransmuteConfig GetConfig(this Options options)
        {
            if (!File.Exists(options.ConfigPath))
            {
                throw new FileNotFoundException(options.ConfigPath);
            }

            string content = File.ReadAllText(options.ConfigPath)!;
            return JsonSerializer.Deserialize<TransmuteConfig>(content)!;
        }

        public static IEnumerable<string> GetOutputFormats(this Options options)
        {
            if (options.OutputFormats == null)
            {
                return [];
            }

            string[] extensions = options.OutputFormats.Split(',');

            if (extensions.Length == 0)
            {
                return [];
            }

            List<string> filters = [];

            foreach (string extension in extensions)
            {
                filters.Add(extension.Trim().ToLower());
            }

            return [.. filters];
        }

        public static string GetQuality(this Options options)
        {
            return options.Quality is not null ? options.Quality : Quality.Default;
        }

        public static FileSystemWatcher GetWatcher(this Options options)
        {
            DirectoryInfo watchDirectory = DirectoryCreator.GetDirectory(options.WatchPath!);

            return new FileSystemWatcher
            {
                Path = watchDirectory.FullName,
                IncludeSubdirectories = false,
                NotifyFilter = NotifyFilters.FileName,
                EnableRaisingEvents = true
            };
        }

        public static IEnumerable<string> GetWatchFilters(this Options options)
        {
            if (options.WatchFilter == null)
            {
                return [];
            }

            string[] extensions = options.WatchFilter.Split(',');

            if (extensions.Length == 0)
            {
                return [];
            }

            List<string> filters = [];

            foreach (string extension in extensions)
            {
                filters.Add("*." + extension.Trim().ToLower());
            }

            return [.. filters];
        }
    }
}