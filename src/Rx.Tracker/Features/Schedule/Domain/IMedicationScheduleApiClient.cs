using System.Collections.Generic;
using System.Threading.Tasks;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Queries;

namespace Rx.Tracker.Features.Schedule.Domain;

public interface IMedicationScheduleApiClient
{
    /// <summary>
    /// Get a collection of <see cref="ScheduledMedication"/>.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task<IReadOnlyCollection<ScheduledMedication>> Get(LoadSchedule.Query query);
}