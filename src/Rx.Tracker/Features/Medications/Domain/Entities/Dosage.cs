namespace Rx.Tracker.Features.Medications.Domain.Entities;

public class Dosage
{
    public DosageType Type { get; }

    public uint Quantity { get; }

    public DosageWeight Weight { get; }
}