using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Mediation.Queries;

namespace Rx.Tracker.Features.Medications.Domain.Queries;

/// <summary>
/// The load medication query definition.
/// </summary>
public static class LoadMedication
{
    /// <summary>
    /// Load medication query.
    /// </summary>
    public record Query : IQuery<Result>;

    /// <summary>
    /// Load medication query.
    /// </summary>
    public record Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="medications">The medications.</param>
        public Result(IEnumerable<Medication> medications) => Medications = new MedicationCollection(medications);

        /// <summary>
        /// Gets the medications.
        /// </summary>
        public MedicationCollection Medications { get; }
    }

    /// <summary>
    /// The add medication query handler.
    /// </summary>
    public class QueryHandler : QueryHandlerBase<Query, Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandler"/> class.
        /// </summary>
        /// <param name="client">The api contract.</param>
        public QueryHandler(IMedicineApiClient client) => _client = client;

        /// <inheritdoc />
        protected override async Task<Result> Handle(Query query, CancellationToken cancellationToken = default)
        {
            var medications = await _client.Get();

            return new Result(medications); // TODO: [rlittlesii: January 26, 2025] source generate result extension methods .ContinueWith(task => new Result(task.Result)).
        }

        private readonly IMedicineApiClient _client;
    }

    /// <summary>
    /// Creates a <see cref="Query"/>.
    /// </summary>
    /// <returns>A query.</returns>
    public static Query Create() => new();

    /// <summary>
    /// Creates a <see cref="Result"/>.
    /// </summary>
    /// <param name="medications">The medications.</param>
    /// <returns>A result.</returns>
    public static Result Create(IEnumerable<Medication> medications) => new(medications.ToArray());
}