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
        if (_schedules.TryGetValue(userId, out var medications))
        {
            medications = [];
            _schedules.Add(userId, medications);
        }

        medications?.Add(ScheduleMapper.Map(command.ScheduledMedication));
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ScheduledMedicineDto>> Read(UserId userId, LoadSchedule.Query query) =>
        Task.FromResult(_schedules.TryGetValue(userId, out var dtos) ? dtos.AsEnumerable() : []);

    private readonly Dictionary<UserId, List<ScheduledMedicineDto>> _schedules = new();
}