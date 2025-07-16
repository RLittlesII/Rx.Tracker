namespace Rx.Tracker.Failures;

/// <summary>
/// Represents an instance of a failure with an associated message.
/// </summary>
public class Failure
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Failure"/> class.
    /// </summary>
    /// <param name="message">The failure message.</param>
    public Failure(string message) => Message = message;

    /// <summary>
    /// Gets the failure message associated with this instance.
    /// Provides a description or details of the error or failure condition.
    /// </summary>
    public string Message { get; }
}