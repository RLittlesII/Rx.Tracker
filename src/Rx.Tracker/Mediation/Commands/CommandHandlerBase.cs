using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Unit = System.Reactive.Unit;

namespace Rx.Tracker.Mediation.Commands;

/// <inheritdoc />
public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// Executes with the provided <see cref="ICommand" />.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task" /> representing the result of the asynchronous operation.</returns>
    protected abstract Task<Unit> Handle(TCommand command, CancellationToken cancellationToken = default);

    private Task<Unit> ExecuteHandle(TCommand command, CancellationToken cancellationToken) => Handle(command, cancellationToken);

    /// <inheritdoc />
    Task IRequestHandler<TCommand>.Handle(TCommand command, CancellationToken cancellationToken) => ExecuteHandle(command, cancellationToken);
}