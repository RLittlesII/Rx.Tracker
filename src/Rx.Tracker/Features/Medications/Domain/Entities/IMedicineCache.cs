using DynamicData;

namespace Rx.Tracker.Features.Medicine.Domain.Entities;

/// <summary>
/// Interface representing the global cache of medicine.
/// </summary>
public interface IMedicineCache : ISourceCache<Medication, MedicineId>;