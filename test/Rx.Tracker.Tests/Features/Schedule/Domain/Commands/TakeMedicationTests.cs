using FluentAssertions;
using LanguageExt;
using NSubstitute;
using Rx.Tracker.Features.Medications.Domain;
using Rx.Tracker.Features.Schedule.Domain.Commands;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Commands;

public class TakeMedicationTests
{
    [Fact]
    public async Task GivenCommandHandler_WhenHandle_ThenDoesNotThrow()
    {
        // Given
        TakeMedication.CommandHandler sut = new TakeMedicationCommandHandlerFixture();

        // When
        var result = await Record.ExceptionAsync(() => sut.Handle(TakeMedication.Create(new ScheduledMedicationFixture())));

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
        TakeMedication.CommandHandler sut = new TakeMedicationCommandHandlerFixture().WithReminders(reminders);

        // When
        var result = await sut.Handle(TakeMedication.Create(new ScheduledMedicationFixture()));

        // Then
        result
           .Should()
           .Be(Unit.Default);
    }
}