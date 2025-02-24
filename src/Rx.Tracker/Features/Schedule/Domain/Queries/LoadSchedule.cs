using NodaTime;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Mediation.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Rx.Tracker.Features.Schedule.Domain.Queries;

/// <summary>
/// The load schedule query definition.
/// </summary>
public static class LoadSchedule
{
    /// <summary>
    /// Creates a <see cref="Query" />.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="date">The date.</param>
    /// <returns>A query.</returns>
    public static Query Create(UserId userId, LocalDate date) => new(userId, date);

    /// <summary>
    /// The load schedule query handler.
    /// </summary>
    public class QueryHandler : QueryHandlerBase<Query, Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandler" /> class.
        /// </summary>
        /// <param name="apiClient">The api client.</param>
        public QueryHandler(IMedicationScheduleApiClient apiClient) => _apiClient = apiClient;

        /// <inheritdoc />
        protected override async Task<Result> Handle(Query query, CancellationToken cancellationToken = default)
        {
            var medicationSchedule = await _apiClient.Get(query);
            return new Result(medicationSchedule);
        }

        private readonly IMedicationScheduleApiClient _apiClient;
    }

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
}