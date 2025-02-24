using System;
using System.Threading.Tasks;

namespace Rx.Tracker.State;

/// <summary>
/// Represents a state machine.
/// </summary>
/// <typeparam name="TState">The state type.</typeparam>
/// <typeparam name="TTrigger">The trigger type.</typeparam>

// NOTE: [rlittlesii: December 05, 2024] This is the abstraction that I don't need.  I have knowingly taken a dependency on Stateless.
public interface IStateMachine<TState, TTrigger>
{
    /// <summary>
    /// Transition from the current state via the specified trigger in async fashion.
    /// The target state is determined by the configuration of the current state.
    /// Actions associated with leaving the current state and entering the new one
    /// will be invoked.
    /// </summary>
    /// <param name="trigger">The trigger to fire.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// The current state does
    /// not allow the trigger to be fired.
    /// </exception>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task Fire(TTrigger trigger);

    /// <summary>
    /// Transition from the current state via the specified trigger in async fashion.
    /// The target state is determined by the configuration of the current state.
    /// Actions associated with leaving the current state and entering the new one
    /// will be invoked.
    /// </summary>
    /// <param name="trigger">The trigger to fire.</param>
    /// <param name="parameter">The parameter.</param>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <exception cref="T:System.InvalidOperationException">
    /// The current state does
    /// not allow the trigger to be fired.
    /// </exception>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task Fire<T>(TTrigger trigger, T parameter);

    /// <summary>
    /// Gets an observable that will tell you if the state machine can fire a given trigger.
    /// </summary>
    /// <param name="trigger">The trigger.</param>
    /// <returns>An observable pipeline of whether the state machine can fire.</returns>
    IObservable<bool> CanFire(TTrigger trigger);

    /// <summary>
    /// Gets an observable of the <see cref="TState" /> changes.
    /// </summary>
    IObservable<TState> StateChanged { get; }
}