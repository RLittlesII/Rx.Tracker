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

    public enum AddMedicineState
    {
        Initial,
        Busy,
        Loaded,
        Failed,
        Valid
    }

    public enum AddMedicineTrigger
    {
        Load,
        Save,
        Validated
    }
}