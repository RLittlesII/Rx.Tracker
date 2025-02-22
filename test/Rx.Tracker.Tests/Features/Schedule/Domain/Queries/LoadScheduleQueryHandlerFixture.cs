using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using Rx.Tracker.Mediation.Queries;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Queries;

[AutoFixture(typeof(LoadSchedule.QueryHandler))]
internal partial class LoadScheduleQueryHandlerFixture
{
    public IQueryHandler<LoadSchedule.Query, LoadSchedule.Result> AsInterface() => Build();
}