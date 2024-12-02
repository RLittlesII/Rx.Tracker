using DynamicData;

namespace Rx.Tracker.Features.Medications.Domain.Entities;

/// <summary>
/// Interface representing the global cache of medicine.
/// </summary>
public interface IMedicineCache : ISourceCache<Medication, MedicationId>;