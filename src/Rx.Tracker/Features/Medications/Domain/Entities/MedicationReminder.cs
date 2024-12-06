using System;

namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <summary>
/// Represents a reminder to take a medication.
/// </summary>
public record MedicationReminder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MedicationReminder"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="medication">The medication.</param>
    /// <param name="reminderTime">The reminder time.</param>
    /// <param name="takenTime">The taken time.</param>
    /// <param name="mealRequirement">The meal requirements.</param>
    public MedicationReminder(Id id, Medication medication, DateTimeOffset reminderTime, DateTimeOffset? takenTime, MealRequirements mealRequirement)
    {
        Id = id;
        Medication = medication;
        ReminderTime = reminderTime;
        TakenTime = takenTime;
        MealRequirement = mealRequirement;
    }

    /// <summary>
    /// Gets the reminder id.
    /// </summary>
    public Id Id { get; }

    /// <summary>
    /// Gets the medication.
    /// </summary>
    public Medication Medication { get; }

    /// <summary>
    /// Gets the time the medication reminder happens.
    /// </summary>
    public DateTimeOffset ReminderTime { get; }

    /// <summary>
    /// Gets the time the medication was taken.
    /// </summary>
    public DateTimeOffset? TakenTime { get; }

    /// <summary>
    /// Gets the meal requirement.
    /// </summary>
    public MealRequirements MealRequirement { get; }
}