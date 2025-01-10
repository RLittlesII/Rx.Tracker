using System.Threading.Tasks;
using Rx.Tracker.Features.Schedule.Domain;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Queries;

namespace Rx.Tracker.Features.Schedule.Data.Api;

/// <inheritdoc />
public class MedicationScheduleClient : IMedicationScheduleApiClient
{
    /// <inheritdoc/>
    public Task<MedicationSchedule> Get(LoadSchedule.Query query) => Task.FromResult<MedicationSchedule>(default);
}