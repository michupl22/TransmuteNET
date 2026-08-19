using CommandLine;
using TransmuteNET.CLI.Extensions;
using TransmuteNET.CLI.Utilities;
using TransmuteNET.Core;
using TransmuteNET.Entities;
using TransmuteNET.Entities.Data;
using TransmuteNET.Entities.Tasks;

namespace TransmuteNET.CLI
{
    internal class Program
    {
        const string DEFAULT_TITLE = "TransmuteNET Converter";

        static TransmuteService? _service;
        static DirectoryInfo? _output;
        static string[]? _outputFormats;
        static string? _quality;
        static FileSystemWatcher? _watcher;

        static void Main(string[] args)
        {
            Console.Title = DEFAULT_TITLE;

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run);
        }

        static void Run(Options options)
        {
            try
            {
                TransmuteConfig config = options.GetConfig();
                _service = new TransmuteService(config);
                Informant.Info("Connected with " + config.Address);

                _output = DirectoryCreator.GetDirectory(options.Output!);
                _outputFormats = [.. options.GetOutputFormats()];
                _quality = options.GetQuality();

                Informant.Info("Output: " + _output.FullName);
                Informant.Info("Output Formats: " + string.Join(", ", _outputFormats));
                Informant.Info("Quality: " + _quality);

                _watcher = options.GetWatcher();
                _watcher.AddFilters(options);

                _watcher.Created += OnCreated;
                _watcher.Error += OnError;

                Console.Title = Path.GetDirectoryName(_watcher.Path) + " - " + DEFAULT_TITLE;
                Informant.Success("Converter has been initialized. Waiting for files...");
            }
            catch (Exception ex)
            {
                Informant.Error(ex);
            }

            ConsoleKeyInfo? pressedKey = null;
            while (!pressedKey.HasValue || pressedKey.Value.Key != ConsoleKey.Q)
            {
                Informant.Info("Press Q to quit");
                pressedKey = Console.ReadKey();
            }
        }

        static void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                byte[] bytes = SourceFileReader.ReadWithTry(e.FullPath);

                Informant.Trace("Uploading file to server... File: " + e.FullPath);
                UploadResult result = _service!.Upload(bytes, Path.GetFileName(e.FullPath));
                Informant.Trace(result.Message!);

                if (result.Source is null)
                {
                    throw new OperationCanceledException("Source is null.");
                }

                foreach (string outputFormat in _outputFormats!)
                {
                    Informant.Trace("Converting file to " + outputFormat);

                    Conversion request = new(result.Source, outputFormat, _quality!);
                    TransmuteConverted fileConverted = _service.Convert(request);
                    Informant.Trace("File has been converted");

                    Informant.Trace("Downloading file from server... File: ");
                    byte[] fileData = _service.Download(fileConverted);
                    string outputFilePath = Path.Combine(_output!.FullName, $"{Path.GetFileNameWithoutExtension(fileConverted.OrginalFileName!)}.{outputFormat}");
                    File.WriteAllBytes(outputFilePath, fileData);
                    Informant.Trace("File has been downloaded in " + outputFilePath);

                    Informant.Success("Conversion completed");
                }
            }
            catch (OperationCanceledException ex)
            {
                Informant.Warning(ex);
            }
            catch (Exception ex)
            {
                Informant.Error(ex);
            }
        }

        static void OnError(object sender, ErrorEventArgs e)
        {
            Exception ex = e.GetException();
            Informant.Error(ex);
        }
    }
}