using NSubstitute;
using Rx.Tracker.Features.Medications.Domain;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Commands;

public class AddMedicationToScheduleTests
{
    [Fact]
    public async Task GivenReminders_WhenHandle_ThenCallsCreate()
    {
        // Given
        var reminders = Substitute.For<IReminders>();
        AddMedicationToSchedule.CommandHandler sut = new AddMedicationToScheduleCommandHandlerFixture().WithReminders(reminders);

        // When
        await sut.Handle(AddMedicationToSchedule.Create(new ScheduledMedicationFixture()));

        // Then
        await reminders.Received(1).Create(Arg.Any<MedicationReminder>());
    }
}