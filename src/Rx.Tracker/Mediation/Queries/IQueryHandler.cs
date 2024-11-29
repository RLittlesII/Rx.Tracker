using System.Threading.Tasks;

namespace Rx.Tracker.Mediation.Queries;

/// <summary>
/// Handles a <see cref="IRequest{TResult}"/>.
/// </summary>
/// <typeparam name="TQuery">The request type.</typeparam>
/// <typeparam name="TResult">The result type.</typeparam>
public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    /// <summary>
    /// Handler the request and provide a valid response.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>The result.</returns>
    Task<TResult> Handle(TQuery query);
}