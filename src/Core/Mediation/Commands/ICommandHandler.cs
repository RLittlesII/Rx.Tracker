// <copyright file="IMediatorCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace Rx.Tracker.Core.Mediation.Commands;

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
    Task Handle(TCommand command);
}