using Microsoft.Extensions.Logging;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Features.Schedule.ViewModels;

namespace Rx.Tracker.Tests.Features.Schedule.ViewModels;

// TODO: [rlittlesii: January 11, 2025] Fix AutoFixture nested classes + => .
internal class ScheduleStateMachineFixture : ITestFixtureBuilder
{
    public ScheduleStateMachineFixture WithInitialState(ScheduleStateMachine.ScheduleState initialState) => this.With(ref _initialState, initialState);

    public ScheduleStateMachineFixture WithFactory(ILoggerFactory loggerFactory) => this.With(ref _loggerFactory, loggerFactory);
    public static implicit operator ScheduleStateMachine(ScheduleStateMachineFixture fixture) => fixture.Build();
    private ScheduleStateMachine Build() => new(_initialState, _loggerFactory);
    private ScheduleStateMachine.ScheduleState _initialState = ScheduleStateMachine.ScheduleState.Initial;
    private ILoggerFactory _loggerFactory = Substitute.For<ILoggerFactory>();
}