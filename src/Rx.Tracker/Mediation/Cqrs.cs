using System.Threading.Tasks;
using Rx.Tracker.Mediation.Commands;
using Rx.Tracker.Mediation.Notifications;
using Rx.Tracker.Mediation.Queries;

namespace Rx.Tracker.Mediation;

/// <summary>
/// Represents the <see cref="ICqrs"/>.
/// </summary>
public sealed class Cqrs : ICqrs
{
    /// <inheritdoc cref="ICommander"/>
    public Task Execute<TCommand>(TCommand command)
        where TCommand : ICommand => Task.CompletedTask;

    /// <inheritdoc cref="IPublisher"/>
    public Task Publish<TNotification>(TNotification notification)
        where TNotification : INotification => Task.CompletedTask;

    /// <inheritdoc cref="ISender"/>
    public Task<TResult> Query<TResult>(IQuery<TResult> query) => Task.FromResult(default(TResult));
}