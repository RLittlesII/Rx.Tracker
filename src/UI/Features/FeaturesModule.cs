using System.Net.Http;
using DryIoc;
using MediatR;
using NodaTime;
using Rx.Tracker.Container;
using Rx.Tracker.Features;
using Rx.Tracker.Mediation;
using Rx.Tracker.Navigation;
using Rx.Tracker.UI.Navigation;

namespace Rx.Tracker.UI.Features;

public class FeaturesModule : ContainerModule
{
    protected override IContainer Register(IContainer registrar)
    {
        registrar.Register<HttpClient>(reuse: Reuse.Singleton);
        registrar.Register<INavigator, Navigator>();
        registrar.RegisterInstance<IClock>(SystemClock.Instance);
        registrar.Register<ICoreServices, CoreServices>();
        registrar.RegisterMany(
            [
                typeof(IMediator).GetAssembly()
            ],
            Registrator.Interfaces);
        registrar.RegisterMany([typeof(ICqrs).GetAssembly()], Registrator.Interfaces, ifAlreadyRegistered: IfAlreadyRegistered.Replace);
        return registrar;
    }
}