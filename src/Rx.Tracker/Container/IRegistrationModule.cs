namespace Rx.Tracker.Container;

/// <summary>
/// Represents a container registration module.
/// </summary>
/// <typeparam name="TRegistrar">The registrar type.</typeparam>
public interface IRegistrationModule<TRegistrar>
{
    /// <summary>
    /// Register dependencies against the <see cref="TRegistrar" />.
    /// </summary>
    /// <param name="registrar">The registrar.</param>
    /// <returns>The <see cref="TRegistrar" />.</returns>
    public TRegistrar Register(TRegistrar registrar);
}