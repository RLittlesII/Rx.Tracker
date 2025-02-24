using System;

namespace Rx.Tracker.Exceptions;

/// <summary>
/// An exception that occurs when initializing a view model.
/// </summary>
public class InitializationException : Exception
{
    /// <summary>
    /// Gets the message template for this exception.
    /// </summary>
    public const string MessageTemplate = "An exception occured when attempting to intialize the View Model.";

    /// <summary>
    /// Initializes a new instance of the <see cref="InitializationException" /> class.
    /// </summary>
    public InitializationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InitializationException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public InitializationException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InitializationException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    public InitializationException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}