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
        DaySchedule
    }

    public enum ScheduleTrigger
    {
        /// <summary>
        /// Load trigger.
        /// </summary>
        Load,

        /// <summary>
        /// Failure trigger.
        /// </summary>
        Failure
    }
}