using MediatR;

namespace Rx.Tracker.Mediation.Queries;

/// <summary>
/// Handles a <see cref="IRequest{TResult}" />.
/// </summary>
/// <typeparam name="TQuery">The request type.</typeparam>
/// <typeparam name="TResult">The result type.</typeparam>
public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>;