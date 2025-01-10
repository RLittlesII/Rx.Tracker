using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Rx.Tracker.Mediation.Queries;

/// <inheritdoc />
public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    /// <inheritdoc/>
    Task<TResult> IRequestHandler<TQuery, TResult>.Handle(TQuery request, CancellationToken cancellationToken) => ExecuteHandle(request, cancellationToken);

    /// <summary>
    /// Handler the request and provide a valid response.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result.</returns>
    protected abstract Task<TResult> Handle(TQuery query, CancellationToken cancellationToken = default);

    private Task<TResult> ExecuteHandle(TQuery query, CancellationToken cancellationToken) => Handle(query, cancellationToken);
}