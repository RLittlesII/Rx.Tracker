using Rocket.Surgery.Extensions.Testing.AutoFixtures;
using Rx.Tracker.Features.Schedule.Domain.Commands;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Commands;

[AutoFixture(typeof(TakeMedication.CommandHandler))]
internal partial class TakeMedicationCommandHandlerFixture;