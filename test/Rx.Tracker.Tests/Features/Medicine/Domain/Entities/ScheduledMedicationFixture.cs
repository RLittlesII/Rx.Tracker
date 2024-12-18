using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Features.Medications.Domain.Entities;
using System;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Entities;

internal class ScheduledMedicationFixture : ITestFixtureBuilder
{
    public static implicit operator ScheduledMedication(ScheduledMedicationFixture fixture) => fixture.Build();

    private ScheduledMedication Build() => new(this._mealRequirements, this._medication, this._recurrence, this._scheduledTime);

    private MealRequirements _mealRequirements = MealRequirements.None;
    private Medication _medication = new();
    private Recurrence _recurrence = Recurrence.Daily;
    private DateTimeOffset _scheduledTime = DateTimeOffset.UtcNow;
}