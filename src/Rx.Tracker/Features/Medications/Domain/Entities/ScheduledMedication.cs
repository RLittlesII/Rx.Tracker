using System;

namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <summary>
/// Represents a <see cref="Medication"/> schedule to be taken at a certain time.
/// </summary>
public class ScheduledMedication
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduledMedication"/> class.
    /// </summary>
    /// <param name="mealRequirement">The meal requirements.</param>
    /// <param name="medication">The medication.</param>
    /// <param name="recurrence">The recurrence.</param>
    /// <param name="scheduledTime">The scheduled time.</param>
    public ScheduledMedication(
        MealRequirements mealRequirement,
        Medication medication,
        Recurrence recurrence,
        DateTimeOffset scheduledTime)
        : this(new Id(), mealRequirement, medication, recurrence, scheduledTime)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduledMedication"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="mealRequirement">The meal requirements.</param>
    /// <param name="medication">The medication.</param>
    /// <param name="recurrence">The recurrence.</param>
    /// <param name="scheduledTime">The scheduled time.</param>
    public ScheduledMedication(Id id, MealRequirements mealRequirement, Medication medication, Recurrence recurrence, DateTimeOffset scheduledTime)
    {
        Id = id;
        MealRequirement = mealRequirement;
        Medication = medication;
        Recurrence = recurrence;
        ScheduledTime = scheduledTime;
    }

    /// <summary>
    /// Gets the id.
    /// </summary>
    public Id Id { get; }

    /// <summary>
    /// Gets the meal requirement.
    /// </summary>
    public MealRequirements MealRequirement { get; }

    /// <summary>
    /// Gets the medication.
    /// </summary>
    public Medication Medication { get; }

    /// <summary>
    /// Gets the recurrence.
    /// </summary>
    public Recurrence Recurrence { get; }

    /// <summary>
    /// Gets the scheduled time.
    /// </summary>
    public DateTimeOffset ScheduledTime { get; }
}