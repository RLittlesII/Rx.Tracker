using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Rx.Tracker.Mediation;

/// <summary>
/// Represents a request made to the mediator.
/// </summary>
/// <typeparam name="TResult">The result type.</typeparam>
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Generic Typed Interface")]
public interface IRequest<TResult>;

/// <summary>
/// Represents a request made to the mediator.
/// </summary>
public interface IRequest : IRequest<Unit>;