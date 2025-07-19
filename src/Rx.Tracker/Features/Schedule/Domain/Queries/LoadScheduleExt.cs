using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using NodaTime;
using Rx.Tracker.Cqrs;
using Rx.Tracker.Failures;
using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.Features.Schedule.Domain.Queries;

/// <summary>
/// Provides functionality for loading a schedule by user and date.
/// </summary>
public static class LoadScheduleExt
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
    public class QueryHandler : QueryHandlerBase<Query, Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandler"/> class.
        /// </summary>
        /// <param name="apiClient">The api client.</param>
        public QueryHandler(IMedicationScheduleApiClientExt apiClient) => _apiClient = apiClient;

        /// <inheritdoc />
        protected override async Task<Either<Failure, Result>> Handle(Query query, CancellationToken cancellationToken = default)
        {
            var medicationSchedule = await _apiClient.Get(query);
            return medicationSchedule.Map<Result>(schedule => new Result(schedule));
        }

        private readonly IMedicationScheduleApiClientExt _apiClient;
    }
}