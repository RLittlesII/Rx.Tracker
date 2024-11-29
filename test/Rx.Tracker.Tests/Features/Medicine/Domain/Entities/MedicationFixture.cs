using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Features.Medicine.Domain.Entities;
using System.Collections.Generic;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Entities;

internal class MedicationFixture : ITestFixtureBuilder
{
    public static implicit operator Medication(MedicationFixture fixture) => fixture.Build();

    public MedicationFixture WithId(MedicineId id) => this.With(ref _id, id);

    public MedicationFixture WithDosage(Dosage dosage) => this.With(ref _dosages, dosage);

    private Medication Build() => new(this._id, _dosages);

    private MedicineId _id = new();
    private List<Dosage> _dosages = [new()];
}