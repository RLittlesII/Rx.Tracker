using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <summary>
/// Represents a collection of medication.
/// </summary>
public class MedicationCollection : IReadOnlyCollection<Medication>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MedicationCollection"/> class.
    /// </summary>
    /// <param name="medications">The medications.</param>
    public MedicationCollection(IEnumerable<Medication> medications)
    {
        _medications = medications.ToList();
        Dosages = _medications
           .SelectMany(medication => medication.Dosages)
           .GroupBy(dosage => dosage.Weight, dosage => dosage)
           .SelectMany(grouping => grouping.DistinctBy(dosage => (dosage.Amount, dosage.Weight)))
           .ToArray();
    }

    /// <summary>
    /// Gets the dosages.
    /// </summary>
    public IReadOnlyCollection<Dosage> Dosages { get; }

    /// <inheritdoc/>
    public IEnumerator<Medication> GetEnumerator() => _medications.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc/>
    public int Count => _medications.Count;

    private List<Medication> _medications;
}