using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Mediation.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rx.Tracker.Features.Medications.Domain.Queries;

/// <summary>
/// The load medicine query definition.
/// </summary>
public static class LoadMedicine
{
    /// <summary>
    /// Load medicine query.
    /// </summary>
    public record Query : IQuery<Result>;

    /// <summary>
    /// Load medicine query.
    /// </summary>
    public record Result(IReadOnlyCollection<Medication> Medicines);

    /// <summary>
    /// The add medicine query handler.
    /// </summary>
    public class QueryHandler : IQueryHandler<Query, Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandler"/> class.
        /// </summary>
        /// <param name="client">The api contract.</param>
        public QueryHandler(IMedicineApiClient client) => _client = client;

        /// <inheritdoc/>
        public async Task<Result> Handle(Query query)
        {
            var things = await _client.Get();

            return new Result(things);
        }

        private readonly IMedicineApiClient _client;
    }

    /// <summary>
    /// Creates a <see cref="Query"/>.
    /// </summary>
    /// <returns>A query.</returns>
    public static Query Create() => new Query();

    /// <summary>
    /// Creates a <see cref="Result"/>.
    /// </summary>
    /// <param name="medications">The medications.</param>
    /// <returns>A result.</returns>
    public static Result Create(IEnumerable<Medication> medications) => new Result(medications.ToArray());
}