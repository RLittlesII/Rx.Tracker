using System.Reactive.Concurrency;
using DryIoc;
using ReactiveMarbles.Locator;
using ReactiveMarbles.Mvvm;
using ReactiveUI;
using Rx.Tracker.Container;

namespace Rx.Tracker.UI.Container;

public class MarblesModule : ContainerModule
{
    protected override IContainer Register(IContainer registrar)
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

        registrar.RegisterInstance(coreRegistration);

        return registrar;
    }
}