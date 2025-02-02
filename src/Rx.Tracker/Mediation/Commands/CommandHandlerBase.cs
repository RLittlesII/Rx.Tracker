using System.Threading.Tasks;
using LanguageExt;

namespace Rx.Tracker.Mediation.Commands;

/// <inheritdoc />
public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    /// <inheritdoc/>
    Task<Unit> ICommandHandler<TCommand>.Handle(TCommand command) =>  Handle(command);

    /// <summary>
    /// Executes with the provided <see cref="ICommand"/>.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    protected abstract Task<Unit> Handle(TCommand command);
}