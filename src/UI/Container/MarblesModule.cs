using DryIoc;
using Rx.Tracker.Container;

namespace Rx.Tracker.UI.Container;

public class MarblesModule : ContainerModule
{
    protected override IContainer Register(IContainer registrar) => registrar;
}