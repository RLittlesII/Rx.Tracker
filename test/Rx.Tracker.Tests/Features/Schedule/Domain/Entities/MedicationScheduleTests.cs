using DynamicData;
using FluentAssertions;
using LanguageExt;
using NodaTime.Extensions;
using Rx.Tracker.Extensions;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Entities;

public class MedicationScheduleTests
{
    [Fact]
    public async Task GivenScheduledMedication_WhenTake_ThenShouldNotBeEmpty()
    {
        // Given
        ScheduledMedication scheduledMedication = new ScheduledMedicationFixture();
        MedicationSchedule sut = new MedicationScheduleFixture().WithEnumerable([scheduledMedication]);
        using var _ =
            sut.Connect()
               .Filter(medication => medication.TakenTime.HasValue)
               .Bind(out var result)
               .Subscribe();

        // When
        await scheduledMedication.Take();

        // Then
        result
           .Should()
           .NotBeEmpty()
           .And
           .Subject
           .Should()
           .ContainSingle(medication => medication == scheduledMedication);
    }

    [Fact]
    public void GivenScheduledMedication_WhenFilterTaken_ThenShouldBeEmpty()
    {
        // Given
        MedicationSchedule sut = new MedicationScheduleFixture().WithEnumerable([new ScheduledMedicationFixture()]);

        // When
        using var _ = sut.Connect().Filter(medication => medication.TakenTime.HasValue).Bind(out var result).Subscribe();

        // Then
        result
           .Should()
           .BeEmpty();
    }

    [Fact]
    public void ScheduledMedication_WhenGroupedByDayOfWeek_ThenGroupContainsMedication()
    {
        // Given
        var now = new DateTimeOffset(new DateTime(2025, 01, 05));
        MedicationSchedule sut = new MedicationScheduleFixture().WithEnumerable(
            [
                new ScheduledMedicationFixture().WithScheduledTime(now.AddDays(-4).ToOffsetDateTime()),
                new ScheduledMedicationFixture().WithScheduledTime(now.AddDays(-3).ToOffsetDateTime()),
                new ScheduledMedicationFixture().WithScheduledTime(now.AddDays(-2).ToOffsetDateTime()),
                new ScheduledMedicationFixture().WithScheduledTime(now.AddDays(-1).ToOffsetDateTime()),
                new ScheduledMedicationFixture().WithScheduledTime(now.ToOffsetDateTime())
            ]
        ).WithToday(now.ToLocalDate());

        // When
        using var _ = sut.Connect()
           .Filter(medication => medication.TakenTime.IsNull())
           .GroupOnProperty(medication => medication.ScheduledTime.DayOfWeek)
           .Bind(out var result)
           .Subscribe();

        // Then
        result
           .Should()
           .NotBeNullOrEmpty()
           .And
           .Subject
           .Should()
           .SatisfyRespectively(
                first => first.Cache.Items.Should().ContainSingle(),
                second => second.Cache.Items.Should().ContainSingle(),
                third => third.Cache.Items.Should().ContainSingle(),
                fourth => fourth.Cache.Items.Should().ContainSingle(),
                fifth => fifth.Cache.Items.Should().ContainSingle()
            );
    }
}