using System.Threading;
using Serilog.Core;
using Serilog.Enrichers;
using Serilog.Events;

namespace Rx.Tracker.UI.Logging;

public sealed class NamedThreadEnricher : ILogEventEnricher
{
    /// <summary>Enrich the log event.</summary>
    /// <param name="logEvent">The log event to enrich.</param>
    /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var name = Thread.CurrentThread.Name;

        name = name switch
        {
            null                                    => ThreadPoolName,
            PlatformThreadPoolName => ThreadPoolName,
            var _                                   => name
        };

        logEvent.AddPropertyIfAbsent(new LogEventProperty(ThreadNameEnricher.ThreadNamePropertyName, new ScalarValue(name)));
    }

    public const string MainThreadName = "ui";
    private const string ThreadPoolName = "pool";
    private const string PlatformThreadPoolName = "Thread Pool";
}