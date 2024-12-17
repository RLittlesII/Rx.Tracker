namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <summary>
/// Represents a dosage for a <see cref="Medication"/>.
/// </summary>
public class Dosage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Dosage"/> class.
    /// </summary>
    /// <param name="type">The way to dosage is taken.</param>
    /// <param name="amount">The amount to take.</param>
    /// <param name="weight">The weight of the dosage.</param>
    public Dosage(DosageType type, uint amount, DosageWeight weight)
    {
        Type = type;
        Amount = amount;
        Weight = weight;
    }

    /// <summary>
    /// Gets the type.
    /// </summary>
    public DosageType Type { get; }

    /// <summary>
    /// Gets the quantity.
    /// </summary>
    public uint Amount { get; }

    /// <summary>
    /// Gets the weight.
    /// </summary>
    public DosageWeight Weight { get; }
}