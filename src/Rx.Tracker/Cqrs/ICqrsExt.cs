namespace Rx.Tracker.Cqrs;

/// <summary>
/// Represents the combined contract for executing commands and sending queries in a CQRS-based system.
/// </summary>
/// <remarks>
/// This interface is an extension that combines the responsibilities of <see cref="ICommander" /> and
/// <see cref="ISender" />, allowing both command execution and query handling within a unified abstraction.
/// </remarks>
public interface ICqrsExt : ICommander, ISender;