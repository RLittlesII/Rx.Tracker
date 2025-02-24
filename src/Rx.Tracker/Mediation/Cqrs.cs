using MediatR;
using Rx.Tracker.Mediation.Commands;
using Rx.Tracker.Mediation.Queries;
using System.Threading.Tasks;
using INotification = Rx.Tracker.Mediation.Notifications.INotification;

namespace Rx.Tracker.Mediation;

/// <summary>
/// Represents the <see cref="ICqrs" />.
/// </summary>
public sealed class Cqrs : ICqrs
{
    /// <inheritdoc cref="ICommander" />
    public Task Execute<TCommand>(TCommand command)
        where TCommand : ICommand => _mediator.Send(command);

    /// <inheritdoc cref="Notifications.IPublisher" />
    public Task Publish<TNotification>(TNotification notification)
        where TNotification : INotification => Task.CompletedTask;

    /// <inheritdoc cref="Queries.ISender" />
    public Task<TResult> Query<TResult>(IQuery<TResult> query) => _mediator.Send(query);

    /// <summary>
    /// Initializes a new instance of the <see cref="Cqrs" /> class.
    /// </summary>
    /// <param name="mediator">A mediator.</param>
    public Cqrs(IMediator mediator) => _mediator = mediator;

    private readonly IMediator _mediator;
}