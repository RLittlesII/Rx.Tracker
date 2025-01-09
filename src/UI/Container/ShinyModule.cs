using DryIoc;
using Rx.Tracker.Container;
using Shiny.Mediator;
using Shiny.Mediator.Infrastructure.Impl;

namespace Rx.Tracker.UI.Container;

public class ShinyModule : ContainerModule
{
    /// <inheritdoc />
    protected override IContainer Register(IContainer registrar)
    {
        registrar.Register<IMediator, Mediator>();
        return registrar;
    }
}