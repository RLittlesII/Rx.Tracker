using DryIoc;
using FluentAssertions;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.Tests.Container;
using System;

namespace Rx.Tracker.Tests.Features.Schedule.ViewModels;

public class ScheduleStateMachineTests
{
    [Fact]
    public void Given_When_Then()
    {
        // Given
        var container = new ContainerFixture().AsInterface();

        // When
        container.Register<ScheduleStateMachine>();

        // Then
        container.Resolve<Func<ScheduleStateMachine.ScheduleState, ScheduleStateMachine>>().Should().NotBeNull();
    }

    [Fact]
    public void Given_WhenInvoke_Then()
    {
        // Given
        var container = new ContainerFixture().WithRegistration(registrar => registrar.Register<ScheduleStateMachine>()).AsInterface();
        var factory = container.Resolve<Func<ScheduleStateMachine.ScheduleState, ScheduleStateMachine>>();

        // When, Then
        factory.Invoke(ScheduleStateMachine.ScheduleState.Initial).Should().BeOfType<ScheduleStateMachine>();
    }
}