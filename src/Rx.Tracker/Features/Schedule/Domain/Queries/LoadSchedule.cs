using System.Threading.Tasks;
using NodaTime;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Mediation.Queries;

namespace Rx.Tracker.Features.Schedule.Domain.Queries;

/// <summary>
/// The load schedule query definition.
/// </summary>
public static class LoadSchedule
{
    /// <summary>
    /// Load schedule query.
    /// </summary>
    /// <param name="User">The user id.</param>
    /// <param name="Date">The date for the schedule.</param>
    public record Query(UserId User, LocalDate Date) : IQuery<Result>;

    /// <summary>
    /// Load schedule query.
    /// </summary>
    public record Result(MedicationSchedule Schedule);

    /// <summary>
    /// The load schedule query handler.
    /// </summary>
    public class QueryHandler : IQueryHandler<Query, Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandler"/> class.
        /// </summary>
        /// <param name="apiClient">The api client.</param>
        public QueryHandler(IMedicationScheduleApiClient apiClient) => _apiClient = apiClient;

        /// <inheritdoc/>
        public async Task<Result> Handle(Query query)
        {
            var medicationSchedule = await _apiClient.Get(query);
            return new Result(medicationSchedule);
        }

        private readonly IMedicationScheduleApiClient _apiClient;
    }

    /// <summary>
    /// Creates a <see cref="Query"/>.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="date">The date.</param>
    /// <returns>A query.</returns>
    public static Query Create(UserId userId, LocalDate date) => new(userId, date);
}