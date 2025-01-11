using DryIoc;
using Rx.Tracker.Container;

namespace Rx.Tracker.UI;

public class IosRegistrar : IRegistrationModule<IContainer>
{
    public IContainer Register(IContainer registrar) => registrar;
}