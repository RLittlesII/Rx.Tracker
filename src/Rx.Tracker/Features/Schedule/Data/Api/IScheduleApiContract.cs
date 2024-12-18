using System.Threading.Tasks;
using Refit;
using Rx.Tracker.Features.Schedule.Data.Dto;

namespace Rx.Tracker.Features.Schedule.Data.Api;

/// <summary>
/// Interface representing the <see cref="ScheduleDto"/> api contract.
/// </summary>
public interface IScheduleApiContract
{
    /// <summary>
    /// Get a set of <see cref="ScheduleDto"/>.
    /// </summary>
    /// <param name="userId">The userId.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Get("/schedule/{id}")]
    Task<ScheduleDto> Get([AliasAs("id")] string userId);
}