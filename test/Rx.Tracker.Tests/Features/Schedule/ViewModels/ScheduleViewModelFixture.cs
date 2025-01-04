using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rx.Tracker.Features.Schedule.ViewModels;

namespace Rx.Tracker.Tests.Features.Schedule.ViewModels;

[AutoFixture(typeof(ScheduleViewModel))]
internal partial class ScheduleViewModelFixture
{
    internal ScheduleViewModelFixture() => _stateMachineFactory = () => new ScheduleStateMachineFixture();
}