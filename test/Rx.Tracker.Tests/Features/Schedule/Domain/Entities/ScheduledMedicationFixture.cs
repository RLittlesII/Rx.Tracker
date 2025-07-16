using NodaTime;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Entities;

internal class ScheduledMedicationFixture : ITestFixtureBuilder
{
    public ScheduledMedicationFixture WithScheduledTime(OffsetDateTime scheduledTime) => this.With(ref _scheduledTime, scheduledTime);
    public ScheduledMedicationFixture WithMedication(Medication medication) => this.With(ref _medication, medication);

    public ScheduledMedicationFixture WithMedication(Func<MedicationFixture, Medication> medication)
        => this.With(ref _medication, medication.Invoke(new MedicationFixture()));

    public static implicit operator ScheduledMedication(ScheduledMedicationFixture fixture) => fixture.Build();
    private ScheduledMedication Build() => new(_scheduleId, _mealRequirements, _medication, _recurrence, _scheduledTime);

    private readonly MealRequirements _mealRequirements = MealRequirements.None;
    private Medication _medication = new();
    private readonly Recurrence _recurrence = Recurrence.Daily;
    private OffsetDateTime _scheduledTime;
    private readonly ScheduleId _scheduleId = new();
}