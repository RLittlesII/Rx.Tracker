using Rx.Tracker.Extensions;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Exceptions;
using System;

namespace Rx.Tracker.UI.Logging;

public abstract class PlatformLogger : IPlatformLogger
{
    protected abstract void WriteToConsoleConditional(LoggerSinkConfiguration config, string consoleMessage);

    private static bool NotExceptional(LogEvent e) => e.Exception is null;

    private static bool Exceptional(LogEvent e) => !NotExceptional(e);

    private LoggerConfiguration ConfigurePlatformLogger(Guid sessionId) => LoggerConfiguration ??= new LoggerConfiguration()
       .MinimumLevel.Verbose()
       .Enrich.WithProperty("SessionId", sessionId)
       .Enrich.WithNamedThread()
       .Enrich.WithThreadId()
       .Enrich.FromLogContext()
       .Enrich.WithExceptionDetails()
       .Restructure()
       .WriteTo.Conditional(Exceptional, c => WriteToConsoleConditional(c, ConsoleExceptionTemplate))
       .WriteTo.Conditional(NotExceptional, c => WriteToConsoleConditional(c, OutputTemplate));

    LoggerConfiguration IPlatformLogger.ConfigureLogger() => ConfigurePlatformLogger(Guid.NewGuid());

    LoggerConfiguration IPlatformLogger.ConfigureLogger(Guid sessionId) => ConfigurePlatformLogger(sessionId);

    private const string OutputTemplate = "[{Level:u3}]" + Tab + "[{ThreadName}-{ThreadId}]" + Tab + "{SourceContext}-{Message:l}";
    private const string ConsoleExceptionTemplate = OutputTemplate + Exception;
    private const string Tab = "\t";
    private const string Exception = "{NewLine:l}{Exception:l}";
    private static LoggerConfiguration? LoggerConfiguration;
}