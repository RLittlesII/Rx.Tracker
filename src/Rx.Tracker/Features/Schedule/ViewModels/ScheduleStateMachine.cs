using Microsoft.Extensions.Logging;
using Rx.Tracker.State;
using static Rx.Tracker.Features.Schedule.ViewModels.ScheduleStateMachine;

namespace Rx.Tracker.Features.Schedule.ViewModels;

/// <summary>
/// Defines an <see cref="ObservableStateMachine{TState,TTrigger}"/> for the schedule.
/// </summary>
public class ScheduleStateMachine : ObservableStateMachine<ScheduleState, ScheduleTrigger>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduleStateMachine"/> class.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    public ScheduleStateMachine(ILoggerFactory loggerFactory)
        : base(loggerFactory, ScheduleState.Initial)
    {
    }

    /// <summary>
    /// The available state.
    /// </summary>
    public enum ScheduleState
    {
        /// <summary>
        /// The initial state.
        /// </summary>
        Initial,

        /// <summary>
        /// The busy state.
        /// </summary>
        Busy,

        /// <summary>
        /// The data loaded state.
        /// </summary>
        Loaded,

        /// <summary>
        /// The application failed state.
        /// </summary>
        Failed,

        /// <summary>
        /// The days schedule.
        /// </summary>
        DaySchedule
    }

    /// <summary>
    /// The available triggers.
    /// </summary>
    public enum ScheduleTrigger
    {
        /// <summary>
        /// Load trigger.
        /// </summary>
        Load,

        /// <summary>
        /// Forward trigger.
        /// </summary>
        Forward,

        /// <summary>
        /// Back trigger.
        /// </summary>
        Back,

        /// <summary>
        /// Failure trigger.
        /// </summary>
        Failure
    }
}