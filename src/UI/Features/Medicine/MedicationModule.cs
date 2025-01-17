using Prism.Ioc;
using Rx.Tracker.Features.Medications.ViewModels;
using Rx.Tracker.Navigation;
using Rx.Tracker.UI.Container;
using Rx.Tracker.UI.Features.Medicine.Add;

namespace Rx.Tracker.UI.Features.Medicine;

public class MedicationModule : ContainerRegistryModule
{
    protected override IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry) => containerRegistry
       .RegisterForNavigation<AddMedicineModal, AddMedicineViewModel>(Routes.Instance.AddMedicine.Name);
}