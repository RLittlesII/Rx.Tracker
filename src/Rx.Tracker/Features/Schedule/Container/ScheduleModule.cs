using DryIoc;
using Rx.Tracker.Container;
using Rx.Tracker.Features.Schedule.Data.Api;
using Rx.Tracker.Features.Schedule.Domain;
using Rx.Tracker.Features.Schedule.ViewModels;

namespace Rx.Tracker.Features.Schedule.Container;

/// <inheritdoc />
public class ScheduleModule : ContainerModule
{
    /// <inheritdoc/>
    protected override IContainer Register(IContainer registrar)
    {
        registrar.Register<ScheduleStateMachine>();
        registrar.Register<IMedicationScheduleApiClient, MedicationScheduleClient>();

        return registrar;
    }
}