using NodaTime;
using Rx.Tracker.Features.Medications.Data.Dto;
using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.Features.Schedule.Data.Dto;

/// <summary>
/// Represents a scheduled medicine data transfer object.
/// </summary>
public record ScheduledMedicineDto(
    Id Id,
    MealRequirements MealRequirement,
    Recurrence Recurrence,
    OffsetDateTime ScheduledTime,
    OffsetDateTime? TakenTime) : MedicineDto(Id);