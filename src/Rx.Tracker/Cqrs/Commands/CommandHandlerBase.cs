using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Rx.Tracker.Failures;

namespace Rx.Tracker.Cqrs;

/// <summary>
/// Represents the base class for handling commands implementing the <see cref="ICommandHandler{TCommand}"/> interface.
/// Provides abstraction for handling command execution with failure handling capabilities.
/// </summary>
/// <typeparam name="TCommand">The type of the command to be handled. Must implement <see cref="ICommand"/>.</typeparam>
public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    /// <inheritdoc/>
    Task<Either<Failure, Unit>> ICommandHandler<TCommand>.Handle(TCommand command, CancellationToken cancellationToken) => Handle(command, cancellationToken);

    /// <summary>
    /// Handler the request and provide a completion.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns either the result or a failure.</returns>
    protected abstract Task<Either<Failure, Unit>> Handle(TCommand command, CancellationToken cancellationToken = default);
}