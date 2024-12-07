using DryIoc;

namespace Rx.Tracker.UI.Container;

public abstract class ContainerModule : IRegistrationModule<IContainer>
{
    IContainer IRegistrationModule<IContainer>.Register(IContainer registrar) => Register(registrar);

    protected abstract IContainer Register(IContainer registrar);
}