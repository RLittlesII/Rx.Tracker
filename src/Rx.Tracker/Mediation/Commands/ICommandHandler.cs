using MediatR;

namespace Rx.Tracker.Mediation.Commands;

/// <summary>
/// Handles a <see cref="ICommand"/>.
/// </summary>
/// <typeparam name="TCommand">The command type.</typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand;