namespace Rx.Tracker.Features.Medicine.Domain.Entities;

/// <summary>
/// The recurrence of taking a medication.
/// </summary>
public enum Recurrence
{
    /// <summary>
    /// One and done.
    /// </summary>
    OneTime,

    /// <summary>
    /// One a day.
    /// </summary>
    Daily,

    /// <summary>
    /// Two times.
    /// </summary>
    TwiceDaily,

    /// <summary>
    /// Once a week.
    /// </summary>
    Weekly,

    /// <summary>
    /// Once a month.
    /// </summary>
    Monthly
}