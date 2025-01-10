using Prism.Ioc;
using Rx.Tracker.Features.Schedule.Data.Api;
using Rx.Tracker.Features.Schedule.Domain;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.UI.Container;

namespace Rx.Tracker.UI.Features.Schedule;

public class ScheduleModule : ContainerRegistryModule
{
    protected override IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry) => containerRegistry
       .RegisterForNavigation<ScheduleScreen, ScheduleViewModel>()
       .Register<ScheduleStateMachine>()
       .Register<LoadSchedule.QueryHandler>()
       .Register<IMedicationScheduleApiClient, MedicationScheduleClient>();
}