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
        where TCommand : ICommand => throw new System.NotImplementedException();

    /// <inheritdoc cref="IPublisher"/>
    public Task Publish<TNotification>(TNotification notification)
        where TNotification : INotification => throw new System.NotImplementedException();

    /// <inheritdoc cref="ISender"/>
    public Task<TResult> Query<TResult>(IQuery<TResult> query) => throw new System.NotImplementedException();
}