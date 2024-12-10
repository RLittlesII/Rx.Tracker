using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using Rx.Tracker.Features.Medications.ViewModels;

namespace Rx.Tracker.UI.Features.Medicine.Add;

public partial class AddMedicine : IInitializeAsync
{
    public AddMedicine() => InitializeComponent();

    public async Task InitializeAsync(INavigationParameters parameters) => await ((AddMedicineViewModel)BindingContext).InitializeCommand.Execute(Unit.Default);
}