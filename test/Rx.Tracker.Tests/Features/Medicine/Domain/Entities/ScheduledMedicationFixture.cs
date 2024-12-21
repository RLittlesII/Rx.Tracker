using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using System;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Entities;

internal class ScheduledMedicationFixture : ITestFixtureBuilder
{
    public static implicit operator ScheduledMedication(ScheduledMedicationFixture fixture) => fixture.Build();
    public ScheduledMedicationFixture WithTaken(DateTimeOffset taken) => this.With(ref _taken, taken);
    private ScheduledMedication Build() => new(this._scheduleId, this._mealRequirements, this._medication, this._recurrence, this._scheduledTime) { TakenTime = _taken };

    private MealRequirements _mealRequirements = MealRequirements.None;
    private Medication _medication = new();
    private Recurrence _recurrence = Recurrence.Daily;
    private DateTimeOffset _scheduledTime = DateTimeOffset.UtcNow;
    private DateTimeOffset? _taken;
    private ScheduleId _scheduleId = new ScheduleId();
}