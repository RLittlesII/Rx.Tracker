using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Rx.Tracker.Features.Schedule.Data.Api;
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
    public record Query(UserId User, DateTimeOffset Date) : IQuery<Result>;

    /// <summary>
    /// Load schedule query.
    /// </summary>
    public record Result(MedicationSchedule Schedule, IReadOnlyDictionary<DayOfWeek, DateOnly> Week);

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
        public Task<Result> Handle(Query query) => Task.FromResult(new Result(new MedicationSchedule([]), ReadOnlyDictionary<DayOfWeek, DateOnly>.Empty));

        private readonly IScheduleApiContract _apiContract;
    }

    /// <summary>
    /// Creates a <see cref="Query"/>.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="date">The date.</param>
    /// <returns>A query.</returns>
    public static Query Create(UserId userId, DateTimeOffset date) => new(userId, date);
}