using Microsoft.Extensions.Logging;
using Rx.Tracker.State;
using static Rx.Tracker.Features.Medications.ViewModels.AddMedicineStateMachine;

namespace Rx.Tracker.Features.Medications.ViewModels;

/// <summary>
/// Defines an <see cref="ObservableStateMachine{TState,TTrigger}"/> for adding medicine.
/// </summary>
public class AddMedicineStateMachine : ObservableStateMachine<AddMedicineState, AddMedicineTrigger>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddMedicineStateMachine"/> class.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    public AddMedicineStateMachine(ILoggerFactory loggerFactory)
        : base(loggerFactory, AddMedicineState.Initial)
    {
    }

    /// <summary>
    /// The available state.
    /// </summary>
    public enum AddMedicineState
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
        /// The input is valid state.
        /// </summary>
        Valid
    }

    /// <summary>
    /// The available triggers.
    /// </summary>
    public enum AddMedicineTrigger
    {
        /// <summary>
        /// Load trigger.
        /// </summary>
        Load,

        /// <summary>
        /// Save trigger.
        /// </summary>
        Save,

        /// <summary>
        /// Validated trigger.
        /// </summary>
        Validated
    }
}