using Shouldly;
using TransmuteNET.Core;
using TransmuteNET.Entities;
using TransmuteNET.Entities.Health;
using TransmuteNET.Tests.Resources;

namespace TransmuteNET.Tests
{
    [TestFixture]
    public class HealthCheckTest
    {
        private TransmuteService? _service;

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
        public void GettingLiveness()
        {
            _service.ShouldNotBeNull();
            Liveness? liveness = null;

            Should.NotThrow(() =>
            {
                liveness = _service.GetLiveness();
            });

            liveness.ShouldNotBeNull();
            liveness.Status.ShouldBe("alive");
        }

        [TestCase, Order(2)]
        public void GettingReadiness()
        {
            _service.ShouldNotBeNull();
            Readiness? readiness = null;

            Should.NotThrow(() =>
            {
                readiness = _service.GetReadiness();
            });

            readiness.ShouldNotBeNull();
            readiness.Status.ShouldBe("ready");
            readiness.Checks.ShouldNotBeNull();
            readiness.Checks.Database.ShouldBe("ok");
            readiness.Checks.Storage.ShouldBe("ok");
        }

        [TestCase, Order(3)]
        public void GettingInfo()
        {
            _service.ShouldNotBeNull();
            AppInfo? info = null;

            Should.NotThrow(() =>
            {
                info = _service.GetInfo();
            });

            info.ShouldNotBeNull();
            info.Name.ShouldBe("Transmute");
            info.Version.ShouldBe("v2.0.0");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _service = null;
        }
    }
}