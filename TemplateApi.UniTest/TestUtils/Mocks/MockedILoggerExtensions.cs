using Moq;
using Serilog;

namespace TemplateApi.UniTest.TestUtils.Mocks;

internal static class MockedILoggerExtensions
{
    public static void SetLogger(this Mock<ILogger> MockLogger)
    {
        MockLogger.Setup(x => x.Debug(It.IsAny<string>())).Verifiable();
        MockLogger.Setup(x => x.Error(It.IsAny<string>())).Verifiable();
        MockLogger.Setup(x => x.Fatal(It.IsAny<string>())).Verifiable();
        MockLogger.Setup(x => x.Information(It.IsAny<string>())).Verifiable();
        MockLogger.Setup(x => x.Warning(It.IsAny<string>())).Verifiable();

    }
}
