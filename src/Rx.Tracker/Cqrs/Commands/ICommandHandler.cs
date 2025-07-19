using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Rx.Tracker.Failures;

namespace Rx.Tracker.Cqrs;

/// <summary>
/// Handles a <see cref="ICommand"/>.
/// </summary>
/// <typeparam name="TCommand">The command type.</typeparam>
public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// Handles a command.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response from the command.</returns>
    Task<Either<Failure, Unit>> Handle(TCommand command, CancellationToken cancellationToken);
}