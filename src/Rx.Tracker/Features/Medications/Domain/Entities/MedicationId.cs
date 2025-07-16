namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <inheritdoc />
public record MedicationId : Id
{
    /// <inheritdoc />
    public override string ToString() => Value;

    public static implicit operator MedicationId(string value) => new(value);

    /// <summary>
    /// Initializes a new instance of the <see cref="MedicationId" /> class.
    /// </summary>
    public MedicationId()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MedicationId" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public MedicationId(string value)
        : base(value)
    {
    }
}