using DynamicData;
using FluentAssertions;
using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Entities;

public class MedicationScheduleTests
{
    [Fact]
    public void Given_When_ThenShouldBeEmpty()
    {
        // Given
        MedicationSchedule sut = new MedicationScheduleFixture().WithEnumerable([new ScheduledMedicationFixture()]);
        using var _ = sut.Connect(medication => medication.TakenTime.HasValue).Bind(out var result).Subscribe();

        // When

        // Then
        result
           .Should()
           .BeEmpty();
    }

    [Fact]
    public void Given_When_ThenShouldNotBeEmpty()
    {
        // Given
        MedicationSchedule sut = new MedicationScheduleFixture().WithEnumerable([new ScheduledMedicationFixture().WithTaken(DateTimeOffset.Now)]);

        using var _ = sut.Connect(medication => medication.TakenTime.HasValue).Bind(out var result).Subscribe();

        // When

        // Then
        result
           .Should()
           .NotBeEmpty();
    }
}

[AutoFixture(typeof(MedicationSchedule))]
internal partial class MedicationScheduleFixture;