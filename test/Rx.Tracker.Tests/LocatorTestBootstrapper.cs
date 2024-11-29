using Microsoft.Reactive.Testing;
using NSubstitute;
using ReactiveMarbles.Locator;
using ReactiveMarbles.Mvvm;
using System;
using System.Runtime.CompilerServices;

namespace Rx.Tracker.Tests;

public class LocatorTestBootstrapper
{
    [ModuleInitializer]
    public static void Initialize() =>
        ServiceLocator.Current()
                      .AddService(() => CoreRegistrationBuilder.Create()
                                                               .WithMainThreadScheduler(new TestScheduler())
                                                               .WithTaskPoolScheduler(new TestScheduler())
                                                               .WithExceptionHandler(
                                                                   Substitute.For<IObserver<Exception>>()).Build());
}