using DynamicData;
using FluentAssertions;
using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Entities;

public class MedicationScheduleTests
{
    [Fact]
    public async Task Given_When_ThenShouldNotBeEmpty()
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
    public void Given_When_ThenShouldBeEmpty()
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
}

[AutoFixture(typeof(MedicationSchedule))]
internal partial class MedicationScheduleFixture;