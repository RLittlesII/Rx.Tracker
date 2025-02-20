using NSubstitute;
using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Features.Schedule.ViewModels;

namespace Rx.Tracker.Tests.Features.Schedule.ViewModels;

// TODO: [rlittlesii: January 11, 2025] Fix AutoFixture nested classes + => .
internal class ScheduleStateMachineFixture : ITestFixtureBuilder
{
    public static implicit operator ScheduleStateMachine(ScheduleStateMachineFixture fixture) => fixture.Build();

    public ScheduleStateMachineFixture WithInitialState(ScheduleStateMachine.ScheduleState initialState)
        => this.With(ref _initialState, initialState);

    public ScheduleStateMachineFixture WithFactory(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory) => this.With(ref _loggerFactory, loggerFactory);
    private ScheduleStateMachine Build() => new(_initialState, _loggerFactory);
    private ScheduleStateMachine.ScheduleState _initialState = ScheduleStateMachine.ScheduleState.Initial;
    private Microsoft.Extensions.Logging.ILoggerFactory _loggerFactory = Substitute.For<Microsoft.Extensions.Logging.ILoggerFactory>();
}

[AutoFixture(typeof(ScheduleStateMachine))]
internal class ScheduleStateMachineFixture2
{
}