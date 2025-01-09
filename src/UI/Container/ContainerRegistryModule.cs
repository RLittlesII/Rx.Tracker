using Prism.Ioc;
using Rx.Tracker.Container;

namespace Rx.Tracker.UI.Container;

public abstract class ContainerRegistryModule : IRegistrationModule<IContainerRegistry>
{
    /// <inheritdoc />
    IContainerRegistry IRegistrationModule<IContainerRegistry>.Register(IContainerRegistry registrar) => RegisterTypes(registrar);

    protected abstract IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry);
}