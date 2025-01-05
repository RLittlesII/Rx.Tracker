using FluentAssertions;
using LanguageExt;
using NSubstitute;
using Rx.Tracker.Features.Medications.Domain;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Tests.Features.Schedule.Domain.Entities;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Commands;

public class AddMedicationToScheduleTests
{
    [Fact]
    public async Task GivenCommandHandler_WhenHandle_ThenDoesNotThrow()
    {
        // Given
        AddMedicationToSchedule.CommandHandler sut = new AddMedicationToScheduleCommandHandlerFixture();

        // When
        var result = await Record.ExceptionAsync(() => sut.Handle(AddMedicationToSchedule.Create(new ScheduledMedicationFixture())));

        // Then
        result
           .Should()
           .BeNull();
    }

    [Fact]
    public async Task GivenReminders_WhenHandle_ThenReturnsCompletion()
    {
        // Given
        var reminders = Substitute.For<IReminders>();
        AddMedicationToSchedule.CommandHandler sut = new AddMedicationToScheduleCommandHandlerFixture().WithReminders(reminders);

        // When
        var result = await sut.Handle(AddMedicationToSchedule.Create(new ScheduledMedicationFixture()));

        // Then
        result
           .Should()
           .Be(Unit.Default);
    }
}