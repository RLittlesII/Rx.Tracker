using FluentAssertions;
using NSubstitute;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.Mediation;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using Rx.Tracker.Tests.Features.Schedule.Domain.Entities;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Schedule.ViewModels;

public class ScheduleViewModelTests
{
    [Fact]
    public void Given_When_Then()
    {
        // Given
        ScheduleViewModel sut = new ScheduleViewModelFixture();

        // When

        // Then
    }

    [Fact]
    public void Given_When_ThenCurrentStateShouldBeInitial()
    {
        // Given
        ScheduleViewModel sut = new ScheduleViewModelFixture();

        // When

        // Then
        sut.CurrentState.Should().Be(ScheduleStateMachine.ScheduleState.Initial);
    }

    [Fact]
    public async Task Given_When_ThenCurrentStateShouldBeBusy()
    {
        // Given
        ScheduleViewModel sut = new ScheduleViewModelFixture();

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.CurrentState.Should().Be(ScheduleStateMachine.ScheduleState.Busy);
    }

    [Fact]
    public async Task Given_When_ThenCurrentStateShouldBeLoaded()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(Task.FromResult(new LoadSchedule.Result(new MedicationScheduleFixture().WithEnumerable([]))));
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.CurrentState.Should().Be(ScheduleStateMachine.ScheduleState.Loaded);
    }

    [Fact]
    public async Task Given_WhenLoaded_ThenWeekShouldNotBeNull()
    {
        // Given
        ScheduleViewModel sut = new ScheduleViewModelFixture();

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.Week.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Given_WhenLoaded_ThenScheduleShouldNotBeNull()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(
            Task.FromResult(
                new LoadSchedule.Result(
                    new MedicationScheduleFixture().WithEnumerable(
                        [
                            new ScheduledMedicationFixture().WithScheduledTime(DateTimeOffset.UtcNow),
                        ]
                    )
                )
            )
        );
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.Schedule.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Given_WhenLoaded_ThenScheduleShouldBeForDate()
    {
        // Given
        var now = DateTimeOffset.UtcNow;
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(
            Task.FromResult(
                new LoadSchedule.Result(
                    new MedicationScheduleFixture().WithEnumerable(
                        [
                            new ScheduledMedicationFixture().WithScheduledTime(now),
                            new ScheduledMedicationFixture().WithScheduledTime(now),
                            new ScheduledMedicationFixture().WithScheduledTime(now),
                            new ScheduledMedicationFixture().WithScheduledTime(now.AddDays(2)),
                        ]
                    )
                )
            )
        );
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.Schedule.Should().HaveCount(3).And.Subject.Should().OnlyContain(scheduledMedication => scheduledMedication.ScheduledTime == now);
    }
}