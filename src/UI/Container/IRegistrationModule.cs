namespace Rx.Tracker.UI.Container;

/// <summary>
/// Represents a container registration module.
/// </summary>
/// <typeparam name="TRegistrar">The registrar type.</typeparam>
public interface IRegistrationModule<TRegistrar>
{
    public TRegistrar Register(TRegistrar registrar);
}