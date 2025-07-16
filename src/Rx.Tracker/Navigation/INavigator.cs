using System;
using System.Threading.Tasks;

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
    /// Navigates to the uri from the specified route.
    /// </summary>
    /// <param name="routes">The routes.</param>
    /// <param name="arguments">The arguments.</param>
    /// <typeparam name="TRoute">The router type.</typeparam>
    /// <returns>a navigation result.</returns>
    Task<NavigationState> Navigate<TRoute>(Func<TRoute, Uri> routes, Action<IArguments> arguments)
        where TRoute : new();

    /// <summary>
    /// Presents a modal to the uri from the specified route.
    /// </summary>
    /// <param name="routes">The routes.</param>
    /// <typeparam name="TRoute">The router type.</typeparam>
    /// <returns>a navigation result.</returns>
    Task<NavigationState> Modal<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new();

    /// <summary>
    /// Goes back to a specific uri from the specified route.
    /// </summary>
    /// <returns>a navigation result.</returns>
    Task<NavigationState> Back();

    /// <summary>
    /// Goes back to a specific uri from the specified route.
    /// </summary>
    /// <param name="backwards">The number of backward navigations.</param>
    /// <returns>a navigation result.</returns>
    Task<NavigationState> Back(uint backwards);

    /// <summary>
    /// Goes back to a specific uri from the specified route.
    /// </summary>
    /// <param name="routes">The routes.</param>
    /// <typeparam name="TRoute">The router type.</typeparam>
    /// <returns>a navigation result.</returns>
    Task<NavigationState> Back<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new();

    /// <summary>
    /// Dismiss the modal.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}" /> representing the result of the asynchronous operation.</returns>
    Task<NavigationState> Dismiss();

    /// <summary>
    /// Dismiss the modal.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    /// <returns>A <see cref="Task{TResult}" /> representing the result of the asynchronous operation.</returns>
    Task<NavigationState> Dismiss(Action<IArguments> arguments);
}