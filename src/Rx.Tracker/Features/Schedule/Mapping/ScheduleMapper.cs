using Riok.Mapperly.Abstractions;
using Rx.Tracker.Features.Medications.Mapping;
using Rx.Tracker.Features.Schedule.Data.Dto;
using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.Features.Schedule.Mapping;

/// <summary>
/// Represents a mapper for <see cref="ScheduledMedication"/>.
/// </summary>
[Mapper]
[UseStaticMapper(typeof(MedicineMapper))]
[UseStaticMapper(typeof(MedicationIdMapper))]
public static partial class ScheduleMapper
{
    [MapperIgnoreSource(nameof(ScheduledMedication.Changing))]
    [MapperIgnoreSource(nameof(ScheduledMedication.Changed))]
    [MapperIgnoreSource(nameof(ScheduledMedication.ThrownExceptions))]
    public static partial ScheduledMedicineDto Map(ScheduledMedication request);

    [MapperIgnoreTarget(nameof(ScheduledMedication.Changing))]
    [MapperIgnoreTarget(nameof(ScheduledMedication.Changed))]
    [MapperIgnoreTarget(nameof(ScheduledMedication.ThrownExceptions))]
    public static partial ScheduledMedication Map(ScheduledMedicineDto request);
}