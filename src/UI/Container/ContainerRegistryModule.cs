using Prism.Ioc;

namespace Rx.Tracker.UI.Container;

public abstract class ContainerRegistryModule : IContainerRegistryModule
{
    /// <inheritdoc />
    IContainerRegistry IRegistrationModule<IContainerRegistry>.Register(IContainerRegistry registrar) => RegisterTypes(registrar);

    protected abstract IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry);
}