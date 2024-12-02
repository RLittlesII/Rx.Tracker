using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using ReactiveMarbles.Extensions;
using Stateless;

namespace Rx.Tracker.State;

public abstract class ObservableStateMachine<TState, TTrigger> : StateMachine<TState, TTrigger>, IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableStateMachine{TState, TTrigger}"/> class.
    /// </summary>
    /// <param name="initialState">The initial state of the machine.</param>
    protected ObservableStateMachine(TState initialState)
        : base(initialState)
    {
        var stateChange = new Subject<TState>().DisposeWith(Garbage);
        var unhandledExceptions = new Subject<string>().DisposeWith(Garbage);

        OnUnhandledTrigger(
            (state, trigger) =>
                unhandledExceptions.OnNext($"{trigger} is not configured for {state}"));

        OnTransitionCompletedAsync(transition => Task.CompletedTask.ContinueWith(_ => stateChange.OnNext(transition.Destination)));

        StateChanged =
            stateChange
               .AsObservable()
               .Publish()
               .RefCount()
               .Do(_ => { });

        UnhandledTriggers =
            unhandledExceptions
               .AsObservable()
               .Publish()
               .RefCount()
               .Do(_ => { });
    }

    public IObservable<TState> StateChanged { get; }

    public IObservable<string> UnhandledTriggers { get; }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected CompositeDisposable Garbage { get; } = new CompositeDisposable();

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Garbage.Dispose();
        }
    }

    protected void CommonEntry(Transition transition) => TraceTransition(transition);

    protected void CommonExit(Transition transition) => TraceTransition(transition);

    protected void TraceTransition(Transition transition) => Console.WriteLine($"{transition}");
}