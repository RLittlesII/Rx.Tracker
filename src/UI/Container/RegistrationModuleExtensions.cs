using DryIoc;
using Prism.Ioc;
using Rx.Tracker.Container;

namespace Rx.Tracker.UI.Container;

public static class RegistrationModuleExtensions
{
    public static IContainer ContainerModule<TModule>(this IContainer container)
        where TModule : IRegistrationModule<IContainer>, new() => RegistrationModule<IContainer, TModule>(container);

    public static IContainerRegistry ContainerRegistryModule<TModule>(this IContainerRegistry containerRegistry)
        where TModule : IRegistrationModule<IContainerRegistry>, new() => RegistrationModule<IContainerRegistry, TModule>(containerRegistry);

    private static TRegistrar RegistrationModule<TRegistrar, TModule>(this TRegistrar containerRegistry)
        where TModule : IRegistrationModule<TRegistrar>, new() => new TModule().Register(containerRegistry);
}