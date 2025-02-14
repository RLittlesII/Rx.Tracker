using DynamicData.Binding;

namespace Rx.Tracker.Extensions;

public class DynamicDataExtensions
{
    /// <summary>
    /// Gets the eager binding options.
    /// </summary>
    public static BindingOptions EagerBindingOptions { get; } = new(1);
}