using NodaTime;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Entities;

internal class ScheduledMedicationFixture : ITestFixtureBuilder
{
    public static implicit operator ScheduledMedication(ScheduledMedicationFixture fixture) => fixture.Build();
    public ScheduledMedicationFixture WithScheduledTime(OffsetDateTime scheduledTime) => this.With(ref _scheduledTime, scheduledTime);
    public ScheduledMedicationFixture WithMedication(Medication medication) => this.With(ref _medication, medication);
    public ScheduledMedicationFixture WithMedication(Func<MedicationFixture, Medication> medication) => this.With(ref _medication, medication.Invoke(new MedicationFixture()));
    private ScheduledMedication Build() => new(this._scheduleId, this._mealRequirements, this._medication, this._recurrence, this._scheduledTime);

    private MealRequirements _mealRequirements = MealRequirements.None;
    private Medication _medication = new();
    private Recurrence _recurrence = Recurrence.Daily;
    private OffsetDateTime _scheduledTime;
    private ScheduleId _scheduleId = new ScheduleId();
}