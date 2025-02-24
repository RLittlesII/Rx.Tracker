using Rx.Tracker.UI.Logging;

namespace Rx.Tracker.UI;

/// <summary>
/// Interface representing platform specific MAUI registrations so we don't have compiler definitions all over the <see cref="MauiProgram" />.
/// </summary>
public interface IPlatformRegistrations
{
    /// <summary>
    /// Gets the <see cref="IPlatformLogger" />.
    /// </summary>
    IPlatformLogger Logger { get; }
}