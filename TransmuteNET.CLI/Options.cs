using CommandLine;

namespace TransmuteNET.CLI
{
    internal class Options
    {
        [Option("config", Required = true, HelpText = "Transmute config path. It must be a json file.")]
        public string? ConfigPath { get; set; }

        [Option("watch", Required = true, HelpText = "Path to watch files for converters.")]
        public string? WatchPath { get; set; }

        [Option("watch-filter", Required = true, HelpText = "Comma separated list of file formats to convert.")]
        public string? WatchFilter { get; set; }

        [Option("output", Required = true, HelpText = "Path to store converted files.")]
        public string? Output { get; set; }

        [Option("output-formats", Required = true, HelpText = "Comma separated list of file output formats.")]
        public string? OutputFormats { get; set; }

        [Option("quality", Required = false, HelpText = "Quality of conversion.")]
        public string? Quality { get; set; }
    }
}