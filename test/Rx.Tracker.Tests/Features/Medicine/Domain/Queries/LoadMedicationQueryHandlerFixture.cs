using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rx.Tracker.Features.Medications.Domain.Queries;
using Rx.Tracker.Mediation.Queries;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Queries;

[AutoFixture(typeof(LoadMedication.QueryHandler))]
internal partial class LoadMedicationQueryHandlerFixture
{
    public IQueryHandler<LoadMedication.Query, LoadMedication.Result> AsInterface() => Build();
}