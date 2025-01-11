using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Prism;
using Prism.Container.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
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
       .UseMauiApp<App>()
       .UseMauiCommunityToolkitMarkup()
       .UseAirframe(ContainerRegistry.GetContainer(), BuildAirframe())
       .UsePrism((IContainerExtension)ContainerRegistry, ConfigurePrism())
       .UseSerilog(ContainerRegistry.GetContainer(), platformRegistrations.Logger.ConfigureLogger(Guid.NewGuid()).CreateLogger())
       .ConfigureFonts(FontDelegate)
       .Build();

    private static Action<AirframeBuilder> BuildAirframe() => builder
        => builder.AddGlobalExceptionHandler<GlobalExceptionHandler>(registrar => registrar.AddHandler<LoggingExceptionHandler>());

    private static Action<PrismAppBuilder> ConfigurePrism() => configuration => configuration
       .CreateWindow(CreateWindow)
       .OnInitialized(Initialize)
       .RegisterTypes(RegisterTypes);

    private static Task CreateWindow(IContainerProvider containerProvider, INavigationService navigationService) => containerProvider.Resolve<INavigator>().Navigate<Routes>(routes => routes.AddMedicine);

    private static void FontDelegate(IFontCollection fonts) => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
       .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

    private static void Initialize(IContainerProvider containerProvider) => containerProvider.GetContainer();

    private static void RegisterTypes(IContainerRegistry registrar) => registrar
       .RegisterModule<MainModule>()
       .RegisterModule<MedicationModule>()
       .RegisterModule<ScheduleModule>()
       .GetContainer()
       .ContainerModule<FeaturesModule>()
       .ContainerModule<MarblesModule>()
       .ContainerModule<Tracker.Features.Schedule.Container.ScheduleModule>()
       .ContainerModule<Tracker.Features.Medications.Container.AddMedicineModule>();

    private static readonly IContainerRegistry ContainerRegistry = new DryIocContainerExtension();
}