using System.Diagnostics.CodeAnalysis;
using NodaTime;
using ReactiveMarbles.Mvvm;

namespace Rx.Tracker.Features;

/// <summary>
/// Interface representing core services needed by the application.
/// </summary>
public interface ICoreServices
{
    /// <summary>
    /// Gets the core registrations.
    /// </summary>
    ICoreRegistration CoreRegistration { get; }

    /// <summary>
    /// Gets the clock.
    /// </summary>
    IClock Clock { get; }
}

/// <inheritdoc />
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Implementation by interface")]
public class CoreServices : ICoreServices
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CoreServices"/> class.
    /// </summary>
    /// <param name="coreRegistration">The core registrations.</param>
    /// <param name="clock">The clock.</param>
    public CoreServices(ICoreRegistration coreRegistration, IClock clock)
    {
        CoreRegistration = coreRegistration;
        Clock = clock;
    }

    /// <inheritdoc/>
    public ICoreRegistration CoreRegistration { get; }

    /// <inheritdoc/>
    public IClock Clock { get; }
}