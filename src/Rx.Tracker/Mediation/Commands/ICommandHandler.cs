using System.Threading.Tasks;
using LanguageExt;

namespace Rx.Tracker.Mediation.Commands;

/// <summary>
/// Handles a <see cref="ICommand"/>.
/// </summary>
/// <typeparam name="TCommand">The command type.</typeparam>
public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// Executes with the provided <see cref="ICommand"/>.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task<Unit> Handle(TCommand command);
}