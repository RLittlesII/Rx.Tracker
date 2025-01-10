using System;

namespace Rx.Tracker.Navigation;

/// <summary>
/// The application routes.
/// </summary>
public class Routes
{
    /// <summary>
    /// Gets the routes instance.
    /// </summary>
    public static Routes Instance { get; } = new();

    /// <summary>
    /// Gets the main navigation route.
    /// </summary>
    public Route MainNavigation { get; } = new(nameof(MainNavigation), new Uri($"/{NavigationPage}/{nameof(MainNavigation)}"));

    /// <summary>
    /// Gets the splash route.
    /// </summary>
    public Route Splash { get; } = new(nameof(Splash), new Uri($"/{NavigationPage}/{nameof(Splash)}"));

    /// <summary>
    /// Gets the add medicine route.
    /// </summary>
    public Route AddMedicine { get; } = new(nameof(AddMedicine), new Uri($"/{NavigationPage}/{nameof(AddMedicine)}"));

    /// <summary>
    /// Gets the schedule route.
    /// </summary>
    public Route Schedule { get; } = new(nameof(Schedule), new Uri($"/{NavigationPage}/{nameof(Schedule)}"));

    /// <summary>
    /// Gets the back route.
    /// </summary>
    public const string Back = "../";

    /// <summary>
    /// Gets the root route.
    /// </summary>
    public const string Root = "//";

    /// <summary>
    /// Gets the splash route.
    /// </summary>
    public const string NavigationPage = nameof(NavigationPage);

    /// <summary>
    /// Represents a route for navigation.
    /// </summary>
    public record Route
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Route"/> class.
        /// </summary>
        /// <param name="name">The route name.</param>
        /// <param name="path">The route path.</param>
        public Route(string name, Uri path)
        {
            Name = name;
            Path = path;
        }

        public static implicit operator Uri(Route route) => route.Path;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the path.
        /// </summary>
        public Uri Path { get; }
    }
}