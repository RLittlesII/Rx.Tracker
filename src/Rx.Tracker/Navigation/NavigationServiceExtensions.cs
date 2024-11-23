namespace Rx.Tracker.Navigation;

/// <summary>
/// <see cref="INavigationService"/> extensions.
/// </summary>
public static class NavigationServiceExtensions
{
    /// <summary>
    /// Handle a navigation result.
    /// </summary>
    /// <param name="task">A completion.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static Task HandleResult(this Task<INavigationResult> task) => HandleResult(task, result => Console.WriteLine(result.Exception));

    /// <summary>
    /// Handle a navigation result.
    /// </summary>
    /// <param name="task">A completion.</param>
    /// <param name="handler">The handler.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static Task HandleResult(this Task<INavigationResult> task, Action<INavigationResult> handler) => task.ContinueWith(
        continuation =>
        {
            if (!continuation.Result.Success)
            {
                handler.Invoke(continuation.Result);
            }
        });
}