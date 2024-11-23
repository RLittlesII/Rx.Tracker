using Prism.Ioc;

namespace Rx.Tracker.UI.Container;

internal static class ContainerRegistryModuleExtensions
{
    public static IContainerRegistry RegisterModule<TModule>(this IContainerRegistry containerRegistry)
        where TModule : IContainerRegistryModule, new() => new TModule().Register(containerRegistry);
}