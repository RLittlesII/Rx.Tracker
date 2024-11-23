using Prism.Ioc;
using Rx.Tracker.Navigation;

namespace Rx.Tracker.UI.Container;

public class PrismNavigationModule : ContainerRegistryModule
{
    /// <inheritdoc />
    protected override IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry)
        => containerRegistry.RegisterForNavigation<Features.Main.MainPage>(Routes.Instance.MainNavigation.Name);
}