using System.Net.Http;
using DryIoc;
using Rx.Tracker.Container;
using Rx.Tracker.Mediation;
using Rx.Tracker.Navigation;
using Rx.Tracker.UI.Navigation;

namespace Rx.Tracker.UI.Features;

public class FeaturesModule : ContainerModule
{
    protected override IContainer Register(IContainer registrar)
    {
        registrar.Register<HttpClient>(reuse: Reuse.Singleton);
        registrar.Register<ICqrs, Cqrs>();
        registrar.Register<INavigator, Navigator>(reuse: Reuse.Scoped);

        return registrar;
    }
}