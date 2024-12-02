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
}