namespace Rx.Tracker.Navigation;

/// <summary>
/// Represents a simple navigation router.
/// </summary>
public interface INavigator
{
    /// <summary>
    /// Navigates to the uri from the specified route.
    /// </summary>
    /// <param name="routes">The routes.</param>
    /// <typeparam name="TRoute">The router type.</typeparam>
    /// <returns>a navigation result.</returns>
    Task<NavigationState> Navigate<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new();

    /// <summary>
    /// Goes back to a specific uri from the specified route.
    /// </summary>
    /// <param name="backwards">The number of backward navigations.</param>
    /// <returns>a navigation result.</returns>
    Task<NavigationState> Back(int backwards);

    /// <summary>
    /// Goes back to a specific uri from the specified route.
    /// </summary>
    /// <param name="routes">The routes.</param>
    /// <typeparam name="TRoute">The router type.</typeparam>
    /// <returns>a navigation result.</returns>
    Task<NavigationState> Back<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new();
}