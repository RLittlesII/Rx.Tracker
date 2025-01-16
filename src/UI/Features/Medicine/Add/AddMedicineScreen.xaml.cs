using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Prism.Navigation;
using ReactiveMarbles.Extensions;
using ReactiveUI;

namespace Rx.Tracker.UI.Features.Medicine.Add;

public partial class AddMedicineScreen
{
    public AddMedicineScreen() => InitializeComponent();

    protected override Task Initialize(INavigationParameters parameters)
    {
        this.WhenActivated(
            disposable =>
                this.BindInteraction(ViewModel, model => model.FailedInteraction, context => Toast.Make(context.Input.Message, ToastDuration.Long).Show())
                   .DisposeWith(disposable));

        return Task.CompletedTask;
    }
}