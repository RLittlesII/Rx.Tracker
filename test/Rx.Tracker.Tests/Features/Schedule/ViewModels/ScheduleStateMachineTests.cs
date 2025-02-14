using DryIoc;
using FluentAssertions;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.Tests.Container;
using System;

namespace Rx.Tracker.Tests.Features.Schedule.ViewModels;

public class ScheduleStateMachineTests
{
    [Fact]
    public void GivenStateMachine_WhenResolve_ThenShouldBeScheduleStateMachineFactory() =>
        // Given, When, Then
        new ContainerFixture()
           .WithRegistration(registrar => registrar.Register<ScheduleStateMachine>())
           .AsInterface()
           .Resolve<Func<ScheduleStateMachine.ScheduleState, ScheduleStateMachine>>()
           .Should()
           .NotBeNull();

    [Fact]
    public void GivenStateMachineFactory_WhenInvoke_ThenShouldBeScheduleStateMachine() =>
        // Given, When, Then
        new ContainerFixture().WithRegistration(registrar => registrar.Register<ScheduleStateMachine>())
           .AsInterface()
           .Resolve<Func<ScheduleStateMachine.ScheduleState, ScheduleStateMachine>>()
           .Invoke(ScheduleStateMachine.ScheduleState.Initial)
           .Should()
           .BeOfType<ScheduleStateMachine>();
}