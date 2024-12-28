using FluentAssertions;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Entities;

public class ScheduledMedicationTests
{
    [Fact]
    public async Task Given_WhenTake_ThenTakenChanged()
    {
        // Given
        var result = string.Empty;
        ScheduledMedication sut = new ScheduledMedicationFixture();
        sut.Changed.Select(args => args.PropertyName).Subscribe(propertyName => result = propertyName);

        // When
        await sut.Take();

        // Then
        result
           .Should()
           .NotBeNull()
           .And
           .Subject
           .Should()
           .Be(nameof(ScheduledMedication.TakenTime));
    }
}