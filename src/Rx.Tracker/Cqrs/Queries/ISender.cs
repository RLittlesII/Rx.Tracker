using System.Threading.Tasks;
using LanguageExt;
using Rx.Tracker.Failures;
using Rx.Tracker.Mediation.Queries;

namespace Rx.Tracker.Cqrs;

/// <summary>
/// Represents an abstraction that sends a query in a CQRS-based system.
/// </summary>
/// <remarks>
/// This interface provides a contract for executing queries by encapsulating the logic
/// required to process requests and return results in a consistent format, leveraging
/// the Either type for handling potential failures.
/// </remarks>
public interface ISender
{
    /// <summary>
    /// Queries with the provided <see cref="IRequest{TResult}"/>.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <returns>A result.</returns>
    Task<Either<Failure, TResult>> Query<TResult>(IQuery<TResult> query);
}