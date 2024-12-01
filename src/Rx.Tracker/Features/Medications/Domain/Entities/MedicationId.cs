namespace Rx.Tracker.Features.Medicine.Domain.Entities;

/// <inheritdoc />
public record MedicineId : Id
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MedicineId"/> class.
    /// </summary>
    public MedicineId()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MedicineId"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public MedicineId(string value)
        : base(value)
    {
    }

    public static implicit operator MedicineId(string value) => new(value);
}