using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Features.Schedule.Data.Api;
using Rx.Tracker.Features.Schedule.Data.Dto;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using Rx.Tracker.Features.Schedule.Mapping;

namespace Rx.Tracker.Features.Schedule.Data.Store;

public class LocalMedicationScheduleStore : IMedicationScheduleApiContract
{
    /// <inheritdoc/>
    public Task Create(UserId userId, AddMedicationToSchedule.Command command)
    {
        _schedules.Add(ScheduleMapper.Map(command.ScheduledMedication));
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ScheduledMedicineDto>> Read(UserId userId, LoadSchedule.Query query)
    {
        var scheduledMedicineDtos = _schedules.AsEnumerable();
        return Task.FromResult(scheduledMedicineDtos);
    }

    private readonly List<ScheduledMedicineDto> _schedules = new();
}