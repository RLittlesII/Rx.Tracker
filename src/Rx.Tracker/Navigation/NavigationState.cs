namespace Rx.Tracker.Navigation;

/// <summary>
/// How the navigation completed, if it did ...
/// </summary>
public enum NavigationState
{
    /// <summary>
    /// Successfully.
    /// </summary>
    Succeeded,

    /// <summary>
    /// Failure.
    /// </summary>
    Failed,

    /// <summary>
    /// System is broken.
    /// </summary>
    Ethers
}