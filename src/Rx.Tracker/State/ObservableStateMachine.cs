using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rx.Tracker.Extensions;
using Stateless;

namespace Rx.Tracker.State;

/// <summary>
/// Represents a <see cref="StateMachine{TState,TTrigger}"/> with additional <see cref="IObservable{T}"/> properties.
/// </summary>
/// <typeparam name="TState">The state.</typeparam>
/// <typeparam name="TTrigger">The trigger.</typeparam>
public abstract class ObservableStateMachine<TState, TTrigger> : StateMachine<TState, TTrigger>, IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableStateMachine{TState, TTrigger}"/> class.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="initialState">The initial state of the machine.</param>
    protected ObservableStateMachine(ILoggerFactory loggerFactory, TState initialState)
        : base(initialState)
    {
        RetainSynchronizationContext = true;
        Logger = loggerFactory.CreateLogger(GetType());
        var stateChange = new BehaviorSubject<TState>(initialState).DisposeWith(Garbage);
        var unhandledExceptions = new Subject<string>().DisposeWith(Garbage);

        OnUnhandledTrigger(
            (state, trigger) =>
                unhandledExceptions.OnNext($"{trigger} is not configured for {state}"));

        OnTransitionedAsync(
            transition =>
            {
                stateChange.OnNext(transition.Destination);
                return Task.CompletedTask;
            });

        Current =
            stateChange
               .AsObservable()
               .LogTrace(Logger, state => state, "Current State: {State}")
               .Publish()
               .RefCount();

        UnhandledTriggers =
            unhandledExceptions
               .AsObservable()
               .LogTrace(Logger, x => x, "Unhandled Trigger: {Trigger}")
               .Publish()
               .RefCount();
    }

    /// <summary>
    /// Gets a value indicating the change in state.
    /// </summary>
    public IObservable<TState> Current { get; }

    /// <summary>
    /// Gets a message from unhandled triggers.
    /// </summary>
    public IObservable<string> UnhandledTriggers { get; }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// Gets the garbage.
    /// </summary>
    protected CompositeDisposable Garbage { get; } = new CompositeDisposable();

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">A value indicating whether the object is disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Garbage.Dispose();
        }
    }
}