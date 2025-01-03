using DynamicData;
using FluentAssertions;
using LanguageExt;
using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
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
           .NotBeEmpty();
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
        MedicationSchedule sut = new MedicationScheduleFixture().WithEnumerable(
            [
                new ScheduledMedicationFixture().WithScheduledTime(DateTimeOffset.Now.AddDays(-4)),
                new ScheduledMedicationFixture().WithScheduledTime(DateTimeOffset.Now.AddDays(-3)),
                new ScheduledMedicationFixture().WithScheduledTime(DateTimeOffset.Now.AddDays(-2)),
                new ScheduledMedicationFixture().WithScheduledTime(DateTimeOffset.Now.AddDays(-1)),
                new ScheduledMedicationFixture().WithScheduledTime(DateTimeOffset.Now)
            ]
        );

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

[AutoFixture(typeof(MedicationSchedule))]
internal partial class MedicationScheduleFixture;