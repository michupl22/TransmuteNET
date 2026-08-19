# TransmuteNET

**TransmuteNET** is a free library, written in .NET 9, designed to communicate with the [Transmute](https://github.com/transmute-app/transmute) service. With it, you can automatically upload files, convert them according to specified parameters, and then download the processed data. The project also includes a standalone console application that allows you to monitor a specified folder and perform automatic conversions according to a specific configuration provided as command-line arguments.

The project consists of the following components.

- **TransmuteNET Core**. A set of classes for communicating with the Transmute service. The package includes `TransmuteService` class, which allows you to connect, send files, convert data, and retrieve the resulting data. It can also retrieve the server status and configuration, and update the service configuration.
- **TransmuteNET Entities**. A set of classes containing the basic objects processed by the Transmute service during communication via the REST API. It is part of the *Core* package and forms the foundation for its operation. You can install it if you want to use only the objects themselves.
- **TransmuteNET CLI**. A console application for monitoring folder contents and automatically sending files to the Transmute service for conversion. The program is configured by entering command-line arguments that define the Transmute service settings, the monitored directory, the output directory, the file format being monitored, and the target file format for conversion.
- **TransmuteNET Tests**. Unit tests for the base library to verify correct operation with the Transmute service, as well as to provide sample code. For the tests to run correctly, you must define environment variables by creating a `*.runsettings` file and then loading it into Visual Studio. You can view an example structure by opening the `sample.runsettings` file included with the project.

## Core Library Features

- Uploading source files to the server.
- Downloading source files and converted files.
- Retrieving a list of source and converted files stored on the server.
- Deleting source and converted files.
- Retrieving information about the application's status and version.
- Downloading and updating Transmute settings.

## Quick Start

Here is the basic workflow, starting with connecting to the Transmute service and ending with downloading the resulting file.

### Connecting to the Transmute Service

First, you need to create a Transmute service configuration by entering its address and the generated API key. You can generate the key by logging in to the frontend and going to the *My Account* section.

```csharp
TransmuteConfig config = new()
{
    Address = "http://localhost:3313",
    ApiKey = "SecretKey"
};

TransmuteService service = new(config);
```

### Uploading a File

First, you need to send the source file to the server as bytes. Read the file and specify its name, then call the `Upload` method of the Transmute service. Wait for the operation to complete. Once it’s finished, you’ll receive the `UploadResult` containing a message and information about the source file stored on the server.

```csharp
byte[] bytes = File.ReadAllBytes(path);
UploadResult result = service.Upload(bytes, Path.GetFileName(path));
TransmuteSource sourceFile = result.Source;
```

### Converting a File

Once you have the information about the source file, you create a new conversion operation, specifying the target format and quality. The quality must be supported by the format in question to be taken into account. The conversion result will return information about the converted file located on the server.

```csharp
Conversion conversion = new(sourceFile, "webp", "medium");
TransmuteConverted convertedFile = service.Convert(conversion);
```

### Downloading the Converted File

Once you receive notification that the file has been converted, you can download it and save it to your hard drive.

```csharp
byte[] binaryFiles = _service!.Download(convertedFile);
File.WriteAllBytes(path, binaryFiles);
```

That's how you converted your file.

## Console Application

You can use a ready-made console application on Windows to automatically convert files from one format to another right now. Just run it with the parameters shown in the example below.

```shell
TransmuteCLI.exe --config "C:\Transmute\config.json" --watch "C:\Transmute\Watch" --watch-filter "jpg,gif" --output "D:\Transmute\Output" --output-formats "png,ico" --quality "high"
```

Here is what you need to provide when starting up.

- `--config file`. The path to the JSON file containing the Transmute service configuration. An example is provided below.
- `--watch directory`. The path to the directory to be monitored. The program detects a file of a specific format when it is created in that directory.
- `--watch-filter extensions`. File formats listed after the comma that are to be monitored and automatically sent to the Transmute service. Extensions not listed in this parameter will be ignored when they are created in the monitored directory.
- `--output directory`. The output directory where the converted files will be saved.
- `--output-formats extensions`. The formats listed after the comma to which the source files are to be converted. Specifying more than one may cause the conversion to take longer than usual.
- `--quality low,medium,high`. The quality level to which the files should be converted, provided that the target format supports it. Otherwise, the parameter will be ignored by the Transmute service. You can specify three quality levels: `low`, `medium`, and `high`. The higher the quality, the longer the conversion may take.

### Transmute Service Configuration File

```json
{
	"Address": "http://localhost:3313/",
	"ApiKey": "SecretKey"
}
```