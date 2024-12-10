using System.Collections.Generic;
using System.Linq;

namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <summary>
/// A medicine.
/// </summary>
public class Medication
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Medication"/> class.
    /// </summary>
    public Medication()
        : this(new MedicationId(), [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Medication"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="dosages">The dosages.</param>
    public Medication(MedicationId id, IEnumerable<Dosage> dosages)
    {
        Id = id;
        Dosages = dosages.ToArray();
    }

    /// <summary>
    /// Gets the medicine identifier.
    /// </summary>
    public MedicationId Id { get; }

    /// <summary>
    /// Gets the dosages available for the medication.
    /// </summary>
    public IReadOnlyCollection<Dosage> Dosages { get; }

    /// <summary>
    /// Gets the distinct available dosages for the medication.
    /// </summary>
    /// <returns>The distinct dosages.</returns>
    public IReadOnlyCollection<Dosage> AvailableDosage() => Dosages
       .GroupBy(dosage => dosage.Weight, dosage => dosage)
       .SelectMany(grouping => grouping.DistinctBy(dosage => (Quantity: dosage.Amount, dosage.Weight)))
       .ToArray();
}