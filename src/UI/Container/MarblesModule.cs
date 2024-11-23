using System.Reactive.Concurrency;
using Prism.Ioc;
using ReactiveMarbles.Locator;
using ReactiveMarbles.Mvvm;
using ReactiveUI;

namespace Rx.Tracker.UI.Container;

public class MarblesModule : ContainerRegistryModule
{
    protected override IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry)
    {
        var coreRegistration = CoreRegistrationBuilder
           .Create()
           .WithMainThreadScheduler(RxApp.MainThreadScheduler)
           .WithTaskPoolScheduler(TaskPoolScheduler.Default)
           .WithExceptionHandler(new DebugExceptionHandler())
           .Build();

        ServiceLocator
           .Current()
           .AddCoreRegistrations(() => coreRegistration);

        return containerRegistry.RegisterSingleton<ICoreRegistration>(_ => coreRegistration);
    }
}