using Serilog;
using Serilog.Configuration;
using Serilog.Enrichers;
using System;
using System.Threading;

namespace Rx.Tracker.UI.Logging;

public static class ThreadLoggerConfigurationExtensions
{
    /// <summary>
    /// Enrich log events with a ThreadId property containing the <see cref="P:System.Environment.CurrentManagedThreadId" />.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="T:System.ArgumentNullException"> Thrown when enrichment configuration is null.</exception>
    public static LoggerConfiguration WithThreadId(this LoggerEnrichmentConfiguration enrichmentConfiguration) => enrichmentConfiguration != null
        ? enrichmentConfiguration.With<ThreadIdEnricher>()
        : throw new ArgumentNullException(nameof(enrichmentConfiguration));

    /// <summary>
    /// Enrich log events with a ThreadName property containing the <see cref="P:System.Threading.Thread.CurrentThread" />
    /// <see cref="P:System.Threading.Thread.Name" />.
    /// </summary>
    /// ¬
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <exception cref="T:System.ArgumentNullException"> Thrown when enrichment configuration is null.</exception>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithNamedThread(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        Thread.CurrentThread.Name = NamedThreadEnrichment.MainThreadName;

        return enrichmentConfiguration != null
            ? enrichmentConfiguration.With<NamedThreadEnrichment>()
            : throw new ArgumentNullException(nameof(enrichmentConfiguration));
    }
}