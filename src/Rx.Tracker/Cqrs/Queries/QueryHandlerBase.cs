using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Rx.Tracker.Failures;

namespace Rx.Tracker.Cqrs;

/// <summary>
/// Base class for handling query requests in a CQRS architecture. It provides an abstract method for handling
/// queries of type <typeparamref name="TQuery"/> and returning a result of type <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="TQuery">The type of the query to handle. Must implement <see cref="IQuery{TResult}"/>.</typeparam>
/// <typeparam name="TResult">The type of the result produced by handling the query.</typeparam>
public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    /// <inheritdoc/>
    Task<Either<Failure, TResult>> IQueryHandler<TQuery, TResult>.Handle(TQuery query, CancellationToken cancellationToken) => Handle(query, cancellationToken);

    /// <summary>
    /// Handler the request and provide a valid response.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns either the result or a failure.</returns>
    protected abstract Task<Either<Failure, TResult>> Handle(TQuery query, CancellationToken cancellationToken = default);
}