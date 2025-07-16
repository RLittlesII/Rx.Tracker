using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using MediatR;
using Rx.Tracker.Failures;
using Rx.Tracker.Mediation.Queries;

namespace Rx.Tracker.Cqrs;

/// <summary>
/// Handles a <see cref="IRequest{TResult}"/>.
/// </summary>
/// <typeparam name="TQuery">The request type.</typeparam>
/// <typeparam name="TResult">The result type.</typeparam>
public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    /// <summary>
    /// Handles the specified query and returns a result wrapped in an <see cref="Either{TLeft, TRight}"/> instance.
    /// </summary>
    /// <param name="query">The query to be handled.</param>
    /// <param name="cancellationToken">A token that may be used to cancel the operation.</param>
    /// <returns>
    /// An <see cref="Either{TLeft, TRight}"/> where the left side represents a <see cref="Failure"/>
    /// and the right side represents the result of the query.
    /// </returns>
    Task<Either<Failure, TResult>> Handle(TQuery query, CancellationToken cancellationToken = default);
}