using Prism.Ioc;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.Navigation;
using Rx.Tracker.UI.Container;

namespace Rx.Tracker.UI.Features.Schedule;

public class ScheduleModule : ContainerRegistryModule
{
    protected override IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry) => containerRegistry
       .RegisterForNavigation<ScheduleScreen, ScheduleViewModel>(Routes.Instance.Schedule.Name);
}