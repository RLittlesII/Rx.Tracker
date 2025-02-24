using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rx.Tracker.Features.Medications.ViewModels;

namespace Rx.Tracker.Tests.Features.Medicine.ViewModels;

[AutoFixture(typeof(AddMedicineViewModel))]
internal sealed partial class AddMedicineViewModelFixture
{
    public AddMedicineViewModelFixture WithFactory(AddMedicineStateMachine stateMachine) => With(ref _stateMachineFactory, () => stateMachine);
    internal AddMedicineViewModelFixture() => _stateMachineFactory = () => new AddMedicineStateMachineFixture();
}