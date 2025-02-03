using FluentAssertions;
using NodaTime;
using NodaTime.Extensions;
using NSubstitute;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.Mediation;
using Rx.Tracker.Tests.Features.Schedule.Domain.Entities;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Schedule.ViewModels;

public partial class ScheduleViewModelTests
{
    [Fact]
    public async Task GivenLoadScheduleResult_WhenInitialized_ThenScheduleShouldNotBeNull()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        var now = DateTimeOffset.UtcNow.ToOffsetDateTime();
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(
            Task.FromResult(
                new LoadSchedule.Result(
                    new MedicationScheduleFixture().WithEnumerable(
                        [
                            new ScheduledMedicationFixture().WithScheduledTime(now),
                        ]
                    ).WithToday(now.Date)
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
    public async Task GivenLoadScheduleResult_WhenInitialized_ThenScheduleShouldBeForDate()
    {
        // Given
        var now = DateTimeOffset.Now.ToOffsetDateTime();
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(
            Task.FromResult(
                new LoadSchedule.Result(
                    new MedicationScheduleFixture().WithEnumerable(
                        [
                            new ScheduledMedicationFixture().WithScheduledTime(now),
                            new ScheduledMedicationFixture().WithScheduledTime(now.Plus(Duration.FromHours(2))),
                            new ScheduledMedicationFixture().WithScheduledTime(now.Plus(Duration.FromHours(4))),
                            new ScheduledMedicationFixture().WithScheduledTime(now.Plus(Duration.FromDays(2))),
                        ]
                    ).WithToday(now.Date)
                )
            )
        );
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.Schedule.Should().HaveCount(3).And.Subject.Should().OnlyContain(scheduledMedication => scheduledMedication.Day.Date == now.Date);
    }

    [Fact]
    public async Task GivenLoadScheduleResult_WhenInitialized_ThenScheduleShouldHaveMedications()
    {
        // Given
        const string ibuprofen = "Ibuprofen";
        var now = DateTimeOffset.Now.ToOffsetDateTime();
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(
            Task.FromResult(
                new LoadSchedule.Result(
                    new MedicationScheduleFixture().WithEnumerable(
                        [
                            new ScheduledMedicationFixture().WithMedication(medication => medication.WithId(ibuprofen)).WithScheduledTime(now),
                            new ScheduledMedicationFixture().WithMedication(medication => medication.WithId(ibuprofen)).WithScheduledTime(now.Plus(Duration.FromHours(2))),
                            new ScheduledMedicationFixture().WithMedication(medication => medication.WithId(ibuprofen)).WithScheduledTime(now.Plus(Duration.FromHours(4))),
                            new ScheduledMedicationFixture().WithScheduledTime(now),
                            new ScheduledMedicationFixture().WithScheduledTime(now.Plus(Duration.FromHours(2))),
                            new ScheduledMedicationFixture().WithScheduledTime(now.Plus(Duration.FromHours(4))),
                        ]
                    ).WithToday(now.Date)
                )
            )
        );
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.Schedule
           .SelectMany(daySchedule => daySchedule.Medication)
           .Should()
           .HaveCount(6)
           .And
           .Subject
           .Should()
           .OnlyContain(scheduledMedication => scheduledMedication.ScheduledTime.Date == now.Date);
    }
}

public partial class  ScheduleViewModelTests
{
    [Fact]
    public async Task Given_WhenAddMedication_ThenShouldReturnUnit()
    {
        // Given
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithStateMachineFactory(() => new ScheduleStateMachineFixture().WithInitialState(ScheduleStateMachine.ScheduleState.DaySchedule));

        // When
        var result = await sut.AddMedicineCommand.Execute(Unit.Default);

        // Then
        result.Should().Be(Unit.Default);
    }
    [Fact]
    public async Task Given_WhenAddMedication_ThenShouldBeInDayScheduleState()
    {
        // Given
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithStateMachineFactory(() => new ScheduleStateMachineFixture().WithInitialState(ScheduleStateMachine.ScheduleState.DaySchedule));

        // When
        await sut.AddMedicineCommand.Execute(Unit.Default);

        // Then
        sut.CurrentState.Should().Be(ScheduleStateMachine.ScheduleState.DaySchedule);
    }
}

