namespace Rx.Tracker.Features.Medicine.Data;

/// <summary>
/// Represents a medicine data transport object.
/// </summary>
public record MedicineDto
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public string Id { get; init; }
}