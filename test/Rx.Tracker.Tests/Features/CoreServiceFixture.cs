using NodaTime;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rx.Tracker.Features;
using System;

namespace Rx.Tracker.Tests.Features;

[AutoFixture(typeof(CoreServices))]
internal partial class CoreServicesFixture
{
    public ICoreServices AsInterface() => Build();
}

internal class CoreServicesStub
{
    public static ICoreServices Instance(DateTimeOffset? dateTimeOffset = null) => GetInstance(dateTimeOffset);

    private static ICoreServices GetInstance(DateTimeOffset? dateTimeOffset = null)
    {
        var iClock = Substitute.For<IClock>();
        iClock.GetCurrentInstant().Returns(Instant.FromDateTimeOffset(dateTimeOffset ?? DateTimeOffset.UnixEpoch));
        return new CoreServicesFixture().WithClock(iClock).AsInterface();
    }
}