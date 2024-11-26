using Prism.Ioc;
using Rx.Tracker.Navigation;
using Rx.Tracker.UI.Container;
using Rx.Tracker.UI.Features.Main;
using Rx.Tracker.UI.Navigation;

namespace Rx.Tracker.UI;

public class UiModule : ContainerRegistryModule
{
    protected override IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry) => containerRegistry
       .RegisterScoped<INavigator, Navigator>()
       .RegisterModule<MainModule>();
}