using Prism.Ioc;
using Rx.Tracker.Container;

namespace Rx.Tracker.UI.Container;

public static class ContainerRegistryModuleExtensions
{
    public static IContainerRegistry RegisterModule<TModule>(this IContainerRegistry containerRegistry)
        where TModule : IRegistrationModule<IContainerRegistry>, new() => new TModule().Register(containerRegistry);
}