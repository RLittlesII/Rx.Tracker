using DryIoc;
using Microsoft.Extensions.Logging;
using Rx.Tracker.Container;
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

        return registrar;
    }
}