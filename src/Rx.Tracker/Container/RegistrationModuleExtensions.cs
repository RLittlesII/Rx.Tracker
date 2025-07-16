using DryIoc;

namespace Rx.Tracker.Container;

/// <summary>
/// Registration module extensions.
/// </summary>
public static class RegistrationModuleExtensions
{
    /// <summary>
    /// Registers a <see cref="IRegistrationModule{TRegistrar}" /> to the container.
    /// </summary>
    /// <param name="container">The container.</param>
    /// <typeparam name="TModule">The module type.</typeparam>
    /// <returns>The registered container.</returns>
    public static IContainer RegisterModule<TModule>(this IContainer container)
        where TModule : IRegistrationModule<IContainer>, new() => new TModule().Register(container);
}