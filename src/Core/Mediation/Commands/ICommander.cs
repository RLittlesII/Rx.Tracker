using System.Threading.Tasks;

namespace Rx.Tracker.Core.Mediation.Commands;

/// <summary>
/// Represents an abstraction that executes a command.
/// </summary>
public interface ICommander
{
    /// <summary>
    /// Executes with the provided <see cref="ICommand"/>.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <typeparam name="TCommand">The command type.</typeparam>
    /// <returns>A completion notification.</returns>
    Task Execute<TCommand>(TCommand command)
        where TCommand : ICommand;
}