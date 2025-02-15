using FluentAssertions;
using NodaTime;
using NodaTime.Extensions;
using NSubstitute;
using Rx.Tracker.Features;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.Mediation;
using Rx.Tracker.Tests.Features.Schedule.Domain.Entities;
using Rx.Tracker.Tests.Features.Schedule.Domain.Queries;
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
        var now = DateTimeOffset.UnixEpoch.ToOffsetDateTime();
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

    [Theory]
    [ClassData(typeof(LoadScheduleClassData))]
    public async Task GivenLoadScheduleResult_WhenInitialized_ThenScheduleShouldBeForDate(MedicationSchedule medicationSchedule, DateTimeOffset dateTime)
    {
        // Given
        var now = dateTime.ToOffsetDateTime();
        var cqrs = Substitute.For<ICqrs>();
        var iClock = Substitute.For<IClock>();
        iClock.GetCurrentInstant().Returns(Instant.FromDateTimeOffset(dateTime));
        CoreServices coreServices = new CoreServicesFixture().WithClock(iClock);
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(Task.FromResult(new LoadSchedule.Result(medicationSchedule)));
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs).WithServices(coreServices);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.Schedule.Should().HaveCount(3).And.Subject.Should().OnlyContain(scheduledMedication => scheduledMedication.Day.Date == now.Date);
    }

    [Theory]
    [ClassData(typeof(LoadScheduleClassData))]
    public async Task GivenLoadScheduleResult_WhenInitialized_ThenScheduleShouldHaveMedications(MedicationSchedule medicationSchedule, DateTimeOffset dateTime)
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(Task.FromResult(new LoadSchedule.Result(medicationSchedule)));
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs).WithServices(CoreServicesStub.Instance(dateTime));

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.Schedule
           .SelectMany(daySchedule => daySchedule.Medication)
           .Should()
           .OnlyContain(scheduledMedication => scheduledMedication.ScheduledTime.Date == dateTime.ToOffsetDateTime().Date);
    }

    [Fact]
    public async Task GivenLoadScheduleResult_WhenInitialized_ThenTodayScheduleShouldNotBeNull()
    {
        // Given
        var epoch = DateTimeOffset.UnixEpoch;
        var now = epoch.ToOffsetDateTime();
        var cqrs = Substitute.For<ICqrs>();
        var iClock = Substitute.For<IClock>();
        iClock.GetCurrentInstant().Returns(Instant.FromDateTimeOffset(epoch));
        CoreServices coreServices = new CoreServicesFixture().WithClock(iClock);
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
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs).WithServices(coreServices);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.ScheduledMedications.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(LoadScheduleClassData))]
    public async Task GivenLoadScheduleResult_WhenInitialized_ThenScheduledMedicationsShouldBeForDate(
        MedicationSchedule medicationSchedule,
        DateTimeOffset dateTime
    )
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(Task.FromResult(new LoadSchedule.Result(medicationSchedule)));
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs).WithServices(CoreServicesStub.Instance(dateTime));

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.ScheduledMedications
           .Should()
           .OnlyContain(scheduledMedication => scheduledMedication.ScheduledTime.Date == dateTime.ToOffsetDateTime().Date);
    }

    [Theory]
    [ClassData(typeof(LoadScheduleClassData))]
    public async Task GivenLoadScheduleResult_WhenInitialized_ThenScheduledMedicationsShouldHaveMedications(
        MedicationSchedule medicationSchedule,
        DateTimeOffset dateTime
    )
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(Task.FromResult(new LoadSchedule.Result(medicationSchedule)));
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs).WithServices(CoreServicesStub.Instance(dateTime));

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.ScheduledMedications
           .Should()
           .OnlyContain(scheduledMedication => scheduledMedication.ScheduledTime.Date == dateTime.ToOffsetDateTime().Date);
    }
}