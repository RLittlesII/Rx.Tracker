using System;

namespace Rx.Tracker.Features.Medications.Domain.Entities;

public class MedicationReminder
{
    public Id Id { get; }

    public Medication Medication { get; }

    public DateTimeOffset ReminderTime { get; }

    public DateTimeOffset? TakenTime { get; }

    /// <summary>
    /// Gets the meal requirement.
    /// </summary>
    public MealRequirements MealRequirement { get; }
}