using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ReactiveMarbles.Command;
using ReactiveMarbles.Mvvm;
using Rx.Tracker.Extensions;
using Rx.Tracker.Mediation;
using Rx.Tracker.Navigation;

namespace Rx.Tracker.Features;

/// <summary>
/// A base view model.
/// </summary>
public abstract class ViewModelBase : RxDisposableObject, INavigated
{
    /// <summary>
    /// Gets the initialize command.
    /// </summary>
    public RxCommand<Unit, Unit> InitializeCommand { get; }

    /// <summary>
    /// Initialize the View Model.
    /// </summary>
    /// <param name="cqrs">The mediator.</param>
    /// <returns>A completion notification.</returns>
    protected virtual Task Initialize(ICqrs cqrs) => Task.CompletedTask;

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Garbage.Dispose();
        }
    }

    /// <summary>
    /// Gets the <see cref="INavigator" />.
    /// </summary>
    protected INavigator Navigator { get; }

    /// <summary>
    /// Gets the <see cref="ICqrs" />.
    /// </summary>
    protected ICqrs Mediator { get; }

    /// <summary>
    /// Gets a notification of <see cref="IArguments" /> when the view model is navigated to.
    /// </summary>
    protected IObservable<IArguments> NavigatedTo { get; }

    /// <summary>
    /// Gets a notification of <see cref="IArguments" /> when the view model is navigated from.
    /// </summary>
    protected IObservable<IArguments> NavigatedFrom { get; }

    /// <summary>
    /// Gets the garbage.
    /// </summary>
    protected CompositeDisposable Garbage { get; } = new();

    /// <summary>
    /// Gets the logger.
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModelBase" /> class.
    /// </summary>
    /// <param name="navigator">The navigator.</param>
    /// <param name="cqrs">The cqrs mediator.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    protected ViewModelBase(INavigator navigator, ICqrs cqrs, ILoggerFactory loggerFactory)
    {
        Navigator = navigator;
        Mediator = cqrs;
        Logger = loggerFactory.CreateLogger(GetType());
        _onNavigatedTo = new Subject<IArguments>().DisposeWith(Garbage);
        _onNavigatedFrom = new Subject<IArguments>().DisposeWith(Garbage);
        NavigatedTo = _onNavigatedTo.AsObservable().LogTrace(Logger, "NavigatedTo").Publish().RefCount();
        NavigatedFrom = _onNavigatedFrom.AsObservable().LogTrace(Logger, "NavigatedFrom").Publish().RefCount();
        InitializeCommand = RxCommand.Create(() => ExecuteInitialize(cqrs)).DisposeWith(Garbage);
    }

    private Task ExecuteInitialize(ICqrs cqrs) => Initialize(cqrs);

    /// <inheritdoc />
    void INavigated.OnNavigatedTo(IArguments arguments) => _onNavigatedTo.OnNext(arguments);

    /// <inheritdoc />
    void INavigated.OnNavigatedFrom(IArguments arguments) => _onNavigatedFrom.OnNext(arguments);

    // void IDestructible.Destroy() => Dispose(true);
#pragma warning disable CA2213
    private readonly Subject<IArguments> _onNavigatedTo;
    private readonly Subject<IArguments> _onNavigatedFrom;
#pragma warning restore CA2213
}