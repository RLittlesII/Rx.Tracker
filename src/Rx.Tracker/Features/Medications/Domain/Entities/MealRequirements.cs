namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <summary>
/// Does the medication require a meal.
/// </summary>
public enum MealRequirements
{
    /// <summary>
    /// No meal requirements.
    /// </summary>
    None,

    /// <summary>
    /// Before a meal.
    /// </summary>
    Before,

    /// <summary>
    /// After a meal.
    /// </summary>
    After
}