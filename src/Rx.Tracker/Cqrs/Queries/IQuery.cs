namespace Rx.Tracker.Cqrs;

/// <summary>
/// Marker interface representing a query in the Command and Query Responsibility Segregation (CQRS) pattern.
/// Queries are used to retrieve data or retrieve specific information without modifying state.
/// </summary>
/// <typeparam name="TResult">
/// The type of the result produced by the query.
/// </typeparam>
public interface IQuery<TResult>;