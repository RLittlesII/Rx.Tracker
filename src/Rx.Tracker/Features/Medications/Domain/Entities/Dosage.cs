namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <summary>
/// Represents a dosage for a <see cref="Medication"/>.
/// </summary>
public class Dosage
{
    public DosageType Type { get; }

    public uint Quantity { get; }

    public DosageWeight Weight { get; }
}