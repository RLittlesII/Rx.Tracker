using DryIoc;
using Rx.Tracker.Container;
using Rx.Tracker.Features.Medications.ViewModels;

namespace Rx.Tracker.Features.Medications.Container;

/// <inheritdoc />
public class AddMedicineModule : ContainerModule
{
    /// <inheritdoc />
    protected override IContainer Register(IContainer registrar)
    {
        registrar.Register<AddMedicineStateMachine>();
        return registrar;
    }
}