using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Features.Medications.Domain.Entities;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Entities;

internal class DosageFixture : ITestFixtureBuilder
{
    public static implicit operator Dosage(DosageFixture fixture) => fixture.Build();

    private Dosage Build() => new(DosageType.Chewable, 100, DosageWeight.Milligrams);
}