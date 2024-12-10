namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <summary>
/// Medication Type.
/// </summary>
public enum DosageType
{
    /// <summary>
    /// Chewable.
    /// </summary>
    Chewable,

    /// <summary>
    /// Capsule.
    /// </summary>
    Capsule,

    /// <summary>
    /// Tablet.
    /// </summary>
    Tablet,

    /// <summary>
    /// Injection.
    /// </summary>
    Injection,

    /// <summary>
    /// Topical.
    /// </summary>
    Topical,

    /// <summary>
    /// Inhalant.
    /// </summary>
    Inhalant
}