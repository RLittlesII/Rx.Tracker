using Microsoft.Extensions.Logging;
using Rocket.Surgery.Airframe.Exceptions;
using System;

namespace Rx.Tracker.UI.Exceptions.Handlers;

public class LoggingExceptionHandler : UnhandledExceptionHandlerBase
{
    public LoggingExceptionHandler(ILoggerFactory loggerFactory)
        : base(loggerFactory)
    {
    }

    /// <inheritdoc />
    protected override void Handle(Exception exception, Guid correlationId) => Logger.LogCritical(exception, MessageTemplate, correlationId, exception.Message);

    /// <inheritdoc />
    protected override bool CanHandle(Exception exception) => true;

    private const string MessageTemplate = "{0}: {1}";
}