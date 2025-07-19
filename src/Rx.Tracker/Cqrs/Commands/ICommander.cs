using System.Threading.Tasks;
using LanguageExt;
using Rx.Tracker.Failures;
using Rx.Tracker.Mediation.Commands;

namespace Rx.Tracker.Cqrs;

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
    Task<Either<Failure, Unit>> Execute<TCommand>(TCommand command)
        where TCommand : ICommand;
}