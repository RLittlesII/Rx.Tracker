using System.Net.Http;
using DryIoc;
using Rx.Tracker.UI.Container;

namespace Rx.Tracker.UI.Features;

public class FeaturesModule : ContainerModule
{
    protected override IContainer Register(IContainer registrar)
    {
        registrar.Register<HttpClient>(reuse: Reuse.Singleton);

        return registrar;
    }
}