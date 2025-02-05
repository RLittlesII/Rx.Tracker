using Rx.Tracker.Features.Medications.Domain.Entities;

namespace Rx.Tracker.Features.Medications.Mapping;

/// <summary>
/// Maps <see cref="MedicationId"/>.
/// </summary>
public static class MedicationIdMapper
{
    /// <summary>
    /// Maps the id.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The medication id.</returns>
    public static MedicationId Map(MedicationId id) => id;
}