using DryIoc;
using Microsoft.Extensions.Logging;
using Rx.Tracker.Container;
using Rx.Tracker.Features.Schedule.Data.Api;
using Rx.Tracker.Features.Schedule.Data.Store;
using Rx.Tracker.Features.Schedule.Domain;
using Rx.Tracker.Features.Schedule.ViewModels;

namespace Rx.Tracker.Features.Schedule.Container;

/// <inheritdoc />
public class ScheduleModule : ContainerModule
{
    /// <inheritdoc/>
    protected override IContainer Register(IContainer registrar)
    {
        registrar.RegisterDelegate<ScheduleStateMachine>(
            resolver => new ScheduleStateMachine(
                ScheduleStateMachine.ScheduleState.Initial,
                resolver.Resolve<ILoggerFactory>()));

        registrar.Register<IMedicationScheduleApiClient, MedicationScheduleClient>(ifAlreadyRegistered: IfAlreadyRegistered.Replace);
        registrar.Register<IMedicationScheduleApiContract, LocalMedicationScheduleStore>(ifAlreadyRegistered: IfAlreadyRegistered.Replace);

        return registrar;
    }
}