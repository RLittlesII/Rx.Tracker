using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Features.Schedule.Data.Dto;
using Rx.Tracker.Features.Schedule.Domain.Queries;

namespace Rx.Tracker.Features.Schedule.Data.Api;

/// <summary>
/// Interface representing the <see cref="MedicationScheduleDto"/> api contract.
/// </summary>
public interface IMedicationScheduleApiContract
{
    /// <summary>
    /// Creates and instance of a <see cref="ScheduledMedicineDto"/>.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="command">The command.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    // [Post("/schedule/{id}/add")]
    Task Create([AliasAs("id")] UserId userId, [Body] AddMedicationToSchedule.Command command);

    /// <summary>
    /// Get a set of <see cref="MedicationScheduleDto"/>.
    /// </summary>
    /// <param name="userId">The userId.</param>
    /// <param name="query">The query.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    // [Get("/schedule/{id}")]
    Task<IEnumerable<ScheduledMedicineDto>> Read([AliasAs("id")] UserId userId, [Query] LoadSchedule.Query query);
}