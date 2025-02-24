using DynamicData.Binding;

namespace Rx.Tracker.Extensions;

/// <summary>
/// Extensions for <see cref="DynamicData" />.
/// </summary>
public static class DynamicDataExtensions
{
    /// <summary>
    /// Gets the eager binding options.
    /// </summary>
    public static BindingOptions EagerBindingOptions { get; } = new(1);
}