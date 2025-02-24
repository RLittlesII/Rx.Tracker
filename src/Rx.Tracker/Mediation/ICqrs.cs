using Rx.Tracker.Mediation.Commands;
using Rx.Tracker.Mediation.Notifications;
using Rx.Tracker.Mediation.Queries;
using System.Diagnostics.CodeAnalysis;

namespace Rx.Tracker.Mediation;

/// <summary>
/// Interface representing a mediator implementation.
/// </summary>
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Marker Interface")]
public interface ICqrs : ICommander, ISender, IPublisher;