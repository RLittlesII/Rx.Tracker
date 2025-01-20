using System;
using System.Reactive;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI.Maui;
using Rx.Tracker.Features;
using Rx.Tracker.Navigation;
using Rx.Tracker.UI.Navigation;

namespace Rx.Tracker.UI.Features.Components;

/// <summary>
/// Represents a base screen with implementations for lifecycle methods.
/// </summary>
/// <typeparam name="T">The View Model Type.</typeparam>
public abstract class ScreenBase<T> : ReactiveContentPage<T>, IInitializeAsync, INavigatedAware
    where T : ViewModelBase
{
    /// <inheritdoc/>
    Task IInitializeAsync.InitializeAsync(INavigationParameters parameters) => InitializeAsync(parameters);

    void INavigatedAware.OnNavigatedFrom(INavigationParameters parameters) => OnNavigatedFrom(parameters);

    void INavigatedAware.OnNavigatedTo(INavigationParameters parameters) => OnNavigatedTo(parameters);

    /// <summary>
    /// Initialize the instance.
    /// </summary>
    /// <param name="parameters">The navigation parameters.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    protected virtual Task Initialize(INavigationParameters parameters) => Task.CompletedTask;

    private async Task InitializeAsync(INavigationParameters parameters)
    {
        await Initialize(parameters);

        using var disposable = ViewModel?.InitializeCommand.Execute(Unit.Default).Subscribe();
    }

    private void OnNavigatedFrom(INavigationParameters parameters)
    {
        INavigated? navigatedFrom = ViewModel;

        navigatedFrom?.OnNavigatedFrom(new NavigatorArguments(parameters));
    }

    private void OnNavigatedTo(INavigationParameters parameters)
    {
        INavigated? navigatedTo = ViewModel;

        navigatedTo?.OnNavigatedTo(new NavigatorArguments(parameters));
    }
}