namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <summary>
/// Represents a dosage for a <see cref="Medication"/>.
/// </summary>
public class Dosage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Dosage"/> class.
    /// </summary>
    /// <param name="amount">The amount to take.</param>
    /// <param name="type">The way to dosage is taken.</param>
    /// <param name="weight">The weight of the dosage.</param>
    public Dosage(uint amount, DosageType type, DosageWeight weight)
    {
        Amount = amount;
        Type = type;
        Weight = weight;
    }

    /// <summary>
    /// Factory method for creating dosages weighed in milligrams.
    /// </summary>
    /// <param name="amount">The dosage amount in milligrams.</param>
    /// <param name="type">The dosage type.</param>
    /// <returns>A dosage in milligrams.</returns>
    public static Dosage Milligrams(uint amount, DosageType type = DosageType.Tablet) => new Dosage(amount, type, DosageWeight.Milligrams);

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

    /// <inheritdoc/>
    public override string ToString() => $"{Amount} {Weight} - {Type}";
}