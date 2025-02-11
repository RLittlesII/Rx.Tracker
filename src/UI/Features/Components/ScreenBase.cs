using System;
using System.Reactive;
using System.Reactive.Disposables;
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
public abstract class ScreenBase<T> : ReactiveContentPage<T>, IInitializeAsync, INavigatedAware, IDestructible
    where T : ViewModelBase
{
    /// <inheritdoc/>
    Task IInitializeAsync.InitializeAsync(INavigationParameters parameters) => InitializeAsync(parameters);

    /// <inheritdoc />
    void INavigatedAware.OnNavigatedFrom(INavigationParameters parameters) => OnNavigatedFrom(parameters);

    /// <inheritdoc />
    void INavigatedAware.OnNavigatedTo(INavigationParameters parameters) => OnNavigatedTo(parameters);

    /// <inheritdoc />
    void IDestructible.Destroy() => TearDown();

    /// <summary>
    /// Initialize the instance.
    /// </summary>
    /// <param name="parameters">The navigation parameters.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    protected virtual Task Initialize(INavigationParameters parameters) => Task.CompletedTask;

    /// <summary>
    /// Gets the garbage for disposal.
    /// </summary>
    protected CompositeDisposable Garbage { get; } = new();

    protected virtual void Destroy()
    {
    }

    private async Task InitializeAsync(INavigationParameters parameters)
    {
        await Initialize(parameters);

        using var disposable = ViewModel?.DisposeWith(Garbage).InitializeCommand.Execute(Unit.Default).Subscribe();
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

    private void TearDown()
    {
        Garbage.Dispose();
        ViewModel?.Dispose();
        Destroy();
    }
}