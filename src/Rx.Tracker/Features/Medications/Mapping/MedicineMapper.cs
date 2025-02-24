using Riok.Mapperly.Abstractions;
using Rx.Tracker.Features.Medications.Data.Dto;
using Rx.Tracker.Features.Medications.Domain.Entities;

namespace Rx.Tracker.Features.Medications.Mapping;

/// <summary>
/// Represents a mapper for <see cref="Medication" />.
/// </summary>
[Mapper]
public static partial class MedicineMapper
{
    public static partial MedicineDto Map(Medication medication);
}