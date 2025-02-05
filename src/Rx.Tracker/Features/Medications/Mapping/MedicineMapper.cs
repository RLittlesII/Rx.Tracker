using Riok.Mapperly.Abstractions;
using Rx.Tracker.Features.Medications.Data.Dto;
using Rx.Tracker.Features.Medications.Domain.Entities;

namespace Rx.Tracker.Features.Medications.Mapping;

/// <summary>
/// Represents a mapper for <see cref="Medication"/>.
/// </summary>
[Mapper]
[UseStaticMapper(typeof(MedicationIdMapper))]
public static partial class MedicineMapper
{
    public static partial MedicineDto Map(Medication medication);

    /// <summary>
    /// Maps the medication.
    /// </summary>
    /// <param name="medication">The dto.</param>
    /// <returns>Medication.</returns>
    public static Medication Map(MedicineDto medication) => new Medication(medication.Id, medication.Dosages);
}