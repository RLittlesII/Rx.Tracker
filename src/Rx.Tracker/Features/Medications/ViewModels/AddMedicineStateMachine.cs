using Rx.Tracker.State;

namespace Rx.Tracker.Features.Medications.ViewModels;

public class AddMedicineStateMachine : ObservableStateMachine<AddMedicineStateMachine.AddMedicineState, AddMedicineStateMachine.AddMedicineTrigger>
{
    public AddMedicineStateMachine()
        : base(AddMedicineState.Initial)
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