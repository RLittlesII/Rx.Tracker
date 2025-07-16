using LanguageExt;
using Rx.Tracker.Failures;
using Rx.Tracker.Mediation.Queries;
using System.Threading.Tasks;

namespace Rx.Tracker.Cqrs;

public interface ISenderz
{
    /// <summary>
    /// Queries with the provided <see cref="IRequest{TResult}"/>.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <returns>A result.</returns>
    Task<Either<Failure, TResult>> Query<TResult>(IQuery<TResult> query);
}