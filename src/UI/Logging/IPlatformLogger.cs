using Serilog;
using System;

namespace Rx.Tracker.UI.Logging;

public interface IPlatformLogger
{
    LoggerConfiguration ConfigureLogger();

    LoggerConfiguration ConfigureLogger(Guid sessionId);
}