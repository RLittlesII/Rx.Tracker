using System.Collections.Generic;
using System.Linq;
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
        /// <param name="medicines">The medications.</param>
        public Result(IEnumerable<Medication> medicines) => Dosages = medicines
           .SelectMany(medication => medication.Dosages)
           .GroupBy(dosage => dosage.Weight, dosage => dosage)
           .SelectMany(grouping => grouping.DistinctBy(dosage => (Quantity: dosage.Amount, dosage.Weight)))
           .ToArray();

        /// <summary>
        /// Gets the dosages.
        /// </summary>
        public IReadOnlyCollection<Dosage> Dosages { get; }
    }

    /// <summary>
    /// The add medication query handler.
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
    public static Query Create() => new();

    /// <summary>
    /// Creates a <see cref="Result"/>.
    /// </summary>
    /// <param name="medications">The medications.</param>
    /// <returns>A result.</returns>
    public static Result Create(IEnumerable<Medication> medications) => new(medications.ToArray());
}