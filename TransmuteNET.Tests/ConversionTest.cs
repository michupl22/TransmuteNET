using Shouldly;
using TransmuteNET.Core;
using TransmuteNET.Entities;
using TransmuteNET.Entities.Data;
using TransmuteNET.Entities.Tasks;
using TransmuteNET.Tests.Resources;

namespace TransmuteNET.Tests
{
    [TestFixture]
    public class ConversionTest
    {
        private const string TARGET_FORMAT = "webp";
        private const string TARGET_QUALITY = "high";

        private TransmuteService? _service;
        private TransmuteSource? _source;
        private TransmuteConverted? _converted;

        [OneTimeSetUp]
        public void SetUp()
        {
            TransmuteConfig config = new()
            {
                Address = TransmuteParameters.Host,
                ApiKey = TransmuteParameters.ApiKey
            };

            _service = new TransmuteService(config);
        }

        [TestCase, Order(1)]
        public void Uploading()
        {
            _service.ShouldNotBeNull();
            UploadResult? uploadResponse = null;

            Should.NotThrow(() =>
            {
                byte[] bytes = SampleFile.Binary;
                uploadResponse = _service!.Upload(bytes, Path.GetFileName(SampleFile.FileName));
            });

            uploadResponse.ShouldNotBeNull();
            uploadResponse.Message.ShouldNotBeNullOrWhiteSpace();
            uploadResponse.Message.ShouldBe("File uploaded successfully");
            uploadResponse.Source.ShouldNotBeNull();
            uploadResponse.Source.ID.ShouldNotBeNullOrWhiteSpace();

            _source = uploadResponse.Source;
        }

        [TestCase, Order(2)]
        public void Converting()
        {
            _service.ShouldNotBeNull("The Transmute service has not been created");
            _source.ShouldNotBeNull("The source file was not sent");
            _source.CompatibleFormats.ShouldNotBeNull();
            _source.CompatibleFormats.ContainsKey(TARGET_FORMAT).ShouldBeTrue();
            _source.CompatibleFormats[TARGET_FORMAT].Contains(TARGET_QUALITY).ShouldBeTrue();

            Conversion conversion = new(_source, "webp", "medium");
            TransmuteConverted? converted = null;

            Should.NotThrow(() =>
            {
                converted = _service!.Convert(conversion);
            });

            converted.ShouldNotBeNull();
            converted.ID.ShouldNotBeNullOrWhiteSpace();

            _converted = converted;
        }

        [TestCase, Order(3)]
        public void Downloading()
        {
            _service.ShouldNotBeNull("The Transmute service has not been created");
            _converted.ShouldNotBeNull("No information about the converted file");

            byte[]? binaryFiles = null;

            Should.NotThrow(() =>
            {
                binaryFiles = _service!.Download(_converted!);
            });

            binaryFiles.ShouldNotBeNull();
            binaryFiles.Length.ShouldBeGreaterThan(0);
        }

        [TestCase, Order(4)]
        public void DeletingSource()
        {
            _service.ShouldNotBeNull("The Transmute service has not been created");
            _source.ShouldNotBeNull("The source file was not sent");

            Should.NotThrow(() =>
            {
                _service!.Delete(_source);
            });
        }

        [TestCase, Order(5)]
        public void DeletingConversion()
        {
            _service.ShouldNotBeNull("The Transmute service has not been created");
            _converted.ShouldNotBeNull("No information about the converted file");

            Should.NotThrow(() =>
            {
                _service!.Delete(_converted);
            });
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _service = null;
            _source = null;
            _converted = null;
        }
    }
}