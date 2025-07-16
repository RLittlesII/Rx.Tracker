using Rx.Tracker.Features.Medications.Domain.Entities;
using System.Collections.Generic;

namespace Rx.Tracker.Features.Medications.Data.Dto;

/// <summary>
/// Represents a medicine data transfer object.
/// </summary>
public record MedicineDto(MedicationId Id, IReadOnlyCollection<Dosage> Dosages);