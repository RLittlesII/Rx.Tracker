using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rx.Tracker.Features.Medications.ViewModels;

namespace Rx.Tracker.Tests.Features.Medicine.ViewModels;

[AutoFixture(typeof(AddMedicineViewModel))]
internal sealed partial class AddMedicineViewModelFixture
{
    internal AddMedicineViewModelFixture() => this._stateMachineFactory = () => new AddMedicineStateMachineFixture();

    public AddMedicineViewModelFixture WithFactory(AddMedicineStateMachine stateMachine) => this.With(ref _stateMachineFactory, () => stateMachine);
}