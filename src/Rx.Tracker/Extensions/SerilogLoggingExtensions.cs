using System;
using Rx.Tracker.Features.Medications.ViewModels;
using Rx.Tracker.Features.Schedule.ViewModels;
using Serilog;
using Stateless;

namespace Rx.Tracker.Extensions;

/// <summary>
/// <see cref="Serilog"/> extensions.
/// </summary>
public static class SerilogLoggingExtensions
{
    /// <summary>
    /// Restructures all typed logging.
    /// </summary>
    /// <param name="configuration">The logger configuration.</param>
    /// <returns>The log configuration.</returns>
    public static LoggerConfiguration Restructure(this LoggerConfiguration configuration) => configuration
       .TransformStructure<StateMachine<ScheduleStateMachine.ScheduleState, ScheduleStateMachine.ScheduleTrigger>.Transition>(transition => new
        {
            CurrentState = transition.Source,
            Event = transition.Trigger,
            NextState = transition.Destination
        })
       .TransformStructure<StateMachine<AddMedicineStateMachine.AddMedicineState, AddMedicineStateMachine.AddMedicineTrigger>.Transition>(transition => new
        {
            CurrentState = transition.Source,
            Event = transition.Trigger,
            NextState = transition.Destination
        });

    /// <summary>
    /// Transforms the structure of the provided instance of <see cref="T"/>.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="destructure">The destructure action.</param>
    /// <typeparam name="T">The object type.</typeparam>
    /// <returns>The log configuration.</returns>
    private static LoggerConfiguration TransformStructure<T>(this LoggerConfiguration configuration, Func<T, object> destructure)
        => configuration.Destructure.ByTransforming<T>(stuff => destructure.Invoke(stuff));
}