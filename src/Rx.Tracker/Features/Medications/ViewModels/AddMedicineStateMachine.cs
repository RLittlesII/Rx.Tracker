using Microsoft.Extensions.Logging;
using Rx.Tracker.State;

namespace Rx.Tracker.Features.Medications.ViewModels;

public class AddMedicineStateMachine : ObservableStateMachine<AddMedicineStateMachine.AddMedicineState, AddMedicineStateMachine.AddMedicineTrigger>
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