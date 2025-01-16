using System;
using System.Reactive;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI.Maui;

namespace Rx.Tracker.UI.Features.Components;

/// <summary>
/// Represents a base screen with implementations for lifecycle methods.
/// </summary>
/// <typeparam name="T">The View Model Type.</typeparam>
public abstract class ScreenBase<T> : ReactiveContentPage<T>, IInitializeAsync
    where T : ViewModelBase
{
    /// <inheritdoc/>
    Task IInitializeAsync.InitializeAsync(INavigationParameters parameters) => InitializeAsync(parameters);

    /// <summary>
    /// Initialize the instance.
    /// </summary>
    /// <param name="parameters">The navigation parameters.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    protected virtual Task Initialize(INavigationParameters parameters) => Task.CompletedTask;

    private async Task InitializeAsync(INavigationParameters parameters)
    {
        await Initialize(parameters);

        using var disposable = ((T)BindingContext).InitializeCommand.Execute(Unit.Default).Subscribe();
    }
}