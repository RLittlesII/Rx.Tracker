using System.Collections.Generic;

namespace Rx.Tracker.Features.Schedule.Data.Dto;

/// <summary>
/// Represents a schedule data transfer object.
/// </summary>
public record MedicationScheduleDto(IEnumerable<ScheduledMedicineDto> Medicine);