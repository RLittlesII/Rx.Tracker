using Rx.Tracker.UI.Logging;

namespace Rx.Tracker.UI;

public class IosRegistrations : IPlatformRegistrations
{
    public IPlatformLogger Logger { get; } = new IosLogger();
}