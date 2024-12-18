using System.Threading.Tasks;
using Rx.Tracker.Features.Schedule.Data.Api;
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
    /// <param name="UserId">The user id.</param>
    public record Query(UserId UserId) : IQuery<Result>;

    /// <summary>
    /// Load schedule query.
    /// </summary>
    public record Result;

    /// <summary>
    /// The load schedule query handler.
    /// </summary>
    public class QueryHandler : IQueryHandler<Query, Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandler"/> class.
        /// </summary>
        /// <param name="apiContract">The api contract.</param>
        public QueryHandler(IScheduleApiContract apiContract) => _apiContract = apiContract;

        /// <inheritdoc/>
        public Task<Result> Handle(Query query) => Task.FromResult(new Result());

        private readonly IScheduleApiContract _apiContract;
    }

    /// <summary>
    /// Creates a <see cref="Query"/>.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>A query.</returns>
    public static Query Create(UserId userId) => new(userId);
}