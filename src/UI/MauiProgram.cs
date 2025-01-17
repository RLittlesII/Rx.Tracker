using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using DryIoc;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Prism;
using Prism.Container.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using ReactiveMarbles.Locator;
using ReactiveMarbles.Mvvm;
using ReactiveUI;
using Rocket.Surgery.Airframe.Exceptions;
using Rx.Tracker.Container;
using Rx.Tracker.Navigation;
using Rx.Tracker.UI.Container;
using Rx.Tracker.UI.Exceptions.Handlers;
using Rx.Tracker.UI.Features;
using Rx.Tracker.UI.Features.Main;
using Rx.Tracker.UI.Features.Medicine;
using Rx.Tracker.UI.Features.Schedule;

[assembly: SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "MAUI elements don't need to be documented.")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1601:Partial elements should be documented",
    Justification = "MAUI elements don't need to be documented.")]
[assembly: SuppressMessage("ReSharper", "ArrangeThisQualifier", Justification = "Resharper won't turn it of!")]

namespace Rx.Tracker.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp(IPlatformRegistrations platformRegistrations) => MauiApp.CreateBuilder()
       .UseAirframe(ContainerRegistry.GetContainer(), BuildAirframe())
       .ConfigureFonts(FontDelegate)
       .UseMauiApp<App>()
       .UseMauiCommunityToolkit()
       .UseMauiCommunityToolkitMarkup()
       .UsePrism((IContainerExtension)ContainerRegistry, ConfigurePrism())
       .UseSerilog(ContainerRegistry.GetContainer(), platformRegistrations.Logger.ConfigureLogger(Guid.NewGuid()).CreateLogger())
       .Build();

    private static Action<AirframeBuilder> BuildAirframe() => builder
        => builder.AddGlobalExceptionHandler<GlobalExceptionHandler>(registrar => registrar.AddHandler<LoggingExceptionHandler>());

    private static Action<PrismAppBuilder> ConfigurePrism() => configuration => configuration
       .CreateWindow(CreateWindow)
       .OnInitialized(InitializeMarbles)
       .RegisterTypes(RegisterTypes);

    private static Task CreateWindow(IContainerProvider containerProvider, INavigationService navigationService)
        => containerProvider.Resolve<INavigator>().Navigate<Routes>(routes => routes.Schedule);

    private static void FontDelegate(IFontCollection fonts) => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
       .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

    private static void InitializeMarbles(IContainerProvider containerProvider)
    {
        var container = containerProvider.GetContainer();
        var exceptionHandler = container.Resolve<IObserver<Exception>>();

        var coreRegistration = CoreRegistrationBuilder
           .Create()
           .WithMainThreadScheduler(RxApp.MainThreadScheduler)
           .WithTaskPoolScheduler(TaskPoolScheduler.Default)
           .WithExceptionHandler(exceptionHandler)
           .Build();

        ServiceLocator
           .Current()
           .AddCoreRegistrations(() => coreRegistration);

        container.RegisterInstance(coreRegistration);
    }

    private static void RegisterTypes(IContainerRegistry registrar) => registrar
       .RegisterModule<MainModule>()
       .RegisterModule<MedicationModule>()
       .RegisterModule<ScheduleModule>()
       .GetContainer()
       .RegisterModule<FeaturesModule>()
       .RegisterModule<MarblesModule>()
       .RegisterModule<Tracker.Features.Schedule.Container.ScheduleModule>()
       .RegisterModule<Tracker.Features.Medications.Container.AddMedicineModule>();

    private static readonly IContainerRegistry ContainerRegistry = new DryIocContainerExtension(TrackerContainer.Rules);
}