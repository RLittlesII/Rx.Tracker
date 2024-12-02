using System;
using System.Threading.Tasks;

namespace Rx.Tracker.State;

public interface IStateMachine<TState, TTrigger>
{
    /// <summary>
    /// Transition from the current state via the specified trigger in async fashion.
    /// The target state is determined by the configuration of the current state.
    /// Actions associated with leaving the current state and entering the new one
    /// will be invoked.
    /// </summary>
    /// <param name="trigger">The trigger to fire.</param>
    /// <exception cref="T:System.InvalidOperationException">The current state does
    /// not allow the trigger to be fired.</exception>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Fire(TTrigger trigger);

    Task Fire(TTrigger trigger, params object[] parameters);

    Task Fire<T>(TTrigger trigger, T parameter);

    Task Fire<T0, T1>(TTrigger trigger, T0 parameter0, T1 parameter1);

    Task Fire<T0, T1, T2>(TTrigger trigger, T0 parameter0, T1 parameter1, T2 parameter2);

    bool CanFire(TTrigger trigger);

    IObservable<bool> ObserveCanTrigger(TTrigger trigger);

    IObservable<TState> StateChanged { get; }
}