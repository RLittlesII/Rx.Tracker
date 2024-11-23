using Prism.Ioc;
using Shiny.Stores;

namespace Rx.Tracker.UI.Container;

public class ShinyModule : ContainerRegistryModule
{
    /// <inheritdoc />
    protected override IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry)
        => containerRegistry.RegisterSingleton<IKeyValueStore, MemoryKeyValueStore>();
}