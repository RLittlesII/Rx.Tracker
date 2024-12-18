using System.Threading.Tasks;
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
        /// <inheritdoc/>
        public Task<Result> Handle(Query query) => Task.FromResult(new Result());
    }

    /// <summary>
    /// Creates a <see cref="Query"/>.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>A query.</returns>
    public static Query Create(UserId userId) => new(userId);
}