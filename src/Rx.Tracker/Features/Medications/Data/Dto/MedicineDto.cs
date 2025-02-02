using System.Collections.Generic;
using Rx.Tracker.Features.Medications.Domain.Entities;

namespace Rx.Tracker.Features.Medications.Data.Dto;

/// <summary>
/// Represents a medicine data transfer object.
/// </summary>
public record MedicineDto(MedicationId Id, IReadOnlyCollection<Dosage> Dosages);
