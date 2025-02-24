using Prism.Ioc;
using Rx.Tracker.Container;

namespace Rx.Tracker.UI.Container;

public abstract class ContainerRegistryModule : IRegistrationModule<IContainerRegistry>
{
    protected abstract IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry);

    /// <inheritdoc />
    IContainerRegistry IRegistrationModule<IContainerRegistry>.Register(IContainerRegistry registrar) => RegisterTypes(registrar);
}