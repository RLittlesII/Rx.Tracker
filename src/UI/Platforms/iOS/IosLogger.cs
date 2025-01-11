using Rx.Tracker.UI.Logging;
using Serilog;
using Serilog.Configuration;

namespace Rx.Tracker.UI;

public class IosLogger : PlatformLogger
{
    /// <inheritdoc />
    protected override void WriteToConsoleConditional(LoggerSinkConfiguration config, string consoleMessage) =>
        config.NSLog(outputTemplate: consoleMessage);
}