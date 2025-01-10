using System.Reactive.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using Rx.Tracker.Features.Medications.ViewModels;
using Unit = System.Reactive.Unit;

namespace Rx.Tracker.UI.Features.Medicine.Add;

public partial class AddMedicineScreen : IInitializeAsync
{
    public AddMedicineScreen() => InitializeComponent();

    public async Task InitializeAsync(INavigationParameters parameters) => await ((AddMedicineViewModel)BindingContext).InitializeCommand.Execute(Unit.Default);
}