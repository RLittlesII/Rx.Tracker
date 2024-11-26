using System.Threading.Tasks;

namespace Rx.Tracker.Core.Mediation.Queries;

/// <summary>
/// Represents an abstraction that sends a query request and returns a result.
/// </summary>
public interface ISender
{
    /// <summary>
    /// Queries with the provided <see cref="IRequest{TResult}"/>.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <typeparam name="TQuery">The request type.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <returns>A result.</returns>
    Task<TResult> Query<TQuery, TResult>(TQuery query)
        where TQuery : IQuery<TResult>;
}