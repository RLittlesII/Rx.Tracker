namespace Rx.Tracker.Features.Medicine.Data.Dto;

/// <summary>
/// Represents a medicine data transport object.
/// </summary>
public record MedicineDto
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public required string Id { get; init; }
}