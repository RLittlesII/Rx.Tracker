using DryIoc;

namespace Rx.Tracker.Container;

/// <inheritdoc />
public abstract class ContainerModule : IRegistrationModule<IContainer>
{
    /// <inheritdoc/>
    IContainer IRegistrationModule<IContainer>.Register(IContainer registrar) => Register(registrar);

    /// <summary>
    /// Register dependencies against the registrar.
    /// </summary>
    /// <param name="registrar">The registrar.</param>
    /// <returns>The container.</returns>
    protected abstract IContainer Register(IContainer registrar);
}